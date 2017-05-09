// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_VDI
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_VDI
  {
    public string uuid;
    public string name_label;
    public string name_description;
    public string[] allowed_operations;
    public object current_operations;
    public string SR;
    public string[] VBDs;
    public string[] crash_dumps;
    public string virtual_size;
    public string physical_utilisation;
    public string type;
    public bool sharable;
    public bool read_only;
    public object other_config;
    public bool storage_lock;
    public string location;
    public bool managed;
    public bool missing;
    public string parent;
    public object xenstore_data;
    public object sm_config;
    public bool is_a_snapshot;
    public string snapshot_of;
    public string[] snapshots;
    public DateTime snapshot_time;
    public string[] tags;
    public bool allow_caching;
    public string on_boot;
    public string metadata_of_pool;
    public bool metadata_latest;
  }
}
