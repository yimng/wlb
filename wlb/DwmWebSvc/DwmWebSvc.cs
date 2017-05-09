using Halsign.DWM.Communication;
using Halsign.DWM.Framework;
using System;
using System.Reflection;
using System.ServiceProcess;
namespace DwmWebSvc
{
	public class DwmWebSvc : ServiceBase
	{
		private WorkloadBalanceHost _wlbHost;
		public DwmWebSvc()
		{
			base.ServiceName = "DwmWebSvc";
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
				this._wlbHost = new WorkloadBalanceHost();
				this._wlbHost.Start(true);
				Logger.Trace("Started WCF hosts.");
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
				Logger.Trace("OnStop called.  Stopping the service.");
				this._wlbHost.Stop();
				this._wlbHost = null;
				Logger.Trace("Stopped.");
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
			}
		}
		protected override void OnShutdown()
		{
			Logger.Trace("OnShutdown called.  Stopping the service.");
			this.OnStop();
			base.OnShutdown();
		}
	}
}
