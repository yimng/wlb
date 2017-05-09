// Decompiled with JetBrains decompiler
// Type: XenAPI.GPU_group
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class GPU_group : XenObject<GPU_group>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private List<XenRef<PGPU>> _PGPUs;
    private List<XenRef<VGPU>> _VGPUs;
    private string[] _GPU_types;
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

    public List<XenRef<PGPU>> PGPUs
    {
      get
      {
        return this._PGPUs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._PGPUs))
          return;
        this._PGPUs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("PGPUs");
      }
    }

    public List<XenRef<VGPU>> VGPUs
    {
      get
      {
        return this._VGPUs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VGPUs))
          return;
        this._VGPUs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VGPUs");
      }
    }

    public string[] GPU_types
    {
      get
      {
        return this._GPU_types;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._GPU_types))
          return;
        this._GPU_types = value;
        this.Changed = true;
        this.NotifyPropertyChanged("GPU_types");
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

    public GPU_group()
    {
    }

    public GPU_group(string uuid, string name_label, string name_description, List<XenRef<PGPU>> PGPUs, List<XenRef<VGPU>> VGPUs, string[] GPU_types, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.PGPUs = PGPUs;
      this.VGPUs = VGPUs;
      this.GPU_types = GPU_types;
      this.other_config = other_config;
    }

    public GPU_group(Proxy_GPU_group proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public GPU_group(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.PGPUs = Marshalling.ParseSetRef<PGPU>(table, "PGPUs");
      this.VGPUs = Marshalling.ParseSetRef<VGPU>(table, "VGPUs");
      this.GPU_types = Marshalling.ParseStringArray(table, "GPU_types");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(GPU_group update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.PGPUs = update.PGPUs;
      this.VGPUs = update.VGPUs;
      this.GPU_types = update.GPU_types;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_GPU_group proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.PGPUs = proxy.PGPUs == null ? (List<XenRef<PGPU>>) null : XenRef<PGPU>.Create(proxy.PGPUs);
      this.VGPUs = proxy.VGPUs == null ? (List<XenRef<VGPU>>) null : XenRef<VGPU>.Create(proxy.VGPUs);
      this.GPU_types = proxy.GPU_types == null ? new string[0] : proxy.GPU_types;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_GPU_group ToProxy()
    {
      return new Proxy_GPU_group()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        PGPUs = this.PGPUs != null ? Helper.RefListToStringArray<PGPU>(this.PGPUs) : new string[0],
        VGPUs = this.VGPUs != null ? Helper.RefListToStringArray<VGPU>(this.VGPUs) : new string[0],
        GPU_types = this.GPU_types,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(GPU_group other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._name_label, other._name_label) && (Helper.AreEqual2<string>(this._name_description, other._name_description) && Helper.AreEqual2<List<XenRef<PGPU>>>(this._PGPUs, other._PGPUs)) && (Helper.AreEqual2<List<XenRef<VGPU>>>(this._VGPUs, other._VGPUs) && Helper.AreEqual2<string[]>(this._GPU_types, other._GPU_types)))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, GPU_group server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
        GPU_group.set_name_label(session, opaqueRef, this._name_label);
      if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
        GPU_group.set_name_description(session, opaqueRef, this._name_description);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        GPU_group.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static GPU_group get_record(Session session, string _gpu_group)
    {
      return new GPU_group(session.proxy.gpu_group_get_record(session.uuid, _gpu_group != null ? _gpu_group : "").parse());
    }

    public static XenRef<GPU_group> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<GPU_group>.Create(session.proxy.gpu_group_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static List<XenRef<GPU_group>> get_by_name_label(Session session, string _label)
    {
      return XenRef<GPU_group>.Create(session.proxy.gpu_group_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _gpu_group)
    {
      return session.proxy.gpu_group_get_uuid(session.uuid, _gpu_group != null ? _gpu_group : "").parse();
    }

    public static string get_name_label(Session session, string _gpu_group)
    {
      return session.proxy.gpu_group_get_name_label(session.uuid, _gpu_group != null ? _gpu_group : "").parse();
    }

    public static string get_name_description(Session session, string _gpu_group)
    {
      return session.proxy.gpu_group_get_name_description(session.uuid, _gpu_group != null ? _gpu_group : "").parse();
    }

    public static List<XenRef<PGPU>> get_PGPUs(Session session, string _gpu_group)
    {
      return XenRef<PGPU>.Create(session.proxy.gpu_group_get_pgpus(session.uuid, _gpu_group != null ? _gpu_group : "").parse());
    }

    public static List<XenRef<VGPU>> get_VGPUs(Session session, string _gpu_group)
    {
      return XenRef<VGPU>.Create(session.proxy.gpu_group_get_vgpus(session.uuid, _gpu_group != null ? _gpu_group : "").parse());
    }

    public static string[] get_GPU_types(Session session, string _gpu_group)
    {
      return session.proxy.gpu_group_get_gpu_types(session.uuid, _gpu_group != null ? _gpu_group : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _gpu_group)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.gpu_group_get_other_config(session.uuid, _gpu_group != null ? _gpu_group : "").parse());
    }

    public static void set_name_label(Session session, string _gpu_group, string _label)
    {
      session.proxy.gpu_group_set_name_label(session.uuid, _gpu_group != null ? _gpu_group : "", _label != null ? _label : "").parse();
    }

    public static void set_name_description(Session session, string _gpu_group, string _description)
    {
      session.proxy.gpu_group_set_name_description(session.uuid, _gpu_group != null ? _gpu_group : "", _description != null ? _description : "").parse();
    }

    public static void set_other_config(Session session, string _gpu_group, Dictionary<string, string> _other_config)
    {
      session.proxy.gpu_group_set_other_config(session.uuid, _gpu_group != null ? _gpu_group : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _gpu_group, string _key, string _value)
    {
      session.proxy.gpu_group_add_to_other_config(session.uuid, _gpu_group != null ? _gpu_group : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _gpu_group, string _key)
    {
      session.proxy.gpu_group_remove_from_other_config(session.uuid, _gpu_group != null ? _gpu_group : "", _key != null ? _key : "").parse();
    }

    public static List<XenRef<GPU_group>> get_all(Session session)
    {
      return XenRef<GPU_group>.Create(session.proxy.gpu_group_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<GPU_group>, GPU_group> get_all_records(Session session)
    {
      return XenRef<GPU_group>.Create<Proxy_GPU_group>(session.proxy.gpu_group_get_all_records(session.uuid).parse());
    }
  }
}
