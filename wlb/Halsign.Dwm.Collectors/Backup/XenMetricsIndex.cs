// Decompiled with JetBrains decompiler
// Type: Citrix.DWM.Collectors.XenMetricsIndex
// Assembly: Citrix.Dwm.Collectors, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0844E477-F94E-4593-A883-69DEC5AD079C
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\Citrix.Dwm.Collectors.dll

using System.Collections.Generic;

namespace Citrix.DWM.Collectors
{
  /// <summary>
  /// Class to describe the row index within Xen 5 XML metrics of each
  ///             metric that is used by Kirkwood.
  /// 
  /// </summary>
  internal class XenMetricsIndex
  {
    /// <summary>
    /// The index within Xen 5.0 metrics of the load average metric.
    ///             This value should only be used by the Xen data collector.
    /// 
    /// </summary>
    internal int LoadAverageRowIndex = -1;
    /// <summary>
    /// The index within Xen 5.0 metrics of the total memory metric.
    ///             This value should only be used by the Xen data collector.
    /// 
    /// </summary>
    internal int TotalMemRowIndex = -1;
    /// <summary>
    /// The index within Xen 5.0 metrics of the free memory metric.
    ///             This value should only be used by the Xen data collector.
    /// 
    /// </summary>
    internal int FreeMemRowIndex = -1;
    /// <summary>
    /// The index within Xen 5.5 metrics of the memory target metric.
    ///             This value should only be used by the Xen data collector.
    /// 
    /// </summary>
    internal int TargetMemRowIndex = -1;
    /// <summary>
    /// The index within Xen 5.5 metrics for the percent of time all
    ///             VCPUs are blocked
    ///             This value should only be used by the Xen data collector.///
    /// 
    /// </summary>
    internal int RunstateBlocked = -1;
    /// <summary>
    /// The index within Xen 5.5 metrics for when some VCPUs are running,
    ///             others are blocked
    ///             This value should only be used by the Xen data collector.///
    /// 
    /// </summary>
    internal int RunstatePartialRun = -1;
    /// <summary>
    /// The index within Xen 5.5 metrics for % of time all VCPUs are
    ///             running
    ///             This value should only be used by the Xen data collector.///
    /// 
    /// </summary>
    internal int RunstateFullRun = -1;
    /// <summary>
    /// The index within Xen 5.5 metrics for % of time some VCPUs
    ///             are blocked, others are waiting for the CPU
    ///             This value should only be used by the Xen data collector.///
    /// 
    /// </summary>
    internal int RunstatePartialContention = -1;
    /// <summary>
    /// The index within Xen 5.5 metrics for % of time where some VCPU's
    ///             are running, others are waiting for the CPU.
    ///             This value should only be used by the Xen data collector.///
    /// 
    /// </summary>
    internal int RunstateConcurrencyHazard = -1;
    /// <summary>
    /// The index within Xen 5.5 metrics for the % of time all VCPUs
    ///             are waiting for the CPU
    ///             This value should only be used by the Xen data collector.///
    /// 
    /// </summary>
    internal int RunstateFullContention = -1;
    private string _hostUuid;
    private string _vmUuid;
    private List<MetricItemIndex> _cpuUtilizationRowIndex;
    private List<MetricItemIndex> _pifIoReadRowIndex;
    private List<MetricItemIndex> _pifIoWriteRowIndex;
    private List<MetricItemIndex> _vbdIoReadRowIndex;
    private List<MetricItemIndex> _vbdIoWriteRowIndex;

    /// <summary>
    /// Get or set the unique identifier of the host to which the
    ///             metrics apply.
    /// 
    /// </summary>
    internal string HostUuid
    {
      get
      {
        return this._hostUuid;
      }
      set
      {
        this._hostUuid = value;
      }
    }

    /// <summary>
    /// Get or set the unique identifier of the virtual machine to which
    ///             the metrics apply.
    /// 
    /// </summary>
    internal string VmUuid
    {
      get
      {
        return this._vmUuid;
      }
      set
      {
        this._vmUuid = value;
      }
    }

    /// <summary>
    /// The indices within Xen 5.0 metrics of the CPU utilization metrics.
    ///             This value should only be used by the Xen data collector.
    /// 
    /// </summary>
    internal List<MetricItemIndex> CpuUtilizationRowIndex
    {
      get
      {
        return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._cpuUtilizationRowIndex);
      }
    }

    /// <summary>
    /// The indices within Xen 5.0 metrics of the physical interface read
    ///             per second metrics.  This value should only be used by the Xen data
    ///             collector.
    /// 
    /// </summary>
    internal List<MetricItemIndex> PifIoReadRowIndex
    {
      get
      {
        return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._pifIoReadRowIndex);
      }
    }

    /// <summary>
    /// The indices within Xen 5.0 metrics of the physical interface write
    ///             per second metrics.  This value should only be used by the Xen data
    ///             collector.
    /// 
    /// </summary>
    internal List<MetricItemIndex> PifIoWriteRowIndex
    {
      get
      {
        return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._pifIoWriteRowIndex);
      }
    }

    /// <summary>
    /// The indices within Xen 5.0 metrics of the virtual block device
    ///             IO read per second metrics.
    /// 
    /// </summary>
    internal List<MetricItemIndex> VbdIoReadRowIndex
    {
      get
      {
        return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._vbdIoReadRowIndex);
      }
    }

    /// <summary>
    /// The indices within Xen 5.0 metrics of the virtual block device
    ///             IO write per second metrics.
    /// 
    /// </summary>
    internal List<MetricItemIndex> VbdIoWriteRowIndex
    {
      get
      {
        return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._vbdIoWriteRowIndex);
      }
    }

    /// <summary>
    /// Initialize a new instance of the XenMetricsIndex class.
    /// 
    /// </summary>
    /// <param name="hostUuid">Unique identifier of the host to which the
    ///             metrics apply.</param>
    internal XenMetricsIndex(string hostUuid)
    {
      this._hostUuid = hostUuid;
    }

    /// <summary>
    /// Initialize a new instance of the XenMetricsIndex class.
    /// 
    /// </summary>
    /// <param name="hostUuid">Unique identifier of the host to which the
    ///             metrics apply.</param><param name="vmUuid">Unique identifier of the vm to which the
    ///             metrics apply.</param>
    internal XenMetricsIndex(string hostUuid, string vmUuid)
    {
      this._hostUuid = hostUuid;
      this._vmUuid = vmUuid;
    }

    /// <summary>
    /// Retrieve a non-null reference to the specified item.
    /// 
    /// </summary>
    /// <typeparam name="T">Type of item to retrieve.</typeparam><param name="item">Instance of item to retrieve/return.  If this
    ///             instance is null, a new instance will be created.</param>
    /// <returns>
    /// Non null instance of item.
    /// </returns>
    internal static T SafeGetItem<T>(ref T item) where T : new()
    {
      if ((object) item == null)
        item = new T();
      return item;
    }
  }
}
