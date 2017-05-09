// Decompiled with JetBrains decompiler
// Type: XenAPI.Secret
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Secret : XenObject<Secret>
  {
    private string _uuid;
    private string _value;
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

    public string value
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

    public Secret()
    {
    }

    public Secret(string uuid, string value, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.value = value;
      this.other_config = other_config;
    }

    public Secret(Proxy_Secret proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Secret(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.value = Marshalling.ParseString(table, "value");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(Secret update)
    {
      this.uuid = update.uuid;
      this.value = update.value;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_Secret proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.value = proxy.value == null ? (string) null : proxy.value;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_Secret ToProxy()
    {
      return new Proxy_Secret()
      {
        uuid = this.uuid != null ? this.uuid : "",
        value = this.value != null ? this.value : "",
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(Secret other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._value, other._value))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Secret server)
    {
      if (opaqueRef == null)
      {
        Proxy_Secret _record = this.ToProxy();
        return session.proxy.secret_create(session.uuid, _record).parse();
      }
      if (!Helper.AreEqual2<string>(this._value, server._value))
        Secret.set_value(session, opaqueRef, this._value);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Secret.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static Secret get_record(Session session, string _secret)
    {
      return new Secret(session.proxy.secret_get_record(session.uuid, _secret != null ? _secret : "").parse());
    }

    public static XenRef<Secret> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Secret>.Create(session.proxy.secret_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<Secret> create(Session session, Secret _record)
    {
      return XenRef<Secret>.Create(session.proxy.secret_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, Secret _record)
    {
      return XenRef<Task>.Create(session.proxy.async_secret_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _secret)
    {
      session.proxy.secret_destroy(session.uuid, _secret != null ? _secret : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _secret)
    {
      return XenRef<Task>.Create(session.proxy.async_secret_destroy(session.uuid, _secret != null ? _secret : "").parse());
    }

    public static string get_uuid(Session session, string _secret)
    {
      return session.proxy.secret_get_uuid(session.uuid, _secret != null ? _secret : "").parse();
    }

    public static string get_value(Session session, string _secret)
    {
      return session.proxy.secret_get_value(session.uuid, _secret != null ? _secret : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _secret)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.secret_get_other_config(session.uuid, _secret != null ? _secret : "").parse());
    }

    public static void set_value(Session session, string _secret, string _value)
    {
      session.proxy.secret_set_value(session.uuid, _secret != null ? _secret : "", _value != null ? _value : "").parse();
    }

    public static void set_other_config(Session session, string _secret, Dictionary<string, string> _other_config)
    {
      session.proxy.secret_set_other_config(session.uuid, _secret != null ? _secret : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _secret, string _key, string _value)
    {
      session.proxy.secret_add_to_other_config(session.uuid, _secret != null ? _secret : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _secret, string _key)
    {
      session.proxy.secret_remove_from_other_config(session.uuid, _secret != null ? _secret : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<Secret>> get_all(Session session)
    {
      return XenRef<Secret>.Create(session.proxy.secret_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Secret>, Secret> get_all_records(Session session)
    {
      return XenRef<Secret>.Create<Proxy_Secret>(session.proxy.secret_get_all_records(session.uuid).parse());
    }
  }
}
