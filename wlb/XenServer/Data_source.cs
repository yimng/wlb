// Decompiled with JetBrains decompiler
// Type: XenAPI.Data_source
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Data_source : XenObject<Data_source>
  {
    private string _name_label;
    private string _name_description;
    private bool _enabled;
    private bool _standard;
    private string _units;
    private double _min;
    private double _max;
    private double _value;

    public string name_label
    {
      get
      {
        return this._name_label;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._name_label))
          return;
        this._name_label = value;
        this.Changed = true;
        this.NotifyPropertyChanged("name_label");
      }
    }

    public string name_description
    {
      get
      {
        return this._name_description;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._name_description))
          return;
        this._name_description = value;
        this.Changed = true;
        this.NotifyPropertyChanged("name_description");
      }
    }

    public bool enabled
    {
      get
      {
        return this._enabled;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._enabled ? 1 : 0)))
          return;
        this._enabled = value;
        this.Changed = true;
        this.NotifyPropertyChanged("enabled");
      }
    }

    public bool standard
    {
      get
      {
        return this._standard;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._standard ? 1 : 0)))
          return;
        this._standard = value;
        this.Changed = true;
        this.NotifyPropertyChanged("standard");
      }
    }

    public string units
    {
      get
      {
        return this._units;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._units))
          return;
        this._units = value;
        this.Changed = true;
        this.NotifyPropertyChanged("units");
      }
    }

    public double min
    {
      get
      {
        return this._min;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._min))
          return;
        this._min = value;
        this.Changed = true;
        this.NotifyPropertyChanged("min");
      }
    }

    public double max
    {
      get
      {
        return this._max;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._max))
          return;
        this._max = value;
        this.Changed = true;
        this.NotifyPropertyChanged("max");
      }
    }

    public double value
    {
      get
      {
        return this._value;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._value))
          return;
        this._value = value;
        this.Changed = true;
        this.NotifyPropertyChanged("value");
      }
    }

    public Data_source()
    {
    }

    public Data_source(string name_label, string name_description, bool enabled, bool standard, string units, double min, double max, double value)
    {
      this.name_label = name_label;
      this.name_description = name_description;
      this.enabled = enabled;
      this.standard = standard;
      this.units = units;
      this.min = min;
      this.max = max;
      this.value = value;
    }

    public Data_source(Proxy_Data_source proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Data_source(Hashtable table)
    {
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.enabled = Marshalling.ParseBool(table, "enabled");
      this.standard = Marshalling.ParseBool(table, "standard");
      this.units = Marshalling.ParseString(table, "units");
      this.min = Marshalling.ParseDouble(table, "min");
      this.max = Marshalling.ParseDouble(table, "max");
      this.value = Marshalling.ParseDouble(table, "value");
    }

    public override void UpdateFrom(Data_source update)
    {
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.enabled = update.enabled;
      this.standard = update.standard;
      this.units = update.units;
      this.min = update.min;
      this.max = update.max;
      this.value = update.value;
    }

    internal void UpdateFromProxy(Proxy_Data_source proxy)
    {
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.enabled = proxy.enabled;
      this.standard = proxy.standard;
      this.units = proxy.units == null ? (string) null : proxy.units;
      this.min = Convert.ToDouble(proxy.min);
      this.max = Convert.ToDouble(proxy.max);
      this.value = Convert.ToDouble(proxy.value);
    }

    public Proxy_Data_source ToProxy()
    {
      return new Proxy_Data_source()
      {
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        enabled = this.enabled,
        standard = this.standard,
        units = this.units != null ? this.units : "",
        min = this.min,
        max = this.max,
        value = this.value
      };
    }

    public bool DeepEquals(Data_source other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._name_label, other._name_label) && Helper.AreEqual2<string>(this._name_description, other._name_description) && (Helper.AreEqual2<bool>(this._enabled, other._enabled) && Helper.AreEqual2<bool>(this._standard, other._standard)) && (Helper.AreEqual2<string>(this._units, other._units) && Helper.AreEqual2<double>(this._min, other._min) && Helper.AreEqual2<double>(this._max, other._max)))
        return Helper.AreEqual2<double>(this._value, other._value);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Data_source server)
    {
      if (opaqueRef == null)
        return "";
      throw new InvalidOperationException("This type has no read/write properties");
    }

    public static Dictionary<XenRef<Data_source>, Data_source> get_all_records(Session session)
    {
      return XenRef<Data_source>.Create<Proxy_Data_source>(session.proxy.data_source_get_all_records(session.uuid).parse());
    }
  }
}
