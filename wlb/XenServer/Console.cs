// Decompiled with JetBrains decompiler
// Type: XenAPI.Console
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Console : XenObject<Console>
  {
    private string _uuid;
    private console_protocol _protocol;
    private string _location;
    private XenRef<XenAPI.VM> _VM;
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

    public console_protocol protocol
    {
      get
      {
        return this._protocol;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._protocol))
          return;
        this._protocol = value;
        this.Changed = true;
        this.NotifyPropertyChanged("protocol");
      }
    }

    public string location
    {
      get
      {
        return this._location;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._location))
          return;
        this._location = value;
        this.Changed = true;
        this.NotifyPropertyChanged("location");
      }
    }

    public XenRef<XenAPI.VM> VM
    {
      get
      {
        return this._VM;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VM))
          return;
        this._VM = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VM");
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

    public Console()
    {
    }

    public Console(string uuid, console_protocol protocol, string location, XenRef<XenAPI.VM> VM, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.protocol = protocol;
      this.location = location;
      this.VM = VM;
      this.other_config = other_config;
    }

    public Console(Proxy_Console proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Console(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.protocol = (console_protocol) Helper.EnumParseDefault(typeof (console_protocol), Marshalling.ParseString(table, "protocol"));
      this.location = Marshalling.ParseString(table, "location");
      this.VM = Marshalling.ParseRef<XenAPI.VM>(table, "VM");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(Console update)
    {
      this.uuid = update.uuid;
      this.protocol = update.protocol;
      this.location = update.location;
      this.VM = update.VM;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_Console proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.protocol = proxy.protocol == null ? console_protocol.vt100 : (console_protocol) Helper.EnumParseDefault(typeof (console_protocol), proxy.protocol);
      this.location = proxy.location == null ? (string) null : proxy.location;
      this.VM = proxy.VM == null ? (XenRef<XenAPI.VM>) null : XenRef<XenAPI.VM>.Create(proxy.VM);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_Console ToProxy()
    {
      return new Proxy_Console()
      {
        uuid = this.uuid != null ? this.uuid : "",
        protocol = console_protocol_helper.ToString(this.protocol),
        location = this.location != null ? this.location : "",
        VM = this.VM != null ? (string) this.VM : "",
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(Console other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<console_protocol>(this._protocol, other._protocol) && (Helper.AreEqual2<string>(this._location, other._location) && Helper.AreEqual2<XenRef<XenAPI.VM>>(this._VM, other._VM)))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Console server)
    {
      if (opaqueRef == null)
      {
        Proxy_Console _record = this.ToProxy();
        return session.proxy.console_create(session.uuid, _record).parse();
      }
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Console.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static Console get_record(Session session, string _console)
    {
      return new Console(session.proxy.console_get_record(session.uuid, _console != null ? _console : "").parse());
    }

    public static XenRef<Console> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Console>.Create(session.proxy.console_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<Console> create(Session session, Console _record)
    {
      return XenRef<Console>.Create(session.proxy.console_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, Console _record)
    {
      return XenRef<Task>.Create(session.proxy.async_console_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _console)
    {
      session.proxy.console_destroy(session.uuid, _console != null ? _console : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _console)
    {
      return XenRef<Task>.Create(session.proxy.async_console_destroy(session.uuid, _console != null ? _console : "").parse());
    }

    public static string get_uuid(Session session, string _console)
    {
      return session.proxy.console_get_uuid(session.uuid, _console != null ? _console : "").parse();
    }

    public static console_protocol get_protocol(Session session, string _console)
    {
      return (console_protocol) Helper.EnumParseDefault(typeof (console_protocol), session.proxy.console_get_protocol(session.uuid, _console != null ? _console : "").parse());
    }

    public static string get_location(Session session, string _console)
    {
      return session.proxy.console_get_location(session.uuid, _console != null ? _console : "").parse();
    }

    public static XenRef<XenAPI.VM> get_VM(Session session, string _console)
    {
      return XenRef<XenAPI.VM>.Create(session.proxy.console_get_vm(session.uuid, _console != null ? _console : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _console)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.console_get_other_config(session.uuid, _console != null ? _console : "").parse());
    }

    public static void set_other_config(Session session, string _console, Dictionary<string, string> _other_config)
    {
      session.proxy.console_set_other_config(session.uuid, _console != null ? _console : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _console, string _key, string _value)
    {
      session.proxy.console_add_to_other_config(session.uuid, _console != null ? _console : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _console, string _key)
    {
      session.proxy.console_remove_from_other_config(session.uuid, _console != null ? _console : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<Console>> get_all(Session session)
    {
      return XenRef<Console>.Create(session.proxy.console_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Console>, Console> get_all_records(Session session)
    {
      return XenRef<Console>.Create<Proxy_Console>(session.proxy.console_get_all_records(session.uuid).parse());
    }
  }
}
