using Halsign.DWM.Communication2;
using Halsign.DWM.Framework;
using System;
using System.ServiceProcess;
using System.Threading;
namespace Halsign.DWM.DwmRestWebSvc
{
	internal class Program
	{
		private static void Main(string[] arguments)
		{
			Environment.SetEnvironmentVariable("ProcName", "DwmRestWebSvc", EnvironmentVariableTarget.Process);
			CmdLineArgs cmdLineArgs = new CmdLineArgs(arguments);
			if (!cmdLineArgs.AreArgsValid)
			{
				cmdLineArgs.ShowUsage();
				return;
			}
			int valueAsInt = Configuration.GetValueAsInt(ConfigItems.RestHttpPort);
			string text = string.Format("http://*:{0}/", valueAsInt);
			RestWebServiceHost appHost = new RestWebServiceHost();
			DBAccess.PollUntilDbAcceptsConnection();
			if (cmdLineArgs.IsDebug)
			{
				Logger.ConsoleLogEnabled = true;
				Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
				{
					Logger.Trace("Cancel key pressed, stopping sever.");
					if (appHost != null)
					{
						appHost.Stop();
					}
				};
				appHost.Init();
				appHost.Start(text);
				Console.WriteLine("Press <CTRL>+C to stop.");
				Thread.Sleep(-1);
			}
			else
			{
				DwmRestWebSvc service = new DwmRestWebSvc(appHost, text);
				ServiceBase.Run(service);
			}
		}
	}
}
