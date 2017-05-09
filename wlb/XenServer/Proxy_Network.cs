// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_Network
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_Network
  {
    public string uuid;
    public string name_label;
    public string name_description;
    public string[] allowed_operations;
    public object current_operations;
    public string[] VIFs;
    public string[] PIFs;
    public string MTU;
    public object other_config;
    public string bridge;
    public object blobs;
    public string[] tags;
    public string default_locking_mode;
  }
}
