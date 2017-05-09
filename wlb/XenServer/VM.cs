// Decompiled with JetBrains decompiler
// Type: XenAPI.VM
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class VM : XenObject<VM>
  {
    private string _uuid;
    private List<vm_operations> _allowed_operations;
    private Dictionary<string, vm_operations> _current_operations;
    private vm_power_state _power_state;
    private string _name_label;
    private string _name_description;
    private long _user_version;
    private bool _is_a_template;
    private XenRef<VDI> _suspend_VDI;
    private XenRef<Host> _resident_on;
    private XenRef<Host> _affinity;
    private long _memory_overhead;
    private long _memory_target;
    private long _memory_static_max;
    private long _memory_dynamic_max;
    private long _memory_dynamic_min;
    private long _memory_static_min;
    private Dictionary<string, string> _VCPUs_params;
    private long _VCPUs_max;
    private long _VCPUs_at_startup;
    private on_normal_exit _actions_after_shutdown;
    private on_normal_exit _actions_after_reboot;
    private on_crash_behaviour _actions_after_crash;
    private List<XenRef<Console>> _consoles;
    private List<XenRef<VIF>> _VIFs;
    private List<XenRef<VBD>> _VBDs;
    private List<XenRef<Crashdump>> _crash_dumps;
    private List<XenRef<VTPM>> _VTPMs;
    private string _PV_bootloader;
    private string _PV_kernel;
    private string _PV_ramdisk;
    private string _PV_args;
    private string _PV_bootloader_args;
    private string _PV_legacy_args;
    private string _HVM_boot_policy;
    private Dictionary<string, string> _HVM_boot_params;
    private double _HVM_shadow_multiplier;
    private Dictionary<string, string> _platform;
    private string _PCI_bus;
    private Dictionary<string, string> _other_config;
    private long _domid;
    private string _domarch;
    private Dictionary<string, string> _last_boot_CPU_flags;
    private bool _is_control_domain;
    private XenRef<VM_metrics> _metrics;
    private XenRef<VM_guest_metrics> _guest_metrics;
    private string _last_booted_record;
    private string _recommendations;
    private Dictionary<string, string> _xenstore_data;
    private bool _ha_always_run;
    private string _ha_restart_priority;
    private bool _is_a_snapshot;
    private XenRef<VM> _snapshot_of;
    private List<XenRef<VM>> _snapshots;
    private DateTime _snapshot_time;
    private string _transportable_snapshot_id;
    private Dictionary<string, XenRef<Blob>> _blobs;
    private string[] _tags;
    private Dictionary<vm_operations, string> _blocked_operations;
    private Dictionary<string, string> _snapshot_info;
    private string _snapshot_metadata;
    private XenRef<VM> _parent;
    private List<XenRef<VM>> _children;
    private Dictionary<string, string> _bios_strings;
    private XenRef<VMPP> _protection_policy;
    private bool _is_snapshot_from_vmpp;
    private XenRef<VM_appliance> _appliance;
    private long _start_delay;
    private long _shutdown_delay;
    private long _order;
    private List<XenRef<VGPU>> _VGPUs;
    private List<XenRef<PCI>> _attached_PCIs;
    private XenRef<SR> _suspend_SR;
    private long _version;

    public string uuid
    {
      get
      {
        return this._uuid;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._uuid))
          return;
        this._uuid = value;
        this.Changed = true;
        this.NotifyPropertyChanged("uuid");
      }
    }

    public List<vm_operations> allowed_operations
    {
      get
      {
        return this._allowed_operations;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._allowed_operations))
          return;
        this._allowed_operations = value;
        this.Changed = true;
        this.NotifyPropertyChanged("allowed_operations");
      }
    }

    public Dictionary<string, vm_operations> current_operations
    {
      get
      {
        return this._current_operations;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._current_operations))
          return;
        this._current_operations = value;
        this.Changed = true;
        this.NotifyPropertyChanged("current_operations");
      }
    }

    public vm_power_state power_state
    {
      get
      {
        return this._power_state;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._power_state))
          return;
        this._power_state = value;
        this.Changed = true;
        this.NotifyPropertyChanged("power_state");
      }
    }

    public string name_label
    {
      get
      {
        return this._name_label;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._name_label))
          return;
        this._name_label = value;
        this.Changed = true;
        this.NotifyPropertyChanged("name_label");
      }
    }

    public string name_description
    {
      get
      {
        return this._name_description;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._name_description))
          return;
        this._name_description = value;
        this.Changed = true;
        this.NotifyPropertyChanged("name_description");
      }
    }

    public long user_version
    {
      get
      {
        return this._user_version;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._user_version))
          return;
        this._user_version = value;
        this.Changed = true;
        this.NotifyPropertyChanged("user_version");
      }
    }

    public bool is_a_template
    {
      get
      {
        return this._is_a_template;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._is_a_template ? 1 : 0)))
          return;
        this._is_a_template = value;
        this.Changed = true;
        this.NotifyPropertyChanged("is_a_template");
      }
    }

    public XenRef<VDI> suspend_VDI
    {
      get
      {
        return this._suspend_VDI;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._suspend_VDI))
          return;
        this._suspend_VDI = value;
        this.Changed = true;
        this.NotifyPropertyChanged("suspend_VDI");
      }
    }

    public XenRef<Host> resident_on
    {
      get
      {
        return this._resident_on;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._resident_on))
          return;
        this._resident_on = value;
        this.Changed = true;
        this.NotifyPropertyChanged("resident_on");
      }
    }

    public XenRef<Host> affinity
    {
      get
      {
        return this._affinity;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._affinity))
          return;
        this._affinity = value;
        this.Changed = true;
        this.NotifyPropertyChanged("affinity");
      }
    }

    public long memory_overhead
    {
      get
      {
        return this._memory_overhead;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_overhead))
          return;
        this._memory_overhead = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_overhead");
      }
    }

    public long memory_target
    {
      get
      {
        return this._memory_target;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_target))
          return;
        this._memory_target = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_target");
      }
    }

    public long memory_static_max
    {
      get
      {
        return this._memory_static_max;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_static_max))
          return;
        this._memory_static_max = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_static_max");
      }
    }

    public long memory_dynamic_max
    {
      get
      {
        return this._memory_dynamic_max;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_dynamic_max))
          return;
        this._memory_dynamic_max = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_dynamic_max");
      }
    }

    public long memory_dynamic_min
    {
      get
      {
        return this._memory_dynamic_min;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_dynamic_min))
          return;
        this._memory_dynamic_min = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_dynamic_min");
      }
    }

    public long memory_static_min
    {
      get
      {
        return this._memory_static_min;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_static_min))
          return;
        this._memory_static_min = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_static_min");
      }
    }

    public Dictionary<string, string> VCPUs_params
    {
      get
      {
        return this._VCPUs_params;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VCPUs_params))
          return;
        this._VCPUs_params = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VCPUs_params");
      }
    }

    public long VCPUs_max
    {
      get
      {
        return this._VCPUs_max;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VCPUs_max))
          return;
        this._VCPUs_max = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VCPUs_max");
      }
    }

    public long VCPUs_at_startup
    {
      get
      {
        return this._VCPUs_at_startup;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VCPUs_at_startup))
          return;
        this._VCPUs_at_startup = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VCPUs_at_startup");
      }
    }

    public on_normal_exit actions_after_shutdown
    {
      get
      {
        return this._actions_after_shutdown;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._actions_after_shutdown))
          return;
        this._actions_after_shutdown = value;
        this.Changed = true;
        this.NotifyPropertyChanged("actions_after_shutdown");
      }
    }

    public on_normal_exit actions_after_reboot
    {
      get
      {
        return this._actions_after_reboot;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._actions_after_reboot))
          return;
        this._actions_after_reboot = value;
        this.Changed = true;
        this.NotifyPropertyChanged("actions_after_reboot");
      }
    }

    public on_crash_behaviour actions_after_crash
    {
      get
      {
        return this._actions_after_crash;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._actions_after_crash))
          return;
        this._actions_after_crash = value;
        this.Changed = true;
        this.NotifyPropertyChanged("actions_after_crash");
      }
    }

    public List<XenRef<Console>> consoles
    {
      get
      {
        return this._consoles;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._consoles))
          return;
        this._consoles = value;
        this.Changed = true;
        this.NotifyPropertyChanged("consoles");
      }
    }

    public List<XenRef<VIF>> VIFs
    {
      get
      {
        return this._VIFs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VIFs))
          return;
        this._VIFs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VIFs");
      }
    }

    public List<XenRef<VBD>> VBDs
    {
      get
      {
        return this._VBDs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VBDs))
          return;
        this._VBDs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VBDs");
      }
    }

    public List<XenRef<Crashdump>> crash_dumps
    {
      get
      {
        return this._crash_dumps;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._crash_dumps))
          return;
        this._crash_dumps = value;
        this.Changed = true;
        this.NotifyPropertyChanged("crash_dumps");
      }
    }

    public List<XenRef<VTPM>> VTPMs
    {
      get
      {
        return this._VTPMs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VTPMs))
          return;
        this._VTPMs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VTPMs");
      }
    }

    public string PV_bootloader
    {
      get
      {
        return this._PV_bootloader;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PV_bootloader))
          return;
        this._PV_bootloader = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PV_bootloader");
      }
    }

    public string PV_kernel
    {
      get
      {
        return this._PV_kernel;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PV_kernel))
          return;
        this._PV_kernel = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PV_kernel");
      }
    }

    public string PV_ramdisk
    {
      get
      {
        return this._PV_ramdisk;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PV_ramdisk))
          return;
        this._PV_ramdisk = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PV_ramdisk");
      }
    }

    public string PV_args
    {
      get
      {
        return this._PV_args;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PV_args))
          return;
        this._PV_args = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PV_args");
      }
    }

    public string PV_bootloader_args
    {
      get
      {
        return this._PV_bootloader_args;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PV_bootloader_args))
          return;
        this._PV_bootloader_args = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PV_bootloader_args");
      }
    }

    public string PV_legacy_args
    {
      get
      {
        return this._PV_legacy_args;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PV_legacy_args))
          return;
        this._PV_legacy_args = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PV_legacy_args");
      }
    }

    public string HVM_boot_policy
    {
      get
      {
        return this._HVM_boot_policy;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._HVM_boot_policy))
          return;
        this._HVM_boot_policy = value;
        this.Changed = true;
        this.NotifyPropertyChanged("HVM_boot_policy");
      }
    }

    public Dictionary<string, string> HVM_boot_params
    {
      get
      {
        return this._HVM_boot_params;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._HVM_boot_params))
          return;
        this._HVM_boot_params = value;
        this.Changed = true;
        this.NotifyPropertyChanged("HVM_boot_params");
      }
    }

    public double HVM_shadow_multiplier
    {
      get
      {
        return this._HVM_shadow_multiplier;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._HVM_shadow_multiplier))
          return;
        this._HVM_shadow_multiplier = value;
        this.Changed = true;
        this.NotifyPropertyChanged("HVM_shadow_multiplier");
      }
    }

    public Dictionary<string, string> platform
    {
      get
      {
        return this._platform;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._platform))
          return;
        this._platform = value;
        this.Changed = true;
        this.NotifyPropertyChanged("platform");
      }
    }

    public string PCI_bus
    {
      get
      {
        return this._PCI_bus;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PCI_bus))
          return;
        this._PCI_bus = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PCI_bus");
      }
    }

    public Dictionary<string, string> other_config
    {
      get
      {
        return this._other_config;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._other_config))
          return;
        this._other_config = value;
        this.Changed = true;
        this.NotifyPropertyChanged("other_config");
      }
    }

    public long domid
    {
      get
      {
        return this._domid;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._domid))
          return;
        this._domid = value;
        this.Changed = true;
        this.NotifyPropertyChanged("domid");
      }
    }

    public string domarch
    {
      get
      {
        return this._domarch;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._domarch))
          return;
        this._domarch = value;
        this.Changed = true;
        this.NotifyPropertyChanged("domarch");
      }
    }

    public Dictionary<string, string> last_boot_CPU_flags
    {
      get
      {
        return this._last_boot_CPU_flags;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._last_boot_CPU_flags))
          return;
        this._last_boot_CPU_flags = value;
        this.Changed = true;
        this.NotifyPropertyChanged("last_boot_CPU_flags");
      }
    }

    public bool is_control_domain
    {
      get
      {
        return this._is_control_domain;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._is_control_domain ? 1 : 0)))
          return;
        this._is_control_domain = value;
        this.Changed = true;
        this.NotifyPropertyChanged("is_control_domain");
      }
    }

    public XenRef<VM_metrics> metrics
    {
      get
      {
        return this._metrics;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._metrics))
          return;
        this._metrics = value;
        this.Changed = true;
        this.NotifyPropertyChanged("metrics");
      }
    }

    public XenRef<VM_guest_metrics> guest_metrics
    {
      get
      {
        return this._guest_metrics;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._guest_metrics))
          return;
        this._guest_metrics = value;
        this.Changed = true;
        this.NotifyPropertyChanged("guest_metrics");
      }
    }

    public string last_booted_record
    {
      get
      {
        return this._last_booted_record;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._last_booted_record))
          return;
        this._last_booted_record = value;
        this.Changed = true;
        this.NotifyPropertyChanged("last_booted_record");
      }
    }

    public string recommendations
    {
      get
      {
        return this._recommendations;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._recommendations))
          return;
        this._recommendations = value;
        this.Changed = true;
        this.NotifyPropertyChanged("recommendations");
      }
    }

    public Dictionary<string, string> xenstore_data
    {
      get
      {
        return this._xenstore_data;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._xenstore_data))
          return;
        this._xenstore_data = value;
        this.Changed = true;
        this.NotifyPropertyChanged("xenstore_data");
      }
    }

    public bool ha_always_run
    {
      get
      {
        return this._ha_always_run;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._ha_always_run ? 1 : 0)))
          return;
        this._ha_always_run = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_always_run");
      }
    }

    public string ha_restart_priority
    {
      get
      {
        return this._ha_restart_priority;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ha_restart_priority))
          return;
        this._ha_restart_priority = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_restart_priority");
      }
    }

    public bool is_a_snapshot
    {
      get
      {
        return this._is_a_snapshot;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._is_a_snapshot ? 1 : 0)))
          return;
        this._is_a_snapshot = value;
        this.Changed = true;
        this.NotifyPropertyChanged("is_a_snapshot");
      }
    }

    public XenRef<VM> snapshot_of
    {
      get
      {
        return this._snapshot_of;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._snapshot_of))
          return;
        this._snapshot_of = value;
        this.Changed = true;
        this.NotifyPropertyChanged("snapshot_of");
      }
    }

    public List<XenRef<VM>> snapshots
    {
      get
      {
        return this._snapshots;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._snapshots))
          return;
        this._snapshots = value;
        this.Changed = true;
        this.NotifyPropertyChanged("snapshots");
      }
    }

    public DateTime snapshot_time
    {
      get
      {
        return this._snapshot_time;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._snapshot_time))
          return;
        this._snapshot_time = value;
        this.Changed = true;
        this.NotifyPropertyChanged("snapshot_time");
      }
    }

    public string transportable_snapshot_id
    {
      get
      {
        return this._transportable_snapshot_id;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._transportable_snapshot_id))
          return;
        this._transportable_snapshot_id = value;
        this.Changed = true;
        this.NotifyPropertyChanged("transportable_snapshot_id");
      }
    }

    public Dictionary<string, XenRef<Blob>> blobs
    {
      get
      {
        return this._blobs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._blobs))
          return;
        this._blobs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("blobs");
      }
    }

    public string[] tags
    {
      get
      {
        return this._tags;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._tags))
          return;
        this._tags = value;
        this.Changed = true;
        this.NotifyPropertyChanged("tags");
      }
    }

    public Dictionary<vm_operations, string> blocked_operations
    {
      get
      {
        return this._blocked_operations;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._blocked_operations))
          return;
        this._blocked_operations = value;
        this.Changed = true;
        this.NotifyPropertyChanged("blocked_operations");
      }
    }

    public Dictionary<string, string> snapshot_info
    {
      get
      {
        return this._snapshot_info;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._snapshot_info))
          return;
        this._snapshot_info = value;
        this.Changed = true;
        this.NotifyPropertyChanged("snapshot_info");
      }
    }

    public string snapshot_metadata
    {
      get
      {
        return this._snapshot_metadata;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._snapshot_metadata))
          return;
        this._snapshot_metadata = value;
        this.Changed = true;
        this.NotifyPropertyChanged("snapshot_metadata");
      }
    }

    public XenRef<VM> parent
    {
      get
      {
        return this._parent;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._parent))
          return;
        this._parent = value;
        this.Changed = true;
        this.NotifyPropertyChanged("parent");
      }
    }

    public List<XenRef<VM>> children
    {
      get
      {
        return this._children;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._children))
          return;
        this._children = value;
        this.Changed = true;
        this.NotifyPropertyChanged("children");
      }
    }

    public Dictionary<string, string> bios_strings
    {
      get
      {
        return this._bios_strings;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._bios_strings))
          return;
        this._bios_strings = value;
        this.Changed = true;
        this.NotifyPropertyChanged("bios_strings");
      }
    }

    public XenRef<VMPP> protection_policy
    {
      get
      {
        return this._protection_policy;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._protection_policy))
          return;
        this._protection_policy = value;
        this.Changed = true;
        this.NotifyPropertyChanged("protection_policy");
      }
    }

    public bool is_snapshot_from_vmpp
    {
      get
      {
        return this._is_snapshot_from_vmpp;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._is_snapshot_from_vmpp ? 1 : 0)))
          return;
        this._is_snapshot_from_vmpp = value;
        this.Changed = true;
        this.NotifyPropertyChanged("is_snapshot_from_vmpp");
      }
    }

    public XenRef<VM_appliance> appliance
    {
      get
      {
        return this._appliance;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._appliance))
          return;
        this._appliance = value;
        this.Changed = true;
        this.NotifyPropertyChanged("appliance");
      }
    }

    public long start_delay
    {
      get
      {
        return this._start_delay;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._start_delay))
          return;
        this._start_delay = value;
        this.Changed = true;
        this.NotifyPropertyChanged("start_delay");
      }
    }

    public long shutdown_delay
    {
      get
      {
        return this._shutdown_delay;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._shutdown_delay))
          return;
        this._shutdown_delay = value;
        this.Changed = true;
        this.NotifyPropertyChanged("shutdown_delay");
      }
    }

    public long order
    {
      get
      {
        return this._order;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._order))
          return;
        this._order = value;
        this.Changed = true;
        this.NotifyPropertyChanged("order");
      }
    }

    public List<XenRef<VGPU>> VGPUs
    {
      get
      {
        return this._VGPUs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VGPUs))
          return;
        this._VGPUs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VGPUs");
      }
    }

    public List<XenRef<PCI>> attached_PCIs
    {
      get
      {
        return this._attached_PCIs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._attached_PCIs))
          return;
        this._attached_PCIs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("attached_PCIs");
      }
    }

    public XenRef<SR> suspend_SR
    {
      get
      {
        return this._suspend_SR;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._suspend_SR))
          return;
        this._suspend_SR = value;
        this.Changed = true;
        this.NotifyPropertyChanged("suspend_SR");
      }
    }

    public long version
    {
      get
      {
        return this._version;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._version))
          return;
        this._version = value;
        this.Changed = true;
        this.NotifyPropertyChanged("version");
      }
    }

    public VM()
    {
    }

    public VM(string uuid, List<vm_operations> allowed_operations, Dictionary<string, vm_operations> current_operations, vm_power_state power_state, string name_label, string name_description, long user_version, bool is_a_template, XenRef<VDI> suspend_VDI, XenRef<Host> resident_on, XenRef<Host> affinity, long memory_overhead, long memory_target, long memory_static_max, long memory_dynamic_max, long memory_dynamic_min, long memory_static_min, Dictionary<string, string> VCPUs_params, long VCPUs_max, long VCPUs_at_startup, on_normal_exit actions_after_shutdown, on_normal_exit actions_after_reboot, on_crash_behaviour actions_after_crash, List<XenRef<Console>> consoles, List<XenRef<VIF>> VIFs, List<XenRef<VBD>> VBDs, List<XenRef<Crashdump>> crash_dumps, List<XenRef<VTPM>> VTPMs, string PV_bootloader, string PV_kernel, string PV_ramdisk, string PV_args, string PV_bootloader_args, string PV_legacy_args, string HVM_boot_policy, Dictionary<string, string> HVM_boot_params, double HVM_shadow_multiplier, Dictionary<string, string> platform, string PCI_bus, Dictionary<string, string> other_config, long domid, string domarch, Dictionary<string, string> last_boot_CPU_flags, bool is_control_domain, XenRef<VM_metrics> metrics, XenRef<VM_guest_metrics> guest_metrics, string last_booted_record, string recommendations, Dictionary<string, string> xenstore_data, bool ha_always_run, string ha_restart_priority, bool is_a_snapshot, XenRef<VM> snapshot_of, List<XenRef<VM>> snapshots, DateTime snapshot_time, string transportable_snapshot_id, Dictionary<string, XenRef<Blob>> blobs, string[] tags, Dictionary<vm_operations, string> blocked_operations, Dictionary<string, string> snapshot_info, string snapshot_metadata, XenRef<VM> parent, List<XenRef<VM>> children, Dictionary<string, string> bios_strings, XenRef<VMPP> protection_policy, bool is_snapshot_from_vmpp, XenRef<VM_appliance> appliance, long start_delay, long shutdown_delay, long order, List<XenRef<VGPU>> VGPUs, List<XenRef<PCI>> attached_PCIs, XenRef<SR> suspend_SR, long version)
    {
      this.uuid = uuid;
      this.allowed_operations = allowed_operations;
      this.current_operations = current_operations;
      this.power_state = power_state;
      this.name_label = name_label;
      this.name_description = name_description;
      this.user_version = user_version;
      this.is_a_template = is_a_template;
      this.suspend_VDI = suspend_VDI;
      this.resident_on = resident_on;
      this.affinity = affinity;
      this.memory_overhead = memory_overhead;
      this.memory_target = memory_target;
      this.memory_static_max = memory_static_max;
      this.memory_dynamic_max = memory_dynamic_max;
      this.memory_dynamic_min = memory_dynamic_min;
      this.memory_static_min = memory_static_min;
      this.VCPUs_params = VCPUs_params;
      this.VCPUs_max = VCPUs_max;
      this.VCPUs_at_startup = VCPUs_at_startup;
      this.actions_after_shutdown = actions_after_shutdown;
      this.actions_after_reboot = actions_after_reboot;
      this.actions_after_crash = actions_after_crash;
      this.consoles = consoles;
      this.VIFs = VIFs;
      this.VBDs = VBDs;
      this.crash_dumps = crash_dumps;
      this.VTPMs = VTPMs;
      this.PV_bootloader = PV_bootloader;
      this.PV_kernel = PV_kernel;
      this.PV_ramdisk = PV_ramdisk;
      this.PV_args = PV_args;
      this.PV_bootloader_args = PV_bootloader_args;
      this.PV_legacy_args = PV_legacy_args;
      this.HVM_boot_policy = HVM_boot_policy;
      this.HVM_boot_params = HVM_boot_params;
      this.HVM_shadow_multiplier = HVM_shadow_multiplier;
      this.platform = platform;
      this.PCI_bus = PCI_bus;
      this.other_config = other_config;
      this.domid = domid;
      this.domarch = domarch;
      this.last_boot_CPU_flags = last_boot_CPU_flags;
      this.is_control_domain = is_control_domain;
      this.metrics = metrics;
      this.guest_metrics = guest_metrics;
      this.last_booted_record = last_booted_record;
      this.recommendations = recommendations;
      this.xenstore_data = xenstore_data;
      this.ha_always_run = ha_always_run;
      this.ha_restart_priority = ha_restart_priority;
      this.is_a_snapshot = is_a_snapshot;
      this.snapshot_of = snapshot_of;
      this.snapshots = snapshots;
      this.snapshot_time = snapshot_time;
      this.transportable_snapshot_id = transportable_snapshot_id;
      this.blobs = blobs;
      this.tags = tags;
      this.blocked_operations = blocked_operations;
      this.snapshot_info = snapshot_info;
      this.snapshot_metadata = snapshot_metadata;
      this.parent = parent;
      this.children = children;
      this.bios_strings = bios_strings;
      this.protection_policy = protection_policy;
      this.is_snapshot_from_vmpp = is_snapshot_from_vmpp;
      this.appliance = appliance;
      this.start_delay = start_delay;
      this.shutdown_delay = shutdown_delay;
      this.order = order;
      this.VGPUs = VGPUs;
      this.attached_PCIs = attached_PCIs;
      this.suspend_SR = suspend_SR;
      this.version = version;
    }

    public VM(Proxy_VM proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public VM(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.allowed_operations = Helper.StringArrayToEnumList<vm_operations>(Marshalling.ParseStringArray(table, "allowed_operations"));
      this.current_operations = Maps.convert_from_proxy_string_vm_operations((object) Marshalling.ParseHashTable(table, "current_operations"));
      this.power_state = (vm_power_state) Helper.EnumParseDefault(typeof (vm_power_state), Marshalling.ParseString(table, "power_state"));
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.user_version = Marshalling.ParseLong(table, "user_version");
      this.is_a_template = Marshalling.ParseBool(table, "is_a_template");
      this.suspend_VDI = Marshalling.ParseRef<VDI>(table, "suspend_VDI");
      this.resident_on = Marshalling.ParseRef<Host>(table, "resident_on");
      this.affinity = Marshalling.ParseRef<Host>(table, "affinity");
      this.memory_overhead = Marshalling.ParseLong(table, "memory_overhead");
      this.memory_target = Marshalling.ParseLong(table, "memory_target");
      this.memory_static_max = Marshalling.ParseLong(table, "memory_static_max");
      this.memory_dynamic_max = Marshalling.ParseLong(table, "memory_dynamic_max");
      this.memory_dynamic_min = Marshalling.ParseLong(table, "memory_dynamic_min");
      this.memory_static_min = Marshalling.ParseLong(table, "memory_static_min");
      this.VCPUs_params = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "VCPUs_params"));
      this.VCPUs_max = Marshalling.ParseLong(table, "VCPUs_max");
      this.VCPUs_at_startup = Marshalling.ParseLong(table, "VCPUs_at_startup");
      this.actions_after_shutdown = (on_normal_exit) Helper.EnumParseDefault(typeof (on_normal_exit), Marshalling.ParseString(table, "actions_after_shutdown"));
      this.actions_after_reboot = (on_normal_exit) Helper.EnumParseDefault(typeof (on_normal_exit), Marshalling.ParseString(table, "actions_after_reboot"));
      this.actions_after_crash = (on_crash_behaviour) Helper.EnumParseDefault(typeof (on_crash_behaviour), Marshalling.ParseString(table, "actions_after_crash"));
      this.consoles = Marshalling.ParseSetRef<Console>(table, "consoles");
      this.VIFs = Marshalling.ParseSetRef<VIF>(table, "VIFs");
      this.VBDs = Marshalling.ParseSetRef<VBD>(table, "VBDs");
      this.crash_dumps = Marshalling.ParseSetRef<Crashdump>(table, "crash_dumps");
      this.VTPMs = Marshalling.ParseSetRef<VTPM>(table, "VTPMs");
      this.PV_bootloader = Marshalling.ParseString(table, "PV_bootloader");
      this.PV_kernel = Marshalling.ParseString(table, "PV_kernel");
      this.PV_ramdisk = Marshalling.ParseString(table, "PV_ramdisk");
      this.PV_args = Marshalling.ParseString(table, "PV_args");
      this.PV_bootloader_args = Marshalling.ParseString(table, "PV_bootloader_args");
      this.PV_legacy_args = Marshalling.ParseString(table, "PV_legacy_args");
      this.HVM_boot_policy = Marshalling.ParseString(table, "HVM_boot_policy");
      this.HVM_boot_params = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "HVM_boot_params"));
      this.HVM_shadow_multiplier = Marshalling.ParseDouble(table, "HVM_shadow_multiplier");
      this.platform = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "platform"));
      this.PCI_bus = Marshalling.ParseString(table, "PCI_bus");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
      this.domid = Marshalling.ParseLong(table, "domid");
      this.domarch = Marshalling.ParseString(table, "domarch");
      this.last_boot_CPU_flags = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "last_boot_CPU_flags"));
      this.is_control_domain = Marshalling.ParseBool(table, "is_control_domain");
      this.metrics = Marshalling.ParseRef<VM_metrics>(table, "metrics");
      this.guest_metrics = Marshalling.ParseRef<VM_guest_metrics>(table, "guest_metrics");
      this.last_booted_record = Marshalling.ParseString(table, "last_booted_record");
      this.recommendations = Marshalling.ParseString(table, "recommendations");
      this.xenstore_data = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "xenstore_data"));
      this.ha_always_run = Marshalling.ParseBool(table, "ha_always_run");
      this.ha_restart_priority = Marshalling.ParseString(table, "ha_restart_priority");
      this.is_a_snapshot = Marshalling.ParseBool(table, "is_a_snapshot");
      this.snapshot_of = Marshalling.ParseRef<VM>(table, "snapshot_of");
      this.snapshots = Marshalling.ParseSetRef<VM>(table, "snapshots");
      this.snapshot_time = Marshalling.ParseDateTime(table, "snapshot_time");
      this.transportable_snapshot_id = Marshalling.ParseString(table, "transportable_snapshot_id");
      this.blobs = Maps.convert_from_proxy_string_XenRefBlob((object) Marshalling.ParseHashTable(table, "blobs"));
      this.tags = Marshalling.ParseStringArray(table, "tags");
      this.blocked_operations = Maps.convert_from_proxy_vm_operations_string((object) Marshalling.ParseHashTable(table, "blocked_operations"));
      this.snapshot_info = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "snapshot_info"));
      this.snapshot_metadata = Marshalling.ParseString(table, "snapshot_metadata");
      this.parent = Marshalling.ParseRef<VM>(table, "parent");
      this.children = Marshalling.ParseSetRef<VM>(table, "children");
      this.bios_strings = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "bios_strings"));
      this.protection_policy = Marshalling.ParseRef<VMPP>(table, "protection_policy");
      this.is_snapshot_from_vmpp = Marshalling.ParseBool(table, "is_snapshot_from_vmpp");
      this.appliance = Marshalling.ParseRef<VM_appliance>(table, "appliance");
      this.start_delay = Marshalling.ParseLong(table, "start_delay");
      this.shutdown_delay = Marshalling.ParseLong(table, "shutdown_delay");
      this.order = Marshalling.ParseLong(table, "order");
      this.VGPUs = Marshalling.ParseSetRef<VGPU>(table, "VGPUs");
      this.attached_PCIs = Marshalling.ParseSetRef<PCI>(table, "attached_PCIs");
      this.suspend_SR = Marshalling.ParseRef<SR>(table, "suspend_SR");
      this.version = Marshalling.ParseLong(table, "version");
    }

    public override void UpdateFrom(VM update)
    {
      this.uuid = update.uuid;
      this.allowed_operations = update.allowed_operations;
      this.current_operations = update.current_operations;
      this.power_state = update.power_state;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.user_version = update.user_version;
      this.is_a_template = update.is_a_template;
      this.suspend_VDI = update.suspend_VDI;
      this.resident_on = update.resident_on;
      this.affinity = update.affinity;
      this.memory_overhead = update.memory_overhead;
      this.memory_target = update.memory_target;
      this.memory_static_max = update.memory_static_max;
      this.memory_dynamic_max = update.memory_dynamic_max;
      this.memory_dynamic_min = update.memory_dynamic_min;
      this.memory_static_min = update.memory_static_min;
      this.VCPUs_params = update.VCPUs_params;
      this.VCPUs_max = update.VCPUs_max;
      this.VCPUs_at_startup = update.VCPUs_at_startup;
      this.actions_after_shutdown = update.actions_after_shutdown;
      this.actions_after_reboot = update.actions_after_reboot;
      this.actions_after_crash = update.actions_after_crash;
      this.consoles = update.consoles;
      this.VIFs = update.VIFs;
      this.VBDs = update.VBDs;
      this.crash_dumps = update.crash_dumps;
      this.VTPMs = update.VTPMs;
      this.PV_bootloader = update.PV_bootloader;
      this.PV_kernel = update.PV_kernel;
      this.PV_ramdisk = update.PV_ramdisk;
      this.PV_args = update.PV_args;
      this.PV_bootloader_args = update.PV_bootloader_args;
      this.PV_legacy_args = update.PV_legacy_args;
      this.HVM_boot_policy = update.HVM_boot_policy;
      this.HVM_boot_params = update.HVM_boot_params;
      this.HVM_shadow_multiplier = update.HVM_shadow_multiplier;
      this.platform = update.platform;
      this.PCI_bus = update.PCI_bus;
      this.other_config = update.other_config;
      this.domid = update.domid;
      this.domarch = update.domarch;
      this.last_boot_CPU_flags = update.last_boot_CPU_flags;
      this.is_control_domain = update.is_control_domain;
      this.metrics = update.metrics;
      this.guest_metrics = update.guest_metrics;
      this.last_booted_record = update.last_booted_record;
      this.recommendations = update.recommendations;
      this.xenstore_data = update.xenstore_data;
      this.ha_always_run = update.ha_always_run;
      this.ha_restart_priority = update.ha_restart_priority;
      this.is_a_snapshot = update.is_a_snapshot;
      this.snapshot_of = update.snapshot_of;
      this.snapshots = update.snapshots;
      this.snapshot_time = update.snapshot_time;
      this.transportable_snapshot_id = update.transportable_snapshot_id;
      this.blobs = update.blobs;
      this.tags = update.tags;
      this.blocked_operations = update.blocked_operations;
      this.snapshot_info = update.snapshot_info;
      this.snapshot_metadata = update.snapshot_metadata;
      this.parent = update.parent;
      this.children = update.children;
      this.bios_strings = update.bios_strings;
      this.protection_policy = update.protection_policy;
      this.is_snapshot_from_vmpp = update.is_snapshot_from_vmpp;
      this.appliance = update.appliance;
      this.start_delay = update.start_delay;
      this.shutdown_delay = update.shutdown_delay;
      this.order = update.order;
      this.VGPUs = update.VGPUs;
      this.attached_PCIs = update.attached_PCIs;
      this.suspend_SR = update.suspend_SR;
      this.version = update.version;
    }

    internal void UpdateFromProxy(Proxy_VM proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.allowed_operations = proxy.allowed_operations == null ? (List<vm_operations>) null : Helper.StringArrayToEnumList<vm_operations>(proxy.allowed_operations);
      this.current_operations = proxy.current_operations == null ? (Dictionary<string, vm_operations>) null : Maps.convert_from_proxy_string_vm_operations(proxy.current_operations);
      this.power_state = proxy.power_state == null ? vm_power_state.Halted : (vm_power_state) Helper.EnumParseDefault(typeof (vm_power_state), proxy.power_state);
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.user_version = proxy.user_version == null ? 0L : long.Parse(proxy.user_version);
      this.is_a_template = proxy.is_a_template;
      this.suspend_VDI = proxy.suspend_VDI == null ? (XenRef<VDI>) null : XenRef<VDI>.Create(proxy.suspend_VDI);
      this.resident_on = proxy.resident_on == null ? (XenRef<Host>) null : XenRef<Host>.Create(proxy.resident_on);
      this.affinity = proxy.affinity == null ? (XenRef<Host>) null : XenRef<Host>.Create(proxy.affinity);
      this.memory_overhead = proxy.memory_overhead == null ? 0L : long.Parse(proxy.memory_overhead);
      this.memory_target = proxy.memory_target == null ? 0L : long.Parse(proxy.memory_target);
      this.memory_static_max = proxy.memory_static_max == null ? 0L : long.Parse(proxy.memory_static_max);
      this.memory_dynamic_max = proxy.memory_dynamic_max == null ? 0L : long.Parse(proxy.memory_dynamic_max);
      this.memory_dynamic_min = proxy.memory_dynamic_min == null ? 0L : long.Parse(proxy.memory_dynamic_min);
      this.memory_static_min = proxy.memory_static_min == null ? 0L : long.Parse(proxy.memory_static_min);
      this.VCPUs_params = proxy.VCPUs_params == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.VCPUs_params);
      this.VCPUs_max = proxy.VCPUs_max == null ? 0L : long.Parse(proxy.VCPUs_max);
      this.VCPUs_at_startup = proxy.VCPUs_at_startup == null ? 0L : long.Parse(proxy.VCPUs_at_startup);
      this.actions_after_shutdown = proxy.actions_after_shutdown == null ? on_normal_exit.destroy : (on_normal_exit) Helper.EnumParseDefault(typeof (on_normal_exit), proxy.actions_after_shutdown);
      this.actions_after_reboot = proxy.actions_after_reboot == null ? on_normal_exit.destroy : (on_normal_exit) Helper.EnumParseDefault(typeof (on_normal_exit), proxy.actions_after_reboot);
      this.actions_after_crash = proxy.actions_after_crash == null ? on_crash_behaviour.destroy : (on_crash_behaviour) Helper.EnumParseDefault(typeof (on_crash_behaviour), proxy.actions_after_crash);
      this.consoles = proxy.consoles == null ? (List<XenRef<Console>>) null : XenRef<Console>.Create(proxy.consoles);
      this.VIFs = proxy.VIFs == null ? (List<XenRef<VIF>>) null : XenRef<VIF>.Create(proxy.VIFs);
      this.VBDs = proxy.VBDs == null ? (List<XenRef<VBD>>) null : XenRef<VBD>.Create(proxy.VBDs);
      this.crash_dumps = proxy.crash_dumps == null ? (List<XenRef<Crashdump>>) null : XenRef<Crashdump>.Create(proxy.crash_dumps);
      this.VTPMs = proxy.VTPMs == null ? (List<XenRef<VTPM>>) null : XenRef<VTPM>.Create(proxy.VTPMs);
      this.PV_bootloader = proxy.PV_bootloader == null ? (string) null : proxy.PV_bootloader;
      this.PV_kernel = proxy.PV_kernel == null ? (string) null : proxy.PV_kernel;
      this.PV_ramdisk = proxy.PV_ramdisk == null ? (string) null : proxy.PV_ramdisk;
      this.PV_args = proxy.PV_args == null ? (string) null : proxy.PV_args;
      this.PV_bootloader_args = proxy.PV_bootloader_args == null ? (string) null : proxy.PV_bootloader_args;
      this.PV_legacy_args = proxy.PV_legacy_args == null ? (string) null : proxy.PV_legacy_args;
      this.HVM_boot_policy = proxy.HVM_boot_policy == null ? (string) null : proxy.HVM_boot_policy;
      this.HVM_boot_params = proxy.HVM_boot_params == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.HVM_boot_params);
      this.HVM_shadow_multiplier = Convert.ToDouble(proxy.HVM_shadow_multiplier);
      this.platform = proxy.platform == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.platform);
      this.PCI_bus = proxy.PCI_bus == null ? (string) null : proxy.PCI_bus;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
      this.domid = proxy.domid == null ? 0L : long.Parse(proxy.domid);
      this.domarch = proxy.domarch == null ? (string) null : proxy.domarch;
      this.last_boot_CPU_flags = proxy.last_boot_CPU_flags == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.last_boot_CPU_flags);
      this.is_control_domain = proxy.is_control_domain;
      this.metrics = proxy.metrics == null ? (XenRef<VM_metrics>) null : XenRef<VM_metrics>.Create(proxy.metrics);
      this.guest_metrics = proxy.guest_metrics == null ? (XenRef<VM_guest_metrics>) null : XenRef<VM_guest_metrics>.Create(proxy.guest_metrics);
      this.last_booted_record = proxy.last_booted_record == null ? (string) null : proxy.last_booted_record;
      this.recommendations = proxy.recommendations == null ? (string) null : proxy.recommendations;
      this.xenstore_data = proxy.xenstore_data == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.xenstore_data);
      this.ha_always_run = proxy.ha_always_run;
      this.ha_restart_priority = proxy.ha_restart_priority == null ? (string) null : proxy.ha_restart_priority;
      this.is_a_snapshot = proxy.is_a_snapshot;
      this.snapshot_of = proxy.snapshot_of == null ? (XenRef<VM>) null : XenRef<VM>.Create(proxy.snapshot_of);
      this.snapshots = proxy.snapshots == null ? (List<XenRef<VM>>) null : XenRef<VM>.Create(proxy.snapshots);
      this.snapshot_time = proxy.snapshot_time;
      this.transportable_snapshot_id = proxy.transportable_snapshot_id == null ? (string) null : proxy.transportable_snapshot_id;
      this.blobs = proxy.blobs == null ? (Dictionary<string, XenRef<Blob>>) null : Maps.convert_from_proxy_string_XenRefBlob(proxy.blobs);
      this.tags = proxy.tags == null ? new string[0] : proxy.tags;
      this.blocked_operations = proxy.blocked_operations == null ? (Dictionary<vm_operations, string>) null : Maps.convert_from_proxy_vm_operations_string(proxy.blocked_operations);
      this.snapshot_info = proxy.snapshot_info == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.snapshot_info);
      this.snapshot_metadata = proxy.snapshot_metadata == null ? (string) null : proxy.snapshot_metadata;
      this.parent = proxy.parent == null ? (XenRef<VM>) null : XenRef<VM>.Create(proxy.parent);
      this.children = proxy.children == null ? (List<XenRef<VM>>) null : XenRef<VM>.Create(proxy.children);
      this.bios_strings = proxy.bios_strings == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.bios_strings);
      this.protection_policy = proxy.protection_policy == null ? (XenRef<VMPP>) null : XenRef<VMPP>.Create(proxy.protection_policy);
      this.is_snapshot_from_vmpp = proxy.is_snapshot_from_vmpp;
      this.appliance = proxy.appliance == null ? (XenRef<VM_appliance>) null : XenRef<VM_appliance>.Create(proxy.appliance);
      this.start_delay = proxy.start_delay == null ? 0L : long.Parse(proxy.start_delay);
      this.shutdown_delay = proxy.shutdown_delay == null ? 0L : long.Parse(proxy.shutdown_delay);
      this.order = proxy.order == null ? 0L : long.Parse(proxy.order);
      this.VGPUs = proxy.VGPUs == null ? (List<XenRef<VGPU>>) null : XenRef<VGPU>.Create(proxy.VGPUs);
      this.attached_PCIs = proxy.attached_PCIs == null ? (List<XenRef<PCI>>) null : XenRef<PCI>.Create(proxy.attached_PCIs);
      this.suspend_SR = proxy.suspend_SR == null ? (XenRef<SR>) null : XenRef<SR>.Create(proxy.suspend_SR);
      this.version = proxy.version == null ? 0L : long.Parse(proxy.version);
    }

    public Proxy_VM ToProxy()
    {
      return new Proxy_VM()
      {
        uuid = this.uuid != null ? this.uuid : "",
        allowed_operations = this.allowed_operations != null ? Helper.ObjectListToStringArray<vm_operations>(this.allowed_operations) : new string[0],
        current_operations = (object) Maps.convert_to_proxy_string_vm_operations(this.current_operations),
        power_state = vm_power_state_helper.ToString(this.power_state),
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        user_version = this.user_version.ToString(),
        is_a_template = this.is_a_template,
        suspend_VDI = this.suspend_VDI != null ? (string) this.suspend_VDI : "",
        resident_on = this.resident_on != null ? (string) this.resident_on : "",
        affinity = this.affinity != null ? (string) this.affinity : "",
        memory_overhead = this.memory_overhead.ToString(),
        memory_target = this.memory_target.ToString(),
        memory_static_max = this.memory_static_max.ToString(),
        memory_dynamic_max = this.memory_dynamic_max.ToString(),
        memory_dynamic_min = this.memory_dynamic_min.ToString(),
        memory_static_min = this.memory_static_min.ToString(),
        VCPUs_params = (object) Maps.convert_to_proxy_string_string(this.VCPUs_params),
        VCPUs_max = this.VCPUs_max.ToString(),
        VCPUs_at_startup = this.VCPUs_at_startup.ToString(),
        actions_after_shutdown = on_normal_exit_helper.ToString(this.actions_after_shutdown),
        actions_after_reboot = on_normal_exit_helper.ToString(this.actions_after_reboot),
        actions_after_crash = on_crash_behaviour_helper.ToString(this.actions_after_crash),
        consoles = this.consoles != null ? Helper.RefListToStringArray<Console>(this.consoles) : new string[0],
        VIFs = this.VIFs != null ? Helper.RefListToStringArray<VIF>(this.VIFs) : new string[0],
        VBDs = this.VBDs != null ? Helper.RefListToStringArray<VBD>(this.VBDs) : new string[0],
        crash_dumps = this.crash_dumps != null ? Helper.RefListToStringArray<Crashdump>(this.crash_dumps) : new string[0],
        VTPMs = this.VTPMs != null ? Helper.RefListToStringArray<VTPM>(this.VTPMs) : new string[0],
        PV_bootloader = this.PV_bootloader != null ? this.PV_bootloader : "",
        PV_kernel = this.PV_kernel != null ? this.PV_kernel : "",
        PV_ramdisk = this.PV_ramdisk != null ? this.PV_ramdisk : "",
        PV_args = this.PV_args != null ? this.PV_args : "",
        PV_bootloader_args = this.PV_bootloader_args != null ? this.PV_bootloader_args : "",
        PV_legacy_args = this.PV_legacy_args != null ? this.PV_legacy_args : "",
        HVM_boot_policy = this.HVM_boot_policy != null ? this.HVM_boot_policy : "",
        HVM_boot_params = (object) Maps.convert_to_proxy_string_string(this.HVM_boot_params),
        HVM_shadow_multiplier = this.HVM_shadow_multiplier,
        platform = (object) Maps.convert_to_proxy_string_string(this.platform),
        PCI_bus = this.PCI_bus != null ? this.PCI_bus : "",
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config),
        domid = this.domid.ToString(),
        domarch = this.domarch != null ? this.domarch : "",
        last_boot_CPU_flags = (object) Maps.convert_to_proxy_string_string(this.last_boot_CPU_flags),
        is_control_domain = this.is_control_domain,
        metrics = this.metrics != null ? (string) this.metrics : "",
        guest_metrics = this.guest_metrics != null ? (string) this.guest_metrics : "",
        last_booted_record = this.last_booted_record != null ? this.last_booted_record : "",
        recommendations = this.recommendations != null ? this.recommendations : "",
        xenstore_data = (object) Maps.convert_to_proxy_string_string(this.xenstore_data),
        ha_always_run = this.ha_always_run,
        ha_restart_priority = this.ha_restart_priority != null ? this.ha_restart_priority : "",
        is_a_snapshot = this.is_a_snapshot,
        snapshot_of = this.snapshot_of != null ? (string) this.snapshot_of : "",
        snapshots = this.snapshots != null ? Helper.RefListToStringArray<VM>(this.snapshots) : new string[0],
        snapshot_time = this.snapshot_time,
        transportable_snapshot_id = this.transportable_snapshot_id != null ? this.transportable_snapshot_id : "",
        blobs = (object) Maps.convert_to_proxy_string_XenRefBlob(this.blobs),
        tags = this.tags,
        blocked_operations = (object) Maps.convert_to_proxy_vm_operations_string(this.blocked_operations),
        snapshot_info = (object) Maps.convert_to_proxy_string_string(this.snapshot_info),
        snapshot_metadata = this.snapshot_metadata != null ? this.snapshot_metadata : "",
        parent = this.parent != null ? (string) this.parent : "",
        children = this.children != null ? Helper.RefListToStringArray<VM>(this.children) : new string[0],
        bios_strings = (object) Maps.convert_to_proxy_string_string(this.bios_strings),
        protection_policy = this.protection_policy != null ? (string) this.protection_policy : "",
        is_snapshot_from_vmpp = this.is_snapshot_from_vmpp,
        appliance = this.appliance != null ? (string) this.appliance : "",
        start_delay = this.start_delay.ToString(),
        shutdown_delay = this.shutdown_delay.ToString(),
        order = this.order.ToString(),
        VGPUs = this.VGPUs != null ? Helper.RefListToStringArray<VGPU>(this.VGPUs) : new string[0],
        attached_PCIs = this.attached_PCIs != null ? Helper.RefListToStringArray<PCI>(this.attached_PCIs) : new string[0],
        suspend_SR = this.suspend_SR != null ? (string) this.suspend_SR : "",
        version = this.version.ToString()
      };
    }

    public bool DeepEquals(VM other, bool ignoreCurrentOperations)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (!ignoreCurrentOperations && !Helper.AreEqual2<Dictionary<string, vm_operations>>(this.current_operations, other.current_operations) || (!Helper.AreEqual2<string>(this._uuid, other._uuid) || !Helper.AreEqual2<List<vm_operations>>(this._allowed_operations, other._allowed_operations)) || (!Helper.AreEqual2<vm_power_state>(this._power_state, other._power_state) || !Helper.AreEqual2<string>(this._name_label, other._name_label) || (!Helper.AreEqual2<string>(this._name_description, other._name_description) || !Helper.AreEqual2<long>(this._user_version, other._user_version))) || (!Helper.AreEqual2<bool>(this._is_a_template, other._is_a_template) || !Helper.AreEqual2<XenRef<VDI>>(this._suspend_VDI, other._suspend_VDI) || (!Helper.AreEqual2<XenRef<Host>>(this._resident_on, other._resident_on) || !Helper.AreEqual2<XenRef<Host>>(this._affinity, other._affinity)) || (!Helper.AreEqual2<long>(this._memory_overhead, other._memory_overhead) || !Helper.AreEqual2<long>(this._memory_target, other._memory_target) || (!Helper.AreEqual2<long>(this._memory_static_max, other._memory_static_max) || !Helper.AreEqual2<long>(this._memory_dynamic_max, other._memory_dynamic_max)))) || (!Helper.AreEqual2<long>(this._memory_dynamic_min, other._memory_dynamic_min) || !Helper.AreEqual2<long>(this._memory_static_min, other._memory_static_min) || (!Helper.AreEqual2<Dictionary<string, string>>(this._VCPUs_params, other._VCPUs_params) || !Helper.AreEqual2<long>(this._VCPUs_max, other._VCPUs_max)) || (!Helper.AreEqual2<long>(this._VCPUs_at_startup, other._VCPUs_at_startup) || !Helper.AreEqual2<on_normal_exit>(this._actions_after_shutdown, other._actions_after_shutdown) || (!Helper.AreEqual2<on_normal_exit>(this._actions_after_reboot, other._actions_after_reboot) || !Helper.AreEqual2<on_crash_behaviour>(this._actions_after_crash, other._actions_after_crash))) || (!Helper.AreEqual2<List<XenRef<Console>>>(this._consoles, other._consoles) || !Helper.AreEqual2<List<XenRef<VIF>>>(this._VIFs, other._VIFs) || (!Helper.AreEqual2<List<XenRef<VBD>>>(this._VBDs, other._VBDs) || !Helper.AreEqual2<List<XenRef<Crashdump>>>(this._crash_dumps, other._crash_dumps)) || (!Helper.AreEqual2<List<XenRef<VTPM>>>(this._VTPMs, other._VTPMs) || !Helper.AreEqual2<string>(this._PV_bootloader, other._PV_bootloader) || (!Helper.AreEqual2<string>(this._PV_kernel, other._PV_kernel) || !Helper.AreEqual2<string>(this._PV_ramdisk, other._PV_ramdisk))))) || (!Helper.AreEqual2<string>(this._PV_args, other._PV_args) || !Helper.AreEqual2<string>(this._PV_bootloader_args, other._PV_bootloader_args) || (!Helper.AreEqual2<string>(this._PV_legacy_args, other._PV_legacy_args) || !Helper.AreEqual2<string>(this._HVM_boot_policy, other._HVM_boot_policy)) || (!Helper.AreEqual2<Dictionary<string, string>>(this._HVM_boot_params, other._HVM_boot_params) || !Helper.AreEqual2<double>(this._HVM_shadow_multiplier, other._HVM_shadow_multiplier) || (!Helper.AreEqual2<Dictionary<string, string>>(this._platform, other._platform) || !Helper.AreEqual2<string>(this._PCI_bus, other._PCI_bus))) || (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config) || !Helper.AreEqual2<long>(this._domid, other._domid) || (!Helper.AreEqual2<string>(this._domarch, other._domarch) || !Helper.AreEqual2<Dictionary<string, string>>(this._last_boot_CPU_flags, other._last_boot_CPU_flags)) || (!Helper.AreEqual2<bool>(this._is_control_domain, other._is_control_domain) || !Helper.AreEqual2<XenRef<VM_metrics>>(this._metrics, other._metrics) || (!Helper.AreEqual2<XenRef<VM_guest_metrics>>(this._guest_metrics, other._guest_metrics) || !Helper.AreEqual2<string>(this._last_booted_record, other._last_booted_record)))) || (!Helper.AreEqual2<string>(this._recommendations, other._recommendations) || !Helper.AreEqual2<Dictionary<string, string>>(this._xenstore_data, other._xenstore_data) || (!Helper.AreEqual2<bool>(this._ha_always_run, other._ha_always_run) || !Helper.AreEqual2<string>(this._ha_restart_priority, other._ha_restart_priority)) || (!Helper.AreEqual2<bool>(this._is_a_snapshot, other._is_a_snapshot) || !Helper.AreEqual2<XenRef<VM>>(this._snapshot_of, other._snapshot_of) || (!Helper.AreEqual2<List<XenRef<VM>>>(this._snapshots, other._snapshots) || !Helper.AreEqual2<DateTime>(this._snapshot_time, other._snapshot_time))) || (!Helper.AreEqual2<string>(this._transportable_snapshot_id, other._transportable_snapshot_id) || !Helper.AreEqual2<Dictionary<string, XenRef<Blob>>>(this._blobs, other._blobs) || (!Helper.AreEqual2<string[]>(this._tags, other._tags) || !Helper.AreEqual2<Dictionary<vm_operations, string>>(this._blocked_operations, other._blocked_operations)) || (!Helper.AreEqual2<Dictionary<string, string>>(this._snapshot_info, other._snapshot_info) || !Helper.AreEqual2<string>(this._snapshot_metadata, other._snapshot_metadata) || (!Helper.AreEqual2<XenRef<VM>>(this._parent, other._parent) || !Helper.AreEqual2<List<XenRef<VM>>>(this._children, other._children)))))) || (!Helper.AreEqual2<Dictionary<string, string>>(this._bios_strings, other._bios_strings) || !Helper.AreEqual2<XenRef<VMPP>>(this._protection_policy, other._protection_policy) || (!Helper.AreEqual2<bool>(this._is_snapshot_from_vmpp, other._is_snapshot_from_vmpp) || !Helper.AreEqual2<XenRef<VM_appliance>>(this._appliance, other._appliance)) || (!Helper.AreEqual2<long>(this._start_delay, other._start_delay) || !Helper.AreEqual2<long>(this._shutdown_delay, other._shutdown_delay) || (!Helper.AreEqual2<long>(this._order, other._order) || !Helper.AreEqual2<List<XenRef<VGPU>>>(this._VGPUs, other._VGPUs))) || (!Helper.AreEqual2<List<XenRef<PCI>>>(this._attached_PCIs, other._attached_PCIs) || !Helper.AreEqual2<XenRef<SR>>(this._suspend_SR, other._suspend_SR))))
        return false;
      return Helper.AreEqual2<long>(this._version, other._version);
    }

    public override string SaveChanges(Session session, string opaqueRef, VM server)
    {
      if (opaqueRef == null)
      {
        Proxy_VM _record = this.ToProxy();
        return session.proxy.vm_create(session.uuid, _record).parse();
      }
      if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
        VM.set_name_label(session, opaqueRef, this._name_label);
      if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
        VM.set_name_description(session, opaqueRef, this._name_description);
      if (!Helper.AreEqual2<long>(this._user_version, server._user_version))
        VM.set_user_version(session, opaqueRef, this._user_version);
      if (!Helper.AreEqual2<bool>(this._is_a_template, server._is_a_template))
        VM.set_is_a_template(session, opaqueRef, this._is_a_template);
      if (!Helper.AreEqual2<XenRef<Host>>(this._affinity, server._affinity))
        VM.set_affinity(session, opaqueRef, (string) this._affinity);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._VCPUs_params, server._VCPUs_params))
        VM.set_VCPUs_params(session, opaqueRef, this._VCPUs_params);
      if (!Helper.AreEqual2<on_normal_exit>(this._actions_after_shutdown, server._actions_after_shutdown))
        VM.set_actions_after_shutdown(session, opaqueRef, this._actions_after_shutdown);
      if (!Helper.AreEqual2<on_normal_exit>(this._actions_after_reboot, server._actions_after_reboot))
        VM.set_actions_after_reboot(session, opaqueRef, this._actions_after_reboot);
      if (!Helper.AreEqual2<on_crash_behaviour>(this._actions_after_crash, server._actions_after_crash))
        VM.set_actions_after_crash(session, opaqueRef, this._actions_after_crash);
      if (!Helper.AreEqual2<string>(this._PV_bootloader, server._PV_bootloader))
        VM.set_PV_bootloader(session, opaqueRef, this._PV_bootloader);
      if (!Helper.AreEqual2<string>(this._PV_kernel, server._PV_kernel))
        VM.set_PV_kernel(session, opaqueRef, this._PV_kernel);
      if (!Helper.AreEqual2<string>(this._PV_ramdisk, server._PV_ramdisk))
        VM.set_PV_ramdisk(session, opaqueRef, this._PV_ramdisk);
      if (!Helper.AreEqual2<string>(this._PV_args, server._PV_args))
        VM.set_PV_args(session, opaqueRef, this._PV_args);
      if (!Helper.AreEqual2<string>(this._PV_bootloader_args, server._PV_bootloader_args))
        VM.set_PV_bootloader_args(session, opaqueRef, this._PV_bootloader_args);
      if (!Helper.AreEqual2<string>(this._PV_legacy_args, server._PV_legacy_args))
        VM.set_PV_legacy_args(session, opaqueRef, this._PV_legacy_args);
      if (!Helper.AreEqual2<string>(this._HVM_boot_policy, server._HVM_boot_policy))
        VM.set_HVM_boot_policy(session, opaqueRef, this._HVM_boot_policy);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._HVM_boot_params, server._HVM_boot_params))
        VM.set_HVM_boot_params(session, opaqueRef, this._HVM_boot_params);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._platform, server._platform))
        VM.set_platform(session, opaqueRef, this._platform);
      if (!Helper.AreEqual2<string>(this._PCI_bus, server._PCI_bus))
        VM.set_PCI_bus(session, opaqueRef, this._PCI_bus);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        VM.set_other_config(session, opaqueRef, this._other_config);
      if (!Helper.AreEqual2<string>(this._recommendations, server._recommendations))
        VM.set_recommendations(session, opaqueRef, this._recommendations);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._xenstore_data, server._xenstore_data))
        VM.set_xenstore_data(session, opaqueRef, this._xenstore_data);
      if (!Helper.AreEqual2<string[]>(this._tags, server._tags))
        VM.set_tags(session, opaqueRef, this._tags);
      if (!Helper.AreEqual2<Dictionary<vm_operations, string>>(this._blocked_operations, server._blocked_operations))
        VM.set_blocked_operations(session, opaqueRef, this._blocked_operations);
      if (!Helper.AreEqual2<XenRef<SR>>(this._suspend_SR, server._suspend_SR))
        VM.set_suspend_SR(session, opaqueRef, (string) this._suspend_SR);
      if (!Helper.AreEqual2<long>(this._memory_static_max, server._memory_static_max))
        VM.set_memory_static_max(session, opaqueRef, this._memory_static_max);
      if (!Helper.AreEqual2<long>(this._memory_dynamic_max, server._memory_dynamic_max))
        VM.set_memory_dynamic_max(session, opaqueRef, this._memory_dynamic_max);
      if (!Helper.AreEqual2<long>(this._memory_dynamic_min, server._memory_dynamic_min))
        VM.set_memory_dynamic_min(session, opaqueRef, this._memory_dynamic_min);
      if (!Helper.AreEqual2<long>(this._memory_static_min, server._memory_static_min))
        VM.set_memory_static_min(session, opaqueRef, this._memory_static_min);
      if (!Helper.AreEqual2<long>(this._VCPUs_max, server._VCPUs_max))
        VM.set_VCPUs_max(session, opaqueRef, this._VCPUs_max);
      if (!Helper.AreEqual2<long>(this._VCPUs_at_startup, server._VCPUs_at_startup))
        VM.set_VCPUs_at_startup(session, opaqueRef, this._VCPUs_at_startup);
      if (!Helper.AreEqual2<double>(this._HVM_shadow_multiplier, server._HVM_shadow_multiplier))
        VM.set_HVM_shadow_multiplier(session, opaqueRef, this._HVM_shadow_multiplier);
      if (!Helper.AreEqual2<bool>(this._ha_always_run, server._ha_always_run))
        VM.set_ha_always_run(session, opaqueRef, this._ha_always_run);
      if (!Helper.AreEqual2<string>(this._ha_restart_priority, server._ha_restart_priority))
        VM.set_ha_restart_priority(session, opaqueRef, this._ha_restart_priority);
      if (!Helper.AreEqual2<XenRef<VMPP>>(this._protection_policy, server._protection_policy))
        VM.set_protection_policy(session, opaqueRef, (string) this._protection_policy);
      if (!Helper.AreEqual2<XenRef<VM_appliance>>(this._appliance, server._appliance))
        VM.set_appliance(session, opaqueRef, (string) this._appliance);
      if (!Helper.AreEqual2<long>(this._start_delay, server._start_delay))
        VM.set_start_delay(session, opaqueRef, this._start_delay);
      if (!Helper.AreEqual2<long>(this._shutdown_delay, server._shutdown_delay))
        VM.set_shutdown_delay(session, opaqueRef, this._shutdown_delay);
      if (!Helper.AreEqual2<long>(this._order, server._order))
        VM.set_order(session, opaqueRef, this._order);
      return (string) null;
    }

    public static VM get_record(Session session, string _vm)
    {
      return new VM(session.proxy.vm_get_record(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<VM> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<VM>.Create(session.proxy.vm_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<VM> create(Session session, VM _record)
    {
      return XenRef<VM>.Create(session.proxy.vm_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, VM _record)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _vm)
    {
      session.proxy.vm_destroy(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_destroy(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<VM>> get_by_name_label(Session session, string _label)
    {
      return XenRef<VM>.Create(session.proxy.vm_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _vm)
    {
      return session.proxy.vm_get_uuid(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static List<vm_operations> get_allowed_operations(Session session, string _vm)
    {
      return Helper.StringArrayToEnumList<vm_operations>(session.proxy.vm_get_allowed_operations(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static Dictionary<string, vm_operations> get_current_operations(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_vm_operations(session.proxy.vm_get_current_operations(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static vm_power_state get_power_state(Session session, string _vm)
    {
      return (vm_power_state) Helper.EnumParseDefault(typeof (vm_power_state), session.proxy.vm_get_power_state(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static string get_name_label(Session session, string _vm)
    {
      return session.proxy.vm_get_name_label(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_name_description(Session session, string _vm)
    {
      return session.proxy.vm_get_name_description(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static long get_user_version(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_user_version(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static bool get_is_a_template(Session session, string _vm)
    {
      return session.proxy.vm_get_is_a_template(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<VDI> get_suspend_VDI(Session session, string _vm)
    {
      return XenRef<VDI>.Create(session.proxy.vm_get_suspend_vdi(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<Host> get_resident_on(Session session, string _vm)
    {
      return XenRef<Host>.Create(session.proxy.vm_get_resident_on(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<Host> get_affinity(Session session, string _vm)
    {
      return XenRef<Host>.Create(session.proxy.vm_get_affinity(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_memory_overhead(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_memory_overhead(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_memory_target(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_memory_target(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_memory_static_max(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_memory_static_max(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_memory_dynamic_max(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_memory_dynamic_max(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_memory_dynamic_min(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_memory_dynamic_min(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_memory_static_min(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_memory_static_min(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static Dictionary<string, string> get_VCPUs_params(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_get_vcpus_params(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_VCPUs_max(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_vcpus_max(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_VCPUs_at_startup(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_vcpus_at_startup(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static on_normal_exit get_actions_after_shutdown(Session session, string _vm)
    {
      return (on_normal_exit) Helper.EnumParseDefault(typeof (on_normal_exit), session.proxy.vm_get_actions_after_shutdown(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static on_normal_exit get_actions_after_reboot(Session session, string _vm)
    {
      return (on_normal_exit) Helper.EnumParseDefault(typeof (on_normal_exit), session.proxy.vm_get_actions_after_reboot(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static on_crash_behaviour get_actions_after_crash(Session session, string _vm)
    {
      return (on_crash_behaviour) Helper.EnumParseDefault(typeof (on_crash_behaviour), session.proxy.vm_get_actions_after_crash(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<Console>> get_consoles(Session session, string _vm)
    {
      return XenRef<Console>.Create(session.proxy.vm_get_consoles(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<VIF>> get_VIFs(Session session, string _vm)
    {
      return XenRef<VIF>.Create(session.proxy.vm_get_vifs(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<VBD>> get_VBDs(Session session, string _vm)
    {
      return XenRef<VBD>.Create(session.proxy.vm_get_vbds(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<Crashdump>> get_crash_dumps(Session session, string _vm)
    {
      return XenRef<Crashdump>.Create(session.proxy.vm_get_crash_dumps(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<VTPM>> get_VTPMs(Session session, string _vm)
    {
      return XenRef<VTPM>.Create(session.proxy.vm_get_vtpms(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static string get_PV_bootloader(Session session, string _vm)
    {
      return session.proxy.vm_get_pv_bootloader(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_PV_kernel(Session session, string _vm)
    {
      return session.proxy.vm_get_pv_kernel(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_PV_ramdisk(Session session, string _vm)
    {
      return session.proxy.vm_get_pv_ramdisk(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_PV_args(Session session, string _vm)
    {
      return session.proxy.vm_get_pv_args(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_PV_bootloader_args(Session session, string _vm)
    {
      return session.proxy.vm_get_pv_bootloader_args(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_PV_legacy_args(Session session, string _vm)
    {
      return session.proxy.vm_get_pv_legacy_args(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_HVM_boot_policy(Session session, string _vm)
    {
      return session.proxy.vm_get_hvm_boot_policy(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static Dictionary<string, string> get_HVM_boot_params(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_get_hvm_boot_params(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static double get_HVM_shadow_multiplier(Session session, string _vm)
    {
      return Convert.ToDouble(session.proxy.vm_get_hvm_shadow_multiplier(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static Dictionary<string, string> get_platform(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_get_platform(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static string get_PCI_bus(Session session, string _vm)
    {
      return session.proxy.vm_get_pci_bus(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_get_other_config(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_domid(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_domid(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static string get_domarch(Session session, string _vm)
    {
      return session.proxy.vm_get_domarch(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static Dictionary<string, string> get_last_boot_CPU_flags(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_get_last_boot_cpu_flags(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static bool get_is_control_domain(Session session, string _vm)
    {
      return session.proxy.vm_get_is_control_domain(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<VM_metrics> get_metrics(Session session, string _vm)
    {
      return XenRef<VM_metrics>.Create(session.proxy.vm_get_metrics(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<VM_guest_metrics> get_guest_metrics(Session session, string _vm)
    {
      return XenRef<VM_guest_metrics>.Create(session.proxy.vm_get_guest_metrics(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static string get_last_booted_record(Session session, string _vm)
    {
      return session.proxy.vm_get_last_booted_record(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_recommendations(Session session, string _vm)
    {
      return session.proxy.vm_get_recommendations(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static Dictionary<string, string> get_xenstore_data(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_get_xenstore_data(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static bool get_ha_always_run(Session session, string _vm)
    {
      return session.proxy.vm_get_ha_always_run(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_ha_restart_priority(Session session, string _vm)
    {
      return session.proxy.vm_get_ha_restart_priority(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static bool get_is_a_snapshot(Session session, string _vm)
    {
      return session.proxy.vm_get_is_a_snapshot(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<VM> get_snapshot_of(Session session, string _vm)
    {
      return XenRef<VM>.Create(session.proxy.vm_get_snapshot_of(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<VM>> get_snapshots(Session session, string _vm)
    {
      return XenRef<VM>.Create(session.proxy.vm_get_snapshots(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static DateTime get_snapshot_time(Session session, string _vm)
    {
      return session.proxy.vm_get_snapshot_time(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string get_transportable_snapshot_id(Session session, string _vm)
    {
      return session.proxy.vm_get_transportable_snapshot_id(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static Dictionary<string, XenRef<Blob>> get_blobs(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_XenRefBlob(session.proxy.vm_get_blobs(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static string[] get_tags(Session session, string _vm)
    {
      return session.proxy.vm_get_tags(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static Dictionary<vm_operations, string> get_blocked_operations(Session session, string _vm)
    {
      return Maps.convert_from_proxy_vm_operations_string(session.proxy.vm_get_blocked_operations(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static Dictionary<string, string> get_snapshot_info(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_get_snapshot_info(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static string get_snapshot_metadata(Session session, string _vm)
    {
      return session.proxy.vm_get_snapshot_metadata(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<VM> get_parent(Session session, string _vm)
    {
      return XenRef<VM>.Create(session.proxy.vm_get_parent(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<VM>> get_children(Session session, string _vm)
    {
      return XenRef<VM>.Create(session.proxy.vm_get_children(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static Dictionary<string, string> get_bios_strings(Session session, string _vm)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_get_bios_strings(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<VMPP> get_protection_policy(Session session, string _vm)
    {
      return XenRef<VMPP>.Create(session.proxy.vm_get_protection_policy(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static bool get_is_snapshot_from_vmpp(Session session, string _vm)
    {
      return session.proxy.vm_get_is_snapshot_from_vmpp(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<VM_appliance> get_appliance(Session session, string _vm)
    {
      return XenRef<VM_appliance>.Create(session.proxy.vm_get_appliance(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_start_delay(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_start_delay(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_shutdown_delay(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_shutdown_delay(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_order(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_order(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<VGPU>> get_VGPUs(Session session, string _vm)
    {
      return XenRef<VGPU>.Create(session.proxy.vm_get_vgpus(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static List<XenRef<PCI>> get_attached_PCIs(Session session, string _vm)
    {
      return XenRef<PCI>.Create(session.proxy.vm_get_attached_pcis(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<SR> get_suspend_SR(Session session, string _vm)
    {
      return XenRef<SR>.Create(session.proxy.vm_get_suspend_sr(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static long get_version(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_get_version(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void set_name_label(Session session, string _vm, string _label)
    {
      session.proxy.vm_set_name_label(session.uuid, _vm != null ? _vm : "", _label != null ? _label : "").parse();
    }

    public static void set_name_description(Session session, string _vm, string _description)
    {
      session.proxy.vm_set_name_description(session.uuid, _vm != null ? _vm : "", _description != null ? _description : "").parse();
    }

    public static void set_user_version(Session session, string _vm, long _user_version)
    {
      session.proxy.vm_set_user_version(session.uuid, _vm != null ? _vm : "", _user_version.ToString()).parse();
    }

    public static void set_is_a_template(Session session, string _vm, bool _is_a_template)
    {
      session.proxy.vm_set_is_a_template(session.uuid, _vm != null ? _vm : "", _is_a_template).parse();
    }

    public static void set_affinity(Session session, string _vm, string _affinity)
    {
      session.proxy.vm_set_affinity(session.uuid, _vm != null ? _vm : "", _affinity != null ? _affinity : "").parse();
    }

    public static void set_VCPUs_params(Session session, string _vm, Dictionary<string, string> _params)
    {
      session.proxy.vm_set_vcpus_params(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_params)).parse();
    }

    public static void add_to_VCPUs_params(Session session, string _vm, string _key, string _value)
    {
      session.proxy.vm_add_to_vcpus_params(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_VCPUs_params(Session session, string _vm, string _key)
    {
      session.proxy.vm_remove_from_vcpus_params(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "").parse();
    }

    public static void set_actions_after_shutdown(Session session, string _vm, on_normal_exit _after_shutdown)
    {
      session.proxy.vm_set_actions_after_shutdown(session.uuid, _vm != null ? _vm : "", on_normal_exit_helper.ToString(_after_shutdown)).parse();
    }

    public static void set_actions_after_reboot(Session session, string _vm, on_normal_exit _after_reboot)
    {
      session.proxy.vm_set_actions_after_reboot(session.uuid, _vm != null ? _vm : "", on_normal_exit_helper.ToString(_after_reboot)).parse();
    }

    public static void set_actions_after_crash(Session session, string _vm, on_crash_behaviour _after_crash)
    {
      session.proxy.vm_set_actions_after_crash(session.uuid, _vm != null ? _vm : "", on_crash_behaviour_helper.ToString(_after_crash)).parse();
    }

    public static void set_PV_bootloader(Session session, string _vm, string _bootloader)
    {
      session.proxy.vm_set_pv_bootloader(session.uuid, _vm != null ? _vm : "", _bootloader != null ? _bootloader : "").parse();
    }

    public static void set_PV_kernel(Session session, string _vm, string _kernel)
    {
      session.proxy.vm_set_pv_kernel(session.uuid, _vm != null ? _vm : "", _kernel != null ? _kernel : "").parse();
    }

    public static void set_PV_ramdisk(Session session, string _vm, string _ramdisk)
    {
      session.proxy.vm_set_pv_ramdisk(session.uuid, _vm != null ? _vm : "", _ramdisk != null ? _ramdisk : "").parse();
    }

    public static void set_PV_args(Session session, string _vm, string _args)
    {
      session.proxy.vm_set_pv_args(session.uuid, _vm != null ? _vm : "", _args != null ? _args : "").parse();
    }

    public static void set_PV_bootloader_args(Session session, string _vm, string _bootloader_args)
    {
      session.proxy.vm_set_pv_bootloader_args(session.uuid, _vm != null ? _vm : "", _bootloader_args != null ? _bootloader_args : "").parse();
    }

    public static void set_PV_legacy_args(Session session, string _vm, string _legacy_args)
    {
      session.proxy.vm_set_pv_legacy_args(session.uuid, _vm != null ? _vm : "", _legacy_args != null ? _legacy_args : "").parse();
    }

    public static void set_HVM_boot_policy(Session session, string _vm, string _boot_policy)
    {
      session.proxy.vm_set_hvm_boot_policy(session.uuid, _vm != null ? _vm : "", _boot_policy != null ? _boot_policy : "").parse();
    }

    public static void set_HVM_boot_params(Session session, string _vm, Dictionary<string, string> _boot_params)
    {
      session.proxy.vm_set_hvm_boot_params(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_boot_params)).parse();
    }

    public static void add_to_HVM_boot_params(Session session, string _vm, string _key, string _value)
    {
      session.proxy.vm_add_to_hvm_boot_params(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_HVM_boot_params(Session session, string _vm, string _key)
    {
      session.proxy.vm_remove_from_hvm_boot_params(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "").parse();
    }

    public static void set_platform(Session session, string _vm, Dictionary<string, string> _platform)
    {
      session.proxy.vm_set_platform(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_platform)).parse();
    }

    public static void add_to_platform(Session session, string _vm, string _key, string _value)
    {
      session.proxy.vm_add_to_platform(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_platform(Session session, string _vm, string _key)
    {
      session.proxy.vm_remove_from_platform(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "").parse();
    }

    public static void set_PCI_bus(Session session, string _vm, string _pci_bus)
    {
      session.proxy.vm_set_pci_bus(session.uuid, _vm != null ? _vm : "", _pci_bus != null ? _pci_bus : "").parse();
    }

    public static void set_other_config(Session session, string _vm, Dictionary<string, string> _other_config)
    {
      session.proxy.vm_set_other_config(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _vm, string _key, string _value)
    {
      session.proxy.vm_add_to_other_config(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _vm, string _key)
    {
      session.proxy.vm_remove_from_other_config(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "").parse();
    }

    public static void set_recommendations(Session session, string _vm, string _recommendations)
    {
      session.proxy.vm_set_recommendations(session.uuid, _vm != null ? _vm : "", _recommendations != null ? _recommendations : "").parse();
    }

    public static void set_xenstore_data(Session session, string _vm, Dictionary<string, string> _xenstore_data)
    {
      session.proxy.vm_set_xenstore_data(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_xenstore_data)).parse();
    }

    public static void add_to_xenstore_data(Session session, string _vm, string _key, string _value)
    {
      session.proxy.vm_add_to_xenstore_data(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_xenstore_data(Session session, string _vm, string _key)
    {
      session.proxy.vm_remove_from_xenstore_data(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "").parse();
    }

    public static void set_tags(Session session, string _vm, string[] _tags)
    {
      session.proxy.vm_set_tags(session.uuid, _vm != null ? _vm : "", _tags).parse();
    }

    public static void add_tags(Session session, string _vm, string _value)
    {
      session.proxy.vm_add_tags(session.uuid, _vm != null ? _vm : "", _value != null ? _value : "").parse();
    }

    public static void remove_tags(Session session, string _vm, string _value)
    {
      session.proxy.vm_remove_tags(session.uuid, _vm != null ? _vm : "", _value != null ? _value : "").parse();
    }

    public static void set_blocked_operations(Session session, string _vm, Dictionary<vm_operations, string> _blocked_operations)
    {
      session.proxy.vm_set_blocked_operations(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_vm_operations_string(_blocked_operations)).parse();
    }

    public static void add_to_blocked_operations(Session session, string _vm, vm_operations _key, string _value)
    {
      session.proxy.vm_add_to_blocked_operations(session.uuid, _vm != null ? _vm : "", vm_operations_helper.ToString(_key), _value != null ? _value : "").parse();
    }

    public static void remove_from_blocked_operations(Session session, string _vm, vm_operations _key)
    {
      session.proxy.vm_remove_from_blocked_operations(session.uuid, _vm != null ? _vm : "", vm_operations_helper.ToString(_key)).parse();
    }

    public static void set_suspend_SR(Session session, string _vm, string _suspend_sr)
    {
      session.proxy.vm_set_suspend_sr(session.uuid, _vm != null ? _vm : "", _suspend_sr != null ? _suspend_sr : "").parse();
    }

    public static XenRef<VM> snapshot(Session session, string _vm, string _new_name)
    {
      return XenRef<VM>.Create(session.proxy.vm_snapshot(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "").parse());
    }

    public static XenRef<Task> async_snapshot(Session session, string _vm, string _new_name)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_snapshot(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "").parse());
    }

    public static XenRef<VM> snapshot_with_quiesce(Session session, string _vm, string _new_name)
    {
      return XenRef<VM>.Create(session.proxy.vm_snapshot_with_quiesce(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "").parse());
    }

    public static XenRef<Task> async_snapshot_with_quiesce(Session session, string _vm, string _new_name)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_snapshot_with_quiesce(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "").parse());
    }

    public static XenRef<VM> clone(Session session, string _vm, string _new_name)
    {
      return XenRef<VM>.Create(session.proxy.vm_clone(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "").parse());
    }

    public static XenRef<Task> async_clone(Session session, string _vm, string _new_name)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_clone(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "").parse());
    }

    public static XenRef<VM> copy(Session session, string _vm, string _new_name, string _sr)
    {
      return XenRef<VM>.Create(session.proxy.vm_copy(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "", _sr != null ? _sr : "").parse());
    }

    public static XenRef<Task> async_copy(Session session, string _vm, string _new_name, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_copy(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "", _sr != null ? _sr : "").parse());
    }

    public static void revert(Session session, string _snapshot)
    {
      session.proxy.vm_revert(session.uuid, _snapshot != null ? _snapshot : "").parse();
    }

    public static XenRef<Task> async_revert(Session session, string _snapshot)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_revert(session.uuid, _snapshot != null ? _snapshot : "").parse());
    }

    public static XenRef<VM> checkpoint(Session session, string _vm, string _new_name)
    {
      return XenRef<VM>.Create(session.proxy.vm_checkpoint(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "").parse());
    }

    public static XenRef<Task> async_checkpoint(Session session, string _vm, string _new_name)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_checkpoint(session.uuid, _vm != null ? _vm : "", _new_name != null ? _new_name : "").parse());
    }

    public static void provision(Session session, string _vm)
    {
      session.proxy.vm_provision(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_provision(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_provision(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void start(Session session, string _vm, bool _start_paused, bool _force)
    {
      session.proxy.vm_start(session.uuid, _vm != null ? _vm : "", _start_paused, _force).parse();
    }

    public static XenRef<Task> async_start(Session session, string _vm, bool _start_paused, bool _force)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_start(session.uuid, _vm != null ? _vm : "", _start_paused, _force).parse());
    }

    public static void start_on(Session session, string _vm, string _host, bool _start_paused, bool _force)
    {
      session.proxy.vm_start_on(session.uuid, _vm != null ? _vm : "", _host != null ? _host : "", _start_paused, _force).parse();
    }

    public static XenRef<Task> async_start_on(Session session, string _vm, string _host, bool _start_paused, bool _force)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_start_on(session.uuid, _vm != null ? _vm : "", _host != null ? _host : "", _start_paused, _force).parse());
    }

    public static void pause(Session session, string _vm)
    {
      session.proxy.vm_pause(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_pause(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_pause(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void unpause(Session session, string _vm)
    {
      session.proxy.vm_unpause(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_unpause(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_unpause(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void clean_shutdown(Session session, string _vm)
    {
      session.proxy.vm_clean_shutdown(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_clean_shutdown(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_clean_shutdown(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void clean_reboot(Session session, string _vm)
    {
      session.proxy.vm_clean_reboot(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_clean_reboot(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_clean_reboot(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void hard_shutdown(Session session, string _vm)
    {
      session.proxy.vm_hard_shutdown(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_hard_shutdown(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_hard_shutdown(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void power_state_reset(Session session, string _vm)
    {
      session.proxy.vm_power_state_reset(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_power_state_reset(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_power_state_reset(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void hard_reboot(Session session, string _vm)
    {
      session.proxy.vm_hard_reboot(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_hard_reboot(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_hard_reboot(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void suspend(Session session, string _vm)
    {
      session.proxy.vm_suspend(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static XenRef<Task> async_suspend(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_suspend(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void resume(Session session, string _vm, bool _start_paused, bool _force)
    {
      session.proxy.vm_resume(session.uuid, _vm != null ? _vm : "", _start_paused, _force).parse();
    }

    public static XenRef<Task> async_resume(Session session, string _vm, bool _start_paused, bool _force)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_resume(session.uuid, _vm != null ? _vm : "", _start_paused, _force).parse());
    }

    public static void resume_on(Session session, string _vm, string _host, bool _start_paused, bool _force)
    {
      session.proxy.vm_resume_on(session.uuid, _vm != null ? _vm : "", _host != null ? _host : "", _start_paused, _force).parse();
    }

    public static XenRef<Task> async_resume_on(Session session, string _vm, string _host, bool _start_paused, bool _force)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_resume_on(session.uuid, _vm != null ? _vm : "", _host != null ? _host : "", _start_paused, _force).parse());
    }

    public static void pool_migrate(Session session, string _vm, string _host, Dictionary<string, string> _options)
    {
      session.proxy.vm_pool_migrate(session.uuid, _vm != null ? _vm : "", _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_options)).parse();
    }

    public static XenRef<Task> async_pool_migrate(Session session, string _vm, string _host, Dictionary<string, string> _options)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_pool_migrate(session.uuid, _vm != null ? _vm : "", _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_options)).parse());
    }

    public static void set_VCPUs_number_live(Session session, string _self, long _nvcpu)
    {
      session.proxy.vm_set_vcpus_number_live(session.uuid, _self != null ? _self : "", _nvcpu.ToString()).parse();
    }

    public static XenRef<Task> async_set_VCPUs_number_live(Session session, string _self, long _nvcpu)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_vcpus_number_live(session.uuid, _self != null ? _self : "", _nvcpu.ToString()).parse());
    }

    public static void add_to_VCPUs_params_live(Session session, string _self, string _key, string _value)
    {
      session.proxy.vm_add_to_vcpus_params_live(session.uuid, _self != null ? _self : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static XenRef<Task> async_add_to_VCPUs_params_live(Session session, string _self, string _key, string _value)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_add_to_vcpus_params_live(session.uuid, _self != null ? _self : "", _key != null ? _key : "", _value != null ? _value : "").parse());
    }

    public static void set_ha_restart_priority(Session session, string _self, string _value)
    {
      session.proxy.vm_set_ha_restart_priority(session.uuid, _self != null ? _self : "", _value != null ? _value : "").parse();
    }

    public static void set_ha_always_run(Session session, string _self, bool _value)
    {
      session.proxy.vm_set_ha_always_run(session.uuid, _self != null ? _self : "", _value).parse();
    }

    public static long compute_memory_overhead(Session session, string _vm)
    {
      return long.Parse(session.proxy.vm_compute_memory_overhead(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<Task> async_compute_memory_overhead(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_compute_memory_overhead(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void set_memory_dynamic_max(Session session, string _self, long _value)
    {
      session.proxy.vm_set_memory_dynamic_max(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void set_memory_dynamic_min(Session session, string _self, long _value)
    {
      session.proxy.vm_set_memory_dynamic_min(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void set_memory_dynamic_range(Session session, string _self, long _min, long _max)
    {
      session.proxy.vm_set_memory_dynamic_range(session.uuid, _self != null ? _self : "", _min.ToString(), _max.ToString()).parse();
    }

    public static XenRef<Task> async_set_memory_dynamic_range(Session session, string _self, long _min, long _max)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_memory_dynamic_range(session.uuid, _self != null ? _self : "", _min.ToString(), _max.ToString()).parse());
    }

    public static void set_memory_static_max(Session session, string _self, long _value)
    {
      session.proxy.vm_set_memory_static_max(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void set_memory_static_min(Session session, string _self, long _value)
    {
      session.proxy.vm_set_memory_static_min(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void set_memory_static_range(Session session, string _self, long _min, long _max)
    {
      session.proxy.vm_set_memory_static_range(session.uuid, _self != null ? _self : "", _min.ToString(), _max.ToString()).parse();
    }

    public static XenRef<Task> async_set_memory_static_range(Session session, string _self, long _min, long _max)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_memory_static_range(session.uuid, _self != null ? _self : "", _min.ToString(), _max.ToString()).parse());
    }

    public static void set_memory_limits(Session session, string _self, long _static_min, long _static_max, long _dynamic_min, long _dynamic_max)
    {
      session.proxy.vm_set_memory_limits(session.uuid, _self != null ? _self : "", _static_min.ToString(), _static_max.ToString(), _dynamic_min.ToString(), _dynamic_max.ToString()).parse();
    }

    public static XenRef<Task> async_set_memory_limits(Session session, string _self, long _static_min, long _static_max, long _dynamic_min, long _dynamic_max)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_memory_limits(session.uuid, _self != null ? _self : "", _static_min.ToString(), _static_max.ToString(), _dynamic_min.ToString(), _dynamic_max.ToString()).parse());
    }

    public static void set_memory_target_live(Session session, string _self, long _target)
    {
      session.proxy.vm_set_memory_target_live(session.uuid, _self != null ? _self : "", _target.ToString()).parse();
    }

    public static XenRef<Task> async_set_memory_target_live(Session session, string _self, long _target)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_memory_target_live(session.uuid, _self != null ? _self : "", _target.ToString()).parse());
    }

    public static void wait_memory_target_live(Session session, string _self)
    {
      session.proxy.vm_wait_memory_target_live(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_wait_memory_target_live(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_wait_memory_target_live(session.uuid, _self != null ? _self : "").parse());
    }

    public static void set_HVM_shadow_multiplier(Session session, string _self, double _value)
    {
      session.proxy.vm_set_hvm_shadow_multiplier(session.uuid, _self != null ? _self : "", _value).parse();
    }

    public static void set_shadow_multiplier_live(Session session, string _self, double _multiplier)
    {
      session.proxy.vm_set_shadow_multiplier_live(session.uuid, _self != null ? _self : "", _multiplier).parse();
    }

    public static XenRef<Task> async_set_shadow_multiplier_live(Session session, string _self, double _multiplier)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_shadow_multiplier_live(session.uuid, _self != null ? _self : "", _multiplier).parse());
    }

    public static void set_VCPUs_max(Session session, string _self, long _value)
    {
      session.proxy.vm_set_vcpus_max(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void set_VCPUs_at_startup(Session session, string _self, long _value)
    {
      session.proxy.vm_set_vcpus_at_startup(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void send_sysrq(Session session, string _vm, string _key)
    {
      session.proxy.vm_send_sysrq(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "").parse();
    }

    public static XenRef<Task> async_send_sysrq(Session session, string _vm, string _key)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_send_sysrq(session.uuid, _vm != null ? _vm : "", _key != null ? _key : "").parse());
    }

    public static void send_trigger(Session session, string _vm, string _trigger)
    {
      session.proxy.vm_send_trigger(session.uuid, _vm != null ? _vm : "", _trigger != null ? _trigger : "").parse();
    }

    public static XenRef<Task> async_send_trigger(Session session, string _vm, string _trigger)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_send_trigger(session.uuid, _vm != null ? _vm : "", _trigger != null ? _trigger : "").parse());
    }

    public static long maximise_memory(Session session, string _self, long _total, bool _approximate)
    {
      return long.Parse(session.proxy.vm_maximise_memory(session.uuid, _self != null ? _self : "", _total.ToString(), _approximate).parse());
    }

    public static XenRef<Task> async_maximise_memory(Session session, string _self, long _total, bool _approximate)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_maximise_memory(session.uuid, _self != null ? _self : "", _total.ToString(), _approximate).parse());
    }

    public static void migrate_send(Session session, string _vm, Dictionary<string, string> _dest, bool _live, Dictionary<XenRef<VDI>, XenRef<SR>> _vdi_map, Dictionary<XenRef<VIF>, XenRef<Network>> _vif_map, Dictionary<string, string> _options)
    {
      session.proxy.vm_migrate_send(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_dest), _live, (object) Maps.convert_to_proxy_XenRefVDI_XenRefSR(_vdi_map), (object) Maps.convert_to_proxy_XenRefVIF_XenRefNetwork(_vif_map), (object) Maps.convert_to_proxy_string_string(_options)).parse();
    }

    public static XenRef<Task> async_migrate_send(Session session, string _vm, Dictionary<string, string> _dest, bool _live, Dictionary<XenRef<VDI>, XenRef<SR>> _vdi_map, Dictionary<XenRef<VIF>, XenRef<Network>> _vif_map, Dictionary<string, string> _options)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_migrate_send(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_dest), _live, (object) Maps.convert_to_proxy_XenRefVDI_XenRefSR(_vdi_map), (object) Maps.convert_to_proxy_XenRefVIF_XenRefNetwork(_vif_map), (object) Maps.convert_to_proxy_string_string(_options)).parse());
    }

    public static void assert_can_migrate(Session session, string _vm, Dictionary<string, string> _dest, bool _live, Dictionary<XenRef<VDI>, XenRef<SR>> _vdi_map, Dictionary<XenRef<VIF>, XenRef<Network>> _vif_map, Dictionary<string, string> _options)
    {
      session.proxy.vm_assert_can_migrate(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_dest), _live, (object) Maps.convert_to_proxy_XenRefVDI_XenRefSR(_vdi_map), (object) Maps.convert_to_proxy_XenRefVIF_XenRefNetwork(_vif_map), (object) Maps.convert_to_proxy_string_string(_options)).parse();
    }

    public static XenRef<Task> async_assert_can_migrate(Session session, string _vm, Dictionary<string, string> _dest, bool _live, Dictionary<XenRef<VDI>, XenRef<SR>> _vdi_map, Dictionary<XenRef<VIF>, XenRef<Network>> _vif_map, Dictionary<string, string> _options)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_assert_can_migrate(session.uuid, _vm != null ? _vm : "", (object) Maps.convert_to_proxy_string_string(_dest), _live, (object) Maps.convert_to_proxy_XenRefVDI_XenRefSR(_vdi_map), (object) Maps.convert_to_proxy_XenRefVIF_XenRefNetwork(_vif_map), (object) Maps.convert_to_proxy_string_string(_options)).parse());
    }

    public static VM get_boot_record(Session session, string _self)
    {
      return new VM(session.proxy.vm_get_boot_record(session.uuid, _self != null ? _self : "").parse());
    }

    public static List<Data_source> get_data_sources(Session session, string _self)
    {
      return Helper.Proxy_Data_sourceArrayToData_sourceList(session.proxy.vm_get_data_sources(session.uuid, _self != null ? _self : "").parse());
    }

    public static void record_data_source(Session session, string _self, string _data_source)
    {
      session.proxy.vm_record_data_source(session.uuid, _self != null ? _self : "", _data_source != null ? _data_source : "").parse();
    }

    public static double query_data_source(Session session, string _self, string _data_source)
    {
      return Convert.ToDouble(session.proxy.vm_query_data_source(session.uuid, _self != null ? _self : "", _data_source != null ? _data_source : "").parse());
    }

    public static void forget_data_source_archives(Session session, string _self, string _data_source)
    {
      session.proxy.vm_forget_data_source_archives(session.uuid, _self != null ? _self : "", _data_source != null ? _data_source : "").parse();
    }

    public static void assert_operation_valid(Session session, string _self, vm_operations _op)
    {
      session.proxy.vm_assert_operation_valid(session.uuid, _self != null ? _self : "", vm_operations_helper.ToString(_op)).parse();
    }

    public static XenRef<Task> async_assert_operation_valid(Session session, string _self, vm_operations _op)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_assert_operation_valid(session.uuid, _self != null ? _self : "", vm_operations_helper.ToString(_op)).parse());
    }

    public static void update_allowed_operations(Session session, string _self)
    {
      session.proxy.vm_update_allowed_operations(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_update_allowed_operations(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_update_allowed_operations(session.uuid, _self != null ? _self : "").parse());
    }

    public static string[] get_allowed_VBD_devices(Session session, string _vm)
    {
      return session.proxy.vm_get_allowed_vbd_devices(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static string[] get_allowed_VIF_devices(Session session, string _vm)
    {
      return session.proxy.vm_get_allowed_vif_devices(session.uuid, _vm != null ? _vm : "").parse();
    }

    public static List<XenRef<Host>> get_possible_hosts(Session session, string _vm)
    {
      return XenRef<Host>.Create(session.proxy.vm_get_possible_hosts(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<Task> async_get_possible_hosts(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_get_possible_hosts(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void assert_can_boot_here(Session session, string _self, string _host)
    {
      session.proxy.vm_assert_can_boot_here(session.uuid, _self != null ? _self : "", _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_assert_can_boot_here(Session session, string _self, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_assert_can_boot_here(session.uuid, _self != null ? _self : "", _host != null ? _host : "").parse());
    }

    public static XenRef<Blob> create_new_blob(Session session, string _vm, string _name, string _mime_type, bool _public)
    {
      return XenRef<Blob>.Create(session.proxy.vm_create_new_blob(session.uuid, _vm != null ? _vm : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _vm, string _name, string _mime_type, bool _public)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_create_new_blob(session.uuid, _vm != null ? _vm : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static void assert_agile(Session session, string _self)
    {
      session.proxy.vm_assert_agile(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_assert_agile(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_assert_agile(session.uuid, _self != null ? _self : "").parse());
    }

    public static Dictionary<XenRef<Host>, string[]> retrieve_wlb_recommendations(Session session, string _vm)
    {
      return Maps.convert_from_proxy_XenRefHost_string_array(session.proxy.vm_retrieve_wlb_recommendations(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static XenRef<Task> async_retrieve_wlb_recommendations(Session session, string _vm)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_retrieve_wlb_recommendations(session.uuid, _vm != null ? _vm : "").parse());
    }

    public static void copy_bios_strings(Session session, string _vm, string _host)
    {
      session.proxy.vm_copy_bios_strings(session.uuid, _vm != null ? _vm : "", _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_copy_bios_strings(Session session, string _vm, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_copy_bios_strings(session.uuid, _vm != null ? _vm : "", _host != null ? _host : "").parse());
    }

    public static void set_protection_policy(Session session, string _self, string _value)
    {
      session.proxy.vm_set_protection_policy(session.uuid, _self != null ? _self : "", _value != null ? _value : "").parse();
    }

    public static void set_start_delay(Session session, string _self, long _value)
    {
      session.proxy.vm_set_start_delay(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static XenRef<Task> async_set_start_delay(Session session, string _self, long _value)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_start_delay(session.uuid, _self != null ? _self : "", _value.ToString()).parse());
    }

    public static void set_shutdown_delay(Session session, string _self, long _value)
    {
      session.proxy.vm_set_shutdown_delay(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static XenRef<Task> async_set_shutdown_delay(Session session, string _self, long _value)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_shutdown_delay(session.uuid, _self != null ? _self : "", _value.ToString()).parse());
    }

    public static void set_order(Session session, string _self, long _value)
    {
      session.proxy.vm_set_order(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static XenRef<Task> async_set_order(Session session, string _self, long _value)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_order(session.uuid, _self != null ? _self : "", _value.ToString()).parse());
    }

    public static void set_suspend_VDI(Session session, string _self, string _value)
    {
      session.proxy.vm_set_suspend_vdi(session.uuid, _self != null ? _self : "", _value != null ? _value : "").parse();
    }

    public static XenRef<Task> async_set_suspend_VDI(Session session, string _self, string _value)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_suspend_vdi(session.uuid, _self != null ? _self : "", _value != null ? _value : "").parse());
    }

    public static void assert_can_be_recovered(Session session, string _self, string _session_to)
    {
      session.proxy.vm_assert_can_be_recovered(session.uuid, _self != null ? _self : "", _session_to != null ? _session_to : "").parse();
    }

    public static XenRef<Task> async_assert_can_be_recovered(Session session, string _self, string _session_to)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_assert_can_be_recovered(session.uuid, _self != null ? _self : "", _session_to != null ? _session_to : "").parse());
    }

    public static void recover(Session session, string _self, string _session_to, bool _force)
    {
      session.proxy.vm_recover(session.uuid, _self != null ? _self : "", _session_to != null ? _session_to : "", _force).parse();
    }

    public static XenRef<Task> async_recover(Session session, string _self, string _session_to, bool _force)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_recover(session.uuid, _self != null ? _self : "", _session_to != null ? _session_to : "", _force).parse());
    }

    public static void import_convert(Session session, string _type, string _username, string _password, string _sr, Dictionary<string, string> _remote_config)
    {
      session.proxy.vm_import_convert(session.uuid, _type != null ? _type : "", _username != null ? _username : "", _password != null ? _password : "", _sr != null ? _sr : "", (object) Maps.convert_to_proxy_string_string(_remote_config)).parse();
    }

    public static XenRef<Task> async_import_convert(Session session, string _type, string _username, string _password, string _sr, Dictionary<string, string> _remote_config)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_import_convert(session.uuid, _type != null ? _type : "", _username != null ? _username : "", _password != null ? _password : "", _sr != null ? _sr : "", (object) Maps.convert_to_proxy_string_string(_remote_config)).parse());
    }

    public static void set_appliance(Session session, string _self, string _value)
    {
      session.proxy.vm_set_appliance(session.uuid, _self != null ? _self : "", _value != null ? _value : "").parse();
    }

    public static XenRef<Task> async_set_appliance(Session session, string _self, string _value)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_set_appliance(session.uuid, _self != null ? _self : "", _value != null ? _value : "").parse());
    }

    public static Dictionary<string, string> query_services(Session session, string _self)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_query_services(session.uuid, _self != null ? _self : "").parse());
    }

    public static XenRef<Task> async_query_services(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_query_services(session.uuid, _self != null ? _self : "").parse());
    }

    public static List<XenRef<VM>> get_all(Session session)
    {
      return XenRef<VM>.Create(session.proxy.vm_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<VM>, VM> get_all_records(Session session)
    {
      return XenRef<VM>.Create<Proxy_VM>(session.proxy.vm_get_all_records(session.uuid).parse());
    }

    public static XenRef<Blob> create_new_blob(Session session, string _vm, string _name, string _mime_type)
    {
      return XenRef<Blob>.Create(session.proxy.vm_create_new_blob(session.uuid, _vm != null ? _vm : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _vm, string _name, string _mime_type)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_create_new_blob(session.uuid, _vm != null ? _vm : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }
  }
}
