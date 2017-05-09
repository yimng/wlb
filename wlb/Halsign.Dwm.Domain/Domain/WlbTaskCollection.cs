using Halsign.DWM.Framework;
using System;
using System.Data;
namespace Halsign.DWM.Domain
{
	public class WlbTaskCollection : DwmBaseCollection<WlbTask>
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
		public WlbTask GetItem(int key)
		{
			WlbTask result = null;
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Id == key)
				{
					result = base[i];
					break;
				}
			}
			return result;
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
			this.Load(0, 0);
		}
		public void Load(int PoolId)
		{
			this.Load(PoolId, 0);
		}
		public void Load(string poolUuid)
		{
			int num = DwmPoolBase.UuidToId(poolUuid);
			if (num > 0)
			{
				this.Load(num, 0);
			}
		}
		public void Load(string poolUuid, int ActionType)
		{
			int num = DwmPoolBase.UuidToId(poolUuid);
			if (num > 0)
			{
				this.Load(num, ActionType);
			}
		}
		public void Load(int PoolId, int ActionType)
		{
			string sqlStatement = "load_sched_tasks_by_poolid_and_type";
			this.InternalLoad(sqlStatement, new StoredProcParamCollection
			{
				new StoredProcParam("@poolid", PoolId),
				new StoredProcParam("@actiontype", ActionType)
			});
		}
		public void Load(int PoolId, string ActionName)
		{
			string sqlStatement = "load_sched_tasks_by_poolid_and_type_name";
			this.InternalLoad(sqlStatement, new StoredProcParamCollection
			{
				new StoredProcParam("@poolid", PoolId),
				new StoredProcParam("@actiontypename", ActionName)
			});
		}
		public void LoadPending()
		{
			string sqlStatement = "load_pending_sched_tasks";
			this.InternalLoad(sqlStatement, null);
		}
		private void InternalLoad(string sqlStatement, StoredProcParamCollection parms)
		{
			base.Clear();
			try
			{
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.UseTransaction = true;
					using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, parms))
					{
						if (dataReader != null)
						{
							while (dataReader.Read())
							{
								base.Add(new WlbTask
								{
									Id = DBAccess.GetInt(dataReader, "taskid"),
									PoolId = DBAccess.GetInt(dataReader, "poolid"),
									Name = DBAccess.GetString(dataReader, "name"),
									Description = DBAccess.GetString(dataReader, "description"),
									Enabled = DBAccess.GetBool(dataReader, "enabled"),
									Owner = DBAccess.GetString(dataReader, "owner"),
									LastRunResult = DBAccess.GetBool(dataReader, "last_run_result"),
									LastTouchedBy = DBAccess.GetString(dataReader, "last_touched_by"),
									LastTouched = DBAccess.GetDateTime(dataReader, "last_touched"),
									Trigger = 
									{
										TriggerId = DBAccess.GetInt(dataReader, "tiggerid"),
										Type = (WlbTriggerType)DBAccess.GetInt(dataReader, "trigger_type"),
										DaysOfWeek = (TriggerDaysOfWeek)DBAccess.GetInt(dataReader, "days_of_week"),
										ExecuteTime = DBAccess.GetDateTime(dataReader, "execute_time"),
										EnableDate = DBAccess.GetDateTime(dataReader, "enable_date"),
										DisableDate = DBAccess.GetDateTime(dataReader, "disable_date"),
										LastRun = DBAccess.GetDateTime(dataReader, "last_run"),
										Action = 
										{
											Type = DBAccess.GetInt(dataReader, "action_type"),
											Name = DBAccess.GetString(dataReader, "action_name"),
											Description = DBAccess.GetString(dataReader, "action_description"),
											Assembly = DBAccess.GetString(dataReader, "assembly"),
											Class = DBAccess.GetString(dataReader, "class")
										}
									}
								});
							}
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								WlbTask item = this.GetItem(DBAccess.GetInt(dataReader, "taskid"));
								if (item != null && !item.Trigger.Action.Parameters.ContainsKey(DBAccess.GetString(dataReader, "param_name")))
								{
									item.Trigger.Action.Parameters.Add(DBAccess.GetString(dataReader, "param_name"), DBAccess.GetString(dataReader, "param_value"));
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("The following error was encountered when attempting to load scheduled tasks:", new object[0]);
				Logger.LogException(ex);
			}
		}
	}
}
