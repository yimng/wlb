// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_Session
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_Session
  {
    public string uuid;
    public string this_host;
    public string this_user;
    public DateTime last_active;
    public bool pool;
    public object other_config;
    public bool is_local_superuser;
    public string subject;
    public DateTime validation_time;
    public string auth_user_sid;
    public string auth_user_name;
    public string[] rbac_permissions;
    public string[] tasks;
    public string parent;
  }
}
