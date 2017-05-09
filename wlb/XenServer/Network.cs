// Decompiled with JetBrains decompiler
// Type: XenAPI.Network
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Network : XenObject<Network>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private List<network_operations> _allowed_operations;
    private Dictionary<string, network_operations> _current_operations;
    private List<XenRef<VIF>> _VIFs;
    private List<XenRef<PIF>> _PIFs;
    private long _MTU;
    private Dictionary<string, string> _other_config;
    private string _bridge;
    private Dictionary<string, XenRef<Blob>> _blobs;
    private string[] _tags;
    private network_default_locking_mode _default_locking_mode;

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

    public List<network_operations> allowed_operations
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

    public Dictionary<string, network_operations> current_operations
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

    public List<XenRef<VIF>> VIFs
    {
      get
      {
        return this._VIFs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VIFs))
          return;
        this._VIFs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VIFs");
      }
    }

    public List<XenRef<PIF>> PIFs
    {
      get
      {
        return this._PIFs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PIFs))
          return;
        this._PIFs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PIFs");
      }
    }

    public long MTU
    {
      get
      {
        return this._MTU;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._MTU))
          return;
        this._MTU = value;
        this.Changed = true;
        this.NotifyPropertyChanged("MTU");
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

    public string bridge
    {
      get
      {
        return this._bridge;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._bridge))
          return;
        this._bridge = value;
        this.Changed = true;
        this.NotifyPropertyChanged("bridge");
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

    public network_default_locking_mode default_locking_mode
    {
      get
      {
        return this._default_locking_mode;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._default_locking_mode))
          return;
        this._default_locking_mode = value;
        this.Changed = true;
        this.NotifyPropertyChanged("default_locking_mode");
      }
    }

    public Network()
    {
    }

    public Network(string uuid, string name_label, string name_description, List<network_operations> allowed_operations, Dictionary<string, network_operations> current_operations, List<XenRef<VIF>> VIFs, List<XenRef<PIF>> PIFs, long MTU, Dictionary<string, string> other_config, string bridge, Dictionary<string, XenRef<Blob>> blobs, string[] tags, network_default_locking_mode default_locking_mode)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.allowed_operations = allowed_operations;
      this.current_operations = current_operations;
      this.VIFs = VIFs;
      this.PIFs = PIFs;
      this.MTU = MTU;
      this.other_config = other_config;
      this.bridge = bridge;
      this.blobs = blobs;
      this.tags = tags;
      this.default_locking_mode = default_locking_mode;
    }

    public Network(Proxy_Network proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Network(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.allowed_operations = Helper.StringArrayToEnumList<network_operations>(Marshalling.ParseStringArray(table, "allowed_operations"));
      this.current_operations = Maps.convert_from_proxy_string_network_operations((object) Marshalling.ParseHashTable(table, "current_operations"));
      this.VIFs = Marshalling.ParseSetRef<VIF>(table, "VIFs");
      this.PIFs = Marshalling.ParseSetRef<PIF>(table, "PIFs");
      this.MTU = Marshalling.ParseLong(table, "MTU");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
      this.bridge = Marshalling.ParseString(table, "bridge");
      this.blobs = Maps.convert_from_proxy_string_XenRefBlob((object) Marshalling.ParseHashTable(table, "blobs"));
      this.tags = Marshalling.ParseStringArray(table, "tags");
      this.default_locking_mode = (network_default_locking_mode) Helper.EnumParseDefault(typeof (network_default_locking_mode), Marshalling.ParseString(table, "default_locking_mode"));
    }

    public override void UpdateFrom(Network update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.allowed_operations = update.allowed_operations;
      this.current_operations = update.current_operations;
      this.VIFs = update.VIFs;
      this.PIFs = update.PIFs;
      this.MTU = update.MTU;
      this.other_config = update.other_config;
      this.bridge = update.bridge;
      this.blobs = update.blobs;
      this.tags = update.tags;
      this.default_locking_mode = update.default_locking_mode;
    }

    internal void UpdateFromProxy(Proxy_Network proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.allowed_operations = proxy.allowed_operations == null ? (List<network_operations>) null : Helper.StringArrayToEnumList<network_operations>(proxy.allowed_operations);
      this.current_operations = proxy.current_operations == null ? (Dictionary<string, network_operations>) null : Maps.convert_from_proxy_string_network_operations(proxy.current_operations);
      this.VIFs = proxy.VIFs == null ? (List<XenRef<VIF>>) null : XenRef<VIF>.Create(proxy.VIFs);
      this.PIFs = proxy.PIFs == null ? (List<XenRef<PIF>>) null : XenRef<PIF>.Create(proxy.PIFs);
      this.MTU = proxy.MTU == null ? 0L : long.Parse(proxy.MTU);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
      this.bridge = proxy.bridge == null ? (string) null : proxy.bridge;
      this.blobs = proxy.blobs == null ? (Dictionary<string, XenRef<Blob>>) null : Maps.convert_from_proxy_string_XenRefBlob(proxy.blobs);
      this.tags = proxy.tags == null ? new string[0] : proxy.tags;
      this.default_locking_mode = proxy.default_locking_mode == null ? network_default_locking_mode.unlocked : (network_default_locking_mode) Helper.EnumParseDefault(typeof (network_default_locking_mode), proxy.default_locking_mode);
    }

    public Proxy_Network ToProxy()
    {
      return new Proxy_Network()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        allowed_operations = this.allowed_operations != null ? Helper.ObjectListToStringArray<network_operations>(this.allowed_operations) : new string[0],
        current_operations = (object) Maps.convert_to_proxy_string_network_operations(this.current_operations),
        VIFs = this.VIFs != null ? Helper.RefListToStringArray<VIF>(this.VIFs) : new string[0],
        PIFs = this.PIFs != null ? Helper.RefListToStringArray<PIF>(this.PIFs) : new string[0],
        MTU = this.MTU.ToString(),
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config),
        bridge = this.bridge != null ? this.bridge : "",
        blobs = (object) Maps.convert_to_proxy_string_XenRefBlob(this.blobs),
        tags = this.tags,
        default_locking_mode = network_default_locking_mode_helper.ToString(this.default_locking_mode)
      };
    }

    public bool DeepEquals(Network other, bool ignoreCurrentOperations)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (!ignoreCurrentOperations && !Helper.AreEqual2<Dictionary<string, network_operations>>(this.current_operations, other.current_operations) || (!Helper.AreEqual2<string>(this._uuid, other._uuid) || !Helper.AreEqual2<string>(this._name_label, other._name_label)) || (!Helper.AreEqual2<string>(this._name_description, other._name_description) || !Helper.AreEqual2<List<network_operations>>(this._allowed_operations, other._allowed_operations) || (!Helper.AreEqual2<List<XenRef<VIF>>>(this._VIFs, other._VIFs) || !Helper.AreEqual2<List<XenRef<PIF>>>(this._PIFs, other._PIFs))) || (!Helper.AreEqual2<long>(this._MTU, other._MTU) || !Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config) || (!Helper.AreEqual2<string>(this._bridge, other._bridge) || !Helper.AreEqual2<Dictionary<string, XenRef<Blob>>>(this._blobs, other._blobs)) || !Helper.AreEqual2<string[]>(this._tags, other._tags)))
        return false;
      return Helper.AreEqual2<network_default_locking_mode>(this._default_locking_mode, other._default_locking_mode);
    }

    public override string SaveChanges(Session session, string opaqueRef, Network server)
    {
      if (opaqueRef == null)
      {
        Proxy_Network _record = this.ToProxy();
        return session.proxy.network_create(session.uuid, _record).parse();
      }
      if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
        Network.set_name_label(session, opaqueRef, this._name_label);
      if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
        Network.set_name_description(session, opaqueRef, this._name_description);
      if (!Helper.AreEqual2<long>(this._MTU, server._MTU))
        Network.set_MTU(session, opaqueRef, this._MTU);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Network.set_other_config(session, opaqueRef, this._other_config);
      if (!Helper.AreEqual2<string[]>(this._tags, server._tags))
        Network.set_tags(session, opaqueRef, this._tags);
      return (string) null;
    }

    public static Network get_record(Session session, string _network)
    {
      return new Network(session.proxy.network_get_record(session.uuid, _network != null ? _network : "").parse());
    }

    public static XenRef<Network> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Network>.Create(session.proxy.network_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<Network> create(Session session, Network _record)
    {
      return XenRef<Network>.Create(session.proxy.network_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, Network _record)
    {
      return XenRef<Task>.Create(session.proxy.async_network_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _network)
    {
      session.proxy.network_destroy(session.uuid, _network != null ? _network : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _network)
    {
      return XenRef<Task>.Create(session.proxy.async_network_destroy(session.uuid, _network != null ? _network : "").parse());
    }

    public static List<XenRef<Network>> get_by_name_label(Session session, string _label)
    {
      return XenRef<Network>.Create(session.proxy.network_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _network)
    {
      return session.proxy.network_get_uuid(session.uuid, _network != null ? _network : "").parse();
    }

    public static string get_name_label(Session session, string _network)
    {
      return session.proxy.network_get_name_label(session.uuid, _network != null ? _network : "").parse();
    }

    public static string get_name_description(Session session, string _network)
    {
      return session.proxy.network_get_name_description(session.uuid, _network != null ? _network : "").parse();
    }

    public static List<network_operations> get_allowed_operations(Session session, string _network)
    {
      return Helper.StringArrayToEnumList<network_operations>(session.proxy.network_get_allowed_operations(session.uuid, _network != null ? _network : "").parse());
    }

    public static Dictionary<string, network_operations> get_current_operations(Session session, string _network)
    {
      return Maps.convert_from_proxy_string_network_operations(session.proxy.network_get_current_operations(session.uuid, _network != null ? _network : "").parse());
    }

    public static List<XenRef<VIF>> get_VIFs(Session session, string _network)
    {
      return XenRef<VIF>.Create(session.proxy.network_get_vifs(session.uuid, _network != null ? _network : "").parse());
    }

    public static List<XenRef<PIF>> get_PIFs(Session session, string _network)
    {
      return XenRef<PIF>.Create(session.proxy.network_get_pifs(session.uuid, _network != null ? _network : "").parse());
    }

    public static long get_MTU(Session session, string _network)
    {
      return long.Parse(session.proxy.network_get_mtu(session.uuid, _network != null ? _network : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _network)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.network_get_other_config(session.uuid, _network != null ? _network : "").parse());
    }

    public static string get_bridge(Session session, string _network)
    {
      return session.proxy.network_get_bridge(session.uuid, _network != null ? _network : "").parse();
    }

    public static Dictionary<string, XenRef<Blob>> get_blobs(Session session, string _network)
    {
      return Maps.convert_from_proxy_string_XenRefBlob(session.proxy.network_get_blobs(session.uuid, _network != null ? _network : "").parse());
    }

    public static string[] get_tags(Session session, string _network)
    {
      return session.proxy.network_get_tags(session.uuid, _network != null ? _network : "").parse();
    }

    public static network_default_locking_mode get_default_locking_mode(Session session, string _network)
    {
      return (network_default_locking_mode) Helper.EnumParseDefault(typeof (network_default_locking_mode), session.proxy.network_get_default_locking_mode(session.uuid, _network != null ? _network : "").parse());
    }

    public static void set_name_label(Session session, string _network, string _label)
    {
      session.proxy.network_set_name_label(session.uuid, _network != null ? _network : "", _label != null ? _label : "").parse();
    }

    public static void set_name_description(Session session, string _network, string _description)
    {
      session.proxy.network_set_name_description(session.uuid, _network != null ? _network : "", _description != null ? _description : "").parse();
    }

    public static void set_MTU(Session session, string _network, long _mtu)
    {
      session.proxy.network_set_mtu(session.uuid, _network != null ? _network : "", _mtu.ToString()).parse();
    }

    public static void set_other_config(Session session, string _network, Dictionary<string, string> _other_config)
    {
      session.proxy.network_set_other_config(session.uuid, _network != null ? _network : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _network, string _key, string _value)
    {
      session.proxy.network_add_to_other_config(session.uuid, _network != null ? _network : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _network, string _key)
    {
      session.proxy.network_remove_from_other_config(session.uuid, _network != null ? _network : "", _key != null ? _key : "").parse();
    }

    public static void set_tags(Session session, string _network, string[] _tags)
    {
      session.proxy.network_set_tags(session.uuid, _network != null ? _network : "", _tags).parse();
    }

    public static void add_tags(Session session, string _network, string _value)
    {
      session.proxy.network_add_tags(session.uuid, _network != null ? _network : "", _value != null ? _value : "").parse();
    }

    public static void remove_tags(Session session, string _network, string _value)
    {
      session.proxy.network_remove_tags(session.uuid, _network != null ? _network : "", _value != null ? _value : "").parse();
    }

    public static XenRef<Blob> create_new_blob(Session session, string _network, string _name, string _mime_type, bool _public)
    {
      return XenRef<Blob>.Create(session.proxy.network_create_new_blob(session.uuid, _network != null ? _network : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _network, string _name, string _mime_type, bool _public)
    {
      return XenRef<Task>.Create(session.proxy.async_network_create_new_blob(session.uuid, _network != null ? _network : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static void set_default_locking_mode(Session session, string _network, network_default_locking_mode _value)
    {
      session.proxy.network_set_default_locking_mode(session.uuid, _network != null ? _network : "", network_default_locking_mode_helper.ToString(_value)).parse();
    }

    public static XenRef<Task> async_set_default_locking_mode(Session session, string _network, network_default_locking_mode _value)
    {
      return XenRef<Task>.Create(session.proxy.async_network_set_default_locking_mode(session.uuid, _network != null ? _network : "", network_default_locking_mode_helper.ToString(_value)).parse());
    }

    public static List<XenRef<Network>> get_all(Session session)
    {
      return XenRef<Network>.Create(session.proxy.network_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Network>, Network> get_all_records(Session session)
    {
      return XenRef<Network>.Create<Proxy_Network>(session.proxy.network_get_all_records(session.uuid).parse());
    }

    public static XenRef<Blob> create_new_blob(Session session, string _network, string _name, string _mime_type)
    {
      return XenRef<Blob>.Create(session.proxy.network_create_new_blob(session.uuid, _network != null ? _network : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _network, string _name, string _mime_type)
    {
      return XenRef<Task>.Create(session.proxy.async_network_create_new_blob(session.uuid, _network != null ? _network : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }
  }
}
