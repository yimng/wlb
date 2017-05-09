// Decompiled with JetBrains decompiler
// Type: XenAPI.FriendlyErrorNames
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace XenAPI
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [DebuggerNonUserCode]
  internal class FriendlyErrorNames
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) FriendlyErrorNames.resourceMan, (object) null))
          FriendlyErrorNames.resourceMan = new ResourceManager("XenAPI.FriendlyErrorNames", typeof (FriendlyErrorNames).Assembly);
        return FriendlyErrorNames.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return FriendlyErrorNames.resourceCulture;
      }
      set
      {
        FriendlyErrorNames.resourceCulture = value;
      }
    }

    internal static string ACTIVATION_WHILE_NOT_FREE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("ACTIVATION_WHILE_NOT_FREE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_ALREADY_ENABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_ALREADY_ENABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_DISABLE_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_DISABLE_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_DISABLE_FAILED_PERMISSION_DENIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_DISABLE_FAILED_PERMISSION_DENIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_DISABLE_FAILED_WRONG_CREDENTIALS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_DISABLE_FAILED_WRONG_CREDENTIALS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_ENABLE_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_ENABLE_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_ENABLE_FAILED_DOMAIN_LOOKUP_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_ENABLE_FAILED_DOMAIN_LOOKUP_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_ENABLE_FAILED_PERMISSION_DENIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_ENABLE_FAILED_PERMISSION_DENIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_ENABLE_FAILED_UNAVAILABLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_ENABLE_FAILED_UNAVAILABLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_ENABLE_FAILED_WRONG_CREDENTIALS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_ENABLE_FAILED_WRONG_CREDENTIALS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_IS_DISABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_IS_DISABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_SERVICE_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_SERVICE_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string AUTH_UNKNOWN_TYPE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("AUTH_UNKNOWN_TYPE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string BACKUP_SCRIPT_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("BACKUP_SCRIPT_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string BOOTLOADER_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("BOOTLOADER_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_ADD_TUNNEL_TO_BOND_SLAVE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_ADD_TUNNEL_TO_BOND_SLAVE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_ADD_VLAN_TO_BOND_SLAVE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_ADD_VLAN_TO_BOND_SLAVE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_CONTACT_HOST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_CONTACT_HOST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_CREATE_STATE_FILE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_CREATE_STATE_FILE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_DESTROY_DISASTER_RECOVERY_TASK
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_DESTROY_DISASTER_RECOVERY_TASK", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_DESTROY_SYSTEM_NETWORK
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_DESTROY_SYSTEM_NETWORK", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_ENABLE_REDO_LOG
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_ENABLE_REDO_LOG", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_EVACUATE_HOST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_EVACUATE_HOST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_FETCH_PATCH
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_FETCH_PATCH", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_FIND_OEM_BACKUP_PARTITION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_FIND_OEM_BACKUP_PARTITION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_FIND_PATCH
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_FIND_PATCH", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_FIND_STATE_PARTITION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_FIND_STATE_PARTITION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_PLUG_BOND_SLAVE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_PLUG_BOND_SLAVE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_PLUG_VIF
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_PLUG_VIF", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CANNOT_RESET_CONTROL_DOMAIN
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CANNOT_RESET_CONTROL_DOMAIN", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CERTIFICATE_ALREADY_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CERTIFICATE_ALREADY_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CERTIFICATE_CORRUPT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CERTIFICATE_CORRUPT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CERTIFICATE_DOES_NOT_EXIST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CERTIFICATE_DOES_NOT_EXIST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CERTIFICATE_LIBRARY_CORRUPT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CERTIFICATE_LIBRARY_CORRUPT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CERTIFICATE_NAME_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CERTIFICATE_NAME_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CHANGE_PASSWORD_REJECTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CHANGE_PASSWORD_REJECTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string COULD_NOT_FIND_NETWORK_INTERFACE_WITH_SPECIFIED_DEVICE_NAME_AND_MAC_ADDRESS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("COULD_NOT_FIND_NETWORK_INTERFACE_WITH_SPECIFIED_DEVICE_NAME_AND_MAC_ADDRESS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string COULD_NOT_IMPORT_DATABASE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("COULD_NOT_IMPORT_DATABASE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CPU_FEATURE_MASKING_NOT_SUPPORTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CPU_FEATURE_MASKING_NOT_SUPPORTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CRL_ALREADY_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CRL_ALREADY_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CRL_CORRUPT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CRL_CORRUPT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CRL_DOES_NOT_EXIST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CRL_DOES_NOT_EXIST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string CRL_NAME_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("CRL_NAME_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DB_UNIQUENESS_CONSTRAINT_VIOLATION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DB_UNIQUENESS_CONSTRAINT_VIOLATION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DEFAULT_SR_NOT_FOUND
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DEFAULT_SR_NOT_FOUND", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DEVICE_ALREADY_ATTACHED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DEVICE_ALREADY_ATTACHED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DEVICE_ALREADY_DETACHED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DEVICE_ALREADY_DETACHED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DEVICE_ALREADY_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DEVICE_ALREADY_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DEVICE_ATTACH_TIMEOUT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DEVICE_ATTACH_TIMEOUT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DEVICE_DETACH_REJECTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DEVICE_DETACH_REJECTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DEVICE_DETACH_TIMEOUT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DEVICE_DETACH_TIMEOUT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DEVICE_NOT_ATTACHED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DEVICE_NOT_ATTACHED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DISK_VBD_MUST_BE_READWRITE_FOR_HVM
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DISK_VBD_MUST_BE_READWRITE_FOR_HVM", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DOMAIN_BUILDER_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DOMAIN_BUILDER_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DOMAIN_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DOMAIN_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DUPLICATE_PIF_DEVICE_NAME
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DUPLICATE_PIF_DEVICE_NAME", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string DUPLICATE_VM
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("DUPLICATE_VM", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string EVACUATE_NO_OTHER_HOSTS_FOR_MASTER
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("EVACUATE_NO_OTHER_HOSTS_FOR_MASTER", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string EVENT_FROM_TOKEN_PARSE_FAILURE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("EVENT_FROM_TOKEN_PARSE_FAILURE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string EVENT_SUBSCRIPTION_PARSE_FAILURE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("EVENT_SUBSCRIPTION_PARSE_FAILURE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string EVENTS_LOST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("EVENTS_LOST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string FEATURE_REQUIRES_HVM
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("FEATURE_REQUIRES_HVM", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string FEATURE_RESTRICTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("FEATURE_RESTRICTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string FIELD_TYPE_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("FIELD_TYPE_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string GPU_GROUP_CONTAINS_PGPU
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("GPU_GROUP_CONTAINS_PGPU", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string GPU_GROUP_CONTAINS_VGPU
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("GPU_GROUP_CONTAINS_VGPU", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_ABORT_NEW_MASTER
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_ABORT_NEW_MASTER", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_CANNOT_CHANGE_BOND_STATUS_OF_MGMT_IFACE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_CANNOT_CHANGE_BOND_STATUS_OF_MGMT_IFACE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_CONSTRAINT_VIOLATION_NETWORK_NOT_SHARED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_CONSTRAINT_VIOLATION_NETWORK_NOT_SHARED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_CONSTRAINT_VIOLATION_SR_NOT_SHARED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_CONSTRAINT_VIOLATION_SR_NOT_SHARED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_FAILED_TO_FORM_LIVESET
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_FAILED_TO_FORM_LIVESET", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_HEARTBEAT_DAEMON_STARTUP_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_HEARTBEAT_DAEMON_STARTUP_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_HOST_CANNOT_ACCESS_STATEFILE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_HOST_CANNOT_ACCESS_STATEFILE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_HOST_CANNOT_SEE_PEERS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_HOST_CANNOT_SEE_PEERS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_HOST_IS_ARMED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_HOST_IS_ARMED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_IS_ENABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_IS_ENABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_LOST_STATEFILE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_LOST_STATEFILE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_NO_PLAN
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_NO_PLAN", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_NOT_ENABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_NOT_ENABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_NOT_INSTALLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_NOT_INSTALLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_OPERATION_WOULD_BREAK_FAILOVER_PLAN
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_OPERATION_WOULD_BREAK_FAILOVER_PLAN", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_POOL_IS_ENABLED_BUT_HOST_IS_DISABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_POOL_IS_ENABLED_BUT_HOST_IS_DISABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_SHOULD_BE_FENCED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_SHOULD_BE_FENCED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HA_TOO_FEW_HOSTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HA_TOO_FEW_HOSTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HANDLE_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HANDLE_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_BROKEN
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_BROKEN", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_CANNOT_ATTACH_NETWORK
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_CANNOT_ATTACH_NETWORK", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_CANNOT_ATTACH_NETWORK_SHORT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_CANNOT_ATTACH_NETWORK-SHORT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_CANNOT_DESTROY_SELF
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_CANNOT_DESTROY_SELF", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_CANNOT_READ_METRICS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_CANNOT_READ_METRICS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_CD_DRIVE_EMPTY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_CD_DRIVE_EMPTY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_DISABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_DISABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_DISABLED_SHORT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_DISABLED-SHORT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_DISABLED_UNTIL_REBOOT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_DISABLED_UNTIL_REBOOT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_HAS_NO_MANAGEMENT_IP
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_HAS_NO_MANAGEMENT_IP", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_HAS_RESIDENT_VMS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_HAS_RESIDENT_VMS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_IN_EMERGENCY_MODE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_IN_EMERGENCY_MODE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_IN_RECOVERY_MODE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_IN_RECOVERY_MODE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_IN_USE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_IN_USE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_IS_LIVE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_IS_LIVE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_IS_SLAVE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_IS_SLAVE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_ITS_OWN_SLAVE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_ITS_OWN_SLAVE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_MASTER_CANNOT_TALK_BACK
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_MASTER_CANNOT_TALK_BACK", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_NAME_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_NAME_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_NOT_DISABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_NOT_DISABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_NOT_ENOUGH_FREE_MEMORY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_NOT_ENOUGH_FREE_MEMORY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_NOT_ENOUGH_FREE_MEMORY_SHORT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_NOT_ENOUGH_FREE_MEMORY-SHORT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_NOT_IN_RECOVERY_MODE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_NOT_IN_RECOVERY_MODE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_NOT_LIVE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_NOT_LIVE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_NOT_LIVE_SHORT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_NOT_LIVE-SHORT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_OFFLINE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_OFFLINE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_POWER_ON_MODE_DISABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_POWER_ON_MODE_DISABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_STILL_BOOTING
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_STILL_BOOTING", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOST_UNKNOWN_TO_MASTER
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOST_UNKNOWN_TO_MASTER", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOSTS_NOT_COMPATIBLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOSTS_NOT_COMPATIBLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string HOSTS_NOT_HOMOGENEOUS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("HOSTS_NOT_HOMOGENEOUS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string ILLEGAL_VBD_DEVICE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("ILLEGAL_VBD_DEVICE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IMPORT_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IMPORT_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IMPORT_ERROR_ATTACHED_DISKS_NOT_FOUND
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IMPORT_ERROR_ATTACHED_DISKS_NOT_FOUND", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IMPORT_ERROR_CANNOT_HANDLE_CHUNKED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IMPORT_ERROR_CANNOT_HANDLE_CHUNKED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IMPORT_ERROR_FAILED_TO_FIND_OBJECT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IMPORT_ERROR_FAILED_TO_FIND_OBJECT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IMPORT_ERROR_PREMATURE_EOF
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IMPORT_ERROR_PREMATURE_EOF", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IMPORT_ERROR_SOME_CHECKSUMS_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IMPORT_ERROR_SOME_CHECKSUMS_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IMPORT_ERROR_UNEXPECTED_FILE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IMPORT_ERROR_UNEXPECTED_FILE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IMPORT_INCOMPATIBLE_VERSION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IMPORT_INCOMPATIBLE_VERSION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INTERFACE_HAS_NO_IP
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INTERFACE_HAS_NO_IP", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INTERNAL_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INTERNAL_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INVALID_DEVICE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INVALID_DEVICE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INVALID_EDITION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INVALID_EDITION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INVALID_FEATURE_STRING
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INVALID_FEATURE_STRING", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INVALID_IP_ADDRESS_SPECIFIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INVALID_IP_ADDRESS_SPECIFIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INVALID_PATCH
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INVALID_PATCH", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INVALID_PATCH_WITH_LOG
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INVALID_PATCH_WITH_LOG", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string INVALID_VALUE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("INVALID_VALUE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string IS_TUNNEL_ACCESS_PIF
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("IS_TUNNEL_ACCESS_PIF", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string JOINING_HOST_CANNOT_BE_MASTER_OF_OTHER_HOSTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("JOINING_HOST_CANNOT_BE_MASTER_OF_OTHER_HOSTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string JOINING_HOST_CANNOT_CONTAIN_SHARED_SRS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("JOINING_HOST_CANNOT_CONTAIN_SHARED_SRS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string JOINING_HOST_CANNOT_HAVE_RUNNING_OR_SUSPENDED_VMS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("JOINING_HOST_CANNOT_HAVE_RUNNING_OR_SUSPENDED_VMS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string JOINING_HOST_CANNOT_HAVE_RUNNING_VMS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("JOINING_HOST_CANNOT_HAVE_RUNNING_VMS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string JOINING_HOST_CANNOT_HAVE_VMS_WITH_CURRENT_OPERATIONS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("JOINING_HOST_CANNOT_HAVE_VMS_WITH_CURRENT_OPERATIONS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string JOINING_HOST_CONNECTION_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("JOINING_HOST_CONNECTION_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string JOINING_HOST_SERVICE_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("JOINING_HOST_SERVICE_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LICENCE_RESTRICTION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LICENCE_RESTRICTION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LICENSE_CANNOT_DOWNGRADE_WHILE_IN_POOL
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LICENSE_CANNOT_DOWNGRADE_WHILE_IN_POOL", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LICENSE_CHECKOUT_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LICENSE_CHECKOUT_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LICENSE_DOES_NOT_SUPPORT_POOLING
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LICENSE_DOES_NOT_SUPPORT_POOLING", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LICENSE_DOES_NOT_SUPPORT_XHA
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LICENSE_DOES_NOT_SUPPORT_XHA", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LICENSE_EXPIRED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LICENSE_EXPIRED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LICENSE_FILE_DEPRECATED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LICENSE_FILE_DEPRECATED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LICENSE_PROCESSING_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LICENSE_PROCESSING_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LIKEWISE_WINERROR_0251E
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LIKEWISE_WINERROR_0251E", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string LOCATION_NOT_UNIQUE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("LOCATION_NOT_UNIQUE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MAC_DOES_NOT_EXIST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MAC_DOES_NOT_EXIST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MAC_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MAC_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MAC_STILL_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MAC_STILL_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MAP_DUPLICATE_KEY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MAP_DUPLICATE_KEY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MEM_MAX_ALLOWED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MEM_MAX_ALLOWED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MEM_MAX_ALLOWED_TITLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MEM_MAX_ALLOWED_TITLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MESSAGE_DEPRECATED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MESSAGE_DEPRECATED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MESSAGE_METHOD_UNKNOWN
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MESSAGE_METHOD_UNKNOWN", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MESSAGE_PARAMETER_COUNT_MISMATCH
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MESSAGE_PARAMETER_COUNT_MISMATCH", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MIRROR_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MIRROR_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string MISSING_CONNECTION_DETAILS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("MISSING_CONNECTION_DETAILS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NETWORK_ALREADY_CONNECTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NETWORK_ALREADY_CONNECTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NETWORK_CONTAINS_PIF
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NETWORK_CONTAINS_PIF", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NETWORK_CONTAINS_VIF
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NETWORK_CONTAINS_VIF", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NO_HOSTS_AVAILABLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NO_HOSTS_AVAILABLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NO_MORE_REDO_LOGS_ALLOWED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NO_MORE_REDO_LOGS_ALLOWED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NOT_ALLOWED_ON_OEM_EDITION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NOT_ALLOWED_ON_OEM_EDITION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NOT_IMPLEMENTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NOT_IMPLEMENTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NOT_IN_EMERGENCY_MODE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NOT_IN_EMERGENCY_MODE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NOT_SUPPORTED_DURING_UPGRADE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NOT_SUPPORTED_DURING_UPGRADE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string NOT_SYSTEM_DOMAIN
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("NOT_SYSTEM_DOMAIN", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string OBJECT_NOLONGER_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("OBJECT_NOLONGER_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string ONLY_ALLOWED_ON_OEM_EDITION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("ONLY_ALLOWED_ON_OEM_EDITION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string OPENVSWITCH_NOT_ACTIVE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("OPENVSWITCH_NOT_ACTIVE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string OPERATION_BLOCKED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("OPERATION_BLOCKED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string OPERATION_NOT_ALLOWED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("OPERATION_NOT_ALLOWED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string OPERATION_PARTIALLY_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("OPERATION_PARTIALLY_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string OTHER_OPERATION_IN_PROGRESS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("OTHER_OPERATION_IN_PROGRESS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string OUT_OF_SPACE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("OUT_OF_SPACE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_ALREADY_APPLIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_ALREADY_APPLIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_ALREADY_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_ALREADY_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_APPLY_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_APPLY_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_IS_APPLIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_IS_APPLIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_PRECHECK_FAILED_PREREQUISITE_MISSING
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_PRECHECK_FAILED_PREREQUISITE_MISSING", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_PRECHECK_FAILED_UNKNOWN_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_PRECHECK_FAILED_UNKNOWN_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_PRECHECK_FAILED_VM_RUNNING
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_PRECHECK_FAILED_VM_RUNNING", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_PRECHECK_FAILED_WRONG_SERVER_BUILD
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_PRECHECK_FAILED_WRONG_SERVER_BUILD", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PATCH_PRECHECK_FAILED_WRONG_SERVER_VERSION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PATCH_PRECHECK_FAILED_WRONG_SERVER_VERSION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PBD_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PBD_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PERMISSION_DENIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PERMISSION_DENIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_ALREADY_BONDED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_ALREADY_BONDED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_BOND_NEEDS_MORE_MEMBERS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_BOND_NEEDS_MORE_MEMBERS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_CANNOT_BOND_CROSS_HOST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_CANNOT_BOND_CROSS_HOST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_CONFIGURATION_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_CONFIGURATION_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_DEVICE_NOT_FOUND
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_DEVICE_NOT_FOUND", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_DOES_NOT_ALLOW_UNPLUG
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_DOES_NOT_ALLOW_UNPLUG", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_HAS_NO_NETWORK_CONFIGURATION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_HAS_NO_NETWORK_CONFIGURATION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_HAS_NO_V6_NETWORK_CONFIGURATION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_HAS_NO_V6_NETWORK_CONFIGURATION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_INCOMPATIBLE_PRIMARY_ADDRESS_TYPE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_INCOMPATIBLE_PRIMARY_ADDRESS_TYPE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_IS_MANAGEMENT_INTERFACE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_IS_MANAGEMENT_INTERFACE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_IS_PHYSICAL
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_IS_PHYSICAL", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_IS_VLAN
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_IS_VLAN", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_TUNNEL_STILL_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_TUNNEL_STILL_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_VLAN_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_VLAN_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PIF_VLAN_STILL_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PIF_VLAN_STILL_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_ALREADY_ENABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_ALREADY_ENABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_DISABLE_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_DISABLE_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_DISABLE_FAILED_PERMISSION_DENIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_DISABLE_FAILED_PERMISSION_DENIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_DISABLE_FAILED_WRONG_CREDENTIALS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_DISABLE_FAILED_WRONG_CREDENTIALS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_ENABLE_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_ENABLE_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_ENABLE_FAILED_DOMAIN_LOOKUP_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_ENABLE_FAILED_DOMAIN_LOOKUP_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_ENABLE_FAILED_DUPLICATE_HOSTNAME
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_ENABLE_FAILED_DUPLICATE_HOSTNAME", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_ENABLE_FAILED_INVALID_OU
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_ENABLE_FAILED_INVALID_OU", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_ENABLE_FAILED_PERMISSION_DENIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_ENABLE_FAILED_PERMISSION_DENIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_ENABLE_FAILED_UNAVAILABLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_ENABLE_FAILED_UNAVAILABLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_AUTH_ENABLE_FAILED_WRONG_CREDENTIALS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_AUTH_ENABLE_FAILED_WRONG_CREDENTIALS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_JOINING_EXTERNAL_AUTH_MISMATCH
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_JOINING_EXTERNAL_AUTH_MISMATCH", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_JOINING_HOST_MUST_HAVE_PHYSICAL_MANAGEMENT_NIC
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_JOINING_HOST_MUST_HAVE_PHYSICAL_MANAGEMENT_NIC", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string POOL_JOINING_HOST_MUST_HAVE_SAME_PRODUCT_VERSION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("POOL_JOINING_HOST_MUST_HAVE_SAME_PRODUCT_VERSION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PROVISION_FAILED_OUT_OF_SPACE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PROVISION_FAILED_OUT_OF_SPACE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string PROVISION_ONLY_ALLOWED_ON_TEMPLATE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("PROVISION_ONLY_ALLOWED_ON_TEMPLATE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string RBAC_PERMISSION_DENIED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("RBAC_PERMISSION_DENIED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string RBAC_PERMISSION_DENIED_FRIENDLY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("RBAC_PERMISSION_DENIED_FRIENDLY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string RBAC_PERMISSION_DENIED_FRIENDLY_CONNECTION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("RBAC_PERMISSION_DENIED_FRIENDLY_CONNECTION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string REDO_LOG_IS_ENABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("REDO_LOG_IS_ENABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string RESTORE_INCOMPATIBLE_VERSION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("RESTORE_INCOMPATIBLE_VERSION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string RESTORE_SCRIPT_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("RESTORE_SCRIPT_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string RESTORE_TARGET_MGMT_IF_NOT_IN_BACKUP
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("RESTORE_TARGET_MGMT_IF_NOT_IN_BACKUP", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string RESTORE_TARGET_MISSING_DEVICE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("RESTORE_TARGET_MISSING_DEVICE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string ROLE_ALREADY_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("ROLE_ALREADY_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string ROLE_NOT_FOUND
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("ROLE_NOT_FOUND", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SESSION_AUTHENTICATION_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SESSION_AUTHENTICATION_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SESSION_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SESSION_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SESSION_NOT_REGISTERED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SESSION_NOT_REGISTERED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SLAVE_REQUIRES_MANAGEMENT_INTERFACE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SLAVE_REQUIRES_MANAGEMENT_INTERFACE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SM_PLUGIN_COMMUNICATION_FAILURE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SM_PLUGIN_COMMUNICATION_FAILURE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_ATTACH_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_ATTACH_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_1
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_1", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_100
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_100", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_101
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_101", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_102
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_102", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_103
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_103", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_104
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_104", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_105
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_105", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_106
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_106", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_107
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_107", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_108
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_108", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_109
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_109", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_110
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_110", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_120
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_120", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_121
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_121", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_122
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_122", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_123
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_123", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_124
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_124", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_125
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_125", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_126
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_126", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_127
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_127", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_128
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_128", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_129
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_129", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_130
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_130", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_131
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_131", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_132
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_132", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_133
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_133", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_134
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_134", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_135
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_135", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_140
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_140", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_141
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_141", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_142
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_142", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_143
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_143", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_144
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_144", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_150
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_150", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_151
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_151", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_152
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_152", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_153
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_153", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_16
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_16", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_160
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_160", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_161
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_161", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_162
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_162", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_163
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_163", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_164
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_164", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_165
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_165", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_166
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_166", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_167
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_167", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_168
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_168", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_169
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_169", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_170
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_170", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_171
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_171", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_172
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_172", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_173
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_173", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_174
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_174", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_175
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_175", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_176
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_176", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_180
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_180", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_181
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_181", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_19
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_19", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_2
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_2", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_20
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_20", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_200
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_200", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_201
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_201", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_202
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_202", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_203
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_203", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_220
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_220", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_221
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_221", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_222
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_222", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_223
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_223", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_224
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_224", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_225
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_225", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_226
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_226", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_24
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_24", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_37
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_37", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_38
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_38", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_39
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_39", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_40
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_40", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_400
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_400", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_401
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_401", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_402
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_402", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_41
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_41", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_410
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_410", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_411
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_411", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_412
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_412", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_413
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_413", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_414
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_414", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_416
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_416", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_417
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_417", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_418
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_418", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_419
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_419", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_42
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_42", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_420
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_420", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_421
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_421", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_422
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_422", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_423
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_423", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_424
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_424", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_425
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_425", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_426
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_426", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_427
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_427", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_428
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_428", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_429
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_429", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_43
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_43", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_430
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_430", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_431
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_431", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_432
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_432", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_433
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_433", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_434
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_434", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_435
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_435", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_436
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_436", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_437
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_437", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_438
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_438", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_439
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_439", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_44
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_44", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_440
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_440", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_441
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_441", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_442
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_442", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_443
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_443", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_444
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_444", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_445
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_445", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_446
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_446", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_46
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_46", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_47
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_47", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_48
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_48", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_49
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_49", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_50
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_50", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_51
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_51", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_52
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_52", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_53
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_53", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_54
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_54", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_55
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_55", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_56
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_56", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_57
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_57", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_58
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_58", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_59
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_59", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_60
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_60", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_61
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_61", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_62
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_62", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_63
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_63", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_64
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_64", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_65
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_65", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_66
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_66", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_67
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_67", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_68
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_68", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_69
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_69", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_70
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_70", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_71
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_71", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_72
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_72", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_73
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_73", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_74
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_74", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_75
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_75", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_76
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_76", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_77
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_77", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_78
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_78", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_79
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_79", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_80
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_80", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_81
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_81", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_82
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_82", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_83
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_83", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_84
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_84", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_85
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_85", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_86
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_86", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_87
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_87", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_88
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_88", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_89
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_89", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_90
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_90", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_91
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_91", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_92
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_92", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_93
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_93", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_94
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_94", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_95
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_95", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_96
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_96", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_97
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_97", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_98
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_98", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_BACKEND_FAILURE_99
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_BACKEND_FAILURE_99", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_DEVICE_IN_USE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_DEVICE_IN_USE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_FULL
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_FULL", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_HAS_MULTIPLE_PBDS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_HAS_MULTIPLE_PBDS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_HAS_NO_PBDS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_HAS_NO_PBDS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_HAS_PBD
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_HAS_PBD", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_INDESTRUCTIBLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_INDESTRUCTIBLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_NOT_EMPTY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_NOT_EMPTY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_NOT_SHARABLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_NOT_SHARABLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_OPERATION_NOT_SUPPORTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_OPERATION_NOT_SUPPORTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_REQUIRES_UPGRADE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_REQUIRES_UPGRADE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_UNKNOWN_DRIVER
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_UNKNOWN_DRIVER", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_UUID_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_UUID_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SR_VDI_LOCKING_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SR_VDI_LOCKING_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SSL_VERIFY_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SSL_VERIFY_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SUBJECT_ALREADY_EXISTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SUBJECT_ALREADY_EXISTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SUBJECT_CANNOT_BE_RESOLVED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SUBJECT_CANNOT_BE_RESOLVED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SYSTEM_STATUS_MUST_USE_TAR_ON_OEM
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SYSTEM_STATUS_MUST_USE_TAR_ON_OEM", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string SYSTEM_STATUS_RETRIEVAL_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("SYSTEM_STATUS_RETRIEVAL_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string TASK_CANCELLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("TASK_CANCELLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string TOO_BUSY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("TOO_BUSY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string TOO_MANY_PENDING_TASKS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("TOO_MANY_PENDING_TASKS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string TOO_MANY_STORAGE_MIGRATES
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("TOO_MANY_STORAGE_MIGRATES", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string TRANSPORT_PIF_NOT_CONFIGURED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("TRANSPORT_PIF_NOT_CONFIGURED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string UNKNOWN_BOOTLOADER
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("UNKNOWN_BOOTLOADER", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string USER_IS_NOT_LOCAL_SUPERUSER
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("USER_IS_NOT_LOCAL_SUPERUSER", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string UUID_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("UUID_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string V6D_FAILURE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("V6D_FAILURE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VALUE_NOT_SUPPORTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VALUE_NOT_SUPPORTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VBD_CDS_MUST_BE_READONLY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VBD_CDS_MUST_BE_READONLY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VBD_IS_EMPTY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VBD_IS_EMPTY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VBD_NOT_EMPTY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VBD_NOT_EMPTY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VBD_NOT_REMOVABLE_MEDIA
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VBD_NOT_REMOVABLE_MEDIA", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VBD_NOT_UNPLUGGABLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VBD_NOT_UNPLUGGABLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VBD_TRAY_LOCKED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VBD_TRAY_LOCKED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VBDS_MAX_ALLOWED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VBDS_MAX_ALLOWED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VBDS_MAX_ALLOWED_TITLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VBDS_MAX_ALLOWED_TITLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VCPUS_MAX_ALLOWED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VCPUS_MAX_ALLOWED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VCPUS_MAX_ALLOWED_TITLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VCPUS_MAX_ALLOWED_TITLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_CONTAINS_METADATA_OF_THIS_POOL
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_CONTAINS_METADATA_OF_THIS_POOL", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_IN_USE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_IN_USE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_INCOMPATIBLE_TYPE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_INCOMPATIBLE_TYPE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_IS_A_PHYSICAL_DEVICE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_IS_A_PHYSICAL_DEVICE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_IS_NOT_ISO
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_IS_NOT_ISO", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_LOCATION_MISSING
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_LOCATION_MISSING", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_MISSING
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_MISSING", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_NEEDS_VM_FOR_MIGRATE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_NEEDS_VM_FOR_MIGRATE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_NOT_AVAILABLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_NOT_AVAILABLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_NOT_IN_MAP
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_NOT_IN_MAP", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_NOT_MANAGED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_NOT_MANAGED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VDI_READONLY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VDI_READONLY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VIF_IN_USE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VIF_IN_USE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VIFS_MAX_ALLOWED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VIFS_MAX_ALLOWED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VIFS_MAX_ALLOWED_TITLE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VIFS_MAX_ALLOWED_TITLE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VLAN_TAG_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VLAN_TAG_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_ASSIGNED_TO_PROTECTION_POLICY
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_ASSIGNED_TO_PROTECTION_POLICY", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_ATTACHED_TO_MORE_THAN_ONE_VDI_WITH_TIMEOFFSET_MARKED_AS_RESET_ON_BOOT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_ATTACHED_TO_MORE_THAN_ONE_VDI_WITH_TIMEOFFSET_MARKED_AS_RESET_ON_BOOT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_BAD_POWER_STATE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_BAD_POWER_STATE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_BIOS_STRINGS_ALREADY_SET
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_BIOS_STRINGS_ALREADY_SET", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_CANNOT_DELETE_DEFAULT_TEMPLATE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_CANNOT_DELETE_DEFAULT_TEMPLATE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_CHECKPOINT_RESUME_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_CHECKPOINT_RESUME_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_CHECKPOINT_SUSPEND_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_CHECKPOINT_SUSPEND_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_CRASHED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_CRASHED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_DUPLICATE_VBD_DEVICE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_DUPLICATE_VBD_DEVICE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_FAILED_SHUTDOWN_ACKNOWLEDGMENT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_FAILED_SHUTDOWN_ACKNOWLEDGMENT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_HALTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_HALTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_HAS_CHECKPOINT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_HAS_CHECKPOINT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_HAS_PCI_ATTACHED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_HAS_PCI_ATTACHED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_HAS_TOO_MANY_SNAPSHOTS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_HAS_TOO_MANY_SNAPSHOTS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_HOST_INCOMPATIBLE_VERSION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_HOST_INCOMPATIBLE_VERSION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_HVM_REQUIRED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_HVM_REQUIRED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_HVM_REQUIRED_SHORT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_HVM_REQUIRED-SHORT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_INCOMPATIBLE_WITH_THIS_HOST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_INCOMPATIBLE_WITH_THIS_HOST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_IS_PART_OF_AN_APPLIANCE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_IS_PART_OF_AN_APPLIANCE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_IS_PROTECTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_IS_PROTECTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_IS_TEMPLATE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_IS_TEMPLATE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_MEMORY_SIZE_TOO_LOW
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_MEMORY_SIZE_TOO_LOW", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_MIGRATE_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_MIGRATE_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_MISSING_PV_DRIVERS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_MISSING_PV_DRIVERS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_NO_CRASHDUMP_SR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_NO_CRASHDUMP_SR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_NO_SUSPEND_SR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_NO_SUSPEND_SR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_NO_VCPUS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_NO_VCPUS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_NOT_RESIDENT_HERE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_NOT_RESIDENT_HERE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_OLD_PV_DRIVERS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_OLD_PV_DRIVERS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REBOOTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REBOOTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REQUIRES_GPU
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REQUIRES_GPU", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REQUIRES_GPU_SHORT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REQUIRES_GPU-SHORT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REQUIRES_IOMMU
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REQUIRES_IOMMU", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REQUIRES_NETWORK
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REQUIRES_NETWORK", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REQUIRES_NETWORK_SHORT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REQUIRES_NETWORK-SHORT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REQUIRES_SR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REQUIRES_SR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REQUIRES_SR_SHORT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REQUIRES_SR-SHORT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REQUIRES_VDI
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REQUIRES_VDI", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_REVERT_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_REVERT_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_SHUTDOWN_TIMEOUT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_SHUTDOWN_TIMEOUT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_SNAPSHOT_WITH_QUIESCE_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_SNAPSHOT_WITH_QUIESCE_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_SNAPSHOT_WITH_QUIESCE_NOT_SUPPORTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_SNAPSHOT_WITH_QUIESCE_NOT_SUPPORTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_SNAPSHOT_WITH_QUIESCE_PLUGIN_DEOS_NOT_RESPOND
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_SNAPSHOT_WITH_QUIESCE_PLUGIN_DEOS_NOT_RESPOND", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_SNAPSHOT_WITH_QUIESCE_TIMEOUT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_SNAPSHOT_WITH_QUIESCE_TIMEOUT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_TO_IMPORT_IS_NOT_NEWER_VERSION
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_TO_IMPORT_IS_NOT_NEWER_VERSION", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_TOO_MANY_VCPUS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_TOO_MANY_VCPUS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VM_UNSAFE_BOOT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VM_UNSAFE_BOOT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VMPP_ARCHIVE_MORE_FREQUENT_THAN_BACKUP
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VMPP_ARCHIVE_MORE_FREQUENT_THAN_BACKUP", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VMPP_HAS_VM
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VMPP_HAS_VM", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string VMS_FAILED_TO_COOPERATE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("VMS_FAILED_TO_COOPERATE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_AUTHENTICATION_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_AUTHENTICATION_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_CONNECTION_REFUSED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_CONNECTION_REFUSED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_CONNECTION_RESET
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_CONNECTION_RESET", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_DISABLED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_DISABLED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_INTERNAL_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_INTERNAL_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_MALFORMED_REQUEST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_MALFORMED_REQUEST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_MALFORMED_RESPONSE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_MALFORMED_RESPONSE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_NOT_INITIALIZED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_NOT_INITIALIZED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_TIMEOUT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_TIMEOUT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_UNKNOWN_HOST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_UNKNOWN_HOST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_URL_INVALID
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_URL_INVALID", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_XENSERVER_AUTHENTICATION_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_XENSERVER_AUTHENTICATION_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_XENSERVER_CONNECTION_REFUSED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_XENSERVER_CONNECTION_REFUSED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_XENSERVER_MALFORMED_RESPONSE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_XENSERVER_MALFORMED_RESPONSE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_XENSERVER_TIMEOUT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_XENSERVER_TIMEOUT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string WLB_XENSERVER_UNKNOWN_HOST
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("WLB_XENSERVER_UNKNOWN_HOST", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XAPI_HOOK_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XAPI_HOOK_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XEN_VSS_REQ_ERROR_ADDING_VOLUME_TO_SNAPSET_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XEN_VSS_REQ_ERROR_ADDING_VOLUME_TO_SNAPSET_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XEN_VSS_REQ_ERROR_CREATING_SNAPSHOT
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XEN_VSS_REQ_ERROR_CREATING_SNAPSHOT", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XEN_VSS_REQ_ERROR_CREATING_SNAPSHOT_XML_STRING
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XEN_VSS_REQ_ERROR_CREATING_SNAPSHOT_XML_STRING", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XEN_VSS_REQ_ERROR_INIT_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XEN_VSS_REQ_ERROR_INIT_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XEN_VSS_REQ_ERROR_NO_VOLUMES_SUPPORTED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XEN_VSS_REQ_ERROR_NO_VOLUMES_SUPPORTED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XEN_VSS_REQ_ERROR_PREPARING_WRITERS
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XEN_VSS_REQ_ERROR_PREPARING_WRITERS", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XEN_VSS_REQ_ERROR_PROV_NOT_LOADED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XEN_VSS_REQ_ERROR_PROV_NOT_LOADED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XEN_VSS_REQ_ERROR_START_SNAPSHOT_SET_FAILED
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XEN_VSS_REQ_ERROR_START_SNAPSHOT_SET_FAILED", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XENAPI_MISSING_PLUGIN
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XENAPI_MISSING_PLUGIN", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XENAPI_PLUGIN_FAILURE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XENAPI_PLUGIN_FAILURE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XMLRPC_GENERAL_ERROR
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XMLRPC_GENERAL_ERROR", FriendlyErrorNames.resourceCulture);
      }
    }

    internal static string XMLRPC_UNMARSHAL_FAILURE
    {
      get
      {
        return FriendlyErrorNames.ResourceManager.GetString("XMLRPC_UNMARSHAL_FAILURE", FriendlyErrorNames.resourceCulture);
      }
    }

    internal FriendlyErrorNames()
    {
    }
  }
}
