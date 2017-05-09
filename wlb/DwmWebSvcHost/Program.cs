using Halsign.DWM.Communication;
using System;
namespace DwmWebSvcHost
{
	internal class Program
	{
		private static WorkloadBalanceHost host;
		private static void Main(string[] args)
		{
			Environment.SetEnvironmentVariable("ProcName", "DwmWebSvcHost", EnvironmentVariableTarget.Process);
			Program.host = new WorkloadBalanceHost();
			Program.host.Start(true);
		}
	}
}
