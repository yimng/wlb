using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
namespace Halsign.DWM.Domain
{
	public class CloudVMPlacement
	{
		public static DwmHostCollection GetAllHosts(DwmVirtualMachine vm, bool sharedStorage, List<string> poolList, List<string> hostList, out DwmErrorCode resultCode)
		{
			DwmHostCollection dwmHostCollection = null;
			if (vm == null)
			{
				throw new DwmException("VM is null, missing required VM information.", DwmErrorCode.InvalidParameter, null);
			}
			if ((poolList != null && poolList.Count > 0) || (hostList != null && hostList.Count > 0))
			{
				dwmHostCollection = CloudVMPlacement.GetAllHosts(vm, "place_vm_by_uuids", new StoredProcParamCollection
				{
					new StoredProcParam("host_ids", (hostList == null || hostList.Count <= 0) ? null : hostList.ToArray(), (StoredProcParam.DataTypes)(-2147483626)),
					new StoredProcParam("pool_ids", (poolList == null || poolList.Count <= 0) ? null : poolList.ToArray(), (StoredProcParam.DataTypes)(-2147483626)),
					new StoredProcParam("sharedMemory", sharedStorage, StoredProcParam.DataTypes.Boolean)
				}, out resultCode);
				if (dwmHostCollection != null)
				{
					List<string> list = new List<string>();
					foreach (DwmHost current in dwmHostCollection)
					{
						list.Add(current.Uuid);
					}
					if (hostList != null && hostList.Count > 0)
					{
						foreach (string current2 in hostList)
						{
							if (!list.Contains(current2))
							{
								dwmHostCollection.Add(new DwmHost(-1)
								{
									Uuid = current2,
									PowerState = PowerStatus.None,
									Metrics = 
									{
										ZeroScoreReasons = 
										{
											ZeroScoreReason.HostDoesNotExist
										}
									}
								});
							}
						}
					}
				}
				DwmAEVirtualMachine.EnsurePositiveScores(dwmHostCollection);
				return dwmHostCollection;
			}
			throw new DwmException("At least one of the two lists needs to be provided, pool uuid list or host uuid list", DwmErrorCode.InvalidParameter, null);
		}
		internal static DwmHostCollection GetAllHosts(DwmVirtualMachine vm, string sqlStatement, StoredProcParamCollection parms, out DwmErrorCode resultCode)
		{
			DwmHostCollection dwmHostCollection = null;
			DwmPoolCollection dwmPoolCollection = null;
			DwmPool dwmPool = null;
			DwmHost dwmHost = null;
			int num = 0;
			resultCode = DwmErrorCode.None;
			using (DBAccess dBAccess = new DBAccess())
			{
				try
				{
					dBAccess.UseTransaction = true;
					using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, parms))
					{
						dwmHostCollection = new DwmHostCollection();
						dwmPoolCollection = new DwmPoolCollection();
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
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								string @string = DBAccess.GetString(dataReader, "uuid");
								string string2 = DBAccess.GetString(dataReader, "name");
								DwmHypervisorType int2 = (DwmHypervisorType)DBAccess.GetInt(dataReader, "hv_type");
								if (string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(string2))
								{
									DwmHostCollection result = null;
									return result;
								}
								dwmPool = new DwmPool(@string, string2, int2);
								dwmPool.Id = DBAccess.GetInt(dataReader, "id");
								dwmPool.OptMode = (OptimizationMode)DBAccess.GetInt(dataReader, "opt_mode");
								dwmPool.MaxCpuRating = DBAccess.GetInt(dataReader, "max_cpu_rating");
								dwmPool.IsLicensed = DBAccess.GetBool(dataReader, "is_licensed");
								if (!dwmPool.IsLicensed)
								{
									resultCode = DwmErrorCode.NotLicensed;
								}
								dwmPoolCollection.Add(dwmPool);
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								dwmPool = dwmPoolCollection.GetPool(DBAccess.GetInt(dataReader, "poolid"));
								if (dwmPool != null)
								{
									dwmPool.LoadThresholdsAndWeights(dataReader);
								}
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								dwmHost = DwmHost.LoadWithMetrics(dataReader);
								dwmPool = dwmPoolCollection.GetPool(DBAccess.GetInt(dataReader, "poolid"));
								if (dwmPool != null)
								{
									if (dwmPool.Hosts == null)
									{
										dwmPool.Hosts = new DwmHostCollection();
									}
									dwmPool.Hosts.Add(dwmHost);
									num++;
								}
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int int3 = DBAccess.GetInt(dataReader, "host_id");
								int int4 = DBAccess.GetInt(dataReader, "num_vm");
								dwmPool = dwmPoolCollection.GetPool(DBAccess.GetInt(dataReader, "poolid"));
								if (dwmPool != null && dwmPool.Hosts != null)
								{
									dwmHost = dwmPool.Hosts.GetHost(int3);
									if (dwmHost != null)
									{
										dwmHost.Metrics.NumVMs = int4;
									}
								}
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int int5 = DBAccess.GetInt(dataReader, "host_id");
								int int6 = DBAccess.GetInt(dataReader, "poolid");
								if (dwmPool == null || dwmPool.Id != int6)
								{
									dwmPool = dwmPoolCollection.GetPool(int6);
								}
								if (dwmPool != null && dwmPool.Hosts != null)
								{
									dwmHost = dwmPool.Hosts.GetHost(int5);
									if (dwmHost != null)
									{
										DwmStorageRepository dwmStorageRepository = new DwmStorageRepository(DBAccess.GetInt(dataReader, "sr_id"));
										dwmStorageRepository.Size = DBAccess.GetInt64(dataReader, "sr_size");
										dwmStorageRepository.Used = DBAccess.GetInt64(dataReader, "sr_used");
										dwmStorageRepository.PoolId = int6;
										dwmStorageRepository.PoolDefaultSR = DBAccess.GetBool(dataReader, "sr_default");
										dwmStorageRepository.Name = DBAccess.GetString(dataReader, "sr_name");
										dwmStorageRepository.Uuid = DBAccess.GetString(dataReader, "sr_uuid");
										dwmHost.AvailableStorage.Add(dwmStorageRepository);
									}
								}
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int int7 = DBAccess.GetInt(dataReader, "host_id");
								int int8 = DBAccess.GetInt(dataReader, "poolid");
								if (dwmPool == null || dwmPool.Id != int8)
								{
									dwmPool = dwmPoolCollection.GetPool(int8);
								}
								if (dwmPool != null && dwmPool.Hosts != null)
								{
									if (dwmHost == null || dwmHost.Id != int7)
									{
										dwmHost = dwmPool.Hosts.GetHost(int7);
									}
									if (dwmHost != null)
									{
										dwmHost.LoadPif(dataReader);
									}
								}
							}
						}
					}
				}
				catch
				{
					throw;
				}
			}
			if (num > 0 && resultCode == DwmErrorCode.None)
			{
				CloudVMPlacement.GetHostsScore(vm, dwmPoolCollection);
				foreach (DwmPool current in dwmPoolCollection)
				{
					dwmHostCollection.AddRange(current.Hosts);
				}
			}
			return dwmHostCollection;
		}
		private static void GetHostsScore(DwmVirtualMachine vm, DwmPoolCollection pools)
		{
			for (int i = pools.Count - 1; i >= 0; i--)
			{
				if (!DwmAEVirtualMachine.EvaluateHostCpuCount(pools[i].Hosts, vm))
				{
					return;
				}
				MemoryScoringType memScoringType = MemoryScoringType.StaticMax;
				bool flag;
				bool flag2;
				bool flag3;
				if (!DwmAEVirtualMachine.EvaluateHostMemory(pools[i].Hosts, vm, pools[i], out flag, out flag2, out flag3))
				{
					if (!flag3)
					{
						for (int j = pools[i].Hosts.Count - 1; j >= 0; j--)
						{
							pools[i].Hosts[j].Metrics.ZeroScoreReasons.Add(ZeroScoreReason.Memory);
						}
					}
					else
					{
						memScoringType = MemoryScoringType.PotentialFree;
					}
				}
				else
				{
					memScoringType = ((!flag) ? MemoryScoringType.DynamicMax : MemoryScoringType.StaticMax);
				}
				bool flag4 = false;
				for (int k = 0; k <= pools[i].Hosts.Count - 1; k++)
				{
					if (pools[i].Hosts[k].Enabled && pools[i].Hosts[k].AvailableStorage.Count > 0 && pools[i].Hosts[k].AvailableStorage[0].Size - pools[i].Hosts[k].AvailableStorage[0].Used > vm.RequiredStorage[0].Size)
					{
						flag4 = true;
						break;
					}
				}
				if (!flag4)
				{
					for (int l = pools[i].Hosts.Count - 1; l >= 0; l--)
					{
						pools[i].Hosts[l].Metrics.ZeroScoreReasons.Add(ZeroScoreReason.VmRequiresSr);
					}
				}
				if (pools[i].Hosts != null && pools[i].Hosts.Count > 0)
				{
					vm.Metrics.RunstateWeight = pools[i].VmRunstateWeight.High;
					vm.Metrics.CpuWeight = pools[i].VmCpuUtilizationWeight.High;
					vm.Metrics.MemoryWeight = pools[i].VmMemoryWeight.High;
					vm.Metrics.PifReadWeight = pools[i].VmNetworkReadWeight.High;
					vm.Metrics.PifWriteWeight = pools[i].VmNetworkWriteWeight.High;
					vm.Metrics.PbdReadWeight = pools[i].VmDiskReadWeight.High;
					vm.Metrics.PbdWriteWeight = pools[i].VmDiskWriteWeight.High;
					vm.Metrics.PowerManagementWeight = ((!pools[i].AutoBalance || !pools[i].ManagePower) ? ((!pools[i].AutoBalance && !pools[i].ManagePower) ? pools[i].VmPowerManagementWeight.Low : pools[i].VmPowerManagementWeight.Medium) : pools[i].VmPowerManagementWeight.High);
					DwmAEHostScorer dwmAEHostScorer = new DwmAEHostScorer(vm, pools[i], memScoringType, true);
					for (int m = pools[i].Hosts.Count - 1; m >= 0; m--)
					{
						if (pools[i].Hosts[m].PowerState == PowerStatus.Off)
						{
							pools[i].Hosts[m].Metrics.Score = 0;
							pools[i].Hosts[m].Metrics.Stars = 0.0;
							pools[i].Hosts[m].Metrics.ZeroScoreReasons.Add(ZeroScoreReason.HostOffline);
						}
						if (pools[i].Hosts[m].ExcludeFromPlacementRecommendations)
						{
							pools[i].Hosts[m].Metrics.Score = 0;
							pools[i].Hosts[m].Metrics.ZeroScoreReasons.Add(ZeroScoreReason.HostExcluded);
						}
						if (pools[i].Hosts[m].Metrics.ZeroScoreReasons.Count == 0)
						{
							pools[i].Hosts[m].Metrics.Score = dwmAEHostScorer.ComputeScore(pools[i].Hosts[m]);
							pools[i].Hosts[m].Metrics.Stars = DwmAEVirtualMachine.ComputeStars(pools[i].Hosts[m]);
						}
					}
				}
			}
		}
		private static void Trace(string msg, string hostName, int vmId)
		{
			if (Configuration.GetValueAsBool(ConfigItems.ScoreHostTrace))
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
