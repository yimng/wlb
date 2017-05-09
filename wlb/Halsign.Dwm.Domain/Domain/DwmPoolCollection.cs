using Halsign.DWM.Framework;
using System;
using System.Data;
namespace Halsign.DWM.Domain
{
	public class DwmPoolCollection : DwmBaseCollection<DwmPool>
	{
		public bool ContainsKey(int key)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Id == key)
				{
					return true;
				}
			}
			return false;
		}
		public override void Save(DBAccess db)
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Save(db);
			}
		}
		public void Load()
		{
			string sqlStatement = "load_hv_pools";
			base.Clear();
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement))
				{
					if (dataReader != null)
					{
						while (dataReader.Read())
						{
							DwmPool dwmPool = new DwmPool(DBAccess.GetString(dataReader, "uuid"), DBAccess.GetString(dataReader, "name"), (DwmHypervisorType)DBAccess.GetInt(dataReader, "hv_type"));
							dwmPool.Id = DBAccess.GetInt(dataReader, "id");
							dwmPool.Name = DBAccess.GetString(dataReader, "name");
							dwmPool.Description = DBAccess.GetString(dataReader, "description");
							dwmPool.PrimaryPoolMasterAddr = DBAccess.GetString(dataReader, "pool_master_1_addr");
							dwmPool.SecondaryPoolMasterAddr = DBAccess.GetString(dataReader, "pool_master_2_addr");
							dwmPool.PrimaryPoolMasterPort = DBAccess.GetInt(dataReader, "pool_master_1_port");
							dwmPool.SecondaryPoolMasterPort = DBAccess.GetInt(dataReader, "pool_master_2_port");
							dwmPool.Protocol = DBAccess.GetString(dataReader, "protocol");
							dwmPool.Enabled = DBAccess.GetBool(dataReader, "enabled");
							dwmPool.IsLicensed = DBAccess.GetBool(dataReader, "is_licensed");
							dwmPool.UserName = DBAccess.GetString(dataReader, "username");
							dwmPool.EncryptedPassword = DBAccess.GetString(dataReader, "password");
							dwmPool.TouchedBy = DBAccess.GetString(dataReader, "touched_by");
							dwmPool.TimeStamp = DBAccess.GetDateTime(dataReader, "tstamp");
							dwmPool.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
							dwmPool.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
							dwmPool.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
							dwmPool.PoolDiscoveryStatus = (DiscoveryStatus)DBAccess.GetInt(dataReader, "discovery_status");
							dwmPool.LastDiscoveryCompleted = DBAccess.GetDateTime(dataReader, "last_discovery_completed");
							dwmPool.LoadThresholdsAndWeights(dataReader);
							base.Add(dwmPool);
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int @int = DBAccess.GetInt(dataReader, "poolid");
								int int2 = DBAccess.GetInt(dataReader, "hostid");
								string @string = DBAccess.GetString(dataReader, "name");
								string string2 = DBAccess.GetString(dataReader, "uuid");
								DwmHost dwmHost = new DwmHost(string2, @string, @int);
								dwmHost.Id = int2;
								dwmHost.Description = DBAccess.GetString(dataReader, "description");
								dwmHost.NumCpus = DBAccess.GetInt(dataReader, "num_cpus");
								dwmHost.CpuSpeed = DBAccess.GetInt(dataReader, "cpu_speed");
								dwmHost.NumNics = DBAccess.GetInt(dataReader, "num_pifs");
								dwmHost.IsPoolMaster = DBAccess.GetBool(dataReader, "is_pool_master");
								dwmHost.Enabled = DBAccess.GetBool(dataReader, "enabled");
								dwmHost.Metrics.FillOrder = DBAccess.GetInt(dataReader, "fill_order");
								dwmHost.IPAddress = DBAccess.GetString(dataReader, "ip_address");
								dwmHost.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
								dwmHost.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
								dwmHost.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
								dwmHost.Metrics.TotalMemory = DBAccess.GetInt64(dataReader, "total_mem");
								DwmPool pool = this.GetPool(@int);
								if (pool != null)
								{
									pool.Hosts.Add(dwmHost);
								}
							}
						}
						if (dataReader.NextResult())
						{
							int num = 0;
							int num2 = 0;
							DwmPool dwmPool2 = null;
							DwmHost dwmHost2 = null;
							while (dataReader.Read())
							{
								int int3 = DBAccess.GetInt(dataReader, "poolid");
								int int4 = DBAccess.GetInt(dataReader, "hostid");
								if (int3 != num || dwmPool2 == null)
								{
									dwmPool2 = this.GetPool(int3);
									if (dwmPool2 != null)
									{
										num = int3;
										num2 = 0;
										dwmHost2 = null;
									}
								}
								if (dwmPool2 != null)
								{
									if (int4 != num2 || dwmHost2 == null)
									{
										dwmHost2 = dwmPool2.Hosts.GetHost(int4);
										if (dwmHost2 != null)
										{
											num2 = int4;
										}
									}
									if (dwmHost2 != null)
									{
										int int5 = DBAccess.GetInt(dataReader, "vmid");
										string string3 = DBAccess.GetString(dataReader, "name");
										string string4 = DBAccess.GetString(dataReader, "uuid");
										DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(string4, string3, dwmPool2.Id);
										dwmVirtualMachine.Id = int5;
										dwmVirtualMachine.Description = DBAccess.GetString(dataReader, "description");
										dwmVirtualMachine.MinimumDynamicMemory = DBAccess.GetInt64(dataReader, "min_dynamic_memory");
										dwmVirtualMachine.MaximumDynamicMemory = DBAccess.GetInt64(dataReader, "max_dynamic_memory");
										dwmVirtualMachine.MinimumStaticMemory = DBAccess.GetInt64(dataReader, "min_static_memory");
										dwmVirtualMachine.MaximumStaticMemory = DBAccess.GetInt64(dataReader, "max_static_memory");
										dwmVirtualMachine.TargetMemory = DBAccess.GetInt64(dataReader, "target_memory");
										dwmVirtualMachine.MemoryOverhead = DBAccess.GetInt64(dataReader, "memory_overhead");
										dwmVirtualMachine.MinimumCpus = DBAccess.GetInt(dataReader, "min_cpus");
										dwmVirtualMachine.HvMemoryMultiplier = DBAccess.GetDouble(dataReader, "hv_memory_multiplier");
										dwmVirtualMachine.RequiredMemory = DBAccess.GetInt64(dataReader, "required_memory");
										dwmVirtualMachine.IsControlDomain = DBAccess.GetBool(dataReader, "is_control_domain");
										dwmVirtualMachine.IsAgile = DBAccess.GetBool(dataReader, "is_agile");
										dwmVirtualMachine.DriversUpToDate = DBAccess.GetBool(dataReader, "drivers_up_to_date");
										dwmVirtualMachine.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
										dwmVirtualMachine.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
										dwmVirtualMachine.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
										dwmVirtualMachine.RunningOnHostId = dwmHost2.Id;
										dwmVirtualMachine.RunningOnHostName = dwmHost2.Name;
										dwmVirtualMachine.RunningOnHostUuid = dwmHost2.Uuid;
										dwmHost2.VirtualMachines.Add(dwmVirtualMachine);
									}
								}
							}
						}
					}
				}
			}
		}
		public static DwmPoolCollection LoadPoolsForDataCollection()
		{
			string sqlStatement = "data_collection_get_all_pools";
			DwmPoolCollection dwmPoolCollection = new DwmPoolCollection();
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement))
				{
					while (dataReader.Read())
					{
						dwmPoolCollection.Add(new DwmPool(DBAccess.GetInt(dataReader, "id"))
						{
							HVType = (DwmHypervisorType)DBAccess.GetInt(dataReader, "hv_type"),
							PrimaryPoolMasterAddr = DBAccess.GetString(dataReader, "pool_master_1_addr"),
							PrimaryPoolMasterPort = DBAccess.GetInt(dataReader, "pool_master_1_port"),
							Protocol = DBAccess.GetString(dataReader, "protocol"),
							UserName = DBAccess.GetString(dataReader, "username"),
							EncryptedPassword = DBAccess.GetString(dataReader, "password")
						});
					}
				}
			}
			return dwmPoolCollection;
		}
		public DwmPool GetPool(int poolId)
		{
			DwmPool result = null;
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Id == poolId)
				{
					result = base[i];
					break;
				}
			}
			return result;
		}
	}
}
