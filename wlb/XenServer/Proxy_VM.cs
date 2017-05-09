// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_VM
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_VM
  {
    public string uuid;
    public string[] allowed_operations;
    public object current_operations;
    public string power_state;
    public string name_label;
    public string name_description;
    public string user_version;
    public bool is_a_template;
    public string suspend_VDI;
    public string resident_on;
    public string affinity;
    public string memory_overhead;
    public string memory_target;
    public string memory_static_max;
    public string memory_dynamic_max;
    public string memory_dynamic_min;
    public string memory_static_min;
    public object VCPUs_params;
    public string VCPUs_max;
    public string VCPUs_at_startup;
    public string actions_after_shutdown;
    public string actions_after_reboot;
    public string actions_after_crash;
    public string[] consoles;
    public string[] VIFs;
    public string[] VBDs;
    public string[] crash_dumps;
    public string[] VTPMs;
    public string PV_bootloader;
    public string PV_kernel;
    public string PV_ramdisk;
    public string PV_args;
    public string PV_bootloader_args;
    public string PV_legacy_args;
    public string HVM_boot_policy;
    public object HVM_boot_params;
    public double HVM_shadow_multiplier;
    public object platform;
    public string PCI_bus;
    public object other_config;
    public string domid;
    public string domarch;
    public object last_boot_CPU_flags;
    public bool is_control_domain;
    public string metrics;
    public string guest_metrics;
    public string last_booted_record;
    public string recommendations;
    public object xenstore_data;
    public bool ha_always_run;
    public string ha_restart_priority;
    public bool is_a_snapshot;
    public string snapshot_of;
    public string[] snapshots;
    public DateTime snapshot_time;
    public string transportable_snapshot_id;
    public object blobs;
    public string[] tags;
    public object blocked_operations;
    public object snapshot_info;
    public string snapshot_metadata;
    public string parent;
    public string[] children;
    public object bios_strings;
    public string protection_policy;
    public bool is_snapshot_from_vmpp;
    public string appliance;
    public string start_delay;
    public string shutdown_delay;
    public string order;
    public string[] VGPUs;
    public string[] attached_PCIs;
    public string suspend_SR;
    public string version;
  }
}
