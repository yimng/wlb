using Halsign.DWM.Framework;
using System;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmAnalysisEngine : DwmAEBase
	{
		private const int StopEvent = 0;
		private bool _isRunning;
		private Thread _thread;
		private int _pollInterval = 300000;
		private WaitHandle[] _waitHandles = new WaitHandle[1];
		public virtual void Start()
		{
			if (!this._isRunning)
			{
				this._waitHandles[0] = new AutoResetEvent(false);
				this._thread = new Thread(new ThreadStart(this.ThreadProc));
				this._thread.Name = "WlbAnalysisEngineThread";
				this._thread.Start();
				this._isRunning = true;
			}
		}
		public virtual void Stop()
		{
			if (this._isRunning)
			{
				((AutoResetEvent)this._waitHandles[0]).Set();
				this._isRunning = false;
			}
		}
		private void ThreadProc()
		{
			bool flag = false;
			try
			{
				this._pollInterval = Configuration.GetValueAsInt(ConfigItems.AnalysisEnginePollInterval);
				if (this._pollInterval > 0)
				{
					Logger.Trace("Analysis Engine poll interval set to {0} seconds", new object[]
					{
						this._pollInterval
					});
					this._pollInterval *= 1000;
				}
			}
			catch (DwmException ex)
			{
				Logger.LogException(ex);
			}
			while (!flag)
			{
				int num = WaitHandle.WaitAny(this._waitHandles, this._pollInterval, false);
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 == 258)
					{
						this.OnInterval();
					}
				}
				else
				{
					flag = true;
				}
			}
			DwmPool.UnloadCache();
		}
		private void OnInterval()
		{
			DwmAEAnalyzer dwmAEAnalyzer = new DwmAEAnalyzer();
			dwmAEAnalyzer.Analyze();
		}
	}
}
