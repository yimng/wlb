// Decompiled with JetBrains decompiler
// Type: XenAPI.User
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class User : XenObject<User>
  {
    private string _uuid;
    private string _short_name;
    private string _fullname;
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

    public string short_name
    {
      get
      {
        return this._short_name;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._short_name))
          return;
        this._short_name = value;
        this.Changed = true;
        this.NotifyPropertyChanged("short_name");
      }
    }

    public string fullname
    {
      get
      {
        return this._fullname;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._fullname))
          return;
        this._fullname = value;
        this.Changed = true;
        this.NotifyPropertyChanged("fullname");
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

    public User()
    {
    }

    public User(string uuid, string short_name, string fullname, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.short_name = short_name;
      this.fullname = fullname;
      this.other_config = other_config;
    }

    public User(Proxy_User proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public User(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.short_name = Marshalling.ParseString(table, "short_name");
      this.fullname = Marshalling.ParseString(table, "fullname");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(User update)
    {
      this.uuid = update.uuid;
      this.short_name = update.short_name;
      this.fullname = update.fullname;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_User proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.short_name = proxy.short_name == null ? (string) null : proxy.short_name;
      this.fullname = proxy.fullname == null ? (string) null : proxy.fullname;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_User ToProxy()
    {
      return new Proxy_User()
      {
        uuid = this.uuid != null ? this.uuid : "",
        short_name = this.short_name != null ? this.short_name : "",
        fullname = this.fullname != null ? this.fullname : "",
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(User other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._short_name, other._short_name) && Helper.AreEqual2<string>(this._fullname, other._fullname))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, User server)
    {
      if (opaqueRef == null)
      {
        Proxy_User _record = this.ToProxy();
        return session.proxy.user_create(session.uuid, _record).parse();
      }
      if (!Helper.AreEqual2<string>(this._fullname, server._fullname))
        User.set_fullname(session, opaqueRef, this._fullname);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        User.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static User get_record(Session session, string _user)
    {
      return new User(session.proxy.user_get_record(session.uuid, _user != null ? _user : "").parse());
    }

    public static XenRef<User> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<User>.Create(session.proxy.user_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<User> create(Session session, User _record)
    {
      return XenRef<User>.Create(session.proxy.user_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, User _record)
    {
      return XenRef<Task>.Create(session.proxy.async_user_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _user)
    {
      session.proxy.user_destroy(session.uuid, _user != null ? _user : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _user)
    {
      return XenRef<Task>.Create(session.proxy.async_user_destroy(session.uuid, _user != null ? _user : "").parse());
    }

    public static string get_uuid(Session session, string _user)
    {
      return session.proxy.user_get_uuid(session.uuid, _user != null ? _user : "").parse();
    }

    public static string get_short_name(Session session, string _user)
    {
      return session.proxy.user_get_short_name(session.uuid, _user != null ? _user : "").parse();
    }

    public static string get_fullname(Session session, string _user)
    {
      return session.proxy.user_get_fullname(session.uuid, _user != null ? _user : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _user)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.user_get_other_config(session.uuid, _user != null ? _user : "").parse());
    }

    public static void set_fullname(Session session, string _user, string _fullname)
    {
      session.proxy.user_set_fullname(session.uuid, _user != null ? _user : "", _fullname != null ? _fullname : "").parse();
    }

    public static void set_other_config(Session session, string _user, Dictionary<string, string> _other_config)
    {
      session.proxy.user_set_other_config(session.uuid, _user != null ? _user : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _user, string _key, string _value)
    {
      session.proxy.user_add_to_other_config(session.uuid, _user != null ? _user : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _user, string _key)
    {
      session.proxy.user_remove_from_other_config(session.uuid, _user != null ? _user : "", _key != null ? _key : "").parse();
    }

    public static Dictionary<XenRef<User>, User> get_all_records(Session session)
    {
      return XenRef<User>.Create<Proxy_User>(session.proxy.user_get_all_records(session.uuid).parse());
    }
  }
}
