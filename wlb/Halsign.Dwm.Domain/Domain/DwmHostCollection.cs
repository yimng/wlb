using Halsign.DWM.Framework;
using System;
using System.Data;
namespace Halsign.DWM.Domain
{
	public class DwmHostCollection : DwmBaseCollection<DwmHost>
	{
		public override void Save(DBAccess db)
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Save(db);
			}
		}
		public void Load(int poolId)
		{
			this.InternalLoad(poolId);
		}
		public void Load(string poolUuid)
		{
			int num = DwmPoolBase.UuidToId(poolUuid);
			if (num > 0)
			{
				this.InternalLoad(num);
			}
		}
		public static DwmHostCollection GetPoweredOffHosts(int poolId)
		{
			string sql = "load_host_by_power_state";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("pool_id", poolId));
			storedProcParamCollection.Add(new StoredProcParam("power_state", 2));
			DwmHostCollection dwmHostCollection = new DwmHostCollection();
			dwmHostCollection.InternalLoad(sql, storedProcParamCollection);
			return dwmHostCollection;
		}
		private void InternalLoad(int poolId)
		{
			string sql = "load_host_by_pool_id";
			this.InternalLoad(sql, new StoredProcParamCollection
			{
				new StoredProcParam("pool_id", poolId)
			});
		}
		private void InternalLoad(string sql, StoredProcParamCollection parms)
		{
			base.Clear();
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader(sql, parms))
				{
					if (dataReader.Read())
					{
						do
						{
							int @int = DBAccess.GetInt(dataReader, "hostid");
							string @string = DBAccess.GetString(dataReader, "name");
							string string2 = DBAccess.GetString(dataReader, "uuid");
							int int2 = DBAccess.GetInt(dataReader, "poolid");
							DwmHost dwmHost = new DwmHost(string2, @string, int2);
							if (@int > 0)
							{
							}
							dwmHost.Description = DBAccess.GetString(dataReader, "description");
							dwmHost.NumCpus = DBAccess.GetInt(dataReader, "num_cpus");
							dwmHost.CpuSpeed = DBAccess.GetInt(dataReader, "cpu_speed");
							dwmHost.NumNics = DBAccess.GetInt(dataReader, "num_pifs");
							dwmHost.IsPoolMaster = DBAccess.GetBool(dataReader, "is_pool_master");
							dwmHost.Enabled = DBAccess.GetBool(dataReader, "enabled");
							dwmHost.Metrics.FillOrder = DBAccess.GetInt(dataReader, "fill_order");
							dwmHost.IPAddress = DBAccess.GetString(dataReader, "ip_address");
							dwmHost.MemoryOverhead = DBAccess.GetInt64(dataReader, "memory_overhead");
							dwmHost.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
							dwmHost.ParticipatesInPowerManagement = DBAccess.GetBool(dataReader, "can_power");
							dwmHost.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
							dwmHost.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
							dwmHost.Metrics.TotalMemory = DBAccess.GetInt64(dataReader, "total_mem");
							if (dwmHost.Metrics.TotalMemory == 0L)
							{
								dwmHost.Metrics.TotalMemory = DBAccess.GetInt64(dataReader, "total_mem_con");
							}
							base.Add(dwmHost);
						}
						while (dataReader.Read());
						if (dataReader.NextResult())
						{
							this.LoadVMs(dataReader);
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int int3 = DBAccess.GetInt(dataReader, "host_id");
								DwmHost host = this.GetHost(int3);
								if (host != null)
								{
									DwmStorageRepository item = new DwmStorageRepository(DBAccess.GetString(dataReader, "uuid"), DBAccess.GetString(dataReader, "name"), DBAccess.GetInt(dataReader, "poolid"), DBAccess.GetInt64(dataReader, "size"), DBAccess.GetInt64(dataReader, "used"), DBAccess.GetBool(dataReader, "pool_default_sr"));
									host.AvailableStorage.Add(item);
								}
							}
						}
					}
				}
			}
		}
		internal void LoadVMs(IDataReader reader)
		{
			while (reader.Read())
			{
				int @int = DBAccess.GetInt(reader, 0);
				DwmHost host = this.GetHost(@int);
				if (host != null)
				{
					int int2 = DBAccess.GetInt(reader, "vmid");
					string @string = DBAccess.GetString(reader, "name");
					string string2 = DBAccess.GetString(reader, "uuid");
					int int3 = DBAccess.GetInt(reader, "poolid");
					DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(string2, @string, int3);
					dwmVirtualMachine.Id = int2;
					dwmVirtualMachine.Description = DBAccess.GetString(reader, "description");
					dwmVirtualMachine.MinimumDynamicMemory = DBAccess.GetInt64(reader, "min_dynamic_memory");
					dwmVirtualMachine.MaximumDynamicMemory = DBAccess.GetInt64(reader, "max_dynamic_memory");
					dwmVirtualMachine.MinimumStaticMemory = DBAccess.GetInt64(reader, "min_static_memory");
					dwmVirtualMachine.MaximumStaticMemory = DBAccess.GetInt64(reader, "max_static_memory");
					dwmVirtualMachine.TargetMemory = DBAccess.GetInt64(reader, "target_memory");
					dwmVirtualMachine.MemoryOverhead = DBAccess.GetInt64(reader, "memory_overhead");
					dwmVirtualMachine.MinimumCpus = DBAccess.GetInt(reader, "min_cpus");
					dwmVirtualMachine.HvMemoryMultiplier = DBAccess.GetDouble(reader, "hv_memory_multiplier");
					dwmVirtualMachine.RequiredMemory = DBAccess.GetInt64(reader, "required_memory");
					dwmVirtualMachine.IsControlDomain = DBAccess.GetBool(reader, "is_control_domain");
					dwmVirtualMachine.IsAgile = DBAccess.GetBool(reader, "is_agile");
					dwmVirtualMachine.DriversUpToDate = DBAccess.GetBool(reader, "drivers_up_to_date");
					dwmVirtualMachine.RunningOnHostId = host.Id;
					dwmVirtualMachine.RunningOnHostName = host.Name;
					dwmVirtualMachine.RunningOnHostUuid = host.Uuid;
					dwmVirtualMachine.Status = (DwmStatus)DBAccess.GetInt(reader, "status");
					dwmVirtualMachine.LastResult = (DwmStatus)DBAccess.GetInt(reader, "last_result");
					dwmVirtualMachine.LastResultTime = DBAccess.GetDateTime(reader, "last_result_time");
					host.VirtualMachines.Add(dwmVirtualMachine);
				}
			}
		}
		public bool ContainsKey(int key)
		{
			bool result = false;
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Id == key)
				{
					result = true;
					break;
				}
			}
			return result;
		}
		public int IndexOf(string uuid)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (Localization.Compare(base[i].Uuid, uuid, true) == 0)
				{
					return i;
				}
			}
			return -1;
		}
		public DwmHost GetHost(int hostId)
		{
			DwmHost result = null;
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Id == hostId)
				{
					result = base[i];
					break;
				}
			}
			return result;
		}
		public DwmHostCollection Copy()
		{
			DwmHostCollection dwmHostCollection = new DwmHostCollection();
			for (int i = 0; i < base.Count; i++)
			{
				dwmHostCollection.Add(base[i].Copy());
			}
			return dwmHostCollection;
		}
	}
}
