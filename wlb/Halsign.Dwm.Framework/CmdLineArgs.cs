using System;
namespace Halsign.DWM.Framework
{
	public class CmdLineArgs
	{
		public bool IsDebug
		{
			get;
			private set;
		}
		public string ProgramName
		{
			get;
			private set;
		}
		public bool AreArgsValid
		{
			get;
			private set;
		}
		public CmdLineArgs(string[] args)
		{
			this.ProgramName = AppDomain.CurrentDomain.FriendlyName;
			this.AreArgsValid = true;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == "--debug")
				{
					this.IsDebug = true;
				}
				else
				{
					this.AreArgsValid = false;
				}
			}
		}
		public void ShowUsage()
		{
			Console.WriteLine("Usage is: {0} [OPTION]", this.ProgramName);
			Console.WriteLine("Options:");
			Console.WriteLine("\t--debug\t\tRun in console mode instead of running as a service.");
		}
	}
}
