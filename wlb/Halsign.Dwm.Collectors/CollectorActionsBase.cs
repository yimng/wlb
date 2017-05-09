using Halsign.DWM.Framework;
using System;
namespace Halsign.DWM.Collectors
{
	public abstract class CollectorActionsBase : CollectorBase, ICollectorActions
	{
		public void Start()
		{
		}
		public void Stop()
		{
		}
		public virtual string GetPoolName()
		{
			if (this.IsInitialized)
			{
				return string.Empty;
			}
			throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the GetPoolName method", DwmErrorCode.NotInitialized, null);
		}
		public virtual string GetPoolUniqueIdentifier()
		{
			if (this.IsInitialized)
			{
				return string.Empty;
			}
			throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the GetPoolUniqueIdentifier method", DwmErrorCode.NotInitialized, null);
		}
		public virtual void GetCerts()
		{
		}
		public virtual CantBootReason CanStartVM(string vmUuid, string hostUuid)
		{
			return CantBootReason.None;
		}
		public virtual int StartVM(string vmUuid, string hostUuid, bool startPaused)
		{
			return 4006;
		}
		public virtual int MigrateVM(string vmUuid, string migrateToHostUuid, int recommendationId, bool liveMigration)
		{
			return 4006;
		}
		public virtual void SendMessage(string category, string message)
		{
		}
		public int PowerHostOff(string hostUuid)
		{
			return 4006;
		}
		public int PowerHostOn(string hostUuid)
		{
			return 4006;
		}
	}
}
