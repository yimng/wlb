// Decompiled with JetBrains decompiler
// Type: XenAPI.Host
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Host : XenObject<Host>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private long _memory_overhead;
    private List<host_allowed_operations> _allowed_operations;
    private Dictionary<string, host_allowed_operations> _current_operations;
    private long _API_version_major;
    private long _API_version_minor;
    private string _API_version_vendor;
    private Dictionary<string, string> _API_version_vendor_implementation;
    private bool _enabled;
    private Dictionary<string, string> _software_version;
    private Dictionary<string, string> _other_config;
    private string[] _capabilities;
    private Dictionary<string, string> _cpu_configuration;
    private string _sched_policy;
    private string[] _supported_bootloaders;
    private List<XenRef<VM>> _resident_VMs;
    private Dictionary<string, string> _logging;
    private List<XenRef<PIF>> _PIFs;
    private XenRef<SR> _suspend_image_sr;
    private XenRef<SR> _crash_dump_sr;
    private List<XenRef<Host_crashdump>> _crashdumps;
    private List<XenRef<Host_patch>> _patches;
    private List<XenRef<PBD>> _PBDs;
    private List<XenRef<Host_cpu>> _host_CPUs;
    private Dictionary<string, string> _cpu_info;
    private string _hostname;
    private string _address;
    private XenRef<Host_metrics> _metrics;
    private Dictionary<string, string> _license_params;
    private string[] _ha_statefiles;
    private string[] _ha_network_peers;
    private Dictionary<string, XenRef<Blob>> _blobs;
    private string[] _tags;
    private string _external_auth_type;
    private string _external_auth_service_name;
    private Dictionary<string, string> _external_auth_configuration;
    private string _edition;
    private Dictionary<string, string> _license_server;
    private Dictionary<string, string> _bios_strings;
    private string _power_on_mode;
    private Dictionary<string, string> _power_on_config;
    private XenRef<SR> _local_cache_sr;
    private Dictionary<string, string> _chipset_info;
    private List<XenRef<PCI>> _PCIs;
    private List<XenRef<PGPU>> _PGPUs;

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

    public List<host_allowed_operations> allowed_operations
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

    public Dictionary<string, host_allowed_operations> current_operations
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

    public long API_version_major
    {
      get
      {
        return this._API_version_major;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._API_version_major))
          return;
        this._API_version_major = value;
        this.Changed = true;
        this.NotifyPropertyChanged("API_version_major");
      }
    }

    public long API_version_minor
    {
      get
      {
        return this._API_version_minor;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._API_version_minor))
          return;
        this._API_version_minor = value;
        this.Changed = true;
        this.NotifyPropertyChanged("API_version_minor");
      }
    }

    public string API_version_vendor
    {
      get
      {
        return this._API_version_vendor;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._API_version_vendor))
          return;
        this._API_version_vendor = value;
        this.Changed = true;
        this.NotifyPropertyChanged("API_version_vendor");
      }
    }

    public Dictionary<string, string> API_version_vendor_implementation
    {
      get
      {
        return this._API_version_vendor_implementation;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._API_version_vendor_implementation))
          return;
        this._API_version_vendor_implementation = value;
        this.Changed = true;
        this.NotifyPropertyChanged("API_version_vendor_implementation");
      }
    }

    public bool enabled
    {
      get
      {
        return this._enabled;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._enabled ? 1 : 0)))
          return;
        this._enabled = value;
        this.Changed = true;
        this.NotifyPropertyChanged("enabled");
      }
    }

    public Dictionary<string, string> software_version
    {
      get
      {
        return this._software_version;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._software_version))
          return;
        this._software_version = value;
        this.Changed = true;
        this.NotifyPropertyChanged("software_version");
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

    public string[] capabilities
    {
      get
      {
        return this._capabilities;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._capabilities))
          return;
        this._capabilities = value;
        this.Changed = true;
        this.NotifyPropertyChanged("capabilities");
      }
    }

    public Dictionary<string, string> cpu_configuration
    {
      get
      {
        return this._cpu_configuration;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._cpu_configuration))
          return;
        this._cpu_configuration = value;
        this.Changed = true;
        this.NotifyPropertyChanged("cpu_configuration");
      }
    }

    public string sched_policy
    {
      get
      {
        return this._sched_policy;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._sched_policy))
          return;
        this._sched_policy = value;
        this.Changed = true;
        this.NotifyPropertyChanged("sched_policy");
      }
    }

    public string[] supported_bootloaders
    {
      get
      {
        return this._supported_bootloaders;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._supported_bootloaders))
          return;
        this._supported_bootloaders = value;
        this.Changed = true;
        this.NotifyPropertyChanged("supported_bootloaders");
      }
    }

    public List<XenRef<VM>> resident_VMs
    {
      get
      {
        return this._resident_VMs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._resident_VMs))
          return;
        this._resident_VMs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("resident_VMs");
      }
    }

    public Dictionary<string, string> logging
    {
      get
      {
        return this._logging;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._logging))
          return;
        this._logging = value;
        this.Changed = true;
        this.NotifyPropertyChanged("logging");
      }
    }

    public List<XenRef<PIF>> PIFs
    {
      get
      {
        return this._PIFs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PIFs))
          return;
        this._PIFs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PIFs");
      }
    }

    public XenRef<SR> suspend_image_sr
    {
      get
      {
        return this._suspend_image_sr;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._suspend_image_sr))
          return;
        this._suspend_image_sr = value;
        this.Changed = true;
        this.NotifyPropertyChanged("suspend_image_sr");
      }
    }

    public XenRef<SR> crash_dump_sr
    {
      get
      {
        return this._crash_dump_sr;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._crash_dump_sr))
          return;
        this._crash_dump_sr = value;
        this.Changed = true;
        this.NotifyPropertyChanged("crash_dump_sr");
      }
    }

    public List<XenRef<Host_crashdump>> crashdumps
    {
      get
      {
        return this._crashdumps;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._crashdumps))
          return;
        this._crashdumps = value;
        this.Changed = true;
        this.NotifyPropertyChanged("crashdumps");
      }
    }

    public List<XenRef<Host_patch>> patches
    {
      get
      {
        return this._patches;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._patches))
          return;
        this._patches = value;
        this.Changed = true;
        this.NotifyPropertyChanged("patches");
      }
    }

    public List<XenRef<PBD>> PBDs
    {
      get
      {
        return this._PBDs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PBDs))
          return;
        this._PBDs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PBDs");
      }
    }

    public List<XenRef<Host_cpu>> host_CPUs
    {
      get
      {
        return this._host_CPUs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._host_CPUs))
          return;
        this._host_CPUs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("host_CPUs");
      }
    }

    public Dictionary<string, string> cpu_info
    {
      get
      {
        return this._cpu_info;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._cpu_info))
          return;
        this._cpu_info = value;
        this.Changed = true;
        this.NotifyPropertyChanged("cpu_info");
      }
    }

    public string hostname
    {
      get
      {
        return this._hostname;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._hostname))
          return;
        this._hostname = value;
        this.Changed = true;
        this.NotifyPropertyChanged("hostname");
      }
    }

    public string address
    {
      get
      {
        return this._address;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._address))
          return;
        this._address = value;
        this.Changed = true;
        this.NotifyPropertyChanged("address");
      }
    }

    public XenRef<Host_metrics> metrics
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

    public Dictionary<string, string> license_params
    {
      get
      {
        return this._license_params;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._license_params))
          return;
        this._license_params = value;
        this.Changed = true;
        this.NotifyPropertyChanged("license_params");
      }
    }

    public string[] ha_statefiles
    {
      get
      {
        return this._ha_statefiles;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ha_statefiles))
          return;
        this._ha_statefiles = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_statefiles");
      }
    }

    public string[] ha_network_peers
    {
      get
      {
        return this._ha_network_peers;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ha_network_peers))
          return;
        this._ha_network_peers = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_network_peers");
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

    public string external_auth_type
    {
      get
      {
        return this._external_auth_type;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._external_auth_type))
          return;
        this._external_auth_type = value;
        this.Changed = true;
        this.NotifyPropertyChanged("external_auth_type");
      }
    }

    public string external_auth_service_name
    {
      get
      {
        return this._external_auth_service_name;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._external_auth_service_name))
          return;
        this._external_auth_service_name = value;
        this.Changed = true;
        this.NotifyPropertyChanged("external_auth_service_name");
      }
    }

    public Dictionary<string, string> external_auth_configuration
    {
      get
      {
        return this._external_auth_configuration;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._external_auth_configuration))
          return;
        this._external_auth_configuration = value;
        this.Changed = true;
        this.NotifyPropertyChanged("external_auth_configuration");
      }
    }

    public string edition
    {
      get
      {
        return this._edition;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._edition))
          return;
        this._edition = value;
        this.Changed = true;
        this.NotifyPropertyChanged("edition");
      }
    }

    public Dictionary<string, string> license_server
    {
      get
      {
        return this._license_server;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._license_server))
          return;
        this._license_server = value;
        this.Changed = true;
        this.NotifyPropertyChanged("license_server");
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

    public string power_on_mode
    {
      get
      {
        return this._power_on_mode;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._power_on_mode))
          return;
        this._power_on_mode = value;
        this.Changed = true;
        this.NotifyPropertyChanged("power_on_mode");
      }
    }

    public Dictionary<string, string> power_on_config
    {
      get
      {
        return this._power_on_config;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._power_on_config))
          return;
        this._power_on_config = value;
        this.Changed = true;
        this.NotifyPropertyChanged("power_on_config");
      }
    }

    public XenRef<SR> local_cache_sr
    {
      get
      {
        return this._local_cache_sr;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._local_cache_sr))
          return;
        this._local_cache_sr = value;
        this.Changed = true;
        this.NotifyPropertyChanged("local_cache_sr");
      }
    }

    public Dictionary<string, string> chipset_info
    {
      get
      {
        return this._chipset_info;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._chipset_info))
          return;
        this._chipset_info = value;
        this.Changed = true;
        this.NotifyPropertyChanged("chipset_info");
      }
    }

    public List<XenRef<PCI>> PCIs
    {
      get
      {
        return this._PCIs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PCIs))
          return;
        this._PCIs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PCIs");
      }
    }

    public List<XenRef<PGPU>> PGPUs
    {
      get
      {
        return this._PGPUs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PGPUs))
          return;
        this._PGPUs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PGPUs");
      }
    }

    public Host()
    {
    }

    public Host(string uuid, string name_label, string name_description, long memory_overhead, List<host_allowed_operations> allowed_operations, Dictionary<string, host_allowed_operations> current_operations, long API_version_major, long API_version_minor, string API_version_vendor, Dictionary<string, string> API_version_vendor_implementation, bool enabled, Dictionary<string, string> software_version, Dictionary<string, string> other_config, string[] capabilities, Dictionary<string, string> cpu_configuration, string sched_policy, string[] supported_bootloaders, List<XenRef<VM>> resident_VMs, Dictionary<string, string> logging, List<XenRef<PIF>> PIFs, XenRef<SR> suspend_image_sr, XenRef<SR> crash_dump_sr, List<XenRef<Host_crashdump>> crashdumps, List<XenRef<Host_patch>> patches, List<XenRef<PBD>> PBDs, List<XenRef<Host_cpu>> host_CPUs, Dictionary<string, string> cpu_info, string hostname, string address, XenRef<Host_metrics> metrics, Dictionary<string, string> license_params, string[] ha_statefiles, string[] ha_network_peers, Dictionary<string, XenRef<Blob>> blobs, string[] tags, string external_auth_type, string external_auth_service_name, Dictionary<string, string> external_auth_configuration, string edition, Dictionary<string, string> license_server, Dictionary<string, string> bios_strings, string power_on_mode, Dictionary<string, string> power_on_config, XenRef<SR> local_cache_sr, Dictionary<string, string> chipset_info, List<XenRef<PCI>> PCIs, List<XenRef<PGPU>> PGPUs)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.memory_overhead = memory_overhead;
      this.allowed_operations = allowed_operations;
      this.current_operations = current_operations;
      this.API_version_major = API_version_major;
      this.API_version_minor = API_version_minor;
      this.API_version_vendor = API_version_vendor;
      this.API_version_vendor_implementation = API_version_vendor_implementation;
      this.enabled = enabled;
      this.software_version = software_version;
      this.other_config = other_config;
      this.capabilities = capabilities;
      this.cpu_configuration = cpu_configuration;
      this.sched_policy = sched_policy;
      this.supported_bootloaders = supported_bootloaders;
      this.resident_VMs = resident_VMs;
      this.logging = logging;
      this.PIFs = PIFs;
      this.suspend_image_sr = suspend_image_sr;
      this.crash_dump_sr = crash_dump_sr;
      this.crashdumps = crashdumps;
      this.patches = patches;
      this.PBDs = PBDs;
      this.host_CPUs = host_CPUs;
      this.cpu_info = cpu_info;
      this.hostname = hostname;
      this.address = address;
      this.metrics = metrics;
      this.license_params = license_params;
      this.ha_statefiles = ha_statefiles;
      this.ha_network_peers = ha_network_peers;
      this.blobs = blobs;
      this.tags = tags;
      this.external_auth_type = external_auth_type;
      this.external_auth_service_name = external_auth_service_name;
      this.external_auth_configuration = external_auth_configuration;
      this.edition = edition;
      this.license_server = license_server;
      this.bios_strings = bios_strings;
      this.power_on_mode = power_on_mode;
      this.power_on_config = power_on_config;
      this.local_cache_sr = local_cache_sr;
      this.chipset_info = chipset_info;
      this.PCIs = PCIs;
      this.PGPUs = PGPUs;
    }

    public Host(Proxy_Host proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Host(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.memory_overhead = Marshalling.ParseLong(table, "memory_overhead");
      this.allowed_operations = Helper.StringArrayToEnumList<host_allowed_operations>(Marshalling.ParseStringArray(table, "allowed_operations"));
      this.current_operations = Maps.convert_from_proxy_string_host_allowed_operations((object) Marshalling.ParseHashTable(table, "current_operations"));
      this.API_version_major = Marshalling.ParseLong(table, "API_version_major");
      this.API_version_minor = Marshalling.ParseLong(table, "API_version_minor");
      this.API_version_vendor = Marshalling.ParseString(table, "API_version_vendor");
      this.API_version_vendor_implementation = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "API_version_vendor_implementation"));
      this.enabled = Marshalling.ParseBool(table, "enabled");
      this.software_version = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "software_version"));
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
      this.capabilities = Marshalling.ParseStringArray(table, "capabilities");
      this.cpu_configuration = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "cpu_configuration"));
      this.sched_policy = Marshalling.ParseString(table, "sched_policy");
      this.supported_bootloaders = Marshalling.ParseStringArray(table, "supported_bootloaders");
      this.resident_VMs = Marshalling.ParseSetRef<VM>(table, "resident_VMs");
      this.logging = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "logging"));
      this.PIFs = Marshalling.ParseSetRef<PIF>(table, "PIFs");
      this.suspend_image_sr = Marshalling.ParseRef<SR>(table, "suspend_image_sr");
      this.crash_dump_sr = Marshalling.ParseRef<SR>(table, "crash_dump_sr");
      this.crashdumps = Marshalling.ParseSetRef<Host_crashdump>(table, "crashdumps");
      this.patches = Marshalling.ParseSetRef<Host_patch>(table, "patches");
      this.PBDs = Marshalling.ParseSetRef<PBD>(table, "PBDs");
      this.host_CPUs = Marshalling.ParseSetRef<Host_cpu>(table, "host_CPUs");
      this.cpu_info = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "cpu_info"));
      this.hostname = Marshalling.ParseString(table, "hostname");
      this.address = Marshalling.ParseString(table, "address");
      this.metrics = Marshalling.ParseRef<Host_metrics>(table, "metrics");
      this.license_params = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "license_params"));
      this.ha_statefiles = Marshalling.ParseStringArray(table, "ha_statefiles");
      this.ha_network_peers = Marshalling.ParseStringArray(table, "ha_network_peers");
      this.blobs = Maps.convert_from_proxy_string_XenRefBlob((object) Marshalling.ParseHashTable(table, "blobs"));
      this.tags = Marshalling.ParseStringArray(table, "tags");
      this.external_auth_type = Marshalling.ParseString(table, "external_auth_type");
      this.external_auth_service_name = Marshalling.ParseString(table, "external_auth_service_name");
      this.external_auth_configuration = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "external_auth_configuration"));
      this.edition = Marshalling.ParseString(table, "edition");
      this.license_server = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "license_server"));
      this.bios_strings = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "bios_strings"));
      this.power_on_mode = Marshalling.ParseString(table, "power_on_mode");
      this.power_on_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "power_on_config"));
      this.local_cache_sr = Marshalling.ParseRef<SR>(table, "local_cache_sr");
      this.chipset_info = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "chipset_info"));
      this.PCIs = Marshalling.ParseSetRef<PCI>(table, "PCIs");
      this.PGPUs = Marshalling.ParseSetRef<PGPU>(table, "PGPUs");
    }

    public override void UpdateFrom(Host update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.memory_overhead = update.memory_overhead;
      this.allowed_operations = update.allowed_operations;
      this.current_operations = update.current_operations;
      this.API_version_major = update.API_version_major;
      this.API_version_minor = update.API_version_minor;
      this.API_version_vendor = update.API_version_vendor;
      this.API_version_vendor_implementation = update.API_version_vendor_implementation;
      this.enabled = update.enabled;
      this.software_version = update.software_version;
      this.other_config = update.other_config;
      this.capabilities = update.capabilities;
      this.cpu_configuration = update.cpu_configuration;
      this.sched_policy = update.sched_policy;
      this.supported_bootloaders = update.supported_bootloaders;
      this.resident_VMs = update.resident_VMs;
      this.logging = update.logging;
      this.PIFs = update.PIFs;
      this.suspend_image_sr = update.suspend_image_sr;
      this.crash_dump_sr = update.crash_dump_sr;
      this.crashdumps = update.crashdumps;
      this.patches = update.patches;
      this.PBDs = update.PBDs;
      this.host_CPUs = update.host_CPUs;
      this.cpu_info = update.cpu_info;
      this.hostname = update.hostname;
      this.address = update.address;
      this.metrics = update.metrics;
      this.license_params = update.license_params;
      this.ha_statefiles = update.ha_statefiles;
      this.ha_network_peers = update.ha_network_peers;
      this.blobs = update.blobs;
      this.tags = update.tags;
      this.external_auth_type = update.external_auth_type;
      this.external_auth_service_name = update.external_auth_service_name;
      this.external_auth_configuration = update.external_auth_configuration;
      this.edition = update.edition;
      this.license_server = update.license_server;
      this.bios_strings = update.bios_strings;
      this.power_on_mode = update.power_on_mode;
      this.power_on_config = update.power_on_config;
      this.local_cache_sr = update.local_cache_sr;
      this.chipset_info = update.chipset_info;
      this.PCIs = update.PCIs;
      this.PGPUs = update.PGPUs;
    }

    internal void UpdateFromProxy(Proxy_Host proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.memory_overhead = proxy.memory_overhead == null ? 0L : long.Parse(proxy.memory_overhead);
      this.allowed_operations = proxy.allowed_operations == null ? (List<host_allowed_operations>) null : Helper.StringArrayToEnumList<host_allowed_operations>(proxy.allowed_operations);
      this.current_operations = proxy.current_operations == null ? (Dictionary<string, host_allowed_operations>) null : Maps.convert_from_proxy_string_host_allowed_operations(proxy.current_operations);
      this.API_version_major = proxy.API_version_major == null ? 0L : long.Parse(proxy.API_version_major);
      this.API_version_minor = proxy.API_version_minor == null ? 0L : long.Parse(proxy.API_version_minor);
      this.API_version_vendor = proxy.API_version_vendor == null ? (string) null : proxy.API_version_vendor;
      this.API_version_vendor_implementation = proxy.API_version_vendor_implementation == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.API_version_vendor_implementation);
      this.enabled = proxy.enabled;
      this.software_version = proxy.software_version == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.software_version);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
      this.capabilities = proxy.capabilities == null ? new string[0] : proxy.capabilities;
      this.cpu_configuration = proxy.cpu_configuration == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.cpu_configuration);
      this.sched_policy = proxy.sched_policy == null ? (string) null : proxy.sched_policy;
      this.supported_bootloaders = proxy.supported_bootloaders == null ? new string[0] : proxy.supported_bootloaders;
      this.resident_VMs = proxy.resident_VMs == null ? (List<XenRef<VM>>) null : XenRef<VM>.Create(proxy.resident_VMs);
      this.logging = proxy.logging == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.logging);
      this.PIFs = proxy.PIFs == null ? (List<XenRef<PIF>>) null : XenRef<PIF>.Create(proxy.PIFs);
      this.suspend_image_sr = proxy.suspend_image_sr == null ? (XenRef<SR>) null : XenRef<SR>.Create(proxy.suspend_image_sr);
      this.crash_dump_sr = proxy.crash_dump_sr == null ? (XenRef<SR>) null : XenRef<SR>.Create(proxy.crash_dump_sr);
      this.crashdumps = proxy.crashdumps == null ? (List<XenRef<Host_crashdump>>) null : XenRef<Host_crashdump>.Create(proxy.crashdumps);
      this.patches = proxy.patches == null ? (List<XenRef<Host_patch>>) null : XenRef<Host_patch>.Create(proxy.patches);
      this.PBDs = proxy.PBDs == null ? (List<XenRef<PBD>>) null : XenRef<PBD>.Create(proxy.PBDs);
      this.host_CPUs = proxy.host_CPUs == null ? (List<XenRef<Host_cpu>>) null : XenRef<Host_cpu>.Create(proxy.host_CPUs);
      this.cpu_info = proxy.cpu_info == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.cpu_info);
      this.hostname = proxy.hostname == null ? (string) null : proxy.hostname;
      this.address = proxy.address == null ? (string) null : proxy.address;
      this.metrics = proxy.metrics == null ? (XenRef<Host_metrics>) null : XenRef<Host_metrics>.Create(proxy.metrics);
      this.license_params = proxy.license_params == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.license_params);
      this.ha_statefiles = proxy.ha_statefiles == null ? new string[0] : proxy.ha_statefiles;
      this.ha_network_peers = proxy.ha_network_peers == null ? new string[0] : proxy.ha_network_peers;
      this.blobs = proxy.blobs == null ? (Dictionary<string, XenRef<Blob>>) null : Maps.convert_from_proxy_string_XenRefBlob(proxy.blobs);
      this.tags = proxy.tags == null ? new string[0] : proxy.tags;
      this.external_auth_type = proxy.external_auth_type == null ? (string) null : proxy.external_auth_type;
      this.external_auth_service_name = proxy.external_auth_service_name == null ? (string) null : proxy.external_auth_service_name;
      this.external_auth_configuration = proxy.external_auth_configuration == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.external_auth_configuration);
      this.edition = proxy.edition == null ? (string) null : proxy.edition;
      this.license_server = proxy.license_server == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.license_server);
      this.bios_strings = proxy.bios_strings == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.bios_strings);
      this.power_on_mode = proxy.power_on_mode == null ? (string) null : proxy.power_on_mode;
      this.power_on_config = proxy.power_on_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.power_on_config);
      this.local_cache_sr = proxy.local_cache_sr == null ? (XenRef<SR>) null : XenRef<SR>.Create(proxy.local_cache_sr);
      this.chipset_info = proxy.chipset_info == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.chipset_info);
      this.PCIs = proxy.PCIs == null ? (List<XenRef<PCI>>) null : XenRef<PCI>.Create(proxy.PCIs);
      this.PGPUs = proxy.PGPUs == null ? (List<XenRef<PGPU>>) null : XenRef<PGPU>.Create(proxy.PGPUs);
    }

    public Proxy_Host ToProxy()
    {
      return new Proxy_Host()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        memory_overhead = this.memory_overhead.ToString(),
        allowed_operations = this.allowed_operations != null ? Helper.ObjectListToStringArray<host_allowed_operations>(this.allowed_operations) : new string[0],
        current_operations = (object) Maps.convert_to_proxy_string_host_allowed_operations(this.current_operations),
        API_version_major = this.API_version_major.ToString(),
        API_version_minor = this.API_version_minor.ToString(),
        API_version_vendor = this.API_version_vendor != null ? this.API_version_vendor : "",
        API_version_vendor_implementation = (object) Maps.convert_to_proxy_string_string(this.API_version_vendor_implementation),
        enabled = this.enabled,
        software_version = (object) Maps.convert_to_proxy_string_string(this.software_version),
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config),
        capabilities = this.capabilities,
        cpu_configuration = (object) Maps.convert_to_proxy_string_string(this.cpu_configuration),
        sched_policy = this.sched_policy != null ? this.sched_policy : "",
        supported_bootloaders = this.supported_bootloaders,
        resident_VMs = this.resident_VMs != null ? Helper.RefListToStringArray<VM>(this.resident_VMs) : new string[0],
        logging = (object) Maps.convert_to_proxy_string_string(this.logging),
        PIFs = this.PIFs != null ? Helper.RefListToStringArray<PIF>(this.PIFs) : new string[0],
        suspend_image_sr = this.suspend_image_sr != null ? (string) this.suspend_image_sr : "",
        crash_dump_sr = this.crash_dump_sr != null ? (string) this.crash_dump_sr : "",
        crashdumps = this.crashdumps != null ? Helper.RefListToStringArray<Host_crashdump>(this.crashdumps) : new string[0],
        patches = this.patches != null ? Helper.RefListToStringArray<Host_patch>(this.patches) : new string[0],
        PBDs = this.PBDs != null ? Helper.RefListToStringArray<PBD>(this.PBDs) : new string[0],
        host_CPUs = this.host_CPUs != null ? Helper.RefListToStringArray<Host_cpu>(this.host_CPUs) : new string[0],
        cpu_info = (object) Maps.convert_to_proxy_string_string(this.cpu_info),
        hostname = this.hostname != null ? this.hostname : "",
        address = this.address != null ? this.address : "",
        metrics = this.metrics != null ? (string) this.metrics : "",
        license_params = (object) Maps.convert_to_proxy_string_string(this.license_params),
        ha_statefiles = this.ha_statefiles,
        ha_network_peers = this.ha_network_peers,
        blobs = (object) Maps.convert_to_proxy_string_XenRefBlob(this.blobs),
        tags = this.tags,
        external_auth_type = this.external_auth_type != null ? this.external_auth_type : "",
        external_auth_service_name = this.external_auth_service_name != null ? this.external_auth_service_name : "",
        external_auth_configuration = (object) Maps.convert_to_proxy_string_string(this.external_auth_configuration),
        edition = this.edition != null ? this.edition : "",
        license_server = (object) Maps.convert_to_proxy_string_string(this.license_server),
        bios_strings = (object) Maps.convert_to_proxy_string_string(this.bios_strings),
        power_on_mode = this.power_on_mode != null ? this.power_on_mode : "",
        power_on_config = (object) Maps.convert_to_proxy_string_string(this.power_on_config),
        local_cache_sr = this.local_cache_sr != null ? (string) this.local_cache_sr : "",
        chipset_info = (object) Maps.convert_to_proxy_string_string(this.chipset_info),
        PCIs = this.PCIs != null ? Helper.RefListToStringArray<PCI>(this.PCIs) : new string[0],
        PGPUs = this.PGPUs != null ? Helper.RefListToStringArray<PGPU>(this.PGPUs) : new string[0]
      };
    }

    public bool DeepEquals(Host other, bool ignoreCurrentOperations)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (!ignoreCurrentOperations && !Helper.AreEqual2<Dictionary<string, host_allowed_operations>>(this.current_operations, other.current_operations) || (!Helper.AreEqual2<string>(this._uuid, other._uuid) || !Helper.AreEqual2<string>(this._name_label, other._name_label)) || (!Helper.AreEqual2<string>(this._name_description, other._name_description) || !Helper.AreEqual2<long>(this._memory_overhead, other._memory_overhead) || (!Helper.AreEqual2<List<host_allowed_operations>>(this._allowed_operations, other._allowed_operations) || !Helper.AreEqual2<long>(this._API_version_major, other._API_version_major))) || (!Helper.AreEqual2<long>(this._API_version_minor, other._API_version_minor) || !Helper.AreEqual2<string>(this._API_version_vendor, other._API_version_vendor) || (!Helper.AreEqual2<Dictionary<string, string>>(this._API_version_vendor_implementation, other._API_version_vendor_implementation) || !Helper.AreEqual2<bool>(this._enabled, other._enabled)) || (!Helper.AreEqual2<Dictionary<string, string>>(this._software_version, other._software_version) || !Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config) || (!Helper.AreEqual2<string[]>(this._capabilities, other._capabilities) || !Helper.AreEqual2<Dictionary<string, string>>(this._cpu_configuration, other._cpu_configuration)))) || (!Helper.AreEqual2<string>(this._sched_policy, other._sched_policy) || !Helper.AreEqual2<string[]>(this._supported_bootloaders, other._supported_bootloaders) || (!Helper.AreEqual2<List<XenRef<VM>>>(this._resident_VMs, other._resident_VMs) || !Helper.AreEqual2<Dictionary<string, string>>(this._logging, other._logging)) || (!Helper.AreEqual2<List<XenRef<PIF>>>(this._PIFs, other._PIFs) || !Helper.AreEqual2<XenRef<SR>>(this._suspend_image_sr, other._suspend_image_sr) || (!Helper.AreEqual2<XenRef<SR>>(this._crash_dump_sr, other._crash_dump_sr) || !Helper.AreEqual2<List<XenRef<Host_crashdump>>>(this._crashdumps, other._crashdumps))) || (!Helper.AreEqual2<List<XenRef<Host_patch>>>(this._patches, other._patches) || !Helper.AreEqual2<List<XenRef<PBD>>>(this._PBDs, other._PBDs) || (!Helper.AreEqual2<List<XenRef<Host_cpu>>>(this._host_CPUs, other._host_CPUs) || !Helper.AreEqual2<Dictionary<string, string>>(this._cpu_info, other._cpu_info)) || (!Helper.AreEqual2<string>(this._hostname, other._hostname) || !Helper.AreEqual2<string>(this._address, other._address) || (!Helper.AreEqual2<XenRef<Host_metrics>>(this._metrics, other._metrics) || !Helper.AreEqual2<Dictionary<string, string>>(this._license_params, other._license_params))))) || (!Helper.AreEqual2<string[]>(this._ha_statefiles, other._ha_statefiles) || !Helper.AreEqual2<string[]>(this._ha_network_peers, other._ha_network_peers) || (!Helper.AreEqual2<Dictionary<string, XenRef<Blob>>>(this._blobs, other._blobs) || !Helper.AreEqual2<string[]>(this._tags, other._tags)) || (!Helper.AreEqual2<string>(this._external_auth_type, other._external_auth_type) || !Helper.AreEqual2<string>(this._external_auth_service_name, other._external_auth_service_name) || (!Helper.AreEqual2<Dictionary<string, string>>(this._external_auth_configuration, other._external_auth_configuration) || !Helper.AreEqual2<string>(this._edition, other._edition))) || (!Helper.AreEqual2<Dictionary<string, string>>(this._license_server, other._license_server) || !Helper.AreEqual2<Dictionary<string, string>>(this._bios_strings, other._bios_strings) || (!Helper.AreEqual2<string>(this._power_on_mode, other._power_on_mode) || !Helper.AreEqual2<Dictionary<string, string>>(this._power_on_config, other._power_on_config)) || (!Helper.AreEqual2<XenRef<SR>>(this._local_cache_sr, other._local_cache_sr) || !Helper.AreEqual2<Dictionary<string, string>>(this._chipset_info, other._chipset_info) || !Helper.AreEqual2<List<XenRef<PCI>>>(this._PCIs, other._PCIs)))))
        return false;
      return Helper.AreEqual2<List<XenRef<PGPU>>>(this._PGPUs, other._PGPUs);
    }

    public override string SaveChanges(Session session, string opaqueRef, Host server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
        Host.set_name_label(session, opaqueRef, this._name_label);
      if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
        Host.set_name_description(session, opaqueRef, this._name_description);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Host.set_other_config(session, opaqueRef, this._other_config);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._logging, server._logging))
        Host.set_logging(session, opaqueRef, this._logging);
      if (!Helper.AreEqual2<XenRef<SR>>(this._suspend_image_sr, server._suspend_image_sr))
        Host.set_suspend_image_sr(session, opaqueRef, (string) this._suspend_image_sr);
      if (!Helper.AreEqual2<XenRef<SR>>(this._crash_dump_sr, server._crash_dump_sr))
        Host.set_crash_dump_sr(session, opaqueRef, (string) this._crash_dump_sr);
      if (!Helper.AreEqual2<string>(this._hostname, server._hostname))
        Host.set_hostname(session, opaqueRef, this._hostname);
      if (!Helper.AreEqual2<string>(this._address, server._address))
        Host.set_address(session, opaqueRef, this._address);
      if (!Helper.AreEqual2<string[]>(this._tags, server._tags))
        Host.set_tags(session, opaqueRef, this._tags);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._license_server, server._license_server))
        Host.set_license_server(session, opaqueRef, this._license_server);
      return (string) null;
    }

    public static Host get_record(Session session, string _host)
    {
      return new Host(session.proxy.host_get_record(session.uuid, _host != null ? _host : "").parse());
    }

    public static XenRef<Host> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Host>.Create(session.proxy.host_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static List<XenRef<Host>> get_by_name_label(Session session, string _label)
    {
      return XenRef<Host>.Create(session.proxy.host_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _host)
    {
      return session.proxy.host_get_uuid(session.uuid, _host != null ? _host : "").parse();
    }

    public static string get_name_label(Session session, string _host)
    {
      return session.proxy.host_get_name_label(session.uuid, _host != null ? _host : "").parse();
    }

    public static string get_name_description(Session session, string _host)
    {
      return session.proxy.host_get_name_description(session.uuid, _host != null ? _host : "").parse();
    }

    public static long get_memory_overhead(Session session, string _host)
    {
      return long.Parse(session.proxy.host_get_memory_overhead(session.uuid, _host != null ? _host : "").parse());
    }

    public static List<host_allowed_operations> get_allowed_operations(Session session, string _host)
    {
      return Helper.StringArrayToEnumList<host_allowed_operations>(session.proxy.host_get_allowed_operations(session.uuid, _host != null ? _host : "").parse());
    }

    public static Dictionary<string, host_allowed_operations> get_current_operations(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_host_allowed_operations(session.proxy.host_get_current_operations(session.uuid, _host != null ? _host : "").parse());
    }

    public static long get_API_version_major(Session session, string _host)
    {
      return long.Parse(session.proxy.host_get_api_version_major(session.uuid, _host != null ? _host : "").parse());
    }

    public static long get_API_version_minor(Session session, string _host)
    {
      return long.Parse(session.proxy.host_get_api_version_minor(session.uuid, _host != null ? _host : "").parse());
    }

    public static string get_API_version_vendor(Session session, string _host)
    {
      return session.proxy.host_get_api_version_vendor(session.uuid, _host != null ? _host : "").parse();
    }

    public static Dictionary<string, string> get_API_version_vendor_implementation(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_api_version_vendor_implementation(session.uuid, _host != null ? _host : "").parse());
    }

    public static bool get_enabled(Session session, string _host)
    {
      return session.proxy.host_get_enabled(session.uuid, _host != null ? _host : "").parse();
    }

    public static Dictionary<string, string> get_software_version(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_software_version(session.uuid, _host != null ? _host : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_other_config(session.uuid, _host != null ? _host : "").parse());
    }

    public static string[] get_capabilities(Session session, string _host)
    {
      return session.proxy.host_get_capabilities(session.uuid, _host != null ? _host : "").parse();
    }

    public static Dictionary<string, string> get_cpu_configuration(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_cpu_configuration(session.uuid, _host != null ? _host : "").parse());
    }

    public static string get_sched_policy(Session session, string _host)
    {
      return session.proxy.host_get_sched_policy(session.uuid, _host != null ? _host : "").parse();
    }

    public static string[] get_supported_bootloaders(Session session, string _host)
    {
      return session.proxy.host_get_supported_bootloaders(session.uuid, _host != null ? _host : "").parse();
    }

    public static List<XenRef<VM>> get_resident_VMs(Session session, string _host)
    {
      return XenRef<VM>.Create(session.proxy.host_get_resident_vms(session.uuid, _host != null ? _host : "").parse());
    }

    public static Dictionary<string, string> get_logging(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_logging(session.uuid, _host != null ? _host : "").parse());
    }

    public static List<XenRef<PIF>> get_PIFs(Session session, string _host)
    {
      return XenRef<PIF>.Create(session.proxy.host_get_pifs(session.uuid, _host != null ? _host : "").parse());
    }

    public static XenRef<SR> get_suspend_image_sr(Session session, string _host)
    {
      return XenRef<SR>.Create(session.proxy.host_get_suspend_image_sr(session.uuid, _host != null ? _host : "").parse());
    }

    public static XenRef<SR> get_crash_dump_sr(Session session, string _host)
    {
      return XenRef<SR>.Create(session.proxy.host_get_crash_dump_sr(session.uuid, _host != null ? _host : "").parse());
    }

    public static List<XenRef<Host_crashdump>> get_crashdumps(Session session, string _host)
    {
      return XenRef<Host_crashdump>.Create(session.proxy.host_get_crashdumps(session.uuid, _host != null ? _host : "").parse());
    }

    public static List<XenRef<Host_patch>> get_patches(Session session, string _host)
    {
      return XenRef<Host_patch>.Create(session.proxy.host_get_patches(session.uuid, _host != null ? _host : "").parse());
    }

    public static List<XenRef<PBD>> get_PBDs(Session session, string _host)
    {
      return XenRef<PBD>.Create(session.proxy.host_get_pbds(session.uuid, _host != null ? _host : "").parse());
    }

    public static List<XenRef<Host_cpu>> get_host_CPUs(Session session, string _host)
    {
      return XenRef<Host_cpu>.Create(session.proxy.host_get_host_cpus(session.uuid, _host != null ? _host : "").parse());
    }

    public static Dictionary<string, string> get_cpu_info(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_cpu_info(session.uuid, _host != null ? _host : "").parse());
    }

    public static string get_hostname(Session session, string _host)
    {
      return session.proxy.host_get_hostname(session.uuid, _host != null ? _host : "").parse();
    }

    public static string get_address(Session session, string _host)
    {
      return session.proxy.host_get_address(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Host_metrics> get_metrics(Session session, string _host)
    {
      return XenRef<Host_metrics>.Create(session.proxy.host_get_metrics(session.uuid, _host != null ? _host : "").parse());
    }

    public static Dictionary<string, string> get_license_params(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_license_params(session.uuid, _host != null ? _host : "").parse());
    }

    public static string[] get_ha_statefiles(Session session, string _host)
    {
      return session.proxy.host_get_ha_statefiles(session.uuid, _host != null ? _host : "").parse();
    }

    public static string[] get_ha_network_peers(Session session, string _host)
    {
      return session.proxy.host_get_ha_network_peers(session.uuid, _host != null ? _host : "").parse();
    }

    public static Dictionary<string, XenRef<Blob>> get_blobs(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_XenRefBlob(session.proxy.host_get_blobs(session.uuid, _host != null ? _host : "").parse());
    }

    public static string[] get_tags(Session session, string _host)
    {
      return session.proxy.host_get_tags(session.uuid, _host != null ? _host : "").parse();
    }

    public static string get_external_auth_type(Session session, string _host)
    {
      return session.proxy.host_get_external_auth_type(session.uuid, _host != null ? _host : "").parse();
    }

    public static string get_external_auth_service_name(Session session, string _host)
    {
      return session.proxy.host_get_external_auth_service_name(session.uuid, _host != null ? _host : "").parse();
    }

    public static Dictionary<string, string> get_external_auth_configuration(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_external_auth_configuration(session.uuid, _host != null ? _host : "").parse());
    }

    public static string get_edition(Session session, string _host)
    {
      return session.proxy.host_get_edition(session.uuid, _host != null ? _host : "").parse();
    }

    public static Dictionary<string, string> get_license_server(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_license_server(session.uuid, _host != null ? _host : "").parse());
    }

    public static Dictionary<string, string> get_bios_strings(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_bios_strings(session.uuid, _host != null ? _host : "").parse());
    }

    public static string get_power_on_mode(Session session, string _host)
    {
      return session.proxy.host_get_power_on_mode(session.uuid, _host != null ? _host : "").parse();
    }

    public static Dictionary<string, string> get_power_on_config(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_power_on_config(session.uuid, _host != null ? _host : "").parse());
    }

    public static XenRef<SR> get_local_cache_sr(Session session, string _host)
    {
      return XenRef<SR>.Create(session.proxy.host_get_local_cache_sr(session.uuid, _host != null ? _host : "").parse());
    }

    public static Dictionary<string, string> get_chipset_info(Session session, string _host)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_get_chipset_info(session.uuid, _host != null ? _host : "").parse());
    }

    public static List<XenRef<PCI>> get_PCIs(Session session, string _host)
    {
      return XenRef<PCI>.Create(session.proxy.host_get_pcis(session.uuid, _host != null ? _host : "").parse());
    }

    public static List<XenRef<PGPU>> get_PGPUs(Session session, string _host)
    {
      return XenRef<PGPU>.Create(session.proxy.host_get_pgpus(session.uuid, _host != null ? _host : "").parse());
    }

    public static void set_name_label(Session session, string _host, string _label)
    {
      session.proxy.host_set_name_label(session.uuid, _host != null ? _host : "", _label != null ? _label : "").parse();
    }

    public static void set_name_description(Session session, string _host, string _description)
    {
      session.proxy.host_set_name_description(session.uuid, _host != null ? _host : "", _description != null ? _description : "").parse();
    }

    public static void set_other_config(Session session, string _host, Dictionary<string, string> _other_config)
    {
      session.proxy.host_set_other_config(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _host, string _key, string _value)
    {
      session.proxy.host_add_to_other_config(session.uuid, _host != null ? _host : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _host, string _key)
    {
      session.proxy.host_remove_from_other_config(session.uuid, _host != null ? _host : "", _key != null ? _key : "").parse();
    }

    public static void set_logging(Session session, string _host, Dictionary<string, string> _logging)
    {
      session.proxy.host_set_logging(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_logging)).parse();
    }

    public static void add_to_logging(Session session, string _host, string _key, string _value)
    {
      session.proxy.host_add_to_logging(session.uuid, _host != null ? _host : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_logging(Session session, string _host, string _key)
    {
      session.proxy.host_remove_from_logging(session.uuid, _host != null ? _host : "", _key != null ? _key : "").parse();
    }

    public static void set_suspend_image_sr(Session session, string _host, string _suspend_image_sr)
    {
      session.proxy.host_set_suspend_image_sr(session.uuid, _host != null ? _host : "", _suspend_image_sr != null ? _suspend_image_sr : "").parse();
    }

    public static void set_crash_dump_sr(Session session, string _host, string _crash_dump_sr)
    {
      session.proxy.host_set_crash_dump_sr(session.uuid, _host != null ? _host : "", _crash_dump_sr != null ? _crash_dump_sr : "").parse();
    }

    public static void set_hostname(Session session, string _host, string _hostname)
    {
      session.proxy.host_set_hostname(session.uuid, _host != null ? _host : "", _hostname != null ? _hostname : "").parse();
    }

    public static void set_address(Session session, string _host, string _address)
    {
      session.proxy.host_set_address(session.uuid, _host != null ? _host : "", _address != null ? _address : "").parse();
    }

    public static void set_tags(Session session, string _host, string[] _tags)
    {
      session.proxy.host_set_tags(session.uuid, _host != null ? _host : "", _tags).parse();
    }

    public static void add_tags(Session session, string _host, string _value)
    {
      session.proxy.host_add_tags(session.uuid, _host != null ? _host : "", _value != null ? _value : "").parse();
    }

    public static void remove_tags(Session session, string _host, string _value)
    {
      session.proxy.host_remove_tags(session.uuid, _host != null ? _host : "", _value != null ? _value : "").parse();
    }

    public static void set_license_server(Session session, string _host, Dictionary<string, string> _license_server)
    {
      session.proxy.host_set_license_server(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_license_server)).parse();
    }

    public static void add_to_license_server(Session session, string _host, string _key, string _value)
    {
      session.proxy.host_add_to_license_server(session.uuid, _host != null ? _host : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_license_server(Session session, string _host, string _key)
    {
      session.proxy.host_remove_from_license_server(session.uuid, _host != null ? _host : "", _key != null ? _key : "").parse();
    }

    public static void disable(Session session, string _host)
    {
      session.proxy.host_disable(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_disable(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_disable(session.uuid, _host != null ? _host : "").parse());
    }

    public static void enable(Session session, string _host)
    {
      session.proxy.host_enable(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_enable(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_enable(session.uuid, _host != null ? _host : "").parse());
    }

    public static void shutdown(Session session, string _host)
    {
      session.proxy.host_shutdown(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_shutdown(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_shutdown(session.uuid, _host != null ? _host : "").parse());
    }

    public static void reboot(Session session, string _host)
    {
      session.proxy.host_reboot(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_reboot(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_reboot(session.uuid, _host != null ? _host : "").parse());
    }

    public static string dmesg(Session session, string _host)
    {
      return session.proxy.host_dmesg(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_dmesg(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_dmesg(session.uuid, _host != null ? _host : "").parse());
    }

    public static string dmesg_clear(Session session, string _host)
    {
      return session.proxy.host_dmesg_clear(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_dmesg_clear(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_dmesg_clear(session.uuid, _host != null ? _host : "").parse());
    }

    public static string get_log(Session session, string _host)
    {
      return session.proxy.host_get_log(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_get_log(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_get_log(session.uuid, _host != null ? _host : "").parse());
    }

    public static void send_debug_keys(Session session, string _host, string _keys)
    {
      session.proxy.host_send_debug_keys(session.uuid, _host != null ? _host : "", _keys != null ? _keys : "").parse();
    }

    public static XenRef<Task> async_send_debug_keys(Session session, string _host, string _keys)
    {
      return XenRef<Task>.Create(session.proxy.async_host_send_debug_keys(session.uuid, _host != null ? _host : "", _keys != null ? _keys : "").parse());
    }

    public static void bugreport_upload(Session session, string _host, string _url, Dictionary<string, string> _options)
    {
      session.proxy.host_bugreport_upload(session.uuid, _host != null ? _host : "", _url != null ? _url : "", (object) Maps.convert_to_proxy_string_string(_options)).parse();
    }

    public static XenRef<Task> async_bugreport_upload(Session session, string _host, string _url, Dictionary<string, string> _options)
    {
      return XenRef<Task>.Create(session.proxy.async_host_bugreport_upload(session.uuid, _host != null ? _host : "", _url != null ? _url : "", (object) Maps.convert_to_proxy_string_string(_options)).parse());
    }

    public static string[] list_methods(Session session)
    {
      return session.proxy.host_list_methods(session.uuid).parse();
    }

    public static void license_apply(Session session, string _host, string _contents)
    {
      session.proxy.host_license_apply(session.uuid, _host != null ? _host : "", _contents != null ? _contents : "").parse();
    }

    public static XenRef<Task> async_license_apply(Session session, string _host, string _contents)
    {
      return XenRef<Task>.Create(session.proxy.async_host_license_apply(session.uuid, _host != null ? _host : "", _contents != null ? _contents : "").parse());
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.host_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_host_destroy(session.uuid, _self != null ? _self : "").parse());
    }

    public static void power_on(Session session, string _host)
    {
      session.proxy.host_power_on(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_power_on(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_power_on(session.uuid, _host != null ? _host : "").parse());
    }

    public static void emergency_ha_disable(Session session)
    {
      session.proxy.host_emergency_ha_disable(session.uuid).parse();
    }

    public static List<Data_source> get_data_sources(Session session, string _host)
    {
      return Helper.Proxy_Data_sourceArrayToData_sourceList(session.proxy.host_get_data_sources(session.uuid, _host != null ? _host : "").parse());
    }

    public static void record_data_source(Session session, string _host, string _data_source)
    {
      session.proxy.host_record_data_source(session.uuid, _host != null ? _host : "", _data_source != null ? _data_source : "").parse();
    }

    public static double query_data_source(Session session, string _host, string _data_source)
    {
      return Convert.ToDouble(session.proxy.host_query_data_source(session.uuid, _host != null ? _host : "", _data_source != null ? _data_source : "").parse());
    }

    public static void forget_data_source_archives(Session session, string _host, string _data_source)
    {
      session.proxy.host_forget_data_source_archives(session.uuid, _host != null ? _host : "", _data_source != null ? _data_source : "").parse();
    }

    public static void assert_can_evacuate(Session session, string _host)
    {
      session.proxy.host_assert_can_evacuate(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_assert_can_evacuate(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_assert_can_evacuate(session.uuid, _host != null ? _host : "").parse());
    }

    public static Dictionary<XenRef<VM>, string[]> get_vms_which_prevent_evacuation(Session session, string _self)
    {
      return Maps.convert_from_proxy_XenRefVM_string_array(session.proxy.host_get_vms_which_prevent_evacuation(session.uuid, _self != null ? _self : "").parse());
    }

    public static XenRef<Task> async_get_vms_which_prevent_evacuation(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_host_get_vms_which_prevent_evacuation(session.uuid, _self != null ? _self : "").parse());
    }

    public static void evacuate(Session session, string _host)
    {
      session.proxy.host_evacuate(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_evacuate(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_evacuate(session.uuid, _host != null ? _host : "").parse());
    }

    public static void syslog_reconfigure(Session session, string _host)
    {
      session.proxy.host_syslog_reconfigure(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_syslog_reconfigure(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_syslog_reconfigure(session.uuid, _host != null ? _host : "").parse());
    }

    public static void management_reconfigure(Session session, string _pif)
    {
      session.proxy.host_management_reconfigure(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static XenRef<Task> async_management_reconfigure(Session session, string _pif)
    {
      return XenRef<Task>.Create(session.proxy.async_host_management_reconfigure(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static void local_management_reconfigure(Session session, string _interface)
    {
      session.proxy.host_local_management_reconfigure(session.uuid, _interface != null ? _interface : "").parse();
    }

    public static void management_disable(Session session)
    {
      session.proxy.host_management_disable(session.uuid).parse();
    }

    public static XenRef<PIF> get_management_interface(Session session, string _host)
    {
      return XenRef<PIF>.Create(session.proxy.host_get_management_interface(session.uuid, _host != null ? _host : "").parse());
    }

    public static XenRef<Task> async_get_management_interface(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_get_management_interface(session.uuid, _host != null ? _host : "").parse());
    }

    public static string get_system_status_capabilities(Session session, string _host)
    {
      return session.proxy.host_get_system_status_capabilities(session.uuid, _host != null ? _host : "").parse();
    }

    public static void restart_agent(Session session, string _host)
    {
      session.proxy.host_restart_agent(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_restart_agent(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_restart_agent(session.uuid, _host != null ? _host : "").parse());
    }

    public static void shutdown_agent(Session session)
    {
      session.proxy.host_shutdown_agent(session.uuid).parse();
    }

    public static void set_hostname_live(Session session, string _host, string _hostname)
    {
      session.proxy.host_set_hostname_live(session.uuid, _host != null ? _host : "", _hostname != null ? _hostname : "").parse();
    }

    public static long compute_free_memory(Session session, string _host)
    {
      return long.Parse(session.proxy.host_compute_free_memory(session.uuid, _host != null ? _host : "").parse());
    }

    public static XenRef<Task> async_compute_free_memory(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_compute_free_memory(session.uuid, _host != null ? _host : "").parse());
    }

    public static long compute_memory_overhead(Session session, string _host)
    {
      return long.Parse(session.proxy.host_compute_memory_overhead(session.uuid, _host != null ? _host : "").parse());
    }

    public static XenRef<Task> async_compute_memory_overhead(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_compute_memory_overhead(session.uuid, _host != null ? _host : "").parse());
    }

    public static void sync_data(Session session, string _host)
    {
      session.proxy.host_sync_data(session.uuid, _host != null ? _host : "").parse();
    }

    public static void backup_rrds(Session session, string _host, double _delay)
    {
      session.proxy.host_backup_rrds(session.uuid, _host != null ? _host : "", _delay).parse();
    }

    public static XenRef<Blob> create_new_blob(Session session, string _host, string _name, string _mime_type, bool _public)
    {
      return XenRef<Blob>.Create(session.proxy.host_create_new_blob(session.uuid, _host != null ? _host : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _host, string _name, string _mime_type, bool _public)
    {
      return XenRef<Task>.Create(session.proxy.async_host_create_new_blob(session.uuid, _host != null ? _host : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static string call_plugin(Session session, string _host, string _plugin, string _fn, Dictionary<string, string> _args)
    {
      return session.proxy.host_call_plugin(session.uuid, _host != null ? _host : "", _plugin != null ? _plugin : "", _fn != null ? _fn : "", (object) Maps.convert_to_proxy_string_string(_args)).parse();
    }

    public static XenRef<Task> async_call_plugin(Session session, string _host, string _plugin, string _fn, Dictionary<string, string> _args)
    {
      return XenRef<Task>.Create(session.proxy.async_host_call_plugin(session.uuid, _host != null ? _host : "", _plugin != null ? _plugin : "", _fn != null ? _fn : "", (object) Maps.convert_to_proxy_string_string(_args)).parse());
    }

    public static DateTime get_servertime(Session session, string _host)
    {
      return session.proxy.host_get_servertime(session.uuid, _host != null ? _host : "").parse();
    }

    public static DateTime get_server_localtime(Session session, string _host)
    {
      return session.proxy.host_get_server_localtime(session.uuid, _host != null ? _host : "").parse();
    }

    public static void enable_external_auth(Session session, string _host, Dictionary<string, string> _config, string _service_name, string _auth_type)
    {
      session.proxy.host_enable_external_auth(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_config), _service_name != null ? _service_name : "", _auth_type != null ? _auth_type : "").parse();
    }

    public static void disable_external_auth(Session session, string _host, Dictionary<string, string> _config)
    {
      session.proxy.host_disable_external_auth(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_config)).parse();
    }

    public static Dictionary<XenRef<VM>, string[]> retrieve_wlb_evacuate_recommendations(Session session, string _self)
    {
      return Maps.convert_from_proxy_XenRefVM_string_array(session.proxy.host_retrieve_wlb_evacuate_recommendations(session.uuid, _self != null ? _self : "").parse());
    }

    public static XenRef<Task> async_retrieve_wlb_evacuate_recommendations(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_host_retrieve_wlb_evacuate_recommendations(session.uuid, _self != null ? _self : "").parse());
    }

    public static string get_server_certificate(Session session, string _host)
    {
      return session.proxy.host_get_server_certificate(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_get_server_certificate(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_get_server_certificate(session.uuid, _host != null ? _host : "").parse());
    }

    public static void apply_edition(Session session, string _host, string _edition)
    {
      session.proxy.host_apply_edition(session.uuid, _host != null ? _host : "", _edition != null ? _edition : "").parse();
    }

    public static void refresh_pack_info(Session session, string _host)
    {
      session.proxy.host_refresh_pack_info(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_refresh_pack_info(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_host_refresh_pack_info(session.uuid, _host != null ? _host : "").parse());
    }

    public static void set_power_on_mode(Session session, string _self, string _power_on_mode, Dictionary<string, string> _power_on_config)
    {
      session.proxy.host_set_power_on_mode(session.uuid, _self != null ? _self : "", _power_on_mode != null ? _power_on_mode : "", (object) Maps.convert_to_proxy_string_string(_power_on_config)).parse();
    }

    public static XenRef<Task> async_set_power_on_mode(Session session, string _self, string _power_on_mode, Dictionary<string, string> _power_on_config)
    {
      return XenRef<Task>.Create(session.proxy.async_host_set_power_on_mode(session.uuid, _self != null ? _self : "", _power_on_mode != null ? _power_on_mode : "", (object) Maps.convert_to_proxy_string_string(_power_on_config)).parse());
    }

    public static void set_cpu_features(Session session, string _host, string _features)
    {
      session.proxy.host_set_cpu_features(session.uuid, _host != null ? _host : "", _features != null ? _features : "").parse();
    }

    public static void reset_cpu_features(Session session, string _host)
    {
      session.proxy.host_reset_cpu_features(session.uuid, _host != null ? _host : "").parse();
    }

    public static void enable_local_storage_caching(Session session, string _host, string _sr)
    {
      session.proxy.host_enable_local_storage_caching(session.uuid, _host != null ? _host : "", _sr != null ? _sr : "").parse();
    }

    public static void disable_local_storage_caching(Session session, string _host)
    {
      session.proxy.host_disable_local_storage_caching(session.uuid, _host != null ? _host : "").parse();
    }

    public static Dictionary<string, string> migrate_receive(Session session, string _host, string _network, Dictionary<string, string> _options)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_migrate_receive(session.uuid, _host != null ? _host : "", _network != null ? _network : "", (object) Maps.convert_to_proxy_string_string(_options)).parse());
    }

    public static XenRef<Task> async_migrate_receive(Session session, string _host, string _network, Dictionary<string, string> _options)
    {
      return XenRef<Task>.Create(session.proxy.async_host_migrate_receive(session.uuid, _host != null ? _host : "", _network != null ? _network : "", (object) Maps.convert_to_proxy_string_string(_options)).parse());
    }

    public static List<XenRef<Host>> get_all(Session session)
    {
      return XenRef<Host>.Create(session.proxy.host_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Host>, Host> get_all_records(Session session)
    {
      return XenRef<Host>.Create<Proxy_Host>(session.proxy.host_get_all_records(session.uuid).parse());
    }

    public static XenRef<Blob> create_new_blob(Session session, string _host, string _name, string _mime_type)
    {
      return XenRef<Blob>.Create(session.proxy.host_create_new_blob(session.uuid, _host != null ? _host : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _host, string _name, string _mime_type)
    {
      return XenRef<Task>.Create(session.proxy.async_host_create_new_blob(session.uuid, _host != null ? _host : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }
  }
}
