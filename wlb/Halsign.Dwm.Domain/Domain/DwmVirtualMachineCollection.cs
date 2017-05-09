using Halsign.DWM.Framework;
using System;
using System.Data;
using System.Text;
namespace Halsign.DWM.Domain
{
	public class DwmVirtualMachineCollection : DwmBaseCollection<DwmVirtualMachine>
	{
		public override void Save(DBAccess db)
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Save(db);
			}
		}
		public void Save(int poolId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (DBAccess dBAccess = new DBAccess())
			{
				for (int i = 0; i < base.Count; i++)
				{
					base[i].Save(dBAccess);
					stringBuilder.AppendFormat("{0}{1}", (i == 0) ? string.Empty : ",", base[i].Id);
				}
				string sqlStatement = Localization.Format("update virtual_machine set active=false where id not in ({0}) and poolid={1}", stringBuilder.ToString(), poolId);
				dBAccess.ExecuteNonQuery(sqlStatement);
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
		public DwmVirtualMachine GetVM(int vmId)
		{
			DwmVirtualMachine result = null;
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Id == vmId)
				{
					result = base[i];
					break;
				}
			}
			return result;
		}
		public void Load(int poolId)
		{
			string sql = "load_vms_by_pool_id";
			this.InternalLoad(sql, new StoredProcParamCollection
			{
				new StoredProcParam("pool_id", poolId)
			});
		}
		public void Load(string poolUuid)
		{
			int poolId = DwmPoolBase.UuidToId(poolUuid);
			this.Load(poolId);
		}
		private void InternalLoad(string sql, StoredProcParamCollection parms)
		{
			base.Clear();
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sql, parms))
				{
					if (dataReader != null)
					{
						while (dataReader.Read())
						{
							base.Add(new DwmVirtualMachine(DBAccess.GetString(dataReader, "uuid"), DBAccess.GetString(dataReader, "name"), DBAccess.GetInt(dataReader, "poolid"))
							{
								Id = DBAccess.GetInt(dataReader, 0),
								Description = DBAccess.GetString(dataReader, "description"),
								AffinityHostId = DBAccess.GetInt(dataReader, "host_affinity"),
								MinimumDynamicMemory = DBAccess.GetInt64(dataReader, "min_dynamic_memory"),
								MaximumDynamicMemory = DBAccess.GetInt64(dataReader, "max_dynamic_memory"),
								MinimumStaticMemory = DBAccess.GetInt64(dataReader, "min_static_memory"),
								MaximumStaticMemory = DBAccess.GetInt64(dataReader, "max_static_memory"),
								TargetMemory = DBAccess.GetInt64(dataReader, "target_memory"),
								MemoryOverhead = DBAccess.GetInt64(dataReader, "memory_overhead"),
								HvMemoryMultiplier = DBAccess.GetDouble(dataReader, "hv_memory_multiplier"),
								RequiredMemory = DBAccess.GetInt64(dataReader, "required_memory"),
								MinimumCpus = DBAccess.GetInt(dataReader, "min_cpus"),
								IsAgile = DBAccess.GetBool(dataReader, "is_agile"),
								DriversUpToDate = DBAccess.GetBool(dataReader, "drivers_up_to_date"),
								IsControlDomain = DBAccess.GetBool(dataReader, "is_control_domain"),
								RunningOnHostId = DBAccess.GetInt(dataReader, "hostid"),
								RunningOnHostName = DBAccess.GetString(dataReader, "hostname"),
								RunningOnHostUuid = DBAccess.GetString(dataReader, "host_uuid"),
								Active = DBAccess.GetBool(dataReader, "active"),
								Status = (DwmStatus)DBAccess.GetInt(dataReader, "status"),
								LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result"),
								LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time")
							});
						}
					}
				}
			}
		}
		public DwmVirtualMachineCollection Copy()
		{
			DwmVirtualMachineCollection dwmVirtualMachineCollection = new DwmVirtualMachineCollection();
			for (int i = 0; i < base.Count; i++)
			{
				dwmVirtualMachineCollection.Add(base[i].Copy());
			}
			return dwmVirtualMachineCollection;
		}
	}
}
