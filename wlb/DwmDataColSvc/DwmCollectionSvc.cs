using Halsign.DWM.Framework;
using System;
using System.Reflection;
using System.ServiceProcess;
namespace Halsign.Dwm.DataColSvc
{
	public class DwmCollectionSvc : ServiceBase
	{
		private DataCollectionManager _collectionMgr;
		private QueueWorker _queueWorker;
		public DwmCollectionSvc()
		{
			base.ServiceName = "DwmCollectionSvc";
			base.AutoLog = false;
		}
		protected override void OnStart(string[] args)
		{
			try
			{
				Logger.Trace("{0}", new object[]
				{
					Assembly.GetExecutingAssembly().FullName
				});
				Logger.Trace("Starting on {0} at {1}", new object[]
				{
					Environment.MachineName,
					DateTime.Now
				});
				this._collectionMgr = new DataCollectionManager();
				this._collectionMgr.Start();
				Logger.Trace("Started data collection.");
				this._queueWorker = new QueueWorker();
				this._queueWorker.Start();
				Logger.Trace("Started queue workers.");
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
			}
		}
		protected override void OnStop()
		{
			try
			{
				this._collectionMgr.Stop();
				Logger.Trace("Stopped data collection.");
				this._queueWorker.Stop();
				Logger.Trace("Stopped queue workers.");
				this._collectionMgr = null;
				this._queueWorker = null;
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
			}
		}
		protected override void OnShutdown()
		{
			Logger.Trace("OnShutdown called.  Stopping the service...");
			this.OnStop();
			base.OnShutdown();
		}
	}
}
