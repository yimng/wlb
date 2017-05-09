using Halsign.DWM.Domain;
using System;
namespace DwmAnalEngHost
{
	internal class Program
	{
		private static DwmAnalysisEngine _analysisEngine;
		private static void Main(string[] args)
		{
			Environment.SetEnvironmentVariable("ProcName", "DwmAnalEngHost", EnvironmentVariableTarget.Process);
			Program._analysisEngine = new DwmAnalysisEngine();
			Program._analysisEngine.Start();
		}
	}
}
