// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_Host
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_Host
  {
    public string uuid;
    public string name_label;
    public string name_description;
    public string memory_overhead;
    public string[] allowed_operations;
    public object current_operations;
    public string API_version_major;
    public string API_version_minor;
    public string API_version_vendor;
    public object API_version_vendor_implementation;
    public bool enabled;
    public object software_version;
    public object other_config;
    public string[] capabilities;
    public object cpu_configuration;
    public string sched_policy;
    public string[] supported_bootloaders;
    public string[] resident_VMs;
    public object logging;
    public string[] PIFs;
    public string suspend_image_sr;
    public string crash_dump_sr;
    public string[] crashdumps;
    public string[] patches;
    public string[] PBDs;
    public string[] host_CPUs;
    public object cpu_info;
    public string hostname;
    public string address;
    public string metrics;
    public object license_params;
    public string[] ha_statefiles;
    public string[] ha_network_peers;
    public object blobs;
    public string[] tags;
    public string external_auth_type;
    public string external_auth_service_name;
    public object external_auth_configuration;
    public string edition;
    public object license_server;
    public object bios_strings;
    public string power_on_mode;
    public object power_on_config;
    public string local_cache_sr;
    public object chipset_info;
    public string[] PCIs;
    public string[] PGPUs;
  }
}
