// Decompiled with JetBrains decompiler
// Type: XenAPI.PIF
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class PIF : XenObject<PIF>
  {
    private string _uuid;
    private string _device;
    private XenRef<Network> _network;
    private XenRef<Host> _host;
    private string _MAC;
    private long _MTU;
    private long _VLAN;
    private XenRef<PIF_metrics> _metrics;
    private bool _physical;
    private bool _currently_attached;
    private ip_configuration_mode _ip_configuration_mode;
    private string _IP;
    private string _netmask;
    private string _gateway;
    private string _DNS;
    private XenRef<Bond> _bond_slave_of;
    private List<XenRef<Bond>> _bond_master_of;
    private XenRef<XenAPI.VLAN> _VLAN_master_of;
    private List<XenRef<XenAPI.VLAN>> _VLAN_slave_of;
    private bool _management;
    private Dictionary<string, string> _other_config;
    private bool _disallow_unplug;
    private List<XenRef<Tunnel>> _tunnel_access_PIF_of;
    private List<XenRef<Tunnel>> _tunnel_transport_PIF_of;
    private ipv6_configuration_mode _ipv6_configuration_mode;
    private string[] _IPv6;
    private string _ipv6_gateway;
    private primary_address_type _primary_address_type;

    public string uuid
    {
      get
      {
        return this._uuid;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._uuid))
          return;
        this._uuid = value;
        this.Changed = true;
        this.NotifyPropertyChanged("uuid");
      }
    }

    public string device
    {
      get
      {
        return this._device;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._device))
          return;
        this._device = value;
        this.Changed = true;
        this.NotifyPropertyChanged("device");
      }
    }

    public XenRef<Network> network
    {
      get
      {
        return this._network;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._network))
          return;
        this._network = value;
        this.Changed = true;
        this.NotifyPropertyChanged("network");
      }
    }

    public XenRef<Host> host
    {
      get
      {
        return this._host;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._host))
          return;
        this._host = value;
        this.Changed = true;
        this.NotifyPropertyChanged("host");
      }
    }

    public string MAC
    {
      get
      {
        return this._MAC;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._MAC))
          return;
        this._MAC = value;
        this.Changed = true;
        this.NotifyPropertyChanged("MAC");
      }
    }

    public long MTU
    {
      get
      {
        return this._MTU;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._MTU))
          return;
        this._MTU = value;
        this.Changed = true;
        this.NotifyPropertyChanged("MTU");
      }
    }

    public long VLAN
    {
      get
      {
        return this._VLAN;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VLAN))
          return;
        this._VLAN = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VLAN");
      }
    }

    public XenRef<PIF_metrics> metrics
    {
      get
      {
        return this._metrics;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._metrics))
          return;
        this._metrics = value;
        this.Changed = true;
        this.NotifyPropertyChanged("metrics");
      }
    }

    public bool physical
    {
      get
      {
        return this._physical;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._physical ? 1 : 0)))
          return;
        this._physical = value;
        this.Changed = true;
        this.NotifyPropertyChanged("physical");
      }
    }

    public bool currently_attached
    {
      get
      {
        return this._currently_attached;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._currently_attached ? 1 : 0)))
          return;
        this._currently_attached = value;
        this.Changed = true;
        this.NotifyPropertyChanged("currently_attached");
      }
    }

    public ip_configuration_mode ip_configuration_mode
    {
      get
      {
        return this._ip_configuration_mode;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ip_configuration_mode))
          return;
        this._ip_configuration_mode = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ip_configuration_mode");
      }
    }

    public string IP
    {
      get
      {
        return this._IP;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._IP))
          return;
        this._IP = value;
        this.Changed = true;
        this.NotifyPropertyChanged("IP");
      }
    }

    public string netmask
    {
      get
      {
        return this._netmask;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._netmask))
          return;
        this._netmask = value;
        this.Changed = true;
        this.NotifyPropertyChanged("netmask");
      }
    }

    public string gateway
    {
      get
      {
        return this._gateway;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._gateway))
          return;
        this._gateway = value;
        this.Changed = true;
        this.NotifyPropertyChanged("gateway");
      }
    }

    public string DNS
    {
      get
      {
        return this._DNS;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._DNS))
          return;
        this._DNS = value;
        this.Changed = true;
        this.NotifyPropertyChanged("DNS");
      }
    }

    public XenRef<Bond> bond_slave_of
    {
      get
      {
        return this._bond_slave_of;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._bond_slave_of))
          return;
        this._bond_slave_of = value;
        this.Changed = true;
        this.NotifyPropertyChanged("bond_slave_of");
      }
    }

    public List<XenRef<Bond>> bond_master_of
    {
      get
      {
        return this._bond_master_of;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._bond_master_of))
          return;
        this._bond_master_of = value;
        this.Changed = true;
        this.NotifyPropertyChanged("bond_master_of");
      }
    }

    public XenRef<XenAPI.VLAN> VLAN_master_of
    {
      get
      {
        return this._VLAN_master_of;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VLAN_master_of))
          return;
        this._VLAN_master_of = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VLAN_master_of");
      }
    }

    public List<XenRef<XenAPI.VLAN>> VLAN_slave_of
    {
      get
      {
        return this._VLAN_slave_of;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VLAN_slave_of))
          return;
        this._VLAN_slave_of = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VLAN_slave_of");
      }
    }

    public bool management
    {
      get
      {
        return this._management;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._management ? 1 : 0)))
          return;
        this._management = value;
        this.Changed = true;
        this.NotifyPropertyChanged("management");
      }
    }

    public Dictionary<string, string> other_config
    {
      get
      {
        return this._other_config;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._other_config))
          return;
        this._other_config = value;
        this.Changed = true;
        this.NotifyPropertyChanged("other_config");
      }
    }

    public bool disallow_unplug
    {
      get
      {
        return this._disallow_unplug;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._disallow_unplug ? 1 : 0)))
          return;
        this._disallow_unplug = value;
        this.Changed = true;
        this.NotifyPropertyChanged("disallow_unplug");
      }
    }

    public List<XenRef<Tunnel>> tunnel_access_PIF_of
    {
      get
      {
        return this._tunnel_access_PIF_of;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._tunnel_access_PIF_of))
          return;
        this._tunnel_access_PIF_of = value;
        this.Changed = true;
        this.NotifyPropertyChanged("tunnel_access_PIF_of");
      }
    }

    public List<XenRef<Tunnel>> tunnel_transport_PIF_of
    {
      get
      {
        return this._tunnel_transport_PIF_of;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._tunnel_transport_PIF_of))
          return;
        this._tunnel_transport_PIF_of = value;
        this.Changed = true;
        this.NotifyPropertyChanged("tunnel_transport_PIF_of");
      }
    }

    public ipv6_configuration_mode ipv6_configuration_mode
    {
      get
      {
        return this._ipv6_configuration_mode;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ipv6_configuration_mode))
          return;
        this._ipv6_configuration_mode = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ipv6_configuration_mode");
      }
    }

    public string[] IPv6
    {
      get
      {
        return this._IPv6;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._IPv6))
          return;
        this._IPv6 = value;
        this.Changed = true;
        this.NotifyPropertyChanged("IPv6");
      }
    }

    public string ipv6_gateway
    {
      get
      {
        return this._ipv6_gateway;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ipv6_gateway))
          return;
        this._ipv6_gateway = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ipv6_gateway");
      }
    }

    public primary_address_type primary_address_type
    {
      get
      {
        return this._primary_address_type;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._primary_address_type))
          return;
        this._primary_address_type = value;
        this.Changed = true;
        this.NotifyPropertyChanged("primary_address_type");
      }
    }

    public PIF()
    {
    }

    public PIF(string uuid, string device, XenRef<Network> network, XenRef<Host> host, string MAC, long MTU, long VLAN, XenRef<PIF_metrics> metrics, bool physical, bool currently_attached, ip_configuration_mode ip_configuration_mode, string IP, string netmask, string gateway, string DNS, XenRef<Bond> bond_slave_of, List<XenRef<Bond>> bond_master_of, XenRef<XenAPI.VLAN> VLAN_master_of, List<XenRef<XenAPI.VLAN>> VLAN_slave_of, bool management, Dictionary<string, string> other_config, bool disallow_unplug, List<XenRef<Tunnel>> tunnel_access_PIF_of, List<XenRef<Tunnel>> tunnel_transport_PIF_of, ipv6_configuration_mode ipv6_configuration_mode, string[] IPv6, string ipv6_gateway, primary_address_type primary_address_type)
    {
      this.uuid = uuid;
      this.device = device;
      this.network = network;
      this.host = host;
      this.MAC = MAC;
      this.MTU = MTU;
      this.VLAN = VLAN;
      this.metrics = metrics;
      this.physical = physical;
      this.currently_attached = currently_attached;
      this.ip_configuration_mode = ip_configuration_mode;
      this.IP = IP;
      this.netmask = netmask;
      this.gateway = gateway;
      this.DNS = DNS;
      this.bond_slave_of = bond_slave_of;
      this.bond_master_of = bond_master_of;
      this.VLAN_master_of = VLAN_master_of;
      this.VLAN_slave_of = VLAN_slave_of;
      this.management = management;
      this.other_config = other_config;
      this.disallow_unplug = disallow_unplug;
      this.tunnel_access_PIF_of = tunnel_access_PIF_of;
      this.tunnel_transport_PIF_of = tunnel_transport_PIF_of;
      this.ipv6_configuration_mode = ipv6_configuration_mode;
      this.IPv6 = IPv6;
      this.ipv6_gateway = ipv6_gateway;
      this.primary_address_type = primary_address_type;
    }

    public PIF(Proxy_PIF proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public PIF(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.device = Marshalling.ParseString(table, "device");
      this.network = Marshalling.ParseRef<Network>(table, "network");
      this.host = Marshalling.ParseRef<Host>(table, "host");
      this.MAC = Marshalling.ParseString(table, "MAC");
      this.MTU = Marshalling.ParseLong(table, "MTU");
      this.VLAN = Marshalling.ParseLong(table, "VLAN");
      this.metrics = Marshalling.ParseRef<PIF_metrics>(table, "metrics");
      this.physical = Marshalling.ParseBool(table, "physical");
      this.currently_attached = Marshalling.ParseBool(table, "currently_attached");
      this.ip_configuration_mode = (ip_configuration_mode) Helper.EnumParseDefault(typeof (ip_configuration_mode), Marshalling.ParseString(table, "ip_configuration_mode"));
      this.IP = Marshalling.ParseString(table, "IP");
      this.netmask = Marshalling.ParseString(table, "netmask");
      this.gateway = Marshalling.ParseString(table, "gateway");
      this.DNS = Marshalling.ParseString(table, "DNS");
      this.bond_slave_of = Marshalling.ParseRef<Bond>(table, "bond_slave_of");
      this.bond_master_of = Marshalling.ParseSetRef<Bond>(table, "bond_master_of");
      this.VLAN_master_of = Marshalling.ParseRef<XenAPI.VLAN>(table, "VLAN_master_of");
      this.VLAN_slave_of = Marshalling.ParseSetRef<XenAPI.VLAN>(table, "VLAN_slave_of");
      this.management = Marshalling.ParseBool(table, "management");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
      this.disallow_unplug = Marshalling.ParseBool(table, "disallow_unplug");
      this.tunnel_access_PIF_of = Marshalling.ParseSetRef<Tunnel>(table, "tunnel_access_PIF_of");
      this.tunnel_transport_PIF_of = Marshalling.ParseSetRef<Tunnel>(table, "tunnel_transport_PIF_of");
      this.ipv6_configuration_mode = (ipv6_configuration_mode) Helper.EnumParseDefault(typeof (ipv6_configuration_mode), Marshalling.ParseString(table, "ipv6_configuration_mode"));
      this.IPv6 = Marshalling.ParseStringArray(table, "IPv6");
      this.ipv6_gateway = Marshalling.ParseString(table, "ipv6_gateway");
      this.primary_address_type = (primary_address_type) Helper.EnumParseDefault(typeof (primary_address_type), Marshalling.ParseString(table, "primary_address_type"));
    }

    public override void UpdateFrom(PIF update)
    {
      this.uuid = update.uuid;
      this.device = update.device;
      this.network = update.network;
      this.host = update.host;
      this.MAC = update.MAC;
      this.MTU = update.MTU;
      this.VLAN = update.VLAN;
      this.metrics = update.metrics;
      this.physical = update.physical;
      this.currently_attached = update.currently_attached;
      this.ip_configuration_mode = update.ip_configuration_mode;
      this.IP = update.IP;
      this.netmask = update.netmask;
      this.gateway = update.gateway;
      this.DNS = update.DNS;
      this.bond_slave_of = update.bond_slave_of;
      this.bond_master_of = update.bond_master_of;
      this.VLAN_master_of = update.VLAN_master_of;
      this.VLAN_slave_of = update.VLAN_slave_of;
      this.management = update.management;
      this.other_config = update.other_config;
      this.disallow_unplug = update.disallow_unplug;
      this.tunnel_access_PIF_of = update.tunnel_access_PIF_of;
      this.tunnel_transport_PIF_of = update.tunnel_transport_PIF_of;
      this.ipv6_configuration_mode = update.ipv6_configuration_mode;
      this.IPv6 = update.IPv6;
      this.ipv6_gateway = update.ipv6_gateway;
      this.primary_address_type = update.primary_address_type;
    }

    internal void UpdateFromProxy(Proxy_PIF proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.device = proxy.device == null ? (string) null : proxy.device;
      this.network = proxy.network == null ? (XenRef<Network>) null : XenRef<Network>.Create(proxy.network);
      this.host = proxy.host == null ? (XenRef<Host>) null : XenRef<Host>.Create(proxy.host);
      this.MAC = proxy.MAC == null ? (string) null : proxy.MAC;
      this.MTU = proxy.MTU == null ? 0L : long.Parse(proxy.MTU);
      this.VLAN = proxy.VLAN == null ? 0L : long.Parse(proxy.VLAN);
      this.metrics = proxy.metrics == null ? (XenRef<PIF_metrics>) null : XenRef<PIF_metrics>.Create(proxy.metrics);
      this.physical = proxy.physical;
      this.currently_attached = proxy.currently_attached;
      this.ip_configuration_mode = proxy.ip_configuration_mode == null ? ip_configuration_mode.None : (ip_configuration_mode) Helper.EnumParseDefault(typeof (ip_configuration_mode), proxy.ip_configuration_mode);
      this.IP = proxy.IP == null ? (string) null : proxy.IP;
      this.netmask = proxy.netmask == null ? (string) null : proxy.netmask;
      this.gateway = proxy.gateway == null ? (string) null : proxy.gateway;
      this.DNS = proxy.DNS == null ? (string) null : proxy.DNS;
      this.bond_slave_of = proxy.bond_slave_of == null ? (XenRef<Bond>) null : XenRef<Bond>.Create(proxy.bond_slave_of);
      this.bond_master_of = proxy.bond_master_of == null ? (List<XenRef<Bond>>) null : XenRef<Bond>.Create(proxy.bond_master_of);
      this.VLAN_master_of = proxy.VLAN_master_of == null ? (XenRef<XenAPI.VLAN>) null : XenRef<XenAPI.VLAN>.Create(proxy.VLAN_master_of);
      this.VLAN_slave_of = proxy.VLAN_slave_of == null ? (List<XenRef<XenAPI.VLAN>>) null : XenRef<XenAPI.VLAN>.Create(proxy.VLAN_slave_of);
      this.management = proxy.management;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
      this.disallow_unplug = proxy.disallow_unplug;
      this.tunnel_access_PIF_of = proxy.tunnel_access_PIF_of == null ? (List<XenRef<Tunnel>>) null : XenRef<Tunnel>.Create(proxy.tunnel_access_PIF_of);
      this.tunnel_transport_PIF_of = proxy.tunnel_transport_PIF_of == null ? (List<XenRef<Tunnel>>) null : XenRef<Tunnel>.Create(proxy.tunnel_transport_PIF_of);
      this.ipv6_configuration_mode = proxy.ipv6_configuration_mode == null ? ipv6_configuration_mode.None : (ipv6_configuration_mode) Helper.EnumParseDefault(typeof (ipv6_configuration_mode), proxy.ipv6_configuration_mode);
      this.IPv6 = proxy.IPv6 == null ? new string[0] : proxy.IPv6;
      this.ipv6_gateway = proxy.ipv6_gateway == null ? (string) null : proxy.ipv6_gateway;
      this.primary_address_type = proxy.primary_address_type == null ? primary_address_type.IPv4 : (primary_address_type) Helper.EnumParseDefault(typeof (primary_address_type), proxy.primary_address_type);
    }

    public Proxy_PIF ToProxy()
    {
      return new Proxy_PIF()
      {
        uuid = this.uuid != null ? this.uuid : "",
        device = this.device != null ? this.device : "",
        network = this.network != null ? (string) this.network : "",
        host = this.host != null ? (string) this.host : "",
        MAC = this.MAC != null ? this.MAC : "",
        MTU = this.MTU.ToString(),
        VLAN = this.VLAN.ToString(),
        metrics = this.metrics != null ? (string) this.metrics : "",
        physical = this.physical,
        currently_attached = this.currently_attached,
        ip_configuration_mode = ip_configuration_mode_helper.ToString(this.ip_configuration_mode),
        IP = this.IP != null ? this.IP : "",
        netmask = this.netmask != null ? this.netmask : "",
        gateway = this.gateway != null ? this.gateway : "",
        DNS = this.DNS != null ? this.DNS : "",
        bond_slave_of = this.bond_slave_of != null ? (string) this.bond_slave_of : "",
        bond_master_of = this.bond_master_of != null ? Helper.RefListToStringArray<Bond>(this.bond_master_of) : new string[0],
        VLAN_master_of = this.VLAN_master_of != null ? (string) this.VLAN_master_of : "",
        VLAN_slave_of = this.VLAN_slave_of != null ? Helper.RefListToStringArray<XenAPI.VLAN>(this.VLAN_slave_of) : new string[0],
        management = this.management,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config),
        disallow_unplug = this.disallow_unplug,
        tunnel_access_PIF_of = this.tunnel_access_PIF_of != null ? Helper.RefListToStringArray<Tunnel>(this.tunnel_access_PIF_of) : new string[0],
        tunnel_transport_PIF_of = this.tunnel_transport_PIF_of != null ? Helper.RefListToStringArray<Tunnel>(this.tunnel_transport_PIF_of) : new string[0],
        ipv6_configuration_mode = ipv6_configuration_mode_helper.ToString(this.ipv6_configuration_mode),
        IPv6 = this.IPv6,
        ipv6_gateway = this.ipv6_gateway != null ? this.ipv6_gateway : "",
        primary_address_type = primary_address_type_helper.ToString(this.primary_address_type)
      };
    }

    public bool DeepEquals(PIF other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._device, other._device) && (Helper.AreEqual2<XenRef<Network>>(this._network, other._network) && Helper.AreEqual2<XenRef<Host>>(this._host, other._host)) && (Helper.AreEqual2<string>(this._MAC, other._MAC) && Helper.AreEqual2<long>(this._MTU, other._MTU) && (Helper.AreEqual2<long>(this._VLAN, other._VLAN) && Helper.AreEqual2<XenRef<PIF_metrics>>(this._metrics, other._metrics))) && (Helper.AreEqual2<bool>(this._physical, other._physical) && Helper.AreEqual2<bool>(this._currently_attached, other._currently_attached) && (Helper.AreEqual2<ip_configuration_mode>(this._ip_configuration_mode, other._ip_configuration_mode) && Helper.AreEqual2<string>(this._IP, other._IP)) && (Helper.AreEqual2<string>(this._netmask, other._netmask) && Helper.AreEqual2<string>(this._gateway, other._gateway) && (Helper.AreEqual2<string>(this._DNS, other._DNS) && Helper.AreEqual2<XenRef<Bond>>(this._bond_slave_of, other._bond_slave_of)))) && (Helper.AreEqual2<List<XenRef<Bond>>>(this._bond_master_of, other._bond_master_of) && Helper.AreEqual2<XenRef<XenAPI.VLAN>>(this._VLAN_master_of, other._VLAN_master_of) && (Helper.AreEqual2<List<XenRef<XenAPI.VLAN>>>(this._VLAN_slave_of, other._VLAN_slave_of) && Helper.AreEqual2<bool>(this._management, other._management)) && (Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config) && Helper.AreEqual2<bool>(this._disallow_unplug, other._disallow_unplug) && (Helper.AreEqual2<List<XenRef<Tunnel>>>(this._tunnel_access_PIF_of, other._tunnel_access_PIF_of) && Helper.AreEqual2<List<XenRef<Tunnel>>>(this._tunnel_transport_PIF_of, other._tunnel_transport_PIF_of))) && (Helper.AreEqual2<ipv6_configuration_mode>(this._ipv6_configuration_mode, other._ipv6_configuration_mode) && Helper.AreEqual2<string[]>(this._IPv6, other._IPv6) && Helper.AreEqual2<string>(this._ipv6_gateway, other._ipv6_gateway))))
        return Helper.AreEqual2<primary_address_type>(this._primary_address_type, other._primary_address_type);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, PIF server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        PIF.set_other_config(session, opaqueRef, this._other_config);
      if (!Helper.AreEqual2<bool>(this._disallow_unplug, server._disallow_unplug))
        PIF.set_disallow_unplug(session, opaqueRef, this._disallow_unplug);
      return (string) null;
    }

    public static PIF get_record(Session session, string _pif)
    {
      return new PIF(session.proxy.pif_get_record(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static XenRef<PIF> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<PIF>.Create(session.proxy.pif_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _pif)
    {
      return session.proxy.pif_get_uuid(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static string get_device(Session session, string _pif)
    {
      return session.proxy.pif_get_device(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static XenRef<Network> get_network(Session session, string _pif)
    {
      return XenRef<Network>.Create(session.proxy.pif_get_network(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static XenRef<Host> get_host(Session session, string _pif)
    {
      return XenRef<Host>.Create(session.proxy.pif_get_host(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static string get_MAC(Session session, string _pif)
    {
      return session.proxy.pif_get_mac(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static long get_MTU(Session session, string _pif)
    {
      return long.Parse(session.proxy.pif_get_mtu(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static long get_VLAN(Session session, string _pif)
    {
      return long.Parse(session.proxy.pif_get_vlan(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static XenRef<PIF_metrics> get_metrics(Session session, string _pif)
    {
      return XenRef<PIF_metrics>.Create(session.proxy.pif_get_metrics(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static bool get_physical(Session session, string _pif)
    {
      return session.proxy.pif_get_physical(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static bool get_currently_attached(Session session, string _pif)
    {
      return session.proxy.pif_get_currently_attached(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static ip_configuration_mode get_ip_configuration_mode(Session session, string _pif)
    {
      return (ip_configuration_mode) Helper.EnumParseDefault(typeof (ip_configuration_mode), session.proxy.pif_get_ip_configuration_mode(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static string get_IP(Session session, string _pif)
    {
      return session.proxy.pif_get_ip(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static string get_netmask(Session session, string _pif)
    {
      return session.proxy.pif_get_netmask(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static string get_gateway(Session session, string _pif)
    {
      return session.proxy.pif_get_gateway(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static string get_DNS(Session session, string _pif)
    {
      return session.proxy.pif_get_dns(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static XenRef<Bond> get_bond_slave_of(Session session, string _pif)
    {
      return XenRef<Bond>.Create(session.proxy.pif_get_bond_slave_of(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static List<XenRef<Bond>> get_bond_master_of(Session session, string _pif)
    {
      return XenRef<Bond>.Create(session.proxy.pif_get_bond_master_of(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static XenRef<XenAPI.VLAN> get_VLAN_master_of(Session session, string _pif)
    {
      return XenRef<XenAPI.VLAN>.Create(session.proxy.pif_get_vlan_master_of(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static List<XenRef<XenAPI.VLAN>> get_VLAN_slave_of(Session session, string _pif)
    {
      return XenRef<XenAPI.VLAN>.Create(session.proxy.pif_get_vlan_slave_of(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static bool get_management(Session session, string _pif)
    {
      return session.proxy.pif_get_management(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _pif)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pif_get_other_config(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static bool get_disallow_unplug(Session session, string _pif)
    {
      return session.proxy.pif_get_disallow_unplug(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static List<XenRef<Tunnel>> get_tunnel_access_PIF_of(Session session, string _pif)
    {
      return XenRef<Tunnel>.Create(session.proxy.pif_get_tunnel_access_pif_of(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static List<XenRef<Tunnel>> get_tunnel_transport_PIF_of(Session session, string _pif)
    {
      return XenRef<Tunnel>.Create(session.proxy.pif_get_tunnel_transport_pif_of(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static ipv6_configuration_mode get_ipv6_configuration_mode(Session session, string _pif)
    {
      return (ipv6_configuration_mode) Helper.EnumParseDefault(typeof (ipv6_configuration_mode), session.proxy.pif_get_ipv6_configuration_mode(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static string[] get_IPv6(Session session, string _pif)
    {
      return session.proxy.pif_get_ipv6(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static string get_ipv6_gateway(Session session, string _pif)
    {
      return session.proxy.pif_get_ipv6_gateway(session.uuid, _pif != null ? _pif : "").parse();
    }

    public static primary_address_type get_primary_address_type(Session session, string _pif)
    {
      return (primary_address_type) Helper.EnumParseDefault(typeof (primary_address_type), session.proxy.pif_get_primary_address_type(session.uuid, _pif != null ? _pif : "").parse());
    }

    public static void set_other_config(Session session, string _pif, Dictionary<string, string> _other_config)
    {
      session.proxy.pif_set_other_config(session.uuid, _pif != null ? _pif : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _pif, string _key, string _value)
    {
      session.proxy.pif_add_to_other_config(session.uuid, _pif != null ? _pif : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _pif, string _key)
    {
      session.proxy.pif_remove_from_other_config(session.uuid, _pif != null ? _pif : "", _key != null ? _key : "").parse();
    }

    public static void set_disallow_unplug(Session session, string _pif, bool _disallow_unplug)
    {
      session.proxy.pif_set_disallow_unplug(session.uuid, _pif != null ? _pif : "", _disallow_unplug).parse();
    }

    public static XenRef<PIF> create_VLAN(Session session, string _device, string _network, string _host, long _vlan)
    {
      return XenRef<PIF>.Create(session.proxy.pif_create_vlan(session.uuid, _device != null ? _device : "", _network != null ? _network : "", _host != null ? _host : "", _vlan.ToString()).parse());
    }

    public static XenRef<Task> async_create_VLAN(Session session, string _device, string _network, string _host, long _vlan)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_create_vlan(session.uuid, _device != null ? _device : "", _network != null ? _network : "", _host != null ? _host : "", _vlan.ToString()).parse());
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.pif_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_destroy(session.uuid, _self != null ? _self : "").parse());
    }

    public static void reconfigure_ip(Session session, string _self, ip_configuration_mode _mode, string _ip, string _netmask, string _gateway, string _dns)
    {
      session.proxy.pif_reconfigure_ip(session.uuid, _self != null ? _self : "", ip_configuration_mode_helper.ToString(_mode), _ip != null ? _ip : "", _netmask != null ? _netmask : "", _gateway != null ? _gateway : "", _dns != null ? _dns : "").parse();
    }

    public static XenRef<Task> async_reconfigure_ip(Session session, string _self, ip_configuration_mode _mode, string _ip, string _netmask, string _gateway, string _dns)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_reconfigure_ip(session.uuid, _self != null ? _self : "", ip_configuration_mode_helper.ToString(_mode), _ip != null ? _ip : "", _netmask != null ? _netmask : "", _gateway != null ? _gateway : "", _dns != null ? _dns : "").parse());
    }

    public static void reconfigure_ipv6(Session session, string _self, ipv6_configuration_mode _mode, string _ipv6, string _gateway, string _dns)
    {
      session.proxy.pif_reconfigure_ipv6(session.uuid, _self != null ? _self : "", ipv6_configuration_mode_helper.ToString(_mode), _ipv6 != null ? _ipv6 : "", _gateway != null ? _gateway : "", _dns != null ? _dns : "").parse();
    }

    public static XenRef<Task> async_reconfigure_ipv6(Session session, string _self, ipv6_configuration_mode _mode, string _ipv6, string _gateway, string _dns)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_reconfigure_ipv6(session.uuid, _self != null ? _self : "", ipv6_configuration_mode_helper.ToString(_mode), _ipv6 != null ? _ipv6 : "", _gateway != null ? _gateway : "", _dns != null ? _dns : "").parse());
    }

    public static void set_primary_address_type(Session session, string _self, primary_address_type _primary_address_type)
    {
      session.proxy.pif_set_primary_address_type(session.uuid, _self != null ? _self : "", primary_address_type_helper.ToString(_primary_address_type)).parse();
    }

    public static XenRef<Task> async_set_primary_address_type(Session session, string _self, primary_address_type _primary_address_type)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_set_primary_address_type(session.uuid, _self != null ? _self : "", primary_address_type_helper.ToString(_primary_address_type)).parse());
    }

    public static void scan(Session session, string _host)
    {
      session.proxy.pif_scan(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_scan(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_scan(session.uuid, _host != null ? _host : "").parse());
    }

    public static XenRef<PIF> introduce(Session session, string _host, string _mac, string _device)
    {
      return XenRef<PIF>.Create(session.proxy.pif_introduce(session.uuid, _host != null ? _host : "", _mac != null ? _mac : "", _device != null ? _device : "").parse());
    }

    public static XenRef<Task> async_introduce(Session session, string _host, string _mac, string _device)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_introduce(session.uuid, _host != null ? _host : "", _mac != null ? _mac : "", _device != null ? _device : "").parse());
    }

    public static void forget(Session session, string _self)
    {
      session.proxy.pif_forget(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_forget(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_forget(session.uuid, _self != null ? _self : "").parse());
    }

    public static void unplug(Session session, string _self)
    {
      session.proxy.pif_unplug(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_unplug(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_unplug(session.uuid, _self != null ? _self : "").parse());
    }

    public static void plug(Session session, string _self)
    {
      session.proxy.pif_plug(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_plug(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_plug(session.uuid, _self != null ? _self : "").parse());
    }

    public static XenRef<PIF> db_introduce(Session session, string _device, string _network, string _host, string _mac, long _mtu, long _vlan, bool _physical, ip_configuration_mode _ip_configuration_mode, string _ip, string _netmask, string _gateway, string _dns, string _bond_slave_of, string _vlan_master_of, bool _management, Dictionary<string, string> _other_config, bool _disallow_unplug, ipv6_configuration_mode _ipv6_configuration_mode, string[] _ipv6, string _ipv6_gateway, primary_address_type _primary_address_type)
    {
      return XenRef<PIF>.Create(session.proxy.pif_db_introduce(session.uuid, _device != null ? _device : "", _network != null ? _network : "", _host != null ? _host : "", _mac != null ? _mac : "", _mtu.ToString(), _vlan.ToString(), _physical, ip_configuration_mode_helper.ToString(_ip_configuration_mode), _ip != null ? _ip : "", _netmask != null ? _netmask : "", _gateway != null ? _gateway : "", _dns != null ? _dns : "", _bond_slave_of != null ? _bond_slave_of : "", _vlan_master_of != null ? _vlan_master_of : "", _management, (object) Maps.convert_to_proxy_string_string(_other_config), _disallow_unplug, ipv6_configuration_mode_helper.ToString(_ipv6_configuration_mode), _ipv6, _ipv6_gateway != null ? _ipv6_gateway : "", primary_address_type_helper.ToString(_primary_address_type)).parse());
    }

    public static XenRef<Task> async_db_introduce(Session session, string _device, string _network, string _host, string _mac, long _mtu, long _vlan, bool _physical, ip_configuration_mode _ip_configuration_mode, string _ip, string _netmask, string _gateway, string _dns, string _bond_slave_of, string _vlan_master_of, bool _management, Dictionary<string, string> _other_config, bool _disallow_unplug, ipv6_configuration_mode _ipv6_configuration_mode, string[] _ipv6, string _ipv6_gateway, primary_address_type _primary_address_type)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_db_introduce(session.uuid, _device != null ? _device : "", _network != null ? _network : "", _host != null ? _host : "", _mac != null ? _mac : "", _mtu.ToString(), _vlan.ToString(), _physical, ip_configuration_mode_helper.ToString(_ip_configuration_mode), _ip != null ? _ip : "", _netmask != null ? _netmask : "", _gateway != null ? _gateway : "", _dns != null ? _dns : "", _bond_slave_of != null ? _bond_slave_of : "", _vlan_master_of != null ? _vlan_master_of : "", _management, (object) Maps.convert_to_proxy_string_string(_other_config), _disallow_unplug, ipv6_configuration_mode_helper.ToString(_ipv6_configuration_mode), _ipv6, _ipv6_gateway != null ? _ipv6_gateway : "", primary_address_type_helper.ToString(_primary_address_type)).parse());
    }

    public static void db_forget(Session session, string _self)
    {
      session.proxy.pif_db_forget(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_db_forget(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_db_forget(session.uuid, _self != null ? _self : "").parse());
    }

    public static List<XenRef<PIF>> get_all(Session session)
    {
      return XenRef<PIF>.Create(session.proxy.pif_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<PIF>, PIF> get_all_records(Session session)
    {
      return XenRef<PIF>.Create<Proxy_PIF>(session.proxy.pif_get_all_records(session.uuid).parse());
    }

    public static XenRef<PIF> db_introduce(Session session, string _device, string _network, string _host, string _mac, long _mtu, long _vlan, bool _physical, ip_configuration_mode _ip_configuration_mode, string _ip, string _netmask, string _gateway, string _dns, string _bond_slave_of, string _vlan_master_of, bool _management, Dictionary<string, string> _other_config, bool _disallow_unplug)
    {
      return XenRef<PIF>.Create(session.proxy.pif_db_introduce(session.uuid, _device != null ? _device : "", _network != null ? _network : "", _host != null ? _host : "", _mac != null ? _mac : "", _mtu.ToString(), _vlan.ToString(), _physical, ip_configuration_mode_helper.ToString(_ip_configuration_mode), _ip != null ? _ip : "", _netmask != null ? _netmask : "", _gateway != null ? _gateway : "", _dns != null ? _dns : "", _bond_slave_of != null ? _bond_slave_of : "", _vlan_master_of != null ? _vlan_master_of : "", _management, (object) Maps.convert_to_proxy_string_string(_other_config), _disallow_unplug).parse());
    }

    public static XenRef<Task> async_db_introduce(Session session, string _device, string _network, string _host, string _mac, long _mtu, long _vlan, bool _physical, ip_configuration_mode _ip_configuration_mode, string _ip, string _netmask, string _gateway, string _dns, string _bond_slave_of, string _vlan_master_of, bool _management, Dictionary<string, string> _other_config, bool _disallow_unplug)
    {
      return XenRef<Task>.Create(session.proxy.async_pif_db_introduce(session.uuid, _device != null ? _device : "", _network != null ? _network : "", _host != null ? _host : "", _mac != null ? _mac : "", _mtu.ToString(), _vlan.ToString(), _physical, ip_configuration_mode_helper.ToString(_ip_configuration_mode), _ip != null ? _ip : "", _netmask != null ? _netmask : "", _gateway != null ? _gateway : "", _dns != null ? _dns : "", _bond_slave_of != null ? _bond_slave_of : "", _vlan_master_of != null ? _vlan_master_of : "", _management, (object) Maps.convert_to_proxy_string_string(_other_config), _disallow_unplug).parse());
    }
  }
}
