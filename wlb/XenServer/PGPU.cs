// Decompiled with JetBrains decompiler
// Type: XenAPI.PGPU
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class PGPU : XenObject<PGPU>
  {
    private string _uuid;
    private XenRef<XenAPI.PCI> _PCI;
    private XenRef<XenAPI.GPU_group> _GPU_group;
    private XenRef<Host> _host;
    private Dictionary<string, string> _other_config;

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

    public XenRef<XenAPI.PCI> PCI
    {
      get
      {
        return this._PCI;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PCI))
          return;
        this._PCI = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PCI");
      }
    }

    public XenRef<XenAPI.GPU_group> GPU_group
    {
      get
      {
        return this._GPU_group;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._GPU_group))
          return;
        this._GPU_group = value;
        this.Changed = true;
        this.NotifyPropertyChanged("GPU_group");
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

    public PGPU()
    {
    }

    public PGPU(string uuid, XenRef<XenAPI.PCI> PCI, XenRef<XenAPI.GPU_group> GPU_group, XenRef<Host> host, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.PCI = PCI;
      this.GPU_group = GPU_group;
      this.host = host;
      this.other_config = other_config;
    }

    public PGPU(Proxy_PGPU proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public PGPU(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.PCI = Marshalling.ParseRef<XenAPI.PCI>(table, "PCI");
      this.GPU_group = Marshalling.ParseRef<XenAPI.GPU_group>(table, "GPU_group");
      this.host = Marshalling.ParseRef<Host>(table, "host");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(PGPU update)
    {
      this.uuid = update.uuid;
      this.PCI = update.PCI;
      this.GPU_group = update.GPU_group;
      this.host = update.host;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_PGPU proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.PCI = proxy.PCI == null ? (XenRef<XenAPI.PCI>) null : XenRef<XenAPI.PCI>.Create(proxy.PCI);
      this.GPU_group = proxy.GPU_group == null ? (XenRef<XenAPI.GPU_group>) null : XenRef<XenAPI.GPU_group>.Create(proxy.GPU_group);
      this.host = proxy.host == null ? (XenRef<Host>) null : XenRef<Host>.Create(proxy.host);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_PGPU ToProxy()
    {
      return new Proxy_PGPU()
      {
        uuid = this.uuid != null ? this.uuid : "",
        PCI = this.PCI != null ? (string) this.PCI : "",
        GPU_group = this.GPU_group != null ? (string) this.GPU_group : "",
        host = this.host != null ? (string) this.host : "",
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(PGPU other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<XenRef<XenAPI.PCI>>(this._PCI, other._PCI) && (Helper.AreEqual2<XenRef<XenAPI.GPU_group>>(this._GPU_group, other._GPU_group) && Helper.AreEqual2<XenRef<Host>>(this._host, other._host)))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, PGPU server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        PGPU.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static PGPU get_record(Session session, string _pgpu)
    {
      return new PGPU(session.proxy.pgpu_get_record(session.uuid, _pgpu != null ? _pgpu : "").parse());
    }

    public static XenRef<PGPU> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<PGPU>.Create(session.proxy.pgpu_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _pgpu)
    {
      return session.proxy.pgpu_get_uuid(session.uuid, _pgpu != null ? _pgpu : "").parse();
    }

    public static XenRef<XenAPI.PCI> get_PCI(Session session, string _pgpu)
    {
      return XenRef<XenAPI.PCI>.Create(session.proxy.pgpu_get_pci(session.uuid, _pgpu != null ? _pgpu : "").parse());
    }

    public static XenRef<XenAPI.GPU_group> get_GPU_group(Session session, string _pgpu)
    {
      return XenRef<XenAPI.GPU_group>.Create(session.proxy.pgpu_get_gpu_group(session.uuid, _pgpu != null ? _pgpu : "").parse());
    }

    public static XenRef<Host> get_host(Session session, string _pgpu)
    {
      return XenRef<Host>.Create(session.proxy.pgpu_get_host(session.uuid, _pgpu != null ? _pgpu : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _pgpu)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pgpu_get_other_config(session.uuid, _pgpu != null ? _pgpu : "").parse());
    }

    public static void set_other_config(Session session, string _pgpu, Dictionary<string, string> _other_config)
    {
      session.proxy.pgpu_set_other_config(session.uuid, _pgpu != null ? _pgpu : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _pgpu, string _key, string _value)
    {
      session.proxy.pgpu_add_to_other_config(session.uuid, _pgpu != null ? _pgpu : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _pgpu, string _key)
    {
      session.proxy.pgpu_remove_from_other_config(session.uuid, _pgpu != null ? _pgpu : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<PGPU>> get_all(Session session)
    {
      return XenRef<PGPU>.Create(session.proxy.pgpu_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<PGPU>, PGPU> get_all_records(Session session)
    {
      return XenRef<PGPU>.Create<Proxy_PGPU>(session.proxy.pgpu_get_all_records(session.uuid).parse());
    }
  }
}
