using Halsign.DWM.Communication;
using Halsign.DWM.Framework;
using System;
using System.ServiceProcess;
namespace DwmWebSvc
{
	internal static class Program
	{
		private static void Main(string[] arguments)
		{
			Environment.SetEnvironmentVariable("ProcName", "DwmWebSvc", EnvironmentVariableTarget.Process);
			CmdLineArgs cmdLineArgs = new CmdLineArgs(arguments);
			if (!cmdLineArgs.AreArgsValid)
			{
				cmdLineArgs.ShowUsage();
				return;
			}
			DBAccess.PollUntilDbAcceptsConnection();
			if (cmdLineArgs.IsDebug)
			{
				Logger.ConsoleLogEnabled = true;
				WorkloadBalanceHost workloadBalanceHost = new WorkloadBalanceHost();
				workloadBalanceHost.Start(true);
			}
			else
			{
				DwmWebSvc service = new DwmWebSvc();
				ServiceBase.Run(service);
			}
		}
	}
}
