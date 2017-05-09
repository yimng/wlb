// Decompiled with JetBrains decompiler
// Type: XenAPI.VM_guest_metrics
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class VM_guest_metrics : XenObject<VM_guest_metrics>
  {
    private string _uuid;
    private Dictionary<string, string> _os_version;
    private Dictionary<string, string> _PV_drivers_version;
    private bool _PV_drivers_up_to_date;
    private Dictionary<string, string> _memory;
    private Dictionary<string, string> _disks;
    private Dictionary<string, string> _networks;
    private Dictionary<string, string> _other;
    private DateTime _last_updated;
    private Dictionary<string, string> _other_config;
    private bool _live;

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

    public Dictionary<string, string> os_version
    {
      get
      {
        return this._os_version;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._os_version))
          return;
        this._os_version = value;
        this.Changed = true;
        this.NotifyPropertyChanged("os_version");
      }
    }

    public Dictionary<string, string> PV_drivers_version
    {
      get
      {
        return this._PV_drivers_version;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PV_drivers_version))
          return;
        this._PV_drivers_version = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PV_drivers_version");
      }
    }

    public bool PV_drivers_up_to_date
    {
      get
      {
        return this._PV_drivers_up_to_date;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._PV_drivers_up_to_date ? 1 : 0)))
          return;
        this._PV_drivers_up_to_date = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PV_drivers_up_to_date");
      }
    }

    public Dictionary<string, string> memory
    {
      get
      {
        return this._memory;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory))
          return;
        this._memory = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory");
      }
    }

    public Dictionary<string, string> disks
    {
      get
      {
        return this._disks;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._disks))
          return;
        this._disks = value;
        this.Changed = true;
        this.NotifyPropertyChanged("disks");
      }
    }

    public Dictionary<string, string> networks
    {
      get
      {
        return this._networks;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._networks))
          return;
        this._networks = value;
        this.Changed = true;
        this.NotifyPropertyChanged("networks");
      }
    }

    public Dictionary<string, string> other
    {
      get
      {
        return this._other;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._other))
          return;
        this._other = value;
        this.Changed = true;
        this.NotifyPropertyChanged("other");
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

    public VM_guest_metrics()
    {
    }

    public VM_guest_metrics(string uuid, Dictionary<string, string> os_version, Dictionary<string, string> PV_drivers_version, bool PV_drivers_up_to_date, Dictionary<string, string> memory, Dictionary<string, string> disks, Dictionary<string, string> networks, Dictionary<string, string> other, DateTime last_updated, Dictionary<string, string> other_config, bool live)
    {
      this.uuid = uuid;
      this.os_version = os_version;
      this.PV_drivers_version = PV_drivers_version;
      this.PV_drivers_up_to_date = PV_drivers_up_to_date;
      this.memory = memory;
      this.disks = disks;
      this.networks = networks;
      this.other = other;
      this.last_updated = last_updated;
      this.other_config = other_config;
      this.live = live;
    }

    public VM_guest_metrics(Proxy_VM_guest_metrics proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public VM_guest_metrics(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.os_version = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "os_version"));
      this.PV_drivers_version = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "PV_drivers_version"));
      this.PV_drivers_up_to_date = Marshalling.ParseBool(table, "PV_drivers_up_to_date");
      this.memory = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "memory"));
      this.disks = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "disks"));
      this.networks = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "networks"));
      this.other = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other"));
      this.last_updated = Marshalling.ParseDateTime(table, "last_updated");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
      this.live = Marshalling.ParseBool(table, "live");
    }

    public override void UpdateFrom(VM_guest_metrics update)
    {
      this.uuid = update.uuid;
      this.os_version = update.os_version;
      this.PV_drivers_version = update.PV_drivers_version;
      this.PV_drivers_up_to_date = update.PV_drivers_up_to_date;
      this.memory = update.memory;
      this.disks = update.disks;
      this.networks = update.networks;
      this.other = update.other;
      this.last_updated = update.last_updated;
      this.other_config = update.other_config;
      this.live = update.live;
    }

    internal void UpdateFromProxy(Proxy_VM_guest_metrics proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.os_version = proxy.os_version == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.os_version);
      this.PV_drivers_version = proxy.PV_drivers_version == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.PV_drivers_version);
      this.PV_drivers_up_to_date = proxy.PV_drivers_up_to_date;
      this.memory = proxy.memory == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.memory);
      this.disks = proxy.disks == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.disks);
      this.networks = proxy.networks == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.networks);
      this.other = proxy.other == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other);
      this.last_updated = proxy.last_updated;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
      this.live = proxy.live;
    }

    public Proxy_VM_guest_metrics ToProxy()
    {
      return new Proxy_VM_guest_metrics()
      {
        uuid = this.uuid != null ? this.uuid : "",
        os_version = (object) Maps.convert_to_proxy_string_string(this.os_version),
        PV_drivers_version = (object) Maps.convert_to_proxy_string_string(this.PV_drivers_version),
        PV_drivers_up_to_date = this.PV_drivers_up_to_date,
        memory = (object) Maps.convert_to_proxy_string_string(this.memory),
        disks = (object) Maps.convert_to_proxy_string_string(this.disks),
        networks = (object) Maps.convert_to_proxy_string_string(this.networks),
        other = (object) Maps.convert_to_proxy_string_string(this.other),
        last_updated = this.last_updated,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config),
        live = this.live
      };
    }

    public bool DeepEquals(VM_guest_metrics other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<Dictionary<string, string>>(this._os_version, other._os_version) && (Helper.AreEqual2<Dictionary<string, string>>(this._PV_drivers_version, other._PV_drivers_version) && Helper.AreEqual2<bool>(this._PV_drivers_up_to_date, other._PV_drivers_up_to_date)) && (Helper.AreEqual2<Dictionary<string, string>>(this._memory, other._memory) && Helper.AreEqual2<Dictionary<string, string>>(this._disks, other._disks) && (Helper.AreEqual2<Dictionary<string, string>>(this._networks, other._networks) && Helper.AreEqual2<Dictionary<string, string>>(this._other, other._other))) && (Helper.AreEqual2<DateTime>(this._last_updated, other._last_updated) && Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config)))
        return Helper.AreEqual2<bool>(this._live, other._live);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, VM_guest_metrics server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        VM_guest_metrics.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static VM_guest_metrics get_record(Session session, string _vm_guest_metrics)
    {
      return new VM_guest_metrics(session.proxy.vm_guest_metrics_get_record(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse());
    }

    public static XenRef<VM_guest_metrics> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<VM_guest_metrics>.Create(session.proxy.vm_guest_metrics_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _vm_guest_metrics)
    {
      return session.proxy.vm_guest_metrics_get_uuid(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse();
    }

    public static Dictionary<string, string> get_os_version(Session session, string _vm_guest_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_guest_metrics_get_os_version(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse());
    }

    public static Dictionary<string, string> get_PV_drivers_version(Session session, string _vm_guest_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_guest_metrics_get_pv_drivers_version(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse());
    }

    public static bool get_PV_drivers_up_to_date(Session session, string _vm_guest_metrics)
    {
      return session.proxy.vm_guest_metrics_get_pv_drivers_up_to_date(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse();
    }

    public static Dictionary<string, string> get_memory(Session session, string _vm_guest_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_guest_metrics_get_memory(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse());
    }

    public static Dictionary<string, string> get_disks(Session session, string _vm_guest_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_guest_metrics_get_disks(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse());
    }

    public static Dictionary<string, string> get_networks(Session session, string _vm_guest_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_guest_metrics_get_networks(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse());
    }

    public static Dictionary<string, string> get_other(Session session, string _vm_guest_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_guest_metrics_get_other(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse());
    }

    public static DateTime get_last_updated(Session session, string _vm_guest_metrics)
    {
      return session.proxy.vm_guest_metrics_get_last_updated(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _vm_guest_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_guest_metrics_get_other_config(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse());
    }

    public static bool get_live(Session session, string _vm_guest_metrics)
    {
      return session.proxy.vm_guest_metrics_get_live(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "").parse();
    }

    public static void set_other_config(Session session, string _vm_guest_metrics, Dictionary<string, string> _other_config)
    {
      session.proxy.vm_guest_metrics_set_other_config(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _vm_guest_metrics, string _key, string _value)
    {
      session.proxy.vm_guest_metrics_add_to_other_config(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _vm_guest_metrics, string _key)
    {
      session.proxy.vm_guest_metrics_remove_from_other_config(session.uuid, _vm_guest_metrics != null ? _vm_guest_metrics : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<VM_guest_metrics>> get_all(Session session)
    {
      return XenRef<VM_guest_metrics>.Create(session.proxy.vm_guest_metrics_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<VM_guest_metrics>, VM_guest_metrics> get_all_records(Session session)
    {
      return XenRef<VM_guest_metrics>.Create<Proxy_VM_guest_metrics>(session.proxy.vm_guest_metrics_get_all_records(session.uuid).parse());
    }
  }
}
