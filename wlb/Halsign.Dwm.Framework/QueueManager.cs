using System;
using System.Collections;
using System.Threading;
namespace Halsign.DWM.Framework
{
	public sealed class QueueManager
	{
		private static object _lock = new object();
		private static Queue _queue = new Queue();
		private static AutoResetEvent _newDataEvent = new AutoResetEvent(false);
		public static AutoResetEvent NewDataEvent
		{
			get
			{
				return QueueManager._newDataEvent;
			}
		}
		public static int Count
		{
			get
			{
				object @lock = QueueManager._lock;
				Monitor.Enter(@lock);
				int count;
				try
				{
					count = QueueManager._queue.Count;
				}
				finally
				{
					Monitor.Exit(@lock);
				}
				return count;
			}
		}
		private QueueManager()
		{
		}
		public static void Enqueue(object item)
		{
			object @lock = QueueManager._lock;
			Monitor.Enter(@lock);
			try
			{
				QueueManager._queue.Enqueue(item);
				QueueManager._newDataEvent.Set();
			}
			finally
			{
				Monitor.Exit(@lock);
			}
		}
		public static object Dequeue()
		{
			object result = null;
			object @lock = QueueManager._lock;
			Monitor.Enter(@lock);
			try
			{
				if (QueueManager._queue.Count > 0)
				{
					result = QueueManager._queue.Dequeue();
				}
			}
			finally
			{
				Monitor.Exit(@lock);
			}
			return result;
		}
		public static void Flush()
		{
			object @lock = QueueManager._lock;
			Monitor.Enter(@lock);
			try
			{
				QueueManager._queue.Clear();
			}
			finally
			{
				Monitor.Exit(@lock);
			}
		}
	}
}
