using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmAEHost
	{
		public static DwmAEHostRelocateRecommendation PlaceVMs(string hostUuid, string poolUuid)
		{
			int poolId = DwmPoolBase.UuidToId(poolUuid);
			int hostId = DwmHost.UuidToId(hostUuid, poolId);
			return DwmAEHost.PlaceVMs(hostId);
		}
		public static DwmAEHostRelocateRecommendation PlaceVMsByName(string hostName, string poolName)
		{
			int poolId = DwmPoolBase.NameToId(poolName);
			int hostId = DwmHost.NameToId(hostName, poolId);
			return DwmAEHost.PlaceVMs(hostId);
		}
		public static DwmAEHostRelocateRecommendation PlaceVMs(int hostId)
		{
			string sqlStatement = "place_host_by_id";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			DwmHostCollection dwmHostCollection = null;
			DwmHostCollection dwmHostCollection2 = null;
			DwmVirtualMachineCollection dwmVirtualMachineCollection = null;
			List<DwmHostVM> list = null;
			DwmAEHostRelocateRecommendation dwmAEHostRelocateRecommendation = new DwmAEHostRelocateRecommendation();
			storedProcParamCollection.Add(new StoredProcParam("host_id", hostId));
			DwmPool pool;
			DwmHost dwmHost;
			dwmAEHostRelocateRecommendation.ResultCode = DwmAEHost.PlaceVMsQuery(sqlStatement, storedProcParamCollection, out pool, out dwmHost, out dwmHostCollection, out dwmHostCollection2, out dwmVirtualMachineCollection);
			if (dwmHostCollection2 != null)
			{
				DwmAEHostEvacuator dwmAEHostEvacuator = new DwmAEHostEvacuator(dwmVirtualMachineCollection, dwmHostCollection2, pool);
				dwmAEHostRelocateRecommendation.CanPlaceAllVMs = dwmAEHostEvacuator.Evacuate(out list);
			}
			else
			{
				if (dwmVirtualMachineCollection != null && dwmVirtualMachineCollection.Count > 0)
				{
					dwmAEHostRelocateRecommendation.CanPlaceAllVMs = false;
				}
			}
			if (list != null && list.Count > 0)
			{
				dwmAEHostRelocateRecommendation.MoveRecs = new List<MoveRecommendation>(list.Count);
				int recommendationSetId = 0;
				for (int i = 0; i < list.Count; i++)
				{
					MoveRecommendation moveRecommendation = new MoveRecommendation();
					moveRecommendation.MoveFromHostId = dwmHost.Id;
					moveRecommendation.MoveFromHostName = dwmHost.Name;
					moveRecommendation.MoveFromHostUuid = dwmHost.Uuid;
					moveRecommendation.MoveToHostId = list[i].HostId;
					moveRecommendation.MoveToHostName = list[i].HostName;
					moveRecommendation.MoveToHostUuid = list[i].HostUuid;
					moveRecommendation.VmId = list[i].VmId;
					moveRecommendation.VmName = list[i].VmName;
					moveRecommendation.VmUuid = list[i].VmUuid;
					if (moveRecommendation.VmId > 0)
					{
						moveRecommendation.RecommendationId = DwmAudit.AddRecommendationRecord(ref recommendationSetId, MoveRecommendationType.HostPlacementRecommendation, moveRecommendation.VmId, moveRecommendation.MoveFromHostId, moveRecommendation.MoveToHostId);
					}
					else
					{
						moveRecommendation.RecommendationId = DwmAudit.AddRecommendationRecord(ref recommendationSetId, MoveRecommendationType.HostPlacementRecommendation, new int?(0), new int?(moveRecommendation.MoveFromHostId), new int?(0), OptimizationSeverity.None, ResourceToOptimize.PowerOn, DateTime.UtcNow, null, null);
					}
					dwmAEHostRelocateRecommendation.MoveRecs.Add(moveRecommendation);
				}
				dwmAEHostRelocateRecommendation.RecommendationSetId = recommendationSetId;
			}
			return dwmAEHostRelocateRecommendation;
		}
		private static DwmErrorCode PlaceVMsQuery(string sqlStatement, StoredProcParamCollection parms, out DwmPool pool, out DwmHost moveFromHost, out DwmHostCollection hostsSupportAllVms, out DwmHostCollection hosts, out DwmVirtualMachineCollection vms)
		{
			pool = null;
			moveFromHost = null;
			hostsSupportAllVms = null;
			hosts = null;
			vms = null;
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, parms))
				{
					if (dataReader != null)
					{
						if (dataReader.Read())
						{
							int @int = DBAccess.GetInt(dataReader, "_err");
							if (@int != 0)
							{
								Logger.Trace("Query to place all VMs on a host {0} returns {1}", new object[]
								{
									sqlStatement,
									@int
								});
								DwmErrorCode result = DwmErrorCode.InvalidParameter;
								if (@int == -3)
								{
									result = DwmErrorCode.NoMetrics;
								}
								else
								{
									if (@int == -4)
									{
										result = DwmErrorCode.NotLicensed;
									}
									else
									{
										if (@int == -5)
										{
											result = DwmErrorCode.HostExcluded;
										}
									}
								}
								return result;
							}
							pool = new DwmPool(DBAccess.GetString(dataReader, 1), DBAccess.GetString(dataReader, 2), (DwmHypervisorType)DBAccess.GetInt(dataReader, 3));
							pool.Id = DBAccess.GetInt(dataReader, 0);
							pool.OptMode = (OptimizationMode)DBAccess.GetInt(dataReader, 4);
							pool.MaxCpuRating = DBAccess.GetInt(dataReader, 5);
						}
						if (dataReader.NextResult() && dataReader.Read())
						{
							pool.LoadThresholdsAndWeights(dataReader);
						}
						if (dataReader.NextResult() && dataReader.Read())
						{
							moveFromHost = new DwmHost(DBAccess.GetString(dataReader, "uuid", null), DBAccess.GetString(dataReader, "name", null), DBAccess.GetInt(dataReader, "poolid", 0));
							moveFromHost.Id = DBAccess.GetInt(dataReader, "id", 0);
						}
						if (dataReader.NextResult())
						{
							hosts = new DwmHostCollection();
							DwmAEHostScorer dwmAEHostScorer = new DwmAEHostScorer(null, pool, MemoryScoringType.DynamicMax);
							while (dataReader.Read())
							{
								DwmHost dwmHost = DwmHost.LoadWithMetrics(dataReader);
								dwmHost.Metrics.Score = dwmAEHostScorer.ComputeScore(dwmHost);
								if (!dwmHost.ExcludeFromEvacuationRecommendations)
								{
									hosts.Add(dwmHost);
								}
							}
						}
						if (dataReader.NextResult() && hosts != null)
						{
							while (dataReader.Read())
							{
								int int2 = DBAccess.GetInt(dataReader, "host_id");
								int int3 = DBAccess.GetInt(dataReader, "sr_id");
								DwmHost host = hosts.GetHost(int2);
								if (host != null)
								{
									DwmStorageRepository dwmStorageRepository = new DwmStorageRepository(int3);
									dwmStorageRepository.PoolId = pool.Id;
									host.AvailableStorage.Add(dwmStorageRepository);
								}
							}
						}
						if (dataReader.NextResult() && hosts != null)
						{
							DwmHost dwmHost2 = null;
							while (dataReader.Read())
							{
								int int4 = DBAccess.GetInt(dataReader, "hostid");
								if (dwmHost2 == null || dwmHost2.Id != int4)
								{
									dwmHost2 = hosts.GetHost(int4);
								}
								if (dwmHost2 != null)
								{
									dwmHost2.LoadPif(dataReader);
								}
							}
						}
						if (dataReader.NextResult())
						{
							vms = new DwmVirtualMachineCollection();
							while (dataReader.Read())
							{
								DwmVirtualMachine dwmVirtualMachine = DwmVirtualMachine.LoadWithMetrics(dataReader);
								DwmAEVirtualMachine.WeightVmMetrics(dwmVirtualMachine.Metrics, pool);
								dwmVirtualMachine.Metrics.Score = DwmAEVirtualMachine.ScoreVM(dwmVirtualMachine, pool);
								vms.Add(dwmVirtualMachine);
							}
						}
						if (dataReader.NextResult() && vms != null)
						{
							while (dataReader.Read())
							{
								int int5 = DBAccess.GetInt(dataReader, "vm_id");
								int int6 = DBAccess.GetInt(dataReader, "sr_id");
								DwmVirtualMachine vM = vms.GetVM(int5);
								if (vM != null)
								{
									vM.RequiredStorage.Add(new DwmStorageRepository(int6));
								}
							}
						}
						if (dataReader.NextResult())
						{
							DwmVirtualMachine dwmVirtualMachine2 = null;
							while (dataReader.Read())
							{
								int int7 = DBAccess.GetInt(dataReader, "vm_id");
								if (dwmVirtualMachine2 == null || dwmVirtualMachine2.Id != int7)
								{
									dwmVirtualMachine2 = vms.GetVM(int7);
								}
								if (dwmVirtualMachine2 != null && dwmVirtualMachine2.Id == int7)
								{
									int int8 = DBAccess.GetInt(dataReader, "vif_id");
									string @string = DBAccess.GetString(dataReader, "uuid");
									int int9 = DBAccess.GetInt(dataReader, "networkid");
									int int10 = DBAccess.GetInt(dataReader, "poolid");
									DwmVif dwmVif = new DwmVif(@string, int9, int10);
									dwmVif.Id = int8;
									dwmVirtualMachine2.NetworkInterfaces.Add(dwmVif);
								}
							}
						}
					}
				}
			}
			return DwmErrorCode.None;
		}
		internal static bool PoolMasterExceedsLimits(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			return DwmAEHost.PoolMasterExceedsCpuLimit(host, vm, pool) | DwmAEHost.PoolMasterExceedsNetIoLimit(host, vm, pool);
		}
		internal static bool PoolMasterExceedsCpuLimit(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			bool result = false;
			if (host.IsPoolMaster && pool.PoolMasterCpuLimit > 0.0)
			{
				double num = vm.Metrics.MetricsNow.AverageCpuUtilization * ((double)vm.MinimumCpus / (double)host.NumCpus);
				if (host.Metrics.MetricsNow.AverageCpuUtilization + num > pool.PoolMasterCpuLimit)
				{
					result = true;
				}
			}
			return result;
		}
		internal static bool PoolMasterExceedsNetIoLimit(DwmHost host, DwmVirtualMachine vm, DwmPool pool)
		{
			bool result = false;
			if (host.IsPoolMaster && pool.PoolMasterNetIoLimit > 0.0)
			{
				for (int i = 0; i < vm.NetworkInterfaces.Count; i++)
				{
					int networkId = vm.NetworkInterfaces[i].NetworkId;
					for (int j = 0; j < host.PIFs.Count; j++)
					{
						if (host.PIFs[j].NetworkId == networkId && 
                            host.PIFs[j].IsManagementInterface && 
                            vm.Metrics.MetricsNow.AveragePifReadsPerSecond + 
                            vm.Metrics.MetricsNow.AveragePifWritesPerSecond/** + 
                            vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond + 
                            vm.Metrics.MetricsNow.TotalVbdNetWritesPerSecond**/ > pool.PoolMasterNetIoLimit)
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}
		public static int SetPowerState(string hostUuid, string poolUuid, PowerStatus powerState, int recommendationId)
		{
			return DwmAEHost.SetPowerState(hostUuid, poolUuid, powerState, recommendationId, false);
		}
		public static int SetPowerState(string hostUuid, string poolUuid, PowerStatus powerState, int recommendationId, bool async)
		{
			int result = 1;
			DwmAsyncVmActions dwmAsyncVmActions = new DwmAsyncVmActions();
			dwmAsyncVmActions._hostUuid = hostUuid;
			dwmAsyncVmActions._poolId = DwmPoolBase.UuidToId(poolUuid);
			dwmAsyncVmActions._recommendationId = recommendationId;
			if (async)
			{
				Thread thread = null;
				if (powerState == PowerStatus.On)
				{
					thread = new Thread(new ThreadStart(dwmAsyncVmActions.HostPowerOnThreadProc));
					thread.Name = "HostPowerOnAsynchThread";
				}
				else
				{
					if (powerState == PowerStatus.Off)
					{
						thread = new Thread(new ThreadStart(dwmAsyncVmActions.HostPowerOffThreadProc));
						thread.Name = "HostPowerOffAsynchThread";
					}
				}
				if (thread != null)
				{
					thread.Start();
					result = 0;
				}
			}
			else
			{
				if (powerState == PowerStatus.On)
				{
					dwmAsyncVmActions.HostPowerOnThreadProc();
					result = dwmAsyncVmActions._result;
				}
				else
				{
					if (powerState == PowerStatus.Off)
					{
						dwmAsyncVmActions.HostPowerOffThreadProc();
						result = dwmAsyncVmActions._result;
					}
				}
			}
			return result;
		}
		internal static int CompareHostMetrics(DwmHost x, DwmHost y)
		{
			int result;
			if (x == y)
			{
				result = 0;
			}
			else
			{
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
						result = y.Metrics.Score - x.Metrics.Score;
					}
				}
			}
			return result;
		}
		private static void Trace(string msg, string hostId)
		{
			if (Configuration.GetValueAsBool(ConfigItems.ScoreHostTrace))
			{
				Logger.Trace("Relocate VM for host {0} - {1}", new object[]
				{
					hostId,
					msg
				});
			}
		}
		internal static DwmHostCollection SelectHostsToPowerOn(DwmPool pool, DwmVirtualMachineCollection vms, Comparison<DwmHost> hostCompare)
		{
			DwmHostCollection result = null;
			if (vms != null && vms.Count > 0)
			{
				result = DwmAEHost.SelectHostsToPowerOn(DwmHostCollection.GetPoweredOffHosts(pool.Id), pool, vms, hostCompare);
			}
			return result;
		}
		internal static DwmHostCollection SelectHostsToPowerOn(DwmHostCollection poweredOffHosts, DwmPool pool, DwmVirtualMachineCollection vms, Comparison<DwmHost> hostCompare)
		{
			DwmHostCollection dwmHostCollection = null;
			if (vms != null && vms.Count > 0 && poweredOffHosts != null && poweredOffHosts.Count > 0)
			{
				bool flag = (pool.OptMode != OptimizationMode.MaxDensity) ? pool.OverCommitCpusInPerfMode : pool.OverCommitCpusInDensityMode;
				long num = 0L;
				int num2 = 0;
				double num3 = 0.0;
				DwmStorageRepositoryCollection dwmStorageRepositoryCollection = new DwmStorageRepositoryCollection();
				foreach (DwmVirtualMachine current in vms)
				{
					num += current.RequiredMemory;
					if (!flag)
					{
						num2 += current.MinimumCpus;
					}
					else
					{
						num3 += current.Metrics.MetricsNow.AverageCpuUtilization * (double)current.MinimumCpus;
					}
					foreach (DwmStorageRepository current2 in current.RequiredStorage)
					{
						if (!dwmStorageRepositoryCollection.ContainsKey(current2.Id))
						{
							dwmStorageRepositoryCollection.Add(current2);
						}
					}
				}
				if (flag)
				{
					num2 = (int)Math.Round(num3 / pool.HostCpuThreshold.High + 0.5, 0);
				}
				poweredOffHosts.Sort(hostCompare);
				dwmHostCollection = new DwmHostCollection();
				foreach (DwmHost current3 in poweredOffHosts)
				{
					if (current3.ParticipatesInPowerManagement)
					{
						if (current3.Flags != 2 && current3.HasRequiredStorage(dwmStorageRepositoryCollection))
						{
							current3.Flags = 2;
							dwmHostCollection.Add(current3);
							num += current3.MemoryOverhead;
						}
						long num4 = 0L;
						int num5 = 0;
						foreach (DwmHost current4 in dwmHostCollection)
						{
							num4 += current4.Metrics.TotalMemory;
							num5 += current4.NumCpus;
						}
						if (num4 >= num && num5 >= num2)
						{
							break;
						}
					}
				}
			}
			return dwmHostCollection;
		}
	}
}
