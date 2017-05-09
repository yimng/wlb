// Decompiled with JetBrains decompiler
// Type: Citrix.DWM.Collectors.XenCollector
// Assembly: Citrix.Dwm.Collectors, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0844E477-F94E-4593-A883-69DEC5AD079C
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\Citrix.Dwm.Collectors.dll

using Citrix.DWM.Domain;
using Citrix.DWM.Framework;
using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using XenAPI;

namespace Citrix.DWM.Collectors
{
  /// <summary>
  /// Data collector for Xen hypervisors
  /// 
  /// </summary>
  public class XenCollector : XenCollectorBase, ICollector
  {
    /// <summary>
    /// Number format info used to force double value to use a period (.)
    ///             as the decimal separator.
    /// 
    /// </summary>
    private static NumberFormatInfo _nfi = new CultureInfo("en-US", false).NumberFormat;
    private object _hostCacheLock = new object();
    private object _vmCacheLock = new object();
    private object _netCacheLock = new object();
    private object _pbdCacheLock = new object();
    private object _pifCacheLock = new object();
    private object _srCacheLock = new object();
    private object _vbdCacheLock = new object();
    private object _vdiCacheLock = new object();
    private object _vifCacheLock = new object();
    /// <summary>
    /// This will be set to false once the datacollector has started.
    ///             This flag is used to check whether the datacollector just started,
    ///             and if it's true, Discover() will be run regardless.
    /// 
    /// </summary>
    private bool _isInitialRun = true;
    private DateTime _lastLicenseCheck = DateTime.MinValue;
    private DateTime _lastValidPoolCheck = DateTime.MinValue;
    /// <summary>
    /// Version number for Xen 5.
    /// 
    /// </summary>
    private const int XEN_5 = 5;
    /// <summary>
    /// Number of times to retry host communication after failure
    /// 
    /// </summary>
    private const int HOST_COMMUNICATION_RETRY_COUNT = 5;
    /// <summary>
    /// Special user name to use when the collector save the pool.  The
    ///             save stored proc will not update the touched_by column if it sees
    ///             this user.
    /// 
    /// </summary>
    private const string XEN_COLLECTOR_NAME = "__XEN_DATA_COLLECTOR__";
    private Dictionary<string, HostCacheItem> _hostCache;
    private Dictionary<string, VmCacheItem> _vmCache;
    private Dictionary<string, VM> _vmCache2;
    private Dictionary<string, Network> _networkCache;
    private Dictionary<string, PBD> _pbdCache;
    private Dictionary<string, PIF> _pifCache;
    private Dictionary<string, XenAPI.SR> _srCache;
    private Dictionary<string, VBD> _vbdCache;
    private Dictionary<string, VDI> _vdiCache;
    private Dictionary<string, VIF> _vifCache;
    private static Dictionary<string, HostHttpError> _hostHttpErrors;

    /// <summary>
    /// Initialize a new instance of the XenCollector.
    /// 
    /// </summary>
    public XenCollector()
    {
    }

    /// <summary>
    /// Initialize a new instance of the XenCollector.
    /// 
    /// </summary>
    /// <param name="hostname">DNS name or TCP/IP address of a host in
    ///             a Xen pool from which data will be collected.</param><param name="port">TCP/IP port on which the host is listening for
    ///             requests.  The default is port 80.</param><param name="protocol">The protocol (http/https/etc) to use
    ///             retrieving metrics from each host in the pool.</param><param name="username">User name to connect to the host.</param><param name="password">Password to connect to the host.</param>
    /// <remarks>
    /// The constructor will throw an DwmException if a session
    ///             cannot be established.
    /// </remarks>
    public XenCollector(string hostname, int port, string protocol, string username, string password)
      : base(hostname, port, protocol, username, password)
    {
    }

    /// <summary>
    /// Clear all the local caches
    /// 
    /// </summary>
    protected void ClearCache()
    {
      Logger.Trace("Clearing collector cache...");
      this._hostCache.Clear();
      this._vmCache.Clear();
      this._networkCache.Clear();
      this._pbdCache.Clear();
      this._pifCache.Clear();
      this._srCache.Clear();
      this._vbdCache.Clear();
      this._vdiCache.Clear();
      this._vifCache.Clear();
    }

