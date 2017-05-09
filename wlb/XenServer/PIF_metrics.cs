// Decompiled with JetBrains decompiler
// Type: XenAPI.PIF_metrics
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class PIF_metrics : XenObject<PIF_metrics>
  {
    private string _uuid;
    private double _io_read_kbs;
    private double _io_write_kbs;
    private bool _carrier;
    private string _vendor_id;
    private string _vendor_name;
    private string _device_id;
    private string _device_name;
    private long _speed;
    private bool _duplex;
    private string _pci_bus_path;
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

    public bool carrier
    {
      get
      {
        return this._carrier;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._carrier ? 1 : 0)))
          return;
        this._carrier = value;
        this.Changed = true;
        this.NotifyPropertyChanged("carrier");
      }
    }

    public string vendor_id
    {
      get
      {
        return this._vendor_id;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._vendor_id))
          return;
        this._vendor_id = value;
        this.Changed = true;
        this.NotifyPropertyChanged("vendor_id");
      }
    }

    public string vendor_name
    {
      get
      {
        return this._vendor_name;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._vendor_name))
          return;
        this._vendor_name = value;
        this.Changed = true;
        this.NotifyPropertyChanged("vendor_name");
      }
    }

    public string device_id
    {
      get
      {
        return this._device_id;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._device_id))
          return;
        this._device_id = value;
        this.Changed = true;
        this.NotifyPropertyChanged("device_id");
      }
    }

    public string device_name
    {
      get
      {
        return this._device_name;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._device_name))
          return;
        this._device_name = value;
        this.Changed = true;
        this.NotifyPropertyChanged("device_name");
      }
    }

    public long speed
    {
      get
      {
        return this._speed;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._speed))
          return;
        this._speed = value;
        this.Changed = true;
        this.NotifyPropertyChanged("speed");
      }
    }

    public bool duplex
    {
      get
      {
        return this._duplex;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._duplex ? 1 : 0)))
          return;
        this._duplex = value;
        this.Changed = true;
        this.NotifyPropertyChanged("duplex");
      }
    }

    public string pci_bus_path
    {
      get
      {
        return this._pci_bus_path;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._pci_bus_path))
          return;
        this._pci_bus_path = value;
        this.Changed = true;
        this.NotifyPropertyChanged("pci_bus_path");
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

    public PIF_metrics()
    {
    }

    public PIF_metrics(string uuid, double io_read_kbs, double io_write_kbs, bool carrier, string vendor_id, string vendor_name, string device_id, string device_name, long speed, bool duplex, string pci_bus_path, DateTime last_updated, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.io_read_kbs = io_read_kbs;
      this.io_write_kbs = io_write_kbs;
      this.carrier = carrier;
      this.vendor_id = vendor_id;
      this.vendor_name = vendor_name;
      this.device_id = device_id;
      this.device_name = device_name;
      this.speed = speed;
      this.duplex = duplex;
      this.pci_bus_path = pci_bus_path;
      this.last_updated = last_updated;
      this.other_config = other_config;
    }

    public PIF_metrics(Proxy_PIF_metrics proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public PIF_metrics(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.io_read_kbs = Marshalling.ParseDouble(table, "io_read_kbs");
      this.io_write_kbs = Marshalling.ParseDouble(table, "io_write_kbs");
      this.carrier = Marshalling.ParseBool(table, "carrier");
      this.vendor_id = Marshalling.ParseString(table, "vendor_id");
      this.vendor_name = Marshalling.ParseString(table, "vendor_name");
      this.device_id = Marshalling.ParseString(table, "device_id");
      this.device_name = Marshalling.ParseString(table, "device_name");
      this.speed = Marshalling.ParseLong(table, "speed");
      this.duplex = Marshalling.ParseBool(table, "duplex");
      this.pci_bus_path = Marshalling.ParseString(table, "pci_bus_path");
      this.last_updated = Marshalling.ParseDateTime(table, "last_updated");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(PIF_metrics update)
    {
      this.uuid = update.uuid;
      this.io_read_kbs = update.io_read_kbs;
      this.io_write_kbs = update.io_write_kbs;
      this.carrier = update.carrier;
      this.vendor_id = update.vendor_id;
      this.vendor_name = update.vendor_name;
      this.device_id = update.device_id;
      this.device_name = update.device_name;
      this.speed = update.speed;
      this.duplex = update.duplex;
      this.pci_bus_path = update.pci_bus_path;
      this.last_updated = update.last_updated;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_PIF_metrics proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.io_read_kbs = Convert.ToDouble(proxy.io_read_kbs);
      this.io_write_kbs = Convert.ToDouble(proxy.io_write_kbs);
      this.carrier = proxy.carrier;
      this.vendor_id = proxy.vendor_id == null ? (string) null : proxy.vendor_id;
      this.vendor_name = proxy.vendor_name == null ? (string) null : proxy.vendor_name;
      this.device_id = proxy.device_id == null ? (string) null : proxy.device_id;
      this.device_name = proxy.device_name == null ? (string) null : proxy.device_name;
      this.speed = proxy.speed == null ? 0L : long.Parse(proxy.speed);
      this.duplex = proxy.duplex;
      this.pci_bus_path = proxy.pci_bus_path == null ? (string) null : proxy.pci_bus_path;
      this.last_updated = proxy.last_updated;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_PIF_metrics ToProxy()
    {
      return new Proxy_PIF_metrics()
      {
        uuid = this.uuid != null ? this.uuid : "",
        io_read_kbs = this.io_read_kbs,
        io_write_kbs = this.io_write_kbs,
        carrier = this.carrier,
        vendor_id = this.vendor_id != null ? this.vendor_id : "",
        vendor_name = this.vendor_name != null ? this.vendor_name : "",
        device_id = this.device_id != null ? this.device_id : "",
        device_name = this.device_name != null ? this.device_name : "",
        speed = this.speed.ToString(),
        duplex = this.duplex,
        pci_bus_path = this.pci_bus_path != null ? this.pci_bus_path : "",
        last_updated = this.last_updated,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(PIF_metrics other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<double>(this._io_read_kbs, other._io_read_kbs) && (Helper.AreEqual2<double>(this._io_write_kbs, other._io_write_kbs) && Helper.AreEqual2<bool>(this._carrier, other._carrier)) && (Helper.AreEqual2<string>(this._vendor_id, other._vendor_id) && Helper.AreEqual2<string>(this._vendor_name, other._vendor_name) && (Helper.AreEqual2<string>(this._device_id, other._device_id) && Helper.AreEqual2<string>(this._device_name, other._device_name))) && (Helper.AreEqual2<long>(this._speed, other._speed) && Helper.AreEqual2<bool>(this._duplex, other._duplex) && (Helper.AreEqual2<string>(this._pci_bus_path, other._pci_bus_path) && Helper.AreEqual2<DateTime>(this._last_updated, other._last_updated))))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, PIF_metrics server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        PIF_metrics.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static PIF_metrics get_record(Session session, string _pif_metrics)
    {
      return new PIF_metrics(session.proxy.pif_metrics_get_record(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse());
    }

    public static XenRef<PIF_metrics> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<PIF_metrics>.Create(session.proxy.pif_metrics_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_uuid(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static double get_io_read_kbs(Session session, string _pif_metrics)
    {
      return Convert.ToDouble(session.proxy.pif_metrics_get_io_read_kbs(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse());
    }

    public static double get_io_write_kbs(Session session, string _pif_metrics)
    {
      return Convert.ToDouble(session.proxy.pif_metrics_get_io_write_kbs(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse());
    }

    public static bool get_carrier(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_carrier(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static string get_vendor_id(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_vendor_id(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static string get_vendor_name(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_vendor_name(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static string get_device_id(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_device_id(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static string get_device_name(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_device_name(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static long get_speed(Session session, string _pif_metrics)
    {
      return long.Parse(session.proxy.pif_metrics_get_speed(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse());
    }

    public static bool get_duplex(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_duplex(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static string get_pci_bus_path(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_pci_bus_path(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static DateTime get_last_updated(Session session, string _pif_metrics)
    {
      return session.proxy.pif_metrics_get_last_updated(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _pif_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pif_metrics_get_other_config(session.uuid, _pif_metrics != null ? _pif_metrics : "").parse());
    }

    public static void set_other_config(Session session, string _pif_metrics, Dictionary<string, string> _other_config)
    {
      session.proxy.pif_metrics_set_other_config(session.uuid, _pif_metrics != null ? _pif_metrics : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _pif_metrics, string _key, string _value)
    {
      session.proxy.pif_metrics_add_to_other_config(session.uuid, _pif_metrics != null ? _pif_metrics : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _pif_metrics, string _key)
    {
      session.proxy.pif_metrics_remove_from_other_config(session.uuid, _pif_metrics != null ? _pif_metrics : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<PIF_metrics>> get_all(Session session)
    {
      return XenRef<PIF_metrics>.Create(session.proxy.pif_metrics_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<PIF_metrics>, PIF_metrics> get_all_records(Session session)
    {
      return XenRef<PIF_metrics>.Create<Proxy_PIF_metrics>(session.proxy.pif_metrics_get_all_records(session.uuid).parse());
    }
  }
}
