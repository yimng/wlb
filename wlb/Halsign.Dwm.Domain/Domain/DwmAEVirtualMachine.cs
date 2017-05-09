using Halsign.DWM.Collectors;
using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmAEVirtualMachine
	{
		private static bool _traceEnabled;
		public static DwmHost GetOptimalHost(int vmId)
		{
			DwmErrorCode dwmErrorCode;
			DwmHostCollection allHosts = DwmAEVirtualMachine.GetAllHosts(vmId, 0, false, out dwmErrorCode);
			return DwmAEVirtualMachine.GetHighestScoringHosts(allHosts);
		}
		public static DwmHost GetOptimalHost(int vmId, int similarVmId)
		{
			DwmErrorCode dwmErrorCode;
			DwmHostCollection allHosts = DwmAEVirtualMachine.GetAllHosts(vmId, similarVmId, false, out dwmErrorCode);
			return DwmAEVirtualMachine.GetHighestScoringHosts(allHosts);
		}
		public static DwmHost GetOptimalHost(string vmUuid, string poolUuid)
		{
			DwmErrorCode dwmErrorCode;
			DwmHostCollection allHosts = DwmAEVirtualMachine.GetAllHosts(vmUuid, null, poolUuid, false, out dwmErrorCode);
			return DwmAEVirtualMachine.GetHighestScoringHosts(allHosts);
		}
		public static DwmHost GetOptimalHost(string vmUuid, string similarVmUuid, string poolUuid)
		{
			DwmErrorCode dwmErrorCode;
			DwmHostCollection allHosts = DwmAEVirtualMachine.GetAllHosts(vmUuid, similarVmUuid, poolUuid, false, out dwmErrorCode);
			return DwmAEVirtualMachine.GetHighestScoringHosts(allHosts);
		}
		public static DwmHost GetOptimalHostByName(string vmName, string poolName)
		{
			DwmErrorCode dwmErrorCode;
			DwmHostCollection allHostsByName = DwmAEVirtualMachine.GetAllHostsByName(vmName, null, poolName, false, out dwmErrorCode);
			return DwmAEVirtualMachine.GetHighestScoringHosts(allHostsByName);
		}
		public static DwmHost GetOptimalHostByName(string vmName, string similarVmName, string poolName)
		{
			DwmErrorCode dwmErrorCode;
			DwmHostCollection allHostsByName = DwmAEVirtualMachine.GetAllHostsByName(vmName, similarVmName, poolName, false, out dwmErrorCode);
			return DwmAEVirtualMachine.GetHighestScoringHosts(allHostsByName);
		}
		public static CantBootReason CanStartVM(string vmUuid, string hostUuid, string poolUuid)
		{
			CantBootReason result = CantBootReason.Unknown;
			ICollectorActions collector = DwmPool.GetCollector(poolUuid);
			if (collector != null)
			{
				result = collector.CanStartVM(vmUuid, hostUuid);
			}
			return result;
		}
		public static int StartVmOnHost(string vmUuid, string hostUuid, string poolUuid, bool async)
		{
			return DwmAEVirtualMachine.StartVmOnHost(vmUuid, hostUuid, poolUuid, async, 0);
		}
		public static int StartVmOnHost(string vmUuid, string hostUuid, string poolUuid, bool async, int recommendationId)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(hostUuid) && !string.IsNullOrEmpty(poolUuid))
			{
				DwmAsyncVmActions dwmAsyncVmActions = new DwmAsyncVmActions();
				dwmAsyncVmActions._poolId = DwmPoolBase.UuidToId(poolUuid);
				dwmAsyncVmActions._vmUuid = vmUuid;
				dwmAsyncVmActions._hostUuid = hostUuid;
				if (async)
				{
					new Thread(new ThreadStart(dwmAsyncVmActions.StartVmThreadProc))
					{
						Name = "StartVMOnHostAsynchThread"
					}.Start();
				}
				else
				{
					dwmAsyncVmActions.StartVmThreadProc();
					result = dwmAsyncVmActions._result;
				}
			}
			return result;
		}
		public static int MigrateVM(string vmUuid, string migrateToHostUuid, string migrateFromHostUuid, string poolUuid, bool async)
		{
			return DwmAEVirtualMachine.MigrateVM(vmUuid, migrateToHostUuid, migrateFromHostUuid, poolUuid, async, 0);
		}
		public static int MigrateVM(string vmUuid, string migrateToHostUuid, string migrateFromHostUuid, string poolUuid, bool async, int recommendationId)
		{
			List<MoveRecommendation> list = new List<MoveRecommendation>(1);
			MoveRecommendation moveRecommendation = new MoveRecommendation();
			int poolId = DwmPoolBase.UuidToId(poolUuid);
			moveRecommendation.PoolId = poolId;
			moveRecommendation.PoolUuid = poolUuid;
			moveRecommendation.MoveToHostId = DwmHost.UuidToId(migrateToHostUuid, poolId);
			moveRecommendation.MoveToHostUuid = migrateToHostUuid;
			moveRecommendation.MoveFromHostId = DwmHost.UuidToId(migrateFromHostUuid, poolId);
			moveRecommendation.MoveFromHostUuid = migrateFromHostUuid;
			moveRecommendation.VmId = DwmVirtualMachine.UuidToId(vmUuid, poolId);
			moveRecommendation.VmUuid = vmUuid;
			moveRecommendation.RecommendationId = recommendationId;
			list.Add(moveRecommendation);
			return DwmAEVirtualMachine.MigrateVMs(list, async);
		}
		public static int MigrateVMs(List<MoveRecommendation> migrations, bool async)
		{
			int result = 0;
			if (migrations != null && migrations.Count > 0)
			{
				DwmAsyncVmActions dwmAsyncVmActions = new DwmAsyncVmActions();
				dwmAsyncVmActions._migrations = migrations;
				if (async)
				{
					new Thread(new ThreadStart(dwmAsyncVmActions.MigrationVmThreadProc))
					{
						Name = "MigrateVMsAsynchThread"
					}.Start();
				}
				else
				{
					dwmAsyncVmActions.MigrationVmThreadProc();
					result = dwmAsyncVmActions._result;
				}
			}
			return result;
		}
		public static DwmHostCollection GetAllHosts(int vmId, out DwmErrorCode resultCode)
		{
			return DwmAEVirtualMachine.GetAllHosts(vmId, 0, true, out resultCode);
		}
		public static DwmHostCollection GetAllHosts(int vmId, int similarVmId, out DwmErrorCode resultCode)
		{
			return DwmAEVirtualMachine.GetAllHosts(vmId, similarVmId, true, out resultCode);
		}
		private static DwmHostCollection GetAllHosts(int vmId, int similarVmId, bool shouldAudit, out DwmErrorCode resultCode)
		{
			if (vmId > 0)
			{
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("vm_id", vmId));
				if (similarVmId > 0)
				{
					storedProcParamCollection.Add(new StoredProcParam("@similar_vm_id", similarVmId));
				}
				return DwmAEVirtualMachine.GetAllHosts("place_vm_by_id", storedProcParamCollection, shouldAudit, out resultCode);
			}
			throw new DwmException("The VM ID must be greater than zero.", DwmErrorCode.InvalidParameter, null);
		}
		public static DwmHostCollection GetAllHosts(string vmUuid, string poolUuid, out DwmErrorCode resultCode)
		{
			return DwmAEVirtualMachine.GetAllHosts(vmUuid, null, poolUuid, true, out resultCode);
		}
		public static DwmHostCollection GetAllHosts(string vmUuid, string similarVmUuid, string poolUuid, out DwmErrorCode resultCode)
		{
			return DwmAEVirtualMachine.GetAllHosts(vmUuid, similarVmUuid, poolUuid, true, out resultCode);
		}
		private static DwmHostCollection GetAllHosts(string vmUuid, string similarVmUuid, string poolUuid, bool shouldAudit, out DwmErrorCode resultCode)
		{
			int num = DwmPoolBase.UuidToId(poolUuid);
			int num2 = DwmVirtualMachine.UuidToId(vmUuid, num);
			int similarVmId = 0;
			if (!string.IsNullOrEmpty(similarVmUuid))
			{
				similarVmId = DwmVirtualMachine.UuidToId(similarVmUuid, num);
			}
			if (num <= 0)
			{
				throw new DwmException("The pool unique ID is invalid.", DwmErrorCode.InvalidParameter, null);
			}
			if (num2 > 0)
			{
				return DwmAEVirtualMachine.GetAllHosts(num2, similarVmId, shouldAudit, out resultCode);
			}
			throw new DwmException("The VM unique ID is invalid.", DwmErrorCode.InvalidParameter, null);
		}
		public static DwmHostCollection GetAllHostsByName(string vmName, string poolName, out DwmErrorCode resultCode)
		{
			return DwmAEVirtualMachine.GetAllHostsByName(vmName, null, poolName, true, out resultCode);
		}
		public static DwmHostCollection GetAllHostsByName(string vmName, string similarVmName, string poolName, out DwmErrorCode resultCode)
		{
			return DwmAEVirtualMachine.GetAllHostsByName(vmName, similarVmName, poolName, true, out resultCode);
		}
		private static DwmHostCollection GetAllHostsByName(string vmName, string similarVmName, string poolName, bool shouldAudit, out DwmErrorCode resultCode)
		{
			int num = DwmPoolBase.NameToId(poolName);
			int num2 = DwmVirtualMachine.NameToId(vmName, num);
			int similarVmId = 0;
			if (!string.IsNullOrEmpty(similarVmName))
			{
				similarVmId = DwmVirtualMachine.NameToId(similarVmName, num);
			}
			if (num <= 0)
			{
				throw new DwmException("The pool name is invalid.", DwmErrorCode.InvalidParameter, null);
			}
			if (num2 > 0)
			{
				return DwmAEVirtualMachine.GetAllHosts(num2, similarVmId, shouldAudit, out resultCode);
			}
			throw new DwmException("The VM is invalid.", DwmErrorCode.InvalidParameter, null);
		}
		private static DwmHostCollection GetAllHosts(string sqlStatement, StoredProcParamCollection parms, bool shouldAudit, out DwmErrorCode resultCode)
		{
			DwmHostCollection dwmHostCollection = null;
			DwmVirtualMachine dwmVirtualMachine = null;
			DwmPool dwmPool = null;
			DwmHost dwmHost = null;
			bool flag = false;
			DwmAEVirtualMachine._traceEnabled = Configuration.GetValueAsBool(ConfigItems.ScoreHostTrace);
			resultCode = DwmErrorCode.None;
			using (DBAccess dBAccess = new DBAccess())
			{
				try
				{
					dBAccess.UseTransaction = true;
					using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, parms))
					{
						dwmHostCollection = new DwmHostCollection();
						if (dataReader.Read())
						{
							int @int = DBAccess.GetInt(dataReader, "err");
							if (@int < 0)
							{
								Logger.Trace("Could not find any host to run VM.  Error = {0}", new object[]
								{
									@int
								});
								if (@int == -1 || @int == -2)
								{
									resultCode = DwmErrorCode.InvalidParameter;
									DwmHostCollection result = dwmHostCollection;
									return result;
								}
								if (@int == -6)
								{
									resultCode = DwmErrorCode.NoMetrics;
								}
								int int2 = DBAccess.GetInt(dataReader, "vm_id");
								string @string = DBAccess.GetString(dataReader, "vm_uuid");
								int int3 = DBAccess.GetInt(dataReader, "pool_id");
								bool @bool = DBAccess.GetBool(dataReader, "auto_balance_enabled");
								bool bool2 = DBAccess.GetBool(dataReader, "power_management_enabled");
								dwmPool = new DwmPool(int3);
								dwmVirtualMachine = new DwmVirtualMachine(@string, null, int3);
								dwmVirtualMachine.Id = int2;
							}
						}
						if (dataReader.NextResult() && dataReader.Read())
						{
							string string2 = DBAccess.GetString(dataReader, "uuid");
							string string3 = DBAccess.GetString(dataReader, "name");
							DwmHypervisorType int4 = (DwmHypervisorType)DBAccess.GetInt(dataReader, "hv_type");
							if (string.IsNullOrEmpty(string2) && string.IsNullOrEmpty(string3))
							{
								DwmHostCollection result = null;
								return result;
							}
							dwmPool = new DwmPool(string2, string3, int4);
							dwmPool.Id = DBAccess.GetInt(dataReader, "id");
							dwmPool.OptMode = (OptimizationMode)DBAccess.GetInt(dataReader, "opt_mode");
							dwmPool.MaxCpuRating = DBAccess.GetInt(dataReader, "max_cpu_rating");
							dwmPool.IsLicensed = DBAccess.GetBool(dataReader, "is_licensed");
							if (!dwmPool.IsLicensed)
							{
								resultCode = DwmErrorCode.NotLicensed;
							}
						}
						if (dataReader.NextResult() && dataReader.Read())
						{
							dwmPool.LoadThresholdsAndWeights(dataReader);
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								DwmHost item = DwmHost.LoadWithMetrics(dataReader);
								dwmHostCollection.Add(item);
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int int5 = DBAccess.GetInt(dataReader, 0);
								int int6 = DBAccess.GetInt(dataReader, 1);
								DwmHost host = dwmHostCollection.GetHost(int5);
								if (host != null)
								{
									host.Metrics.NumVMs = int6;
								}
							}
						}
						if (dataReader.NextResult())
						{
							DwmHost dwmHost2 = null;
							while (dataReader.Read())
							{
								int int7 = DBAccess.GetInt(dataReader, "host_id");
								if (dwmHost2 == null || dwmHost2.Id != int7)
								{
									dwmHost2 = dwmHostCollection.GetHost(int7);
								}
								if (dwmHost2 != null)
								{
									int int8 = DBAccess.GetInt(dataReader, "sr_id");
									dwmHost2.AvailableStorage.Add(new DwmStorageRepository(int8));
								}
							}
						}
						if (dataReader.NextResult())
						{
							DwmHost dwmHost3 = null;
							while (dataReader.Read())
							{
								int int9 = DBAccess.GetInt(dataReader, "host_id");
								if (dwmHost3 == null || dwmHost3.Id != int9)
								{
									dwmHost3 = dwmHostCollection.GetHost(int9);
								}
								if (dwmHost3 != null)
								{
									dwmHost3.LoadPif(dataReader);
								}
							}
						}
						if (dataReader.NextResult() && dataReader.Read())
						{
							dwmVirtualMachine = DwmVirtualMachine.LoadWithMetrics(dataReader);
							DwmAEVirtualMachine.WeightVmMetrics(dwmVirtualMachine.Metrics, dwmPool);
							dwmVirtualMachine.Metrics.Score = DwmAEVirtualMachine.ScoreVM(dwmVirtualMachine, dwmPool);
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int int10 = DBAccess.GetInt(dataReader, "sr_id");
								if (int10 > 0)
								{
									dwmVirtualMachine.RequiredStorage.Add(new DwmStorageRepository(int10));
								}
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int int11 = DBAccess.GetInt(dataReader, "vif_id");
								string string4 = DBAccess.GetString(dataReader, "uuid");
								int int12 = DBAccess.GetInt(dataReader, "poolid");
								int int13 = DBAccess.GetInt(dataReader, "networkid");
								if (int11 > 0)
								{
									dwmVirtualMachine.NetworkInterfaces.Add(new DwmVif(string4, int13, int12));
								}
							}
						}
					}
				}
				catch
				{
					throw;
				}
				if (resultCode == DwmErrorCode.None)
				{
					if (!DwmAEVirtualMachine.EvaluateHostCpuCount(dwmHostCollection, dwmVirtualMachine))
					{
						DwmHostCollection result = dwmHostCollection;
						return result;
					}
					MemoryScoringType memScoringType = MemoryScoringType.StaticMax;
					bool flag2;
					bool flag3;
					bool flag4;
					if (!DwmAEVirtualMachine.EvaluateHostMemory(dwmHostCollection, dwmVirtualMachine, dwmPool, out flag2, out flag3, out flag4))
					{
						if (!flag4)
						{
							for (int i = 0; i < dwmHostCollection.Count; i++)
							{
								dwmHostCollection[i].Metrics.ZeroScoreReasons.Add(ZeroScoreReason.Memory);
							}
							if (dwmPool.PowerOnHostIfNoMemory && DwmAEVirtualMachine.TryToPowerOnHost(dwmVirtualMachine, dwmPool, out dwmHost))
							{
								flag = true;
							}
						}
						else
						{
							if (dwmPool.PreferPowerOnOverCompression)
							{
								if (DwmAEVirtualMachine.TryToPowerOnHost(dwmVirtualMachine, dwmPool, out dwmHost))
								{
									flag = true;
								}
								else
								{
									memScoringType = MemoryScoringType.PotentialFree;
								}
							}
							else
							{
								memScoringType = MemoryScoringType.PotentialFree;
							}
						}
					}
					else
					{
						memScoringType = ((!flag2) ? MemoryScoringType.DynamicMax : MemoryScoringType.StaticMax);
					}
					if (!flag)
					{
						bool flag5 = false;
						for (int j = 0; j < dwmHostCollection.Count; j++)
						{
							if (dwmHostCollection[j].Enabled && dwmHostCollection[j].HasRequiredStorage(dwmVirtualMachine.RequiredStorage))
							{
								flag5 = true;
								break;
							}
						}
						if (!flag5)
						{
							for (int k = 0; k < dwmHostCollection.Count; k++)
							{
								dwmHostCollection[k].Metrics.ZeroScoreReasons.Add(ZeroScoreReason.VmRequiresSr);
							}
							if (dwmPool.PowerOnHostIfNoSR && DwmAEVirtualMachine.TryToPowerOnHost(dwmVirtualMachine, dwmPool, out dwmHost))
							{
								flag = true;
							}
						}
					}
					if (!flag && dwmHostCollection != null && dwmHostCollection.Count > 0)
					{
						DwmAEHostScorer dwmAEHostScorer = new DwmAEHostScorer(dwmVirtualMachine, dwmPool, memScoringType);
						for (int l = dwmHostCollection.Count - 1; l >= 0; l--)
						{
							if (dwmHostCollection[l].PowerState == PowerStatus.Off)
							{
								dwmHostCollection[l].Metrics.Score = 0;
								dwmHostCollection[l].Metrics.Stars = 0.0;
								dwmHostCollection[l].Metrics.ZeroScoreReasons.Add(ZeroScoreReason.HostOffline);
							}
							if (dwmHostCollection[l].ExcludeFromPlacementRecommendations)
							{
								dwmHostCollection[l].Metrics.Score = 0;
								dwmHostCollection[l].Metrics.Stars = 0.0;
								dwmHostCollection[l].Metrics.ZeroScoreReasons.Add(ZeroScoreReason.HostExcluded);
							}
							if (dwmHostCollection[l].Metrics.ZeroScoreReasons.Count == 0)
							{
								dwmHostCollection[l].Metrics.Score = dwmAEHostScorer.ComputeScore(dwmHostCollection[l]);
								dwmHostCollection[l].Metrics.Stars = DwmAEVirtualMachine.ComputeStars(dwmHostCollection[l]);
							}
						}
					}
				}
				if (flag && dwmHost != null)
				{
					DwmHost host2 = dwmHostCollection.GetHost(dwmHost.Id);
					if (host2 == null)
					{
						dwmHostCollection.Add(dwmHost);
						host2 = dwmHostCollection.GetHost(dwmHost.Id);
					}
					host2.Metrics.ZeroScoreReasons.Clear();
					host2.Metrics.Score = 100;
					host2.Metrics.MaxPossibleScore = 100;
					host2.Metrics.Stars = DwmAEVirtualMachine.ComputeStars(host2);
				}
				DwmAEVirtualMachine.EnsurePositiveScores(dwmHostCollection);
			}
			if (shouldAudit && dwmVirtualMachine != null)
			{
				DwmAEVirtualMachine.AuditGetAllHost(dwmHostCollection, dwmVirtualMachine.Id);
			}
			return dwmHostCollection;
		}
		internal static bool EvaluateHostCpuCount(DwmHostCollection hosts, DwmVirtualMachine vm)
		{
			bool result = false;
			foreach (DwmHost current in hosts)
			{
				if (current.NumCpus >= vm.MinimumCpus)
				{
					result = true;
					current.Flags |= 22;
				}
				else
				{
					current.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.NotEnoughCpus);
					current.Metrics.Score = 0;
					current.Metrics.Stars = 0.0;
					DwmAEVirtualMachine.Trace("Host does not have enough CPUs for the VM", current.Name, vm.Id);
				}
			}
			return result;
		}
		internal static bool EvaluateHostMemory(DwmHostCollection hosts, DwmVirtualMachine vm, DwmPool pool, out bool haveStaticMax, out bool haveDynamicMax, out bool havePotential)
		{
			bool result = false;
			haveStaticMax = false;
			haveDynamicMax = false;
			havePotential = false;
			if (hosts != null && hosts.Count > 0)
			{
				double num = (double)(vm.MaximumStaticMemory + vm.MemoryOverhead);
				double num2 = (double)(vm.MaximumDynamicMemory + vm.MemoryOverhead);
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				for (int i = 0; i < hosts.Count; i++)
				{
					if (hosts[i].Enabled)
					{
						if ((double)hosts[i].Metrics.FreeMemory >= num)
						{
							num3++;
							hosts[i].Flags |= 8;
							hosts[i].Flags |= 16;
							hosts[i].Flags |= 32;
						}
						else
						{
							if ((double)hosts[i].Metrics.FreeMemory >= num2)
							{
								num4++;
								hosts[i].Flags |= 8;
								hosts[i].Flags |= 16;
								hosts[i].Flags |= 32;
							}
							else
							{
								if (DwmAEVirtualMachine.HostHasPotentialMemory(hosts[i], vm, pool))
								{
									num5++;
									hosts[i].Flags |= 32;
								}
							}
						}
					}
				}
				if (num3 > 0 && num4 == 0 && num5 == 0)
				{
					result = true;
					haveStaticMax = true;
					haveDynamicMax = true;
					havePotential = true;
				}
				else
				{
					if (num4 > 0 && num5 == 0)
					{
						result = true;
						haveDynamicMax = true;
						havePotential = true;
					}
					else
					{
						if (num5 > 0)
						{
							havePotential = true;
						}
					}
				}
			}
			return result;
		}
		private static bool HostHasPotentialMemory(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			long num = ((vm.MinimumDynamicMemory == vm.MaximumStaticMemory) ? vm.MaximumStaticMemory : vm.MinimumDynamicMemory) + vm.MemoryOverhead;
			return host.Metrics.PotentialFreeMemory >= num;
		}
		private static bool TryToPowerOnHost(DwmVirtualMachine vm, DwmPool pool, out DwmHost host)
		{
			bool result = false;
			host = null;
			if (pool.ManagePower)
			{
				vm.Load();
				pool.Load();
				bool flag = (pool.OptMode != OptimizationMode.MaxDensity) ? pool.OverCommitCpusInPerfMode : pool.OverCommitCpusInDensityMode;
				DwmHostCollection dwmHostCollection = DwmAEHost.SelectHostsToPowerOn(pool, new DwmVirtualMachineCollection
				{
					vm
				}, (!flag) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByCpuRating) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByTotalMemory));
				if (dwmHostCollection != null && dwmHostCollection.Count > 0 && DwmAEHost.SetPowerState(dwmHostCollection[0].Uuid, pool.Uuid, PowerStatus.On, 0) == 0)
				{
					host = dwmHostCollection[0];
					result = true;
				}
			}
			return result;
		}
		private static void AuditGetAllHost(DwmHostCollection hosts, int vmId)
		{
			if (hosts != null)
			{
				int recommendationSetId = 0;
				for (int i = 0; i < hosts.Count; i++)
				{
					DwmHost dwmHost = hosts[i];
					dwmHost.Metrics.RecommendationId = DwmAudit.AddRecommendationRecord(ref recommendationSetId, MoveRecommendationType.VmPlacementRecommendation, vmId, 0, hosts[i].Id);
					hosts[i].Metrics.RecommendationSetId = recommendationSetId;
				}
			}
		}
		internal static void EnsurePositiveScores(DwmHostCollection hosts)
		{
			int num = 0;
			for (int i = 0; i < hosts.Count; i++)
			{
				if (hosts[i].Metrics.Score < 0)
				{
					int num2 = Math.Abs(hosts[i].Metrics.Score);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			if (num > 0)
			{
				for (int j = 0; j < hosts.Count; j++)
				{
					DwmHostAverageMetric metrics = hosts[j].Metrics;
					if (metrics.CanBootVM && metrics.ZeroScoreReasons.Count == 0)
					{
						metrics.Score += num;
						metrics.MaxPossibleScore += num;
						metrics.AddMessage("Adding {0} to score to keep all scores positive", new object[]
						{
							num
						});
						metrics.Stars = DwmAEVirtualMachine.ComputeStars(hosts[j]);
					}
					else
					{
						metrics.Stars = 0.0;
						metrics.Score = 0;
					}
				}
			}
		}
		internal static DwmHost GetHighestScoringHosts(DwmHostCollection hosts)
		{
			DwmHost result = null;
			int num = -1;
			if (hosts != null)
			{
				for (int i = 0; i < hosts.Count; i++)
				{
					if (hosts[i].Metrics.Score > num)
					{
						num = hosts[i].Metrics.Score;
						result = hosts[i];
					}
				}
			}
			return result;
		}
		internal static void WeightVmMetrics(DwmVmAverageMetric vm, DwmPool pool)
		{
			if (!vm.HaveCurrentMetrics)
			{
				if (vm.HaveLast30Metrics)
				{
					vm.MetricsNow = vm.MetricsLast30Minutes;
					vm.HaveCurrentMetrics = true;
				}
			}
			else
			{
				if (!vm.HaveLast30Metrics)
				{
					vm.HaveLast30Metrics = vm.HaveCurrentMetrics;
					vm.HaveYesterdayMetrics = vm.HaveCurrentMetrics;
				}
			}
			if (vm.HaveCurrentMetrics)
			{
				vm.VmCpuUtil = vm.MetricsNow.AverageCpuUtilization * pool.WeightCurrentMetrics + vm.MetricsLast30Minutes.AverageCpuUtilization * pool.WeightRecentMetrics + vm.MetricsYesterday.AverageCpuUtilization * pool.WeightHistoricMetrics;
				vm.VmMemory = (double)vm.MetricsNow.AverageUsedMemory * pool.WeightCurrentMetrics + (double)vm.MetricsLast30Minutes.AverageUsedMemory * pool.WeightRecentMetrics + (double)vm.MetricsYesterday.AverageUsedMemory * pool.WeightHistoricMetrics;
				vm.VmVifRead = (vm.MetricsNow.AveragePifReadsPerSecond + vm.MetricsNow.TotalVbdNetReadsPerSecond) * pool.WeightCurrentMetrics + (vm.MetricsLast30Minutes.AveragePifReadsPerSecond + vm.MetricsLast30Minutes.TotalVbdNetReadsPerSecond) * pool.WeightRecentMetrics + (vm.MetricsYesterday.AveragePifReadsPerSecond + vm.MetricsYesterday.TotalVbdNetReadsPerSecond) * pool.WeightHistoricMetrics;
				vm.VmVifWrite = (vm.MetricsNow.AveragePifWritesPerSecond + vm.MetricsNow.TotalVbdNetWritesPerSecond) * pool.WeightCurrentMetrics + (vm.MetricsLast30Minutes.AveragePifWritesPerSecond + vm.MetricsLast30Minutes.TotalVbdNetWritesPerSecond) * pool.WeightRecentMetrics + (vm.MetricsYesterday.AveragePifWritesPerSecond + vm.MetricsYesterday.TotalVbdNetWritesPerSecond) * pool.WeightHistoricMetrics;
				vm.VmVbdRead = vm.MetricsNow.AveragePbdReadsPerSecond * pool.WeightCurrentMetrics + vm.MetricsLast30Minutes.AveragePbdReadsPerSecond * pool.WeightRecentMetrics + vm.MetricsYesterday.AveragePbdReadsPerSecond * pool.WeightHistoricMetrics;
				vm.VmVbdWrite = vm.MetricsNow.AveragePbdWritesPerSecond * pool.WeightCurrentMetrics + vm.MetricsLast30Minutes.AveragePbdWritesPerSecond * pool.WeightRecentMetrics + vm.MetricsYesterday.AveragePbdWritesPerSecond * pool.WeightHistoricMetrics;
				vm.CpuWeight = DwmAEVirtualMachine.GetWeight(vm.VmCpuUtil, pool.VmCpuUtilizationThreshold, pool.VmCpuUtilizationWeight);
				vm.MemoryWeight = DwmAEVirtualMachine.GetWeight(vm.VmMemory, pool.VmMemoryThreshold, pool.VmMemoryWeight);
				vm.PifReadWeight = DwmAEVirtualMachine.GetWeight(vm.VmVifRead + vm.VmVbdRead, pool.VmNetworkReadThreshold, pool.VmNetworkReadWeight);
				vm.PifWriteWeight = DwmAEVirtualMachine.GetWeight(vm.VmVifWrite + vm.VmVbdWrite, pool.VmNetworkWriteThreshold, pool.VmNetworkWriteWeight);
				vm.PbdReadWeight = DwmAEVirtualMachine.GetWeight(vm.VmVbdRead, pool.VmDiskReadThreshold, pool.VmDiskReadWeight);
				vm.PbdWriteWeight = DwmAEVirtualMachine.GetWeight(vm.VmVbdWrite, pool.VmDiskWriteThreshold, pool.VmDiskWriteWeight);
				vm.RunstateWeight = DwmAEVirtualMachine.GetWeight(vm.VmCpuUtil, pool.VmCpuUtilizationThreshold, pool.VmRunstateWeight);
				vm.PowerManagementWeight = ((!pool.AutoBalance || !pool.ManagePower) ? ((!pool.AutoBalance && !pool.ManagePower) ? pool.VmPowerManagementWeight.Low : pool.VmPowerManagementWeight.Medium) : pool.VmPowerManagementWeight.High);
			}
			else
			{
				vm.CpuWeight = pool.VmCpuUtilizationWeight.High;
				vm.MemoryWeight = pool.VmMemoryWeight.High;
				vm.PifReadWeight = pool.VmNetworkReadWeight.High;
				vm.PifWriteWeight = pool.VmNetworkWriteWeight.High;
				vm.PbdReadWeight = pool.VmDiskReadWeight.High;
				vm.PbdWriteWeight = pool.VmDiskWriteWeight.High;
				vm.RunstateWeight = pool.VmRunstateWeight.High;
				vm.PowerManagementWeight = ((!pool.AutoBalance || !pool.ManagePower) ? ((!pool.AutoBalance && !pool.ManagePower) ? pool.VmPowerManagementWeight.Low : pool.VmPowerManagementWeight.Medium) : pool.VmPowerManagementWeight.High);
			}
		}
		internal static int ScoreVM(DwmVirtualMachine vm, DwmPool pool)
		{
			DwmVmAverageMetric.VerifyValidInstance(vm.Metrics, pool);
			double num = vm.Metrics.VmCpuUtil * 100.0 * vm.Metrics.CpuWeight;
			double num2 = 0.0;
			if (vm.Metrics.VmMemory >= pool.VmMemoryThreshold.High)
			{
				num2 = 100.0 * vm.Metrics.MemoryWeight;
			}
			else
			{
				if (vm.Metrics.VmMemory >= pool.VmMemoryThreshold.Medium)
				{
					num2 = 60.0 * vm.Metrics.MemoryWeight;
				}
				else
				{
					if (vm.Metrics.VmMemory >= pool.VmMemoryThreshold.Low)
					{
						num2 = 30.0 * vm.Metrics.MemoryWeight;
					}
				}
			}
			double num3 = 0.0;
			if (vm.Metrics.VmVifRead >= pool.VmNetworkReadThreshold.High)
			{
				num3 = 100.0 * vm.Metrics.PifReadWeight;
			}
			else
			{
				if (vm.Metrics.VmVifRead >= pool.VmNetworkReadThreshold.Medium)
				{
					num3 = 60.0 * vm.Metrics.PifReadWeight;
				}
				else
				{
					if (vm.Metrics.VmVifRead >= pool.VmNetworkReadThreshold.Low)
					{
						num3 = 30.0 * vm.Metrics.PifReadWeight;
					}
				}
			}
			double num4 = 0.0;
			if (vm.Metrics.VmVifWrite >= pool.VmNetworkWriteThreshold.High)
			{
				num4 = 100.0 * vm.Metrics.PifWriteWeight;
			}
			else
			{
				if (vm.Metrics.VmVifWrite >= pool.VmNetworkWriteThreshold.Medium)
				{
					num4 = 60.0 * vm.Metrics.PifWriteWeight;
				}
				else
				{
					if (vm.Metrics.VmVifWrite >= pool.VmNetworkWriteThreshold.Low)
					{
						num4 = 30.0 * vm.Metrics.PifWriteWeight;
					}
				}
			}
			double num5 = 0.0;
			if (vm.Metrics.VmVbdRead >= pool.VmDiskReadThreshold.High)
			{
				num5 = 100.0 * vm.Metrics.PbdReadWeight;
			}
			else
			{
				if (vm.Metrics.VmVbdRead >= pool.VmDiskReadThreshold.Medium)
				{
					num5 = 60.0 * vm.Metrics.PbdReadWeight;
				}
				else
				{
					if (vm.Metrics.VmVbdRead >= pool.VmDiskReadThreshold.Low)
					{
						num5 = 30.0 * vm.Metrics.PbdReadWeight;
					}
				}
			}
			double num6 = 0.0;
			if (vm.Metrics.VmVbdWrite >= pool.VmDiskWriteThreshold.High)
			{
				num6 = 100.0 * vm.Metrics.PbdWriteWeight;
			}
			else
			{
				if (vm.Metrics.VmVbdWrite >= pool.VmDiskWriteThreshold.Medium)
				{
					num6 = 60.0 * vm.Metrics.PbdWriteWeight;
				}
				else
				{
					if (vm.Metrics.VmVbdWrite >= pool.VmDiskWriteThreshold.Low)
					{
						num6 = 30.0 * vm.Metrics.PbdWriteWeight;
					}
				}
			}
			return (int)(num + num2 + num3 + num4 + num5 + num6 + (double)(vm.MinimumCpus * 15));
		}
		internal static double ComputeStars(DwmHost host)
		{
			double num = 0.0;
			if (host.Metrics.MaxPossibleScore > 0)
			{
				num = (double)host.Metrics.Score / (double)host.Metrics.MaxPossibleScore * 5.0;
			}
			string text = string.Format("Awarding {0} stars to the host", num);
			DwmAEVirtualMachine.Trace(text, host.Name, 0);
			host.Metrics.AddMessage(text);
			return num;
		}
		internal static int CompareVmMemory(DwmVirtualMachine x, DwmVirtualMachine y)
		{
			int result = 0;
			long num = y.MaximumDynamicMemory - x.MaximumDynamicMemory;
			if (num > 0L)
			{
				result = 1;
			}
			else
			{
				if (num < 0L)
				{
					result = -1;
				}
			}
			return result;
		}
		private static double GetWeight(double value, Threshold valueRange, Threshold weightRange)
		{
			double result;
			if (value >= valueRange.High)
			{
				result = weightRange.High;
			}
			else
			{
				if (value >= valueRange.Medium)
				{
					result = weightRange.Medium;
				}
				else
				{
					result = weightRange.Low;
				}
			}
			return result;
		}
		private static void Trace(string msg, string hostName, int vmId)
		{
			if (DwmAEVirtualMachine._traceEnabled)
			{
				Logger.Trace("Scoring host {0} for VM {1} - {2}", new object[]
				{
					hostName,
					vmId,
					msg
				});
			}
		}
	}
}
