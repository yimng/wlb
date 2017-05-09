// Decompiled with JetBrains decompiler
// Type: XenAPI.Crashdump
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Crashdump : XenObject<Crashdump>
  {
    private string _uuid;
    private XenRef<XenAPI.VM> _VM;
    private XenRef<XenAPI.VDI> _VDI;
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

    public XenRef<XenAPI.VDI> VDI
    {
      get
      {
        return this._VDI;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VDI))
          return;
        this._VDI = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VDI");
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

    public Crashdump()
    {
    }

    public Crashdump(string uuid, XenRef<XenAPI.VM> VM, XenRef<XenAPI.VDI> VDI, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.VM = VM;
      this.VDI = VDI;
      this.other_config = other_config;
    }

    public Crashdump(Proxy_Crashdump proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Crashdump(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.VM = Marshalling.ParseRef<XenAPI.VM>(table, "VM");
      this.VDI = Marshalling.ParseRef<XenAPI.VDI>(table, "VDI");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(Crashdump update)
    {
      this.uuid = update.uuid;
      this.VM = update.VM;
      this.VDI = update.VDI;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_Crashdump proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.VM = proxy.VM == null ? (XenRef<XenAPI.VM>) null : XenRef<XenAPI.VM>.Create(proxy.VM);
      this.VDI = proxy.VDI == null ? (XenRef<XenAPI.VDI>) null : XenRef<XenAPI.VDI>.Create(proxy.VDI);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_Crashdump ToProxy()
    {
      return new Proxy_Crashdump()
      {
        uuid = this.uuid != null ? this.uuid : "",
        VM = this.VM != null ? (string) this.VM : "",
        VDI = this.VDI != null ? (string) this.VDI : "",
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(Crashdump other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<XenRef<XenAPI.VM>>(this._VM, other._VM) && Helper.AreEqual2<XenRef<XenAPI.VDI>>(this._VDI, other._VDI))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Crashdump server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Crashdump.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static Crashdump get_record(Session session, string _crashdump)
    {
      return new Crashdump(session.proxy.crashdump_get_record(session.uuid, _crashdump != null ? _crashdump : "").parse());
    }

    public static XenRef<Crashdump> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Crashdump>.Create(session.proxy.crashdump_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _crashdump)
    {
      return session.proxy.crashdump_get_uuid(session.uuid, _crashdump != null ? _crashdump : "").parse();
    }

    public static XenRef<XenAPI.VM> get_VM(Session session, string _crashdump)
    {
      return XenRef<XenAPI.VM>.Create(session.proxy.crashdump_get_vm(session.uuid, _crashdump != null ? _crashdump : "").parse());
    }

    public static XenRef<XenAPI.VDI> get_VDI(Session session, string _crashdump)
    {
      return XenRef<XenAPI.VDI>.Create(session.proxy.crashdump_get_vdi(session.uuid, _crashdump != null ? _crashdump : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _crashdump)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.crashdump_get_other_config(session.uuid, _crashdump != null ? _crashdump : "").parse());
    }

    public static void set_other_config(Session session, string _crashdump, Dictionary<string, string> _other_config)
    {
      session.proxy.crashdump_set_other_config(session.uuid, _crashdump != null ? _crashdump : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _crashdump, string _key, string _value)
    {
      session.proxy.crashdump_add_to_other_config(session.uuid, _crashdump != null ? _crashdump : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _crashdump, string _key)
    {
      session.proxy.crashdump_remove_from_other_config(session.uuid, _crashdump != null ? _crashdump : "", _key != null ? _key : "").parse();
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.crashdump_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_crashdump_destroy(session.uuid, _self != null ? _self : "").parse());
    }

    public static List<XenRef<Crashdump>> get_all(Session session)
    {
      return XenRef<Crashdump>.Create(session.proxy.crashdump_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Crashdump>, Crashdump> get_all_records(Session session)
    {
      return XenRef<Crashdump>.Create<Proxy_Crashdump>(session.proxy.crashdump_get_all_records(session.uuid).parse());
    }
  }
}
