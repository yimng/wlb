// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_Pool
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_Pool
  {
    public string uuid;
    public string name_label;
    public string name_description;
    public string master;
    public string default_SR;
    public string suspend_image_SR;
    public string crash_dump_SR;
    public object other_config;
    public bool ha_enabled;
    public object ha_configuration;
    public string[] ha_statefiles;
    public string ha_host_failures_to_tolerate;
    public string ha_plan_exists_for;
    public bool ha_allow_overcommit;
    public bool ha_overcommitted;
    public object blobs;
    public string[] tags;
    public object gui_config;
    public string wlb_url;
    public string wlb_username;
    public bool wlb_enabled;
    public bool wlb_verify_cert;
    public bool redo_log_enabled;
    public string redo_log_vdi;
    public string vswitch_controller;
    public object restrictions;
    public string[] metadata_VDIs;
  }
}
