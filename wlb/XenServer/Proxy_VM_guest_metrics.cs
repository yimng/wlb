// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_VM_guest_metrics
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_VM_guest_metrics
  {
    public string uuid;
    public object os_version;
    public object PV_drivers_version;
    public bool PV_drivers_up_to_date;
    public object memory;
    public object disks;
    public object networks;
    public object other;
    public DateTime last_updated;
    public object other_config;
    public bool live;
  }
}
