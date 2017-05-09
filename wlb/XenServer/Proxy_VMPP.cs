// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_VMPP
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_VMPP
  {
    public string uuid;
    public string name_label;
    public string name_description;
    public bool is_policy_enabled;
    public string backup_type;
    public string backup_retention_value;
    public string backup_frequency;
    public object backup_schedule;
    public bool is_backup_running;
    public DateTime backup_last_run_time;
    public string archive_target_type;
    public object archive_target_config;
    public string archive_frequency;
    public object archive_schedule;
    public bool is_archive_running;
    public DateTime archive_last_run_time;
    public string[] VMs;
    public bool is_alarm_enabled;
    public object alarm_config;
    public string[] recent_alerts;
  }
}
