using Halsign.DWM.Framework;
using System;
using System.Reflection;
using System.ServiceProcess;
namespace Halsign.Dwm.DataColSvc
{
	internal static class Program
	{
		private static void Main(string[] arguments)
		{
			Environment.SetEnvironmentVariable("ProcName", "DwmDataColSvc", EnvironmentVariableTarget.Process);
			CmdLineArgs cmdLineArgs = new CmdLineArgs(arguments);
			if (!cmdLineArgs.AreArgsValid)
			{
				cmdLineArgs.ShowUsage();
				return;
			}
			Logger.Trace("Polling to check if the database is accepting connections...");
			DBAccess.PollUntilDbAcceptsConnection();
			if (cmdLineArgs.IsDebug)
			{
				Logger.ConsoleLogEnabled = true;
				try
				{
					Logger.Trace(string.Empty);
					Logger.Trace(Assembly.GetExecutingAssembly().FullName);
					Logger.Trace(string.Empty);
					Logger.Trace("Starting on {0} at {1}", new object[]
					{
						Environment.MachineName,
						DateTime.Now
					});
					DataCollectionManager dataCollectionManager = new DataCollectionManager();
					dataCollectionManager.Start();
					Logger.Trace("Started data collection.");
					QueueWorker queueWorker = new QueueWorker();
					queueWorker.Start();
					Logger.Trace("Started queue workers.");
				}
				catch (Exception ex)
				{
					Logger.LogException(ex);
				}
			}
			else
			{
				DwmCollectionSvc service = new DwmCollectionSvc();
				ServiceBase.Run(service);
			}
		}
	}
}
