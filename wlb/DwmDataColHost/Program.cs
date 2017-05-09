using Halsign.DWM.Framework;
using System;
using System.Reflection;
namespace DwmDataColHost
{
	internal class Program
	{
		private static DataCollectionManager _collectionMgr;
		private static QueueWorker _queueWorker;
		private static void Main(string[] args)
		{
			Environment.SetEnvironmentVariable("ProcName", "DwmDataColHost", EnvironmentVariableTarget.Process);
			try
			{
				Logger.Trace(string.Empty);
				Logger.Trace("DwmCollectionSvc:  {0}", new object[]
				{
					Assembly.GetExecutingAssembly().FullName
				});
				Logger.Trace(string.Empty);
				Logger.Trace("DwmCollectionSvc:  starting on {0} at {1}", new object[]
				{
					Environment.MachineName,
					DateTime.Now
				});
				Program._collectionMgr = new DataCollectionManager();
				Program._collectionMgr.Start();
				Logger.Trace("DwmCollectionSvc:  started data collection.");
				Program._queueWorker = new QueueWorker();
				Program._queueWorker.Start();
				Logger.Trace("DwmCollectionSvc:  started queue workers.");
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
			}
		}
	}
}
