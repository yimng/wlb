﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_Host_cpu
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_Host_cpu
  {
    public string uuid;
    public string host;
    public string number;
    public string vendor;
    public string speed;
    public string modelname;
    public string family;
    public string model;
    public string stepping;
    public string flags;
    public string features;
    public double utilisation;
    public object other_config;
  }
}
