using Halsign.DWM.Framework;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Reflection;
using System.ServiceProcess;
namespace Halsign.DWM.DwmRestWebSvc
{
	public class DwmRestWebSvc : ServiceBase
	{
		private AppHostHttpListenerBase _appHost;
		private string _listenUrl;
		public DwmRestWebSvc(AppHostHttpListenerBase appHost, string listenUrl)
		{
			base.ServiceName = "DwmRestWebSvc";
			base.AutoLog = false;
			this._listenUrl = listenUrl;
			this._appHost = appHost;
			this._appHost.Init();
		}
		protected override void OnStart(string[] args)
		{
			try
			{
				Logger.Trace("{0}", new object[]
				{
					Assembly.GetExecutingAssembly().FullName
				});
				this._appHost.Start(this._listenUrl);
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
				this._appHost.Stop();
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
			this._appHost.Stop();
			base.OnShutdown();
		}
	}
}
