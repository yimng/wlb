// Decompiled with JetBrains decompiler
// Type: XenAPI.VLAN
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class VLAN : XenObject<VLAN>
  {
    private string _uuid;
    private XenRef<PIF> _tagged_PIF;
    private XenRef<PIF> _untagged_PIF;
    private long _tag;
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

    public XenRef<PIF> tagged_PIF
    {
      get
      {
        return this._tagged_PIF;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._tagged_PIF))
          return;
        this._tagged_PIF = value;
        this.Changed = true;
        this.NotifyPropertyChanged("tagged_PIF");
      }
    }

    public XenRef<PIF> untagged_PIF
    {
      get
      {
        return this._untagged_PIF;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._untagged_PIF))
          return;
        this._untagged_PIF = value;
        this.Changed = true;
        this.NotifyPropertyChanged("untagged_PIF");
      }
    }

    public long tag
    {
      get
      {
        return this._tag;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._tag))
          return;
        this._tag = value;
        this.Changed = true;
        this.NotifyPropertyChanged("tag");
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

    public VLAN()
    {
    }

    public VLAN(string uuid, XenRef<PIF> tagged_PIF, XenRef<PIF> untagged_PIF, long tag, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.tagged_PIF = tagged_PIF;
      this.untagged_PIF = untagged_PIF;
      this.tag = tag;
      this.other_config = other_config;
    }

    public VLAN(Proxy_VLAN proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public VLAN(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.tagged_PIF = Marshalling.ParseRef<PIF>(table, "tagged_PIF");
      this.untagged_PIF = Marshalling.ParseRef<PIF>(table, "untagged_PIF");
      this.tag = Marshalling.ParseLong(table, "tag");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(VLAN update)
    {
      this.uuid = update.uuid;
      this.tagged_PIF = update.tagged_PIF;
      this.untagged_PIF = update.untagged_PIF;
      this.tag = update.tag;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_VLAN proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.tagged_PIF = proxy.tagged_PIF == null ? (XenRef<PIF>) null : XenRef<PIF>.Create(proxy.tagged_PIF);
      this.untagged_PIF = proxy.untagged_PIF == null ? (XenRef<PIF>) null : XenRef<PIF>.Create(proxy.untagged_PIF);
      this.tag = proxy.tag == null ? 0L : long.Parse(proxy.tag);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_VLAN ToProxy()
    {
      return new Proxy_VLAN()
      {
        uuid = this.uuid != null ? this.uuid : "",
        tagged_PIF = this.tagged_PIF != null ? (string) this.tagged_PIF : "",
        untagged_PIF = this.untagged_PIF != null ? (string) this.untagged_PIF : "",
        tag = this.tag.ToString(),
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(VLAN other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<XenRef<PIF>>(this._tagged_PIF, other._tagged_PIF) && (Helper.AreEqual2<XenRef<PIF>>(this._untagged_PIF, other._untagged_PIF) && Helper.AreEqual2<long>(this._tag, other._tag)))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, VLAN server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        VLAN.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static VLAN get_record(Session session, string _vlan)
    {
      return new VLAN(session.proxy.vlan_get_record(session.uuid, _vlan != null ? _vlan : "").parse());
    }

    public static XenRef<VLAN> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<VLAN>.Create(session.proxy.vlan_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _vlan)
    {
      return session.proxy.vlan_get_uuid(session.uuid, _vlan != null ? _vlan : "").parse();
    }

    public static XenRef<PIF> get_tagged_PIF(Session session, string _vlan)
    {
      return XenRef<PIF>.Create(session.proxy.vlan_get_tagged_pif(session.uuid, _vlan != null ? _vlan : "").parse());
    }

    public static XenRef<PIF> get_untagged_PIF(Session session, string _vlan)
    {
      return XenRef<PIF>.Create(session.proxy.vlan_get_untagged_pif(session.uuid, _vlan != null ? _vlan : "").parse());
    }

    public static long get_tag(Session session, string _vlan)
    {
      return long.Parse(session.proxy.vlan_get_tag(session.uuid, _vlan != null ? _vlan : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _vlan)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vlan_get_other_config(session.uuid, _vlan != null ? _vlan : "").parse());
    }

    public static void set_other_config(Session session, string _vlan, Dictionary<string, string> _other_config)
    {
      session.proxy.vlan_set_other_config(session.uuid, _vlan != null ? _vlan : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _vlan, string _key, string _value)
    {
      session.proxy.vlan_add_to_other_config(session.uuid, _vlan != null ? _vlan : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _vlan, string _key)
    {
      session.proxy.vlan_remove_from_other_config(session.uuid, _vlan != null ? _vlan : "", _key != null ? _key : "").parse();
    }

    public static XenRef<VLAN> create(Session session, string _tagged_pif, long _tag, string _network)
    {
      return XenRef<VLAN>.Create(session.proxy.vlan_create(session.uuid, _tagged_pif != null ? _tagged_pif : "", _tag.ToString(), _network != null ? _network : "").parse());
    }

    public static XenRef<Task> async_create(Session session, string _tagged_pif, long _tag, string _network)
    {
      return XenRef<Task>.Create(session.proxy.async_vlan_create(session.uuid, _tagged_pif != null ? _tagged_pif : "", _tag.ToString(), _network != null ? _network : "").parse());
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.vlan_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vlan_destroy(session.uuid, _self != null ? _self : "").parse());
    }

    public static List<XenRef<VLAN>> get_all(Session session)
    {
      return XenRef<VLAN>.Create(session.proxy.vlan_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<VLAN>, VLAN> get_all_records(Session session)
    {
      return XenRef<VLAN>.Create<Proxy_VLAN>(session.proxy.vlan_get_all_records(session.uuid).parse());
    }
  }
}
