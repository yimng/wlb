using Halsign.DWM.Framework;
using System;
using System.Reflection;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class WlbScheduleProcessor
	{
		private const int StopEvent = 0;
		private bool _isRunning;
		private Thread _thread;
		private int _pollInterval = 60000;
		private WaitHandle[] _waitHandles = new WaitHandle[1];
		private static bool _traceEnabled;
		public virtual void Start()
		{
			Logger.Trace("Starting WlbScheduleProcessor...");
			if (!this._isRunning)
			{
				this._waitHandles[0] = new AutoResetEvent(false);
				this._thread = new Thread(new ThreadStart(this.ThreadProc));
				this._thread.Name = "WlbScheduleProcessorThread";
				this._thread.Start();
				this._isRunning = true;
			}
			Logger.Trace("WlbScheduleProcessor started.");
		}
		public virtual void Stop()
		{
			Logger.Trace("Stopping WlbScheduleProcessor...");
			if (this._isRunning)
			{
				((AutoResetEvent)this._waitHandles[0]).Set();
				this._isRunning = false;
			}
			Logger.Trace("WlbScheduleProcessor stopped.");
		}
		private void ThreadProc()
		{
			bool flag = false;
			while (!flag)
			{
				int num = WaitHandle.WaitAny(this._waitHandles, this._pollInterval, false);
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 == 258)
					{
						WlbScheduleProcessor.ProcessSchedules();
					}
				}
				else
				{
					flag = true;
				}
			}
		}
		public static void ProcessSchedules()
		{
			WlbScheduleProcessor._traceEnabled = Configuration.GetValueAsBool(ConfigItems.ScheduledTaskTrace);
			WlbScheduleProcessor.Trace("Checking for scheduled tasks...");
			try
			{
				WlbTaskCollection wlbTaskCollection = new WlbTaskCollection();
				wlbTaskCollection.LoadPending();
				WlbScheduleProcessor.Trace("Found {0} tasks to execute.", wlbTaskCollection.Count);
				foreach (WlbTask current in wlbTaskCollection)
				{
					try
					{
						string assembly = current.Trigger.Action.Assembly;
						string @class = current.Trigger.Action.Class;
						WlbScheduleProcessor.Trace("Executing taskid {0}", current.Id);
						Assembly assembly2 = Assembly.Load(new AssemblyName(assembly));
						ITaskProcessor taskProcessor = assembly2.CreateInstance(@class, true) as ITaskProcessor;
						taskProcessor.Execute(current.Id, current.Trigger.Action.Parameters);
					}
					catch (Exception ex)
					{
						Logger.Trace("Caught exception while executing taskid {0}: {1}", new object[]
						{
							current.Id,
							ex.Message
						});
					}
				}
			}
			catch (Exception ex2)
			{
				Logger.Trace("Caught exception while processing scheduled tasks: {0}", new object[]
				{
					ex2.Message
				});
			}
		}
		internal static void Trace(string msg)
		{
			if (WlbScheduleProcessor._traceEnabled)
			{
				Logger.Trace(msg);
			}
		}
		internal static void Trace(string fmt, object arg)
		{
			if (WlbScheduleProcessor._traceEnabled)
			{
				Logger.Trace(fmt, new object[]
				{
					arg
				});
			}
		}
		internal static void Trace(string fmt, params object[] args)
		{
			if (WlbScheduleProcessor._traceEnabled)
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
