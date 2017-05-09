// Decompiled with JetBrains decompiler
// Type: Citrix.DWM.Collectors.CollectorBase
// Assembly: Citrix.Dwm.Collectors, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0844E477-F94E-4593-A883-69DEC5AD079C
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\Citrix.Dwm.Collectors.dll

using Citrix.DWM.Framework;
using System;
using System.Diagnostics;
using System.Threading;

namespace Citrix.DWM.Collectors
{
  /// <summary>
  /// Abstract base class for DWM data collectors.
  /// 
  /// </summary>
  public abstract class CollectorBase
  {
    /// <summary>
    /// The protocol(http/https) that should be used when communicating
    ///             with the hosts in the pool.
    /// 
    /// </summary>
    protected string _protocol = "http";
    /// <summary>
    /// Interval at which the data collection thread will poll the
    ///             hypervisor for performance data.
    /// 
    /// </summary>
    protected int _pollInterval = 5000;
    /// <summary>
    /// Interval at which the data collection thread will discover the
    ///             contents of a hypervisor pool when the pool does not have a valid
    ///             license.  The data collector will effectively look for a valid
    ///             license at this interval.
    /// 
    /// </summary>
    protected int _noLicensePollInterval = 600000;
    /// <summary>
    /// Array of WaitHandle that control operation of the data collection
    ///             thread.
    /// 
    /// </summary>
    protected WaitHandle[] _waitHandles = new WaitHandle[1];
    /// <summary>
    /// Index in the _waitHandles array where the event that stops data
    ///             collection is stored.
    /// 
    /// </summary>
    protected const int StopEvent = 0;
    /// <summary>
    /// DNS name or TCP/IP address of the physical host from which data
    ///             will be collected.
    /// 
    /// </summary>
    protected string _hostName;
    /// <summary>
    /// TCP/IP port on which the host is listening for requests.
    /// 
    /// </summary>
    protected int _hostPort;
    /// <summary>
    /// User name to connect to the host.
    /// 
    /// </summary>
    protected string _username;
    /// <summary>
    /// Password to connect to the host.
    /// 
    /// </summary>
    protected string _password;
    /// <summary>
    /// The internal, Kirkwood database ID of the pool.
    /// 
    /// </summary>
    protected int _poolId;
    /// <summary>
    /// Flag to indicate if the data collector has been properly
    ///             initialized.
    /// 
    /// </summary>
    protected bool _isInitialized;
    /// <summary>
    /// Flag to indicate if the data collection thread is running.
    /// 
    /// </summary>
    protected bool _collectionIsRunning;
    /// <summary>
    /// Thread instance representing the thread that is collecting data.
    /// 
    /// </summary>
    protected Thread _collectorThread;
    private PerformanceCounter _hostMetricsCounter;
    private PerformanceCounter _vmMetricsCounter;

    /// <summary>
    /// Get or set a flag to indicate if the data collector has been
    ///             properly initialized.
    /// 
    /// </summary>
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

    /// <summary>
    /// Get a flag that indicates whether the data collection thread is
    ///             alive.
    /// 
    /// </summary>
    public bool IsAlive
    {
      get
      {
        bool flag = true;
        if (this._collectionIsRunning)
          flag = this._collectorThread.IsAlive;
        return flag;
      }
    }

    /// <summary>
    /// Get the performance counter that records host metrics captured
    ///             per seconds.
    /// 
    /// </summary>
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
          catch (InvalidOperationException ex)
          {
          }
        }
        return this._hostMetricsCounter;
      }
    }

    /// <summary>
    /// Get the performance counter that records VM metrics captured
    ///             per seconds.
    /// 
    /// </summary>
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
          catch (InvalidOperationException ex)
          {
          }
        }
        return this._vmMetricsCounter;
      }
    }

    /// <summary>
    /// Initialize this instance and create a new Xen session.
    /// 
    /// </summary>
    /// <param name="hostname">DNS name or TCP/IP address of the physical
    ///             host from which data will be collected.</param><param name="port">TCP/IP port on which the host is listening for
    ///             requests.  The default is port 80.</param><param name="protocol">The protocol (http/https/etc) to used
    ///             when interacting with the pool master.</param><param name="username">User name to connect to the host.</param><param name="password">Password to connect to the host.</param><param name="poolId">The internal, database ID of the pool.  If
    ///             the database ID is not known, 0 can be specified.</param>
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

    /// <summary>
    /// Close a data collector action session.
    /// 
    /// </summary>
    public virtual void UnInitialize()
    {
      this._hostName = (string) null;
      this._hostPort = 0;
      this._protocol = (string) null;
      this._username = (string) null;
      this._password = (string) null;
      this._poolId = 0;
      this.IsInitialized = false;
    }

    /// <summary>
    /// Start collecting performance metrics for the Xen pool associated
    ///             with this instance.
    /// 
    /// </summary>
    public virtual void StartCollection()
    {
      if (!this.IsInitialized)
        throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the StartCollection method", DwmErrorCode.NotInitialized, (Exception) null);
      if (this._collectionIsRunning)
      {
        if (this._collectorThread.IsAlive)
          return;
      }
      try
      {
        this._pollInterval = Configuration.GetValueAsInt(ConfigItems.DataCollectionPollInterval);
        if (this._pollInterval > 0)
        {
          string fmt = "Data collection poll interval set to {0} seconds";
          object[] objArray = new object[1];
          int index = 0;
          // ISSUE: variable of a boxed type
          __Boxed<int> local = (ValueType) this._pollInterval;
          objArray[index] = (object) local;
          Logger.Trace(fmt, objArray);
          this._pollInterval *= 1000;
        }
      }
      catch (DwmException ex)
      {
        Logger.LogException((Exception) ex);
      }
      this._waitHandles[0] = (WaitHandle) new AutoResetEvent(false);
      this._collectorThread = new Thread(new ThreadStart(this.ThreadProc));
      this._collectorThread.Start();
      this._collectionIsRunning = true;
    }

    /// <summary>
    /// Stop collecting performance metrics for the Xen pool associated
    ///             with this instance.
    /// 
    /// </summary>
    public virtual void StopCollection()
    {
      if (!this._collectionIsRunning)
        return;
      ((EventWaitHandle) this._waitHandles[0]).Set();
      this._collectionIsRunning = false;
    }

    /// <summary>
    /// Thread that does the actual data collection based.  Must be
    ///             implemented by derived classes.
    /// 
    /// </summary>
    protected abstract void ThreadProc();

    /// <summary>
    /// Increment the specified performance counter.
    /// 
    /// </summary>
    /// <param name="counter">Performance counter to increment.</param>
    protected static void IncrementPerfCounter(PerformanceCounter counter)
    {
      if (counter == null)
        return;
      counter.Increment();
    }
  }
}
