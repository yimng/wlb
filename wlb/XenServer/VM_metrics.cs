// Decompiled with JetBrains decompiler
// Type: XenAPI.VM_metrics
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class VM_metrics : XenObject<VM_metrics>
  {
    private string _uuid;
    private long _memory_actual;
    private long _VCPUs_number;
    private Dictionary<long, double> _VCPUs_utilisation;
    private Dictionary<long, long> _VCPUs_CPU;
    private Dictionary<string, string> _VCPUs_params;
    private Dictionary<long, string[]> _VCPUs_flags;
    private string[] _state;
    private DateTime _start_time;
    private DateTime _install_time;
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

    public long memory_actual
    {
      get
      {
        return this._memory_actual;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._memory_actual))
          return;
        this._memory_actual = value;
        this.Changed = true;
        this.NotifyPropertyChanged("memory_actual");
      }
    }

    public long VCPUs_number
    {
      get
      {
        return this._VCPUs_number;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VCPUs_number))
          return;
        this._VCPUs_number = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VCPUs_number");
      }
    }

    public Dictionary<long, double> VCPUs_utilisation
    {
      get
      {
        return this._VCPUs_utilisation;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VCPUs_utilisation))
          return;
        this._VCPUs_utilisation = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VCPUs_utilisation");
      }
    }

    public Dictionary<long, long> VCPUs_CPU
    {
      get
      {
        return this._VCPUs_CPU;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VCPUs_CPU))
          return;
        this._VCPUs_CPU = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VCPUs_CPU");
      }
    }

    public Dictionary<string, string> VCPUs_params
    {
      get
      {
        return this._VCPUs_params;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VCPUs_params))
          return;
        this._VCPUs_params = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VCPUs_params");
      }
    }

    public Dictionary<long, string[]> VCPUs_flags
    {
      get
      {
        return this._VCPUs_flags;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VCPUs_flags))
          return;
        this._VCPUs_flags = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VCPUs_flags");
      }
    }

    public string[] state
    {
      get
      {
        return this._state;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._state))
          return;
        this._state = value;
        this.Changed = true;
        this.NotifyPropertyChanged("state");
      }
    }

    public DateTime start_time
    {
      get
      {
        return this._start_time;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._start_time))
          return;
        this._start_time = value;
        this.Changed = true;
        this.NotifyPropertyChanged("start_time");
      }
    }

    public DateTime install_time
    {
      get
      {
        return this._install_time;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._install_time))
          return;
        this._install_time = value;
        this.Changed = true;
        this.NotifyPropertyChanged("install_time");
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

    public VM_metrics()
    {
    }

    public VM_metrics(string uuid, long memory_actual, long VCPUs_number, Dictionary<long, double> VCPUs_utilisation, Dictionary<long, long> VCPUs_CPU, Dictionary<string, string> VCPUs_params, Dictionary<long, string[]> VCPUs_flags, string[] state, DateTime start_time, DateTime install_time, DateTime last_updated, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.memory_actual = memory_actual;
      this.VCPUs_number = VCPUs_number;
      this.VCPUs_utilisation = VCPUs_utilisation;
      this.VCPUs_CPU = VCPUs_CPU;
      this.VCPUs_params = VCPUs_params;
      this.VCPUs_flags = VCPUs_flags;
      this.state = state;
      this.start_time = start_time;
      this.install_time = install_time;
      this.last_updated = last_updated;
      this.other_config = other_config;
    }

    public VM_metrics(Proxy_VM_metrics proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public VM_metrics(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.memory_actual = Marshalling.ParseLong(table, "memory_actual");
      this.VCPUs_number = Marshalling.ParseLong(table, "VCPUs_number");
      this.VCPUs_utilisation = Maps.convert_from_proxy_long_double((object) Marshalling.ParseHashTable(table, "VCPUs_utilisation"));
      this.VCPUs_CPU = Maps.convert_from_proxy_long_long((object) Marshalling.ParseHashTable(table, "VCPUs_CPU"));
      this.VCPUs_params = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "VCPUs_params"));
      this.VCPUs_flags = Maps.convert_from_proxy_long_string_array((object) Marshalling.ParseHashTable(table, "VCPUs_flags"));
      this.state = Marshalling.ParseStringArray(table, "state");
      this.start_time = Marshalling.ParseDateTime(table, "start_time");
      this.install_time = Marshalling.ParseDateTime(table, "install_time");
      this.last_updated = Marshalling.ParseDateTime(table, "last_updated");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(VM_metrics update)
    {
      this.uuid = update.uuid;
      this.memory_actual = update.memory_actual;
      this.VCPUs_number = update.VCPUs_number;
      this.VCPUs_utilisation = update.VCPUs_utilisation;
      this.VCPUs_CPU = update.VCPUs_CPU;
      this.VCPUs_params = update.VCPUs_params;
      this.VCPUs_flags = update.VCPUs_flags;
      this.state = update.state;
      this.start_time = update.start_time;
      this.install_time = update.install_time;
      this.last_updated = update.last_updated;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_VM_metrics proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.memory_actual = proxy.memory_actual == null ? 0L : long.Parse(proxy.memory_actual);
      this.VCPUs_number = proxy.VCPUs_number == null ? 0L : long.Parse(proxy.VCPUs_number);
      this.VCPUs_utilisation = proxy.VCPUs_utilisation == null ? (Dictionary<long, double>) null : Maps.convert_from_proxy_long_double(proxy.VCPUs_utilisation);
      this.VCPUs_CPU = proxy.VCPUs_CPU == null ? (Dictionary<long, long>) null : Maps.convert_from_proxy_long_long(proxy.VCPUs_CPU);
      this.VCPUs_params = proxy.VCPUs_params == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.VCPUs_params);
      this.VCPUs_flags = proxy.VCPUs_flags == null ? (Dictionary<long, string[]>) null : Maps.convert_from_proxy_long_string_array(proxy.VCPUs_flags);
      this.state = proxy.state == null ? new string[0] : proxy.state;
      this.start_time = proxy.start_time;
      this.install_time = proxy.install_time;
      this.last_updated = proxy.last_updated;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_VM_metrics ToProxy()
    {
      return new Proxy_VM_metrics()
      {
        uuid = this.uuid != null ? this.uuid : "",
        memory_actual = this.memory_actual.ToString(),
        VCPUs_number = this.VCPUs_number.ToString(),
        VCPUs_utilisation = (object) Maps.convert_to_proxy_long_double(this.VCPUs_utilisation),
        VCPUs_CPU = (object) Maps.convert_to_proxy_long_long(this.VCPUs_CPU),
        VCPUs_params = (object) Maps.convert_to_proxy_string_string(this.VCPUs_params),
        VCPUs_flags = (object) Maps.convert_to_proxy_long_string_array(this.VCPUs_flags),
        state = this.state,
        start_time = this.start_time,
        install_time = this.install_time,
        last_updated = this.last_updated,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(VM_metrics other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<long>(this._memory_actual, other._memory_actual) && (Helper.AreEqual2<long>(this._VCPUs_number, other._VCPUs_number) && Helper.AreEqual2<Dictionary<long, double>>(this._VCPUs_utilisation, other._VCPUs_utilisation)) && (Helper.AreEqual2<Dictionary<long, long>>(this._VCPUs_CPU, other._VCPUs_CPU) && Helper.AreEqual2<Dictionary<string, string>>(this._VCPUs_params, other._VCPUs_params) && (Helper.AreEqual2<Dictionary<long, string[]>>(this._VCPUs_flags, other._VCPUs_flags) && Helper.AreEqual2<string[]>(this._state, other._state))) && (Helper.AreEqual2<DateTime>(this._start_time, other._start_time) && Helper.AreEqual2<DateTime>(this._install_time, other._install_time) && Helper.AreEqual2<DateTime>(this._last_updated, other._last_updated)))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, VM_metrics server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        VM_metrics.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static VM_metrics get_record(Session session, string _vm_metrics)
    {
      return new VM_metrics(session.proxy.vm_metrics_get_record(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse());
    }

    public static XenRef<VM_metrics> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<VM_metrics>.Create(session.proxy.vm_metrics_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _vm_metrics)
    {
      return session.proxy.vm_metrics_get_uuid(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse();
    }

    public static long get_memory_actual(Session session, string _vm_metrics)
    {
      return long.Parse(session.proxy.vm_metrics_get_memory_actual(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse());
    }

    public static long get_VCPUs_number(Session session, string _vm_metrics)
    {
      return long.Parse(session.proxy.vm_metrics_get_vcpus_number(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse());
    }

    public static Dictionary<long, double> get_VCPUs_utilisation(Session session, string _vm_metrics)
    {
      return Maps.convert_from_proxy_long_double(session.proxy.vm_metrics_get_vcpus_utilisation(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse());
    }

    public static Dictionary<long, long> get_VCPUs_CPU(Session session, string _vm_metrics)
    {
      return Maps.convert_from_proxy_long_long(session.proxy.vm_metrics_get_vcpus_cpu(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse());
    }

    public static Dictionary<string, string> get_VCPUs_params(Session session, string _vm_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_metrics_get_vcpus_params(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse());
    }

    public static Dictionary<long, string[]> get_VCPUs_flags(Session session, string _vm_metrics)
    {
      return Maps.convert_from_proxy_long_string_array(session.proxy.vm_metrics_get_vcpus_flags(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse());
    }

    public static string[] get_state(Session session, string _vm_metrics)
    {
      return session.proxy.vm_metrics_get_state(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse();
    }

    public static DateTime get_start_time(Session session, string _vm_metrics)
    {
      return session.proxy.vm_metrics_get_start_time(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse();
    }

    public static DateTime get_install_time(Session session, string _vm_metrics)
    {
      return session.proxy.vm_metrics_get_install_time(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse();
    }

    public static DateTime get_last_updated(Session session, string _vm_metrics)
    {
      return session.proxy.vm_metrics_get_last_updated(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _vm_metrics)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vm_metrics_get_other_config(session.uuid, _vm_metrics != null ? _vm_metrics : "").parse());
    }

    public static void set_other_config(Session session, string _vm_metrics, Dictionary<string, string> _other_config)
    {
      session.proxy.vm_metrics_set_other_config(session.uuid, _vm_metrics != null ? _vm_metrics : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _vm_metrics, string _key, string _value)
    {
      session.proxy.vm_metrics_add_to_other_config(session.uuid, _vm_metrics != null ? _vm_metrics : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _vm_metrics, string _key)
    {
      session.proxy.vm_metrics_remove_from_other_config(session.uuid, _vm_metrics != null ? _vm_metrics : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<VM_metrics>> get_all(Session session)
    {
      return XenRef<VM_metrics>.Create(session.proxy.vm_metrics_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<VM_metrics>, VM_metrics> get_all_records(Session session)
    {
      return XenRef<VM_metrics>.Create<Proxy_VM_metrics>(session.proxy.vm_metrics_get_all_records(session.uuid).parse());
    }
  }
}
