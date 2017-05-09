// Decompiled with JetBrains decompiler
// Type: Citrix.DWM.Collectors.XenCollectorActions
// Assembly: Citrix.Dwm.Collectors, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0844E477-F94E-4593-A883-69DEC5AD079C
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\Citrix.Dwm.Collectors.dll

using Citrix.DWM.Framework;
using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using XenAPI;

namespace Citrix.DWM.Collectors
{
  /// <summary>
  /// Data collector actions for Xen hypervisors
  /// 
  /// </summary>
  public class XenCollectorActions : XenCollectorBase, ICollectorActions
  {
    private object _vmCacheLock = new object();
    private object _hostCacheLock = new object();
    private Dictionary<string, string> _vmCache;
    private Dictionary<string, string> _hostCache;

    /// <summary>
    /// Initialize a new instance of the XenCollectorActions.
    /// 
    /// </summary>
    public XenCollectorActions()
    {
    }

    /// <summary>
    /// Initialize a new instance of the XenCollector.
    /// 
    /// </summary>
    /// <param name="hostname">DNS name or TCP/IP address of a host in
    ///             a Xen pool from which data will be collected.</param><param name="port">TCP/IP port on which the host is listening for
    ///             requests.  The default is port 80.</param><param name="username">User name to connect to the host.</param><param name="password">Password to connect to the host.</param>
    /// <remarks>
    /// The constructor will throw an DwmException if a session
    ///             cannot be established.
    /// </remarks>
    public XenCollectorActions(string hostname, int port, string username, string password)
      : base(hostname, port, "http", username, password)
    {
    }

    /// <summary>
    /// Load the cache of VMs and Hosts and start a heartbeat thread.
    /// 
    /// </summary>
    public void Start()
    {
      this.StartCollection();
    }

    /// <summary>
    /// Unload the cache of VMs and Hosts and stop the heartbeat thread.
    /// 
    /// </summary>
    public void Stop()
    {
      this.StopCollection();
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
      if (this._vmCache == null)
        return;
      if (Localization.Compare(operation, "del", true) == 0)
      {
        if (!this._vmCache.ContainsKey(vmUuid))
          return;
        this._vmCache.Remove(vmUuid);
      }
      else
      {
        if (Localization.Compare(operation, "add", true) != 0)
          return;
        if (!this._vmCache.ContainsKey(vmUuid))
          this._vmCache.Add(vmUuid, serverOpaqueRef);
        else
          this._vmCache[vmUuid] = serverOpaqueRef;
      }
    }

