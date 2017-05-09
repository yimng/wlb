// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_VGPU
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_VGPU
  {
    public string uuid;
    public string VM;
    public string GPU_group;
    public string device;
    public bool currently_attached;
    public object other_config;
  }
}
