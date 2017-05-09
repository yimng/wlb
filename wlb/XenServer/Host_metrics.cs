// Decompiled with JetBrains decompiler
// Type: XenAPI.Host_metrics
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Host_metrics : XenObject<Host_metrics>
  {
    private string _uuid;
    private long _memory_total;
    private long _memory_free;
    private bool _live;
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

    public long memory_total
    {
      get
      {
        return this._memory_total;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_total))
          return;
        this._memory_total = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_total");
      }
    }

    public long memory_free
    {
      get
      {
        return this._memory_free;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_free))
          return;
        this._memory_free = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_free");
      }
    }

    public bool live
    {
      get
      {
        return this._live;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._live ? 1 : 0)))
          return;
        this._live = value;
        this.Changed = true;
        this.NotifyPropertyChanged("live");
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

    public Host_metrics()
    {
    }

    public Host_metrics(string uuid, long memory_total, long memory_free, bool live, DateTime last_updated, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.memory_total = memory_total;
      this.memory_free = memory_free;
      this.live = live;
      this.last_updated = last_updated;
      this.other_config = other_config;
    }

    public Host_metrics(Proxy_Host_metrics proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Host_metrics(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.memory_total = Marshalling.ParseLong(table, "memory_total");
      this.memory_free = Marshalling.ParseLong(table, "memory_free");
      this.live = Marshalling.ParseBool(table, "live");
      this.last_updated = Marshalling.ParseDateTime(table, "last_updated");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(Host_metrics update)
    {
      this.uuid = update.uuid;
      this.memory_total = update.memory_total;
      this.memory_free = update.memory_free;
      this.live = update.live;
      this.last_updated = update.last_updated;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_Host_metrics proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.memory_total = proxy.memory_total == null ? 0L : long.Parse(proxy.memory_total);
      this.memory_free = proxy.memory_free == null ? 0L : long.Parse(proxy.memory_free);
      this.live = proxy.live;
      this.last_updated = proxy.last_updated;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_Host_metrics ToProxy()
    {
      return new Proxy_Host_metrics()
      {
        uuid = this.uuid != null ? this.uuid : "",
        memory_total = this.memory_total.ToString(),
        memory_free = this.memory_free.ToString(),
        live = this.live,
        last_updated = this.last_updated,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(Host_metrics other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<long>(this._memory_total, other._memory_total) && (Helper.AreEqual2<long>(this._memory_free, other._memory_free) && Helper.AreEqual2<bool>(this._live, other._live)) && Helper.AreEqual2<DateTime>(this._last_updated, other._last_updated))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Host_metrics server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Host_metrics.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static Host_metrics get_record(Session session, string _host_metrics)
    {
      return new Host_metrics(session.proxy.host_metrics_get_record(session.uuid, _host_metrics != null ? _host_metrics : "").parse());
    }

    public static XenRef<Host_metrics> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Host_metrics>.Create(session.proxy.host_metrics_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _host_metrics)
    {
      return session.proxy.host_metrics_get_uuid(session.uuid, _host_metrics != null ? _host_metrics : "").parse();
    }

    public static long get_memory_total(Session session, string _host_metrics)
    {
      return long.Parse(session.proxy.host_metrics_get_memory_total(session.uuid, _host_metrics != null ? _host_metrics : "").parse());
    }

    public static long get_memory_free(Session session, string _host_metrics)
    {
      return long.Parse(session.proxy.host_metrics_get_memory_free(session.uuid, _host_metrics != null ? _host_metrics : "").parse());
    }

    public static bool get_live(Session session, string _host_metrics)
    {
      return session.proxy.host_metrics_get_live(session.uuid, _host_metrics != null ? _host_metrics : "").parse();
    }

    public static DateTime get_last_updated(Session session, string _host_metrics)
    {
      return session.proxy.host_metrics_get_last_updated(session.uuid, _host_metrics != null ? _host_metrics : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _host_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_metrics_get_other_config(session.uuid, _host_metrics != null ? _host_metrics : "").parse());
    }

    public static void set_other_config(Session session, string _host_metrics, Dictionary<string, string> _other_config)
    {
      session.proxy.host_metrics_set_other_config(session.uuid, _host_metrics != null ? _host_metrics : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _host_metrics, string _key, string _value)
    {
      session.proxy.host_metrics_add_to_other_config(session.uuid, _host_metrics != null ? _host_metrics : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _host_metrics, string _key)
    {
      session.proxy.host_metrics_remove_from_other_config(session.uuid, _host_metrics != null ? _host_metrics : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<Host_metrics>> get_all(Session session)
    {
      return XenRef<Host_metrics>.Create(session.proxy.host_metrics_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Host_metrics>, Host_metrics> get_all_records(Session session)
    {
      return XenRef<Host_metrics>.Create<Proxy_Host_metrics>(session.proxy.host_metrics_get_all_records(session.uuid).parse());
    }
  }
}
