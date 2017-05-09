using Halsign.DWM.Domain;
using System;
using System.Collections;
using System.Threading;
namespace Halsign.DWM.Framework
{
	public class QueueWorker
	{
		private const int StopEvent = 0;
		private const int NewDataEvent = 1;
		private const int SqlDownQueueSize = 500;
		private bool _isRunning;
		private WaitHandle[] _waitHandles = new WaitHandle[2];
		private Thread _workerThread;
		private int _pollInterval = 10000;
		private Queue _sqlDownQueue;
		public void Start()
		{
			if (!this._isRunning)
			{
				this._waitHandles[0] = new AutoResetEvent(false);
				this._waitHandles[1] = QueueManager.NewDataEvent;
				this._workerThread = new Thread(new ThreadStart(this.ThreadProc));
				this._workerThread.Name = "QueueWorkerThread";
				this._workerThread.Start();
				this._isRunning = true;
			}
		}
		public void Stop()
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
			while (!flag)
			{
				int num = WaitHandle.WaitAny(this._waitHandles, this._pollInterval, false);
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						if (num2 == 258)
						{
							try
							{
								if (QueueManager.Count > 0)
								{
									this.DequeueAndSave(10);
								}
							}
							catch (Exception ex)
							{
								Logger.LogError("An error was encountered in the WaitTimeout process of the data Queueworker thread. Some metric data may be lost.", new object[0]);
								Logger.LogException(ex);
							}
						}
					}
					else
					{
						try
						{
							this.DequeueAndSave(QueueManager.Count);
						}
						catch (Exception ex2)
						{
							Logger.LogError("An error was encountered in the NewDataEvent of the data Queueworker thread. Some metric data may be lost.", new object[0]);
							Logger.LogException(ex2);
						}
					}
				}
				else
				{
					try
					{
						if (this._sqlDownQueue != null)
						{
							this._sqlDownQueue.Clear();
							this._sqlDownQueue = null;
						}
					}
					catch (Exception ex3)
					{
						Logger.LogError("An error was encountered in the StopEvent of the data Queueworker thread.  Some metric data may be lost.", new object[0]);
						Logger.LogException(ex3);
					}
					finally
					{
						flag = true;
					}
				}
			}
		}
		private void DequeueAndSave(int numItemsToDequeue)
		{
			QueueWorker.Trace("Queue Worker:  Dequeuing and Saving {0} of {1} items", new object[]
			{
				numItemsToDequeue,
				QueueManager.Count
			});
			this.DrainSqlDownQueue();
			for (int i = 0; i < numItemsToDequeue; i++)
			{
				if (!this._isRunning)
				{
					break;
				}
				object obj = QueueManager.Dequeue();
				if (obj == null)
				{
					break;
				}
				if (!this.SaveItem(obj))
				{
					this.AddItemToSqlDownQueue(obj);
				}
			}
		}
		private void DrainSqlDownQueue()
		{
			if (this._sqlDownQueue != null && this._isRunning)
			{
				QueueWorker.Trace("Attempting to drain {0} items from the backup queue...", new object[]
				{
					this._sqlDownQueue.Count
				});
				for (int i = 0; i < this._sqlDownQueue.Count; i++)
				{
					if (!this._isRunning)
					{
						break;
					}
					object o = this._sqlDownQueue.Peek();
					if (!this.SaveItem(o))
					{
						break;
					}
					o = this._sqlDownQueue.Dequeue();
				}
				if (this._sqlDownQueue.Count == 0)
				{
					this._sqlDownQueue = null;
				}
			}
		}
		private void AddItemToSqlDownQueue(object o)
		{
			if (this._sqlDownQueue == null)
			{
				this._sqlDownQueue = new Queue(500);
			}
			if (this._sqlDownQueue.Count >= 500)
			{
				this._sqlDownQueue.Dequeue();
			}
			this._sqlDownQueue.Enqueue(o);
		}
		private bool SaveItem(object o)
		{
			bool result = false;
			try
			{
				using (DBAccess2 dBAccess = new DBAccess2())
				{
					if (o.GetType() == typeof(DwmHostMetricCollection))
					{
						QueueWorker.Trace("Queue Worker:  Saving DwmHostMetricCollection instance", new object[0]);
						((DwmHostMetricCollection)o).Save(dBAccess);
						result = true;
					}
					else
					{
						if (o.GetType() == typeof(DwmVmMetricCollection))
						{
							QueueWorker.Trace("Queue Worker:  Saving DwmVmMetricCollection instance", new object[0]);
							((DwmVmMetricCollection)o).Save(dBAccess);
							result = true;
						}
					}
				}
			}
			catch (DwmNpgsqlException ex)
			{
				Logger.LogException(ex);
				if (ex.Severity == DwmNpgsqlException.PgSeverity.Fatal || ex.Severity == DwmNpgsqlException.PgSeverity.Panic || Localization.Compare(ex.Message, "The Connection is broken.", true) == 0)
				{
					result = false;
				}
			}
			catch (InvalidOperationException ex2)
			{
				Logger.LogException(ex2);
				result = false;
			}
			catch (Exception ex3)
			{
				Logger.LogException(ex3);
				result = true;
			}
			return result;
		}
		private static void Trace(string fmt, params object[] args)
		{
			if (Configuration.GetValueAsBool(ConfigItems.QueueManagementTrace))
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
