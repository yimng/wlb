﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_Task
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_Task
  {
    public string uuid;
    public string name_label;
    public string name_description;
    public string[] allowed_operations;
    public object current_operations;
    public DateTime created;
    public DateTime finished;
    public string status;
    public string resident_on;
    public double progress;
    public string type;
    public string result;
    public string[] error_info;
    public object other_config;
    public string subtask_of;
    public string[] subtasks;
  }
}