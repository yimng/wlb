// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_VBD
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_VBD
  {
    public string uuid;
    public string[] allowed_operations;
    public object current_operations;
    public string VM;
    public string VDI;
    public string device;
    public string userdevice;
    public bool bootable;
    public string mode;
    public string type;
    public bool unpluggable;
    public bool storage_lock;
    public bool empty;
    public object other_config;
    public bool currently_attached;
    public string status_code;
    public string status_detail;
    public object runtime_properties;
    public string qos_algorithm_type;
    public object qos_algorithm_params;
    public string[] qos_supported_algorithms;
    public string metrics;
  }
}
