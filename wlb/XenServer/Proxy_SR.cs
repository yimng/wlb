// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_SR
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_SR
  {
    public string uuid;
    public string name_label;
    public string name_description;
    public string[] allowed_operations;
    public object current_operations;
    public string[] VDIs;
    public string[] PBDs;
    public string virtual_allocation;
    public string physical_utilisation;
    public string physical_size;
    public string type;
    public string content_type;
    public bool shared;
    public object other_config;
    public string[] tags;
    public object sm_config;
    public object blobs;
    public bool local_cache_enabled;
    public string introduced_by;
  }
}
