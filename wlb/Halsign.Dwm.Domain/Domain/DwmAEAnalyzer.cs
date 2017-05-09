using Halsign.DWM.Collectors;
using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Halsign.DWM.Domain
{
	internal class DwmAEAnalyzer : DwmAEBase
	{
		private DwmPoolCollection _perfPools;
		private DwmPoolCollection _densityPools;
		private DwmPoolCollection _densityPerfPools;
		private List<int> _poolIdsWithRecommendation;
		private static Dictionary<int, AutoRecommendation> _autoRecommendations;
		private List<int> PoolIdsWithRecommendation
		{
			get
			{
				return DwmBase.SafeGetItem<List<int>>(ref this._poolIdsWithRecommendation);
			}
		}
		private Dictionary<int, AutoRecommendation> RecommendationCache
		{
			get
			{
				if (DwmAEAnalyzer._autoRecommendations == null)
				{
					DwmAEAnalyzer._autoRecommendations = new Dictionary<int, AutoRecommendation>();
				}
				return DwmAEAnalyzer._autoRecommendations;
			}
		}
		internal List<MoveRecommendation> Analyze()
		{
			List<MoveRecommendation> list = new List<MoveRecommendation>();
			DwmAEBase.VerboseTraceEnabled = Configuration.GetValueAsBool(ConfigItems.VerboseTraceEnabled, false);
			try
			{
				DwmPoolBase.RefreshCache();
				if (this.GetPoolsInTrouble())
				{
					if (DwmAEBase.VerboseTraceEnabled)
					{
						DwmAEBase.Trace("Found {0} MaxPerformance pools with performance issues.", new object[]
						{
							(this._perfPools != null) ? this._perfPools.Count.ToString() : "0"
						});
						DwmAEBase.Trace("Found {0} MaxDensity pools with density issues.", new object[]
						{
							(this._densityPools != null) ? this._densityPools.Count.ToString() : "0"
						});
						DwmAEBase.Trace("Found {0} MaxDensity pools with performance issues.", new object[]
						{
							(this._densityPerfPools != null) ? this._densityPerfPools.Count.ToString() : "0"
						});
					}
					if (this._perfPools != null)
					{
						for (int i = 0; i < this._perfPools.Count; i++)
						{
							DwmAEOptimizer dwmAEOptimizer = new DwmAEOptimizer();
							if (dwmAEOptimizer.OptimizePoolForMaxPerf(this._perfPools[i]))
							{
								this.ApplyRecommendations(this._perfPools[i], dwmAEOptimizer.Recommendations, dwmAEOptimizer.HostToPowerOn, dwmAEOptimizer.HostToPowerOff);
								list.AddRange(dwmAEOptimizer.Recommendations);
								this.PoolIdsWithRecommendation.Add(this._perfPools[i].Id);
							}
						}
					}
					if (this._densityPools != null)
					{
						for (int j = 0; j < this._densityPools.Count; j++)
						{
							DwmAEOptimizer dwmAEOptimizer2 = new DwmAEOptimizer();
							if (dwmAEOptimizer2.OptimizePoolForMaxDensity(this._densityPools[j]))
							{
								this.ApplyRecommendations(this._densityPools[j], dwmAEOptimizer2.Recommendations, dwmAEOptimizer2.HostToPowerOn, dwmAEOptimizer2.HostToPowerOff);
								list.AddRange(dwmAEOptimizer2.Recommendations);
								this.PoolIdsWithRecommendation.Add(this._densityPools[j].Id);
							}
						}
					}
					if (this._densityPerfPools != null)
					{
						DwmAEOptimizer dwmAEOptimizer3 = new DwmAEOptimizer();
						for (int k = 0; k < this._densityPerfPools.Count; k++)
						{
							if (dwmAEOptimizer3.OptimizePoolForMaxPerf(this._densityPerfPools[k]))
							{
								this.ApplyRecommendations(this._densityPerfPools[k], dwmAEOptimizer3.Recommendations, dwmAEOptimizer3.HostToPowerOn, dwmAEOptimizer3.HostToPowerOff);
								list.AddRange(dwmAEOptimizer3.Recommendations);
								this.PoolIdsWithRecommendation.Add(this._densityPerfPools[k].Id);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
			}
			finally
			{
				DwmPool.UnloadCache();
				this.ClearPoolsWithoutRecommendations();
			}
			return list;
		}
		private bool GetPoolsInTrouble()
		{
			this._perfPools = null;
			this._densityPerfPools = null;
			this._densityPools = null;
			DwmAEBase.Trace("Checking for optimizations for all pools", new object[0]);
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				dBAccess.Timeout = 120;
				using (IDataReader dataReader = dBAccess.ExecuteReader("ae_get_pools_in_trouble", new StoredProcParamCollection
				{
					new StoredProcParam("_poll_interval_seconds", Configuration.GetValueAsInt(ConfigItems.AnalysisEnginePollInterval))
				}))
				{
					if (dataReader.Read())
					{
						this._perfPools = new DwmPoolCollection();
						DwmPool dwmPool = null;
						do
						{
							DwmAEAnalyzer.LoadPoolHostInTrouble(this._perfPools, ref dwmPool, dataReader);
						}
						while (dataReader.Read());
					}
					if (dataReader.NextResult())
					{
						this._densityPerfPools = new DwmPoolCollection();
						DwmPool dwmPool2 = null;
						while (dataReader.Read())
						{
							DwmAEAnalyzer.LoadPoolHostInTrouble(this._densityPerfPools, ref dwmPool2, dataReader);
						}
					}
					if (dataReader.NextResult())
					{
						this._densityPools = new DwmPoolCollection();
						DwmPool dwmPool3 = null;
						while (dataReader.Read())
						{
							DwmAEAnalyzer.LoadPoolHostInTrouble(this._densityPools, ref dwmPool3, dataReader);
						}
					}
				}
			}
			return this._perfPools != null || this._densityPerfPools != null || this._densityPools != null;
		}
		private static void LoadPoolHostInTrouble(DwmPoolCollection poolsInTrouble, ref DwmPool lastPool, IDataReader reader)
		{
			int @int = DBAccess.GetInt(reader, "poolid");
			string @string = DBAccess.GetString(reader, "pool_uuid");
			string string2 = DBAccess.GetString(reader, "pool_name");
			DwmHypervisorType int2 = (DwmHypervisorType)DBAccess.GetInt(reader, "hv_type");
			double @double = DBAccess.GetDouble(reader, "pool_master_cpu_limit");
			if (lastPool == null || lastPool.Id != @int)
			{
				lastPool = new DwmPool(@string, string2, int2);
				lastPool.Id = @int;
				lastPool.PoolMasterCpuLimit = @double;
				poolsInTrouble.Add(lastPool);
			}
			int int3 = DBAccess.GetInt(reader, "host_id");
			string string3 = DBAccess.GetString(reader, "host_uuid");
			string string4 = DBAccess.GetString(reader, "host_name");
			DwmHost dwmHost = new DwmHost(string3, string4, @int);
			dwmHost.Id = int3;
			dwmHost.IsPoolMaster = DBAccess.GetBool(reader, "is_pool_master");
			dwmHost.ParticipatesInPowerManagement = DBAccess.GetBool(reader, "can_power");
			dwmHost.Metrics.FillOrder = DBAccess.GetInt(reader, "fill_order");
			dwmHost.Metrics.TotalMemory = DBAccess.GetInt64(reader, "total_mem");
			dwmHost.Metrics.PotentialFreeMemory = DBAccess.GetInt64(reader, "potential_free_mem");
			dwmHost.Metrics.NumHighFullContentionVCpus = DBAccess.GetInt(reader, "full_contention_count");
			dwmHost.Metrics.NumHighConcurrencyHazardVCpus = DBAccess.GetInt(reader, "concurrency_hazard_count");
			dwmHost.Metrics.NumHighPartialContentionVCpus = DBAccess.GetInt(reader, "partial_contention_count");
			dwmHost.Metrics.MetricsNow.AverageFreeMemory = DBAccess.GetInt64(reader, "avg_free_mem");
			dwmHost.Metrics.MetricsNow.AverageCpuUtilization = DBAccess.GetDouble(reader, "avg_cpu");
			dwmHost.Metrics.MetricsNow.AveragePifReadsPerSecond = DBAccess.GetDouble(reader, "avg_net_read");
			dwmHost.Metrics.MetricsNow.AveragePifWritesPerSecond = DBAccess.GetDouble(reader, "avg_net_write");
			dwmHost.Metrics.MetricsNow.AverageLoadAverage = DBAccess.GetDouble(reader, "avg_load");
			dwmHost.Metrics.NumVMs = DBAccess.GetInt(reader, "num_vms");
			lastPool.Hosts.Add(dwmHost);
		}
		private void ApplyRecommendations(DwmPool pool, List<MoveRecommendation> recommendations, DwmHostCollection hostsToPowerOn, DwmHostCollection hostsToPowerOff)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num = 0;
			int num2 = 0;
			OptimizationSeverity optimizationSeverity = OptimizationSeverity.None;
			AutoBalanceAggressiveness autoBalanceAggressiveness = AutoBalanceAggressiveness.None;
			if ((recommendations != null && recommendations.Count > 0) || (hostsToPowerOff != null && hostsToPowerOff.Count > 0) || (hostsToPowerOn != null && hostsToPowerOn.Count > 0))
			{
				DwmAEAnalyzer.GetPoolConfiguration(pool, out flag2, out num, out flag3, out num2, out optimizationSeverity, out autoBalanceAggressiveness);
			}
			if (recommendations != null && recommendations.Count > 0)
			{
				AutoRecommendation autoRecommendation = null;
				if (this.RecommendationCache.TryGetValue(pool.Id, out autoRecommendation))
				{
					if (DwmAEAnalyzer.CompareRecommendations(autoRecommendation.MoveRecs, recommendations, autoBalanceAggressiveness))
					{
						autoRecommendation.PollIntervals++;
					}
					else
					{
						if (autoBalanceAggressiveness != AutoBalanceAggressiveness.High)
						{
							autoRecommendation.PollIntervals = 1;
						}
						autoRecommendation.MoveRecs = recommendations;
						autoRecommendation.HostToTurnOn = hostsToPowerOn;
						autoRecommendation.HostToTurnOff = hostsToPowerOff;
					}
				}
				else
				{
					AutoRecommendation autoRecommendation2 = new AutoRecommendation();
					autoRecommendation2.PollIntervals = 1;
					autoRecommendation2.PoolId = pool.Id;
					autoRecommendation2.MoveRecs = recommendations;
					autoRecommendation2.HostToTurnOn = hostsToPowerOn;
					autoRecommendation2.HostToTurnOff = hostsToPowerOff;
					this.RecommendationCache.Add(pool.Id, autoRecommendation2);
					autoRecommendation = autoRecommendation2;
				}
				if (flag2 && flag3)
				{
					if (autoRecommendation.PollIntervals >= num)
					{
						if (optimizationSeverity != OptimizationSeverity.None && recommendations[0].Severity >= optimizationSeverity)
						{
							if (DwmAEAnalyzer.ChangeHostsPowerState(autoRecommendation.MoveRecs, pool, PowerStatus.On) == 0 && DwmAEAnalyzer.MigrateVMs(autoRecommendation.MoveRecs, pool) == 0)
							{
								int num3 = DwmAEAnalyzer.ChangeHostsPowerState(autoRecommendation.MoveRecs, pool, PowerStatus.Off);
							}
						}
						else
						{
							if (optimizationSeverity == OptimizationSeverity.None)
							{
								DwmAEBase.Trace("Not applying recommendations because AutoBalanceSeverity is not set", new object[0]);
								foreach (MoveRecommendation current in autoRecommendation.MoveRecs)
								{
									current.RecommendationStatus = RecommendationStatus.WouldApplyIfSeverityConfigured;
									DwmAEAnalyzer.SaveRecommendationStatus(current);
								}
							}
							else
							{
								DwmAEBase.Trace("Not applying recommendations because severity ({0})is less that AutoBalanceSeverity ({1})", new object[]
								{
									recommendations[0].Severity,
									optimizationSeverity
								});
							}
						}
						flag = true;
					}
				}
				else
				{
					if (flag2 && !flag3)
					{
						if (autoRecommendation.PollIntervals >= num)
						{
							if (optimizationSeverity != OptimizationSeverity.None && recommendations[0].Severity >= optimizationSeverity)
							{
								if (autoRecommendation.HostToTurnOn == null || autoRecommendation.HostToTurnOn.Count == 0)
								{
									int num3 = DwmAEAnalyzer.MigrateVMs(autoRecommendation.MoveRecs, pool);
								}
								foreach (MoveRecommendation current2 in autoRecommendation.MoveRecs)
								{
									if (current2.RecommendationStatus == RecommendationStatus.None)
									{
										current2.RecommendationStatus = RecommendationStatus.WouldApplyIfPowerManagementEnabled;
										DwmAEAnalyzer.SaveRecommendationStatus(current2);
									}
								}
							}
							else
							{
								if (optimizationSeverity == OptimizationSeverity.None)
								{
									DwmAEBase.Trace("Not applying recommendations because AutoBalanceSeverity is not set", new object[0]);
									foreach (MoveRecommendation current3 in autoRecommendation.MoveRecs)
									{
										current3.RecommendationStatus = RecommendationStatus.WouldApplyIfSeverityConfigured;
										DwmAEAnalyzer.SaveRecommendationStatus(current3);
									}
								}
								else
								{
									DwmAEBase.Trace("Not applying recommendations because severity ({0})is less than AutoBalanceSeverity ({1})", new object[]
									{
										recommendations[0].Severity,
										optimizationSeverity
									});
								}
							}
							flag = true;
						}
					}
					else
					{
						if (autoRecommendation.PollIntervals >= num)
						{
							foreach (MoveRecommendation current4 in autoRecommendation.MoveRecs)
							{
								if (current4.Reason == ResourceToOptimize.PowerOn || current4.Reason == ResourceToOptimize.PowerOff)
								{
									current4.RecommendationStatus = RecommendationStatus.WouldApplyIfPowerManagementEnabled;
								}
								else
								{
									current4.RecommendationStatus = RecommendationStatus.WouldApplyIfAutobalanceEnabled;
								}
								DwmAEAnalyzer.SaveRecommendationStatus(current4);
							}
							flag = true;
							DwmAEBase.Trace("The set of recommendation would have been applied if AutoBalance and PowerManagement were enabled", new object[0]);
						}
					}
				}
			}
			if (flag)
			{
				this.RecommendationCache.Remove(pool.Id);
			}
		}
		private static void GetPoolConfiguration(DwmPool pool, out bool autoBalance, out int requiredAutoBalancePollIntervals, out bool managePower, out int requiredPowerPollIntervals, out OptimizationSeverity severity, out AutoBalanceAggressiveness aggressiveness)
		{
			autoBalance = false;
			managePower = false;
			requiredAutoBalancePollIntervals = 0;
			requiredPowerPollIntervals = 0;
			severity = OptimizationSeverity.None;
			aggressiveness = AutoBalanceAggressiveness.None;
			Dictionary<string, string> otherConfig = pool.GetOtherConfig();
			string text;
			if (otherConfig.TryGetValue("AutoBalanceEnabled", out text))
			{
				bool.TryParse(text, out autoBalance);
			}
			DwmAEBase.Trace("AutoBalanceEnabled set to {0}", new object[]
			{
				autoBalance
			});
			if (otherConfig.TryGetValue("PowerManagementEnabled", out text))
			{
				bool.TryParse(text, out managePower);
			}
			DwmAEBase.Trace("PowerManagementEnabled set to {0}", new object[]
			{
				managePower
			});
			if (otherConfig.TryGetValue("AutoBalancePollIntervals", out text))
			{
				DwmAEBase.Trace("AutoBalancePollIntervals set to {0}", new object[]
				{
					text
				});
				if (!int.TryParse(text, out requiredAutoBalancePollIntervals))
				{
					requiredAutoBalancePollIntervals = -1;
				}
			}
			else
			{
				requiredAutoBalancePollIntervals = -1;
			}
			if (autoBalance && requiredAutoBalancePollIntervals < 1)
			{
				autoBalance = false;
				DwmAEBase.Trace("AutoBalance being disabled because AutoBalancePollIntervals is invalid.", new object[0]);
			}
			if (otherConfig.TryGetValue("PowerManagementPollIntervals", out text))
			{
				DwmAEBase.Trace("PowerManagementPollIntervals set to {0}", new object[]
				{
					text
				});
				if (!int.TryParse(text, out requiredPowerPollIntervals))
				{
					requiredPowerPollIntervals = -1;
				}
			}
			if (!autoBalance && managePower && requiredPowerPollIntervals < 1)
			{
				DwmAEBase.Trace("PowerManagmement being disabled because PowerManagementPollIntervls is invalid.", new object[0]);
			}
			if (otherConfig.TryGetValue("AutoBalanceSeverity", out text))
			{
				DwmAEBase.Trace("AutoBalanceSeverity set to {0}", new object[]
				{
					text
				});
				if (Localization.Compare(text, "Critical", true) == 0)
				{
					severity = OptimizationSeverity.Critical;
				}
				else
				{
					if (Localization.Compare(text, "High", true) == 0)
					{
						severity = OptimizationSeverity.High;
					}
					else
					{
						if (Localization.Compare(text, "Medium", true) == 0)
						{
							severity = OptimizationSeverity.Medium;
						}
						else
						{
							if (Localization.Compare(text, "Low", true) == 0)
							{
								severity = OptimizationSeverity.Low;
							}
						}
					}
				}
			}
			if (otherConfig.TryGetValue("AutoBalanceAggressiveness", out text))
			{
				DwmAEBase.Trace("AutoBalanceAggressiveness set to {0}", new object[]
				{
					text
				});
				if (Localization.Compare(text, "High", true) == 0)
				{
					aggressiveness = AutoBalanceAggressiveness.High;
				}
				else
				{
					if (Localization.Compare(text, "Medium", true) == 0)
					{
						aggressiveness = AutoBalanceAggressiveness.Medium;
					}
					else
					{
						if (Localization.Compare(text, "Low", true) == 0)
						{
							aggressiveness = AutoBalanceAggressiveness.Low;
						}
					}
				}
			}
		}
		private static int ChangeHostsPowerState(List<MoveRecommendation> recommendations, DwmPool pool, PowerStatus newPowerState)
		{
			int num = 0;
			ResourceToOptimize resourceToOptimize = (newPowerState != PowerStatus.On) ? ResourceToOptimize.PowerOff : ResourceToOptimize.PowerOn;
			int num2 = 0;
			while (recommendations != null && num2 < recommendations.Count)
			{
				if (recommendations[num2].Reason == resourceToOptimize)
				{
					int num3 = DwmAEHost.SetPowerState(recommendations[num2].MoveFromHostUuid, recommendations[num2].PoolUuid, newPowerState, recommendations[num2].RecommendationId);
					num |= num3;
					recommendations[num2].RecommendationStatus = ((num3 != 0) ? RecommendationStatus.AutomaticalApplicationFailed : RecommendationStatus.AutomaticallyApplied);
					DwmAEAnalyzer.SaveRecommendationStatus(recommendations[num2]);
				}
				num2++;
			}
			return num;
		}
		private static int MigrateVMs(List<MoveRecommendation> recommendations, DwmPool pool)
		{
			int num = 0;
			StringBuilder stringBuilder = null;
			DwmAEBase.Trace("MigrateVMs called.  AutoBalance enabled for {0} recommendations", new object[]
			{
				recommendations.Count.ToString()
			});
			int num2 = 0;
			while (recommendations != null && num2 < recommendations.Count)
			{
				if (recommendations[num2].Reason != ResourceToOptimize.PowerOff && recommendations[num2].Reason != ResourceToOptimize.PowerOn)
				{
					DwmAEBase.Trace("Checking CanStartVM for VM {0} on host {1}", new object[]
					{
						recommendations[num2].VmId.ToString(),
						recommendations[num2].MoveToHostId.ToString()
					});
					if (DwmAEVirtualMachine.CanStartVM(recommendations[num2].VmUuid, recommendations[num2].MoveToHostUuid, recommendations[num2].PoolUuid) == CantBootReason.None)
					{
						DwmAEBase.Trace("MigrateVM for VM {0} to host {1}", new object[]
						{
							recommendations[num2].VmId.ToString(),
							recommendations[num2].MoveToHostId.ToString()
						});
						int num3 = DwmAEVirtualMachine.MigrateVM(recommendations[num2].VmUuid, recommendations[num2].MoveToHostUuid, recommendations[num2].MoveFromHostUuid, recommendations[num2].PoolUuid, false, recommendations[num2].RecommendationId);
						num |= num3;
						recommendations[num2].RecommendationStatus = ((num3 != 0) ? RecommendationStatus.AutomaticalApplicationFailed : RecommendationStatus.AutomaticallyApplied);
						DwmAEBase.Trace("MigrateVM for VM {0} to host {1} resulted in {2}", new object[]
						{
							recommendations[num2].VmId.ToString(),
							recommendations[num2].MoveToHostId.ToString(),
							num3.ToString()
						});
						DwmAEAnalyzer.SaveRecommendationStatus(recommendations[num2]);
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.AppendFormat("{0} migrating {1} from {2} to {3}.  ", new object[]
						{
							(num3 != 0) ? "Failure" : "Success",
							recommendations[num2].VmName,
							recommendations[num2].MoveFromHostName,
							recommendations[num2].MoveToHostName
						});
					}
					else
					{
						recommendations[num2].RecommendationStatus = RecommendationStatus.AutomaticalApplicationFailed;
						DwmAEBase.Trace("CanStartVM did not pass for VM {0} on host {1}", new object[]
						{
							recommendations[num2].VmId.ToString(),
							recommendations[num2].MoveToHostId.ToString()
						});
					}
				}
				num2++;
			}
			if (stringBuilder != null)
			{
				string category = "WLB_VM_RELOCATION";
				DwmAEAnalyzer.NotifyPoolMaster(pool.Id, category, stringBuilder.ToString());
			}
			return num;
		}
		private static void NotifyPoolMaster(int poolId, string category, string message)
		{
			ICollectorActions collector = DwmPool.GetCollector(poolId);
			if (collector != null)
			{
				DwmAEBase.Trace("Sending {0} message {1} to pool {2}", new object[]
				{
					category,
					message,
					poolId
				});
				collector.SendMessage(category, message);
			}
		}
		private static void SaveRecommendationStatus(MoveRecommendation recommendation)
		{
			string sqlStatement = "move_recommendation_update_status";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@recommendation_id", recommendation.RecommendationId));
			storedProcParamCollection.Add(new StoredProcParam("@status", (int)recommendation.RecommendationStatus));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		private void ClearPoolsWithoutRecommendations()
		{
			if (DwmAEAnalyzer._autoRecommendations != null && DwmAEAnalyzer._autoRecommendations.Count > 0 && this._poolIdsWithRecommendation != null && this._poolIdsWithRecommendation.Count > 0)
			{
				List<int> list = new List<int>();
				foreach (KeyValuePair<int, AutoRecommendation> current in DwmAEAnalyzer._autoRecommendations)
				{
					if (!this._poolIdsWithRecommendation.Contains(current.Key))
					{
						list.Add(current.Key);
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					DwmAEAnalyzer._autoRecommendations.Remove(list[i]);
				}
			}
		}
		private static bool CompareRecommendations(List<MoveRecommendation> l1, List<MoveRecommendation> l2, AutoBalanceAggressiveness aggressiveness)
		{
			bool result = false;
			if (l1 != null && l2 != null && l1.Count > 0 && l1.Count == l2.Count)
			{
				int i;
				for (i = 0; i < l1.Count; i++)
				{
					MoveRecommendation moveRecommendation = null;
					for (int j = 0; j < l2.Count; j++)
					{
						if (l2[j].VmId == l1[i].VmId && l2[j].MoveFromHostId == l1[i].MoveFromHostId && ((l1[i].Reason != ResourceToOptimize.PowerOff && l1[i].Reason != ResourceToOptimize.PowerOn) || (l2[j].Reason == l1[i].Reason && (l1[i].Reason == ResourceToOptimize.PowerOff || l1[i].Reason == ResourceToOptimize.PowerOn))) && ((l2[j].MoveToHostId == l1[i].MoveToHostId && aggressiveness == AutoBalanceAggressiveness.Low) || aggressiveness != AutoBalanceAggressiveness.Low))
						{
							moveRecommendation = l2[j];
							break;
						}
					}
					if (moveRecommendation == null)
					{
						break;
					}
				}
				result = (i >= l1.Count);
			}
			return result;
		}
	}
}
