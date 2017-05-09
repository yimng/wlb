using Halsign.DWM.Collectors;
using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace Halsign.DWM.Domain
{
	internal class DwmAsyncVmActions
	{
		internal List<MoveRecommendation> _migrations;
		internal int _result;
		internal string _vmUuid;
		internal string _hostUuid;
		internal int _poolId;
		internal int _recommendationId;
		internal void MigrationVmThreadProc()
		{
			int num = 0;
			DateTime utcNow = DateTime.UtcNow;
			if (this._migrations != null && this._migrations.Count > 0)
			{
				Logger.Trace("MigrationVmThreadProc:  Number of VMs to migration is {0}", new object[]
				{
					this._migrations.Count
				});
				int poolId = (this._migrations[0].PoolId <= 0) ? DwmPoolBase.UuidToId(this._migrations[0].PoolUuid) : this._migrations[0].PoolId;
				ICollectorActions collector = DwmPool.GetCollector(poolId);
				if (collector != null)
				{
					try
					{
						this.LockAll();
						for (int i = 0; i < this._migrations.Count; i++)
						{
							this._migrations[i].VmId = DwmVirtualMachine.UuidToId(this._migrations[i].VmUuid, poolId);
							this._migrations[i].MoveFromHostId = DwmHost.UuidToId(this._migrations[i].MoveFromHostUuid, poolId);
							this._migrations[i].MoveToHostId = DwmHost.UuidToId(this._migrations[i].MoveToHostUuid, poolId);
							this._migrations[i].AuditId = DwmAudit.AddAuditRecord(ref num, new int?(this._migrations[i].RecommendationId), AuditEventType.VmMigrate, new int?(this._migrations[i].VmId), new int?(this._migrations[i].MoveFromHostId), new int?(this._migrations[i].MoveToHostId), AuditEventStatus.NotStarted, utcNow);
						}
						for (int j = 0; j < this._migrations.Count; j++)
						{
							DwmAudit.UpdateAuditRecord(this._migrations[j].AuditId, AuditEventStatus.InProgress, new DateTime?(DateTime.UtcNow));
							Logger.Trace("MigrationVmThreadProc:  Migrating {0} to {1}", new object[]
							{
								string.IsNullOrEmpty(this._migrations[j].VmName) ? this._migrations[j].VmUuid : this._migrations[j].VmName,
								string.IsNullOrEmpty(this._migrations[j].MoveToHostName) ? this._migrations[j].MoveToHostUuid : this._migrations[j].MoveToHostName
							});
							int num2 = collector.MigrateVM(this._migrations[j].VmUuid, this._migrations[j].MoveToHostUuid, this._migrations[j].RecommendationId, true);
							if (num2 != 0)
							{
								Logger.Trace("MigrationVmThreadProc:  migration failed with rc={0}.  Retrying in 20 seconds.", new object[]
								{
									num2
								});
								Thread.Sleep(20000);
								num2 = collector.MigrateVM(this._migrations[j].VmUuid, this._migrations[j].MoveToHostUuid, this._migrations[j].RecommendationId, true);
							}
							this._result |= num2;
							Logger.Trace("MigrationVmThreadProc:  Result of migration is {0}", new object[]
							{
								num2
							});
							DwmAudit.UpdateAuditRecord(this._migrations[j].AuditId, (num2 != 0) ? AuditEventStatus.Failed : AuditEventStatus.Complete, new DateTime?(DateTime.UtcNow));
							DwmStatus result = (num2 != 0) ? DwmStatus.Failed : DwmStatus.Complete;
							DwmVirtualMachine.SetLastResult(this._migrations[j].VmId, result);
							DwmHost.SetLastResult(this._migrations[j].MoveToHostId, result);
							DwmHost.SetLastResult(this._migrations[j].MoveFromHostId, result);
						}
					}
					catch (Exception ex)
					{
						Logger.Trace("MigrationThreadProc: Caught exception: {0}", new object[]
						{
							ex.Message
						});
					}
					finally
					{
						DwmPool.SetLastResultByHostId(DwmHost.UuidToId(this._migrations[0].MoveFromHostUuid, poolId), (this._result != 0) ? DwmStatus.Failed : DwmStatus.Complete);
						this.UnLockAll();
					}
				}
				else
				{
					this._result = 4013;
				}
			}
		}
		internal void StartVmThreadProc()
		{
			int num = 0;
			this._result = 0;
			if (!string.IsNullOrEmpty(this._vmUuid) && !string.IsNullOrEmpty(this._hostUuid) && this._poolId > 0)
			{
				ICollectorActions collector = DwmPool.GetCollector(this._poolId);
				if (collector != null)
				{
					int num2 = 0;
					int num3 = 0;
					try
					{
						num2 = DwmVirtualMachine.UuidToId(this._vmUuid, this._poolId);
						num3 = DwmHost.UuidToId(this._hostUuid, this._poolId);
						DwmVirtualMachine.SetStatus(num2, DwmStatus.OperationsPending);
						DwmHost.SetStatus(num3, DwmStatus.OperationsPending);
						int auditRecordId = DwmAudit.AddAuditRecord(ref num, new int?(this._recommendationId), AuditEventType.VmStart, new int?(num2), new int?(0), new int?(num3), AuditEventStatus.InProgress, DateTime.UtcNow);
						Logger.Trace("StartVmThreadProc:  Starting vm {0} on host {1}", new object[]
						{
							this._vmUuid,
							this._hostUuid
						});
						this._result = collector.StartVM(this._vmUuid, this._hostUuid, false);
						Logger.Trace("StartVmThreadProc:  Result of starting is {0}", new object[]
						{
							this._result
						});
						DwmAudit.UpdateAuditRecord(auditRecordId, (this._result != 0) ? AuditEventStatus.Failed : AuditEventStatus.Complete);
						DwmStatus result = (this._result != 0) ? DwmStatus.Failed : DwmStatus.Complete;
						DwmVirtualMachine.SetLastResult(num2, result);
						DwmHost.SetLastResult(num3, result);
					}
					finally
					{
						DwmVirtualMachine.SetStatus(num2, DwmStatus.None);
						DwmHost.SetStatus(num3, DwmStatus.None);
					}
				}
				else
				{
					this._result = 4013;
				}
			}
		}
		internal void HostPowerOffThreadProc()
		{
			int num = 0;
			int num2 = -1;
			StringBuilder stringBuilder = null;
			if (!string.IsNullOrEmpty(this._hostUuid) && this._poolId > 0)
			{
				ICollectorActions collector = DwmPool.GetCollector(this._poolId);
				if (collector != null)
				{
					int num3 = 0;
					try
					{
						num3 = DwmHost.UuidToId(this._hostUuid, this._poolId);
						DwmHost.SetStatus(num3, DwmStatus.OperationsPending);
						int auditRecordId = DwmAudit.AddAuditRecord(ref num, new int?(this._recommendationId), AuditEventType.HostPowerOff, null, new int?(num3), null, AuditEventStatus.InProgress, DateTime.UtcNow);
						DwmHost.SetPoweredOffByWlb(num3, true);
						Logger.Trace("HostPowerOffThreadProc:  Powering-off host {0}", new object[]
						{
							this._hostUuid
						});
						num2 = collector.PowerHostOff(this._hostUuid);
						Logger.Trace("HostPowerOffThreadProc:  Result of powering-off host is {0}", new object[]
						{
							num2
						});
						DwmAudit.UpdateAuditRecord(auditRecordId, (num2 != 0) ? AuditEventStatus.Failed : AuditEventStatus.Complete);
						DwmStatus result = (num2 != 0) ? DwmStatus.Failed : DwmStatus.Complete;
						DwmHost.SetLastResult(num3, result);
					}
					finally
					{
						if (num2 != 0)
						{
							DwmHost.SetPoweredOffByWlb(num3, false);
						}
						DwmHost.SetStatus(num3, DwmStatus.None);
						try
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							stringBuilder.AppendFormat("{0} : {1}", DwmHost.UuidToName(this._hostUuid, this._poolId), (num2 != 0) ? "Failure" : "Success");
							if (stringBuilder != null)
							{
								string category = "WLB_HOST_POWER_OFF";
								collector.SendMessage(category, stringBuilder.ToString());
							}
						}
						catch
						{
						}
					}
				}
				else
				{
					num2 = 4013;
				}
			}
		}
		internal void HostPowerOnThreadProc()
		{
			int num = 0;
			StringBuilder stringBuilder = null;
			this._result = 0;
			if (!string.IsNullOrEmpty(this._hostUuid) && this._poolId > 0)
			{
				ICollectorActions collector = DwmPool.GetCollector(this._poolId);
				if (collector != null)
				{
					int num2 = 0;
					try
					{
						num2 = DwmHost.UuidToId(this._hostUuid, this._poolId);
						DwmHost.SetStatus(num2, DwmStatus.OperationsPending);
						int auditRecordId = DwmAudit.AddAuditRecord(ref num, new int?(this._recommendationId), AuditEventType.HostPowerOn, null, new int?(num2), null, AuditEventStatus.InProgress, DateTime.UtcNow);
						Logger.Trace("HostPowerOnThreadProc:  Powering-on host {0}", new object[]
						{
							this._hostUuid
						});
						this._result = collector.PowerHostOn(this._hostUuid);
						Logger.Trace("HostPowerOnThreadProc:  Result of powering-on host is {0}", new object[]
						{
							this._result
						});
						DwmAudit.UpdateAuditRecord(auditRecordId, (this._result != 0) ? AuditEventStatus.Failed : AuditEventStatus.Complete);
						DwmStatus result = (this._result != 0) ? DwmStatus.Failed : DwmStatus.Complete;
						DwmHost.SetLastResult(num2, result);
						if (this._result != 0)
						{
							DwmHost.SetOtherConfig(num2, "ParticipatesInPowerManagement", "false");
						}
						DwmHost.SetOtherConfig(num2, "LastPowerOnSucceeded", (this._result != 0) ? "false" : "true");
					}
					finally
					{
						DwmHost.SetStatus(num2, DwmStatus.None);
						try
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							stringBuilder.AppendFormat("{0} : {1}", DwmHost.UuidToName(this._hostUuid, this._poolId), (this._result != 0) ? "Failure" : "Success");
							if (stringBuilder != null)
							{
								string category = "WLB_HOST_POWER_ON";
								collector.SendMessage(category, stringBuilder.ToString());
							}
						}
						catch
						{
						}
					}
				}
				else
				{
					this._result = 4013;
				}
			}
		}
		private void LockAll()
		{
			this.SetStatusAll(DwmStatus.OperationsPending);
		}
		private void UnLockAll()
		{
			this.SetStatusAll(DwmStatus.None);
		}
		private void SetStatusAll(DwmStatus status)
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				StringBuilder stringBuilder = new StringBuilder();
				int poolId = (this._migrations[0].PoolId <= 0) ? DwmPoolBase.UuidToId(this._migrations[0].PoolUuid) : this._migrations[0].PoolId;
				stringBuilder.AppendFormat("select * from set_pool_status_by_host_id({0}, {1});\n", (this._migrations[0].MoveToHostId <= 0) ? DwmHost.UuidToId(this._migrations[0].MoveToHostUuid, poolId) : this._migrations[0].MoveToHostId, (int)status);
				for (int i = 0; i < this._migrations.Count; i++)
				{
					stringBuilder.AppendFormat("select * from set_host_status ({0}, {1});\n", (this._migrations[i].MoveFromHostId <= 0) ? DwmHost.UuidToId(this._migrations[i].MoveFromHostUuid, poolId) : this._migrations[i].MoveFromHostId, (int)status);
					stringBuilder.AppendFormat("select * from  set_host_status ({0}, {1});\n", (this._migrations[i].MoveToHostId <= 0) ? DwmHost.UuidToId(this._migrations[i].MoveToHostUuid, poolId) : this._migrations[i].MoveToHostId, (int)status);
					stringBuilder.AppendFormat("select * from set_vm_status ({0}, {1});\n", (this._migrations[i].VmId <= 0) ? DwmVirtualMachine.UuidToId(this._migrations[i].VmUuid, poolId) : this._migrations[i].VmId, (int)status);
				}
				DwmBase.WriteData(dBAccess, stringBuilder);
			}
		}
	}
}
