// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_SM
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_SM
  {
    public string uuid;
    public string name_label;
    public string name_description;
    public string type;
    public string vendor;
    public string copyright;
    public string version;
    public string required_api_version;
    public object configuration;
    public string[] capabilities;
    public object other_config;
    public string driver_filename;
  }
}
