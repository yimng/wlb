using Halsign.DWM.Framework;
using System;
using System.Data;
namespace Halsign.DWM.Domain
{
	public sealed class DwmAudit
	{
		private DwmAudit()
		{
		}
		internal static int AddAuditRecord(ref int setId, int? moveRecId, AuditEventType type, int? vmId, int? fromHostId, int? toHostId, AuditEventStatus status, DateTime startTime)
		{
			int result = 0;
			string sqlStatement = "hv_audit_add";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@event_id", setId));
			storedProcParamCollection.Add(new StoredProcParam("@move_rec_id", (!moveRecId.HasValue || moveRecId.Value <= 0) ? null : moveRecId));
			storedProcParamCollection.Add(new StoredProcParam("@event_type", (int)type));
			storedProcParamCollection.Add(new StoredProcParam("@vm_id", (!vmId.HasValue || vmId.Value <= 0) ? null : vmId));
			storedProcParamCollection.Add(new StoredProcParam("@from_host_id", (!fromHostId.HasValue || fromHostId.Value <= 0) ? null : fromHostId));
			storedProcParamCollection.Add(new StoredProcParam("@to_host_id", (!toHostId.HasValue || toHostId.Value <= 0) ? null : toHostId));
			storedProcParamCollection.Add(new StoredProcParam("@status", (int)status));
			storedProcParamCollection.Add(new StoredProcParam("@start_time", startTime));
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
				{
					try
					{
						if (dataReader.Read())
						{
							result = DBAccess.GetInt(dataReader, 0);
							setId = DBAccess.GetInt(dataReader, 1);
						}
					}
					catch
					{
						throw;
					}
				}
			}
			return result;
		}
		internal static void UpdateAuditRecord(int auditRecordId, AuditEventStatus status)
		{
			DwmAudit.UpdateAuditRecord(auditRecordId, status, null);
		}
		internal static void UpdateAuditRecord(int auditRecordId, AuditEventStatus status, DateTime? endTime)
		{
			string sqlStatement = "hv_audit_update";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@audit_id", auditRecordId));
			storedProcParamCollection.Add(new StoredProcParam("@status", (int)status));
			if (endTime.HasValue)
			{
				storedProcParamCollection.Add(new StoredProcParam("@end_time", endTime));
			}
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		internal static int AddRecommendationRecord(ref int setId, MoveRecommendationType type, int vmId, int fromHostId, int toHostId)
		{
			return DwmAudit.AddRecommendationRecord(ref setId, type, new int?(vmId), new int?(fromHostId), new int?(toHostId), OptimizationSeverity.None, ResourceToOptimize.None, DateTime.UtcNow, null, null);
		}
		internal static int AddRecommendationRecord(ref int setId, MoveRecommendationType type, int? vmId, int? fromHostId, int? toHostId, OptimizationSeverity severity, ResourceToOptimize reason, DateTime startTime, DateTime? origonalTimeStamp, DateTime? lastNotifyTimeStamp)
		{
			int result = 0;
			string sqlStatement = "move_recommendation_add";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@event_id", setId));
			storedProcParamCollection.Add(new StoredProcParam("@event_type", (int)type));
			storedProcParamCollection.Add(new StoredProcParam("@vm_id", (!vmId.HasValue || vmId.Value <= 0) ? null : vmId));
			storedProcParamCollection.Add(new StoredProcParam("@from_host_id", (!fromHostId.HasValue || fromHostId.Value <= 0) ? null : fromHostId));
			storedProcParamCollection.Add(new StoredProcParam("@to_host_id", (!toHostId.HasValue || toHostId.Value <= 0) ? null : toHostId));
			storedProcParamCollection.Add(new StoredProcParam("@severity", (int)severity));
			storedProcParamCollection.Add(new StoredProcParam("@reason", (int)reason));
			storedProcParamCollection.Add(new StoredProcParam("@start_time", startTime));
			if (origonalTimeStamp.HasValue && origonalTimeStamp.Value == DateTime.MinValue)
			{
				storedProcParamCollection.Add(new StoredProcParam("@origonal_time", null));
			}
			else
			{
				storedProcParamCollection.Add(new StoredProcParam("@origonal_time", origonalTimeStamp));
			}
			if (lastNotifyTimeStamp.HasValue && lastNotifyTimeStamp.Value == DateTime.MinValue)
			{
				storedProcParamCollection.Add(new StoredProcParam("@last_alert_time", null));
			}
			else
			{
				storedProcParamCollection.Add(new StoredProcParam("@last_alert_time", lastNotifyTimeStamp));
			}
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						result = DBAccess.GetInt(dataReader, 0);
						setId = DBAccess.GetInt(dataReader, 1);
					}
				}
			}
			return result;
		}
	}
}
