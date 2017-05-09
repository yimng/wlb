﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.VBD_metrics
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class VBD_metrics : XenObject<VBD_metrics>
  {
    private string _uuid;
    private double _io_read_kbs;
    private double _io_write_kbs;
    private DateTime _last_updated;
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

    public double io_read_kbs
    {
      get
      {
        return this._io_read_kbs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._io_read_kbs))
          return;
        this._io_read_kbs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("io_read_kbs");
      }
    }

    public double io_write_kbs
    {
      get
      {
        return this._io_write_kbs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._io_write_kbs))
          return;
        this._io_write_kbs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("io_write_kbs");
      }
    }

    public DateTime last_updated
    {
      get
      {
        return this._last_updated;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._last_updated))
          return;
        this._last_updated = value;
        this.Changed = true;
        this.NotifyPropertyChanged("last_updated");
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

    public VBD_metrics()
    {
    }

    public VBD_metrics(string uuid, double io_read_kbs, double io_write_kbs, DateTime last_updated, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.io_read_kbs = io_read_kbs;
      this.io_write_kbs = io_write_kbs;
      this.last_updated = last_updated;
      this.other_config = other_config;
    }

    public VBD_metrics(Proxy_VBD_metrics proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public VBD_metrics(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.io_read_kbs = Marshalling.ParseDouble(table, "io_read_kbs");
      this.io_write_kbs = Marshalling.ParseDouble(table, "io_write_kbs");
      this.last_updated = Marshalling.ParseDateTime(table, "last_updated");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(VBD_metrics update)
    {
      this.uuid = update.uuid;
      this.io_read_kbs = update.io_read_kbs;
      this.io_write_kbs = update.io_write_kbs;
      this.last_updated = update.last_updated;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_VBD_metrics proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.io_read_kbs = Convert.ToDouble(proxy.io_read_kbs);
      this.io_write_kbs = Convert.ToDouble(proxy.io_write_kbs);
      this.last_updated = proxy.last_updated;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_VBD_metrics ToProxy()
    {
      return new Proxy_VBD_metrics()
      {
        uuid = this.uuid != null ? this.uuid : "",
        io_read_kbs = this.io_read_kbs,
        io_write_kbs = this.io_write_kbs,
        last_updated = this.last_updated,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(VBD_metrics other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<double>(this._io_read_kbs, other._io_read_kbs) && (Helper.AreEqual2<double>(this._io_write_kbs, other._io_write_kbs) && Helper.AreEqual2<DateTime>(this._last_updated, other._last_updated)))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, VBD_metrics server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        VBD_metrics.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static VBD_metrics get_record(Session session, string _vbd_metrics)
    {
      return new VBD_metrics(session.proxy.vbd_metrics_get_record(session.uuid, _vbd_metrics != null ? _vbd_metrics : "").parse());
    }

    public static XenRef<VBD_metrics> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<VBD_metrics>.Create(session.proxy.vbd_metrics_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _vbd_metrics)
    {
      return session.proxy.vbd_metrics_get_uuid(session.uuid, _vbd_metrics != null ? _vbd_metrics : "").parse();
    }

    public static double get_io_read_kbs(Session session, string _vbd_metrics)
    {
      return Convert.ToDouble(session.proxy.vbd_metrics_get_io_read_kbs(session.uuid, _vbd_metrics != null ? _vbd_metrics : "").parse());
    }

    public static double get_io_write_kbs(Session session, string _vbd_metrics)
    {
      return Convert.ToDouble(session.proxy.vbd_metrics_get_io_write_kbs(session.uuid, _vbd_metrics != null ? _vbd_metrics : "").parse());
    }

    public static DateTime get_last_updated(Session session, string _vbd_metrics)
    {
      return session.proxy.vbd_metrics_get_last_updated(session.uuid, _vbd_metrics != null ? _vbd_metrics : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _vbd_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vbd_metrics_get_other_config(session.uuid, _vbd_metrics != null ? _vbd_metrics : "").parse());
    }

    public static void set_other_config(Session session, string _vbd_metrics, Dictionary<string, string> _other_config)
    {
      session.proxy.vbd_metrics_set_other_config(session.uuid, _vbd_metrics != null ? _vbd_metrics : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _vbd_metrics, string _key, string _value)
    {
      session.proxy.vbd_metrics_add_to_other_config(session.uuid, _vbd_metrics != null ? _vbd_metrics : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _vbd_metrics, string _key)
    {
      session.proxy.vbd_metrics_remove_from_other_config(session.uuid, _vbd_metrics != null ? _vbd_metrics : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<VBD_metrics>> get_all(Session session)
    {
      return XenRef<VBD_metrics>.Create(session.proxy.vbd_metrics_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<VBD_metrics>, VBD_metrics> get_all_records(Session session)
    {
      return XenRef<VBD_metrics>.Create<Proxy_VBD_metrics>(session.proxy.vbd_metrics_get_all_records(session.uuid).parse());
    }
  }
}
