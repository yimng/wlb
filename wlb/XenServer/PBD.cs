﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.PBD
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class PBD : XenObject<PBD>
  {
    private string _uuid;
    private XenRef<Host> _host;
    private XenRef<XenAPI.SR> _SR;
    private Dictionary<string, string> _device_config;
    private bool _currently_attached;
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

    public XenRef<XenAPI.SR> SR
    {
      get
      {
        return this._SR;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._SR))
          return;
        this._SR = value;
        this.Changed = true;
        this.NotifyPropertyChanged("SR");
      }
    }

    public Dictionary<string, string> device_config
    {
      get
      {
        return this._device_config;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._device_config))
          return;
        this._device_config = value;
        this.Changed = true;
        this.NotifyPropertyChanged("device_config");
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

    public PBD()
    {
    }

    public PBD(string uuid, XenRef<Host> host, XenRef<XenAPI.SR> SR, Dictionary<string, string> device_config, bool currently_attached, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.host = host;
      this.SR = SR;
      this.device_config = device_config;
      this.currently_attached = currently_attached;
      this.other_config = other_config;
    }

    public PBD(Proxy_PBD proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public PBD(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.host = Marshalling.ParseRef<Host>(table, "host");
      this.SR = Marshalling.ParseRef<XenAPI.SR>(table, "SR");
      this.device_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "device_config"));
      this.currently_attached = Marshalling.ParseBool(table, "currently_attached");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(PBD update)
    {
      this.uuid = update.uuid;
      this.host = update.host;
      this.SR = update.SR;
      this.device_config = update.device_config;
      this.currently_attached = update.currently_attached;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_PBD proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.host = proxy.host == null ? (XenRef<Host>) null : XenRef<Host>.Create(proxy.host);
      this.SR = proxy.SR == null ? (XenRef<XenAPI.SR>) null : XenRef<XenAPI.SR>.Create(proxy.SR);
      this.device_config = proxy.device_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.device_config);
      this.currently_attached = proxy.currently_attached;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_PBD ToProxy()
    {
      return new Proxy_PBD()
      {
        uuid = this.uuid != null ? this.uuid : "",
        host = this.host != null ? (string) this.host : "",
        SR = this.SR != null ? (string) this.SR : "",
        device_config = (object) Maps.convert_to_proxy_string_string(this.device_config),
        currently_attached = this.currently_attached,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(PBD other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<XenRef<Host>>(this._host, other._host) && (Helper.AreEqual2<XenRef<XenAPI.SR>>(this._SR, other._SR) && Helper.AreEqual2<Dictionary<string, string>>(this._device_config, other._device_config)) && Helper.AreEqual2<bool>(this._currently_attached, other._currently_attached))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, PBD server)
    {
      if (opaqueRef == null)
      {
        Proxy_PBD _record = this.ToProxy();
        return session.proxy.pbd_create(session.uuid, _record).parse();
      }
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        PBD.set_other_config(session, opaqueRef, this._other_config);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._device_config, server._device_config))
        PBD.set_device_config(session, opaqueRef, this._device_config);
      return (string) null;
    }

    public static PBD get_record(Session session, string _pbd)
    {
      return new PBD(session.proxy.pbd_get_record(session.uuid, _pbd != null ? _pbd : "").parse());
    }

    public static XenRef<PBD> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<PBD>.Create(session.proxy.pbd_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<PBD> create(Session session, PBD _record)
    {
      return XenRef<PBD>.Create(session.proxy.pbd_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, PBD _record)
    {
      return XenRef<Task>.Create(session.proxy.async_pbd_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _pbd)
    {
      session.proxy.pbd_destroy(session.uuid, _pbd != null ? _pbd : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _pbd)
    {
      return XenRef<Task>.Create(session.proxy.async_pbd_destroy(session.uuid, _pbd != null ? _pbd : "").parse());
    }

    public static string get_uuid(Session session, string _pbd)
    {
      return session.proxy.pbd_get_uuid(session.uuid, _pbd != null ? _pbd : "").parse();
    }

    public static XenRef<Host> get_host(Session session, string _pbd)
    {
      return XenRef<Host>.Create(session.proxy.pbd_get_host(session.uuid, _pbd != null ? _pbd : "").parse());
    }

    public static XenRef<XenAPI.SR> get_SR(Session session, string _pbd)
    {
      return XenRef<XenAPI.SR>.Create(session.proxy.pbd_get_sr(session.uuid, _pbd != null ? _pbd : "").parse());
    }

    public static Dictionary<string, string> get_device_config(Session session, string _pbd)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pbd_get_device_config(session.uuid, _pbd != null ? _pbd : "").parse());
    }

    public static bool get_currently_attached(Session session, string _pbd)
    {
      return session.proxy.pbd_get_currently_attached(session.uuid, _pbd != null ? _pbd : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _pbd)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pbd_get_other_config(session.uuid, _pbd != null ? _pbd : "").parse());
    }

    public static void set_other_config(Session session, string _pbd, Dictionary<string, string> _other_config)
    {
      session.proxy.pbd_set_other_config(session.uuid, _pbd != null ? _pbd : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _pbd, string _key, string _value)
    {
      session.proxy.pbd_add_to_other_config(session.uuid, _pbd != null ? _pbd : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _pbd, string _key)
    {
      session.proxy.pbd_remove_from_other_config(session.uuid, _pbd != null ? _pbd : "", _key != null ? _key : "").parse();
    }

    public static void plug(Session session, string _self)
    {
      session.proxy.pbd_plug(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_plug(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pbd_plug(session.uuid, _self != null ? _self : "").parse());
    }

    public static void unplug(Session session, string _self)
    {
      session.proxy.pbd_unplug(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_unplug(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pbd_unplug(session.uuid, _self != null ? _self : "").parse());
    }

    public static void set_device_config(Session session, string _self, Dictionary<string, string> _value)
    {
      session.proxy.pbd_set_device_config(session.uuid, _self != null ? _self : "", (object) Maps.convert_to_proxy_string_string(_value)).parse();
    }

    public static XenRef<Task> async_set_device_config(Session session, string _self, Dictionary<string, string> _value)
    {
      return XenRef<Task>.Create(session.proxy.async_pbd_set_device_config(session.uuid, _self != null ? _self : "", (object) Maps.convert_to_proxy_string_string(_value)).parse());
    }

    public static List<XenRef<PBD>> get_all(Session session)
    {
      return XenRef<PBD>.Create(session.proxy.pbd_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<PBD>, PBD> get_all_records(Session session)
    {
      return XenRef<PBD>.Create<Proxy_PBD>(session.proxy.pbd_get_all_records(session.uuid).parse());
    }
  }
}
