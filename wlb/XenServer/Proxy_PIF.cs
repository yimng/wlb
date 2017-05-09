// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_PIF
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_PIF
  {
    public string uuid;
    public string device;
    public string network;
    public string host;
    public string MAC;
    public string MTU;
    public string VLAN;
    public string metrics;
    public bool physical;
    public bool currently_attached;
    public string ip_configuration_mode;
    public string IP;
    public string netmask;
    public string gateway;
    public string DNS;
    public string bond_slave_of;
    public string[] bond_master_of;
    public string VLAN_master_of;
    public string[] VLAN_slave_of;
    public bool management;
    public object other_config;
    public bool disallow_unplug;
    public string[] tunnel_access_PIF_of;
    public string[] tunnel_transport_PIF_of;
    public string ipv6_configuration_mode;
    public string[] IPv6;
    public string ipv6_gateway;
    public string primary_address_type;
  }
}
