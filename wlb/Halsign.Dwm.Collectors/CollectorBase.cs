using Halsign.DWM.Framework;
using System;
using System.Diagnostics;
using System.Threading;
namespace Halsign.DWM.Collectors
{
	public abstract class CollectorBase
	{
		protected const int StopEvent = 0;
		protected string _hostName;
		protected int _hostPort;
		protected string _protocol = "http";
		protected string _username;
		protected string _password;
		protected int _poolId;
		protected bool _isInitialized;
		protected bool _collectionIsRunning;
		protected Thread _collectorThread;
		protected int _pollInterval = 5000;
		protected int _noLicensePollInterval = 600000;
		protected WaitHandle[] _waitHandles = new WaitHandle[1];
		private PerformanceCounter _hostMetricsCounter;
		private PerformanceCounter _vmMetricsCounter;
		protected virtual bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
			set
			{
				this._isInitialized = value;
			}
		}
		public bool IsAlive
		{
			get
			{
				bool result = true;
				if (this._collectionIsRunning)
				{
					result = this._collectorThread.IsAlive;
				}
				return result;
			}
		}
		protected PerformanceCounter HostMetricsCounter
		{
			get
			{
				if (this._hostMetricsCounter == null)
				{
					try
					{
						this._hostMetricsCounter = new PerformanceCounter("DWM Data Collection", "Host Metrics Collected/sec", this._hostName + " pool", false);
						this._hostMetricsCounter.MachineName = ".";
						this._hostMetricsCounter.RawValue = 0L;
					}
					catch (InvalidOperationException)
					{
					}
				}
				return this._hostMetricsCounter;
			}
		}
		protected PerformanceCounter VmMetricsCounter
		{
			get
			{
				if (this._vmMetricsCounter == null)
				{
					try
					{
						this._vmMetricsCounter = new PerformanceCounter("DWM Data Collection", "VM Metrics Collected/sec", this._hostName + " pool", false);
						this._vmMetricsCounter.MachineName = ".";
						this._vmMetricsCounter.RawValue = 0L;
					}
					catch (InvalidOperationException)
					{
					}
				}
				return this._vmMetricsCounter;
			}
		}
		public virtual void Initialize(string hostname, int port, string protocol, string username, string password, int poolId)
		{
			this._hostName = hostname;
			this._hostPort = port;
			this._protocol = protocol;
			this._username = username;
			this._password = password;
			this._poolId = poolId;
			this.IsInitialized = true;
		}
		public virtual void UnInitialize()
		{
			this._hostName = null;
			this._hostPort = 0;
			this._protocol = null;
			this._username = null;
			this._password = null;
			this._poolId = 0;
			this.IsInitialized = false;
		}
		public virtual void StartCollection()
		{
			if (this.IsInitialized)
			{
				if (this._collectionIsRunning)
				{
					if (this._collectorThread.IsAlive)
					{
						return;
					}
				}
				try
				{
					this._pollInterval = Configuration.GetValueAsInt(ConfigItems.DataCollectionPollInterval);
					if (this._pollInterval > 0)
					{
						Logger.Trace("Data collection poll interval set to {0} seconds", new object[]
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
				this._waitHandles[0] = new AutoResetEvent(false);
				this._collectorThread = new Thread(new ThreadStart(this.ThreadProc));
				this._collectorThread.Start();
				this._collectionIsRunning = true;
				return;
			}
			throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the StartCollection method", DwmErrorCode.NotInitialized, null);
		}
		public virtual void StopCollection()
		{
			if (this._collectionIsRunning)
			{
				((AutoResetEvent)this._waitHandles[0]).Set();
				this._collectionIsRunning = false;
			}
		}
		protected abstract void ThreadProc();
		protected static void IncrementPerfCounter(PerformanceCounter counter)
		{
			if (counter != null)
			{
				counter.Increment();
			}
		}
	}
}
