using Halsign.DWM.Framework;
using System;
namespace Halsign.DWM.Domain
{
	public class WlbAuditLogEntryCollection : DwmBaseCollection<WlbAuditLogEntry>
	{
		public override void Save(DBAccess db)
		{
			db.Timeout = 60;
			if (base.IsNew)
			{
				string sqlStatement = "hv_audit_log_add";
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				foreach (WlbAuditLogEntry current in this)
				{
					storedProcParamCollection.Clear();
					WlbAuditLogEntryCollection.Trace("Starting Save Audit Log entry of timestamp: {0}", new object[]
					{
						current.TimeStamp
					});
					if (current.PoolId > 0)
					{
						storedProcParamCollection.Add(new StoredProcParam("_log_tstamp", current.TimeStamp));
						storedProcParamCollection.Add(new StoredProcParam("_log_type", current.LogType));
						storedProcParamCollection.Add(new StoredProcParam("_pool_id", current.PoolId));
						storedProcParamCollection.Add(new StoredProcParam("_session", current.SessionId));
						storedProcParamCollection.Add(new StoredProcParam("_user_sid", current.UserId));
						storedProcParamCollection.Add(new StoredProcParam("_user_name", current.UserName));
						storedProcParamCollection.Add(new StoredProcParam("_access_allowed", current.AccessAllowed));
						storedProcParamCollection.Add(new StoredProcParam("_succeeded", current.Succeeded));
						storedProcParamCollection.Add(new StoredProcParam("_error_info", current.ErrorInfo));
						storedProcParamCollection.Add(new StoredProcParam("_call_type", current.CallType));
						storedProcParamCollection.Add(new StoredProcParam("_event_object", current.EventObject));
						storedProcParamCollection.Add(new StoredProcParam("_event_object_name", current.EventObjectName));
						storedProcParamCollection.Add(new StoredProcParam("_event_object_type", current.EventObjectType));
						storedProcParamCollection.Add(new StoredProcParam("_event_object_uuid", current.EventObjectUuid));
						storedProcParamCollection.Add(new StoredProcParam("_event_object_opaqueref", current.EventObjectOpaqueref));
						storedProcParamCollection.Add(new StoredProcParam("_event_action", current.EventAction));
						db.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
					}
					WlbAuditLogEntryCollection.Trace("Done with Saving Audit Log entry for pool: {0}, timestemp: {1}", new object[]
					{
						current.PoolId,
						current.TimeStamp
					});
				}
			}
		}
		internal static void Trace(string fmt, params object[] args)
		{
			if (Configuration.GetValueAsBool(ConfigItems.AuditLogTrace))
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
