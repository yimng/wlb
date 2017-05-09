using Halsign.DWM.Domain;
using Halsign.DWM.Framework;
using System;
using System.Reflection;
using System.ServiceProcess;
namespace Halsign.Dwm.AnalEngSvc
{
	internal class DwmAnalysisEngineSvc : ServiceBase
	{
		private DwmAnalysisEngine _analysisEngine;
		public DwmAnalysisEngineSvc()
		{
			base.ServiceName = "DwmAnalysisEngineSvc";
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
				this._analysisEngine = new DwmAnalysisEngine();
				this._analysisEngine.Start();
				Logger.Trace("Started the analysis engine.");
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
				this._analysisEngine.Stop();
				this._analysisEngine = null;
				Logger.Trace("Stopped the analysis engine.");
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
