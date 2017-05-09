using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class WlbSetOptModeTaskProcessor : ITaskProcessor
	{
		private const int _newDataEventIndex = 0;
		private static Thread _workThread;
		private static object _lock;
		private static object _threadLock;
		private static Queue<KeyValuePair<int, Dictionary<string, string>>> _queue;
		private static AutoResetEvent _newDataEvent;
		private static WaitHandle[] _waitHandles;
		private static int _pollInterval;
		private static bool _traceEnabled;
		public static AutoResetEvent NewDataEvent
		{
			get
			{
				return WlbSetOptModeTaskProcessor._newDataEvent;
			}
		}
		static WlbSetOptModeTaskProcessor()
		{
			WlbSetOptModeTaskProcessor._lock = new object();
			WlbSetOptModeTaskProcessor._threadLock = new object();
			WlbSetOptModeTaskProcessor._queue = new Queue<KeyValuePair<int, Dictionary<string, string>>>();
			WlbSetOptModeTaskProcessor._newDataEvent = new AutoResetEvent(false);
			WlbSetOptModeTaskProcessor._waitHandles = new WaitHandle[1];
			WlbSetOptModeTaskProcessor._pollInterval = 60000;
			WlbSetOptModeTaskProcessor._traceEnabled = false;
			WlbSetOptModeTaskProcessor._waitHandles[0] = WlbSetOptModeTaskProcessor.NewDataEvent;
		}
		public void Execute(int TaskId, Dictionary<string, string> Parameters)
		{
			KeyValuePair<int, Dictionary<string, string>> item = new KeyValuePair<int, Dictionary<string, string>>(TaskId, Parameters);
			try
			{
				Monitor.Enter(WlbSetOptModeTaskProcessor._threadLock);
				object @lock = WlbSetOptModeTaskProcessor._lock;
				Monitor.Enter(@lock);
				try
				{
					WlbSetOptModeTaskProcessor._queue.Enqueue(item);
					WlbSetOptModeTaskProcessor._newDataEvent.Set();
				}
				finally
				{
					Monitor.Exit(@lock);
				}
				if (WlbSetOptModeTaskProcessor._workThread == null)
				{
					ThreadStart start = new ThreadStart(this.ThreadProc);
					WlbSetOptModeTaskProcessor._workThread = new Thread(start);
					WlbSetOptModeTaskProcessor._workThread.Name = "WlbSetOptModeTaskProcessorThread";
					WlbSetOptModeTaskProcessor._workThread.Start();
				}
			}
			finally
			{
				Monitor.Exit(WlbSetOptModeTaskProcessor._threadLock);
			}
		}
		private void ThreadProc()
		{
			bool flag = false;
			while (!flag)
			{
				int num = WaitHandle.WaitAny(WlbSetOptModeTaskProcessor._waitHandles, WlbSetOptModeTaskProcessor._pollInterval, false);
				WlbSetOptModeTaskProcessor._traceEnabled = Configuration.GetValueAsBool(ConfigItems.ScheduledTaskTrace);
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 == 258)
					{
						if (Monitor.TryEnter(WlbSetOptModeTaskProcessor._threadLock))
						{
							flag = true;
							WlbSetOptModeTaskProcessor._workThread = null;
							Monitor.Exit(WlbSetOptModeTaskProcessor._threadLock);
						}
					}
				}
				else
				{
					this.DoTheWork();
				}
			}
		}
		private static bool GetNextQueuedTask(out KeyValuePair<int, Dictionary<string, string>> taskInformation)
		{
			object @lock = WlbSetOptModeTaskProcessor._lock;
			Monitor.Enter(@lock);
			try
			{
				if (WlbSetOptModeTaskProcessor._queue.Count > 0)
				{
					taskInformation = WlbSetOptModeTaskProcessor._queue.Dequeue();
				}
				else
				{
					taskInformation = default(KeyValuePair<int, Dictionary<string, string>>);
				}
			}
			finally
			{
				Monitor.Exit(@lock);
			}
			return 0 != taskInformation.Key;
		}
		private void DoTheWork()
		{
			bool success = false;
			KeyValuePair<int, Dictionary<string, string>> keyValuePair;
			while (WlbSetOptModeTaskProcessor.GetNextQueuedTask(out keyValuePair))
			{
				try
				{
					string text = string.Empty;
					text = keyValuePair.Value["PoolUUID"];
					OptimizationMode optimizationMode = (OptimizationMode)int.Parse(keyValuePair.Value["OptMode"]);
					DwmPool dwmPool = DwmPool.Load(text);
					if (dwmPool != null)
					{
						bool flag = false;
						bool.TryParse(dwmPool.GetOtherConfigItem("EnableOptimizationModeSchedules"), out flag);
						if (flag)
						{
							WlbScheduleProcessor.Trace("Attempting to change the optimization mode for pool {0} from {1} to {2}.", new object[]
							{
								dwmPool.Name,
								dwmPool.OptMode.ToString(),
								optimizationMode.ToString()
							});
							dwmPool.OptMode = optimizationMode;
							dwmPool.Save();
							WlbScheduleProcessor.Trace("Successfully set the optimization mode for pool {0} to {1}.", new object[]
							{
								dwmPool.Name,
								optimizationMode.ToString()
							});
							success = true;
						}
						else
						{
							WlbScheduleProcessor.Trace("Optimization mode tasks for pool {0} are disabled.  Optimization mode not set.", text);
						}
					}
					else
					{
						WlbScheduleProcessor.Trace("Unable to load pool with uuid of {0}.  Optimization mode not set.", text);
					}
				}
				catch (Exception ex)
				{
					Logger.LogError("Error setting Optimization Mode on schedule: {0}", new object[]
					{
						ex.Message
					});
					Logger.LogException(ex);
				}
				finally
				{
					try
					{
						WlbTask.SetLastRun(keyValuePair.Key, success);
					}
					catch (Exception ex2)
					{
						WlbScheduleProcessor.Trace("Error setting the Last Run Result: {0}", ex2.Message);
					}
				}
			}
		}
	}
}
