// Decompiled with JetBrains decompiler
// Type: XenAPI.Host_cpu
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Host_cpu : XenObject<Host_cpu>
  {
    private string _uuid;
    private XenRef<Host> _host;
    private long _number;
    private string _vendor;
    private long _speed;
    private string _modelname;
    private long _family;
    private long _model;
    private string _stepping;
    private string _flags;
    private string _features;
    private double _utilisation;
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

    public long number
    {
      get
      {
        return this._number;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._number))
          return;
        this._number = value;
        this.Changed = true;
        this.NotifyPropertyChanged("number");
      }
    }

    public string vendor
    {
      get
      {
        return this._vendor;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._vendor))
          return;
        this._vendor = value;
        this.Changed = true;
        this.NotifyPropertyChanged("vendor");
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

    public string modelname
    {
      get
      {
        return this._modelname;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._modelname))
          return;
        this._modelname = value;
        this.Changed = true;
        this.NotifyPropertyChanged("modelname");
      }
    }

    public long family
    {
      get
      {
        return this._family;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._family))
          return;
        this._family = value;
        this.Changed = true;
        this.NotifyPropertyChanged("family");
      }
    }

    public long model
    {
      get
      {
        return this._model;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._model))
          return;
        this._model = value;
        this.Changed = true;
        this.NotifyPropertyChanged("model");
      }
    }

    public string stepping
    {
      get
      {
        return this._stepping;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._stepping))
          return;
        this._stepping = value;
        this.Changed = true;
        this.NotifyPropertyChanged("stepping");
      }
    }

    public string flags
    {
      get
      {
        return this._flags;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._flags))
          return;
        this._flags = value;
        this.Changed = true;
        this.NotifyPropertyChanged("flags");
      }
    }

    public string features
    {
      get
      {
        return this._features;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._features))
          return;
        this._features = value;
        this.Changed = true;
        this.NotifyPropertyChanged("features");
      }
    }

    public double utilisation
    {
      get
      {
        return this._utilisation;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._utilisation))
          return;
        this._utilisation = value;
        this.Changed = true;
        this.NotifyPropertyChanged("utilisation");
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

    public Host_cpu()
    {
    }

    public Host_cpu(string uuid, XenRef<Host> host, long number, string vendor, long speed, string modelname, long family, long model, string stepping, string flags, string features, double utilisation, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.host = host;
      this.number = number;
      this.vendor = vendor;
      this.speed = speed;
      this.modelname = modelname;
      this.family = family;
      this.model = model;
      this.stepping = stepping;
      this.flags = flags;
      this.features = features;
      this.utilisation = utilisation;
      this.other_config = other_config;
    }

    public Host_cpu(Proxy_Host_cpu proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Host_cpu(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.host = Marshalling.ParseRef<Host>(table, "host");
      this.number = Marshalling.ParseLong(table, "number");
      this.vendor = Marshalling.ParseString(table, "vendor");
      this.speed = Marshalling.ParseLong(table, "speed");
      this.modelname = Marshalling.ParseString(table, "modelname");
      this.family = Marshalling.ParseLong(table, "family");
      this.model = Marshalling.ParseLong(table, "model");
      this.stepping = Marshalling.ParseString(table, "stepping");
      this.flags = Marshalling.ParseString(table, "flags");
      this.features = Marshalling.ParseString(table, "features");
      this.utilisation = Marshalling.ParseDouble(table, "utilisation");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(Host_cpu update)
    {
      this.uuid = update.uuid;
      this.host = update.host;
      this.number = update.number;
      this.vendor = update.vendor;
      this.speed = update.speed;
      this.modelname = update.modelname;
      this.family = update.family;
      this.model = update.model;
      this.stepping = update.stepping;
      this.flags = update.flags;
      this.features = update.features;
      this.utilisation = update.utilisation;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_Host_cpu proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.host = proxy.host == null ? (XenRef<Host>) null : XenRef<Host>.Create(proxy.host);
      this.number = proxy.number == null ? 0L : long.Parse(proxy.number);
      this.vendor = proxy.vendor == null ? (string) null : proxy.vendor;
      this.speed = proxy.speed == null ? 0L : long.Parse(proxy.speed);
      this.modelname = proxy.modelname == null ? (string) null : proxy.modelname;
      this.family = proxy.family == null ? 0L : long.Parse(proxy.family);
      this.model = proxy.model == null ? 0L : long.Parse(proxy.model);
      this.stepping = proxy.stepping == null ? (string) null : proxy.stepping;
      this.flags = proxy.flags == null ? (string) null : proxy.flags;
      this.features = proxy.features == null ? (string) null : proxy.features;
      this.utilisation = Convert.ToDouble(proxy.utilisation);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_Host_cpu ToProxy()
    {
      return new Proxy_Host_cpu()
      {
        uuid = this.uuid != null ? this.uuid : "",
        host = this.host != null ? (string) this.host : "",
        number = this.number.ToString(),
        vendor = this.vendor != null ? this.vendor : "",
        speed = this.speed.ToString(),
        modelname = this.modelname != null ? this.modelname : "",
        family = this.family.ToString(),
        model = this.model.ToString(),
        stepping = this.stepping != null ? this.stepping : "",
        flags = this.flags != null ? this.flags : "",
        features = this.features != null ? this.features : "",
        utilisation = this.utilisation,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(Host_cpu other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<XenRef<Host>>(this._host, other._host) && (Helper.AreEqual2<long>(this._number, other._number) && Helper.AreEqual2<string>(this._vendor, other._vendor)) && (Helper.AreEqual2<long>(this._speed, other._speed) && Helper.AreEqual2<string>(this._modelname, other._modelname) && (Helper.AreEqual2<long>(this._family, other._family) && Helper.AreEqual2<long>(this._model, other._model))) && (Helper.AreEqual2<string>(this._stepping, other._stepping) && Helper.AreEqual2<string>(this._flags, other._flags) && (Helper.AreEqual2<string>(this._features, other._features) && Helper.AreEqual2<double>(this._utilisation, other._utilisation))))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Host_cpu server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Host_cpu.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static Host_cpu get_record(Session session, string _host_cpu)
    {
      return new Host_cpu(session.proxy.host_cpu_get_record(session.uuid, _host_cpu != null ? _host_cpu : "").parse());
    }

    public static XenRef<Host_cpu> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Host_cpu>.Create(session.proxy.host_cpu_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _host_cpu)
    {
      return session.proxy.host_cpu_get_uuid(session.uuid, _host_cpu != null ? _host_cpu : "").parse();
    }

    public static XenRef<Host> get_host(Session session, string _host_cpu)
    {
      return XenRef<Host>.Create(session.proxy.host_cpu_get_host(session.uuid, _host_cpu != null ? _host_cpu : "").parse());
    }

    public static long get_number(Session session, string _host_cpu)
    {
      return long.Parse(session.proxy.host_cpu_get_number(session.uuid, _host_cpu != null ? _host_cpu : "").parse());
    }

    public static string get_vendor(Session session, string _host_cpu)
    {
      return session.proxy.host_cpu_get_vendor(session.uuid, _host_cpu != null ? _host_cpu : "").parse();
    }

    public static long get_speed(Session session, string _host_cpu)
    {
      return long.Parse(session.proxy.host_cpu_get_speed(session.uuid, _host_cpu != null ? _host_cpu : "").parse());
    }

    public static string get_modelname(Session session, string _host_cpu)
    {
      return session.proxy.host_cpu_get_modelname(session.uuid, _host_cpu != null ? _host_cpu : "").parse();
    }

    public static long get_family(Session session, string _host_cpu)
    {
      return long.Parse(session.proxy.host_cpu_get_family(session.uuid, _host_cpu != null ? _host_cpu : "").parse());
    }

    public static long get_model(Session session, string _host_cpu)
    {
      return long.Parse(session.proxy.host_cpu_get_model(session.uuid, _host_cpu != null ? _host_cpu : "").parse());
    }

    public static string get_stepping(Session session, string _host_cpu)
    {
      return session.proxy.host_cpu_get_stepping(session.uuid, _host_cpu != null ? _host_cpu : "").parse();
    }

    public static string get_flags(Session session, string _host_cpu)
    {
      return session.proxy.host_cpu_get_flags(session.uuid, _host_cpu != null ? _host_cpu : "").parse();
    }

    public static string get_features(Session session, string _host_cpu)
    {
      return session.proxy.host_cpu_get_features(session.uuid, _host_cpu != null ? _host_cpu : "").parse();
    }

    public static double get_utilisation(Session session, string _host_cpu)
    {
      return Convert.ToDouble(session.proxy.host_cpu_get_utilisation(session.uuid, _host_cpu != null ? _host_cpu : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _host_cpu)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.host_cpu_get_other_config(session.uuid, _host_cpu != null ? _host_cpu : "").parse());
    }

    public static void set_other_config(Session session, string _host_cpu, Dictionary<string, string> _other_config)
    {
      session.proxy.host_cpu_set_other_config(session.uuid, _host_cpu != null ? _host_cpu : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _host_cpu, string _key, string _value)
    {
      session.proxy.host_cpu_add_to_other_config(session.uuid, _host_cpu != null ? _host_cpu : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _host_cpu, string _key)
    {
      session.proxy.host_cpu_remove_from_other_config(session.uuid, _host_cpu != null ? _host_cpu : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<Host_cpu>> get_all(Session session)
    {
      return XenRef<Host_cpu>.Create(session.proxy.host_cpu_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Host_cpu>, Host_cpu> get_all_records(Session session)
    {
      return XenRef<Host_cpu>.Create<Proxy_Host_cpu>(session.proxy.host_cpu_get_all_records(session.uuid).parse());
    }
  }
}
