// Decompiled with JetBrains decompiler
// Type: XenAPI.Pool_patch
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Pool_patch : XenObject<Pool_patch>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private string _version;
    private long _size;
    private bool _pool_applied;
    private List<XenRef<Host_patch>> _host_patches;
    private List<XenAPI.after_apply_guidance> _after_apply_guidance;
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

    public string version
    {
      get
      {
        return this._version;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._version))
          return;
        this._version = value;
        this.Changed = true;
        this.NotifyPropertyChanged("version");
      }
    }

    public long size
    {
      get
      {
        return this._size;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._size))
          return;
        this._size = value;
        this.Changed = true;
        this.NotifyPropertyChanged("size");
      }
    }

    public bool pool_applied
    {
      get
      {
        return this._pool_applied;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._pool_applied ? 1 : 0)))
          return;
        this._pool_applied = value;
        this.Changed = true;
        this.NotifyPropertyChanged("pool_applied");
      }
    }

    public List<XenRef<Host_patch>> host_patches
    {
      get
      {
        return this._host_patches;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._host_patches))
          return;
        this._host_patches = value;
        this.Changed = true;
        this.NotifyPropertyChanged("host_patches");
      }
    }

    public List<XenAPI.after_apply_guidance> after_apply_guidance
    {
      get
      {
        return this._after_apply_guidance;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._after_apply_guidance))
          return;
        this._after_apply_guidance = value;
        this.Changed = true;
        this.NotifyPropertyChanged("after_apply_guidance");
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

    public Pool_patch()
    {
    }

    public Pool_patch(string uuid, string name_label, string name_description, string version, long size, bool pool_applied, List<XenRef<Host_patch>> host_patches, List<XenAPI.after_apply_guidance> after_apply_guidance, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.version = version;
      this.size = size;
      this.pool_applied = pool_applied;
      this.host_patches = host_patches;
      this.after_apply_guidance = after_apply_guidance;
      this.other_config = other_config;
    }

    public Pool_patch(Proxy_Pool_patch proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Pool_patch(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.version = Marshalling.ParseString(table, "version");
      this.size = Marshalling.ParseLong(table, "size");
      this.pool_applied = Marshalling.ParseBool(table, "pool_applied");
      this.host_patches = Marshalling.ParseSetRef<Host_patch>(table, "host_patches");
      this.after_apply_guidance = Helper.StringArrayToEnumList<XenAPI.after_apply_guidance>(Marshalling.ParseStringArray(table, "after_apply_guidance"));
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(Pool_patch update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.version = update.version;
      this.size = update.size;
      this.pool_applied = update.pool_applied;
      this.host_patches = update.host_patches;
      this.after_apply_guidance = update.after_apply_guidance;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_Pool_patch proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.version = proxy.version == null ? (string) null : proxy.version;
      this.size = proxy.size == null ? 0L : long.Parse(proxy.size);
      this.pool_applied = proxy.pool_applied;
      this.host_patches = proxy.host_patches == null ? (List<XenRef<Host_patch>>) null : XenRef<Host_patch>.Create(proxy.host_patches);
      this.after_apply_guidance = proxy.after_apply_guidance == null ? (List<XenAPI.after_apply_guidance>) null : Helper.StringArrayToEnumList<XenAPI.after_apply_guidance>(proxy.after_apply_guidance);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_Pool_patch ToProxy()
    {
      return new Proxy_Pool_patch()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        version = this.version != null ? this.version : "",
        size = this.size.ToString(),
        pool_applied = this.pool_applied,
        host_patches = this.host_patches != null ? Helper.RefListToStringArray<Host_patch>(this.host_patches) : new string[0],
        after_apply_guidance = this.after_apply_guidance != null ? Helper.ObjectListToStringArray<XenAPI.after_apply_guidance>(this.after_apply_guidance) : new string[0],
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(Pool_patch other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._name_label, other._name_label) && (Helper.AreEqual2<string>(this._name_description, other._name_description) && Helper.AreEqual2<string>(this._version, other._version)) && (Helper.AreEqual2<long>(this._size, other._size) && Helper.AreEqual2<bool>(this._pool_applied, other._pool_applied) && (Helper.AreEqual2<List<XenRef<Host_patch>>>(this._host_patches, other._host_patches) && Helper.AreEqual2<List<XenAPI.after_apply_guidance>>(this._after_apply_guidance, other._after_apply_guidance))))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Pool_patch server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Pool_patch.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static Pool_patch get_record(Session session, string _pool_patch)
    {
      return new Pool_patch(session.proxy.pool_patch_get_record(session.uuid, _pool_patch != null ? _pool_patch : "").parse());
    }

    public static XenRef<Pool_patch> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Pool_patch>.Create(session.proxy.pool_patch_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static List<XenRef<Pool_patch>> get_by_name_label(Session session, string _label)
    {
      return XenRef<Pool_patch>.Create(session.proxy.pool_patch_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _pool_patch)
    {
      return session.proxy.pool_patch_get_uuid(session.uuid, _pool_patch != null ? _pool_patch : "").parse();
    }

    public static string get_name_label(Session session, string _pool_patch)
    {
      return session.proxy.pool_patch_get_name_label(session.uuid, _pool_patch != null ? _pool_patch : "").parse();
    }

    public static string get_name_description(Session session, string _pool_patch)
    {
      return session.proxy.pool_patch_get_name_description(session.uuid, _pool_patch != null ? _pool_patch : "").parse();
    }

    public static string get_version(Session session, string _pool_patch)
    {
      return session.proxy.pool_patch_get_version(session.uuid, _pool_patch != null ? _pool_patch : "").parse();
    }

    public static long get_size(Session session, string _pool_patch)
    {
      return long.Parse(session.proxy.pool_patch_get_size(session.uuid, _pool_patch != null ? _pool_patch : "").parse());
    }

    public static bool get_pool_applied(Session session, string _pool_patch)
    {
      return session.proxy.pool_patch_get_pool_applied(session.uuid, _pool_patch != null ? _pool_patch : "").parse();
    }

    public static List<XenRef<Host_patch>> get_host_patches(Session session, string _pool_patch)
    {
      return XenRef<Host_patch>.Create(session.proxy.pool_patch_get_host_patches(session.uuid, _pool_patch != null ? _pool_patch : "").parse());
    }

    public static List<XenAPI.after_apply_guidance> get_after_apply_guidance(Session session, string _pool_patch)
    {
      return Helper.StringArrayToEnumList<XenAPI.after_apply_guidance>(session.proxy.pool_patch_get_after_apply_guidance(session.uuid, _pool_patch != null ? _pool_patch : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _pool_patch)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pool_patch_get_other_config(session.uuid, _pool_patch != null ? _pool_patch : "").parse());
    }

    public static void set_other_config(Session session, string _pool_patch, Dictionary<string, string> _other_config)
    {
      session.proxy.pool_patch_set_other_config(session.uuid, _pool_patch != null ? _pool_patch : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _pool_patch, string _key, string _value)
    {
      session.proxy.pool_patch_add_to_other_config(session.uuid, _pool_patch != null ? _pool_patch : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _pool_patch, string _key)
    {
      session.proxy.pool_patch_remove_from_other_config(session.uuid, _pool_patch != null ? _pool_patch : "", _key != null ? _key : "").parse();
    }

    public static string apply(Session session, string _self, string _host)
    {
      return session.proxy.pool_patch_apply(session.uuid, _self != null ? _self : "", _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_apply(Session session, string _self, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_patch_apply(session.uuid, _self != null ? _self : "", _host != null ? _host : "").parse());
    }

    public static void pool_apply(Session session, string _self)
    {
      session.proxy.pool_patch_pool_apply(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_pool_apply(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_patch_pool_apply(session.uuid, _self != null ? _self : "").parse());
    }

    public static string precheck(Session session, string _self, string _host)
    {
      return session.proxy.pool_patch_precheck(session.uuid, _self != null ? _self : "", _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_precheck(Session session, string _self, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_patch_precheck(session.uuid, _self != null ? _self : "", _host != null ? _host : "").parse());
    }

    public static void clean(Session session, string _self)
    {
      session.proxy.pool_patch_clean(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_clean(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_patch_clean(session.uuid, _self != null ? _self : "").parse());
    }

    public static void pool_clean(Session session, string _self)
    {
      session.proxy.pool_patch_pool_clean(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_pool_clean(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_patch_pool_clean(session.uuid, _self != null ? _self : "").parse());
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.pool_patch_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_patch_destroy(session.uuid, _self != null ? _self : "").parse());
    }

    public static void clean_on_host(Session session, string _self, string _host)
    {
      session.proxy.pool_patch_clean_on_host(session.uuid, _self != null ? _self : "", _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_clean_on_host(Session session, string _self, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_patch_clean_on_host(session.uuid, _self != null ? _self : "", _host != null ? _host : "").parse());
    }

    public static List<XenRef<Pool_patch>> get_all(Session session)
    {
      return XenRef<Pool_patch>.Create(session.proxy.pool_patch_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Pool_patch>, Pool_patch> get_all_records(Session session)
    {
      return XenRef<Pool_patch>.Create<Proxy_Pool_patch>(session.proxy.pool_patch_get_all_records(session.uuid).parse());
    }
  }
}
