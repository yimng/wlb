using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
namespace Halsign.DWM.Domain
{
	public class WlbTask
	{
		private int _id;
		private int _poolId;
		private string _name;
		private string _description;
		private bool _enabled;
		private WlbTaskTrigger _trigger = new WlbTaskTrigger();
		private string _owner;
		private bool _lastRunResult;
		private string _lastTouchedBy;
		private DateTime _lastTouched;
		private DateTime _created;
		public int Id
		{
			get
			{
				return this._id;
			}
			internal set
			{
				this._id = value;
			}
		}
		public int PoolId
		{
			get
			{
				return this._poolId;
			}
			set
			{
				this._poolId = value;
			}
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}
		public WlbTaskTrigger Trigger
		{
			get
			{
				return this._trigger;
			}
			set
			{
				this._trigger = value;
			}
		}
		public string Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}
		public bool LastRunResult
		{
			get
			{
				return this._lastRunResult;
			}
			set
			{
				this._lastRunResult = value;
			}
		}
		public string LastTouchedBy
		{
			get
			{
				return this._lastTouchedBy;
			}
			set
			{
				this._lastTouchedBy = value;
				if (string.IsNullOrEmpty(this._owner))
				{
					this._owner = value;
				}
			}
		}
		public DateTime LastTouched
		{
			get
			{
				return this._lastTouched;
			}
			set
			{
				this._lastTouched = value;
			}
		}
		public DateTime Created
		{
			get
			{
				return this._created;
			}
			set
			{
				this._created = value;
			}
		}
		public WlbTask()
		{
		}
		public WlbTask(int taskId)
		{
			this._id = taskId;
		}
		public override string ToString()
		{
			return this.Name;
		}
		public static List<WlbActionType> GetTaskActionTypes()
		{
			List<WlbActionType> list = new List<WlbActionType>();
			using (DBAccess dBAccess = new DBAccess())
			{
				string sqlStatement = "get_task_action_types";
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement))
				{
					while (dataReader.Read())
					{
						list.Add(new WlbActionType
						{
							Type = DBAccess.GetInt(dataReader, "type"),
							Name = DBAccess.GetString(dataReader, "name"),
							Description = DBAccess.GetString(dataReader, "description"),
							Assembly = DBAccess.GetString(dataReader, "assembly"),
							Class = DBAccess.GetString(dataReader, "class")
						});
					}
				}
			}
			return list;
		}
		public static WlbTask Load(int taskId)
		{
			WlbTask wlbTask = new WlbTask();
			wlbTask.Id = taskId;
			wlbTask.Load();
			return wlbTask;
		}
		public void Load()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader("load_sched_task_by_id", new StoredProcParamCollection
				{
					new StoredProcParam("@taskid", this.Id)
				}))
				{
					if (dataReader.Read())
					{
						this.Id = DBAccess.GetInt(dataReader, "taskid");
						this.PoolId = DBAccess.GetInt(dataReader, "poolid");
						this.Name = DBAccess.GetString(dataReader, "name");
						this.Description = DBAccess.GetString(dataReader, "description");
						this.Enabled = DBAccess.GetBool(dataReader, "enabled");
						this.Owner = DBAccess.GetString(dataReader, "owner");
						this.LastRunResult = DBAccess.GetBool(dataReader, "last_run_result");
						this.LastTouchedBy = DBAccess.GetString(dataReader, "last_touched_by");
						this.LastTouched = DBAccess.GetDateTime(dataReader, "last_touched");
						this.Trigger.TriggerId = DBAccess.GetInt(dataReader, "tiggerid");
						this.Trigger.Type = (WlbTriggerType)DBAccess.GetInt(dataReader, "trigger_type");
						this.Trigger.DaysOfWeek = (TriggerDaysOfWeek)DBAccess.GetInt(dataReader, "days_of_week");
						this.Trigger.ExecuteTime = DBAccess.GetDateTime(dataReader, "execute_time");
						this.Trigger.EnableDate = DBAccess.GetDateTime(dataReader, "enable_date");
						this.Trigger.DisableDate = DBAccess.GetDateTime(dataReader, "disable_date");
						this.Trigger.LastRun = DBAccess.GetDateTime(dataReader, "last_run");
						this.Trigger.Action.Type = DBAccess.GetInt(dataReader, "action_type");
						this.Trigger.Action.Name = DBAccess.GetString(dataReader, "action_name");
						this.Trigger.Action.Description = DBAccess.GetString(dataReader, "action_description");
						this.Trigger.Action.Assembly = DBAccess.GetString(dataReader, "assembly");
						this.Trigger.Action.Class = DBAccess.GetString(dataReader, "class");
					}
					if (dataReader.NextResult())
					{
						this.Trigger.Action.Parameters = new Dictionary<string, string>();
						while (dataReader.Read())
						{
							this.Trigger.Action.Parameters.Add(DBAccess.GetString(dataReader, "param_name"), DBAccess.GetString(dataReader, "param_value"));
						}
					}
				}
			}
		}
		public void Save()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				this.Save(dBAccess);
			}
		}
		public void Save(DBAccess db)
		{
			if (db == null)
			{
				throw new DwmException("Cannot pass null DBAccess instance to Save", DwmErrorCode.NullReference, null);
			}
			if (this.Trigger.Action.Type != 0 || this.Trigger.Action.Name != null)
			{
				string sqlStatement = "save_sched_task";
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("@taskid", this.Id));
				storedProcParamCollection.Add(new StoredProcParam("@poolid", this.PoolId));
				storedProcParamCollection.Add(new StoredProcParam("@name", string.IsNullOrEmpty(this.Name) ? string.Format("Task{0}", this.Id.ToString()) : this.Name));
				storedProcParamCollection.Add(new StoredProcParam("@description", string.IsNullOrEmpty(this.Description) ? string.Format("Task{0}", this.Id.ToString()) : this.Description));
				storedProcParamCollection.Add(new StoredProcParam("@enabled", this.Enabled));
				storedProcParamCollection.Add(new StoredProcParam("@owner", string.IsNullOrEmpty(this.Owner) ? DBAccess.GetCurrentUserName() : this.Owner));
				storedProcParamCollection.Add(new StoredProcParam("@touched_by", string.IsNullOrEmpty(this.LastTouchedBy) ? DBAccess.GetCurrentUserName() : this.LastTouchedBy));
				storedProcParamCollection.Add(new StoredProcParam("@trigger_type", (int)this.Trigger.Type));
				storedProcParamCollection.Add(new StoredProcParam("@days_of_week", (int)this.Trigger.DaysOfWeek));
				storedProcParamCollection.Add(new StoredProcParam("@execute_time", this.Trigger.ExecuteTime, StoredProcParam.DataTypes.Timestamp));
				if (DateTime.Compare(this.Trigger.EnableDate, DateTime.MinValue) != 0)
				{
					storedProcParamCollection.Add(new StoredProcParam("@enable_date", this.Trigger.EnableDate, StoredProcParam.DataTypes.Timestamp));
				}
				else
				{
					storedProcParamCollection.Add(new StoredProcParam("@enable_date", DBNull.Value, StoredProcParam.DataTypes.Timestamp));
				}
				if (DateTime.Compare(this.Trigger.DisableDate, DateTime.MinValue) != 0)
				{
					storedProcParamCollection.Add(new StoredProcParam("@disable_date", this.Trigger.DisableDate, StoredProcParam.DataTypes.Timestamp));
				}
				else
				{
					storedProcParamCollection.Add(new StoredProcParam("disable_date", DBNull.Value, StoredProcParam.DataTypes.Timestamp));
				}
				storedProcParamCollection.Add(new StoredProcParam("@action_type", this.Trigger.Action.Type));
				if (this.Trigger.Action.Name != null)
				{
					storedProcParamCollection.Add(new StoredProcParam("@action_name", this.Trigger.Action.Name));
				}
				else
				{
					storedProcParamCollection.Add(new StoredProcParam("@action_name", DBNull.Value));
				}
				this.Id = db.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
				string sqlStatement2 = "save_sched_task_action_param";
				foreach (string current in this.Trigger.Action.Parameters.Keys)
				{
					storedProcParamCollection.Clear();
					storedProcParamCollection.Add(new StoredProcParam("@taskid", this.Id));
					storedProcParamCollection.Add(new StoredProcParam("@name", current));
					storedProcParamCollection.Add(new StoredProcParam("@value", this.Trigger.Action.Parameters[current]));
					db.ExecuteNonQuery(sqlStatement2, storedProcParamCollection);
				}
				return;
			}
			throw new DwmException("Task is not fully configured.", DwmErrorCode.NotConfigured, null);
		}
		public void Delete()
		{
			if (this.Id > 0)
			{
				WlbTask.Delete(this.Id);
				return;
			}
			throw new DwmException("The ID of the task to delete must be specified", DwmErrorCode.InvalidParameter, null);
		}
		public static void Delete(int TaskId)
		{
			DateTime now = DateTime.Now;
			string sqlStatement = "delete_sched_task_by_id";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@taskid", TaskId));
			int num;
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.Timeout = 60;
				num = dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
			if (num > 0)
			{
				Logger.Trace("Deleted task with id={0} in {1} seconds", new object[]
				{
					TaskId,
					(DateTime.Now - now).Seconds
				});
			}
		}
		public static void SetLastRun(int TaskId, bool Success)
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				string sqlStatement = "set_task_last_run_result";
				dBAccess.ExecuteNonQuery(sqlStatement, new StoredProcParamCollection
				{
					new StoredProcParam("@taskid", TaskId, StoredProcParam.DataTypes.Integer),
					new StoredProcParam("@success", Success, StoredProcParam.DataTypes.Boolean)
				});
			}
		}
	}
}