    /// <summary>
    /// Process a Xen event on the specified host
    /// 
    /// </summary>
    /// <param name="hostUuid">The unique ID of the host that is the target of
    ///             the Xen event.</param><param name="operation">The event type - add, modify, etc.</param><param name="serverOpaqueRef">The server handle of the VM to which
    ///             the event applies.</param><param name="snapshot">The snapshot data associated with the
    ///             event.</param>
    protected override void ProcessHostEvent(string hostUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
    {
      if (this._hostCache == null)
        return;
      if (Localization.Compare(operation, "del", true) == 0)
      {
        if (!this._hostCache.ContainsKey(hostUuid))
          return;
        this._hostCache.Remove(hostUuid);
      }
      else
      {
        if (Localization.Compare(operation, "add", true) != 0)
          return;
        if (!this._hostCache.ContainsKey(hostUuid))
          this._hostCache.Add(hostUuid, serverOpaqueRef);
        else
          this._hostCache[hostUuid] = serverOpaqueRef;
      }
    }

    /// <summary>
    /// End out current Xen session.
    /// 
    /// </summary>
    protected override void EndSession()
    {
      base.EndSession();
      if (this._hostCache != null)
      {
        this._hostCache.Clear();
        this._hostCache = (Dictionary<string, string>) null;
      }
      if (this._vmCache == null)
        return;
      this._vmCache.Clear();
      this._vmCache = (Dictionary<string, string>) null;
    }

    /// <summary>
    /// Get the name of the hypervisor pool associated with the collector.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// The name of the hypervisor pool associated with the collector.
    /// 
    /// </returns>
    public string GetPoolName()
    {
      string str = string.Empty;
      if (!this.IsInitialized)
        throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the GetPoolName method", DwmErrorCode.NotInitialized, (Exception) null);
      foreach (KeyValuePair<XenRef<Pool>, Pool> keyValuePair in Pool.get_all_records(this._session))
        str = keyValuePair.Value.name_label;
      return str;
    }

    /// <summary>
    /// Get the unique identifier of the hypervisor pool associated with
    ///             the collector.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// The unique identifier of the hypervisor pool associated with the
    ///             collector.
    /// 
    /// </returns>
    public string GetPoolUniqueIdentifier()
    {
      string str = string.Empty;
      if (!this.IsInitialized)
        throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the GetPoolUniqueIdentifier method", DwmErrorCode.NotInitialized, (Exception) null);
      foreach (KeyValuePair<XenRef<Pool>, Pool> keyValuePair in Pool.get_all_records(this._session))
        str = keyValuePair.Value.uuid;
      return str;
    }

    /// <summary>
    /// Retrieve the certificates for each host in the pool.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// The certs are stored in files the applications data
    ///             folder.  The file names are in the form HostName.cer.
    /// </remarks>
    public void GetCerts()
    {
      Session session;
      try
      {
        session = new Session(this._hostName, this._hostPort);
        session.login_with_password(this._username, this._password);
      }
      catch (Failure ex)
      {
        throw new DwmException(ex.Message, DwmErrorCode.XenCannotLogIn, (Exception) ex);
      }
      catch (SocketException ex)
      {
        throw new DwmException(ex.Message, DwmErrorCode.XenCannotLogIn, (Exception) ex);
      }
      catch (WebException ex)
      {
        throw new DwmException(Localization.Format("Cannot connect to Xen {0}:{1} hypervisor.  The most likely causes include the machine being offline or an incorrect TCP/IP address.", (object) this._hostName, (object) this._hostPort), DwmErrorCode.XenCannotConnect, (Exception) ex);
      }
      try
      {
        foreach (KeyValuePair<XenRef<Host>, Host> keyValuePair in Host.get_all_records(session))
        {
          Host host = keyValuePair.Value;
          string serverCertificate = Host.get_server_certificate(session, (string) keyValuePair.Key);
          string path = string.Format("{0}\\{1}.cer", (object) Logger.GetLogDirectory(), (object) keyValuePair.Value.name_label);
          char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
          for (int index = 0; index < invalidFileNameChars.Length; ++index)
          {
            if (path.IndexOf(invalidFileNameChars[index]) != -1)
              path.Replace(invalidFileNameChars[index], '_');
          }
          using (StreamWriter streamWriter = new StreamWriter(path))
          {
            streamWriter.Write(serverCertificate);
            streamWriter.Close();
          }
        }
        session.logout();
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
      if (this._session == null && this.InitializeSession(this._hostName, true, false) != XenCollectorBase.SessionInitStatus.Success)
        this.OnConnectionLost((Exception) null);
      if (this._session == null || !this.Heartbeat())
        return;
      object obj1 = this._hostCacheLock;
      Monitor.Enter(obj1);
      try
      {
        if (this._hostCache == null)
          this.GetHosts();
      }
      finally
      {
        Monitor.Exit(obj1);
      }
      object obj2 = this._vmCacheLock;
      Monitor.Enter(obj2);
      try
      {
        if (this._vmCache != null)
          return;
        this.GetVMs();
      }
      finally
      {
        Monitor.Exit(obj2);
      }
    }

    /// <summary>
    /// Retrieve all the hosts that are in the pool and put the in
    ///             _hostCache.
    /// 
    /// </summary>
    private void GetHosts()
    {
      try
      {
        if (this._session == null)
          return;
        Dictionary<XenRef<Host>, Host> allRecords = Host.get_all_records(this._session);
        if (allRecords == null)
          return;
        this._hostCache = new Dictionary<string, string>();
        foreach (KeyValuePair<XenRef<Host>, Host> keyValuePair in allRecords)
          this._hostCache.Add(keyValuePair.Value.uuid, keyValuePair.Key.opaque_ref);
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
    /// Retrieve all the virtual machines that are in the pool and put
    ///             them in _vmCache.
    /// 
    /// </summary>
    private void GetVMs()
    {
      try
      {
        if (this._session == null)
          return;
        Dictionary<XenRef<VM>, VM> allRecords = VM.get_all_records(this._session);
        if (allRecords == null)
          return;
        this._vmCache = new Dictionary<string, string>();
        foreach (KeyValuePair<XenRef<VM>, VM> keyValuePair in allRecords)
          this._vmCache.Add(keyValuePair.Value.uuid, keyValuePair.Key.opaque_ref);
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
    /// Determine if the specified VM can start of the specified host.
    /// 
    /// </summary>
    /// <param name="vmUuid">Unique identifier of the VM to start.</param><param name="hostUuid">Unique identifier of the physical host on
    ///             which the VM would like to start</param>CantBootReason.None if the VM can be started on the host.  Some other
    ///             CantBootReason indicating why the VM cannot start if the VM cannot be
    ///             started on the host.
    public CantBootReason CanStartVM(string vmUuid, string hostUuid)
    {
      CantBootReason cantBootReason = CantBootReason.None;
      string fmt1 = "ICollectorActions.CanStartVM called for VM {0}, Host {1}";
      object[] objArray1 = new object[2];
      int index1 = 0;
      string str1 = vmUuid;
      objArray1[index1] = (object) str1;
      int index2 = 1;
      string str2 = hostUuid;
      objArray1[index2] = (object) str2;
      Logger.Trace(fmt1, objArray1);
      if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(hostUuid))
      {
        if (this.IsInitialized)
        {
          string hostRefForUuid = this.GetHostRefForUuid(hostUuid);
          string _self = (string) null;
          if (hostRefForUuid != null)
            _self = this.GetVmRefForUuid(vmUuid);
          if (_self != null)
          {
            if (hostRefForUuid != null)
            {
              try
              {
                VM.assert_can_boot_here(this._session, _self, hostRefForUuid);
                string fmt2 = "VM.assert_can_boot_here returns true.  VM={0}  Host={1}";
                object[] objArray2 = new object[2];
                int index3 = 0;
                string str3 = vmUuid;
                objArray2[index3] = (object) str3;
                int index4 = 1;
                string str4 = hostUuid;
                objArray2[index4] = (object) str4;
                Logger.Trace(fmt2, objArray2);
                goto label_13;
              }
              catch (Failure ex)
              {
                string fmt2 = "VM.assert_can_boot_here fails with {0}.  VM={1}  Host={2}";
                object[] objArray2 = new object[3];
                int index3 = 0;
                string message = ex.Message;
                objArray2[index3] = (object) message;
                int index4 = 1;
                string str3 = vmUuid;
                objArray2[index4] = (object) str3;
                int index5 = 2;
                string str4 = hostUuid;
                objArray2[index5] = (object) str4;
                Logger.Trace(fmt2, objArray2);
                cantBootReason = this.FailureToReason(ex);
                goto label_13;
              }
              catch (SocketException ex)
              {
                cantBootReason = CantBootReason.SocketException;
                goto label_13;
              }
              catch (WebException ex)
              {
                cantBootReason = CantBootReason.WebException;
                goto label_13;
              }
              catch (Exception ex)
              {
                cantBootReason = CantBootReason.GeneralException;
                goto label_13;
              }
            }
          }
          Logger.Trace("ICollectorActions.CanStartVM - failure - vmServerRef or hostServerRef is not initialized properly");
        }
        else
          Logger.Trace("ICollectorActions.CanStartVM - Data collector not properly initialized");
      }
label_13:
      return cantBootReason;
    }

    /// <summary>
    /// Start the specified VM on the specified host.
    /// 
    /// </summary>
    /// <param name="vmUuid">Unique identifier of the VM to start.</param><param name="hostUuid">Unique identifier of the physical host on
    ///             which the VM should be started</param><param name="startPaused">Flag to indicate if the VM should be
    ///             started in the paused (true) or running (false) state.</param>
    /// <returns>
    /// 0 if the start succeeds; non-zero otherwise.
    /// </returns>
    public int StartVM(string vmUuid, string hostUuid, bool startPaused)
    {
      int num = 0;
      if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(hostUuid))
      {
        if (this.IsInitialized)
        {
          string hostRefForUuid = this.GetHostRefForUuid(hostUuid);
          string _vm = (string) null;
          if (hostRefForUuid != null)
            _vm = this.GetVmRefForUuid(vmUuid);
          if (_vm != null)
          {
            if (hostRefForUuid != null)
            {
              try
              {
                VM.start_on(this._session, _vm, hostRefForUuid, startPaused, true);
                goto label_11;
              }
              catch (Failure ex)
              {
                string fmt = "Exception starting VM {0} to host {1}";
                object[] objArray = new object[2];
                int index1 = 0;
                string str1 = vmUuid;
                objArray[index1] = (object) str1;
                int index2 = 1;
                string str2 = hostUuid;
                objArray[index2] = (object) str2;
                Logger.LogError(fmt, objArray);
                Logger.LogException((Exception) ex);
                num = 1;
                goto label_11;
              }
            }
          }
          num = 4007;
        }
        else
          num = 4012;
      }
      else
        num = 4007;
label_11:
      return num;
    }

    /// <summary>
    /// Move the specified VM from its current host to the specified host.
    /// 
    /// </summary>
    /// <param name="vmUuid">Unique identifier of the VM to migrate.</param><param name="migrateToHostUuid">Unique identifier of the physical
    ///             host to which the VM should be moved.</param><param name="recommendationId">Unique identifier of the VM
    ///             move recommendation.</param><param name="liveMigration">Flag to indicate if the migration is
    ///             done in real time.</param>
    /// <returns>
    /// 0 if the migration succeeds; none zero otherwise.
    /// </returns>
    public int MigrateVM(string vmUuid, string migrateToHostUuid, int recommendationId, bool liveMigration)
    {
      int num = 0;
      if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(migrateToHostUuid))
      {
        if (this.IsInitialized)
        {
          string hostRefForUuid = this.GetHostRefForUuid(migrateToHostUuid);
          string _vm = (string) null;
          if (hostRefForUuid != null)
            _vm = this.GetVmRefForUuid(vmUuid);
          if (_vm != null && hostRefForUuid != null)
          {
            int timeout = this._session.proxy.Timeout;
            try
            {
              int defaultValue = 120;
              int valueAsInt = Configuration.GetValueAsInt(ConfigItems.VmMigrateTimeout, defaultValue);
              this._session.proxy.Timeout = (valueAsInt <= 0 ? defaultValue : valueAsInt) * 1000;
              Dictionary<string, string> _options = new Dictionary<string, string>();
              _options["live"] = "true";
              XenRef<Task> xenRef = VM.async_pool_migrate(this._session, _vm, hostRefForUuid, _options);
              if (recommendationId != 0)
              {
                Task.add_to_other_config(this._session, xenRef.opaque_ref, "wlb_advised", recommendationId.ToString());
                Task.add_to_other_config(this._session, xenRef.opaque_ref, "wlb_action", "vm_migrate");
                Task.add_to_other_config(this._session, xenRef.opaque_ref, "wlb_action_obj_ref", _vm);
                Task.add_to_other_config(this._session, xenRef.opaque_ref, "wlb_action_obj_type", "vm");
              }
              task_status_type status;
              for (status = Task.get_record(this._session, xenRef.opaque_ref).status; status == task_status_type.pending; status = Task.get_record(this._session, xenRef.opaque_ref).status)
                Thread.Sleep(2000);
              if (status != task_status_type.success)
                throw new WebException();
              string fmt = "Auto Migrate VM: migrated vm - {0} to host - {1}, recommendation id - {2}";
              object[] objArray = new object[3];
              int index1 = 0;
              string nameLabel1 = VM.get_name_label(this._session, _vm);
              objArray[index1] = (object) nameLabel1;
              int index2 = 1;
              string nameLabel2 = Host.get_name_label(this._session, hostRefForUuid);
              objArray[index2] = (object) nameLabel2;
              int index3 = 2;
              string str = recommendationId.ToString();
              objArray[index3] = (object) str;
              Logger.Trace(fmt, objArray);
            }
            catch (Failure ex)
            {
              string fmt = "Exception moving VM {0} to host {1}";
              object[] objArray = new object[2];
              int index1 = 0;
              string str1 = vmUuid;
              objArray[index1] = (object) str1;
              int index2 = 1;
              string str2 = migrateToHostUuid;
              objArray[index2] = (object) str2;
              Logger.LogError(fmt, objArray);
              Logger.LogException((Exception) ex);
              num = Localization.Compare(ex.Message, "NOT_IMPLEMENTED", true) != 0 ? 1 : 4006;
            }
            catch (WebException ex)
            {
              string fmt = "Exception moving VM {0} to host {1}";
              object[] objArray = new object[2];
              int index1 = 0;
              string str1 = vmUuid;
              objArray[index1] = (object) str1;
              int index2 = 1;
              string str2 = migrateToHostUuid;
              objArray[index2] = (object) str2;
              Logger.LogError(fmt, objArray);
              Logger.LogException((Exception) ex);
              num = 1;
            }
            finally
            {
              this._session.proxy.Timeout = timeout;
            }
          }
          else
            num = 4007;
        }
        else
          num = 4012;
      }
      else
        num = 4007;
      return num;
    }

    /// <summary>
    /// Send a message to the pool master.
    /// 
    /// </summary>
    /// <param name="category">The category or topic of the message.</param><param name="message">Message to send.</param>
    public void SendMessage(string category, string message)
    {
    }

    /// <summary>
    /// Turn the specifies host off.
    /// 
    /// </summary>
    /// <param name="hostUuid">The unique identifier of the host to
    ///             turn off.</param>
    /// <returns>
    /// 0 if the host was powered off; non-zero otherwise.
    /// </returns>
    public int PowerHostOff(string hostUuid)
    {
      int num = 0;
      if (!string.IsNullOrEmpty(hostUuid))
      {
        if (this.IsInitialized)
        {
          string hostRefForUuid = this.GetHostRefForUuid(hostUuid);
          string uuid = this._session.uuid;
          if (hostUuid == uuid)
          {
            string fmt = "Host Power off cancelled. The specified host {0} is the master.";
            object[] objArray = new object[1];
            int index = 0;
            string str = hostUuid;
            objArray[index] = (object) str;
            Logger.LogError(fmt, objArray);
          }
          else if (hostRefForUuid != null)
          {
            bool flag = false;
            try
            {
              Host.disable(this._session, hostRefForUuid);
              Host.shutdown(this._session, hostRefForUuid);
              flag = true;
            }
            catch (Failure ex)
            {
              string fmt = "Failure Exception shutting down host {0}";
              object[] objArray = new object[1];
              int index = 0;
              string str = hostUuid;
              objArray[index] = (object) str;
              Logger.LogError(fmt, objArray);
              Logger.LogException((Exception) ex);
              num = (int) this.FailureToReason(ex);
            }
            catch (Exception ex)
            {
              string fmt = "Exception shutting down host {0}";
              object[] objArray = new object[1];
              int index = 0;
              string str = hostUuid;
              objArray[index] = (object) str;
              Logger.LogError(fmt, objArray);
              Logger.LogException(ex);
            }
            finally
            {
              if (!flag)
              {
                try
                {
                  Host.enable(this._session, hostRefForUuid);
                }
                catch (Failure ex)
                {
                }
              }
            }
          }
          else
            num = 4007;
        }
        else
          num = 4012;
      }
      else
        num = 4007;
      return num;
    }

    /// <summary>
    /// Turn the specifies host on.
    /// 
    /// </summary>
    /// <param name="hostUuid">The unique identifier of the host to
    ///             turn on.</param>
    /// <returns>
    /// 0 if the host was powered on; non-zero otherwise.
    /// </returns>
    public int PowerHostOn(string hostUuid)
    {
      int num1 = 0;
      if (!string.IsNullOrEmpty(hostUuid))
      {
        if (this.IsInitialized)
        {
          string hostRefForUuid = this.GetHostRefForUuid(hostUuid);
          if (hostRefForUuid != null)
          {
            int timeout = this._session.proxy.Timeout;
            try
            {
              int defaultValue = 600;
              int valueAsInt = Configuration.GetValueAsInt(ConfigItems.HostPowerOnTimeout, defaultValue);
              this._session.proxy.Timeout = (valueAsInt <= 0 ? defaultValue : valueAsInt) * 1000;
              XenRef<Task> xenRef = Host.async_power_on(this._session, hostRefForUuid);
              task_status_type status = Task.get_record(this._session, xenRef.opaque_ref).status;
              int num2 = 0;
              while (status == task_status_type.pending)
              {
                if (num2 != 120)
                {
                  Thread.Sleep(5000);
                  status = Task.get_record(this._session, xenRef.opaque_ref).status;
                  ++num2;
                }
                else
                  break;
              }
            }
            catch (Failure ex)
            {
              string fmt = "Failure Exception powering on host {0}";
              object[] objArray = new object[1];
              int index = 0;
              string str = hostUuid;
              objArray[index] = (object) str;
              Logger.LogError(fmt, objArray);
              Logger.LogException((Exception) ex);
              num1 = (int) this.FailureToReason(ex);
            }
            catch (Exception ex)
            {
              string fmt = "Exception powering on host {0}";
              object[] objArray = new object[1];
              int index = 0;
              string str = hostUuid;
              objArray[index] = (object) str;
              Logger.LogError(fmt, objArray);
              Logger.LogException(ex);
              num1 = 4014;
            }
            bool flag = false;
            int num3 = 0;
            while (!flag)
            {
              if (num3 <= 5)
              {
                try
                {
                  ++num3;
                  Host.enable(this._session, hostRefForUuid);
                  flag = true;
                }
                catch (Failure ex)
                {
                  if (Localization.Compare(ex.Message, "HOST_STILL_BOOTING", true) == 0 && num3 < 5)
                  {
                    Logger.LogWarning("Unable to enable host {0} becuase it is still booting.  Retrying in 5 seconds...");
                    Thread.Sleep(5000);
                  }
                  else
                  {
                    string fmt = "Failure Exception powering on host {0}";
                    object[] objArray = new object[1];
                    int index = 0;
                    string str = hostUuid;
                    objArray[index] = (object) str;
                    Logger.LogError(fmt, objArray);
                    Logger.LogException((Exception) ex);
                    num1 = (int) this.FailureToReason(ex);
                  }
                }
                catch (Exception ex)
                {
                  string fmt = "Exception powering on host {0}";
                  object[] objArray = new object[1];
                  int index = 0;
                  string str = hostUuid;
                  objArray[index] = (object) str;
                  Logger.LogError(fmt, objArray);
                  Logger.LogException(ex);
                  num1 = 4014;
                }
              }
              else
                break;
            }
            this._session.proxy.Timeout = timeout;
          }
          else
            num1 = 4007;
        }
        else
          num1 = 4012;
      }
      else
        num1 = 4007;
      return num1;
    }

    /// <summary>
    /// Retrieve the Xen handle (XenRef.opaque_ref) for the specified
    ///             physical host.
    /// 
    /// </summary>
    /// <param name="uuid">Unique ID of the physical host for which the
    ///             Xen handle is desired.</param>
    /// <returns>
    /// The Xen handle of the specified host if successful; null
    ///             otherwise.
    /// </returns>
    private string GetHostRefForUuid(string uuid)
    {
      string str = (string) null;
      object obj = this._hostCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (this._hostCache == null)
          this.GetHosts();
        if (this._hostCache != null)
          this._hostCache.TryGetValue(uuid, out str);
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return str;
    }

    /// <summary>
    /// Retrieve the Xen handle (XenRef.opaque_ref) for the specified
    ///             virtual machine.
    /// 
    /// </summary>
    /// <param name="uuid">Unique ID of the VM for which the Xen handle is
    ///             desired.</param>
    /// <returns>
    /// The Xen handle of the specified VM if successful; null
    ///             otherwise.
    /// </returns>
    private string GetVmRefForUuid(string uuid)
    {
      string str = (string) null;
      object obj = this._vmCacheLock;
      Monitor.Enter(obj);
      try
      {
        if (this._vmCache == null)
          this.GetVMs();
        if (this._vmCache != null)
          this._vmCache.TryGetValue(uuid, out str);
      }
      finally
      {
        Monitor.Exit(obj);
      }
      return str;
    }
  }
}
