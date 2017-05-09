// Decompiled with JetBrains decompiler
// Type: XenAPI.SR
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class SR : XenObject<SR>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private List<storage_operations> _allowed_operations;
    private Dictionary<string, storage_operations> _current_operations;
    private List<XenRef<VDI>> _VDIs;
    private List<XenRef<PBD>> _PBDs;
    private long _virtual_allocation;
    private long _physical_utilisation;
    private long _physical_size;
    private string _type;
    private string _content_type;
    private bool _shared;
    private Dictionary<string, string> _other_config;
    private string[] _tags;
    private Dictionary<string, string> _sm_config;
    private Dictionary<string, XenRef<Blob>> _blobs;
    private bool _local_cache_enabled;
    private XenRef<DR_task> _introduced_by;

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

    public List<storage_operations> allowed_operations
    {
      get
      {
        return this._allowed_operations;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._allowed_operations))
          return;
        this._allowed_operations = value;
        this.Changed = true;
        this.NotifyPropertyChanged("allowed_operations");
      }
    }

    public Dictionary<string, storage_operations> current_operations
    {
      get
      {
        return this._current_operations;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._current_operations))
          return;
        this._current_operations = value;
        this.Changed = true;
        this.NotifyPropertyChanged("current_operations");
      }
    }

    public List<XenRef<VDI>> VDIs
    {
      get
      {
        return this._VDIs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VDIs))
          return;
        this._VDIs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VDIs");
      }
    }

    public List<XenRef<PBD>> PBDs
    {
      get
      {
        return this._PBDs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PBDs))
          return;
        this._PBDs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PBDs");
      }
    }

    public long virtual_allocation
    {
      get
      {
        return this._virtual_allocation;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._virtual_allocation))
          return;
        this._virtual_allocation = value;
        this.Changed = true;
        this.NotifyPropertyChanged("virtual_allocation");
      }
    }

    public long physical_utilisation
    {
      get
      {
        return this._physical_utilisation;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._physical_utilisation))
          return;
        this._physical_utilisation = value;
        this.Changed = true;
        this.NotifyPropertyChanged("physical_utilisation");
      }
    }

    public long physical_size
    {
      get
      {
        return this._physical_size;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._physical_size))
          return;
        this._physical_size = value;
        this.Changed = true;
        this.NotifyPropertyChanged("physical_size");
      }
    }

    public string type
    {
      get
      {
        return this._type;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._type))
          return;
        this._type = value;
        this.Changed = true;
        this.NotifyPropertyChanged("type");
      }
    }

    public string content_type
    {
      get
      {
        return this._content_type;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._content_type))
          return;
        this._content_type = value;
        this.Changed = true;
        this.NotifyPropertyChanged("content_type");
      }
    }

    public bool shared
    {
      get
      {
        return this._shared;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._shared ? 1 : 0)))
          return;
        this._shared = value;
        this.Changed = true;
        this.NotifyPropertyChanged("shared");
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

    public string[] tags
    {
      get
      {
        return this._tags;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._tags))
          return;
        this._tags = value;
        this.Changed = true;
        this.NotifyPropertyChanged("tags");
      }
    }

    public Dictionary<string, string> sm_config
    {
      get
      {
        return this._sm_config;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._sm_config))
          return;
        this._sm_config = value;
        this.Changed = true;
        this.NotifyPropertyChanged("sm_config");
      }
    }

    public Dictionary<string, XenRef<Blob>> blobs
    {
      get
      {
        return this._blobs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._blobs))
          return;
        this._blobs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("blobs");
      }
    }

    public bool local_cache_enabled
    {
      get
      {
        return this._local_cache_enabled;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._local_cache_enabled ? 1 : 0)))
          return;
        this._local_cache_enabled = value;
        this.Changed = true;
        this.NotifyPropertyChanged("local_cache_enabled");
      }
    }

    public XenRef<DR_task> introduced_by
    {
      get
      {
        return this._introduced_by;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._introduced_by))
          return;
        this._introduced_by = value;
        this.Changed = true;
        this.NotifyPropertyChanged("introduced_by");
      }
    }

    public SR()
    {
    }

    public SR(string uuid, string name_label, string name_description, List<storage_operations> allowed_operations, Dictionary<string, storage_operations> current_operations, List<XenRef<VDI>> VDIs, List<XenRef<PBD>> PBDs, long virtual_allocation, long physical_utilisation, long physical_size, string type, string content_type, bool shared, Dictionary<string, string> other_config, string[] tags, Dictionary<string, string> sm_config, Dictionary<string, XenRef<Blob>> blobs, bool local_cache_enabled, XenRef<DR_task> introduced_by)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.allowed_operations = allowed_operations;
      this.current_operations = current_operations;
      this.VDIs = VDIs;
      this.PBDs = PBDs;
      this.virtual_allocation = virtual_allocation;
      this.physical_utilisation = physical_utilisation;
      this.physical_size = physical_size;
      this.type = type;
      this.content_type = content_type;
      this.shared = shared;
      this.other_config = other_config;
      this.tags = tags;
      this.sm_config = sm_config;
      this.blobs = blobs;
      this.local_cache_enabled = local_cache_enabled;
      this.introduced_by = introduced_by;
    }

    public SR(Proxy_SR proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public SR(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.allowed_operations = Helper.StringArrayToEnumList<storage_operations>(Marshalling.ParseStringArray(table, "allowed_operations"));
      this.current_operations = Maps.convert_from_proxy_string_storage_operations((object) Marshalling.ParseHashTable(table, "current_operations"));
      this.VDIs = Marshalling.ParseSetRef<VDI>(table, "VDIs");
      this.PBDs = Marshalling.ParseSetRef<PBD>(table, "PBDs");
      this.virtual_allocation = Marshalling.ParseLong(table, "virtual_allocation");
      this.physical_utilisation = Marshalling.ParseLong(table, "physical_utilisation");
      this.physical_size = Marshalling.ParseLong(table, "physical_size");
      this.type = Marshalling.ParseString(table, "type");
      this.content_type = Marshalling.ParseString(table, "content_type");
      this.shared = Marshalling.ParseBool(table, "shared");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
      this.tags = Marshalling.ParseStringArray(table, "tags");
      this.sm_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "sm_config"));
      this.blobs = Maps.convert_from_proxy_string_XenRefBlob((object) Marshalling.ParseHashTable(table, "blobs"));
      this.local_cache_enabled = Marshalling.ParseBool(table, "local_cache_enabled");
      this.introduced_by = Marshalling.ParseRef<DR_task>(table, "introduced_by");
    }

    public override void UpdateFrom(SR update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.allowed_operations = update.allowed_operations;
      this.current_operations = update.current_operations;
      this.VDIs = update.VDIs;
      this.PBDs = update.PBDs;
      this.virtual_allocation = update.virtual_allocation;
      this.physical_utilisation = update.physical_utilisation;
      this.physical_size = update.physical_size;
      this.type = update.type;
      this.content_type = update.content_type;
      this.shared = update.shared;
      this.other_config = update.other_config;
      this.tags = update.tags;
      this.sm_config = update.sm_config;
      this.blobs = update.blobs;
      this.local_cache_enabled = update.local_cache_enabled;
      this.introduced_by = update.introduced_by;
    }

    internal void UpdateFromProxy(Proxy_SR proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.allowed_operations = proxy.allowed_operations == null ? (List<storage_operations>) null : Helper.StringArrayToEnumList<storage_operations>(proxy.allowed_operations);
      this.current_operations = proxy.current_operations == null ? (Dictionary<string, storage_operations>) null : Maps.convert_from_proxy_string_storage_operations(proxy.current_operations);
      this.VDIs = proxy.VDIs == null ? (List<XenRef<VDI>>) null : XenRef<VDI>.Create(proxy.VDIs);
      this.PBDs = proxy.PBDs == null ? (List<XenRef<PBD>>) null : XenRef<PBD>.Create(proxy.PBDs);
      this.virtual_allocation = proxy.virtual_allocation == null ? 0L : long.Parse(proxy.virtual_allocation);
      this.physical_utilisation = proxy.physical_utilisation == null ? 0L : long.Parse(proxy.physical_utilisation);
      this.physical_size = proxy.physical_size == null ? 0L : long.Parse(proxy.physical_size);
      this.type = proxy.type == null ? (string) null : proxy.type;
      this.content_type = proxy.content_type == null ? (string) null : proxy.content_type;
      this.shared = proxy.shared;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
      this.tags = proxy.tags == null ? new string[0] : proxy.tags;
      this.sm_config = proxy.sm_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.sm_config);
      this.blobs = proxy.blobs == null ? (Dictionary<string, XenRef<Blob>>) null : Maps.convert_from_proxy_string_XenRefBlob(proxy.blobs);
      this.local_cache_enabled = proxy.local_cache_enabled;
      this.introduced_by = proxy.introduced_by == null ? (XenRef<DR_task>) null : XenRef<DR_task>.Create(proxy.introduced_by);
    }

    public Proxy_SR ToProxy()
    {
      return new Proxy_SR()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        allowed_operations = this.allowed_operations != null ? Helper.ObjectListToStringArray<storage_operations>(this.allowed_operations) : new string[0],
        current_operations = (object) Maps.convert_to_proxy_string_storage_operations(this.current_operations),
        VDIs = this.VDIs != null ? Helper.RefListToStringArray<VDI>(this.VDIs) : new string[0],
        PBDs = this.PBDs != null ? Helper.RefListToStringArray<PBD>(this.PBDs) : new string[0],
        virtual_allocation = this.virtual_allocation.ToString(),
        physical_utilisation = this.physical_utilisation.ToString(),
        physical_size = this.physical_size.ToString(),
        type = this.type != null ? this.type : "",
        content_type = this.content_type != null ? this.content_type : "",
        shared = this.shared,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config),
        tags = this.tags,
        sm_config = (object) Maps.convert_to_proxy_string_string(this.sm_config),
        blobs = (object) Maps.convert_to_proxy_string_XenRefBlob(this.blobs),
        local_cache_enabled = this.local_cache_enabled,
        introduced_by = this.introduced_by != null ? (string) this.introduced_by : ""
      };
    }

    public bool DeepEquals(SR other, bool ignoreCurrentOperations)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (!ignoreCurrentOperations && !Helper.AreEqual2<Dictionary<string, storage_operations>>(this.current_operations, other.current_operations) || (!Helper.AreEqual2<string>(this._uuid, other._uuid) || !Helper.AreEqual2<string>(this._name_label, other._name_label)) || (!Helper.AreEqual2<string>(this._name_description, other._name_description) || !Helper.AreEqual2<List<storage_operations>>(this._allowed_operations, other._allowed_operations) || (!Helper.AreEqual2<List<XenRef<VDI>>>(this._VDIs, other._VDIs) || !Helper.AreEqual2<List<XenRef<PBD>>>(this._PBDs, other._PBDs))) || (!Helper.AreEqual2<long>(this._virtual_allocation, other._virtual_allocation) || !Helper.AreEqual2<long>(this._physical_utilisation, other._physical_utilisation) || (!Helper.AreEqual2<long>(this._physical_size, other._physical_size) || !Helper.AreEqual2<string>(this._type, other._type)) || (!Helper.AreEqual2<string>(this._content_type, other._content_type) || !Helper.AreEqual2<bool>(this._shared, other._shared) || (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config) || !Helper.AreEqual2<string[]>(this._tags, other._tags)))) || (!Helper.AreEqual2<Dictionary<string, string>>(this._sm_config, other._sm_config) || !Helper.AreEqual2<Dictionary<string, XenRef<Blob>>>(this._blobs, other._blobs) || !Helper.AreEqual2<bool>(this._local_cache_enabled, other._local_cache_enabled)))
        return false;
      return Helper.AreEqual2<XenRef<DR_task>>(this._introduced_by, other._introduced_by);
    }

    public override string SaveChanges(Session session, string opaqueRef, SR server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        SR.set_other_config(session, opaqueRef, this._other_config);
      if (!Helper.AreEqual2<string[]>(this._tags, server._tags))
        SR.set_tags(session, opaqueRef, this._tags);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._sm_config, server._sm_config))
        SR.set_sm_config(session, opaqueRef, this._sm_config);
      if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
        SR.set_name_label(session, opaqueRef, this._name_label);
      if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
        SR.set_name_description(session, opaqueRef, this._name_description);
      if (!Helper.AreEqual2<long>(this._physical_size, server._physical_size))
        SR.set_physical_size(session, opaqueRef, this._physical_size);
      return (string) null;
    }

    public static SR get_record(Session session, string _sr)
    {
      return new SR(session.proxy.sr_get_record(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static XenRef<SR> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<SR>.Create(session.proxy.sr_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static List<XenRef<SR>> get_by_name_label(Session session, string _label)
    {
      return XenRef<SR>.Create(session.proxy.sr_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _sr)
    {
      return session.proxy.sr_get_uuid(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static string get_name_label(Session session, string _sr)
    {
      return session.proxy.sr_get_name_label(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static string get_name_description(Session session, string _sr)
    {
      return session.proxy.sr_get_name_description(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static List<storage_operations> get_allowed_operations(Session session, string _sr)
    {
      return Helper.StringArrayToEnumList<storage_operations>(session.proxy.sr_get_allowed_operations(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static Dictionary<string, storage_operations> get_current_operations(Session session, string _sr)
    {
      return Maps.convert_from_proxy_string_storage_operations(session.proxy.sr_get_current_operations(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static List<XenRef<VDI>> get_VDIs(Session session, string _sr)
    {
      return XenRef<VDI>.Create(session.proxy.sr_get_vdis(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static List<XenRef<PBD>> get_PBDs(Session session, string _sr)
    {
      return XenRef<PBD>.Create(session.proxy.sr_get_pbds(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static long get_virtual_allocation(Session session, string _sr)
    {
      return long.Parse(session.proxy.sr_get_virtual_allocation(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static long get_physical_utilisation(Session session, string _sr)
    {
      return long.Parse(session.proxy.sr_get_physical_utilisation(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static long get_physical_size(Session session, string _sr)
    {
      return long.Parse(session.proxy.sr_get_physical_size(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static string get_type(Session session, string _sr)
    {
      return session.proxy.sr_get_type(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static string get_content_type(Session session, string _sr)
    {
      return session.proxy.sr_get_content_type(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static bool get_shared(Session session, string _sr)
    {
      return session.proxy.sr_get_shared(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _sr)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.sr_get_other_config(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static string[] get_tags(Session session, string _sr)
    {
      return session.proxy.sr_get_tags(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static Dictionary<string, string> get_sm_config(Session session, string _sr)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.sr_get_sm_config(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static Dictionary<string, XenRef<Blob>> get_blobs(Session session, string _sr)
    {
      return Maps.convert_from_proxy_string_XenRefBlob(session.proxy.sr_get_blobs(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static bool get_local_cache_enabled(Session session, string _sr)
    {
      return session.proxy.sr_get_local_cache_enabled(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<DR_task> get_introduced_by(Session session, string _sr)
    {
      return XenRef<DR_task>.Create(session.proxy.sr_get_introduced_by(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static void set_other_config(Session session, string _sr, Dictionary<string, string> _other_config)
    {
      session.proxy.sr_set_other_config(session.uuid, _sr != null ? _sr : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _sr, string _key, string _value)
    {
      session.proxy.sr_add_to_other_config(session.uuid, _sr != null ? _sr : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _sr, string _key)
    {
      session.proxy.sr_remove_from_other_config(session.uuid, _sr != null ? _sr : "", _key != null ? _key : "").parse();
    }

    public static void set_tags(Session session, string _sr, string[] _tags)
    {
      session.proxy.sr_set_tags(session.uuid, _sr != null ? _sr : "", _tags).parse();
    }

    public static void add_tags(Session session, string _sr, string _value)
    {
      session.proxy.sr_add_tags(session.uuid, _sr != null ? _sr : "", _value != null ? _value : "").parse();
    }

    public static void remove_tags(Session session, string _sr, string _value)
    {
      session.proxy.sr_remove_tags(session.uuid, _sr != null ? _sr : "", _value != null ? _value : "").parse();
    }

    public static void set_sm_config(Session session, string _sr, Dictionary<string, string> _sm_config)
    {
      session.proxy.sr_set_sm_config(session.uuid, _sr != null ? _sr : "", (object) Maps.convert_to_proxy_string_string(_sm_config)).parse();
    }

    public static void add_to_sm_config(Session session, string _sr, string _key, string _value)
    {
      session.proxy.sr_add_to_sm_config(session.uuid, _sr != null ? _sr : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_sm_config(Session session, string _sr, string _key)
    {
      session.proxy.sr_remove_from_sm_config(session.uuid, _sr != null ? _sr : "", _key != null ? _key : "").parse();
    }

    public static XenRef<SR> create(Session session, string _host, Dictionary<string, string> _device_config, long _physical_size, string _name_label, string _name_description, string _type, string _content_type, bool _shared, Dictionary<string, string> _sm_config)
    {
      if (Helper.APIVersionMeets(session, API_Version.API_1_2))
        return XenRef<SR>.Create(session.proxy.sr_create(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _physical_size.ToString(), _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", _shared, (object) Maps.convert_to_proxy_string_string(_sm_config)).parse());
      return SR.create(session, _host, _device_config, _physical_size, _name_label, _name_description, _type, _content_type, _shared);
    }

    public static XenRef<SR> create(Session session, string _host, Dictionary<string, string> _device_config, long _physical_size, string _name_label, string _name_description, string _type, string _content_type, bool _shared)
    {
      return XenRef<SR>.Create(session.proxy.sr_create(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _physical_size.ToString(), _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", _shared).parse());
    }

    public static XenRef<Task> async_create(Session session, string _host, Dictionary<string, string> _device_config, long _physical_size, string _name_label, string _name_description, string _type, string _content_type, bool _shared, Dictionary<string, string> _sm_config)
    {
      if (Helper.APIVersionMeets(session, API_Version.API_1_2))
        return XenRef<Task>.Create(session.proxy.async_sr_create(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _physical_size.ToString(), _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", _shared, (object) Maps.convert_to_proxy_string_string(_sm_config)).parse());
      return SR.async_create(session, _host, _device_config, _physical_size, _name_label, _name_description, _type, _content_type, _shared);
    }

    public static XenRef<Task> async_create(Session session, string _host, Dictionary<string, string> _device_config, long _physical_size, string _name_label, string _name_description, string _type, string _content_type, bool _shared)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_create(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _physical_size.ToString(), _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", _shared).parse());
    }

    public static XenRef<SR> introduce(Session session, string _uuid, string _name_label, string _name_description, string _type, string _content_type, bool _shared, Dictionary<string, string> _sm_config)
    {
      if (Helper.APIVersionMeets(session, API_Version.API_1_2))
        return XenRef<SR>.Create(session.proxy.sr_introduce(session.uuid, _uuid != null ? _uuid : "", _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", _shared, (object) Maps.convert_to_proxy_string_string(_sm_config)).parse());
      return SR.introduce(session, _uuid, _name_label, _name_description, _type, _content_type, _shared);
    }

    public static XenRef<SR> introduce(Session session, string _uuid, string _name_label, string _name_description, string _type, string _content_type, bool _shared)
    {
      return XenRef<SR>.Create(session.proxy.sr_introduce(session.uuid, _uuid != null ? _uuid : "", _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", _shared).parse());
    }

    public static XenRef<Task> async_introduce(Session session, string _uuid, string _name_label, string _name_description, string _type, string _content_type, bool _shared, Dictionary<string, string> _sm_config)
    {
      if (Helper.APIVersionMeets(session, API_Version.API_1_2))
        return XenRef<Task>.Create(session.proxy.async_sr_introduce(session.uuid, _uuid != null ? _uuid : "", _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", _shared, (object) Maps.convert_to_proxy_string_string(_sm_config)).parse());
      return SR.async_introduce(session, _uuid, _name_label, _name_description, _type, _content_type, _shared);
    }

    public static XenRef<Task> async_introduce(Session session, string _uuid, string _name_label, string _name_description, string _type, string _content_type, bool _shared)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_introduce(session.uuid, _uuid != null ? _uuid : "", _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", _shared).parse());
    }

    public static string make(Session session, string _host, Dictionary<string, string> _device_config, long _physical_size, string _name_label, string _name_description, string _type, string _content_type, Dictionary<string, string> _sm_config)
    {
      if (Helper.APIVersionMeets(session, API_Version.API_1_2))
        return session.proxy.sr_make(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _physical_size.ToString(), _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", (object) Maps.convert_to_proxy_string_string(_sm_config)).parse();
      return SR.make(session, _host, _device_config, _physical_size, _name_label, _name_description, _type, _content_type);
    }

    public static string make(Session session, string _host, Dictionary<string, string> _device_config, long _physical_size, string _name_label, string _name_description, string _type, string _content_type)
    {
      return session.proxy.sr_make(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _physical_size.ToString(), _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "").parse();
    }

    public static XenRef<Task> async_make(Session session, string _host, Dictionary<string, string> _device_config, long _physical_size, string _name_label, string _name_description, string _type, string _content_type, Dictionary<string, string> _sm_config)
    {
      if (Helper.APIVersionMeets(session, API_Version.API_1_2))
        return XenRef<Task>.Create(session.proxy.async_sr_make(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _physical_size.ToString(), _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "", (object) Maps.convert_to_proxy_string_string(_sm_config)).parse());
      return SR.async_make(session, _host, _device_config, _physical_size, _name_label, _name_description, _type, _content_type);
    }

    public static XenRef<Task> async_make(Session session, string _host, Dictionary<string, string> _device_config, long _physical_size, string _name_label, string _name_description, string _type, string _content_type)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_make(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _physical_size.ToString(), _name_label != null ? _name_label : "", _name_description != null ? _name_description : "", _type != null ? _type : "", _content_type != null ? _content_type : "").parse());
    }

    public static void destroy(Session session, string _sr)
    {
      session.proxy.sr_destroy(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_destroy(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static void forget(Session session, string _sr)
    {
      session.proxy.sr_forget(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_forget(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_forget(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static void update(Session session, string _sr)
    {
      session.proxy.sr_update(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_update(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_update(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static string[] get_supported_types(Session session)
    {
      return session.proxy.sr_get_supported_types(session.uuid).parse();
    }

    public static void scan(Session session, string _sr)
    {
      session.proxy.sr_scan(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_scan(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_scan(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static string probe(Session session, string _host, Dictionary<string, string> _device_config, string _type, Dictionary<string, string> _sm_config)
    {
      return session.proxy.sr_probe(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _type != null ? _type : "", (object) Maps.convert_to_proxy_string_string(_sm_config)).parse();
    }

    public static XenRef<Task> async_probe(Session session, string _host, Dictionary<string, string> _device_config, string _type, Dictionary<string, string> _sm_config)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_probe(session.uuid, _host != null ? _host : "", (object) Maps.convert_to_proxy_string_string(_device_config), _type != null ? _type : "", (object) Maps.convert_to_proxy_string_string(_sm_config)).parse());
    }

    public static void set_shared(Session session, string _sr, bool _value)
    {
      session.proxy.sr_set_shared(session.uuid, _sr != null ? _sr : "", _value).parse();
    }

    public static XenRef<Task> async_set_shared(Session session, string _sr, bool _value)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_set_shared(session.uuid, _sr != null ? _sr : "", _value).parse());
    }

    public static void set_name_label(Session session, string _sr, string _value)
    {
      session.proxy.sr_set_name_label(session.uuid, _sr != null ? _sr : "", _value != null ? _value : "").parse();
    }

    public static XenRef<Task> async_set_name_label(Session session, string _sr, string _value)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_set_name_label(session.uuid, _sr != null ? _sr : "", _value != null ? _value : "").parse());
    }

    public static void set_name_description(Session session, string _sr, string _value)
    {
      session.proxy.sr_set_name_description(session.uuid, _sr != null ? _sr : "", _value != null ? _value : "").parse();
    }

    public static XenRef<Task> async_set_name_description(Session session, string _sr, string _value)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_set_name_description(session.uuid, _sr != null ? _sr : "", _value != null ? _value : "").parse());
    }

    public static XenRef<Blob> create_new_blob(Session session, string _sr, string _name, string _mime_type, bool _public)
    {
      return XenRef<Blob>.Create(session.proxy.sr_create_new_blob(session.uuid, _sr != null ? _sr : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _sr, string _name, string _mime_type, bool _public)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_create_new_blob(session.uuid, _sr != null ? _sr : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static void set_physical_size(Session session, string _self, long _value)
    {
      session.proxy.sr_set_physical_size(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void set_virtual_allocation(Session session, string _self, long _value)
    {
      session.proxy.sr_set_virtual_allocation(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void set_physical_utilisation(Session session, string _self, long _value)
    {
      session.proxy.sr_set_physical_utilisation(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static void assert_can_host_ha_statefile(Session session, string _sr)
    {
      session.proxy.sr_assert_can_host_ha_statefile(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_assert_can_host_ha_statefile(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_assert_can_host_ha_statefile(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static void assert_supports_database_replication(Session session, string _sr)
    {
      session.proxy.sr_assert_supports_database_replication(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_assert_supports_database_replication(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_assert_supports_database_replication(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static void enable_database_replication(Session session, string _sr)
    {
      session.proxy.sr_enable_database_replication(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_enable_database_replication(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_enable_database_replication(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static void disable_database_replication(Session session, string _sr)
    {
      session.proxy.sr_disable_database_replication(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_disable_database_replication(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_disable_database_replication(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static List<XenRef<SR>> get_all(Session session)
    {
      return XenRef<SR>.Create(session.proxy.sr_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<SR>, SR> get_all_records(Session session)
    {
      return XenRef<SR>.Create<Proxy_SR>(session.proxy.sr_get_all_records(session.uuid).parse());
    }

    public static XenRef<Blob> create_new_blob(Session session, string _sr, string _name, string _mime_type)
    {
      return XenRef<Blob>.Create(session.proxy.sr_create_new_blob(session.uuid, _sr != null ? _sr : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _sr, string _name, string _mime_type)
    {
      return XenRef<Task>.Create(session.proxy.async_sr_create_new_blob(session.uuid, _sr != null ? _sr : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }
  }
}
