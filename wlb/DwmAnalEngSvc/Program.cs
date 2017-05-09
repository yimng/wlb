using Halsign.DWM.Domain;
using Halsign.DWM.Framework;
using System;
using System.ServiceProcess;
namespace Halsign.Dwm.AnalEngSvc
{
	internal static class Program
	{
		private static void Main(string[] arguments)
		{
			Environment.SetEnvironmentVariable("ProcName", "DwmAnalEngSvc", EnvironmentVariableTarget.Process);
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
				DwmAnalysisEngine dwmAnalysisEngine = new DwmAnalysisEngine();
				dwmAnalysisEngine.Start();
			}
			else
			{
				DwmAnalysisEngineSvc service = new DwmAnalysisEngineSvc();
				ServiceBase.Run(service);
			}
		}
	}
}
