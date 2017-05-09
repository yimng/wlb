using System;
namespace Halsign.DWM.Collectors
{
	public interface ICollectorActions
	{
		void Initialize(string hostname, int port, string protocol, string username, string password, int poolId);
		void UnInitialize();
		void Start();
		void Stop();
		string GetPoolName();
		string GetPoolUniqueIdentifier();
		void GetCerts();
		CantBootReason CanStartVM(string vmUuid, string hostUuid);
		int StartVM(string vmUuid, string hostUuid, bool startPaused);
		int MigrateVM(string vmUuid, string migrateToHostUuid, int recommendationId, bool liveMigration);
		void SendMessage(string category, string message);
		int PowerHostOff(string hostUuid);
		int PowerHostOn(string hostUuid);
	}
}
