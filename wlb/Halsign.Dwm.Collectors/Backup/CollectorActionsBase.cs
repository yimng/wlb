// Decompiled with JetBrains decompiler
// Type: Citrix.DWM.Collectors.CollectorActionsBase
// Assembly: Citrix.Dwm.Collectors, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0844E477-F94E-4593-A883-69DEC5AD079C
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\Citrix.Dwm.Collectors.dll

using Citrix.DWM.Framework;
using System;

namespace Citrix.DWM.Collectors
{
  /// <summary>
  /// Abstract base class that implements ICollectorActions.
  /// 
  /// </summary>
  public abstract class CollectorActionsBase : CollectorBase, ICollectorActions
  {
    /// <summary>
    /// Start any background threads used by the collection actions.
    /// 
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// Stop any background threads used by the collection actions.
    /// 
    /// </summary>
    public void Stop()
    {
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
    public virtual string GetPoolName()
    {
      if (this.IsInitialized)
        return string.Empty;
      throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the GetPoolName method", DwmErrorCode.NotInitialized, (Exception) null);
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
    public virtual string GetPoolUniqueIdentifier()
    {
      if (this.IsInitialized)
        return string.Empty;
      throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the GetPoolUniqueIdentifier method", DwmErrorCode.NotInitialized, (Exception) null);
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
    public virtual void GetCerts()
    {
    }

    /// <summary>
    /// Determine if the specified VM can start of the specified host.
    /// 
    /// </summary>
    /// <param name="vmUuid">Unique identifier of the VM to start.</param><param name="hostUuid">Unique identifier of the physical host on
    ///             which the VM would like to start</param>
    /// <returns>
    /// CantBootReason.None if the VM can be started on the host.  Some other
    ///             CantBootReason indicating why the VM cannot start if the VM cannot be
    ///             started on the host.
    /// 
    /// </returns>
    public virtual CantBootReason CanStartVM(string vmUuid, string hostUuid)
    {
      return CantBootReason.None;
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
    public virtual int StartVM(string vmUuid, string hostUuid, bool startPaused)
    {
      return 4006;
    }

    /// <summary>
    /// Move the specified VM from its current host to the specified host.
    /// 
    /// </summary>
    /// <param name="vmUuid">Unique identifier of the VM to migrate.</param><param name="migrateToHostUuid">Unique identifier of the physical
    ///             host to which the VM should be moved.</param><param name="recommendationId">Unique identifier of the VM migrate
    ///             recommendation</param><param name="liveMigration">Flag to indicate if the migration is
    ///             done in real time.</param>
    /// <returns>
    /// 0 if the migration succeeds; none zero otherwise.
    /// </returns>
    public virtual int MigrateVM(string vmUuid, string migrateToHostUuid, int recommendationId, bool liveMigration)
    {
      return 4006;
    }

    /// <summary>
    /// Send a message to the pool master.
    /// 
    /// </summary>
    /// <param name="category">The category or topic of the message.</param><param name="message">Message to send.</param>
    public virtual void SendMessage(string category, string message)
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
      return 4006;
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
      return 4006;
    }
  }
}
