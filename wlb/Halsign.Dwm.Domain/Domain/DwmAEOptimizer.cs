using Halsign.DWM.Collectors;
using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
namespace Halsign.DWM.Domain
{
	internal class DwmAEOptimizer : DwmAEBase
	{
		private delegate bool BalanceResourceDelegate();
		private struct ResourceImportance
		{
			internal ResourceToOptimize Resource;
			internal double Importance;
			internal DwmAEOptimizer.BalanceResourceDelegate BalanceDelegate;
			internal ResourceImportance(ResourceToOptimize resource, double importance, DwmAEOptimizer.BalanceResourceDelegate balanceDelegate)
			{
				this.Resource = resource;
				this.Importance = importance;
				this.BalanceDelegate = balanceDelegate;
			}
		}
		private List<MoveRecommendation> _recommendations = new List<MoveRecommendation>();
		private DwmHostCollection _hostsToPowerOff;
		private DwmHostCollection _hostsToPowerOn;
		private DwmPool _poolInTrouble;
		private DwmPool _poolDetails;
		private OptimizationSeverity _severity;
		private bool _compressGuests;
		private DwmVirtualMachineCollection _shouldMoveVMs;
		private DwmHostCollection _poweredOffHosts;
		private ICollectorActions _collector;
		private OptimizationSeverity Severity
		{
			get
			{
				return this._severity;
			}
			set
			{
				this._severity = value;
			}
		}
		internal List<MoveRecommendation> Recommendations
		{
			get
			{
				return this._recommendations;
			}
		}
		internal DwmHostCollection HostToPowerOff
		{
			get
			{
				return this._hostsToPowerOff;
			}
		}
		internal DwmHostCollection HostToPowerOn
		{
			get
			{
				return this._hostsToPowerOn;
			}
		}
		private DwmVirtualMachineCollection ShouldMoveVMs
		{
			get
			{
				return DwmBase.SafeGetItem<DwmVirtualMachineCollection>(ref this._shouldMoveVMs);
			}
		}
		internal bool OptimizePoolForMaxPerf(DwmPool poolInTrouble)
		{
			bool flag = false;
			int num = 0;
			bool flag2 = false;
			DwmAEBase.Trace("OptimizePoolForMaxPerf:  poolId = {0}", new object[]
			{
				poolInTrouble.Id
			});
			this._poolInTrouble = poolInTrouble;
			this._recommendations = new List<MoveRecommendation>();
			this._poolDetails = DwmAEOptimizer.GetPoolData(this._poolInTrouble.Id);
			if (this._poolDetails != null && this._poolInTrouble.Hosts != null)
			{
				while (!flag && num < 4)
				{
					if (DwmAEBase.VerboseTraceEnabled)
					{
						DwmAEBase.Trace("Beginning optimization attempt #{0}...", new object[]
						{
							(num + 1).ToString()
						});
					}
					bool flag3 = true;
					if (this._shouldMoveVMs != null)
					{
						this._shouldMoveVMs.Clear();
					}
					List<DwmAEOptimizer.ResourceImportance> list = this.SortResourceByImportance(this._poolDetails);
					for (int i = 0; i < list.Count; i++)
					{
						if (DwmAEBase.VerboseTraceEnabled)
						{
							DwmAEBase.Trace("Calling balance delegate {0}", new object[]
							{
								list[i].BalanceDelegate.Method.Name
							});
						}
						flag3 &= list[i].BalanceDelegate();
					}
					flag = flag3;
					if (DwmAEBase.VerboseTraceEnabled)
					{
						DwmAEBase.Trace("Optimized = {0}, MadeItBetter = {1}", new object[]
						{
							flag.ToString(),
							flag2.ToString()
						});
					}
					if (flag || flag2)
					{
						if (this.Severity == OptimizationSeverity.None)
						{
							List<int> list2 = new List<int>();
							for (int j = 0; j < this._recommendations.Count; j++)
							{
								if (!list2.Contains(this._recommendations[j].MoveFromHostId))
								{
									list2.Add(this._recommendations[j].MoveFromHostId);
								}
								double num2 = (double)list2.Count / (double)this._poolDetails.Hosts.Count;
								if (num2 > 0.5)
								{
									this.Severity = OptimizationSeverity.Critical;
								}
								else
								{
									if (num2 > 0.25)
									{
										this.Severity = OptimizationSeverity.High;
									}
									else
									{
										if (num2 > 0.1)
										{
											this.Severity = OptimizationSeverity.Medium;
										}
										else
										{
											this.Severity = OptimizationSeverity.Low;
										}
									}
								}
							}
						}
						for (int k = 0; k < this._recommendations.Count; k++)
						{
							this._recommendations[k].Severity = this.Severity;
						}
						this.AddPowerRecommendationsToMoveRecommendations();
						if (this._recommendations.Count > 0)
						{
							this.SaveRecommendations();
						}
					}
					else
					{
						this._compressGuests = false;
						if (num == 0)
						{
							if (DwmAEBase.VerboseTraceEnabled)
							{
								DwmAEBase.Trace("First attempt to optimize pool unsuccessful.", new object[0]);
							}
							if (this._poolDetails.PreferPowerOnOverCompression)
							{
								if (DwmAEBase.VerboseTraceEnabled)
								{
									DwmAEBase.Trace("Attempting to locate hosts to power on...", new object[0]);
								}
								this.MaxPerfomancePowerWork();
								if (DwmAEBase.VerboseTraceEnabled)
								{
									if (this._hostsToPowerOn != null && this._hostsToPowerOn.Count > 0)
									{
										DwmAEBase.Trace("found {0} hosts to power on.", new object[]
										{
											this._hostsToPowerOn.Count
										});
									}
									else
									{
										DwmAEBase.Trace("found 0 hosts to power on.", new object[0]);
									}
								}
							}
							else
							{
								if (DwmAEBase.VerboseTraceEnabled)
								{
									DwmAEBase.Trace("Attempting to compress existing guests before powering on another host...", new object[0]);
								}
								this.MaxPerformanceCompressionWork();
							}
						}
						else
						{
							if (num == 1)
							{
								if (DwmAEBase.VerboseTraceEnabled)
								{
									DwmAEBase.Trace("Second attempt to optimize pool unsuccessful.", new object[0]);
								}
								if (this._poolDetails.PreferPowerOnOverCompression)
								{
									if (DwmAEBase.VerboseTraceEnabled)
									{
										DwmAEBase.Trace("Attempting to compress existing guests before powering on another host...", new object[0]);
									}
									this.MaxPerformanceCompressionWork();
								}
								else
								{
									if (DwmAEBase.VerboseTraceEnabled)
									{
										DwmAEBase.Trace("Attempting to locate hosts to power on...", new object[0]);
									}
									if (!this.MaxPerfomancePowerWork())
									{
										if (DwmAEBase.VerboseTraceEnabled)
										{
											DwmAEBase.Trace("Unable to find any hosts to power on. Consider pool as optimized in order to save any recommendations.", new object[0]);
										}
										flag2 = true;
									}
									else
									{
										if (DwmAEBase.VerboseTraceEnabled)
										{
											if (this._hostsToPowerOn != null && this._hostsToPowerOn.Count > 0)
											{
												DwmAEBase.Trace("found {0} hosts to power on.", new object[]
												{
													this._hostsToPowerOn.Count
												});
											}
											else
											{
												DwmAEBase.Trace("found 0 hosts to power on.", new object[0]);
											}
										}
									}
								}
							}
							else
							{
								if (num == 2)
								{
									if (DwmAEBase.VerboseTraceEnabled)
									{
										DwmAEBase.Trace("Third attempt to optimize pool unsuccessful.", new object[0]);
									}
									if (this._hostsToPowerOn != null && this._hostsToPowerOn.Count > 0)
									{
										if (DwmAEBase.VerboseTraceEnabled)
										{
											DwmAEBase.Trace("We do have {0} hosts to power on.  Try adjusting critical thresholds down.", new object[0]);
										}
										if (this._poolDetails.HostCpuThreshold.Critical < 0.5)
										{
											this._poolDetails.HostCpuThreshold.Critical = 0.5;
											this._poolDetails.HostCpuThreshold.High = 0.5;
										}
										this._recommendations.Clear();
										this._poolDetails = DwmAEOptimizer.GetPoolData(this._poolInTrouble.Id);
										for (int l = 0; l < this._hostsToPowerOn.Count; l++)
										{
											this._hostsToPowerOn[l].Metrics = new DwmHostAverageMetric();
											this._hostsToPowerOn[l].SimpleLoad();
											this._hostsToPowerOn[l].Enabled = true;
											this._hostsToPowerOn[l].Metrics.FreeMemory = this._hostsToPowerOn[l].Metrics.TotalMemory - this._hostsToPowerOn[l].MemoryOverhead;
										}
										this._poolDetails.Hosts.AddRange(this._hostsToPowerOn);
										flag2 = true;
									}
								}
							}
						}
					}
					num++;
				}
			}
			if (DwmAEBase.VerboseTraceEnabled)
			{
				DwmAEBase.Trace("Max Performace optimization loop complete.  Found {0} recommendations.", new object[]
				{
					this._recommendations.Count.ToString()
				});
			}
			return this._recommendations.Count > 0;
		}
		internal bool OptimizePoolForMaxDensity(DwmPool poolInTrouble)
		{
			int num = 0;
			this._poolInTrouble = poolInTrouble;
			this._recommendations = new List<MoveRecommendation>();
			this._poolDetails = DwmAEOptimizer.GetPoolData(this._poolInTrouble.Id);
			if (this._poolDetails != null && this._poolInTrouble.Hosts != null && this._poolInTrouble.Hosts.Count > 0)
			{
				DwmAEBase.Trace("OptimizePoolForMaxDensity:  poolId = {0}", new object[]
				{
					this._poolInTrouble.Id
				});
				int num2 = -1;
				for (int i = 0; i < this._poolInTrouble.Hosts.Count; i++)
				{
					if (this._poolInTrouble.Hosts[i].IsPoolMaster)
					{
						num2 = i;
						break;
					}
				}
				if (num2 > -1)
				{
					DwmHost item = this._poolInTrouble.Hosts[num2];
					this._poolInTrouble.Hosts.RemoveAt(num2);
					this._poolInTrouble.Hosts.Add(item);
				}
				int num3 = 0;
				this._poolInTrouble.Hosts.Sort(new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByVmCountAscend));
				while (num == 0 && num3 < 2)
				{
					for (int j = 0; j < this._poolInTrouble.Hosts.Count; j++)
					{
						DwmHost dwmHost = this.ConsolidateHost(this._poolInTrouble.Hosts[j].Id, new Comparison<DwmVirtualMachine>(DwmAEOptimizer.CompareVMsByCpuUtilAscend), new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByCpu), ResourceToOptimize.Consolidate);
						if (dwmHost != null)
						{
							if (dwmHost.ParticipatesInPowerManagement)
							{
								if (this._hostsToPowerOff == null)
								{
									this._hostsToPowerOff = new DwmHostCollection();
								}
								DwmAEBase.Trace("Recommending powering off host {0}", new object[]
								{
									this._poolInTrouble.Hosts[j].Name
								});
								this._hostsToPowerOff.Add(this._poolInTrouble.Hosts[j]);
								num++;
							}
							else
							{
								Logger.Trace("Cannot recommend powering off host {0} because it does not participate in power management", new object[]
								{
									this._poolInTrouble.Hosts[j].Name
								});
							}
						}
					}
					if (num != 0 || !this._poolDetails.CompressGuestsToPreservePower)
					{
						this._compressGuests = false;
						break;
					}
					if (num3 == 0)
					{
						Logger.Trace("Attempting second pass with memory compression on");
						this._compressGuests = true;
						this.ComputeHostPotentialFreeMemory();
					}
					num3++;
				}
				this.AddPowerRecommendationsToMoveRecommendations();
				if (this._recommendations.Count > 0)
				{
					double num4 = (double)num / (double)this._poolDetails.Hosts.Count;
					if (num4 > 0.5)
					{
						this.Severity = OptimizationSeverity.Critical;
					}
					else
					{
						if (num4 > 0.35)
						{
							this.Severity = OptimizationSeverity.High;
						}
						else
						{
							if (num4 > 0.2)
							{
								this.Severity = OptimizationSeverity.Medium;
							}
							else
							{
								this.Severity = OptimizationSeverity.Low;
							}
						}
					}
					for (int k = 0; k < this._recommendations.Count; k++)
					{
						this._recommendations[k].Severity = this.Severity;
					}
					this.SaveRecommendations();
				}
			}
			return this._recommendations.Count > 0;
		}
		private bool MaxPerfomancePowerWork()
		{
			bool result = false;
			bool flag = (this._poolDetails.OptMode != OptimizationMode.MaxDensity) ? this._poolDetails.OverCommitCpusInPerfMode : this._poolDetails.OverCommitCpusInDensityMode;
			DwmHostCollection dwmHostCollection = DwmAEHost.SelectHostsToPowerOn(this.GetPoweredOffHosts(), this._poolDetails, this._shouldMoveVMs, (!flag) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByCpuRating) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByTotalMemory));
			if (dwmHostCollection != null && dwmHostCollection.Count > 0)
			{
				if (this._hostsToPowerOn == null)
				{
					this._hostsToPowerOn = new DwmHostCollection();
				}
				this._hostsToPowerOn.AddRange(dwmHostCollection);
				this._recommendations.Clear();
				this._poolDetails = DwmAEOptimizer.GetPoolData(this._poolInTrouble.Id);
				for (int i = 0; i < this._hostsToPowerOn.Count; i++)
				{
					this._hostsToPowerOn[i].SimpleLoad();
					this._hostsToPowerOn[i].Enabled = true;
					this._hostsToPowerOn[i].Metrics.FreeMemory = this._hostsToPowerOn[i].Metrics.TotalMemory - this._hostsToPowerOn[i].MemoryOverhead;
				}
				this._poolDetails.Hosts.AddRange(this._hostsToPowerOn);
				result = true;
			}
			return result;
		}
		private void MaxPerformanceCompressionWork()
		{
			this._compressGuests = true;
			this._poolDetails = DwmAEOptimizer.GetPoolData(this._poolInTrouble.Id);
			this.ComputeHostPotentialFreeMemory();
		}
		private void ComputeHostPotentialFreeMemory()
		{
			for (int i = 0; i < this._poolDetails.Hosts.Count; i++)
			{
				DwmHost dwmHost = this._poolDetails.Hosts[i];
				dwmHost.Metrics.PotentialFreeMemory = dwmHost.Metrics.TotalMemory - dwmHost.MemoryOverhead - dwmHost.Metrics.MemoryActual - dwmHost.Metrics.ControlMemoryOverhead;
				foreach (DwmVirtualMachine current in dwmHost.VirtualMachines)
				{
					dwmHost.Metrics.PotentialFreeMemory -= current.MemoryOverhead;
					dwmHost.Metrics.PotentialFreeMemory -= ((current.MinimumDynamicMemory == current.MaximumStaticMemory) ? current.MaximumStaticMemory : current.MinimumDynamicMemory);
				}
				dwmHost.Metrics.PotentialFreeMemory = ((dwmHost.Metrics.PotentialFreeMemory <= 0L) ? 0L : dwmHost.Metrics.PotentialFreeMemory);
			}
		}
		private static DwmPool GetPoolData(int poolId)
		{
			DwmPool dwmPool = null;
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				dBAccess.Timeout = 120;
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("_pool_id", poolId));
				dBAccess.SetWorkMemoryMB(Configuration.GetValueAsInt(ConfigItems.DBVacuumAtStartup, 15));
				using (IDataReader dataReader = dBAccess.ExecuteReader("ae_get_pool_data_pool_config", storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						string @string = DBAccess.GetString(dataReader, "uuid");
						string string2 = DBAccess.GetString(dataReader, "name");
						DwmHypervisorType @int = (DwmHypervisorType)DBAccess.GetInt(dataReader, "hv_type");
						dwmPool = new DwmPool(@string, string2, @int);
						dwmPool.Id = poolId;
						dwmPool.MaxCpuRating = DBAccess.GetInt(dataReader, "max_cpu_rating");
					}
				}
				using (IDataReader dataReader2 = dBAccess.ExecuteReader("ae_get_pool_data_pool_thresholds", storedProcParamCollection))
				{
					if (dwmPool != null && dataReader2.Read())
					{
						dwmPool.LoadThresholdsAndWeights(dataReader2);
					}
				}
				using (IDataReader dataReader3 = dBAccess.ExecuteReader("ae_get_pool_data_hosts_with_metrics", storedProcParamCollection))
				{
					while (dataReader3.Read())
					{
						int int2 = DBAccess.GetInt(dataReader3, "host_id");
						string string3 = DBAccess.GetString(dataReader3, "name");
						string string4 = DBAccess.GetString(dataReader3, "uuid");
						int int3 = DBAccess.GetInt(dataReader3, "poolid");
						DwmHost dwmHost = new DwmHost(string4, string3, int3);
						dwmHost.Id = int2;
						dwmHost.NumCpus = DBAccess.GetInt(dataReader3, "num_cpus");
						dwmHost.CpuSpeed = DBAccess.GetInt(dataReader3, "cpu_speed");
						dwmHost.NumNics = DBAccess.GetInt(dataReader3, "num_pifs");
						dwmHost.IsPoolMaster = DBAccess.GetBool(dataReader3, "is_pool_master");
						dwmHost.Enabled = DBAccess.GetBool(dataReader3, "enabled");
						dwmHost.PowerState = (PowerStatus)DBAccess.GetInt(dataReader3, "power_state");
						dwmHost.ParticipatesInPowerManagement = DBAccess.GetBool(dataReader3, "can_power");
						dwmHost.ExcludeFromPlacementRecommendations = DBAccess.GetBool(dataReader3, "exclude_placements");
						dwmHost.ExcludeFromPoolOptimizationAcceptVMs = DBAccess.GetBool(dataReader3, "exclude_optimization_placements");
						dwmHost.MemoryOverhead = DBAccess.GetInt64(dataReader3, "memory_overhead");
						dwmHost.Metrics.FreeCPUs = DBAccess.GetInt(dataReader3, "free_cpus");
						dwmHost.Metrics.FillOrder = DBAccess.GetInt(dataReader3, "fill_order");
						dwmHost.Metrics.FreeMemory = DBAccess.GetInt64(dataReader3, "free_mem");
						dwmHost.Metrics.TotalMemory = DBAccess.GetInt64(dataReader3, "total_mem");
						dwmHost.Metrics.MemoryActual = DBAccess.GetInt64(dataReader3, "memory_actual");
						dwmHost.Metrics.ControlMemoryOverhead = DBAccess.GetInt64(dataReader3, "control_memory_overhead");
						dwmHost.Metrics.NumHighFullContentionVCpus = DBAccess.GetInt(dataReader3, "full_contention_count");
						dwmHost.Metrics.NumHighConcurrencyHazardVCpus = DBAccess.GetInt(dataReader3, "concurrency_hazard_count");
						dwmHost.Metrics.NumHighPartialContentionVCpus = DBAccess.GetInt(dataReader3, "partial_contention_count");
						dwmHost.Metrics.NumHighFullrunVCpus = DBAccess.GetInt(dataReader3, "fullrun_count");
						dwmHost.Metrics.NumHighPartialContentionVCpus = DBAccess.GetInt(dataReader3, "partial_run_count");
						dwmHost.Metrics.NumHighBlockedVCpus = DBAccess.GetInt(dataReader3, "blocked_count");
						dwmHost.Metrics.MetricsNow.AverageCpuUtilization = DBAccess.GetDouble(dataReader3, "cpu_util");
						dwmHost.Metrics.MetricsNow.AverageFreeMemory = DBAccess.GetInt64(dataReader3, "free_mem");
						dwmHost.Metrics.MetricsNow.AveragePifReadsPerSecond = DBAccess.GetDouble(dataReader3, "net_read");
						dwmHost.Metrics.MetricsNow.AveragePifWritesPerSecond = DBAccess.GetDouble(dataReader3, "net_write");
						dwmPool.Hosts.Add(dwmHost);
					}
				}
				using (IDataReader dataReader4 = dBAccess.ExecuteReader("ae_get_pool_data_storage_repositories", storedProcParamCollection))
				{
					DwmHost dwmHost2 = null;
					while (dataReader4.Read())
					{
						int int4 = DBAccess.GetInt(dataReader4, "host_id");
						if (dwmHost2 == null || dwmHost2.Id != int4)
						{
							dwmHost2 = dwmPool.Hosts.GetHost(int4);
						}
						if (dwmHost2 != null)
						{
							int int5 = DBAccess.GetInt(dataReader4, "sr_id");
							string string5 = DBAccess.GetString(dataReader4, "sr_uuid");
							string string6 = DBAccess.GetString(dataReader4, "sr_name");
							int int6 = DBAccess.GetInt(dataReader4, "poolid");
							DwmStorageRepository dwmStorageRepository = new DwmStorageRepository(string5, string6, int6);
							dwmStorageRepository.Id = int5;
							dwmHost2.AvailableStorage.Add(dwmStorageRepository);
						}
					}
				}
				using (IDataReader dataReader5 = dBAccess.ExecuteReader("ae_get_pool_data_pifs", storedProcParamCollection))
				{
					DwmHost dwmHost3 = null;
					while (dataReader5.Read())
					{
						int int7 = DBAccess.GetInt(dataReader5, "host_id");
						if (dwmHost3 == null || dwmHost3.Id != int7)
						{
							dwmHost3 = dwmPool.Hosts.GetHost(int7);
						}
						if (dwmHost3 != null)
						{
							dwmHost3.LoadPif(dataReader5);
						}
					}
				}
				using (IDataReader dataReader6 = dBAccess.ExecuteReader("ae_get_pool_data_vm_metrics", storedProcParamCollection))
				{
					DwmHost dwmHost4 = null;
					while (dataReader6.Read())
					{
						int int8 = DBAccess.GetInt(dataReader6, "host_id");
						if (dwmHost4 == null || dwmHost4.Id != int8)
						{
							dwmHost4 = dwmPool.Hosts.GetHost(int8);
						}
						if (dwmHost4 != null)
						{
							int int9 = DBAccess.GetInt(dataReader6, "vm_id");
							string string7 = DBAccess.GetString(dataReader6, "uuid");
							string string8 = DBAccess.GetString(dataReader6, "name");
							int int10 = DBAccess.GetInt(dataReader6, "poolid");
							DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(string7, string8, int10);
							dwmVirtualMachine.Id = int9;
							dwmVirtualMachine.AffinityHostId = DBAccess.GetInt(dataReader6, "host_affinity");
							dwmVirtualMachine.MinimumDynamicMemory = DBAccess.GetInt64(dataReader6, "min_dynamic_memory");
							dwmVirtualMachine.MaximumDynamicMemory = DBAccess.GetInt64(dataReader6, "max_dynamic_memory");
							dwmVirtualMachine.MinimumStaticMemory = DBAccess.GetInt64(dataReader6, "min_static_memory");
							dwmVirtualMachine.MaximumStaticMemory = DBAccess.GetInt64(dataReader6, "max_static_memory");
							dwmVirtualMachine.TargetMemory = DBAccess.GetInt64(dataReader6, "target_memory");
							dwmVirtualMachine.MemoryOverhead = DBAccess.GetInt64(dataReader6, "memory_overhead");
							dwmVirtualMachine.MinimumCpus = DBAccess.GetInt(dataReader6, "min_cpus");
							dwmVirtualMachine.HvMemoryMultiplier = DBAccess.GetDouble(dataReader6, "hv_memory_multiplier");
							dwmVirtualMachine.RequiredMemory = DBAccess.GetInt64(dataReader6, "required_memory");
							dwmVirtualMachine.Metrics.TotalMemory = DBAccess.GetInt64(dataReader6, "total_memory");
							dwmVirtualMachine.IsAgile = DBAccess.GetBool(dataReader6, "is_agile");
							dwmVirtualMachine.DriversUpToDate = DBAccess.GetBool(dataReader6, "drivers_up_to_date");
							dwmVirtualMachine.Metrics.AverageUsedMemory = DBAccess.GetInt64(dataReader6, "used_mem");
							dwmVirtualMachine.Metrics.MetricsNow.AverageTotalMemory = DBAccess.GetInt64(dataReader6, "total_mem");
							dwmVirtualMachine.Metrics.MetricsNow.AverageFreeMemory = DBAccess.GetInt64(dataReader6, "free_mem");
							dwmVirtualMachine.Metrics.MetricsNow.AverageTargetMemory = DBAccess.GetInt64(dataReader6, "target_mem");
							dwmVirtualMachine.Metrics.MetricsNow.AverageCpuUtilization = DBAccess.GetDouble(dataReader6, "cpu_util");
							dwmVirtualMachine.Metrics.MetricsNow.AveragePbdReadsPerSecond = DBAccess.GetDouble(dataReader6, "disk_read");
							dwmVirtualMachine.Metrics.MetricsNow.AveragePbdWritesPerSecond = DBAccess.GetDouble(dataReader6, "disk_write");
							dwmVirtualMachine.Metrics.MetricsNow.AveragePifReadsPerSecond = DBAccess.GetDouble(dataReader6, "net_read");
							dwmVirtualMachine.Metrics.MetricsNow.AveragePifWritesPerSecond = DBAccess.GetDouble(dataReader6, "net_write");
							dwmVirtualMachine.Metrics.MetricsNow.TotalVbdNetReadsPerSecond = DBAccess.GetDouble(dataReader6, "vbd_net_read");
							dwmVirtualMachine.Metrics.MetricsNow.TotalVbdNetWritesPerSecond = DBAccess.GetDouble(dataReader6, "vbd_net_write");
							dwmVirtualMachine.Metrics.MetricsNow.AverageRunstateFullContention = DBAccess.GetDouble(dataReader6, "runstate_full_contention");
							dwmVirtualMachine.Metrics.MetricsNow.AverageRunstateConcurrencyHazard = DBAccess.GetDouble(dataReader6, "runstate_concurrency_hazard");
							dwmVirtualMachine.Metrics.MetricsNow.AverageRunstatePartialContention = DBAccess.GetDouble(dataReader6, "runstate_partial_contention");
							dwmVirtualMachine.Metrics.MetricsNow.AverageRunstateFullRun = DBAccess.GetDouble(dataReader6, "runstate_fullrun");
							dwmVirtualMachine.Metrics.MetricsNow.AverageRunstatePartialRun = DBAccess.GetDouble(dataReader6, "runstate_partial_run");
							dwmVirtualMachine.Metrics.MetricsNow.AverageRunstateBlocked = DBAccess.GetDouble(dataReader6, "runstate_partial_run");
							dwmVirtualMachine.RunningOnHostId = dwmHost4.Id;
							dwmVirtualMachine.RunningOnHostName = dwmHost4.Name;
							dwmVirtualMachine.RunningOnHostUuid = dwmHost4.Uuid;
							dwmHost4.VirtualMachines.Add(dwmVirtualMachine);
						}
					}
				}
				using (IDataReader dataReader7 = dBAccess.ExecuteReader("ae_get_pool_data_vm_storage_repositories", storedProcParamCollection))
				{
					DwmHost dwmHost5 = null;
					DwmVirtualMachine dwmVirtualMachine2 = null;
					while (dataReader7.Read())
					{
						int int11 = DBAccess.GetInt(dataReader7, "host_id");
						int int12 = DBAccess.GetInt(dataReader7, "vm_id");
						if (dwmHost5 == null || dwmHost5.Id != int11)
						{
							dwmHost5 = dwmPool.Hosts.GetHost(int11);
						}
						if (dwmHost5 != null && (dwmVirtualMachine2 == null || dwmVirtualMachine2.Id != int12))
						{
							dwmVirtualMachine2 = dwmHost5.VirtualMachines.GetVM(int12);
						}
						if (dwmVirtualMachine2 != null && dwmVirtualMachine2.Id == int12)
						{
							int int13 = DBAccess.GetInt(dataReader7, "sr_id");
							string string9 = DBAccess.GetString(dataReader7, "uuid");
							string string10 = DBAccess.GetString(dataReader7, "name");
							int int14 = DBAccess.GetInt(dataReader7, "poolid");
							DwmStorageRepository dwmStorageRepository2 = new DwmStorageRepository(string9, string10, int14);
							dwmStorageRepository2.Id = int13;
							dwmVirtualMachine2.RequiredStorage.Add(dwmStorageRepository2);
						}
					}
				}
				using (IDataReader dataReader8 = dBAccess.ExecuteReader("ae_get_pool_data_vm_vif", storedProcParamCollection))
				{
					DwmHost dwmHost6 = null;
					DwmVirtualMachine dwmVirtualMachine3 = null;
					while (dataReader8.Read())
					{
						int int15 = DBAccess.GetInt(dataReader8, "host_id");
						int int16 = DBAccess.GetInt(dataReader8, "vm_id");
						if (dwmHost6 == null || dwmHost6.Id != int15)
						{
							dwmHost6 = dwmPool.Hosts.GetHost(int15);
						}
						if (dwmHost6 != null && (dwmVirtualMachine3 == null || dwmVirtualMachine3.Id != int16))
						{
							dwmVirtualMachine3 = dwmHost6.VirtualMachines.GetVM(int16);
						}
						if (dwmVirtualMachine3 != null && dwmVirtualMachine3.Id == int16)
						{
							int int17 = DBAccess.GetInt(dataReader8, "vif_id");
							string string11 = DBAccess.GetString(dataReader8, "uuid");
							int int18 = DBAccess.GetInt(dataReader8, "networkid");
							int int19 = DBAccess.GetInt(dataReader8, "poolid");
							DwmVif dwmVif = new DwmVif(string11, int18, int19);
							dwmVif.Id = int17;
							dwmVirtualMachine3.NetworkInterfaces.Add(dwmVif);
						}
					}
				}
				using (IDataReader dataReader9 = dBAccess.ExecuteReader("ae_get_pool_data_recent_moves", storedProcParamCollection))
				{
					while (dataReader9.Read())
					{
						DwmVmMovement dwmVmMovement = new DwmVmMovement();
						dwmVmMovement.MoveToHostId = DBAccess.GetInt(dataReader9, 0);
						dwmVmMovement.MoveToHostName = DBAccess.GetString(dataReader9, 1);
						dwmVmMovement.MoveFromHostId = DBAccess.GetInt(dataReader9, 2);
						dwmVmMovement.MoveFromHostName = DBAccess.GetString(dataReader9, 3);
						dwmVmMovement.VmId = DBAccess.GetInt(dataReader9, 4);
						dwmVmMovement.VmName = DBAccess.GetString(dataReader9, 5);
						dwmVmMovement.TimeOfMove = DBAccess.GetDateTime(dataReader9, 6);
						dwmPool.RecentVmMoves.Add(dwmVmMovement);
					}
				}
				dBAccess.ResetWorkMemory();
			}
			return dwmPool;
		}
		private List<DwmAEOptimizer.ResourceImportance> SortResourceByImportance(DwmPool pool)
		{
			List<DwmAEOptimizer.ResourceImportance> list = new List<DwmAEOptimizer.ResourceImportance>();
			list.Add(new DwmAEOptimizer.ResourceImportance(ResourceToOptimize.Runstate, pool.VmRunstateWeight.High, new DwmAEOptimizer.BalanceResourceDelegate(this.BalanceHostRunstate)));
			list.Add(new DwmAEOptimizer.ResourceImportance(ResourceToOptimize.Cpu, pool.VmCpuUtilizationWeight.High, new DwmAEOptimizer.BalanceResourceDelegate(this.BalanceHostCpu)));
			list.Add(new DwmAEOptimizer.ResourceImportance(ResourceToOptimize.Memory, pool.VmMemoryWeight.High, new DwmAEOptimizer.BalanceResourceDelegate(this.BalanceHostMemory)));
			list.Add(new DwmAEOptimizer.ResourceImportance(ResourceToOptimize.DiskRead, pool.VmDiskReadWeight.High, new DwmAEOptimizer.BalanceResourceDelegate(this.BalanceHostDiskRead)));
			list.Add(new DwmAEOptimizer.ResourceImportance(ResourceToOptimize.DiskWrite, pool.VmDiskWriteWeight.High, new DwmAEOptimizer.BalanceResourceDelegate(this.BalanceHostDiskWrite)));
			list.Add(new DwmAEOptimizer.ResourceImportance(ResourceToOptimize.NetworkRead, pool.VmNetworkReadWeight.High, new DwmAEOptimizer.BalanceResourceDelegate(this.BalanceHostNetRead)));
			list.Add(new DwmAEOptimizer.ResourceImportance(ResourceToOptimize.NetworkWrite, pool.VmNetworkWriteWeight.High, new DwmAEOptimizer.BalanceResourceDelegate(this.BalanceHostNetWrite)));
			list.Sort(new Comparison<DwmAEOptimizer.ResourceImportance>(DwmAEOptimizer.CompareResourceImportance));
			return list;
		}
		private static int CompareResourceImportance(DwmAEOptimizer.ResourceImportance x, DwmAEOptimizer.ResourceImportance y)
		{
			double num = y.Importance - x.Importance;
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			if (x.Resource == y.Resource)
			{
				return 0;
			}
			if (x.Resource == ResourceToOptimize.Cpu)
			{
				return -1;
			}
			if (y.Resource == ResourceToOptimize.Cpu)
			{
				return 1;
			}
			if (x.Resource == ResourceToOptimize.Memory)
			{
				return -1;
			}
			if (y.Resource == ResourceToOptimize.Memory)
			{
				return 1;
			}
			return 0;
		}
		private bool BalanceHostRunstate()
		{
			bool flag = true;
			for (int i = 0; i < this._poolInTrouble.Hosts.Count; i++)
			{
				if (this._poolInTrouble.Hosts[i].Metrics.NumHighFullContentionVCpus > 0 || this._poolInTrouble.Hosts[i].Metrics.NumHighConcurrencyHazardVCpus > 0 || this._poolInTrouble.Hosts[i].Metrics.NumHighPartialContentionVCpus > 0)
				{
					if (this._poolInTrouble.Hosts[i].Metrics.NumHighFullContentionVCpus > 0 || this._poolInTrouble.Hosts[i].Metrics.NumHighConcurrencyHazardVCpus > 0)
					{
						this.Severity = OptimizationSeverity.Critical;
					}
					flag &= this.BalanceHost(this._poolInTrouble.Hosts[i].Id, new Comparison<DwmVirtualMachine>(DwmAEOptimizer.CompareVMsByRunstate), (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByVmCountDescend) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByRunstate), ResourceToOptimize.Runstate);
				}
			}
			return flag;
		}
		private bool BalanceHostCpu()
		{
			bool flag = true;
			double num = (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? this._poolDetails.HostCpuThreshold.Critical : this._poolDetails.HostCpuThreshold.High;
			for (int i = 0; i < this._poolInTrouble.Hosts.Count; i++)
			{
				if (this._poolInTrouble.Hosts[i].IsPoolMaster && this._poolInTrouble.PoolMasterCpuLimit > 0.0 && this._poolInTrouble.PoolMasterCpuLimit < num)
				{
					num = this._poolInTrouble.PoolMasterCpuLimit;
				}
				if (this._poolInTrouble.Hosts[i].Metrics.MetricsNow.AverageCpuUtilization > num)
				{
					if (this._poolInTrouble.Hosts[i].Metrics.MetricsNow.AverageCpuUtilization > this._poolDetails.HostCpuThreshold.Critical)
					{
						this.Severity = OptimizationSeverity.Critical;
					}
					flag &= this.BalanceHost(this._poolInTrouble.Hosts[i].Id, new Comparison<DwmVirtualMachine>(DwmAEOptimizer.CompareVMsByCpuUtilDesc), (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByVmCountDescend) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByCpu), ResourceToOptimize.Cpu);
				}
			}
			if (DwmAEBase.VerboseTraceEnabled)
			{
				DwmAEBase.Trace("Pool workload {0} be optimized for Host CPU Utilization.", new object[]
				{
					(!flag) ? "cannot" : "can"
				});
			}
			return flag;
		}
		private bool BalanceHostMemory()
		{
			bool flag = true;
			double num = (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? this._poolDetails.HostMemoryThreshold.Critical : this._poolDetails.HostMemoryThreshold.High;
			for (int i = 0; i < this._poolInTrouble.Hosts.Count; i++)
			{
				if ((double)((this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? this._poolInTrouble.Hosts[i].Metrics.PotentialFreeMemory : this._poolInTrouble.Hosts[i].Metrics.MetricsNow.AverageFreeMemory) < num)
				{
					if ((double)((this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? this._poolInTrouble.Hosts[i].Metrics.PotentialFreeMemory : this._poolInTrouble.Hosts[i].Metrics.MetricsNow.AverageFreeMemory) < this._poolDetails.HostMemoryThreshold.Critical)
					{
						this.Severity = OptimizationSeverity.Critical;
					}
					flag &= this.BalanceHost(this._poolInTrouble.Hosts[i].Id, new Comparison<DwmVirtualMachine>(DwmAEOptimizer.CompareVMsByMemory), (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByVmCountDescend) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByFreeMemory), ResourceToOptimize.Memory);
				}
			}
			if (DwmAEBase.VerboseTraceEnabled)
			{
				DwmAEBase.Trace("Pool workload {0} be optimized for Host Free Memory.", new object[]
				{
					(!flag) ? "cannot" : "can"
				});
			}
			return flag;
		}
		private bool BalanceHostDiskRead()
		{
			bool flag = true;
			double num = (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? this._poolDetails.HostPbdReadThreshold.Critical : this._poolDetails.HostPbdReadThreshold.High;
			for (int i = 0; i < this._poolInTrouble.Hosts.Count; i++)
			{
				if (this._poolInTrouble.Hosts[i].Metrics.MetricsNow.AveragePbdReadsPerSecond > num)
				{
					if (this._poolInTrouble.Hosts[i].Metrics.MetricsNow.AveragePbdReadsPerSecond > this._poolDetails.HostPbdReadThreshold.Critical)
					{
						this.Severity = OptimizationSeverity.Critical;
					}
					flag &= this.BalanceHost(this._poolInTrouble.Hosts[i].Id, new Comparison<DwmVirtualMachine>(DwmAEOptimizer.CompareVMsByDiskReadIo), (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByVmCountDescend) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByDiskReadIo), ResourceToOptimize.DiskRead);
				}
			}
			if (DwmAEBase.VerboseTraceEnabled)
			{
				DwmAEBase.Trace("Pool workload {0} be optimized for Host Disk Read.", new object[]
				{
					(!flag) ? "cannot" : "can"
				});
			}
			return flag;
		}
		private bool BalanceHostDiskWrite()
		{
			bool flag = true;
			double num = (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? this._poolDetails.HostPbdWriteThreshold.Critical : this._poolDetails.HostPbdWriteThreshold.High;
			for (int i = 0; i < this._poolInTrouble.Hosts.Count; i++)
			{
				if (this._poolInTrouble.Hosts[i].Metrics.MetricsNow.AveragePbdWritesPerSecond > num)
				{
					if (this._poolInTrouble.Hosts[i].Metrics.MetricsNow.AveragePbdWritesPerSecond > this._poolDetails.HostPbdWriteThreshold.Critical)
					{
						this.Severity = OptimizationSeverity.Critical;
					}
					flag &= this.BalanceHost(this._poolInTrouble.Hosts[i].Id, new Comparison<DwmVirtualMachine>(DwmAEOptimizer.CompareVMsByDiskWriteIo), (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByVmCountDescend) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByDiskWriteIo), ResourceToOptimize.DiskWrite);
				}
			}
			if (DwmAEBase.VerboseTraceEnabled)
			{
				DwmAEBase.Trace("Pool workload {0} be optimized for Host Disk Write.", new object[]
				{
					(!flag) ? "cannot" : "can"
				});
			}
			return flag;
		}
		private bool BalanceHostNetRead()
		{
			bool flag = true;
			double num = (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? this._poolDetails.HostPifReadThreshold.Critical : this._poolDetails.HostPifReadThreshold.High;
			for (int i = 0; i < this._poolInTrouble.Hosts.Count; i++)
			{
				DwmHost host = this._poolDetails.Hosts.GetHost(this._poolInTrouble.Hosts[i].Id);
				if (host != null && host.Metrics.MetricsNow.AveragePifReadsPerSecond > num)
				{
					double num2 = 0.0;
					for (int j = 0; j < host.VirtualMachines.Count; j++)
					{
						num2 += host.VirtualMachines[j].Metrics.MetricsNow.AveragePifReadsPerSecond + host.VirtualMachines[j].Metrics.MetricsNow.TotalVbdNetReadsPerSecond;
					}
					if (num2 < 0.9 * num)
					{
						DwmAEBase.Trace("Ignoring high net reads for host {0} because it is not caused by the VMs.", new object[]
						{
							this._poolInTrouble.Hosts[i].Name
						});
						break;
					}
					if (host.Metrics.MetricsNow.AveragePifReadsPerSecond > this._poolDetails.HostPifReadThreshold.Critical)
					{
						this.Severity = OptimizationSeverity.Critical;
					}
					flag &= this.BalanceHost(host.Id, new Comparison<DwmVirtualMachine>(DwmAEOptimizer.CompareVMsByNetworkReadIo), (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByVmCountDescend) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByNetworkReadIo), ResourceToOptimize.NetworkRead);
				}
			}
			if (DwmAEBase.VerboseTraceEnabled)
			{
				DwmAEBase.Trace("Pool workload {0} be optimized for Host Network Reads.", new object[]
				{
					(!flag) ? "cannot" : "can"
				});
			}
			return flag;
		}
		private bool BalanceHostNetWrite()
		{
			bool flag = true;
			double num = (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? this._poolDetails.HostPifWriteThreshold.Critical : this._poolDetails.HostPifWriteThreshold.High;
			for (int i = 0; i < this._poolInTrouble.Hosts.Count; i++)
			{
				DwmHost host = this._poolDetails.Hosts.GetHost(this._poolInTrouble.Hosts[i].Id);
				if (host != null && host.Metrics.MetricsNow.AveragePifWritesPerSecond > num)
				{
					double num2 = 0.0;
					for (int j = 0; j < host.VirtualMachines.Count; j++)
					{
						num2 += host.VirtualMachines[j].Metrics.MetricsNow.AveragePifWritesPerSecond + host.VirtualMachines[j].Metrics.MetricsNow.TotalVbdNetWritesPerSecond;
					}
					if (num2 < 0.9 * num)
					{
						DwmAEBase.Trace("Ignoring high net writes for host {0} because it is not caused by the VMs.", new object[]
						{
							this._poolInTrouble.Hosts[i].Name
						});
						break;
					}
					if (host.Metrics.MetricsNow.AveragePifWritesPerSecond > this._poolDetails.HostPifWriteThreshold.Critical)
					{
						this.Severity = OptimizationSeverity.Critical;
					}
					flag &= this.BalanceHost(host.Id, new Comparison<DwmVirtualMachine>(DwmAEOptimizer.CompareVMsByNetworkWriteIo), (this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByVmCountDescend) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByNetworkWriteIo), ResourceToOptimize.NetworkWrite);
				}
			}
			if (DwmAEBase.VerboseTraceEnabled)
			{
				DwmAEBase.Trace("Pool workload {0} be optimized for Host Network Writes.", new object[]
				{
					(!flag) ? "cannot" : "can"
				});
			}
			return flag;
		}
		private bool BalanceHost(int hostToBalanceID, Comparison<DwmVirtualMachine> vmCompare, Comparison<DwmHost> hostCompare, ResourceToOptimize resourceToOptimize)
		{
			bool result = false;
			if (this._poolDetails.Hosts != null)
			{
				DwmHost host = this._poolDetails.Hosts.GetHost(hostToBalanceID);
				if (host != null)
				{
					if (host.VirtualMachines != null)
					{
						host.VirtualMachines.Sort(vmCompare);
						this._poolDetails.Hosts.Sort(hostCompare);
						result = this.BalanceHost(host, hostCompare, resourceToOptimize);
					}
				}
				else
				{
					Logger.Trace("DwmAnalysisEngine.BalanceHost:  No host with id={0} in the pool", new object[]
					{
						hostToBalanceID
					});
				}
			}
			else
			{
				Logger.Trace("DwmAnalysisEngine.BalanceHost:  No hosts in the pool!");
			}
			return result;
		}
		private bool BalanceHost(DwmHost hostToBalance, Comparison<DwmHost> hostCompare, ResourceToOptimize resourceToOptimize)
		{
			double num;
			double num2;
			double num3;
			double num4;
			double num5;
			double num6;
			if (this._poolDetails.OptMode == OptimizationMode.MaxPerformance)
			{
				num = this._poolDetails.HostCpuThreshold.High;
				num2 = this._poolDetails.HostMemoryThreshold.High;
				num3 = this._poolDetails.HostPifReadThreshold.High;
				num4 = this._poolDetails.HostPifWriteThreshold.High;
				num5 = this._poolDetails.HostPbdReadThreshold.High;
				num6 = this._poolDetails.HostPbdWriteThreshold.High;
			}
			else
			{
				num = this._poolDetails.HostCpuThreshold.Critical;
				num2 = this._poolDetails.HostMemoryThreshold.Critical;
				num3 = this._poolDetails.HostPifReadThreshold.Critical;
				num4 = this._poolDetails.HostPifWriteThreshold.Critical;
				num5 = this._poolDetails.HostPbdReadThreshold.Critical;
				num6 = this._poolDetails.HostPbdWriteThreshold.Critical;
			}
			if (hostToBalance.IsPoolMaster && this._poolDetails.PoolMasterCpuLimit > 0.0)
			{
				num = this._poolDetails.PoolMasterCpuLimit;
			}
			bool flag = true;
			bool flag2 = false;
			DwmVirtualMachineCollection dwmVirtualMachineCollection = null;
			for (int i = 0; i < hostToBalance.VirtualMachines.Count; i++)
			{
				DwmVirtualMachine dwmVirtualMachine = hostToBalance.VirtualMachines[i];
				if (!this._poolDetails.RecentVmMoves.ContainsVm(dwmVirtualMachine.Id))
				{
					if (dwmVirtualMachine.IsAgile)
					{
						flag = false;
						bool flag3 = false;
						foreach (DwmHost current in this._poolDetails.Hosts)
						{
							if (current.Enabled && current.Id != hostToBalance.Id 
                                && this.CanMoveVM(dwmVirtualMachine, current, this._poolDetails))
							{
								this.RecommendMove(dwmVirtualMachine, current, hostToBalance, resourceToOptimize);
								flag3 = true;
								i--;
								switch (resourceToOptimize)
								{
								case ResourceToOptimize.Cpu:
									if (hostToBalance.Metrics.MetricsNow.AverageCpuUtilization < num)
									{
										flag = true;
									}
									break;
								case ResourceToOptimize.Memory:
									if ((double)((this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? hostToBalance.Metrics.PotentialFreeMemory : hostToBalance.Metrics.FreeMemory) > num2)
									{
										flag = true;
									}
									break;
								case ResourceToOptimize.Disk:
									if (hostToBalance.Metrics.MetricsNow.AveragePbdReadsPerSecond < num5 && hostToBalance.Metrics.MetricsNow.AveragePbdWritesPerSecond < num6)
									{
										flag = true;
									}
									break;
								case ResourceToOptimize.Network:
									if (hostToBalance.Metrics.MetricsNow.AveragePifReadsPerSecond < num3 && hostToBalance.Metrics.MetricsNow.AveragePifWritesPerSecond < num4)
									{
										flag = true;
									}
									break;
								case ResourceToOptimize.DiskRead:
									if (hostToBalance.Metrics.MetricsNow.AveragePbdReadsPerSecond < num5)
									{
										flag = true;
									}
									break;
								case ResourceToOptimize.DiskWrite:
									if (hostToBalance.Metrics.MetricsNow.AveragePbdWritesPerSecond < num6)
									{
										flag = true;
									}
									break;
								case ResourceToOptimize.NetworkRead:
									if (hostToBalance.Metrics.MetricsNow.AveragePifReadsPerSecond < num3)
									{
										flag = true;
									}
									break;
								case ResourceToOptimize.NetworkWrite:
									if (hostToBalance.Metrics.MetricsNow.AveragePifWritesPerSecond < num4)
									{
										flag = true;
									}
									break;
								case ResourceToOptimize.LoadAverage:
									flag = true;
									break;
								case ResourceToOptimize.Runstate:
									if ((hostToBalance.Metrics.NumHighPartialContentionVCpus == 0 && hostToBalance.Metrics.NumHighConcurrencyHazardVCpus == 0 && hostToBalance.Metrics.NumHighPartialContentionVCpus == 0) || (double)dwmVirtualMachine.MinimumCpus >= (double)hostToBalance.Metrics.NumHighPartialContentionVCpus * 0.5 + (double)hostToBalance.Metrics.NumHighConcurrencyHazardVCpus * 0.3 + (double)hostToBalance.Metrics.NumHighPartialContentionVCpus * 0.2)
									{
										flag = true;
									}
									break;
								}
								if (flag)
								{
									return flag;
								}
								this._poolDetails.Hosts.Sort(hostCompare);
								break;
							}
						}
						if (!flag3)
						{
							DwmBase.SafeGetItem<DwmVirtualMachineCollection>(ref dwmVirtualMachineCollection);
							if (!flag2 && !dwmVirtualMachineCollection.ContainsKey(dwmVirtualMachine.Id))
							{
								dwmVirtualMachineCollection.Add(dwmVirtualMachine);
								flag2 = this.WouldMovesBalanceHost(dwmVirtualMachineCollection, hostToBalance, resourceToOptimize);
							}
						}
					}
					else
					{
						DwmAEBase.Trace("Not considering VM ({0}) {1} for relocation because it is not agile.", new object[]
						{
							dwmVirtualMachine.Id,
							dwmVirtualMachine.Name
						});
					}
				}
				else
				{
					DwmAEBase.Trace("Not considering VM ({0}) {1} for relocation because it has recently been moved", new object[]
					{
						dwmVirtualMachine.Id,
						dwmVirtualMachine.Name
					});
				}
			}
			if (!flag)
			{
				if (flag2)
				{
					this.ShouldMoveVMs.AddRange(dwmVirtualMachineCollection);
				}
				else
				{
					if (dwmVirtualMachineCollection != null && dwmVirtualMachineCollection.Count > 0)
					{
						DwmAEBase.Trace("Host ({0}) {1} cannot be balanced by moving all the agile VMs.", new object[]
						{
							hostToBalance.Id,
							hostToBalance.Name
						});
						flag = true;
					}
					else
					{
						DwmAEBase.Trace("Host ({0}) {1} cannot be balanced because there are no agile VMs that could not be moved.", new object[]
						{
							hostToBalance.Id,
							hostToBalance.Name
						});
						flag = true;
					}
				}
			}
			return flag;
		}
		private bool WouldMovesBalanceHost(List<DwmVirtualMachine> agileVMs, DwmHost hostToBalance, ResourceToOptimize resourceToOptimize)
		{
			bool result = false;
			double num;
			double num2;
			double num3;
			double num4;
			double num5;
			double num6;
			if (this._poolDetails.OptMode == OptimizationMode.MaxPerformance)
			{
				num = this._poolDetails.HostCpuThreshold.High;
				num2 = this._poolDetails.HostMemoryThreshold.High;
				num3 = this._poolDetails.HostPifReadThreshold.High;
				num4 = this._poolDetails.HostPifWriteThreshold.High;
				num5 = this._poolDetails.HostPbdReadThreshold.High;
				num6 = this._poolDetails.HostPbdWriteThreshold.High;
			}
			else
			{
				num = this._poolDetails.HostCpuThreshold.Critical;
				num2 = this._poolDetails.HostMemoryThreshold.Critical;
				num3 = this._poolDetails.HostPifReadThreshold.Critical;
				num4 = this._poolDetails.HostPifWriteThreshold.Critical;
				num5 = this._poolDetails.HostPbdReadThreshold.Critical;
				num6 = this._poolDetails.HostPbdWriteThreshold.Critical;
			}
			double num7 = 0.0;
			double num8 = 0.0;
			double num9 = 0.0;
			double num10 = 0.0;
			long num11 = 0L;
			double num12 = 0.0;
			double num13 = 0.0;
			double num14 = 0.0;
			double num15 = 0.0;
			switch (resourceToOptimize)
			{
			case ResourceToOptimize.Cpu:
				foreach (DwmVirtualMachine current in agileVMs)
				{
					num10 += current.Metrics.MetricsNow.AverageCpuUtilization * ((double)current.MinimumCpus / (double)hostToBalance.NumCpus);
				}
				if (hostToBalance.Metrics.MetricsNow.AverageCpuUtilization - num10 < num)
				{
					result = true;
				}
				break;
			case ResourceToOptimize.Memory:
				foreach (DwmVirtualMachine current2 in agileVMs)
				{
					num11 += ((this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? (current2.MaximumDynamicMemory + current2.MemoryOverhead) : current2.RequiredMemory);
				}
				if ((double)((this._poolDetails.OptMode != OptimizationMode.MaxPerformance) ? hostToBalance.Metrics.PotentialFreeMemory : hostToBalance.Metrics.FreeMemory) < num2)
				{
					result = true;
				}
				break;
			case ResourceToOptimize.Disk:
				foreach (DwmVirtualMachine current3 in agileVMs)
				{
					num12 += current3.Metrics.MetricsNow.AveragePbdReadsPerSecond;
					num13 += current3.Metrics.MetricsNow.AveragePbdWritesPerSecond;
				}
				if (hostToBalance.Metrics.MetricsNow.AveragePbdReadsPerSecond - num12 < num5 && hostToBalance.Metrics.MetricsNow.AveragePbdWritesPerSecond - num13 < num6)
				{
					result = true;
				}
				break;
			case ResourceToOptimize.Network:
				foreach (DwmVirtualMachine current4 in agileVMs)
				{
					num14 += current4.Metrics.MetricsNow.AveragePifReadsPerSecond + current4.Metrics.MetricsNow.TotalVbdNetReadsPerSecond;
					num15 += current4.Metrics.MetricsNow.AveragePifWritesPerSecond + current4.Metrics.MetricsNow.TotalVbdNetWritesPerSecond;
				}
				if (hostToBalance.Metrics.MetricsNow.AveragePifReadsPerSecond - num14 < num3 && hostToBalance.Metrics.MetricsNow.AveragePifWritesPerSecond - num15 < num4)
				{
					result = true;
				}
				break;
			case ResourceToOptimize.DiskRead:
				foreach (DwmVirtualMachine current5 in agileVMs)
				{
					num12 += current5.Metrics.MetricsNow.AveragePbdReadsPerSecond;
				}
				if (hostToBalance.Metrics.MetricsNow.AveragePbdReadsPerSecond - num12 < num5)
				{
					result = true;
				}
				break;
			case ResourceToOptimize.DiskWrite:
				foreach (DwmVirtualMachine current6 in agileVMs)
				{
					num13 += current6.Metrics.MetricsNow.AveragePbdWritesPerSecond;
				}
				if (hostToBalance.Metrics.MetricsNow.AveragePbdWritesPerSecond - num13 < num6)
				{
					result = true;
				}
				break;
			case ResourceToOptimize.NetworkRead:
				foreach (DwmVirtualMachine current7 in agileVMs)
				{
					num14 += current7.Metrics.MetricsNow.AveragePifReadsPerSecond + current7.Metrics.MetricsNow.TotalVbdNetReadsPerSecond;
				}
				if (hostToBalance.Metrics.MetricsNow.AveragePifReadsPerSecond - num14 < num3)
				{
					result = true;
				}
				break;
			case ResourceToOptimize.NetworkWrite:
				foreach (DwmVirtualMachine current8 in agileVMs)
				{
					num15 += current8.Metrics.MetricsNow.AveragePifWritesPerSecond + current8.Metrics.MetricsNow.TotalVbdNetWritesPerSecond;
				}
				if (hostToBalance.Metrics.MetricsNow.AveragePifWritesPerSecond - num15 < num4)
				{
					result = true;
				}
				break;
			case ResourceToOptimize.LoadAverage:
				result = true;
				break;
			case ResourceToOptimize.Runstate:
				foreach (DwmVirtualMachine current9 in agileVMs)
				{
					if (current9.Metrics.MetricsNow.AverageRunstateFullContention >= this._poolDetails.HostRunstateConcurrencyHazardThreshold.High)
					{
						num7 += (double)current9.MinimumCpus;
					}
					if (current9.Metrics.MetricsNow.AverageRunstateConcurrencyHazard >= this._poolDetails.HostRunstateConcurrencyHazardThreshold.High)
					{
						num8 += (double)current9.MinimumCpus;
					}
					if (current9.Metrics.MetricsNow.AverageRunstatePartialContention >= this._poolDetails.HostRunstatePartialContentionThreshold.High)
					{
						num9 += (double)current9.MinimumCpus;
					}
				}
				if ((num7 >= (double)hostToBalance.Metrics.NumHighFullContentionVCpus && num8 >= (double)hostToBalance.Metrics.NumHighConcurrencyHazardVCpus && num9 >= (double)hostToBalance.Metrics.NumHighPartialContentionVCpus) || num7 + num8 + num9 >= (double)hostToBalance.Metrics.NumHighFullContentionVCpus * 0.5 + (double)hostToBalance.Metrics.NumHighConcurrencyHazardVCpus * 0.3 + (double)hostToBalance.Metrics.NumHighPartialContentionVCpus * 0.2)
				{
					result = true;
				}
				break;
			}
			return result;
		}
		private bool CanMoveVM(DwmVirtualMachine vmToMove, DwmHost moveToHost, DwmPool pool)
		{
			bool result = false;
			long num = (!this._compressGuests) ? vmToMove.RequiredMemory : (vmToMove.MinimumDynamicMemory + vmToMove.MemoryOverhead);
			long num2 = (!this._compressGuests) ? moveToHost.Metrics.FreeMemory : moveToHost.Metrics.PotentialFreeMemory;
			double num3 = (pool.OptMode != OptimizationMode.MaxPerformance) ? pool.HostMemoryThreshold.Critical : pool.HostMemoryThreshold.High;
			if (!moveToHost.ExcludeFromPoolOptimizationAcceptVMs)
			{
				if (moveToHost.NumCpus >= vmToMove.MinimumCpus && (double)(num2 - num) >= num3 && DwmAEOptimizer.HostHasAllStorage(moveToHost, vmToMove.RequiredStorage))
				{
					if (DwmAEOptimizer.HostCanHandleLoadAverage(moveToHost, vmToMove, pool) &&
                        DwmAEOptimizer.HostCanHandleCpuLoad(moveToHost, vmToMove, pool) && 
                        DwmAEOptimizer.HostCanHandleDiskLoad(moveToHost, vmToMove, pool) && 
                        DwmAEOptimizer.HostCanHandleNetworkLoad(moveToHost, vmToMove, pool) &&
                        DwmAEOptimizer.HostCanHandleRunstateLoad(moveToHost, vmToMove, pool))
					{
						if (moveToHost.PowerState == PowerStatus.On)
						{
							ICollectorActions collector = this.GetCollector(pool.Id);
							if (collector != null)
							{
								bool flag;
								bool.TryParse(pool.GetOtherConfigItem("HonorHAPlan"), out flag);
								CantBootReason cantBootReason = collector.CanStartVM(vmToMove.Uuid, moveToHost.Uuid);
								if (cantBootReason == CantBootReason.None || 
                                    (cantBootReason == CantBootReason.HAOperationWouldBreakPlan && !flag) || 
                                    cantBootReason == CantBootReason.HostNotEnoughFreeMemory)
								{
									result = true;
								}
								else
								{
									DwmAEBase.Trace("Not moving vm {0} to host {1} because CantBootReason is {2}", new object[]
									{
										vmToMove.Id,
										moveToHost.Id,
										cantBootReason.ToString()
									});
								}
							}
							else
							{
								DwmAEBase.Trace("Not moving vm {0} to host {1} because could not get ICollectorActions instance for pool", new object[]
								{
									vmToMove.Id,
									moveToHost.Id
								});
							}
						}
						else
						{
							result = true;
						}
					}
				}
				else
				{
					DwmAEBase.Trace("Not moving vm {0} \"{1}\" to host {2} \"{3}\".  One of these failed:", new object[]
					{
						vmToMove.Id,
						vmToMove.Name,
						moveToHost.Id,
						moveToHost.Name
					});
					DwmAEBase.Trace("   moveToHost.NumCpus ({0}) >= vmToMove.MinimumCpus ({1})", new object[]
					{
						moveToHost.NumCpus,
						vmToMove.MinimumCpus
					});
					DwmAEBase.Trace("   moveToHost.FreeMemory ({0,4:N}) - vmRequiredMemory({1,4:N}) > memoryLimit({2})", new object[]
					{
						num2,
						num,
						num3
					});
					DwmAEBase.Trace("   Host must have required storage.", new object[0]);
				}
			}
			else
			{
				DwmAEBase.Trace("Not moving vm {0} to host {1} because the ExcludeFromPoolOptimizationAcceptVMs flag is set to true", new object[]
				{
					vmToMove.Id,
					moveToHost.Id
				});
			}
			return result;
		}
		private DwmHost ConsolidateHost(int hostToConsolidateID, Comparison<DwmVirtualMachine> vmCompare, Comparison<DwmHost> hostCompare, ResourceToOptimize resourceToOptimize)
		{
			bool flag = true;
			DwmHost result = null;
			if (this._poolDetails.Hosts != null)
			{
				DwmHost host = this._poolDetails.Hosts.GetHost(hostToConsolidateID);
				if (host != null)
				{
					if (!host.IsPoolMaster)
					{
						if (host.VirtualMachines != null)
						{
							DwmHostCollection hosts = this._poolDetails.Hosts.Copy();
							int count = this._recommendations.Count;
							int count2 = host.VirtualMachines.Count;
							for (int i = count2 - 1; i >= 0; i--)
							{
								if (!host.VirtualMachines[i].IsAgile || !this.TryMoveVm(host.VirtualMachines[i], this._poolDetails.Hosts, host))
								{
									if (!host.VirtualMachines[i].IsAgile)
									{
										Logger.Trace("VM {0} \"{1}\" is not agile", new object[]
										{
											host.VirtualMachines[i].Id,
											host.VirtualMachines[i].Name
										});
									}
									Logger.Trace("Cannot migrate VM {0} \"{1}\" off host {2} \"{3}\".  Ignoring the last {4} moves.", new object[]
									{
										host.VirtualMachines[i].Id,
										host.VirtualMachines[i].Name,
										host.Id,
										host.Name,
										this._recommendations.Count - count
									});
									this._poolDetails.Hosts = hosts;
									this._recommendations.RemoveRange(count, this._recommendations.Count - count);
									flag = false;
									break;
								}
							}
							if (flag)
							{
								result = host;
							}
						}
					}
					else
					{
						Logger.Trace("Not attempting to consolidate host {0} \"{1}\" because it is the pool master", new object[]
						{
							host.Id,
							host.Name
						});
					}
				}
				else
				{
					Logger.Trace("DwmAnalysisEngine.ConsolidateHost:  No host with id={0} in the pool", new object[]
					{
						hostToConsolidateID
					});
				}
			}
			else
			{
				Logger.Trace("DwmAnalysisEngine.ConsolidateHost:  No hosts in the pool");
			}
			return result;
		}
		private bool TryMoveVm(DwmVirtualMachine vmToMove, DwmHostCollection possibleHosts, DwmHost hostToConsolidate)
		{
			bool result = false;
			for (int i = 0; i < possibleHosts.Count; i++)
			{
				if (possibleHosts[i].Id != hostToConsolidate.Id && (possibleHosts[i].VirtualMachines.Count > 0 || possibleHosts[i].IsPoolMaster) && this.CanMoveVM(vmToMove, possibleHosts[i], this._poolDetails))
				{
					this.RecommendMove(vmToMove, possibleHosts[i], hostToConsolidate, ResourceToOptimize.Consolidate);
					result = true;
					break;
				}
			}
			return result;
		}
		private void RecommendMove(DwmVirtualMachine vmToMove, DwmHost moveToHost, DwmHost moveFromHost, ResourceToOptimize resourceToOptimize)
		{
			Logger.Trace("Recommending moving VM {0} from {1} to {2} to alleviate {3} pressure.", new object[]
			{
				vmToMove.Name,
				moveFromHost.Name,
				moveToHost.Name,
				resourceToOptimize
			});
			MoveRecommendation moveRecommendation = new MoveRecommendation();
			moveRecommendation.PoolId = this._poolDetails.Id;
			moveRecommendation.PoolUuid = this._poolDetails.Uuid;
			moveRecommendation.VmId = vmToMove.Id;
			moveRecommendation.VmUuid = vmToMove.Uuid;
			moveRecommendation.VmName = vmToMove.Name;
			moveRecommendation.MoveToHostId = moveToHost.Id;
			moveRecommendation.MoveToHostUuid = moveToHost.Uuid;
			moveRecommendation.MoveToHostName = moveToHost.Name;
			moveRecommendation.MoveFromHostId = vmToMove.RunningOnHostId;
			moveRecommendation.MoveFromHostUuid = vmToMove.RunningOnHostUuid;
			moveRecommendation.MoveFromHostName = vmToMove.RunningOnHostName;
			moveRecommendation.Reason = resourceToOptimize;
			moveRecommendation.TimeStamp = DateTime.UtcNow;
			moveRecommendation.Severity = this.Severity;
			this._recommendations.Add(moveRecommendation);
			long num = (!this._compressGuests) ? vmToMove.RequiredMemory : (vmToMove.MinimumDynamicMemory + vmToMove.MemoryOverhead);
			moveToHost.Metrics.MetricsNow.AverageCpuUtilization += vmToMove.Metrics.MetricsNow.AverageCpuUtilization * ((double)vmToMove.MinimumCpus / (double)moveToHost.NumCpus);
			moveToHost.Metrics.MetricsNow.AverageFreeMemory -= num;
			moveToHost.Metrics.FreeMemory -= num;
			moveToHost.Metrics.PotentialFreeMemory -= num;
			moveToHost.Metrics.MetricsNow.AveragePbdReadsPerSecond += vmToMove.Metrics.MetricsNow.AveragePbdReadsPerSecond;
			moveToHost.Metrics.MetricsNow.AveragePbdWritesPerSecond += vmToMove.Metrics.MetricsNow.AveragePbdWritesPerSecond;
			moveToHost.Metrics.MetricsNow.AveragePifReadsPerSecond += vmToMove.Metrics.MetricsNow.AveragePifReadsPerSecond;
			moveToHost.Metrics.MetricsNow.AveragePifWritesPerSecond += vmToMove.Metrics.MetricsNow.AveragePifWritesPerSecond;
			moveToHost.VirtualMachines.Add(vmToMove);
			moveFromHost.Metrics.MetricsNow.AverageCpuUtilization -= vmToMove.Metrics.MetricsNow.AverageCpuUtilization * ((double)vmToMove.MinimumCpus / (double)moveFromHost.NumCpus);
			moveFromHost.Metrics.MetricsNow.AverageFreeMemory += num;
			moveFromHost.Metrics.FreeMemory += num;
			moveFromHost.Metrics.PotentialFreeMemory += num;
			moveFromHost.Metrics.MetricsNow.AveragePbdReadsPerSecond -= vmToMove.Metrics.MetricsNow.AveragePbdReadsPerSecond;
			moveFromHost.Metrics.MetricsNow.AveragePbdWritesPerSecond -= vmToMove.Metrics.MetricsNow.AveragePbdWritesPerSecond;
			moveFromHost.Metrics.MetricsNow.AveragePifReadsPerSecond -= vmToMove.Metrics.MetricsNow.AveragePifReadsPerSecond;
			moveFromHost.Metrics.MetricsNow.AveragePifWritesPerSecond -= vmToMove.Metrics.MetricsNow.AveragePifWritesPerSecond;
			moveFromHost.Metrics.NumHighFullContentionVCpus -= vmToMove.MinimumCpus;
			moveFromHost.Metrics.NumHighPartialContentionVCpus -= vmToMove.MinimumCpus;
			moveFromHost.Metrics.NumHighPartialContentionVCpus -= vmToMove.MinimumCpus;
			moveFromHost.VirtualMachines.Remove(vmToMove);
		}
		private void AddPowerRecommendationsToMoveRecommendations()
		{
			if (this._hostsToPowerOn != null)
			{
				for (int i = 0; i < this._hostsToPowerOn.Count; i++)
				{
					DwmHost dwmHost = this._hostsToPowerOn[i];
					MoveRecommendation moveRecommendation = new MoveRecommendation();
					moveRecommendation.PoolId = this._poolDetails.Id;
					moveRecommendation.PoolUuid = this._poolDetails.Uuid;
					moveRecommendation.MoveFromHostId = dwmHost.Id;
					moveRecommendation.MoveFromHostName = dwmHost.Name;
					moveRecommendation.MoveFromHostUuid = dwmHost.Uuid;
					moveRecommendation.Reason = ResourceToOptimize.PowerOn;
					moveRecommendation.Severity = this.Severity;
					this._recommendations.Insert(0, moveRecommendation);
				}
			}
			if (this._hostsToPowerOff != null)
			{
				for (int j = 0; j < this._hostsToPowerOff.Count; j++)
				{
					DwmHost dwmHost2 = this._hostsToPowerOff[j];
					MoveRecommendation moveRecommendation2 = new MoveRecommendation();
					moveRecommendation2.PoolId = this._poolDetails.Id;
					moveRecommendation2.PoolUuid = this._poolDetails.Uuid;
					moveRecommendation2.MoveFromHostId = dwmHost2.Id;
					moveRecommendation2.MoveFromHostName = dwmHost2.Name;
					moveRecommendation2.MoveFromHostUuid = dwmHost2.Uuid;
					moveRecommendation2.Reason = ResourceToOptimize.PowerOff;
					moveRecommendation2.Severity = this.Severity;
					this._recommendations.Add(moveRecommendation2);
				}
			}
		}
		private void SaveRecommendations()
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime;
			DateTime value;
			List<MoveRecommendation> lastRecommendations = DwmAEOptimizer.GetLastRecommendations(this._poolDetails.Id, out dateTime, out value);
			DateTime? dateTime2 = new DateTime?(value);
			this.ConsolidateRecommendations();
			bool flag = DwmAEOptimizer.CompareRecommendations(this._recommendations, lastRecommendations);
			bool flag2 = true;
			if (flag && dateTime != DateTime.MinValue && dateTime2 != DateTime.MinValue)
			{
				TimeSpan timeSpan = utcNow - dateTime;
				TimeSpan timeSpan2 = utcNow - dateTime2.Value;
				flag2 = false;
				if (timeSpan.TotalDays >= 30.0)
				{
					if (timeSpan2.TotalDays >= 30.0)
					{
						flag2 = true;
					}
				}
				else
				{
					if (timeSpan.TotalDays >= 7.0)
					{
						if (timeSpan2.TotalDays >= 30.0)
						{
							flag2 = true;
						}
					}
					else
					{
						if (timeSpan.TotalHours >= 24.0)
						{
							if (timeSpan2.TotalHours >= 24.0)
							{
								flag2 = true;
							}
						}
						else
						{
							if (timeSpan.TotalMinutes >= 60.0 && timeSpan2.TotalMinutes >= 60.0)
							{
								flag2 = true;
							}
						}
					}
				}
			}
			if (flag2)
			{
				this.NotifyPoolMaster(this._poolDetails.Id, this.Severity, this._poolDetails.OptMode);
				dateTime2 = new DateTime?(DateTime.UtcNow);
			}
			this.SaveRecommendations(new DateTime?((!flag) ? utcNow : dateTime), dateTime2);
		}
		private void SaveRecommendations(DateTime? origonalTimeStamp, DateTime? lastNotifyTimeStamp)
		{
			int num = 0;
			DateTime utcNow = DateTime.UtcNow;
			for (int i = 0; i < this._recommendations.Count; i++)
			{
				MoveRecommendation moveRecommendation = this._recommendations[i];
				moveRecommendation.RecommendationId = DwmAudit.AddRecommendationRecord(ref num, MoveRecommendationType.PoolOptimizationRecommendation, new int?(moveRecommendation.VmId), new int?(moveRecommendation.MoveFromHostId), new int?(moveRecommendation.MoveToHostId), moveRecommendation.Severity, moveRecommendation.Reason, utcNow, origonalTimeStamp, lastNotifyTimeStamp);
				if (moveRecommendation.Reason != ResourceToOptimize.PowerOn && moveRecommendation.Reason != ResourceToOptimize.PowerOff)
				{
					Logger.Trace("Recommend moving VM {0} \"{1}\" from host {2} \"{3}\" to host {4} \"{5}\"", new object[]
					{
						moveRecommendation.VmId,
						moveRecommendation.VmName,
						moveRecommendation.MoveFromHostId,
						moveRecommendation.MoveFromHostName,
						moveRecommendation.MoveToHostId,
						moveRecommendation.MoveToHostName
					});
				}
				else
				{
					Logger.Trace("Recommend {0} for host {1} \"{2}\"", new object[]
					{
						moveRecommendation.Reason,
						moveRecommendation.MoveFromHostId,
						moveRecommendation.MoveFromHostName
					});
				}
			}
			Logger.Trace("Recommendation severity is {0}", new object[]
			{
				this.Severity.ToString()
			});
		}
		private static List<MoveRecommendation> GetLastRecommendations(int poolId, out DateTime originalTimeStamp, out DateTime lastAlertTimeStamp)
		{
			string sqlStatement = "move_recommendation_get_last";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@pool_id", poolId));
			List<MoveRecommendation> list = null;
			originalTimeStamp = DateTime.MinValue;
			lastAlertTimeStamp = DateTime.MinValue;
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						originalTimeStamp = DBAccess.GetDateTime(dataReader, "original_tstamp");
						lastAlertTimeStamp = DBAccess.GetDateTime(dataReader, "last_alert_tstamp");
						if (dataReader.NextResult())
						{
							list = new List<MoveRecommendation>();
							while (dataReader.Read())
							{
								list.Add(new MoveRecommendation
								{
									VmId = DBAccess.GetInt(dataReader, "vm_id"),
									MoveFromHostId = DBAccess.GetInt(dataReader, "from_host_id"),
									MoveToHostId = DBAccess.GetInt(dataReader, "to_host_id"),
									Reason = (ResourceToOptimize)DBAccess.GetInt(dataReader, "reason"),
									TimeStamp = originalTimeStamp,
									Severity = (OptimizationSeverity)DBAccess.GetInt(dataReader, "severity")
								});
							}
						}
					}
				}
			}
			return list;
		}
		private static bool CompareRecommendations(List<MoveRecommendation> l1, List<MoveRecommendation> l2)
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
						if (l2[j].VmId == l1[i].VmId)
						{
							moveRecommendation = l2[j];
							break;
						}
					}
					if (moveRecommendation == null)
					{
						break;
					}
					if (moveRecommendation.MoveFromHostId != l1[i].MoveFromHostId || moveRecommendation.MoveToHostId != l1[i].MoveToHostId)
					{
						break;
					}
				}
				result = (i >= l1.Count);
			}
			return result;
		}
		private void NotifyPoolMaster(int poolId, OptimizationSeverity severity, OptimizationMode optMode)
		{
			if (severity == OptimizationSeverity.Critical || severity == OptimizationSeverity.High)
			{
				string text = Localization.Format("severity:{0} mode:{1}", severity.ToString(), optMode.ToString());
				ICollectorActions collector = this.GetCollector(poolId);
				if (collector != null)
				{
					DwmAEBase.Trace("Sending optimization message {0} to pool {1}", new object[]
					{
						text,
						poolId
					});
					collector.SendMessage("WLB_OPTIMIZATION_ALERT", text);
				}
			}
			else
			{
				DwmAEBase.Trace("Not sending optimization message to pool master because severity is {0}", new object[]
				{
					severity
				});
			}
		}
		private void ConsolidateRecommendations()
		{
			DwmAEBase.Trace("Before consolidation", new object[0]);
			for (int i = 0; i < this._recommendations.Count; i++)
			{
				if (this._recommendations[i].Reason != ResourceToOptimize.PowerOff && this._recommendations[i].Reason != ResourceToOptimize.PowerOn)
				{
					DwmAEBase.Trace("Recommend moving VM {0} \"{1}\" from host {2} \"{3}\" to host {4} \"{5}\"", new object[]
					{
						this._recommendations[i].VmId,
						this._recommendations[i].VmName,
						this._recommendations[i].MoveFromHostId,
						this._recommendations[i].MoveFromHostName,
						this._recommendations[i].MoveToHostId,
						this._recommendations[i].MoveToHostName
					});
				}
				else
				{
					DwmAEBase.Trace("Recommend {0} for host {1} \"{2}\"", new object[]
					{
						this._recommendations[i].Reason,
						this._recommendations[i].MoveFromHostId,
						this._recommendations[i].MoveFromHostName
					});
				}
			}
			DwmAEBase.Trace("Recommendation severity is {0}", new object[]
			{
				this.Severity.ToString()
			});
			for (int j = this._recommendations.Count - 1; j >= 0; j--)
			{
				MoveRecommendation moveRecommendation = this._recommendations[j];
				if (moveRecommendation.Reason != ResourceToOptimize.PowerOff && moveRecommendation.Reason != ResourceToOptimize.PowerOn)
				{
					for (int k = j - 1; k >= 0; k--)
					{
						if (this._recommendations[k].VmId == moveRecommendation.VmId)
						{
							moveRecommendation.MoveFromHostId = this._recommendations[k].MoveFromHostId;
							this._recommendations.RemoveAt(k);
							j--;
						}
					}
				}
			}
			DwmAEBase.Trace("After consolidation", new object[0]);
		}
		private static bool HostHasAllStorage(DwmHost host, DwmStorageRepositoryCollection requiredStorage)
		{
			if (requiredStorage == null)
			{
				return true;
			}
			if (host.AvailableStorage == null)
			{
				DwmAEBase.Trace("Not moving VM because host has no storage.", new object[0]);
				return false;
			}
			bool result = true;
			for (int i = 0; i < requiredStorage.Count; i++)
			{
				if (!host.AvailableStorage.ContainsKey(requiredStorage[i].Id))
				{
					DwmAEBase.Trace("Not moving VM because host does not have SR with id={0}", new object[]
					{
						requiredStorage[i].Id
					});
					result = false;
					break;
				}
			}
			return result;
		}
		internal static bool HostCanHandleRunstateLoad(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			bool flag = (host.Metrics.NumHighFullContentionVCpus == 0 
                && host.Metrics.NumHighConcurrencyHazardVCpus == 0 
                && host.Metrics.NumHighPartialContentionVCpus == 0 
                && (double)(host.NumCpus - host.Metrics.NumHighFullrunVCpus) - 0.5 * (double)host.Metrics.NumHighPartialRunVCpus >= (double)vm.MinimumCpus) || host.Metrics.FreeCPUs >= vm.MinimumCpus;
			if (!flag)
			{
				DwmAEBase.Trace("Not moving vm {0} to host {1} because host does not have enough free virtual CPUs.", new object[]
				{
					vm.Id,
					host.Id
				});
			}
			return flag;
		}
		internal static bool HostCanHandleCpuLoad(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			double num = vm.Metrics.MetricsNow.AverageCpuUtilization * ((double)vm.MinimumCpus / (double)host.NumCpus);
			bool flag = host.Metrics.MetricsNow.AverageCpuUtilization + num < 
                ((pool.OptMode != OptimizationMode.MaxPerformance) ? pool.HostCpuThreshold.Critical : pool.HostCpuThreshold.High);
			if (!flag)
			{
				DwmAEBase.Trace("Not moving vm {0} to host {1} because CPU load on host would be too high.", new object[]
				{
					vm.Id,
					host.Id
				});
			}
			else
			{
				if (DwmAEHost.PoolMasterExceedsCpuLimit(host, vm, pool))
				{
					flag = false;
					DwmAEBase.Trace("Not moving vm {0} to host {1} because host is pool master and CPU load on pool master would be too high.", new object[]
					{
						vm.Id,
						host.Id
					});
				}
			}
			return flag;
		}
		internal static bool HostCanHandleDiskLoad(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			double num;
            double num2;
			if (pool.OptMode == OptimizationMode.MaxPerformance)
			{
				num = pool.HostPbdReadThreshold.High;
				num2 = pool.HostPbdWriteThreshold.High;
			}
			else
			{
				num = pool.HostPbdReadThreshold.Critical;
				num2 = pool.HostPbdWriteThreshold.Critical;
			}
            DwmAEBase.Trace(">>>>>>>>>>>>>>>>>>>>>>>>HostCanHandleDistLoad<<<<<<<<<<<<<<<<<<<<<<<");
            DwmAEBase.Trace("host.Metrics.MetricsNow.AveragePbdReadsPerSecond:{0}", new object[] { host.Metrics.MetricsNow.AveragePbdReadsPerSecond });
            DwmAEBase.Trace("vm.Metrics.MetricsNow.AveragePbdReadsPerSecond:{0}", new object[] { vm.Metrics.MetricsNow.AveragePbdReadsPerSecond });
            DwmAEBase.Trace("host.Metrics.MetricsNow.AveragePbdWritesPerSecond:{0}", new object[] { host.Metrics.MetricsNow.AveragePbdWritesPerSecond });
            DwmAEBase.Trace("vm.Metrics.MetricsNow.AveragePbdWritesPerSecond:{0}", new object[] { vm.Metrics.MetricsNow.AveragePbdWritesPerSecond });
            DwmAEBase.Trace(">>>>>>>>>>>>>>>>>>>>>>>>HostCanHandleDistLoad<<<<<<<<<<<<<<<<<<<<<<<");
            //bool flag = host.Metrics.MetricsNow.AveragePbdReadsPerSecond + vm.Metrics.MetricsNow.AveragePbdReadsPerSecond < num &&
            //                host.Metrics.MetricsNow.AveragePbdWritesPerSecond + vm.Metrics.MetricsNow.AveragePbdWritesPerSecond < num2;
            bool flag = true;
			if (!flag)
			{
				DwmAEBase.Trace("Not moving vm {0} to host {1} because disk IO load on host would be too high.", new object[]
				{
					vm.Id,
					host.Id
				});
			}
			return flag;
		}
		internal static bool HostCanHandleNetworkLoad(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			double num;
			double num2;
			if (pool.OptMode == OptimizationMode.MaxPerformance)
			{
				num = pool.HostPifReadThreshold.High;
				num2 = pool.HostPifWriteThreshold.High;
			}
			else
			{
				num = pool.HostPifReadThreshold.Critical;
				num2 = pool.HostPifWriteThreshold.Critical;
			}
            DwmAEBase.Trace(">>>>>>>>>>>>>>>>>>>>>>>>HostCanHandleNetworkLoad start<<<<<<<<<<<<<<<<<<<<<<<");
            DwmAEBase.Trace("pool.HostPifReadThreshold.High:{0}", new object[] { pool.HostPifReadThreshold.High });
            DwmAEBase.Trace("host.Metrics.MetricsNow.AveragePifReadsPerSecond:{0}", new object[] { host.Metrics.MetricsNow.AveragePifReadsPerSecond });
            DwmAEBase.Trace("vm.Metrics.MetricsNow.AveragePifReadsPerSecond:{0}", new object[] { vm.Metrics.MetricsNow.AveragePifReadsPerSecond });
            DwmAEBase.Trace("pool.HostPifWriteThreshold.High:{0}", new object[] { pool.HostPifWriteThreshold.High });
            DwmAEBase.Trace("host.Metrics.MetricsNow.AveragePifWritesPerSecond:{0}", new object[] { host.Metrics.MetricsNow.AveragePifWritesPerSecond });
            DwmAEBase.Trace("vm.Metrics.MetricsNow.AveragePifWritesPerSecond:{0}", new object[] { vm.Metrics.MetricsNow.AveragePifWritesPerSecond });
            DwmAEBase.Trace(">>>>>>>>>>>>>>>>>>>>>>>>HostCanHandleNetworkLoad end<<<<<<<<<<<<<<<<<<<<<<<");
            bool flag = host.Metrics.MetricsNow.AveragePifReadsPerSecond
                + vm.Metrics.MetricsNow.AveragePifReadsPerSecond
                /**+ vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond**/ < num
                && host.Metrics.MetricsNow.AveragePifWritesPerSecond
                + vm.Metrics.MetricsNow.AveragePifWritesPerSecond
                /**+ vm.Metrics.MetricsNow.TotalVbdNetWritesPerSecond**/ < num2;
            if (!flag)
            {
                DwmAEBase.Trace("Not moving vm {0} to host {1} because network IO load on host would be too high.", new object[]
                {
                    vm.Id,
                    host.Id
                });
            }
            else
			{
				if (DwmAEHost.PoolMasterExceedsNetIoLimit(host, vm, pool))
				{
					flag = false;
					DwmAEBase.Trace("Not moving vm {0} to host {1} because host is pool master and network IO load on pool master would be too high.", new object[]
					{
						vm.Id,
						host.Id
					});
				}
			}
			return flag;
		}
		internal static bool HostCanHandleLoadAverage(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			bool flag = host.Metrics.MetricsNow.AverageLoadAverage < pool.HostLoadAverageThreshold.High;
			if (!flag)
			{
				DwmAEBase.Trace("Not moving vm {0} to host {1} because load average on host would be too high.", new object[]
				{
					vm.Id,
					host.Id
				});
			}
			return flag;
		}
		private static int CompareVMsByRunstate(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			double num = y.Metrics.MetricsNow.AverageRunstateFullContention + y.Metrics.MetricsNow.AverageRunstateConcurrencyHazard * 0.5 + y.Metrics.MetricsNow.AverageRunstatePartialContention * 0.25 * (double)y.MinimumCpus - (x.Metrics.MetricsNow.AverageRunstateFullContention + x.Metrics.MetricsNow.AverageRunstateConcurrencyHazard * 0.5 + x.Metrics.MetricsNow.AverageRunstatePartialContention * 0.25 * (double)x.MinimumCpus);
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareVMsByCpuUtilAscend(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			double num = (double)x.MinimumCpus * x.Metrics.MetricsNow.AverageCpuUtilization - (double)y.MinimumCpus * y.Metrics.MetricsNow.AverageCpuUtilization;
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareVMsByCpuUtilDesc(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			double num = (double)y.MinimumCpus * y.Metrics.MetricsNow.AverageCpuUtilization - (double)x.MinimumCpus * x.Metrics.MetricsNow.AverageCpuUtilization;
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareVMsByMemory(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			long num = ((y.MinimumDynamicMemory == y.MaximumStaticMemory) ? y.RequiredMemory : (y.MinimumDynamicMemory + y.MemoryOverhead)) - ((x.MinimumDynamicMemory == x.MaximumStaticMemory) ? x.RequiredMemory : (x.MinimumDynamicMemory + x.MemoryOverhead));
			if (num < 0L)
			{
				return -1;
			}
			if (num > 0L)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareVMsByDiskReadIo(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			double num = y.Metrics.MetricsNow.AveragePbdReadsPerSecond - x.Metrics.MetricsNow.AveragePbdReadsPerSecond;
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareVMsByDiskWriteIo(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			double num = y.Metrics.MetricsNow.AveragePbdWritesPerSecond - x.Metrics.MetricsNow.AveragePbdWritesPerSecond;
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareVMsByNetworkReadIo(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			double num = y.Metrics.MetricsNow.AveragePifReadsPerSecond + y.Metrics.MetricsNow.TotalVbdNetReadsPerSecond - (x.Metrics.MetricsNow.AveragePifReadsPerSecond + x.Metrics.MetricsNow.TotalVbdNetReadsPerSecond);
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareVMsByNetworkWriteIo(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			double num = y.Metrics.MetricsNow.AveragePifWritesPerSecond + y.Metrics.MetricsNow.TotalVbdNetWritesPerSecond - (x.Metrics.MetricsNow.AveragePifWritesPerSecond + x.Metrics.MetricsNow.TotalVbdNetWritesPerSecond);
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareHostsByVmCountDescend(DwmHost x, DwmHost y)
		{
			return y.Metrics.NumVMs.CompareTo(x.Metrics.NumVMs);
		}
		private static int CompareHostsByVmCountAscend(DwmHost x, DwmHost y)
		{
			return x.Metrics.NumVMs.CompareTo(y.Metrics.NumVMs);
		}
		internal static int CompareHostsByRunstate(DwmHost x, DwmHost y)
		{
			int result;
			if (x.IsPoolMaster)
			{
				result = 1;
			}
			else
			{
				if (y.IsPoolMaster)
				{
					result = -1;
				}
				else
				{
					double num = (double)x.Metrics.NumHighFullContentionVCpus + (double)x.Metrics.NumHighConcurrencyHazardVCpus * 0.5 + (double)x.Metrics.NumHighPartialContentionVCpus * 0.25 - ((double)y.Metrics.NumHighFullContentionVCpus + (double)y.Metrics.NumHighConcurrencyHazardVCpus * 0.5 + (double)y.Metrics.NumHighPartialContentionVCpus * 0.25);
					if (num < 0.0)
					{
						result = -1;
					}
					else
					{
						if (num > 0.0)
						{
							result = 1;
						}
						else
						{
							result = DwmAEOptimizer.CompareHostsByCpu(x, y);
						}
					}
				}
			}
			return result;
		}
		internal static int CompareHostsByCpu(DwmHost x, DwmHost y)
		{
			int result;
			if (x.IsPoolMaster)
			{
				result = 1;
			}
			else
			{
				if (y.IsPoolMaster)
				{
					result = -1;
				}
				else
				{
					double num = x.CpuScore - y.CpuScore;
					if (num < 0.0)
					{
						result = 1;
					}
					else
					{
						if (num > 0.0)
						{
							result = -1;
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}
		internal static int CompareHostsByCpuRating(DwmHost x, DwmHost y)
		{
			double num = (double)(y.NumCpus * y.CpuSpeed - x.NumCpus * x.CpuSpeed);
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
		internal static int CompareHostsByFreeMemory(DwmHost x, DwmHost y)
		{
			int result;
			if (x.IsPoolMaster)
			{
				result = 1;
			}
			else
			{
				if (y.IsPoolMaster)
				{
					result = -1;
				}
				else
				{
					long num = y.Metrics.FreeMemory - x.Metrics.FreeMemory;
					if (num < 0L)
					{
						result = -1;
					}
					else
					{
						if (num > 0L)
						{
							result = 1;
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}
		internal static int CompareHostsByTotalMemory(DwmHost x, DwmHost y)
		{
			long num = y.Metrics.TotalMemory - x.Metrics.TotalMemory;
			if (num < 0L)
			{
				return -1;
			}
			if (num > 0L)
			{
				return 1;
			}
			return 0;
		}
		private static int CompareHostsByDiskReadIo(DwmHost x, DwmHost y)
		{
			int result;
			if (x.IsPoolMaster)
			{
				result = 1;
			}
			else
			{
				if (y.IsPoolMaster)
				{
					result = -1;
				}
				else
				{
					double num = x.Metrics.MetricsNow.AveragePbdReadsPerSecond - y.Metrics.MetricsNow.AveragePbdReadsPerSecond;
					if (num < 0.0)
					{
						result = -1;
					}
					else
					{
						if (num > 0.0)
						{
							result = 1;
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}
		private static int CompareHostsByDiskWriteIo(DwmHost x, DwmHost y)
		{
			int result;
			if (x.IsPoolMaster)
			{
				result = 1;
			}
			else
			{
				if (y.IsPoolMaster)
				{
					result = -1;
				}
				else
				{
					double num = x.Metrics.MetricsNow.AveragePbdWritesPerSecond - y.Metrics.MetricsNow.AveragePbdWritesPerSecond;
					if (num < 0.0)
					{
						result = -1;
					}
					else
					{
						if (num > 0.0)
						{
							result = 1;
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}
		private static int CompareHostsByNetworkReadIo(DwmHost x, DwmHost y)
		{
			int result;
			if (x.IsPoolMaster)
			{
				result = 1;
			}
			else
			{
				if (y.IsPoolMaster)
				{
					result = -1;
				}
				else
				{
					double num = x.Metrics.MetricsNow.AveragePifReadsPerSecond - y.Metrics.MetricsNow.AveragePifReadsPerSecond;
					if (num < 0.0)
					{
						result = -1;
					}
					else
					{
						if (num > 0.0)
						{
							result = 1;
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}
		private static int CompareHostsByNetworkWriteIo(DwmHost x, DwmHost y)
		{
			int result;
			if (x.IsPoolMaster)
			{
				result = 1;
			}
			else
			{
				if (y.IsPoolMaster)
				{
					result = -1;
				}
				else
				{
					double num = x.Metrics.MetricsNow.AveragePifWritesPerSecond - y.Metrics.MetricsNow.AveragePifWritesPerSecond;
					if (num < 0.0)
					{
						result = -1;
					}
					else
					{
						if (num > 0.0)
						{
							result = 1;
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}
		private DwmHostCollection GetPoweredOffHosts()
		{
			if (this._poweredOffHosts == null)
			{
				this._poweredOffHosts = DwmHostCollection.GetPoweredOffHosts(this._poolDetails.Id);
			}
			return this._poweredOffHosts;
		}
		private ICollectorActions GetCollector(int poolId)
		{
			if (this._collector == null)
			{
				this._collector = DwmPool.GetCollector(poolId);
			}
			return this._collector;
		}
	}
}