    /// <summary>
    /// Process a Xen event of the specified VM
    /// 
    /// </summary>
    /// <param name="vmUuid">The unique ID of the VM that is the target of
    ///             the Xen event.</param><param name="operation">The event type - add, modify, etc.</param><param name="serverOpaqueRef">The server handle of the VM to which
    ///             the event applies.</param><param name="snapshot">The snapshot data associated with the
    ///             event.</param>
    protected override void ProcessVmEvent(string vmUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
    {
      bool flag = false;
      int recommendationId = 0;
      object obj = this._vmCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (this._vmCache == null)
          return;
        if (Localization.Compare(operation, "mod", true) == 0 || Localization.Compare(operation, "del", true) == 0)
        {
          VmCacheItem vmCacheItem;
          if (!this._vmCache.TryGetValue(vmUuid, out vmCacheItem) || vmCacheItem == null || vmCacheItem.xenVM == null)
            return;
          XenCollectorBase.TraceVmEvent(snapshot, vmCacheItem.xenVM);
          if (Localization.Compare(operation, "mod", true) == 0)
          {
            string propValue1;
            this.GetProperty("resident_on", snapshot, out propValue1);
            string propValue2;
            this.GetProperty("power_state", snapshot, out propValue2);
            if (propValue1 != null)
            {
              if (Localization.Compare(propValue1, vmCacheItem.xenVM.resident_on.opaque_ref, true) != 0)
              {
                string fmt = "residentOn - snapshot: {0}  VM: {1}";
                object[] objArray = new object[2];
                int index1 = 0;
                string str = propValue1;
                objArray[index1] = (object) str;
                int index2 = 1;
                string opaqueRef = vmCacheItem.xenVM.resident_on.opaque_ref;
                objArray[index2] = (object) opaqueRef;
                Logger.Trace(fmt, objArray);
                if (XenCollectorBase.IsValidXenRef(propValue1))
                {
                  Host hostRecord = XenCollector.GetHostRecord(this._session, propValue1);
                  if (hostRecord != null && this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
                  {
                    DwmVirtualMachine.SetRunningOnHost(vmUuid, hostRecord.uuid, this._pool.Uuid, vmCacheItem.pendingRecommendationId);
                    flag = true;
                  }
                }
                else if (propValue2 != null && Localization.Compare(propValue2, vmCacheItem.xenVM.power_state.ToString(), true) != 0 && (this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid)))
                {
                  DwmVirtualMachine.SetRunningOnHost(vmUuid, (string) null, this._pool.Uuid, vmCacheItem.pendingRecommendationId);
                  flag = true;
                }
              }
              else if (Localization.Compare(propValue2, vmCacheItem.xenVM.power_state.ToString(), true) != 0 && Localization.Compare(propValue2, "Running", true) == 0)
                flag = true;
            }
            if (!flag)
            {
              if (!this.HasVmRebooted(snapshot, vmCacheItem.xenVM))
              {
                if (!this.HasPropertyChanged("name_label", snapshot, vmCacheItem.xenVM.name_label))
                {
                  if (!this.HasPropertyChanged("name_description", snapshot, vmCacheItem.xenVM.name_description))
                  {
                    if (!this.HasPropertyChanged("memory_dynamic_min", snapshot, vmCacheItem.xenVM.memory_dynamic_min.ToString()))
                    {
                      if (!this.HasPropertyChanged("memory_dynamic_max", snapshot, vmCacheItem.xenVM.memory_dynamic_max.ToString()))
                      {
                        if (!this.HasPropertyChanged("memory_static_min", snapshot, vmCacheItem.xenVM.memory_static_min.ToString()))
                        {
                          if (!this.HasPropertyChanged("memory_static_max", snapshot, vmCacheItem.xenVM.memory_static_max.ToString()))
                          {
                            if (!this.HasPropertyChanged("memory_target", snapshot, vmCacheItem.xenVM.memory_target.ToString()))
                            {
                              if (!this.HasPropertyChanged("memory_overhead", snapshot, vmCacheItem.xenVM.memory_overhead.ToString()))
                              {
                                if (!this.HasPropertyChanged("VCPUs_at_startup", snapshot, vmCacheItem.xenVM.VCPUs_at_startup.ToString()))
                                {
                                  if (!this.HasPropertyChanged<VBD>("VBDs", snapshot, vmCacheItem.xenVM.VBDs))
                                  {
                                    if (!this.HasPropertyChanged("is_a_template", snapshot, vmCacheItem.xenVM.is_a_template.ToString()))
                                      return;
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
            try
            {
              VM vmRecord = XenCollector.GetVmRecord(this._session, vmCacheItem.serverOpaqueRef);
              if (vmRecord == null)
                return;
              this.DiscoverVm(this._session, vmCacheItem.serverOpaqueRef, vmRecord, true);
            }
            catch (Failure ex)
            {
            }
          }
          else
          {
            if (Localization.Compare(operation, "del", true) != 0 || this._pool == null || string.IsNullOrEmpty(this._pool.Uuid))
              return;
            DwmVirtualMachine.DeleteVM(vmUuid, this._pool.Uuid);
          }
        }
        else
        {
          if (Localization.Compare(operation, "add", true) != 0)
            return;
          string fmt1 = "adding VM {0}";
          object[] objArray1 = new object[1];
          int index1 = 0;
          string str1 = serverOpaqueRef;
          objArray1[index1] = (object) str1;
          Logger.Trace(fmt1, objArray1);
          string propValue1;
          this.GetProperty("resident_on", snapshot, out propValue1);
          string propValue2;
          this.GetProperty("power_state", snapshot, out propValue2);
          string fmt2 = "powerState - {0}";
          object[] objArray2 = new object[1];
          int index2 = 0;
          string str2 = propValue2;
          objArray2[index2] = (object) str2;
          Logger.Trace(fmt2, objArray2);
          string fmt3 = "residentOn - {0}";
          object[] objArray3 = new object[1];
          int index3 = 0;
          string str3 = propValue1;
          objArray3[index3] = (object) str3;
          Logger.Trace(fmt3, objArray3);
          VM vmRecord = XenCollector.GetVmRecord(this._session, serverOpaqueRef);
          if (vmRecord == null)
            return;
          this.DiscoverVm(this._session, serverOpaqueRef, vmRecord, true);
          if (XenCollectorBase.IsValidXenRef(propValue1) && Localization.Compare(propValue2, vm_power_state.Running.ToString(), true) == 0)
          {
            Host hostRecord = XenCollector.GetHostRecord(this._session, propValue1);
            if (hostRecord == null || this._pool == null || string.IsNullOrEmpty(this._pool.Uuid))
              return;
            DwmVirtualMachine.SetRunningOnHost(vmUuid, hostRecord.uuid, this._pool.Uuid, recommendationId);
          }
          else
          {
            VmCacheItem vmCacheItem;
            if (!this._vmCache.TryGetValue(vmUuid, out vmCacheItem) || vmCacheItem == null || (vmCacheItem.xenVM == null || !XenCollectorBase.IsValidXenRef<Host>(vmCacheItem.xenVM.resident_on)) || vmCacheItem.xenVM.power_state != vm_power_state.Running)
              return;
            Host hostRecord = XenCollector.GetHostRecord(this._session, (string) vmCacheItem.xenVM.resident_on);
            if (hostRecord == null || this._pool == null || string.IsNullOrEmpty(this._pool.Uuid))
              return;
            DwmVirtualMachine.SetRunningOnHost(vmUuid, hostRecord.uuid, this._pool.Uuid, recommendationId);
          }
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
    }

    /// <summary>
    /// Process a Xen event on the specified host
    /// 
    /// </summary>
    /// <param name="hostUuid">The unique ID of the host that is the target of
    ///             the Xen event.</param><param name="operation">The event type - add, modify, etc.</param><param name="serverOpaqueRef">The server handle of the Host to which
    ///             the event applies.</param><param name="snapshot">The snapshot data associated with the
    ///             event.</param>
    protected override void ProcessHostEvent(string hostUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
    {
      if (this._hostCache == null)
        return;
      if (Localization.Compare(operation, "mod", true) == 0)
        this.ProcessHostModEvent(snapshot, hostUuid, serverOpaqueRef);
      else if (Localization.Compare(operation, "add", true) == 0)
      {
        if (this.HostCacheContainsKey(hostUuid))
          return;
        this.DiscoverHost(this._session, hostUuid, true, true);
      }
      else
      {
        if (Localization.Compare(operation, "del", true) != 0)
          return;
        object obj = this._hostCacheLock;
        Monitor.Enter(obj);
        try
        {
          if (this._hostCache.ContainsKey(hostUuid))
            this._hostCache.Remove(hostUuid);
        }
        finally
        {
          Monitor.Exit(obj);
        }
        if (this._pool == null || string.IsNullOrEmpty(this._pool.Uuid))
          return;
        DwmHost.DeleteHost(hostUuid, this._pool.Uuid);
      }
    }

    /// <summary>
    /// Process a modification event for the physical host with the
    ///             specified uuid.
    /// 
    /// </summary>
    /// <param name="snapshot">The snapshot data associated with the
    ///             event.</param><param name="hostUuid">Unique ID of the host for which the
    ///             modification event has fired.</param><param name="serverOpaqueRef">Xen handle of the host for which
    ///             the modification event has fired.</param>
    protected void ProcessHostModEvent(XmlRpcStruct snapshot, string hostUuid, string serverOpaqueRef)
    {
      bool flag1 = false;
      object obj = this._hostCacheLock;
      Monitor.Enter(obj);
      try
      {
        HostCacheItem hostCacheItem;
        if (!this._hostCache.TryGetValue(hostUuid, out hostCacheItem) || hostCacheItem == null || hostCacheItem.xenHost == null)
          return;
        object key1 = (object) "enabled";
        if (snapshot.ContainsKey(key1))
        {
          bool enabled = bool.Parse(snapshot[key1].ToString());
          if (hostCacheItem.xenHost.enabled != enabled && this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
          {
            string fmt = "Processing enable changed to {0} for host {1}";
            object[] objArray = new object[2];
            int index1 = 0;
            // ISSUE: variable of a boxed type
            __Boxed<bool> local = (ValueType) (bool) (enabled ? 1 : 0);
            objArray[index1] = (object) local;
            int index2 = 1;
            string str = hostUuid;
            objArray[index2] = (object) str;
            Logger.Trace(fmt, objArray);
            DwmHost.SetEnabled(hostUuid, this._pool.Uuid, enabled);
            hostCacheItem.xenHost.enabled = enabled;
            hostCacheItem.dwmHost.Enabled = enabled;
          }
        }
        bool flag2 = false;
        object key2 = (object) "host_CPUs";
        if (snapshot.ContainsKey(key2))
        {
          object[] objArray1 = (object[]) snapshot[key2];
          if (objArray1.Length != hostCacheItem.xenHost.host_CPUs.Count)
          {
            string fmt = "Processing CPUs changed to {0} for host {1}";
            object[] objArray2 = new object[2];
            int index1 = 0;
            // ISSUE: variable of a boxed type
            __Boxed<int> local = (ValueType) objArray1.Length;
            objArray2[index1] = (object) local;
            int index2 = 1;
            string str = hostUuid;
            objArray2[index2] = (object) str;
            Logger.Trace(fmt, objArray2);
            flag2 = true;
          }
        }
        object key3 = (object) "PBDs";
        if (snapshot.ContainsKey(key3))
        {
          object[] objArray1 = (object[]) snapshot[key3];
          if (objArray1.Length != hostCacheItem.xenHost.PBDs.Count)
          {
            string fmt = "Processing PBDs changed to {0} for host {1}";
            object[] objArray2 = new object[2];
            int index1 = 0;
            // ISSUE: variable of a boxed type
            __Boxed<int> local = (ValueType) objArray1.Length;
            objArray2[index1] = (object) local;
            int index2 = 1;
            string str = hostUuid;
            objArray2[index2] = (object) str;
            Logger.Trace(fmt, objArray2);
            flag2 = true;
          }
        }
        object key4 = (object) "PIFs";
        if (snapshot.ContainsKey(key4))
        {
          object[] objArray1 = (object[]) snapshot[key4];
          if (objArray1.Length != hostCacheItem.xenHost.PIFs.Count)
          {
            string fmt = "Processing PIFs changed to {0} for host {1}";
            object[] objArray2 = new object[2];
            int index1 = 0;
            // ISSUE: variable of a boxed type
            __Boxed<int> local = (ValueType) objArray1.Length;
            objArray2[index1] = (object) local;
            int index2 = 1;
            string str = hostUuid;
            objArray2[index2] = (object) str;
            Logger.Trace(fmt, objArray2);
            flag2 = true;
          }
        }
        object key5 = (object) "software_version";
        if (snapshot.ContainsKey(key5))
        {
          XmlRpcStruct xmlRpcStruct = (XmlRpcStruct) snapshot[key5];
          if (xmlRpcStruct.Count != hostCacheItem.xenHost.software_version.Count)
          {
            string fmt = "Processing software version changed to {0} for host {1}";
            object[] objArray = new object[2];
            int index1 = 0;
            // ISSUE: variable of a boxed type
            __Boxed<int> local = (ValueType) xmlRpcStruct.Count;
            objArray[index1] = (object) local;
            int index2 = 1;
            string str = hostUuid;
            objArray[index2] = (object) str;
            Logger.Trace(fmt, objArray);
            flag2 = true;
          }
        }
        object key6 = (object) "address";
        if (snapshot.ContainsKey(key6))
        {
          string s1 = (string) snapshot[key6];
          if (Localization.Compare(s1, hostCacheItem.xenHost.address, true) != 0)
          {
            string fmt = "Processing adress changed to {0} for host {1}";
            object[] objArray = new object[2];
            int index1 = 0;
            string str1 = s1;
            objArray[index1] = (object) str1;
            int index2 = 1;
            string str2 = hostUuid;
            objArray[index2] = (object) str2;
            Logger.Trace(fmt, objArray);
            flag2 = true;
          }
        }
        object key7 = (object) "name_label";
        if (snapshot.ContainsKey(key7))
        {
          string s1 = (string) snapshot[key7];
          if (Localization.Compare(s1, hostCacheItem.xenHost.name_label, true) != 0)
          {
            string fmt = "Processing name_label changed to {0} for host {1}";
            object[] objArray = new object[2];
            int index1 = 0;
            string str1 = s1;
            objArray[index1] = (object) str1;
            int index2 = 1;
            string str2 = hostUuid;
            objArray[index2] = (object) str2;
            Logger.Trace(fmt, objArray);
            flag2 = true;
          }
        }
        object key8 = (object) "name_description";
        if (snapshot.ContainsKey(key8))
        {
          string s1 = (string) snapshot[key8];
          if (Localization.Compare(s1, hostCacheItem.xenHost.name_description, true) != 0)
          {
            string fmt = "Processing name_description changed to {0} for host {1}";
            object[] objArray = new object[2];
            int index1 = 0;
            string str1 = s1;
            objArray[index1] = (object) str1;
            int index2 = 1;
            string str2 = hostUuid;
            objArray[index2] = (object) str2;
            Logger.Trace(fmt, objArray);
            flag2 = true;
          }
        }
        if ((!Host_metrics.get_live(this._session, hostCacheItem.xenHost.metrics.opaque_ref) ? PowerStatus.Off : PowerStatus.On) != hostCacheItem.dwmHost.PowerState)
        {
          flag2 = true;
          flag1 = true;
        }
        if (!flag2)
          return;
        if (flag1)
        {
          List<string> list = new List<string>();
          foreach (KeyValuePair<string, HostCacheItem> keyValuePair in this._hostCache)
          {
            if (keyValuePair.Value.serverOpaqueRef != serverOpaqueRef)
              list.Add(keyValuePair.Value.serverOpaqueRef);
          }
          foreach (string str in list)
          {
            Host hostRecord = XenCollector.GetHostRecord(this._session, str);
            if (hostRecord != null)
              this.DiscoverHost(this._session, str, hostRecord, true, false);
          }
        }
        Host hostRecord1 = XenCollector.GetHostRecord(this._session, serverOpaqueRef);
        if (hostRecord1 == null)
          return;
        DwmHost dwmHost = this.DiscoverHost(this._session, serverOpaqueRef, hostRecord1, true, true);
        hostCacheItem.dwmHost = dwmHost;
        if (!hostCacheItem.pendingPoweredOffByWlb || !flag1)
          return;
        DwmHost.SetPoweredOffByWlb(dwmHost.Id, true);
      }
      finally
      {
        Monitor.Exit(obj);
      }
    }

    /// <summary>
    /// Process a Xen event of the specified task
    /// 
    /// </summary>
    /// <param name="taskUuid">The unique ID of the task that is the target of
    ///             the Xen event.</param><param name="operation">The event type - add, modify, etc.</param><param name="serverOpaqueRef">The server handle of the task to which
    ///             the event applies.</param><param name="snapshot">The snapshot data associated with the
    ///             event.</param>
    protected override void ProcessTaskEvent(string taskUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
    {
      object key1 = (object) "other_config";
      string str1 = string.Empty;
      int result = 0;
      if (XenCollectorBase._traceEventsEnabled)
      {
        object index1 = (object) "name_label";
        object index2 = (object) "status";
        int num = XenCollectorBase._traceEventsEnabled ? 1 : 0;
        string fmt = "Task {0}.  Status={1}";
        object[] objArray = new object[2];
        int index3 = 0;
        string str2 = (string) snapshot[index1];
        objArray[index3] = (object) str2;
        int index4 = 1;
        string str3 = (string) snapshot[index2];
        objArray[index4] = (object) str3;
        XenCollectorBase.Trace(num != 0, fmt, objArray);
      }
      if (!snapshot.ContainsKey(key1))
        return;
      XmlRpcStruct xmlRpcStruct = (XmlRpcStruct) snapshot[key1];
      if (xmlRpcStruct.Count <= 0)
        return;
      object key2 = (object) "wlb_advised";
      if (!xmlRpcStruct.ContainsKey(key2) || !int.TryParse((string) xmlRpcStruct[key2], out result))
        return;
      object key3 = (object) "wlb_action";
      if (xmlRpcStruct.ContainsKey(key3))
        str1 = (string) xmlRpcStruct[key3];
      object key4 = (object) "wlb_action_obj_type";
      if (!xmlRpcStruct.ContainsKey(key4))
        return;
      string s1 = (string) xmlRpcStruct[key4];
      if (Localization.Compare(s1, "VM", true) == 0)
      {
        object key5 = (object) "wlb_action_obj_ref";
        if (!xmlRpcStruct.ContainsKey(key5))
          return;
        VmCacheItem cachedVmByXenRef = this.GetCachedVmByXenRef((string) xmlRpcStruct[key5]);
        string propValue;
        if (cachedVmByXenRef == null || !this.GetProperty("status", snapshot, out propValue))
          return;
        if (Localization.Compare(propValue, "pending", true) == 0)
          cachedVmByXenRef.pendingRecommendationId = result;
        else
          cachedVmByXenRef.pendingRecommendationId = 0;
      }
      else
      {
        if (Localization.Compare(s1, "HOST", true) != 0)
          return;
        object key5 = (object) "wlb_action_obj_ref";
        if (!xmlRpcStruct.ContainsKey(key5))
          return;
        HostCacheItem cachedHostByXenRef = this.GetCachedHostByXenRef((string) xmlRpcStruct[key5]);
        string propValue;
        if (!this.GetProperty("status", snapshot, out propValue) || Localization.Compare(propValue, "pending", true) != 0)
          return;
        cachedHostByXenRef.pendingRecommendationId = result;
        cachedHostByXenRef.pendingPoweredOffByWlb = true;
      }
    }

    /// <summary>
    /// Process a Xen event for the specified pool.
    /// 
    /// </summary>
    /// <param name="poolUuid">The unique ID of the pool that is the target
    ///             of the Xen event.</param><param name="operation">The event type - add, modify, etc.</param><param name="serverOpaqueRef">The server handle of the Pool to which
    ///             the event applies.</param><param name="snapshot">The snapshot data associated with the
    ///             event.</param>
    protected override void ProcessPoolEvent(string poolUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
    {
      object key = (object) "wlb_enabled";
      if (Localization.Compare(operation, "mod", true) != 0 || snapshot == null || !snapshot.ContainsKey(key))
        return;
      bool enabled = (bool) snapshot[key];
      if (this._pool == null || this._pool.Enabled == enabled)
        return;
      this._pool.SetEnabled(enabled);
      string fmt = "Enabled state of pool {0} ({1}) set to {2}";
      object[] objArray = new object[3];
      int index1 = 0;
      string name = this._pool.Name;
      objArray[index1] = (object) name;
      int index2 = 1;
      string uuid = this._pool.Uuid;
      objArray[index2] = (object) uuid;
      int index3 = 2;
      // ISSUE: variable of a boxed type
      __Boxed<bool> local = (ValueType) (bool) (enabled ? 1 : 0);
      objArray[index3] = (object) local;
      Logger.Trace(fmt, objArray);
    }

    /// <summary>
    /// Determine if the operation to find is in the list of allowed
    ///             operations for a host.
    /// 
    /// </summary>
    /// <param name="operations">List of allowed operations for a host.</param><param name="operationToFind">The operation to find.</param>
    /// <returns>
    /// True if the operation is allowed for the host; false
    ///             otherwise.
    /// </returns>
    private bool OperationAllowed(object[] operations, string operationToFind)
    {
      bool flag = false;
      for (int index = 0; index < operations.Length; ++index)
      {
        if (Localization.Compare(operations[index].ToString(), operationToFind, true) == 0)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>
    /// Determine if the operation to find is in the list of allowed
    ///             operations for a host.
    /// 
    /// </summary>
    /// <param name="operations">List of allowed operations for a host.</param><param name="operationToFind">The operation to find.</param>
    /// <returns>
    /// True if the operation is allowed for the host; false
    ///             otherwise.
    /// </returns>
    private bool OperationAllowed(string[] operations, string operationToFind)
    {
      bool flag = false;
      for (int index = 0; index < operations.Length; ++index)
      {
        if (Localization.Compare(operations[index], operationToFind, true) == 0)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>
    /// Retrieve the VMCacheItem instance from the VM cache for the VM
    ///             with the specified server opaque ref.
    /// 
    /// </summary>
    /// <param name="xenRef">Server opaque ref of the VM to retrieve.</param>
    /// <returns>
    /// VMCacheItem if successful; null otherwise.
    /// </returns>
    private VmCacheItem GetCachedVmByXenRef(string xenRef)
    {
      VmCacheItem vmCacheItem = (VmCacheItem) null;
      object obj = this._vmCacheLock;
      Monitor.Enter(obj);
      try
      {
        foreach (KeyValuePair<string, VmCacheItem> keyValuePair in this._vmCache)
        {
          if (Localization.Compare(keyValuePair.Value.serverOpaqueRef, xenRef, true) == 0)
          {
            vmCacheItem = keyValuePair.Value;
            break;
          }
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return vmCacheItem;
    }

    /// <summary>
    /// Retrieve the HostCacheItem instance from the Host cache for the
    ///             host with the specified server opaque ref.
    /// 
    /// </summary>
    /// <param name="xenRef">Server opaque ref of the host to retrieve.</param>
    /// <returns>
    /// HostCacheItem if successful; null otherwise.
    /// </returns>
    private HostCacheItem GetCachedHostByXenRef(string xenRef)
    {
      HostCacheItem hostCacheItem = (HostCacheItem) null;
      object obj = this._hostCacheLock;
      Monitor.Enter(obj);
      try
      {
        foreach (KeyValuePair<string, HostCacheItem> keyValuePair in this._hostCache)
        {
          if (Localization.Compare(keyValuePair.Value.serverOpaqueRef, xenRef, true) == 0)
          {
            hostCacheItem = keyValuePair.Value;
            break;
          }
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return hostCacheItem;
    }

    /// <summary>
    /// Get the list of all hosts associated with this session.
    /// 
    /// </summary>
    private void GetHosts()
    {
      try
      {
        object obj = this._hostCacheLock;
        Monitor.Enter(obj);
        try
        {
          if (this._hostCache != null && this._hostCache.Count != 0 || this._session == null)
            return;
          Dictionary<XenRef<Host>, Host> allRecords = Host.get_all_records(this._session);
          if (allRecords == null)
            return;
          foreach (KeyValuePair<XenRef<Host>, Host> keyValuePair in allRecords)
          {
            if (!this._hostCache.ContainsKey(keyValuePair.Value.uuid))
              this.DiscoverHost(this._session, keyValuePair.Key.opaque_ref, keyValuePair.Value, true, true);
          }
        }
        finally
        {
          Monitor.Exit(obj);
        }
      }
      catch (Failure ex)
      {
        Logger.LogException((Exception) ex);
      }
      catch (SocketException ex)
      {
        Logger.LogException((Exception) ex);
      }
      catch (WebException ex)
      {
        Logger.LogException((Exception) ex);
      }
    }

    /// <summary>
    /// Get the version of the specified Xen physical host.
    /// 
    /// </summary>
    /// <param name="xenHost">Xen host for which version information will
    ///             be retrieved.</param><param name="verMinor">On output, will contain the minor version of
    ///             the host.</param><param name="verBuild">On output, will contain the build number of
    ///             the host</param>
    /// <returns>
    /// The major version of the host.
    /// </returns>
    private int GetHostVersion(Host xenHost, out int verMinor, out int verBuild)
    {
      int num1 = 5;
      verMinor = 0;
      verBuild = 0;
      if (xenHost != null && xenHost.software_version != null)
      {
        string str = (string) null;
        if (xenHost.software_version.TryGetValue("product_version", out str) && !string.IsNullOrEmpty(str))
        {
          char[] chArray1 = new char[1];
          int index = 0;
          int num2 = 46;
          chArray1[index] = (char) num2;
          char[] chArray2 = chArray1;
          string[] strArray = str.Split(chArray2);
          if ((num1 = Localization.TryParse(strArray[0])) == 0)
            num1 = 5;
          if (strArray.Length > 1)
            verMinor = Localization.TryParse(strArray[1]);
          if (strArray.Length > 2)
            verBuild = Localization.TryParse(strArray[2]);
        }
      }
      return num1;
    }

    /// <summary>
    /// End out current Xen session.
    /// 
    /// </summary>
    protected override void EndSession()
    {
      base.EndSession();
      object obj = this._hostCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (this._hostCache == null)
          return;
        this._hostCache.Clear();
        if (this._pool == null)
          return;
        this._pool.UpdateDiscoveryStatus(DiscoveryStatus.New);
      }
      finally
      {
        Monitor.Exit(obj);
      }
    }

    /// <summary>
    /// Try to find a new pool master.
    /// 
    /// </summary>
    protected override void FindNewMaster()
    {
      XenCollectorBase.SessionInitStatus sessionInitStatus = XenCollectorBase.SessionInitStatus.None;
      Logger.Trace("Trying to find new pool master ...");
      object obj = this._hostCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (this._hostCache != null)
        {
          foreach (KeyValuePair<string, HostCacheItem> keyValuePair in this._hostCache)
          {
            if (keyValuePair.Value != null && keyValuePair.Value.xenHost != null)
            {
              string fmt = "  Trying {0} ...";
              object[] objArray = new object[1];
              int index = 0;
              string address = keyValuePair.Value.xenHost.address;
              objArray[index] = (object) address;
              Logger.Trace(fmt, objArray);
              sessionInitStatus = this.InitializeSession(keyValuePair.Value.xenHost.address, true, false);
              if (sessionInitStatus == XenCollectorBase.SessionInitStatus.Success)
                break;
            }
          }
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      if (sessionInitStatus == XenCollectorBase.SessionInitStatus.Success)
        return;
      base.FindNewMaster();
    }

    public override void StopCollection()
    {
      QueueManager.Flush();
      base.StopCollection();
    }

    /// <summary>
    /// Handle a poll interval for the data collection thread querying
    ///             the hypervisor pool for performance metrics.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This method assumes a Xen 5.x environment.
    /// </remarks>
    protected override void OnInterval()
    {
      XenCollectorBase._traceEnabled = Configuration.GetValueAsBool(ConfigItems.DataCollectionTrace);
      bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.ForcePoolEnabled);
      if (!valueAsBool && this._pool != null && !this._pool.Enabled)
      {
        Configuration.ReloadConfiguration();
        valueAsBool = Configuration.GetValueAsBool(ConfigItems.ForcePoolEnabled);
      }
      int valueAsInt = Configuration.GetValueAsInt(ConfigItems.DiscoveryInterval, 24);
      if (this._session == null && this.InitializeSession(this._hostName, true, false) != XenCollectorBase.SessionInitStatus.Success)
        this.OnConnectionLost((Exception) null);
      if (this._session == null)
        return;
      if (this._isInitialRun)
      {
        this.Discover();
        this._isInitialRun = false;
      }
      else
      {
        DateTime lastDiscoveryCompleted = DateTime.MinValue;
        DiscoveryStatus status = DiscoveryStatus.New;
        if (this._pool != null && this._pool.Id > 0)
          this._pool.GetDiscoveryStatus(out status, out lastDiscoveryCompleted);
        TimeSpan timeSpan = DateTime.UtcNow - lastDiscoveryCompleted;
        if (lastDiscoveryCompleted == DateTime.MinValue || timeSpan.TotalHours >= (double) valueAsInt || status == DiscoveryStatus.New)
        {
          if (timeSpan.TotalHours >= (double) valueAsInt && status != DiscoveryStatus.New)
            Logger.Trace(string.Format("Running Discover() because it has not run for {0} hours.", (object) valueAsInt.ToString()));
          this.Discover();
        }
      }
      if (this._pool != null)
      {
        if (this._pool.IsLicensed)
        {
          if (!this.Heartbeat() || !this._pool.Enabled && !valueAsBool)
            return;
          object obj = this._hostCacheLock;
          Monitor.Enter(obj);
          try
          {
            this.GetHosts();
            if (this._hostCache == null)
              return;
            string str1 = string.Empty;
            DwmHostMetricCollection dwmHostMetrics = new DwmHostMetricCollection();
            DwmVmMetricCollection dwmVmMetrics = new DwmVmMetricCollection();
            foreach (KeyValuePair<string, HostCacheItem> keyValuePair in this._hostCache)
            {
              Host xenHost = keyValuePair.Value.xenHost;
              bool isPoolMaster = keyValuePair.Value.dwmHost.IsPoolMaster;
              string key = keyValuePair.Key;
              try
              {
                int verMinor = 0;
                int verBuild = 0;
                int hostVersion = this.GetHostVersion(xenHost, out verMinor, out verBuild);
                if (this._pool.VersionMajor == -1 || hostVersion != this._pool.VersionMajor || (verMinor != this._pool.VersionMinor || verBuild != this._pool.VersionBuild))
                {
                  this._pool.VersionMajor = hostVersion;
                  this._pool.VersionMinor = verMinor;
                  this._pool.VersionBuild = verBuild;
                  this._pool.Save();
                }
                if (hostVersion >= 5)
                  this.GetXen5Metrics(keyValuePair.Value, dwmHostMetrics, dwmVmMetrics);
                keyValuePair.Value.errorCount = 0;
              }
              catch (Failure ex)
              {
                string fmt = "Caught Exception in XenCollector {0}:{1} -  {2}";
                object[] objArray = new object[3];
                int index1 = 0;
                string str2 = this._hostName;
                objArray[index1] = (object) str2;
                int index2 = 1;
                // ISSUE: variable of a boxed type
                __Boxed<int> local = (ValueType) this._hostPort;
                objArray[index2] = (object) local;
                int index3 = 2;
                string message = ex.Message;
                objArray[index3] = (object) message;
                Logger.LogError(fmt, objArray);
                Logger.LogException((Exception) ex);
                Thread.Sleep(30000);
              }
              catch (SocketException ex)
              {
                Logger.LogException((Exception) ex);
                Thread.Sleep(30000);
              }
              catch (WebException ex)
              {
                if (isPoolMaster)
                {
                  string fmt1 = "Unable to contact pool master host \"{0}\" (attempt {1} of {2})\r\n{3}";
                  object[] objArray1 = new object[4];
                  int index1 = 0;
                  string str2 = DwmHost.UuidToName(key, this._poolId);
                  objArray1[index1] = (object) str2;
                  int index2 = 1;
                  // ISSUE: variable of a boxed type
                  __Boxed<int> local1 = (ValueType) this._hostCache[key].errorCount;
                  objArray1[index2] = (object) local1;
                  int index3 = 2;
                  // ISSUE: variable of a boxed type
                  __Boxed<int> local2 = (ValueType) 5;
                  objArray1[index3] = (object) local2;
                  int index4 = 3;
                  string message = ex.Message;
                  objArray1[index4] = (object) message;
                  Logger.LogError(fmt1, objArray1);
                  ++this._hostCache[key].errorCount;
                  if (this._hostCache[key].errorCount > 5)
                  {
                    string fmt2 = "Ending session for pool \"{0}\" (Id: {2}) to trigger a rediscovery.";
                    object[] objArray2 = new object[2];
                    int index5 = 0;
                    string name = this._pool.Name;
                    objArray2[index5] = (object) name;
                    int index6 = 1;
                    // ISSUE: variable of a boxed type
                    __Boxed<int> local3 = (ValueType) this._poolId;
                    objArray2[index6] = (object) local3;
                    Logger.LogError(fmt2, objArray2);
                    this.EndSession();
                  }
                }
                else
                {
                  string fmt = "Unable to contact host \"{0}\" \r\n{1}";
                  object[] objArray = new object[2];
                  int index1 = 0;
                  string str2 = DwmHost.UuidToName(key, this._poolId);
                  objArray[index1] = (object) str2;
                  int index2 = 1;
                  string message = ex.Message;
                  objArray[index2] = (object) message;
                  Logger.LogError(fmt, objArray);
                }
              }
              catch (Exception ex)
              {
                Logger.LogException(ex);
                Thread.Sleep(30000);
              }
            }
            XenCollector.EnqueueData(dwmHostMetrics, dwmVmMetrics);
          }
          finally
          {
            Monitor.Exit(obj);
          }
        }
        else
        {
          if (this._lastLicenseCheck == DateTime.MinValue)
            this._lastLicenseCheck = DateTime.UtcNow;
          if ((DateTime.UtcNow - this._lastLicenseCheck).TotalSeconds < 300.0)
            return;
          bool flag = true;
          foreach (KeyValuePair<XenRef<Host>, Host> keyValuePair in Host.get_all_records(this._session))
            flag &= this.CheckHostLicense(keyValuePair.Value);
          if (flag)
          {
            if (this._pool != null)
              this._pool.UpdateDiscoveryStatus(DiscoveryStatus.New);
          }
          else
          {
            string fmt = "Pool {0} ({1}) does not have a valid license.  Checking again in 5 minutes.";
            object[] objArray = new object[2];
            int index1 = 0;
            string name = this._pool.Name;
            objArray[index1] = (object) name;
            int index2 = 1;
            string uuid = this._pool.Uuid;
            objArray[index2] = (object) uuid;
            Logger.Trace(fmt, objArray);
          }
          this._lastLicenseCheck = DateTime.UtcNow;
        }
      }
      else
      {
        if (this._lastValidPoolCheck == DateTime.MinValue)
          this._lastValidPoolCheck = DateTime.UtcNow;
        TimeSpan timeSpan = DateTime.UtcNow - this._lastValidPoolCheck;
        if (timeSpan.TotalSeconds >= 600.0)
        {
          if (this._pool == null)
            return;
          this._pool.UpdateDiscoveryStatus(DiscoveryStatus.New);
        }
        else
        {
          if (timeSpan.TotalSeconds >= 5.0)
            return;
          Logger.Trace("Don't have a valid pool.  Trying again in 10 minutes.");
        }
      }
    }

    /// <summary>
    /// Retrieve the host and VM metrics for a Xen 5.x physical host.
    /// 
    /// </summary>
    /// <param name="item">HostCacheItem instance containing both the Xen
    ///             host and Kirkwood host whose metrics will be retrieved.</param><param name="dwmHostMetrics">Collection to which the host metrics
    ///             will be added.</param><param name="dwmVmMetrics">Collection to which the VM metrics will
    ///             be added.</param>
    private void GetXen5Metrics(HostCacheItem item, DwmHostMetricCollection dwmHostMetrics, DwmVmMetricCollection dwmVmMetrics)
    {
      if (item == null || item.dwmHost == null || !item.dwmHost.Enabled)
        return;
      double totalMinutes = (XenCollector.GetHostServerTime(this._session, item.serverOpaqueRef) - DateTime.UtcNow).TotalMinutes;
      if (Math.Abs(totalMinutes) > 1.0)
        Logger.LogWarning(string.Format("The Host ({0}) server time is approximately {1} minutes {2} the WLB server time. Please synchronize the server clocks.", (object) item.dwmHost.Name, (object) Math.Round(totalMinutes, 0).ToString(), totalMinutes <= 0.0 ? (object) "behind" : (object) "ahead of"));
      Stream hostData = this.GetHostData(this._session, item.dwmHost.IPAddress, item.dwmHost.MetricsLastRetrieved, totalMinutes);
      if (hostData == null)
        return;
      DwmHostMetricCollection hostMetrics;
      DwmVmMetricCollection vmMetrics;
      DateTime dateTime = this.ParseMetrics(hostData, totalMinutes, out hostMetrics, out vmMetrics);
      if (hostMetrics != null && hostMetrics.Count > 0)
        dwmHostMetrics.AddRange((IEnumerable<DwmHostMetric>) hostMetrics);
      if (vmMetrics != null && vmMetrics.Count > 0)
        dwmVmMetrics.AddRange((IEnumerable<DwmVmMetric>) vmMetrics);
      if (!(dateTime == DateTime.MinValue))
      {
        if (!(dateTime > DateTime.UtcNow))
        {
          if ((DateTime.UtcNow - dateTime).TotalSeconds <= 60.0)
          {
            item.dwmHost.MetricsLastRetrieved = DateTime.UtcNow;
            return;
          }
        }
      }
      try
      {
        item.dwmHost.MetricsLastRetrieved = Host.get_servertime(this._session, item.serverOpaqueRef);
      }
      catch (Failure ex)
      {
        Logger.LogException((Exception) ex);
      }
      catch (SocketException ex)
      {
        Logger.LogException((Exception) ex);
      }
      catch (WebException ex)
      {
        Logger.LogException((Exception) ex);
      }
    }

    private double AverageMetric(List<DeviceMetric> metrics)
    {
      if (metrics == null || metrics.Count == 0)
        throw new ArgumentException("Invalid argument passed.", "metrics");
      double num = 0.0;
      foreach (DeviceMetric deviceMetric in metrics)
        num += deviceMetric.MetricValue;
      return num / (double) metrics.Count;
    }

    /// <summary>
    /// Parse the string of XML metrics into individual host and VM metrics
    ///             all stored them in collections.
    /// 
    /// </summary>
    /// <param name="xmlStats">Raw XML metrics to parse.</param><param name="hostServerOffsetMinutes">Difference in minutes between
    ///             the WLB Server system clock and the Host server's system clock</param><param name="hostMetrics">On output will contain one DwmHostMetric
    ///             instance for each host metric entry in the raw XML.</param><param name="vmMetrics">On output will contain one DwmVmMetric
    ///             instance for each VM metric entry in the raw XML</param>
    /// <returns>
    /// The UTC time stamp of the last set of metrics that was
    ///             parsed.
    /// </returns>
    public DateTime ParseMetrics(Stream xmlStats, double hostServerOffsetMinutes, out DwmHostMetricCollection hostMetrics, out DwmVmMetricCollection vmMetrics)
    {
      XenMetricsIndex xenMetricsIndex1 = (XenMetricsIndex) null;
      XenMetricsIndex xenMetricsIndex2 = (XenMetricsIndex) null;
      Dictionary<int, XenMetricsIndex> dictionary = new Dictionary<int, XenMetricsIndex>();
      DateTime dateTime1 = DateTime.MinValue;
      DateTime dateTime2 = DateTime.MinValue;
      VmCacheItem vmCacheItem = (VmCacheItem) null;
      string str1 = string.Empty;
      char ch = ':';
      hostMetrics = new DwmHostMetricCollection();
      vmMetrics = new DwmVmMetricCollection();
      using (XmlReader xmlReader = XmlReader.Create(xmlStats))
      {
        int num1 = (int) xmlReader.MoveToContent();
        int num2 = 0;
        while (!xmlReader.EOF)
        {
          if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "entry" && xmlReader.Name != "row")
            xmlReader.Read();
          else if (xmlReader.Name == "entry")
          {
            string str2 = xmlReader.ReadElementContentAsString();
            char[] chArray = new char[1];
            int index = 0;
            int num3 = (int) ch;
            chArray[index] = (char) num3;
            string[] strArray = str2.Split(chArray);
            if (strArray[1] == "host")
            {
              if (num2 == 0)
              {
                string str3 = strArray[2];
                if (!this.HostCacheContainsKey(str3))
                  this.DiscoverHost(this._session, str3, true, true);
                xenMetricsIndex1 = new XenMetricsIndex(str3);
              }
              string str4 = strArray[3];
              if (str4 == "memory_total_kib")
                xenMetricsIndex1.TotalMemRowIndex = num2;
              else if (str4 == "memory_free_kib")
                xenMetricsIndex1.FreeMemRowIndex = num2;
              else if (str4.StartsWith("pif_eth") && !str4.Contains("."))
              {
                int length1 = "pif_eth".Length;
                int length2 = str4.Length - length1 - "_rx".Length;
                if (str4.EndsWith("_rx"))
                {
                  int result;
                  if (int.TryParse(str4.Substring(length1, length2), out result))
                  {
                    MetricItemIndex metricItemIndex = new MetricItemIndex(result, num2);
                    xenMetricsIndex1.PifIoReadRowIndex.Add(metricItemIndex);
                  }
                }
                else
                {
                  int result;
                  if (str4.EndsWith("_tx") && int.TryParse(str4.Substring(length1, length2), out result))
                  {
                    MetricItemIndex metricItemIndex = new MetricItemIndex(result, num2);
                    xenMetricsIndex1.PifIoWriteRowIndex.Add(metricItemIndex);
                  }
                }
              }
              else if (str4.StartsWith("cpu"))
              {
                int result;
                if (int.TryParse(str4.Substring(3), out result))
                {
                  MetricItemIndex metricItemIndex = new MetricItemIndex(result, num2);
                  xenMetricsIndex1.CpuUtilizationRowIndex.Add(metricItemIndex);
                }
              }
              else if (str4 == "loadavg")
                xenMetricsIndex1.LoadAverageRowIndex = num2;
            }
            else if (strArray[1] == "vm")
            {
              string str3 = strArray[2];
              if (str1 != str3)
              {
                vmCacheItem = this.VmCacheGetValue(str3);
                if (vmCacheItem == null)
                {
                  this.DiscoverVm(this._session, str3, true);
                  vmCacheItem = this.VmCacheGetValue(str3);
                }
                xenMetricsIndex2 = new XenMetricsIndex(xenMetricsIndex1.HostUuid, str3);
                dictionary.Add(num2, xenMetricsIndex2);
                str1 = str3;
              }
              string str4 = strArray[3];
              if (str4.StartsWith("cpu"))
              {
                int result;
                if (int.TryParse(str4.Substring(3), out result))
                {
                  MetricItemIndex metricItemIndex = new MetricItemIndex(result, num2);
                  xenMetricsIndex2.CpuUtilizationRowIndex.Add(metricItemIndex);
                }
              }
              else if (str4.StartsWith("vif_"))
              {
                int length1 = "vif_".Length;
                int length2 = str4.Length - length1 - "_rx".Length;
                if (str4.EndsWith("_rx"))
                {
                  int result;
                  if (int.TryParse(str4.Substring(length1, length2), out result))
                  {
                    MetricItemIndex metricItemIndex = new MetricItemIndex(result, num2);
                    xenMetricsIndex2.PifIoReadRowIndex.Add(metricItemIndex);
                  }
                }
                else
                {
                  int result;
                  if (str4.EndsWith("_tx") && int.TryParse(str4.Substring(length1, length2), out result))
                  {
                    MetricItemIndex metricItemIndex = new MetricItemIndex(result, num2);
                    xenMetricsIndex2.PifIoWriteRowIndex.Add(metricItemIndex);
                  }
                }
              }
              else if (str4.StartsWith("vbd_"))
              {
                int length1 = "vbd_".Length;
                int length2 = str4.LastIndexOf('_') - length1;
                string deviceName = str4.Substring(length1, length2);
                if (deviceName.StartsWith("hd") || deviceName.StartsWith("xvd"))
                {
                  MetricItemIndex metricItemIndex = new MetricItemIndex((int) deviceName.ToCharArray()[deviceName.Length - 1] - 97, num2);
                  metricItemIndex.IsNetworkedDevice = this.IsVbdNetworked(vmCacheItem, deviceName);
                  if (str4.EndsWith("_read"))
                    xenMetricsIndex2.VbdIoReadRowIndex.Add(metricItemIndex);
                  else if (str4.EndsWith("_write"))
                    xenMetricsIndex2.VbdIoWriteRowIndex.Add(metricItemIndex);
                }
              }
              else if (str4 == "memory")
                xenMetricsIndex2.TotalMemRowIndex = num2;
              else if (str4 == "memory_target")
                xenMetricsIndex2.TargetMemRowIndex = num2;
              else if (str4 == "memory_internal_free")
                xenMetricsIndex2.FreeMemRowIndex = num2;
              else if (str4.StartsWith("runstate_"))
              {
                string key = str4;
                if (key != null)
                {
                  // ISSUE: reference to a compiler-generated field
                  if (XenCollector.\u003C\u003Ef__switch\u0024map0 == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    XenCollector.\u003C\u003Ef__switch\u0024map0 = new Dictionary<string, int>(6)
                    {
                      {
                        "runstate_blocked",
                        0
                      },
                      {
                        "runstate_partial_run",
                        1
                      },
                      {
                        "runstate_fullrun",
                        2
                      },
                      {
                        "runstate_partial_contention",
                        3
                      },
                      {
                        "runstate_concurrency_hazard",
                        4
                      },
                      {
                        "runstate_full_contention",
                        5
                      }
                    };
                  }
                  int num4;
                  // ISSUE: reference to a compiler-generated field
                  if (XenCollector.\u003C\u003Ef__switch\u0024map0.TryGetValue(key, out num4))
                  {
                    switch (num4)
                    {
                      case 0:
                        xenMetricsIndex2.RunstateBlocked = num2;
                        break;
                      case 1:
                        xenMetricsIndex2.RunstatePartialRun = num2;
                        break;
                      case 2:
                        xenMetricsIndex2.RunstateFullRun = num2;
                        break;
                      case 3:
                        xenMetricsIndex2.RunstatePartialContention = num2;
                        break;
                      case 4:
                        xenMetricsIndex2.RunstateConcurrencyHazard = num2;
                        break;
                      case 5:
                        xenMetricsIndex2.RunstateFullContention = num2;
                        break;
                    }
                  }
                }
              }
            }
            ++num2;
          }
          else if (xmlReader.Name == "row")
          {
            xmlReader.ReadToDescendant("t");
            dateTime1 = this.ConvertToDateTime(xmlReader.ReadElementContentAsDouble());
            DateTime dateTime3 = dateTime1.AddMinutes(-hostServerOffsetMinutes);
            DwmHostMetric dwmHostMetric = (DwmHostMetric) null;
            str1 = string.Empty;
            DwmVmMetric dwmVmMetric = (DwmVmMetric) null;
            for (int key = 0; key < num2; ++key)
            {
              if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "v")
              {
                double num3 = xmlReader.ReadElementContentAsDouble();
                if (double.IsNaN(num3) || double.IsInfinity(num3) || num3 < 0.0)
                {
                  if (key != 0)
                    num3 = 0.0;
                  else
                    break;
                }
                if (key == 0)
                {
                  dwmHostMetric = new DwmHostMetric(xenMetricsIndex1.HostUuid, this._pool.Uuid);
                  hostMetrics.Add(dwmHostMetric);
                  int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                  string fmt1 = "Got metrics for host {0} ({1})";
                  object[] objArray1 = new object[2];
                  int index1 = 0;
                  // ISSUE: variable of a boxed type
                  __Boxed<int> local1 = (ValueType) dwmHostMetric.HostId;
                  objArray1[index1] = (object) local1;
                  int index2 = 1;
                  string hostUuid = dwmHostMetric.HostUuid;
                  objArray1[index2] = (object) hostUuid;
                  XenCollectorBase.Trace(num4 != 0, fmt1, objArray1);
                  int num5 = XenCollectorBase._traceEnabled ? 1 : 0;
                  string fmt2 = "   TStamp={0}";
                  object[] objArray2 = new object[1];
                  int index3 = 0;
                  // ISSUE: variable of a boxed type
                  __Boxed<DateTime> local2 = (ValueType) dateTime1;
                  objArray2[index3] = (object) local2;
                  XenCollectorBase.Trace(num5 != 0, fmt2, objArray2);
                  dwmHostMetric.TStamp = dateTime3;
                }
                bool flag1 = false;
                if (xenMetricsIndex1.TotalMemRowIndex == key)
                {
                  dwmHostMetric.TotalMem = (long) num3 * 1024L;
                  int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                  string fmt = "   Total mem={0}";
                  object[] objArray = new object[1];
                  int index = 0;
                  // ISSUE: variable of a boxed type
                  __Boxed<long> local = (ValueType) dwmHostMetric.TotalMem;
                  objArray[index] = (object) local;
                  XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                  flag1 = true;
                }
                if (xenMetricsIndex1.FreeMemRowIndex == key)
                {
                  dwmHostMetric.FreeMem = (long) num3 * 1024L;
                  int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                  string fmt = "   Free mem ={0}";
                  object[] objArray = new object[1];
                  int index = 0;
                  // ISSUE: variable of a boxed type
                  __Boxed<long> local = (ValueType) dwmHostMetric.FreeMem;
                  objArray[index] = (object) local;
                  XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                  flag1 = true;
                }
                if (!flag1)
                {
                  for (int index1 = 0; index1 < xenMetricsIndex1.PifIoReadRowIndex.Count; ++index1)
                  {
                    if (xenMetricsIndex1.PifIoReadRowIndex[index1].RowIndex == key)
                    {
                      int deviceNumber = xenMetricsIndex1.PifIoReadRowIndex[index1].DeviceNumber;
                      DeviceMetric deviceMetric = new DeviceMetric(deviceNumber, num3);
                      dwmHostMetric.PifIoRead.Add(deviceMetric);
                      int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                      string fmt = "   Pif {0} reads={1}";
                      object[] objArray = new object[2];
                      int index2 = 0;
                      // ISSUE: variable of a boxed type
                      __Boxed<int> local1 = (ValueType) deviceNumber;
                      objArray[index2] = (object) local1;
                      int index3 = 1;
                      // ISSUE: variable of a boxed type
                      __Boxed<double> local2 = (ValueType) num3;
                      objArray[index3] = (object) local2;
                      XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                      if (dwmHostMetric.PifIoRead.Count == xenMetricsIndex1.PifIoReadRowIndex.Count)
                        dwmHostMetric.AvgPifIoReadPerSecond = this.AverageMetric(dwmHostMetric.PifIoRead);
                      flag1 = true;
                    }
                  }
                }
                if (!flag1)
                {
                  for (int index1 = 0; index1 < xenMetricsIndex1.PifIoWriteRowIndex.Count; ++index1)
                  {
                    if (xenMetricsIndex1.PifIoWriteRowIndex[index1].RowIndex == key)
                    {
                      int deviceNumber = xenMetricsIndex1.PifIoWriteRowIndex[index1].DeviceNumber;
                      DeviceMetric deviceMetric = new DeviceMetric(deviceNumber, num3);
                      dwmHostMetric.PifIoWrite.Add(deviceMetric);
                      int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                      string fmt = "   Pif {0} write={1}";
                      object[] objArray = new object[2];
                      int index2 = 0;
                      // ISSUE: variable of a boxed type
                      __Boxed<int> local1 = (ValueType) deviceNumber;
                      objArray[index2] = (object) local1;
                      int index3 = 1;
                      // ISSUE: variable of a boxed type
                      __Boxed<double> local2 = (ValueType) num3;
                      objArray[index3] = (object) local2;
                      XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                      if (dwmHostMetric.PifIoWrite.Count == xenMetricsIndex1.PifIoWriteRowIndex.Count)
                        dwmHostMetric.AvgPifIoWritePerSecond = this.AverageMetric(dwmHostMetric.PifIoWrite);
                      flag1 = true;
                    }
                  }
                }
                if (!flag1)
                {
                  for (int index1 = 0; index1 < xenMetricsIndex1.CpuUtilizationRowIndex.Count; ++index1)
                  {
                    if (xenMetricsIndex1.CpuUtilizationRowIndex[index1].RowIndex == key)
                    {
                      int deviceNumber = xenMetricsIndex1.CpuUtilizationRowIndex[index1].DeviceNumber;
                      DeviceMetric deviceMetric = new DeviceMetric(deviceNumber, num3);
                      dwmHostMetric.CpuUtilization.Add(deviceMetric);
                      int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                      string fmt = "   CPU {0} utilization={1}";
                      object[] objArray = new object[2];
                      int index2 = 0;
                      // ISSUE: variable of a boxed type
                      __Boxed<int> local1 = (ValueType) deviceNumber;
                      objArray[index2] = (object) local1;
                      int index3 = 1;
                      // ISSUE: variable of a boxed type
                      __Boxed<double> local2 = (ValueType) num3;
                      objArray[index3] = (object) local2;
                      XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                      if (dwmHostMetric.CpuUtilization.Count == xenMetricsIndex1.CpuUtilizationRowIndex.Count)
                        dwmHostMetric.AvgCpuUtilization = this.AverageMetric(dwmHostMetric.CpuUtilization);
                      flag1 = true;
                    }
                  }
                }
                if (xenMetricsIndex1.LoadAverageRowIndex == key)
                {
                  dwmHostMetric.LoadAverage = num3;
                  int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                  string fmt = "   Load Average={0}";
                  object[] objArray = new object[1];
                  int index = 0;
                  // ISSUE: variable of a boxed type
                  __Boxed<double> local = (ValueType) dwmHostMetric.LoadAverage;
                  objArray[index] = (object) local;
                  XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                  flag1 = true;
                }
                if (!flag1)
                {
                  if (dictionary.ContainsKey(key))
                  {
                    xenMetricsIndex2 = dictionary[key];
                    if (str1 != xenMetricsIndex2.VmUuid)
                    {
                      dwmVmMetric = new DwmVmMetric(dwmHostMetric.HostUuid, this._pool.Uuid, xenMetricsIndex2.VmUuid);
                      int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                      string fmt = "   Got metrics for VM {0} ({1})";
                      object[] objArray = new object[2];
                      int index1 = 0;
                      // ISSUE: variable of a boxed type
                      __Boxed<int> local = (ValueType) dwmVmMetric.VMId;
                      objArray[index1] = (object) local;
                      int index2 = 1;
                      string vmUuid = dwmVmMetric.VMUuid;
                      objArray[index2] = (object) vmUuid;
                      XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                      str1 = xenMetricsIndex2.VmUuid;
                      vmMetrics.Add(dwmVmMetric);
                      dwmVmMetric.TStamp = dwmHostMetric.TStamp;
                    }
                  }
                  if (xenMetricsIndex2.TotalMemRowIndex == key)
                  {
                    dwmVmMetric.TotalMem = (long) num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      TotalMem={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<long> local = (ValueType) dwmVmMetric.TotalMem;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag1 = true;
                  }
                  if (xenMetricsIndex2.FreeMemRowIndex == key)
                  {
                    dwmVmMetric.FreeMem = (long) num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      FreeMem ={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<long> local = (ValueType) dwmVmMetric.FreeMem;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag1 = true;
                  }
                  if (xenMetricsIndex2.TargetMemRowIndex == key)
                  {
                    dwmVmMetric.TargetMem = (long) num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      TargetMem ={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<long> local = (ValueType) dwmVmMetric.TargetMem;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag1 = true;
                  }
                  if (!flag1)
                  {
                    for (int index1 = 0; index1 < xenMetricsIndex2.PifIoReadRowIndex.Count; ++index1)
                    {
                      if (xenMetricsIndex2.PifIoReadRowIndex[index1].RowIndex == key)
                      {
                        int deviceNumber = xenMetricsIndex2.PifIoReadRowIndex[index1].DeviceNumber;
                        DeviceMetric deviceMetric = new DeviceMetric(deviceNumber, num3);
                        dwmVmMetric.PifIoRead.Add(deviceMetric);
                        int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                        string fmt = "      Vif {0} reads={1}";
                        object[] objArray = new object[2];
                        int index2 = 0;
                        // ISSUE: variable of a boxed type
                        __Boxed<int> local1 = (ValueType) deviceNumber;
                        objArray[index2] = (object) local1;
                        int index3 = 1;
                        // ISSUE: variable of a boxed type
                        __Boxed<double> local2 = (ValueType) num3;
                        objArray[index3] = (object) local2;
                        XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                        if (dwmVmMetric.PifIoRead.Count == xenMetricsIndex2.PifIoReadRowIndex.Count)
                          dwmVmMetric.AvgPifIoReadPerSecond = this.AverageMetric(dwmVmMetric.PifIoRead);
                        flag1 = true;
                      }
                    }
                  }
                  if (!flag1)
                  {
                    for (int index1 = 0; index1 < xenMetricsIndex2.PifIoWriteRowIndex.Count; ++index1)
                    {
                      if (xenMetricsIndex2.PifIoWriteRowIndex[index1].RowIndex == key)
                      {
                        int deviceNumber = xenMetricsIndex2.PifIoWriteRowIndex[index1].DeviceNumber;
                        DeviceMetric deviceMetric = new DeviceMetric(deviceNumber, num3);
                        dwmVmMetric.PifIoWrite.Add(deviceMetric);
                        int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                        string fmt = "      Vif {0} writes={1}";
                        object[] objArray = new object[2];
                        int index2 = 0;
                        // ISSUE: variable of a boxed type
                        __Boxed<int> local1 = (ValueType) deviceNumber;
                        objArray[index2] = (object) local1;
                        int index3 = 1;
                        // ISSUE: variable of a boxed type
                        __Boxed<double> local2 = (ValueType) num3;
                        objArray[index3] = (object) local2;
                        XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                        if (dwmVmMetric.PifIoWrite.Count == xenMetricsIndex2.PifIoWriteRowIndex.Count)
                          dwmVmMetric.AvgPifIoWritePerSecond = this.AverageMetric(dwmVmMetric.PifIoWrite);
                        flag1 = true;
                      }
                    }
                  }
                  if (!flag1)
                  {
                    for (int index1 = 0; index1 < xenMetricsIndex2.VbdIoReadRowIndex.Count; ++index1)
                    {
                      if (xenMetricsIndex2.VbdIoReadRowIndex[index1].RowIndex == key)
                      {
                        int deviceNumber = xenMetricsIndex2.VbdIoReadRowIndex[index1].DeviceNumber;
                        bool isNetworkedDevice = xenMetricsIndex2.VbdIoReadRowIndex[index1].IsNetworkedDevice;
                        DeviceMetric deviceMetric = new DeviceMetric(deviceNumber, num3);
                        dwmVmMetric.VbdIoRead.Add(deviceMetric);
                        int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                        string fmt = "      Vbd {0} reads={1}";
                        object[] objArray = new object[2];
                        int index2 = 0;
                        // ISSUE: variable of a boxed type
                        __Boxed<int> local1 = (ValueType) deviceNumber;
                        objArray[index2] = (object) local1;
                        int index3 = 1;
                        // ISSUE: variable of a boxed type
                        __Boxed<double> local2 = (ValueType) num3;
                        objArray[index3] = (object) local2;
                        XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                        if (isNetworkedDevice)
                          dwmVmMetric.TotalVbdNetworkReadPerSecond += deviceMetric.MetricValue;
                        if (dwmVmMetric.VbdIoRead.Count == xenMetricsIndex2.VbdIoReadRowIndex.Count)
                          dwmVmMetric.AvgVbdIoReadPerSecond = this.AverageMetric(dwmVmMetric.VbdIoRead);
                        flag1 = true;
                      }
                    }
                  }
                  if (!flag1)
                  {
                    for (int index1 = 0; index1 < xenMetricsIndex2.VbdIoWriteRowIndex.Count; ++index1)
                    {
                      if (xenMetricsIndex2.VbdIoWriteRowIndex[index1].RowIndex == key)
                      {
                        int deviceNumber = xenMetricsIndex2.VbdIoWriteRowIndex[index1].DeviceNumber;
                        bool isNetworkedDevice = xenMetricsIndex2.VbdIoReadRowIndex[index1].IsNetworkedDevice;
                        DeviceMetric deviceMetric = new DeviceMetric(deviceNumber, num3);
                        dwmVmMetric.VbdIoWrite.Add(deviceMetric);
                        int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                        string fmt = "      Vbd {0} writes={1}";
                        object[] objArray = new object[2];
                        int index2 = 0;
                        // ISSUE: variable of a boxed type
                        __Boxed<int> local1 = (ValueType) deviceNumber;
                        objArray[index2] = (object) local1;
                        int index3 = 1;
                        // ISSUE: variable of a boxed type
                        __Boxed<double> local2 = (ValueType) num3;
                        objArray[index3] = (object) local2;
                        XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                        if (isNetworkedDevice)
                          dwmVmMetric.TotalVbdNetworkWritePerSecond += deviceMetric.MetricValue;
                        if (dwmVmMetric.VbdIoWrite.Count == xenMetricsIndex2.VbdIoWriteRowIndex.Count)
                          dwmVmMetric.AvgVbdIoWritePerSecond = this.AverageMetric(dwmVmMetric.VbdIoWrite);
                        flag1 = true;
                      }
                    }
                  }
                  bool flag2;
                  if (!flag1)
                  {
                    for (int index1 = 0; index1 < xenMetricsIndex2.CpuUtilizationRowIndex.Count; ++index1)
                    {
                      if (xenMetricsIndex2.CpuUtilizationRowIndex[index1].RowIndex == key)
                      {
                        int deviceNumber = xenMetricsIndex2.CpuUtilizationRowIndex[index1].DeviceNumber;
                        DeviceMetric deviceMetric = new DeviceMetric(deviceNumber, num3);
                        dwmVmMetric.CpuUtilization.Add(deviceMetric);
                        int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                        string fmt = "      VCpu {0} utilization={1}";
                        object[] objArray = new object[2];
                        int index2 = 0;
                        // ISSUE: variable of a boxed type
                        __Boxed<int> local1 = (ValueType) deviceNumber;
                        objArray[index2] = (object) local1;
                        int index3 = 1;
                        // ISSUE: variable of a boxed type
                        __Boxed<double> local2 = (ValueType) num3;
                        objArray[index3] = (object) local2;
                        XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                        if (dwmVmMetric.CpuUtilization.Count == xenMetricsIndex2.CpuUtilizationRowIndex.Count)
                          dwmVmMetric.AvgCpuUtilization = this.AverageMetric(dwmVmMetric.CpuUtilization);
                        flag2 = true;
                      }
                    }
                  }
                  if (xenMetricsIndex2.RunstateBlocked == key)
                  {
                    dwmVmMetric.RunstateBlocked = num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      RunStateBlocked={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<double> local = (ValueType) dwmVmMetric.RunstateBlocked;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag2 = true;
                  }
                  if (xenMetricsIndex2.RunstatePartialRun == key)
                  {
                    dwmVmMetric.RunstatePartialRun = num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      RunstatePartialRun={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<double> local = (ValueType) dwmVmMetric.RunstatePartialRun;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag2 = true;
                  }
                  if (xenMetricsIndex2.RunstateFullRun == key)
                  {
                    dwmVmMetric.RunstateFullRun = num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      RunstateFullRun={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<double> local = (ValueType) dwmVmMetric.RunstateFullRun;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag2 = true;
                  }
                  if (xenMetricsIndex2.RunstatePartialContention == key)
                  {
                    dwmVmMetric.RunstatePartialContention = num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      RunstatePartialContention={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<double> local = (ValueType) dwmVmMetric.RunstatePartialContention;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag2 = true;
                  }
                  if (xenMetricsIndex2.RunstateConcurrencyHazard == key)
                  {
                    dwmVmMetric.RunstateConcurrencyHazard = num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      RunstateConcurrencyHazard={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<double> local = (ValueType) dwmVmMetric.RunstateConcurrencyHazard;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag2 = true;
                  }
                  if (xenMetricsIndex2.RunstateFullContention == key)
                  {
                    dwmVmMetric.RunstateFullContention = num3;
                    int num4 = XenCollectorBase._traceEnabled ? 1 : 0;
                    string fmt = "      RunstateFullContention={0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    // ISSUE: variable of a boxed type
                    __Boxed<double> local = (ValueType) dwmVmMetric.RunstateFullContention;
                    objArray[index] = (object) local;
                    XenCollectorBase.Trace(num4 != 0, fmt, objArray);
                    flag2 = true;
                  }
                }
              }
              else
                xmlReader.Read();
            }
          }
        }
      }
      return dateTime1;
    }

    private static Host GetHostRecord(Session session, string host)
    {
      Host host1 = (Host) null;
      try
      {
        host1 = Host.get_record(session, host);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return host1;
    }

    private static VM GetVmRecord(Session session, string vm)
    {
      VM vm1 = (VM) null;
      try
      {
        vm1 = VM.get_record(session, vm);
        vm1.opaque_ref = vm;
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return vm1;
    }

    private static Host_cpu GetHostCpuRecord(Session session, string hostCpu)
    {
      Host_cpu hostCpu1 = (Host_cpu) null;
      try
      {
        hostCpu1 = Host_cpu.get_record(session, hostCpu);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return hostCpu1;
    }

    private static PIF GetPifRecord(Session session, string pif)
    {
      PIF pif1 = (PIF) null;
      try
      {
        pif1 = PIF.get_record(session, pif);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return pif1;
    }

    private static PBD GetPbdRecord(Session session, string pbd)
    {
      PBD pbd1 = (PBD) null;
      try
      {
        pbd1 = PBD.get_record(session, pbd);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return pbd1;
    }

    private static XenAPI.SR GetSrRecord(Session session, string sr)
    {
      XenAPI.SR sr1 = (XenAPI.SR) null;
      try
      {
        sr1 = XenAPI.SR.get_record(session, sr);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return sr1;
    }

    private static VDI GetVdiRecord(Session session, string vdi)
    {
      VDI vdi1 = (VDI) null;
      try
      {
        vdi1 = VDI.get_record(session, vdi);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return vdi1;
    }

    private static Network GetNetworkRecord(Session session, string network)
    {
      Network network1 = (Network) null;
      try
      {
        network1 = Network.get_record(session, network);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return network1;
    }

    private static VIF GetVifRecord(Session session, string vif)
    {
      VIF vif1 = (VIF) null;
      try
      {
        vif1 = VIF.get_record(session, vif);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return vif1;
    }

    private static VBD GetVbdRecord(Session session, string vbd)
    {
      VBD vbd1 = (VBD) null;
      try
      {
        vbd1 = VBD.get_record(session, vbd);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return vbd1;
    }

    private static DateTime GetHostServerTime(Session session, string host)
    {
      DateTime dateTime = DateTime.MinValue;
      try
      {
        dateTime = Host.get_servertime(session, host);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return dateTime;
    }

    private static Host_metrics GetHostMetrics(Session session, string host)
    {
      Host_metrics hostMetrics = (Host_metrics) null;
      try
      {
        hostMetrics = Host_metrics.get_record(session, host);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return hostMetrics;
    }

    private static VM_metrics GetVmMetrics(Session session, string vm)
    {
      VM_metrics vmMetrics = (VM_metrics) null;
      try
      {
        vmMetrics = VM_metrics.get_record(session, vm);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return vmMetrics;
    }

    private static VM_guest_metrics GetVmGuestMetrics(Session session, string vm)
    {
      VM_guest_metrics vmGuestMetrics = (VM_guest_metrics) null;
      try
      {
        vmGuestMetrics = VM_guest_metrics.get_record(session, vm);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return vmGuestMetrics;
    }

    private static XenRef<PIF_metrics> GetXrPifMetrics(Session session, string pif)
    {
      XenRef<PIF_metrics> xenRef = (XenRef<PIF_metrics>) null;
      try
      {
        xenRef = PIF.get_metrics(session, pif);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return xenRef;
    }

    private static PIF_metrics GetPifMetrics(Session session, string pif)
    {
      PIF_metrics pifMetrics = (PIF_metrics) null;
      try
      {
        pifMetrics = PIF_metrics.get_record(session, pif);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return pifMetrics;
    }

    private static XenRef<VIF_metrics> GetXrVifMetrics(Session session, string vif)
    {
      XenRef<VIF_metrics> xenRef = (XenRef<VIF_metrics>) null;
      try
      {
        xenRef = VIF.get_metrics(session, vif);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return xenRef;
    }

    private static VIF_metrics GetVifMetrics(Session session, string vif)
    {
      VIF_metrics vifMetrics = (VIF_metrics) null;
      try
      {
        vifMetrics = VIF_metrics.get_record(session, vif);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return vifMetrics;
    }

    private static XenRef<VBD_metrics> GetXrVbdMetrics(Session session, string vbd)
    {
      XenRef<VBD_metrics> xenRef = (XenRef<VBD_metrics>) null;
      try
      {
        xenRef = VBD.get_metrics(session, vbd);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return xenRef;
    }

    private static VBD_metrics GetVbdMetrics(Session session, string vbd)
    {
      VBD_metrics vbdMetrics = (VBD_metrics) null;
      try
      {
        vbdMetrics = VBD_metrics.get_record(session, vbd);
      }
      catch (Failure ex)
      {
        XenCollector.HandleFailure(ex);
      }
      return vbdMetrics;
    }

    private static void HandleFailure(Failure f)
    {
      if (f.ErrorDescription != null && f.ErrorDescription.Count > 0 && Localization.Compare(f.ErrorDescription[0], "HANDLE_INVALID", true) != 0)
        throw f;
    }

    /// <summary>
    /// Place metrics data on the queue that is share by the data collectors
    ///             and data writers.
    /// 
    /// </summary>
    /// <param name="dwmHostMetrics">Host metrics to add to the queue.</param><param name="dwmVmMetrics">VM metrics to add to the queue.</param>
    private static void EnqueueData(DwmHostMetricCollection dwmHostMetrics, DwmVmMetricCollection dwmVmMetrics)
    {
      bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.QueueManagementTrace);
      if (dwmHostMetrics.Count > 0)
      {
        QueueManager.Enqueue((object) dwmHostMetrics);
        int num = valueAsBool ? 1 : 0;
        string fmt = "Added DwmHostMetricCollection instance with {0} items to the queue";
        object[] objArray = new object[1];
        int index = 0;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) dwmHostMetrics.Count;
        objArray[index] = (object) local;
        XenCollectorBase.Trace(num != 0, fmt, objArray);
      }
      if (dwmVmMetrics.Count <= 0)
        return;
      QueueManager.Enqueue((object) dwmVmMetrics);
      int num1 = valueAsBool ? 1 : 0;
      string fmt1 = "Added DwmVmMetricCollection instance with {0} items to the queue";
      object[] objArray1 = new object[1];
      int index1 = 0;
      // ISSUE: variable of a boxed type
      __Boxed<int> local1 = (ValueType) dwmVmMetrics.Count;
      objArray1[index1] = (object) local1;
      XenCollectorBase.Trace(num1 != 0, fmt1, objArray1);
    }

    /// <summary>
    /// Determine the pools, hosts and virtual machines know to the active
    ///             session.  Update the database with this information.
    /// 
    /// </summary>
    public void Discover()
    {
      if (!this.IsInitialized)
        throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the Discover method", DwmErrorCode.NotInitialized, (Exception) null);
      DwmPool dwmPool = this.Discover(this._session);
      if (dwmPool == null)
        return;
      dwmPool.Save();
      dwmPool.SavePoolChildren();
    }

    /// <summary>
    /// Converting a System.DateTime value to a UNIX time stamp
    /// 
    /// </summary>
    /// <param name="value">UTC date and time to convert</param>
    /// <returns>
    /// Date as a UNIX time stamp.
    /// </returns>
    private double ConvertToTimestamp(DateTime value)
    {
      return (double) (long) (value - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
    }

    /// <summary>
    /// Converting a UNIX time stamp value to a System.DateTime.
    /// 
    /// </summary>
    /// <param name="value">UNIX time stamp convert</param>
    /// <returns>
    /// UNIX time stamp as a date time.
    /// </returns>
    private DateTime ConvertToDateTime(double value)
    {
      return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value);
    }

    /// <summary>
    /// Retrieve metrics for all the specified host since the specified
    ///             time.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor for
    ///             which metrics will be retrieved.</param><param name="ipAddress">TCP/IP address of the host.</param><param name="getDataSince">UTC time stamp for how far back in time
    ///             metrics should be retrieved.</param><param name="hostServerOffsetMinutes">Number of minutes between WLB
    ///             server's UTC and Hosts server time</param>
    /// <returns>
    /// Stream of XML containing the requested host metrics
    /// 
    /// </returns>
    private Stream GetHostData(Session session, string ipAddress, DateTime getDataSince, double hostServerOffsetMinutes)
    {
      double num = this.ConvertToTimestamp(!(getDataSince != DateTime.MinValue) ? DateTime.UtcNow.AddMinutes(hostServerOffsetMinutes).AddMinutes(-2.0) : getDataSince);
      string fmt = "{0}://{1}:{2}/rrd_updates?session_id={3}&cf=AVERAGE&start={4}&host=true";
      object[] objArray = new object[5];
      int index1 = 0;
      string str1 = this._protocol;
      objArray[index1] = (object) str1;
      int index2 = 1;
      string str2 = ipAddress;
      objArray[index2] = (object) str2;
      int index3 = 2;
      // ISSUE: variable of a boxed type
      __Boxed<int> local1 = (ValueType) this._hostPort;
      objArray[index3] = (object) local1;
      int index4 = 3;
      string uuid = session.uuid;
      objArray[index4] = (object) uuid;
      int index5 = 4;
      // ISSUE: variable of a boxed type
      __Boxed<double> local2 = (ValueType) num;
      objArray[index5] = (object) local2;
      return XenCollector.GetPoolData(Localization.Format(fmt, objArray));
    }

    /// <summary>
    /// Retrieve metrics for all the VMs on the specified host since the
    ///             specified time.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor for
    ///             which metrics will be retrieved.</param><param name="ipAddress">TCP/IP address of the host on which the VMs
    ///             are running.</param><param name="getDataSince">UTC time stamp for how far back in time
    ///             metrics should be retrieved.</param>
    /// <returns>
    /// Stream of XML containing the requested VM metrics
    /// 
    /// </returns>
    private Stream GetVmData(Session session, string ipAddress, DateTime getDataSince)
    {
      double num = this.ConvertToTimestamp(!(getDataSince != DateTime.MinValue) ? DateTime.UtcNow.AddMinutes(-2.0) : getDataSince);
      string fmt = "{0}://{1}:{2}/rrd_updates?session_id={3}&cf=AVERAGE&start={4}";
      object[] objArray = new object[5];
      int index1 = 0;
      string str1 = this._protocol;
      objArray[index1] = (object) str1;
      int index2 = 1;
      string str2 = ipAddress;
      objArray[index2] = (object) str2;
      int index3 = 2;
      // ISSUE: variable of a boxed type
      __Boxed<int> local1 = (ValueType) this._hostPort;
      objArray[index3] = (object) local1;
      int index4 = 3;
      string uuid = session.uuid;
      objArray[index4] = (object) uuid;
      int index5 = 4;
      // ISSUE: variable of a boxed type
      __Boxed<double> local2 = (ValueType) num;
      objArray[index5] = (object) local2;
      return XenCollector.GetPoolData(Localization.Format(fmt, objArray));
    }

    /// <summary>
    /// Retrieve pool metrics data by posting a request to the
    ///             specified URI.
    /// 
    /// </summary>
    /// <param name="uri">URI and query string to retrieve metrics.</param>
    /// <returns>
    /// String of XML containing the requested pool metrics
    /// 
    /// </returns>
    private static Stream GetPoolData(string uri)
    {
      try
      {
        int num1 = XenCollectorBase._traceEnabled ? 1 : 0;
        string fmt1 = "Requesting data from {0}";
        object[] objArray1 = new object[1];
        int index1 = 0;
        string str = uri;
        objArray1[index1] = (object) str;
        XenCollectorBase.Trace(num1 != 0, fmt1, objArray1);
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
        httpWebRequest.UserAgent = "WlbDataCollector";
        httpWebRequest.MaximumAutomaticRedirections = 4;
        httpWebRequest.MaximumResponseHeadersLength = 4;
        HttpWebResponse httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        int num2 = XenCollectorBase._traceEnabled ? 1 : 0;
        string fmt2 = "Content length is {0}";
        object[] objArray2 = new object[1];
        int index2 = 0;
        // ISSUE: variable of a boxed type
        __Boxed<long> local = (ValueType) httpWebResponse.ContentLength;
        objArray2[index2] = (object) local;
        XenCollectorBase.Trace(num2 != 0, fmt2, objArray2);
        int num3 = XenCollectorBase._traceEnabled ? 1 : 0;
        string fmt3 = "Content type is {0}";
        object[] objArray3 = new object[1];
        int index3 = 0;
        string contentType = httpWebResponse.ContentType;
        objArray3[index3] = (object) contentType;
        XenCollectorBase.Trace(num3 != 0, fmt3, objArray3);
        return httpWebResponse.GetResponseStream();
      }
      catch (WebException ex)
      {
        XenCollector.LogGetPoolDataError(uri, ex);
      }
      return (Stream) null;
    }

    /// <summary>
    /// Log an error connecting to a host.  We log errors every 2 minutes
    ///             so we don't flood the log.
    /// 
    /// </summary>
    /// <param name="uri">URI of host that had a connection error.</param><param name="e">Exception from the connection failure.</param>
    private static void LogGetPoolDataError(string uri, WebException e)
    {
      bool flag = true;
      if (XenCollector._hostHttpErrors == null)
        XenCollector._hostHttpErrors = new Dictionary<string, HostHttpError>();
      int num = uri.IndexOf("/rrd_updates?");
      string key = uri.Substring(0, num - 1);
      HostHttpError hostHttpError;
      if (!XenCollector._hostHttpErrors.TryGetValue(key, out hostHttpError))
      {
        hostHttpError = new HostHttpError();
        hostHttpError.HostUri = key;
        hostHttpError.ErrorCount = 0;
        XenCollector._hostHttpErrors.Add(key, hostHttpError);
      }
      else
      {
        ++hostHttpError.ErrorCount;
        if ((DateTime.UtcNow - hostHttpError.LastLogged).TotalSeconds < 3600.0)
          flag = false;
      }
      if (!flag)
        return;
      string fmt1 = "Exception requesting data from {0}";
      object[] objArray1 = new object[1];
      int index1 = 0;
      string str = uri;
      objArray1[index1] = (object) str;
      Logger.Trace(fmt1, objArray1);
      string fmt2 = "This error occurred {0} times.";
      object[] objArray2 = new object[1];
      int index2 = 0;
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (ValueType) hostHttpError.ErrorCount;
      objArray2[index2] = (object) local;
      Logger.Trace(fmt2, objArray2);
      hostHttpError.LastLogged = DateTime.UtcNow;
    }

    /// <summary>
    /// Determine the pools, hosts and virtual machines know to the active
    ///             session.  Update the database with this information.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param>
    /// <returns>
    /// DwmPool instance describing what was found on the host.
    /// 
    /// </returns>
    private DwmPool Discover(Session session)
    {
      int num = 0;
      Logger.Trace("Discover() - ");
      object obj1 = this._vmCacheLock;
      Monitor.Enter(obj1);
      try
      {
        this._vmCache = new Dictionary<string, VmCacheItem>();
        this._vmCache2 = new Dictionary<string, VM>();
      }
      finally
      {
        Monitor.Exit(obj1);
      }
      object obj2 = this._hostCacheLock;
      Monitor.Enter(obj2);
      try
      {
        this._hostCache = new Dictionary<string, HostCacheItem>();
      }
      finally
      {
        Monitor.Exit(obj2);
      }
      object obj3 = this._srCacheLock;
      Monitor.Enter(obj3);
      try
      {
        this._srCache = new Dictionary<string, XenAPI.SR>();
      }
      finally
      {
        Monitor.Exit(obj3);
      }
      object obj4 = this._pbdCacheLock;
      Monitor.Enter(obj4);
      try
      {
        this._pbdCache = new Dictionary<string, PBD>();
      }
      finally
      {
        Monitor.Exit(obj4);
      }
      object obj5 = this._netCacheLock;
      Monitor.Enter(obj5);
      try
      {
        this._networkCache = new Dictionary<string, Network>();
      }
      finally
      {
        Monitor.Exit(obj5);
      }
      object obj6 = this._pifCacheLock;
      Monitor.Enter(obj6);
      try
      {
        this._pifCache = new Dictionary<string, PIF>();
      }
      finally
      {
        Monitor.Exit(obj6);
      }
      object obj7 = this._vbdCacheLock;
      Monitor.Enter(obj7);
      try
      {
        this._vbdCache = new Dictionary<string, VBD>();
      }
      finally
      {
        Monitor.Exit(obj7);
      }
      object obj8 = this._vdiCacheLock;
      Monitor.Enter(obj8);
      try
      {
        this._vdiCache = new Dictionary<string, VDI>();
      }
      finally
      {
        Monitor.Exit(obj8);
      }
      object obj9 = this._vifCacheLock;
      Monitor.Enter(obj9);
      try
      {
        this._vifCache = new Dictionary<string, VIF>();
      }
      finally
      {
        Monitor.Exit(obj9);
      }
      try
      {
        foreach (KeyValuePair<XenRef<Pool>, Pool> keyValuePair1 in Pool.get_all_records(session))
        {
          Pool xenPool = keyValuePair1.Value;
          Pool.get_ha_configuration(session, keyValuePair1.Key.opaque_ref);
          string fmt1 = "       HA - enabled = {0}";
          object[] objArray1 = new object[1];
          int index1 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<bool> local1 = (ValueType) (bool) (xenPool.ha_enabled ? 1 : 0);
          objArray1[index1] = (object) local1;
          Logger.Trace(fmt1, objArray1);
          string fmt2 = "       HA - allow_overcommit = {0}";
          object[] objArray2 = new object[1];
          int index2 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<bool> local2 = (ValueType) (bool) (xenPool.ha_allow_overcommit ? 1 : 0);
          objArray2[index2] = (object) local2;
          Logger.Trace(fmt2, objArray2);
          string fmt3 = "       HA - host_failures_to_tolerate = {0}";
          object[] objArray3 = new object[1];
          int index3 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<long> local3 = (ValueType) xenPool.ha_host_failures_to_tolerate;
          objArray3[index3] = (object) local3;
          Logger.Trace(fmt3, objArray3);
          string fmt4 = "       HA - overcommitted = {0}";
          object[] objArray4 = new object[1];
          int index4 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<bool> local4 = (ValueType) (bool) (xenPool.ha_overcommitted ? 1 : 0);
          objArray4[index4] = (object) local4;
          Logger.Trace(fmt4, objArray4);
          string fmt5 = "       HA - plan_exists_for = {0}";
          object[] objArray5 = new object[1];
          int index5 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<long> local5 = (ValueType) xenPool.ha_plan_exists_for;
          objArray5[index5] = (object) local5;
          Logger.Trace(fmt5, objArray5);
          string[] haStatefiles = xenPool.ha_statefiles;
          string fmt6 = "       HA - statefiles = {0}";
          object[] objArray6 = new object[1];
          int index6 = 0;
          string str1 = haStatefiles == null || haStatefiles.Length <= 0 ? "none" : haStatefiles[0];
          objArray6[index6] = (object) str1;
          Logger.Trace(fmt6, objArray6);
          Host hostRecord = XenCollector.GetHostRecord(session, (string) keyValuePair1.Value.master);
          string fmt7 = "Pool:  master = {0}";
          object[] objArray7 = new object[1];
          int index7 = 0;
          string nameLabel1 = hostRecord.name_label;
          objArray7[index7] = (object) nameLabel1;
          Logger.Trace(fmt7, objArray7);
          string fmt8 = "       name   = {0}";
          object[] objArray8 = new object[1];
          int index8 = 0;
          string nameLabel2 = keyValuePair1.Value.name_label;
          objArray8[index8] = (object) nameLabel2;
          Logger.Trace(fmt8, objArray8);
          string fmt9 = "       desc   = {0}";
          object[] objArray9 = new object[1];
          int index9 = 0;
          string nameDescription = keyValuePair1.Value.name_description;
          objArray9[index9] = (object) nameDescription;
          Logger.Trace(fmt9, objArray9);
          string fmt10 = "       uuid   = {0}";
          object[] objArray10 = new object[1];
          int index10 = 0;
          string uuid = keyValuePair1.Value.uuid;
          objArray10[index10] = (object) uuid;
          Logger.Trace(fmt10, objArray10);
          foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair1.Value.other_config)
          {
            string fmt11 = "{0}={1}";
            object[] objArray11 = new object[2];
            int index11 = 0;
            string key = keyValuePair2.Key;
            objArray11[index11] = (object) key;
            int index12 = 1;
            string str2 = keyValuePair2.Value;
            objArray11[index12] = (object) str2;
            Logger.Trace(fmt11, objArray11);
          }
          this._pool = new DwmPool(keyValuePair1.Value.uuid, keyValuePair1.Value.name_label, DwmHypervisorType.XenServer);
          this._pool.Master = hostRecord.name_label;
          this._pool.Description = keyValuePair1.Value.name_description;
          this._pool.PrimaryPoolMasterAddr = this._hostName;
          this._pool.PrimaryPoolMasterPort = this._hostPort;
          this._pool.UserName = this._username;
          this._pool.Password = this._password;
          this._pool.TouchedBy = "__XEN_DATA_COLLECTOR__";
          this._pool.Enabled = this.GetWlbEnabled(session, xenPool);
          this._pool.DefaultSR = (string) keyValuePair1.Value.default_SR;
          int id = this._pool.Id;
          this._pool.Save();
          this._poolId = this._pool.Id;
          if (id != 0 && this._pool.Id != id)
            DwmPoolBase.RefreshCache();
          ++num;
        }
        this._pool.UpdateDiscoveryStatus(DiscoveryStatus.InProgress);
        DwmStorageRepositoryCollection repositoryCollection = new DwmStorageRepositoryCollection();
        Logger.Trace("Storage Repositories:");
        foreach (KeyValuePair<XenRef<XenAPI.SR>, XenAPI.SR> keyValuePair in XenAPI.SR.get_all_records(session))
        {
          DwmStorageRepository storageRepository = this.DiscoverSr(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, false);
          if (storageRepository != null)
            repositoryCollection.Add(storageRepository);
        }
        repositoryCollection.Save();
        DwmPbdCollection dwmPbdCollection = new DwmPbdCollection();
        Logger.Trace("Physical Block Devices:");
        foreach (KeyValuePair<XenRef<PBD>, PBD> keyValuePair in PBD.get_all_records(session))
        {
          DwmPbd dwmPbd = this.DiscoverPbd(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, false);
          if (dwmPbd != null)
            dwmPbdCollection.Add(dwmPbd);
        }
        dwmPbdCollection.Save();
        DwmNetworkCollection networkCollection = new DwmNetworkCollection();
        Logger.Trace("Networks:");
        foreach (KeyValuePair<XenRef<Network>, Network> keyValuePair in Network.get_all_records(session))
        {
          DwmNetwork dwmNetwork = this.DiscoverNetwork(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, false);
          if (dwmNetwork != null)
            networkCollection.Add(dwmNetwork);
        }
        networkCollection.Save();
        DwmPifCollection dwmPifCollection = new DwmPifCollection();
        Logger.Trace("Physical Interfaces:");
        foreach (KeyValuePair<XenRef<PIF>, PIF> keyValuePair in PIF.get_all_records(session))
        {
          DwmPif dwmPif = this.DiscoverPif(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, false);
          if (dwmPif != null)
            dwmPifCollection.Add(dwmPif);
        }
        dwmPifCollection.Save();
        Logger.Trace("Virtual Device Interfaces:");
        foreach (KeyValuePair<XenRef<VDI>, VDI> keyValuePair in VDI.get_all_records(session))
          this.DiscoverVdi(session, keyValuePair.Key.opaque_ref, keyValuePair.Value);
        DwmVifCollection dwmVifCollection = new DwmVifCollection();
        Logger.Trace("Virtual Interfaces:");
        foreach (KeyValuePair<XenRef<VIF>, VIF> keyValuePair in VIF.get_all_records(session))
        {
          DwmVif dwmVif = this.DiscoverVif(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, false);
          if (dwmVif != null)
            dwmVifCollection.Add(dwmVif);
        }
        dwmVifCollection.Save();
        DwmVbdCollection dwmVbdCollection = new DwmVbdCollection();
        Logger.Trace("Virtual Block Devices:");
        foreach (KeyValuePair<XenRef<VBD>, VBD> keyValuePair in VBD.get_all_records(session))
        {
          DwmVbd dwmVbd = this.DiscoverVbd(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, false);
          if (dwmVbd != null)
            dwmVbdCollection.Add(dwmVbd);
        }
        dwmVbdCollection.Save();
        DwmVirtualMachineCollection machineCollection = new DwmVirtualMachineCollection();
        Logger.Trace("Virtual Machines:");
        foreach (KeyValuePair<XenRef<VM>, VM> keyValuePair in VM.get_all_records(session))
        {
          VM vm = keyValuePair.Value;
          vm.opaque_ref = keyValuePair.Key.opaque_ref;
          DwmVirtualMachine dwmVirtualMachine = this.DiscoverVm(session, keyValuePair.Key.opaque_ref, vm, false);
          if (dwmVirtualMachine != null)
            machineCollection.Add(dwmVirtualMachine);
        }
        if (machineCollection.Count > 0)
          machineCollection.Save(this._poolId);
        Logger.Trace("Hosts:");
        object obj10 = this._hostCacheLock;
        Monitor.Enter(obj10);
        try
        {
          foreach (KeyValuePair<XenRef<Host>, Host> keyValuePair in Host.get_all_records(session))
          {
            DwmHost dwmHost = this.DiscoverHost(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, false, false);
            if (dwmHost != null)
              this._pool.Hosts.Add(dwmHost);
          }
        }
        finally
        {
          Monitor.Exit(obj10);
        }
        this._pool.UpdateDiscoveryStatus(DiscoveryStatus.Complete);
      }
      catch (Failure ex)
      {
        Logger.LogException((Exception) ex);
        this._pool = (DwmPool) null;
        if (Localization.Compare(ex.ErrorDescription[0], "SESSION_INVALID", true) == 0)
          this.EndSession();
      }
      catch (SocketException ex)
      {
        Logger.LogException((Exception) ex);
        this._pool = (DwmPool) null;
        this.EndSession();
      }
      catch (WebException ex)
      {
        Logger.LogException((Exception) ex);
        this._pool = (DwmPool) null;
        if (ex.Status != WebExceptionStatus.Timeout)
          this.EndSession();
      }
      catch (Exception ex)
      {
        Logger.LogException(ex);
        this._pool = (DwmPool) null;
        this.EndSession();
      }
      Logger.Trace("Discover() complete.");
      return this._pool;
    }

    /// <summary>
    /// Discover details about the specified Storage Repository.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param><param name="serverOpaqueRef">The XenServer handle to the storage
    ///             repository.</param><param name="sr">Storage Repository for which details are to be
    ///             discovered.</param><param name="saveSr">If true, the DwmStorageRepository instance
    ///             will be saved before the method returns.</param>
    /// <returns>
    /// DwmStorageRepository instance describing the virtual block device.
    /// 
    /// </returns>
    private DwmStorageRepository DiscoverSr(Session session, string serverOpaqueRef, XenAPI.SR sr, bool saveSr)
    {
      int num = XenCollectorBase._traceEnabled ? 1 : 0;
      string fmt = "    {0} {1} size={2} used={3}";
      object[] objArray = new object[4];
      int index1 = 0;
      string uuid = sr.uuid;
      objArray[index1] = (object) uuid;
      int index2 = 1;
      string nameLabel = sr.name_label;
      objArray[index2] = (object) nameLabel;
      int index3 = 2;
      // ISSUE: variable of a boxed type
      __Boxed<long> local1 = (ValueType) sr.physical_size;
      objArray[index3] = (object) local1;
      int index4 = 3;
      // ISSUE: variable of a boxed type
      __Boxed<long> local2 = (ValueType) sr.physical_utilisation;
      objArray[index4] = (object) local2;
      XenCollectorBase.Trace(num != 0, fmt, objArray);
      DwmStorageRepository storageRepository = new DwmStorageRepository(sr.uuid, sr.name_label, this._pool.Uuid, sr.physical_size, sr.physical_utilisation, serverOpaqueRef == this._pool.DefaultSR);
      object obj = this._srCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._srCache.ContainsKey(serverOpaqueRef))
          this._srCache.Add(serverOpaqueRef, sr);
        else
          this._srCache[serverOpaqueRef] = sr;
      }
      finally
      {
        Monitor.Exit(obj);
      }
      if (saveSr)
      {
        DwmStorageRepositoryCollection repositoryCollection = new DwmStorageRepositoryCollection();
        repositoryCollection.Add(storageRepository);
        repositoryCollection.Save();
      }
      return storageRepository;
    }

    /// <summary>
    /// Discover details about the specified Physical Block Device
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param><param name="serverOpaqueRef">The XenServer handle of the PBD.</param><param name="pbd">Physical Block Device for which details are to be
    ///             discovered.</param><param name="savePbd">If true, the DwmPbd instance will be saved
    ///             before the method returns.</param>
    /// <returns>
    /// DwmPbd instance describing the Physical Block Device.
    /// 
    /// </returns>
    private DwmPbd DiscoverPbd(Session session, string serverOpaqueRef, PBD pbd, bool savePbd)
    {
      DwmPbd dwmPbd = (DwmPbd) null;
      if (XenCollectorBase.IsValidXenRef<XenAPI.SR>(pbd.SR))
      {
        XenAPI.SR srFromCache = this.GetSrFromCache(pbd.SR.opaque_ref);
        if (srFromCache != null)
        {
          dwmPbd = new DwmPbd(pbd.uuid, srFromCache.name_label, srFromCache.uuid, this._pool.Uuid);
          dwmPbd.CurrentlyAttached = pbd.currently_attached;
          if (!this._pbdCache.ContainsKey(serverOpaqueRef))
            this._pbdCache.Add(serverOpaqueRef, pbd);
          else
            this._pbdCache[serverOpaqueRef] = pbd;
          if (savePbd)
          {
            DwmPbdCollection dwmPbdCollection = new DwmPbdCollection();
            dwmPbdCollection.Add(dwmPbd);
            dwmPbdCollection.Save();
          }
        }
      }
      return dwmPbd;
    }

    /// <summary>
    /// Discover details about the specified network.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor pool
    ///             for which discovery will be run.</param><param name="serverOpaqueRef">The XenServer handle of the Network.</param><param name="network">Network for which details are to be
    ///             discovered.</param><param name="saveNetwork">If true, the DwmNetwork instance will be
    ///             saved before the method returns.</param>
    /// <returns>
    /// DwmNetwork instance describing the network.
    /// 
    /// </returns>
    private DwmNetwork DiscoverNetwork(Session session, string serverOpaqueRef, Network network, bool saveNetwork)
    {
      DwmNetwork dwmNetwork = new DwmNetwork(network.uuid, network.name_label, this._poolId);
      dwmNetwork.Bridge = network.bridge;
      dwmNetwork.Description = network.name_description;
      if (!this._networkCache.ContainsKey(serverOpaqueRef))
        this._networkCache.Add(serverOpaqueRef, network);
      else
        this._networkCache[serverOpaqueRef] = network;
      if (saveNetwork)
      {
        DwmNetworkCollection networkCollection = new DwmNetworkCollection();
        networkCollection.Add(dwmNetwork);
        networkCollection.Save();
      }
      return dwmNetwork;
    }

    /// <summary>
    /// Discover details about the specified Physical Interface
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param>/// <param name="serverOpaqueRef">The XenServer handle of the PIF.</param><param name="pif">Physical Interface for which details are to be
    ///             discovered.</param><param name="savePif">If true, the DwmPif instance will be saved
    ///             before the method returns.</param>
    /// <returns>
    /// DwmPIF instance describing the Physical Interface.
    /// 
    /// </returns>
    private DwmPif DiscoverPif(Session session, string serverOpaqueRef, PIF pif, bool savePif)
    {
      Network networkFromCache = this.GetNetworkFromCache(pif.network.opaque_ref);
      DwmPif dwmPif = new DwmPif(pif.uuid, pif.device, networkFromCache == null ? (string) null : networkFromCache.uuid, this._pool.Uuid);
      dwmPif.IsManagementInterface = pif.management;
      dwmPif.IsPhysical = pif.physical;
      dwmPif.MacAddress = pif.MAC;
      if (!this._pifCache.ContainsKey(serverOpaqueRef))
        this._pifCache.Add(serverOpaqueRef, pif);
      else
        this._pifCache[serverOpaqueRef] = pif;
      if (savePif)
      {
        DwmPifCollection dwmPifCollection = new DwmPifCollection();
        dwmPifCollection.Add(dwmPif);
        dwmPifCollection.Save();
      }
      return dwmPif;
    }

    /// <summary>
    /// Discover details about the specified Virtual Network Interface.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor pool
    ///             for which discovery will be run.</param>/// <param name="serverOpaqueRef">The XenServer handle of the VIF.</param><param name="vif">Virtual Interface for which details are to be
    ///             discovered.</param><param name="saveVif">If true, the DwmVif instance will be saved
    ///             before the method returns.</param>
    /// <returns>
    /// DwmVif instance describing the Virtual Interface.
    /// 
    /// </returns>
    private DwmVif DiscoverVif(Session session, string serverOpaqueRef, VIF vif, bool saveVif)
    {
      Network networkFromCache = this.GetNetworkFromCache(vif.network.opaque_ref);
      DwmVif dwmVif = new DwmVif(vif.uuid, networkFromCache == null ? (string) null : networkFromCache.uuid, this._pool.Uuid);
      dwmVif.MacAddress = vif.MAC;
      dwmVif.DeviceNumber = vif.device;
      if (!this._vifCache.ContainsKey(serverOpaqueRef))
        this._vifCache.Add(serverOpaqueRef, vif);
      else
        this._vifCache[serverOpaqueRef] = vif;
      if (saveVif)
      {
        DwmVifCollection dwmVifCollection = new DwmVifCollection();
        dwmVifCollection.Add(dwmVif);
        dwmVifCollection.Save();
      }
      return dwmVif;
    }

    /// <summary>
    /// Discover details about the specified Virtual Device Interface.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor pool
    ///             for which discovery will be run.</param><param name="serverOpaqueRef">The XenServer handle of the VDI.</param><param name="vdi">Virtual Device Interface for which details are to be
    ///             discovered.</param>
    private void DiscoverVdi(Session session, string serverOpaqueRef, VDI vdi)
    {
      if (!XenCollectorBase.IsValidXenRef(serverOpaqueRef) || Localization.Compare(serverOpaqueRef, "cd", true) == 0)
        return;
      this._vdiCache.Add(serverOpaqueRef, vdi);
    }

    /// <summary>
    /// Discover details about the specified Virtual Block Device.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param><param name="serverOpaqueRef">The XenServer handle of the VBD.</param><param name="vbd">Virtual Block Device for which details are to be
    ///             discovered.</param><param name="saveVbd">If true, the DwmVirtualMachine instance
    ///             will be saved before the method returns.</param>
    /// <returns>
    /// DwmVbd instance describing the virtual block device.
    /// 
    /// </returns>
    private DwmVbd DiscoverVbd(Session session, string serverOpaqueRef, VBD vbd, bool saveVbd)
    {
      string storageUuid = string.Empty;
      string name = vbd.type.ToString();
      long num1 = 0L;
      long num2 = 0L;
      if (XenCollectorBase.IsValidXenRef<VDI>(vbd.VDI) && Localization.Compare(vbd.VDI.opaque_ref, "cd", true) != 0)
      {
        VDI vdiRecord = XenCollector.GetVdiRecord(session, vbd.VDI.opaque_ref);
        if (vdiRecord != null)
        {
          if (XenCollectorBase.IsValidXenRef<XenAPI.SR>(vdiRecord.SR))
          {
            XenAPI.SR srFromCache = this.GetSrFromCache(vdiRecord.SR.opaque_ref);
            if (srFromCache != null)
            {
              name = srFromCache.name_label;
              storageUuid = srFromCache.uuid;
            }
          }
          num1 = vdiRecord.virtual_size;
          num2 = vdiRecord.physical_utilisation;
        }
      }
      DwmVbd dwmVbd = new DwmVbd(vbd.uuid, name, storageUuid, this._pool.Uuid);
      dwmVbd.DeviceName = vbd.device;
      dwmVbd.DiskType = (int) vbd.type;
      int result = 0;
      int.TryParse(vbd.userdevice, out result);
      dwmVbd.DiskNumber = result;
      dwmVbd.Size = num1;
      dwmVbd.Used = num2;
      if (!this._vbdCache.ContainsKey(serverOpaqueRef))
        this._vbdCache.Add(serverOpaqueRef, vbd);
      else
        this._vbdCache[serverOpaqueRef] = vbd;
      if (saveVbd)
      {
        DwmVbdCollection dwmVbdCollection = new DwmVbdCollection();
        dwmVbdCollection.Add(dwmVbd);
        dwmVbdCollection.Save();
      }
      return dwmVbd;
    }

    /// <summary>
    /// Discover details about the specified Virtual Machine.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param><param name="serverOpaqueRef">The Xen reference for the VM.</param><param name="vm">Virtual Machine for which details are to be
    ///             discovered.</param><param name="saveVm">If true, the DwmVirtualMachine instance
    ///             will be saved before the method returns.</param>
    /// <returns>
    /// DwmVirtualMachine instance describing the virtual machine.
    /// 
    /// </returns>
    private DwmVirtualMachine DiscoverVm(Session session, string serverOpaqueRef, VM vm, bool saveVm)
    {
      bool flag = true;
      VmCacheItem vmCacheItem = new VmCacheItem();
      vmCacheItem.xenVM = vm;
      vmCacheItem.serverOpaqueRef = serverOpaqueRef;
      DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(vm.uuid, vm.name_label, this._poolId);
      dwmVirtualMachine.Active = true;
      dwmVirtualMachine.Description = vm.name_description;
      vmCacheItem.dwmVM = dwmVirtualMachine;
      XenRef<Host> affinity = vm.affinity;
      dwmVirtualMachine.IsControlDomain = vm.is_control_domain;
      dwmVirtualMachine.MinimumDynamicMemory = vm.memory_dynamic_min;
      dwmVirtualMachine.MaximumDynamicMemory = vm.memory_dynamic_max;
      dwmVirtualMachine.MinimumStaticMemory = vm.memory_static_min;
      dwmVirtualMachine.MaximumStaticMemory = vm.memory_static_max;
      dwmVirtualMachine.TargetMemory = vm.memory_target;
      dwmVirtualMachine.MinimumCpus = (int) vm.VCPUs_at_startup;
      dwmVirtualMachine.HvMemoryMultiplier = vm.HVM_shadow_multiplier;
      dwmVirtualMachine.MemoryOverhead = XenCollector.GetMemoryOverhead(vm);
      List<XenRef<VIF>> viFs = vm.VIFs;
      for (int index = 0; index < viFs.Count; ++index)
      {
        if (XenCollectorBase.IsValidXenRef<VIF>(viFs[index]))
        {
          VIF vifFromCache = this.GetVifFromCache(viFs[index].opaque_ref);
          if (vifFromCache != null)
          {
            Network networkFromCache = this.GetNetworkFromCache(vifFromCache.network.opaque_ref);
            dwmVirtualMachine.NetworkInterfaces.Add(new DwmVif(vifFromCache.uuid, networkFromCache == null ? (string) null : networkFromCache.uuid, this._pool.Uuid));
          }
        }
      }
      List<XenRef<VBD>> vbDs = vm.VBDs;
      for (int index1 = 0; index1 < vbDs.Count; ++index1)
      {
        VBD vbdFromCache = this.GetVbdFromCache(vbDs[index1].opaque_ref);
        if (vbdFromCache != null)
        {
          XenRef<VDI> vdi = vbdFromCache.VDI;
          if (XenCollectorBase.IsValidXenRef<VDI>(vdi))
          {
            DwmVbd dwmVbd = new DwmVbd(vbdFromCache.uuid, (string) null, (string) null, this._pool.Uuid);
            dwmVbd.DeviceName = vbdFromCache.device;
            dwmVirtualMachine.BlockDevices.Add(dwmVbd);
            VDI vdiFromCache = this.GetVdiFromCache(vdi.opaque_ref);
            if (vdiFromCache != null)
            {
              XenAPI.SR srFromCache = this.GetSrFromCache(vdiFromCache.SR.opaque_ref);
              if (srFromCache != null)
              {
                dwmVirtualMachine.RequiredStorage.Add(new DwmStorageRepository(srFromCache.uuid, srFromCache.name_label, this._pool.Uuid));
                if (!srFromCache.shared)
                {
                  string fmt = "VM {0} ({1}) is not agile because it requires SR {2} ({3}) that is not shared.";
                  object[] objArray = new object[4];
                  int index2 = 0;
                  string nameLabel1 = vm.name_label;
                  objArray[index2] = (object) nameLabel1;
                  int index3 = 1;
                  string uuid1 = vm.uuid;
                  objArray[index3] = (object) uuid1;
                  int index4 = 2;
                  string nameLabel2 = srFromCache.name_label;
                  objArray[index4] = (object) nameLabel2;
                  int index5 = 3;
                  string uuid2 = srFromCache.uuid;
                  objArray[index5] = (object) uuid2;
                  Logger.Trace(fmt, objArray);
                }
                flag &= srFromCache.shared;
                dwmVbd.IsNetworkStorage = srFromCache.shared;
              }
            }
          }
        }
      }
      try
      {
        VM.assert_agile(this._session, vm.opaque_ref);
      }
      catch (Failure ex)
      {
        flag = false;
        string fmt = "VM {0} ({1}) is not agile because VM.assert_agaile fails ({2}).";
        object[] objArray = new object[3];
        int index1 = 0;
        string nameLabel = vm.name_label;
        objArray[index1] = (object) nameLabel;
        int index2 = 1;
        string uuid = vm.uuid;
        objArray[index2] = (object) uuid;
        int index3 = 2;
        string message = ex.Message;
        objArray[index3] = (object) message;
        Logger.Trace(fmt, objArray);
      }
      dwmVirtualMachine.IsAgile = flag;
      if (vm.power_state == vm_power_state.Running)
      {
        if (!vm.is_control_domain)
        {
          try
          {
            VM.record_data_source(session, serverOpaqueRef, "runstate_fullrun");
            VM.record_data_source(session, serverOpaqueRef, "runstate_full_contention");
            VM.record_data_source(session, serverOpaqueRef, "runstate_concurrency_hazard");
            VM.record_data_source(session, serverOpaqueRef, "runstate_blocked");
            VM.record_data_source(session, serverOpaqueRef, "runstate_partial_run");
            VM.record_data_source(session, serverOpaqueRef, "runstate_partial_contention");
          }
          catch (Failure ex)
          {
            string fmt = "Unable to turn on runstate metrics for vm '{0}'({1}) in pool {2}";
            object[] objArray = new object[3];
            int index1 = 0;
            string nameLabel = vm.name_label;
            objArray[index1] = (object) nameLabel;
            int index2 = 1;
            string uuid = vm.uuid;
            objArray[index2] = (object) uuid;
            int index3 = 2;
            string str = this._hostName;
            objArray[index3] = (object) str;
            Logger.Trace(fmt, objArray);
            Logger.Trace(ex.ToString());
          }
        }
      }
      object obj = this._vmCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._vmCache.ContainsKey(vm.uuid))
          this._vmCache.Add(vm.uuid, vmCacheItem);
        else
          this._vmCache[vm.uuid] = vmCacheItem;
        if (!this._vmCache2.ContainsKey(serverOpaqueRef))
          this._vmCache2.Add(serverOpaqueRef, vm);
        else
          this._vmCache2[serverOpaqueRef] = vm;
      }
      finally
      {
        Monitor.Exit(obj);
      }
      if (saveVm)
      {
        DwmVirtualMachineCollection machineCollection = new DwmVirtualMachineCollection();
        machineCollection.Add(dwmVirtualMachine);
        machineCollection.Save();
      }
      return dwmVirtualMachine;
    }

    /// <summary>
    /// Capture or compute the hypervisor memory overhead for the
    ///             specified VM.
    /// 
    /// </summary>
    /// <param name="vm">VM for which memory overhead will be computed.
    ///             </param>
    /// <returns>
    /// Memory overhead, in bytes for the specified VM.
    /// </returns>
    private static long GetMemoryOverhead(VM vm)
    {
      long num1 = vm.memory_overhead;
      if (num1 == 0L)
      {
        long num2 = (long) ((double) (long) (2.4 * ((double) vm.memory_dynamic_min / 4096.0)) * vm.HVM_shadow_multiplier);
        num1 = num2 + num2 % 4096L;
      }
      return num1;
    }

    /// <summary>
    /// Discover details about the specified Virtual Machine.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param><param name="vmUuid">Unique identifier of the Virtual Machine for
    ///             which details are to be discovered.</param><param name="saveVm">If true, the DwmVirtualMachine instance
    ///             will be saved before the method returns.</param>
    /// <returns>
    /// DwmVirtualMachine instance describing the virtual machine.
    /// 
    /// </returns>
    private DwmVirtualMachine DiscoverVm(Session session, string vmUuid, bool saveVm)
    {
      DwmVirtualMachine dwmVirtualMachine = (DwmVirtualMachine) null;
      foreach (KeyValuePair<XenRef<VM>, VM> keyValuePair in VM.get_all_records(session))
      {
        if (Localization.Compare(keyValuePair.Value.uuid, vmUuid, true) == 0)
        {
          keyValuePair.Value.opaque_ref = keyValuePair.Key.opaque_ref;
          dwmVirtualMachine = this.DiscoverVm(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, saveVm);
          break;
        }
      }
      return dwmVirtualMachine;
    }

    /// <summary>
    /// Discover details about the specified Physical Host.
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param><param name="serverOpaqueRef">The Xen handle to the host to
    ///             discover.</param><param name="host">Phyiscal Host for which details are to be
    ///             discovered.</param><param name="saveHostToCache">If true, the saveHostToCache instance will be
    ///             saved before the method returns.</param><param name="saveHostToDb">If true, the saveHostToDb instance will be
    ///             saved before the method returns.</param>
    /// <returns>
    /// DwnHost instance describing the physical host.
    /// 
    /// </returns>
    private DwmHost DiscoverHost(Session session, string serverOpaqueRef, Host host, bool saveHostToCache, bool saveHostToDb)
    {
      DwmHost dwmHost = new DwmHost(host.uuid, host.name_label, this._poolId);
      dwmHost.Description = host.name_description;
      dwmHost.NumCpus = host.host_CPUs.Count;
      dwmHost.IsPoolMaster = Localization.Compare(host.name_label, this._pool.Master, false) == 0;
      dwmHost.Enabled = host.enabled;
      dwmHost.IsEnterpriseOrHigher = this.CheckHostLicense(host);
      dwmHost.IPAddress = host.address;
      dwmHost.PowerState = !Host_metrics.get_live(session, host.metrics.opaque_ref) ? PowerStatus.Off : PowerStatus.On;
      List<XenRef<Host_cpu>> hostCpUs = host.host_CPUs;
      if (hostCpUs != null && hostCpUs.Count > 0 && XenCollectorBase.IsValidXenRef<Host_cpu>(hostCpUs[0]))
      {
        Host_cpu hostCpuRecord = XenCollector.GetHostCpuRecord(session, hostCpUs[0].opaque_ref);
        if (hostCpuRecord != null)
          dwmHost.CpuSpeed = (int) hostCpuRecord.speed;
      }
      Logger.Trace("Host:");
      string fmt1 = "   Name:  {0}";
      object[] objArray1 = new object[1];
      int index1 = 0;
      string nameLabel1 = host.name_label;
      objArray1[index1] = (object) nameLabel1;
      Logger.Trace(fmt1, objArray1);
      string fmt2 = "   Uuid:  {0}";
      object[] objArray2 = new object[1];
      int index2 = 0;
      string uuid1 = host.uuid;
      objArray2[index2] = (object) uuid1;
      Logger.Trace(fmt2, objArray2);
      string fmt3 = "   ha_network_peers:  {0}";
      object[] objArray3 = new object[1];
      int index3 = 0;
      string str1 = host.ha_network_peers == null || host.ha_network_peers.Length <= 0 ? "none" : host.ha_network_peers[0];
      objArray3[index3] = (object) str1;
      Logger.Trace(fmt3, objArray3);
      string fmt4 = "   ha_statefiles:     {0}";
      object[] objArray4 = new object[1];
      int index4 = 0;
      string str2 = host.ha_statefiles == null || host.ha_statefiles.Length <= 0 ? "none" : host.ha_statefiles[0];
      objArray4[index4] = (object) str2;
      Logger.Trace(fmt4, objArray4);
      string fmt5 = "   Enabled            {0}";
      object[] objArray5 = new object[1];
      int index5 = 0;
      // ISSUE: variable of a boxed type
      __Boxed<bool> local1 = (ValueType) (bool) (dwmHost.Enabled ? 1 : 0);
      objArray5[index5] = (object) local1;
      Logger.Trace(fmt5, objArray5);
      string fmt6 = "   PowerState         {0}";
      object[] objArray6 = new object[1];
      int index6 = 0;
      // ISSUE: variable of a boxed type
      __Boxed<PowerStatus> local2 = (System.Enum) dwmHost.PowerState;
      objArray6[index6] = (object) local2;
      Logger.Trace(fmt6, objArray6);
      Logger.Trace(string.Empty);
      List<XenRef<PIF>> piFs = host.PIFs;
      int num1 = 0;
      for (int index7 = 0; index7 < piFs.Count; ++index7)
      {
        PIF pifFromCache = this.GetPifFromCache(piFs[index7].opaque_ref);
        if (pifFromCache != null)
        {
          Network networkFromCache = this.GetNetworkFromCache(pifFromCache.network.opaque_ref);
          dwmHost.PIFs.Add(new DwmPif(pifFromCache.uuid, pifFromCache.device, networkFromCache == null ? (string) null : networkFromCache.uuid, this._pool.Uuid));
          if (pifFromCache.physical)
            ++num1;
        }
      }
      dwmHost.NumNics = num1;
      List<XenRef<PBD>> pbDs = host.PBDs;
      for (int index7 = 0; index7 < pbDs.Count; ++index7)
      {
        PBD pbdFromCache = this.GetPbdFromCache(pbDs[index7].opaque_ref);
        if (pbdFromCache != null)
        {
          XenAPI.SR srFromCache = this.GetSrFromCache(pbdFromCache.SR.opaque_ref);
          if (srFromCache != null)
          {
            dwmHost.AvailableStorage.Add(new DwmStorageRepository(srFromCache.uuid, srFromCache.name_label, this._pool.Uuid));
            dwmHost.PBDs.Add(new DwmPbd(pbdFromCache.uuid, string.Empty, srFromCache.uuid, this._pool.Uuid)
            {
              CurrentlyAttached = pbdFromCache.currently_attached
            });
          }
        }
      }
      long num2 = host.memory_overhead;
      List<XenRef<VM>> residentVms = host.resident_VMs;
      Logger.Trace("   Resident VMs:");
      for (int index7 = 0; index7 < residentVms.Count; ++index7)
      {
        VM vmFromCache = this.GetVmFromCache(residentVms[index7].opaque_ref);
        if (vmFromCache != null)
        {
          string fmt7 = "   Name:  {0}";
          object[] objArray7 = new object[1];
          int index8 = 0;
          string nameLabel2 = vmFromCache.name_label;
          objArray7[index8] = (object) nameLabel2;
          Logger.Trace(fmt7, objArray7);
          string fmt8 = "   Uuid:  {0}";
          object[] objArray8 = new object[1];
          int index9 = 0;
          string uuid2 = vmFromCache.uuid;
          objArray8[index9] = (object) uuid2;
          Logger.Trace(fmt8, objArray8);
          string fmt9 = "   After crash:  {0}";
          object[] objArray9 = new object[1];
          int index10 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<on_crash_behaviour> local3 = (System.Enum) vmFromCache.actions_after_crash;
          objArray9[index10] = (object) local3;
          Logger.Trace(fmt9, objArray9);
          string fmt10 = "   After reboot: {0}";
          object[] objArray10 = new object[1];
          int index11 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<on_normal_exit> local4 = (System.Enum) vmFromCache.actions_after_reboot;
          objArray10[index11] = (object) local4;
          Logger.Trace(fmt10, objArray10);
          string fmt11 = "   After shutdown: {0}";
          object[] objArray11 = new object[1];
          int index12 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<on_normal_exit> local5 = (System.Enum) vmFromCache.actions_after_shutdown;
          objArray11[index12] = (object) local5;
          Logger.Trace(fmt11, objArray11);
          string fmt12 = "   power_state:  {0}";
          object[] objArray12 = new object[1];
          int index13 = 0;
          // ISSUE: variable of a boxed type
          __Boxed<vm_power_state> local6 = (System.Enum) vmFromCache.power_state;
          objArray12[index13] = (object) local6;
          Logger.Trace(fmt12, objArray12);
          DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(vmFromCache.uuid, vmFromCache.name_label, this._poolId);
          dwmVirtualMachine.Description = vmFromCache.name_description;
          dwmHost.VirtualMachines.Add(dwmVirtualMachine);
          if (num2 == 0L && vmFromCache.is_control_domain)
            num2 = vmFromCache.memory_target;
        }
      }
      dwmHost.MemoryOverhead = num2;
      HostCacheItem hostCacheItem = new HostCacheItem();
      hostCacheItem.serverOpaqueRef = serverOpaqueRef;
      hostCacheItem.xenHost = host;
      hostCacheItem.dwmHost = dwmHost;
      object obj = this._hostCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._hostCache.ContainsKey(host.uuid))
          this._hostCache.Add(host.uuid, hostCacheItem);
        else
          this._hostCache[host.uuid] = hostCacheItem;
      }
      finally
      {
        Monitor.Exit(obj);
      }
      if (saveHostToCache)
      {
        int index7;
        if ((index7 = this._pool.Hosts.IndexOf(dwmHost.Uuid)) != -1)
          this._pool.Hosts[index7] = dwmHost;
        else
          this._pool.Hosts.Add(dwmHost);
        if (saveHostToDb)
          this._pool.SavePoolChildren();
      }
      return dwmHost;
    }

    /// <summary>
    /// Discover details about the specified physical host
    /// 
    /// </summary>
    /// <param name="session">Session instance representing hypervisor
    ///             for which discovery will be run.</param><param name="hostUuid">Unique identifier of the physical host for
    ///             which details are to be discovered.</param><param name="saveHostToCache">If true, the saveHostToCache instance will be saved
    ///             before the method returns.</param><param name="saveHostToDb">If true, the saveHostToDb instance will be saved
    ///             before the method returns.</param>
    /// <returns>
    /// DwmHost instance describing the physical host.
    /// 
    /// </returns>
    private DwmHost DiscoverHost(Session session, string hostUuid, bool saveHostToCache, bool saveHostToDb)
    {
      DwmHost dwmHost = (DwmHost) null;
      foreach (KeyValuePair<XenRef<Host>, Host> keyValuePair in Host.get_all_records(session))
      {
        if (Localization.Compare(keyValuePair.Value.uuid, hostUuid, true) == 0)
        {
          dwmHost = this.DiscoverHost(session, keyValuePair.Key.opaque_ref, keyValuePair.Value, saveHostToCache, saveHostToDb);
          break;
        }
      }
      return dwmHost;
    }

    /// <summary>
    /// Determine in the host cache contains the specified key in a
    ///             thread safe manner.
    /// 
    /// </summary>
    /// <param name="key">Key to check for</param>
    /// <returns>
    /// True if the host cache contains the specified key; false
    ///             otherwise.
    /// </returns>
    private bool HostCacheContainsKey(string key)
    {
      object obj = this._hostCacheLock;
      Monitor.Enter(obj);
      try
      {
        return this._hostCache.ContainsKey(key);
      }
      finally
      {
        Monitor.Exit(obj);
      }
    }

    /// <summary>
    /// Try to get the value of the specified key from the host cache in a
    ///             thread safe manner.
    /// 
    /// </summary>
    /// <param name="key">Key whose value is to be retrieved.</param><param name="value">On output, will contain the value of the
    ///             specified key if the key is in the cache.</param>
    /// <returns>
    /// True if the cache contains the specified key; false
    ///             otherwise.
    /// </returns>
    private bool HostCacheTryGetValue(string key, out HostCacheItem value)
    {
      value = (HostCacheItem) null;
      object obj = this._hostCacheLock;
      Monitor.Enter(obj);
      try
      {
        return this._hostCache.TryGetValue(key, out value);
      }
      finally
      {
        Monitor.Exit(obj);
      }
    }

    /// <summary>
    /// Determine in the VM cache contains the specified key in a
    ///             thread safe manner.
    /// 
    /// </summary>
    /// <param name="key">Key to check for</param>
    /// <returns>
    /// True if the VM cache contains the specified key; false
    ///             otherwise.
    /// </returns>
    private bool VmCacheContainsKey(string key)
    {
      object obj = this._vmCacheLock;
      Monitor.Enter(obj);
      try
      {
        return this._vmCache.ContainsKey(key);
      }
      finally
      {
        Monitor.Exit(obj);
      }
    }

    /// <summary>
    /// Get the VmCacheItem with the specified key from the VM cache in a
    ///             thread safe manner.
    /// 
    /// </summary>
    /// <param name="key">Key of item to retrieve.</param>
    /// <returns>
    /// VmCacheItem with the specified key if it is in the
    ///             collection; null otherwise.
    /// </returns>
    private VmCacheItem VmCacheGetValue(string key)
    {
      VmCacheItem vmCacheItem = (VmCacheItem) null;
      object obj = this._vmCacheLock;
      Monitor.Enter(obj);
      try
      {
        this._vmCache.TryGetValue(key, out vmCacheItem);
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return vmCacheItem;
    }

    /// <summary>
    /// Attempt to retrieve the virtual machine with the specified
    ///             ServerOpaqueRef from the cache in a thread safe manner.
    /// 
    /// </summary>
    /// <param name="serverOpaqueRef">ServerOpaqueRef of the VM to
    ///             retrieve.</param>
    /// <returns>
    /// VM with the specified ServerOpaqueRef.
    /// </returns>
    /// 
    /// <remarks>
    /// If the cache does not contain the specified VM an attempt
    ///             is made to discover the VM and add it to the cache.
    /// </remarks>
    private VM GetVmFromCache(string serverOpaqueRef)
    {
      VM vm = (VM) null;
      object obj = this._vmCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._vmCache2.TryGetValue(serverOpaqueRef, out vm))
        {
          vm = XenCollector.GetVmRecord(this._session, serverOpaqueRef);
          this.DiscoverVm(this._session, serverOpaqueRef, vm, true);
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return vm;
    }

    /// <summary>
    /// Attempt to retrieve the storage repository with the specified
    ///             ServerOpaqueRef from the cache in a thread safe manner.
    /// 
    /// </summary>
    /// <param name="serverOpaqueRef">ServerOpaqueRef of the SR to
    ///             retrieve.</param>
    /// <returns>
    /// SR with the specified ServerOpaqueRef.
    /// </returns>
    /// 
    /// <remarks>
    /// If the cache does not contain the specified SR an attempt
    ///             is made to discover the SR and add it to the cache.
    /// </remarks>
    private XenAPI.SR GetSrFromCache(string serverOpaqueRef)
    {
      XenAPI.SR sr = (XenAPI.SR) null;
      object obj = this._srCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._srCache.TryGetValue(serverOpaqueRef, out sr))
        {
          sr = XenCollector.GetSrRecord(this._session, serverOpaqueRef);
          this.DiscoverSr(this._session, serverOpaqueRef, sr, true);
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return sr;
    }

    /// <summary>
    /// Attempt to retrieve the physical block device with the specified
    ///             ServerOpaqueRef from the cache in a thread safe manner.
    /// 
    /// </summary>
    /// <param name="serverOpaqueRef">ServerOpaqueRef of the PBD to
    ///             retrieve.</param>
    /// <returns>
    /// PBD with the specified ServerOpaqueRef.
    /// </returns>
    /// 
    /// <remarks>
    /// If the cache does not contain the specified PBD an attempt
    ///             is made to discover the PBD and add it to the cache.
    /// </remarks>
    private PBD GetPbdFromCache(string serverOpaqueRef)
    {
      PBD pbd = (PBD) null;
      object obj = this._pbdCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._pbdCache.TryGetValue(serverOpaqueRef, out pbd))
        {
          pbd = XenCollector.GetPbdRecord(this._session, serverOpaqueRef);
          this.DiscoverPbd(this._session, serverOpaqueRef, pbd, true);
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return pbd;
    }

    /// <summary>
    /// Attempt to retrieve the virtual device interface with the specified
    ///             ServerOpaqueRef from the cache in a thread safe manner.
    /// 
    /// </summary>
    /// <param name="serverOpaqueRef">ServerOpaqueRef of the VDI to
    ///             retrieve.</param>
    /// <returns>
    /// VDI with the specified ServerOpaqueRef.
    /// </returns>
    /// 
    /// <remarks>
    /// If the cache does not contain the specified VDI an attempt
    ///             is made to discover the VDI and add it to the cache.
    /// </remarks>
    private VDI GetVdiFromCache(string serverOpaqueRef)
    {
      VDI vdi = (VDI) null;
      object obj = this._vdiCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._vdiCache.TryGetValue(serverOpaqueRef, out vdi))
        {
          vdi = XenCollector.GetVdiRecord(this._session, serverOpaqueRef);
          this.DiscoverVdi(this._session, serverOpaqueRef, vdi);
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return vdi;
    }

    /// <summary>
    /// Attempt to retrieve the virtual block device with the specified
    ///             ServerOpaqueRef from the cache in a thread safe manner.
    /// 
    /// </summary>
    /// <param name="serverOpaqueRef">ServerOpaqueRef of the VBD to
    ///             retrieve.</param>
    /// <returns>
    /// VBD with the specified ServerOpaqueRef.
    /// </returns>
    /// 
    /// <remarks>
    /// If the cache does not contain the specified VBD an attempt
    ///             is made to discover the VBD and add it to the cache.
    /// </remarks>
    private VBD GetVbdFromCache(string serverOpaqueRef)
    {
      VBD vbd = (VBD) null;
      object obj = this._vbdCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._vbdCache.TryGetValue(serverOpaqueRef, out vbd))
        {
          vbd = XenCollector.GetVbdRecord(this._session, serverOpaqueRef);
          this.DiscoverVbd(this._session, serverOpaqueRef, vbd, true);
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return vbd;
    }

    /// <summary>
    /// Attempt to retrieve the Network with the specified
    ///             ServerOpaqueRef from the cache in a thread safe manner.
    /// 
    /// </summary>
    /// <param name="serverOpaqueRef">ServerOpaqueRef of the Network to
    ///             retrieve.</param>
    /// <returns>
    /// Network with the specified ServerOpaqueRef.
    /// </returns>
    /// 
    /// <remarks>
    /// If the cache does not contain the specified Network an
    ///             attempt is made to discover the Network and add it to the cache.
    /// 
    /// </remarks>
    private Network GetNetworkFromCache(string serverOpaqueRef)
    {
      Network network = (Network) null;
      object obj = this._netCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._networkCache.TryGetValue(serverOpaqueRef, out network))
        {
          network = XenCollector.GetNetworkRecord(this._session, serverOpaqueRef);
          this.DiscoverNetwork(this._session, serverOpaqueRef, network, true);
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return network;
    }

    /// <summary>
    /// Attempt to retrieve the Physical Interface with the specified
    ///             ServerOpaqueRef from the cache in a thread safe manner.
    /// 
    /// </summary>
    /// <param name="serverOpaqueRef">ServerOpaqueRef of the PIF to
    ///             retrieve.</param>
    /// <returns>
    /// PIF with the specified ServerOpaqueRef.
    /// </returns>
    /// 
    /// <remarks>
    /// If the cache does not contain the specified PIF an attempt
    ///             is made to discover the PIF and add it to the cache.
    /// </remarks>
    private PIF GetPifFromCache(string serverOpaqueRef)
    {
      PIF pif = (PIF) null;
      object obj = this._pifCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._pifCache.TryGetValue(serverOpaqueRef, out pif))
        {
          pif = XenCollector.GetPifRecord(this._session, serverOpaqueRef);
          this.DiscoverPif(this._session, serverOpaqueRef, pif, true);
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return pif;
    }

    /// <summary>
    /// Attempt to retrieve the Virtual Network Interface with the specified
    ///             ServerOpaqueRef from the cache in a thread safe manner.
    /// 
    /// </summary>
    /// <param name="serverOpaqueRef">ServerOpaqueRef of the VIF to
    ///             retrieve.</param>
    /// <returns>
    /// VIF with the specified ServerOpaqueRef.
    /// </returns>
    /// 
    /// <remarks>
    /// If the cache does not contain the specified VIF an attempt
    ///             is made to discover the VIF and add it to the cache.
    /// </remarks>
    private VIF GetVifFromCache(string serverOpaqueRef)
    {
      VIF vif = (VIF) null;
      object obj = this._vifCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (!this._vifCache.TryGetValue(serverOpaqueRef, out vif))
        {
          vif = XenCollector.GetVifRecord(this._session, serverOpaqueRef);
          this.DiscoverVif(this._session, serverOpaqueRef, vif, true);
        }
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return vif;
    }

    /// <summary>
    /// Determine the VDB with the specified device name is routed
    ///             through the network.
    /// 
    /// </summary>
    /// <param name="item">VmCacheItem instance containing the VM owning
    ///             the VBD with the specified device name.</param><param name="deviceName">Device name of the VBD to check.</param>
    /// <returns>
    /// True if the device is routed through the network; false
    ///             otherwise.
    /// </returns>
    /// 
    /// <remarks>
    /// If a VBD is routed through the network, IO on the VBD
    ///             will place network load on the host.
    /// </remarks>
    private bool IsVbdNetworked(VmCacheItem item, string deviceName)
    {
      bool flag = false;
      if (item != null && item.dwmVM != null && item.dwmVM.BlockDevices != null)
      {
        DwmVbd deviceByName = item.dwmVM.BlockDevices.GetDeviceByName(deviceName);
        if (deviceByName != null)
          flag = deviceByName.IsNetworkStorage;
      }
      return flag;
    }

    /// <summary>
    /// Determine if WLB is enabled for the specified pool.
    /// 
    /// </summary>
    /// <param name="session">Session instance for the pool whose
    ///             enabled state is being determined.</param><param name="xenPool">XenServer pool whose WLB enabled state is to
    ///             be determined.</param>
    /// <returns>
    /// True if WLB is enabled; false otherwise.
    /// </returns>
    /// 
    /// <remarks>
    /// If the pool is Xen 5.1, we use Pool.wlb_enabled.
    ///             If the pool is Xen 5.0, we'll assume WLB is enabled since
    ///             Pool.wlb_enabled does not exist in 5.0.
    /// </remarks>
    private bool GetWlbEnabled(Session session, Pool xenPool)
    {
      bool flag = xenPool.wlb_enabled;
      if (!flag)
      {
        Host hostRecord = XenCollector.GetHostRecord(session, xenPool.master.opaque_ref);
        int verMinor;
        int verBuild;
        if (hostRecord != null && this.GetHostVersion(hostRecord, out verMinor, out verBuild) == 5 && verMinor == 0)
          flag = true;
      }
      return flag;
    }

    /// <summary>
    /// Determine if the license for the specified host is enterprise or
    ///             higher.
    /// 
    /// </summary>
    /// <param name="host">Host whose license is to be checked.</param>
    /// <returns>
    /// True if the license for the host is enterprise or
    ///             higher; false otherwise.
    /// </returns>
    private bool CheckHostLicense(Host host)
    {
      bool flag = false;
      if (host.license_params != null)
      {
        bool result = true;
        if (host.license_params.ContainsKey("restrict_wlb"))
        {
          if (bool.TryParse(host.license_params["restrict_wlb"], out result))
            flag = !result;
          else
            result = true;
        }
        if (result)
        {
          string fmt = "Host {0}({1}) is not licensed.";
          object[] objArray = new object[2];
          int index1 = 0;
          string nameLabel = host.name_label;
          objArray[index1] = (object) nameLabel;
          int index2 = 1;
          string uuid = host.uuid;
          objArray[index2] = (object) uuid;
          Logger.LogWarning(fmt, objArray);
        }
      }
      return flag;
    }

    bool ICollector.get_IsAlive()
    {
      return this.IsAlive;
    }
  }
}
