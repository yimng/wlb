// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_PCI
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_PCI
  {
    public string uuid;
    public string class_name;
    public string vendor_name;
    public string device_name;
    public string host;
    public string pci_id;
    public string[] dependencies;
    public object other_config;
  }
}
