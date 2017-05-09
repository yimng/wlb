// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_VM_metrics
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_VM_metrics
  {
    public string uuid;
    public string memory_actual;
    public string VCPUs_number;
    public object VCPUs_utilisation;
    public object VCPUs_CPU;
    public object VCPUs_params;
    public object VCPUs_flags;
    public string[] state;
    public DateTime start_time;
    public DateTime install_time;
    public DateTime last_updated;
    public object other_config;
  }
}
