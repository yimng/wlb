using System;
namespace Halsign.DWM.Collectors
{
	public interface ICollector
	{
		bool IsAlive
		{
			get;
		}
		void Initialize(string hostname, int port, string protocol, string username, string password, int poolId);
		void StartCollection();
		void StopCollection();
		void Discover();
	}
}
