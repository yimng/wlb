﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.PCI
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class PCI : XenObject<PCI>
  {
    private string _uuid;
    private string _class_name;
    private string _vendor_name;
    private string _device_name;
    private XenRef<Host> _host;
    private string _pci_id;
    private List<XenRef<PCI>> _dependencies;
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

    public string class_name
    {
      get
      {
        return this._class_name;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._class_name))
          return;
        this._class_name = value;
        this.Changed = true;
        this.NotifyPropertyChanged("class_name");
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

    public string pci_id
    {
      get
      {
        return this._pci_id;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._pci_id))
          return;
        this._pci_id = value;
        this.Changed = true;
        this.NotifyPropertyChanged("pci_id");
      }
    }

    public List<XenRef<PCI>> dependencies
    {
      get
      {
        return this._dependencies;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._dependencies))
          return;
        this._dependencies = value;
        this.Changed = true;
        this.NotifyPropertyChanged("dependencies");
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

    public PCI()
    {
    }

    public PCI(string uuid, string class_name, string vendor_name, string device_name, XenRef<Host> host, string pci_id, List<XenRef<PCI>> dependencies, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.class_name = class_name;
      this.vendor_name = vendor_name;
      this.device_name = device_name;
      this.host = host;
      this.pci_id = pci_id;
      this.dependencies = dependencies;
      this.other_config = other_config;
    }

    public PCI(Proxy_PCI proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public PCI(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.class_name = Marshalling.ParseString(table, "class_name");
      this.vendor_name = Marshalling.ParseString(table, "vendor_name");
      this.device_name = Marshalling.ParseString(table, "device_name");
      this.host = Marshalling.ParseRef<Host>(table, "host");
      this.pci_id = Marshalling.ParseString(table, "pci_id");
      this.dependencies = Marshalling.ParseSetRef<PCI>(table, "dependencies");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(PCI update)
    {
      this.uuid = update.uuid;
      this.class_name = update.class_name;
      this.vendor_name = update.vendor_name;
      this.device_name = update.device_name;
      this.host = update.host;
      this.pci_id = update.pci_id;
      this.dependencies = update.dependencies;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_PCI proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.class_name = proxy.class_name == null ? (string) null : proxy.class_name;
      this.vendor_name = proxy.vendor_name == null ? (string) null : proxy.vendor_name;
      this.device_name = proxy.device_name == null ? (string) null : proxy.device_name;
      this.host = proxy.host == null ? (XenRef<Host>) null : XenRef<Host>.Create(proxy.host);
      this.pci_id = proxy.pci_id == null ? (string) null : proxy.pci_id;
      this.dependencies = proxy.dependencies == null ? (List<XenRef<PCI>>) null : XenRef<PCI>.Create(proxy.dependencies);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_PCI ToProxy()
    {
      return new Proxy_PCI()
      {
        uuid = this.uuid != null ? this.uuid : "",
        class_name = this.class_name != null ? this.class_name : "",
        vendor_name = this.vendor_name != null ? this.vendor_name : "",
        device_name = this.device_name != null ? this.device_name : "",
        host = this.host != null ? (string) this.host : "",
        pci_id = this.pci_id != null ? this.pci_id : "",
        dependencies = this.dependencies != null ? Helper.RefListToStringArray<PCI>(this.dependencies) : new string[0],
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(PCI other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._class_name, other._class_name) && (Helper.AreEqual2<string>(this._vendor_name, other._vendor_name) && Helper.AreEqual2<string>(this._device_name, other._device_name)) && (Helper.AreEqual2<XenRef<Host>>(this._host, other._host) && Helper.AreEqual2<string>(this._pci_id, other._pci_id) && Helper.AreEqual2<List<XenRef<PCI>>>(this._dependencies, other._dependencies)))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, PCI server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        PCI.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static PCI get_record(Session session, string _pci)
    {
      return new PCI(session.proxy.pci_get_record(session.uuid, _pci != null ? _pci : "").parse());
    }

    public static XenRef<PCI> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<PCI>.Create(session.proxy.pci_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _pci)
    {
      return session.proxy.pci_get_uuid(session.uuid, _pci != null ? _pci : "").parse();
    }

    public static string get_class_name(Session session, string _pci)
    {
      return session.proxy.pci_get_class_name(session.uuid, _pci != null ? _pci : "").parse();
    }

    public static string get_vendor_name(Session session, string _pci)
    {
      return session.proxy.pci_get_vendor_name(session.uuid, _pci != null ? _pci : "").parse();
    }

    public static string get_device_name(Session session, string _pci)
    {
      return session.proxy.pci_get_device_name(session.uuid, _pci != null ? _pci : "").parse();
    }

    public static XenRef<Host> get_host(Session session, string _pci)
    {
      return XenRef<Host>.Create(session.proxy.pci_get_host(session.uuid, _pci != null ? _pci : "").parse());
    }

    public static string get_pci_id(Session session, string _pci)
    {
      return session.proxy.pci_get_pci_id(session.uuid, _pci != null ? _pci : "").parse();
    }

    public static List<XenRef<PCI>> get_dependencies(Session session, string _pci)
    {
      return XenRef<PCI>.Create(session.proxy.pci_get_dependencies(session.uuid, _pci != null ? _pci : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _pci)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pci_get_other_config(session.uuid, _pci != null ? _pci : "").parse());
    }

    public static void set_other_config(Session session, string _pci, Dictionary<string, string> _other_config)
    {
      session.proxy.pci_set_other_config(session.uuid, _pci != null ? _pci : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _pci, string _key, string _value)
    {
      session.proxy.pci_add_to_other_config(session.uuid, _pci != null ? _pci : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _pci, string _key)
    {
      session.proxy.pci_remove_from_other_config(session.uuid, _pci != null ? _pci : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<PCI>> get_all(Session session)
    {
      return XenRef<PCI>.Create(session.proxy.pci_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<PCI>, PCI> get_all_records(Session session)
    {
      return XenRef<PCI>.Create<Proxy_PCI>(session.proxy.pci_get_all_records(session.uuid).parse());
    }
  }
}
