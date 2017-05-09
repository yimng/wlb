﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.VGPU
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class VGPU : XenObject<VGPU>
  {
    private string _uuid;
    private XenRef<XenAPI.VM> _VM;
    private XenRef<XenAPI.GPU_group> _GPU_group;
    private string _device;
    private bool _currently_attached;
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

    public XenRef<XenAPI.GPU_group> GPU_group
    {
      get
      {
        return this._GPU_group;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._GPU_group))
          return;
        this._GPU_group = value;
        this.Changed = true;
        this.NotifyPropertyChanged("GPU_group");
      }
    }

    public string device
    {
      get
      {
        return this._device;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._device))
          return;
        this._device = value;
        this.Changed = true;
        this.NotifyPropertyChanged("device");
      }
    }

    public bool currently_attached
    {
      get
      {
        return this._currently_attached;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._currently_attached ? 1 : 0)))
          return;
        this._currently_attached = value;
        this.Changed = true;
        this.NotifyPropertyChanged("currently_attached");
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

    public VGPU()
    {
    }

    public VGPU(string uuid, XenRef<XenAPI.VM> VM, XenRef<XenAPI.GPU_group> GPU_group, string device, bool currently_attached, Dictionary<string, string> other_config)
    {
      this.uuid = uuid;
      this.VM = VM;
      this.GPU_group = GPU_group;
      this.device = device;
      this.currently_attached = currently_attached;
      this.other_config = other_config;
    }

    public VGPU(Proxy_VGPU proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public VGPU(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.VM = Marshalling.ParseRef<XenAPI.VM>(table, "VM");
      this.GPU_group = Marshalling.ParseRef<XenAPI.GPU_group>(table, "GPU_group");
      this.device = Marshalling.ParseString(table, "device");
      this.currently_attached = Marshalling.ParseBool(table, "currently_attached");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
    }

    public override void UpdateFrom(VGPU update)
    {
      this.uuid = update.uuid;
      this.VM = update.VM;
      this.GPU_group = update.GPU_group;
      this.device = update.device;
      this.currently_attached = update.currently_attached;
      this.other_config = update.other_config;
    }

    internal void UpdateFromProxy(Proxy_VGPU proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.VM = proxy.VM == null ? (XenRef<XenAPI.VM>) null : XenRef<XenAPI.VM>.Create(proxy.VM);
      this.GPU_group = proxy.GPU_group == null ? (XenRef<XenAPI.GPU_group>) null : XenRef<XenAPI.GPU_group>.Create(proxy.GPU_group);
      this.device = proxy.device == null ? (string) null : proxy.device;
      this.currently_attached = proxy.currently_attached;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
    }

    public Proxy_VGPU ToProxy()
    {
      return new Proxy_VGPU()
      {
        uuid = this.uuid != null ? this.uuid : "",
        VM = this.VM != null ? (string) this.VM : "",
        GPU_group = this.GPU_group != null ? (string) this.GPU_group : "",
        device = this.device != null ? this.device : "",
        currently_attached = this.currently_attached,
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config)
      };
    }

    public bool DeepEquals(VGPU other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<XenRef<XenAPI.VM>>(this._VM, other._VM) && (Helper.AreEqual2<XenRef<XenAPI.GPU_group>>(this._GPU_group, other._GPU_group) && Helper.AreEqual2<string>(this._device, other._device)) && Helper.AreEqual2<bool>(this._currently_attached, other._currently_attached))
        return Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, VGPU server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        VGPU.set_other_config(session, opaqueRef, this._other_config);
      return (string) null;
    }

    public static VGPU get_record(Session session, string _vgpu)
    {
      return new VGPU(session.proxy.vgpu_get_record(session.uuid, _vgpu != null ? _vgpu : "").parse());
    }

    public static XenRef<VGPU> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<VGPU>.Create(session.proxy.vgpu_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _vgpu)
    {
      return session.proxy.vgpu_get_uuid(session.uuid, _vgpu != null ? _vgpu : "").parse();
    }

    public static XenRef<XenAPI.VM> get_VM(Session session, string _vgpu)
    {
      return XenRef<XenAPI.VM>.Create(session.proxy.vgpu_get_vm(session.uuid, _vgpu != null ? _vgpu : "").parse());
    }

    public static XenRef<XenAPI.GPU_group> get_GPU_group(Session session, string _vgpu)
    {
      return XenRef<XenAPI.GPU_group>.Create(session.proxy.vgpu_get_gpu_group(session.uuid, _vgpu != null ? _vgpu : "").parse());
    }

    public static string get_device(Session session, string _vgpu)
    {
      return session.proxy.vgpu_get_device(session.uuid, _vgpu != null ? _vgpu : "").parse();
    }

    public static bool get_currently_attached(Session session, string _vgpu)
    {
      return session.proxy.vgpu_get_currently_attached(session.uuid, _vgpu != null ? _vgpu : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _vgpu)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.vgpu_get_other_config(session.uuid, _vgpu != null ? _vgpu : "").parse());
    }

    public static void set_other_config(Session session, string _vgpu, Dictionary<string, string> _other_config)
    {
      session.proxy.vgpu_set_other_config(session.uuid, _vgpu != null ? _vgpu : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _vgpu, string _key, string _value)
    {
      session.proxy.vgpu_add_to_other_config(session.uuid, _vgpu != null ? _vgpu : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _vgpu, string _key)
    {
      session.proxy.vgpu_remove_from_other_config(session.uuid, _vgpu != null ? _vgpu : "", _key != null ? _key : "").parse();
    }

    public static XenRef<VGPU> create(Session session, string _vm, string _gpu_group, string _device, Dictionary<string, string> _other_config)
    {
      return XenRef<VGPU>.Create(session.proxy.vgpu_create(session.uuid, _vm != null ? _vm : "", _gpu_group != null ? _gpu_group : "", _device != null ? _device : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse());
    }

    public static XenRef<Task> async_create(Session session, string _vm, string _gpu_group, string _device, Dictionary<string, string> _other_config)
    {
      return XenRef<Task>.Create(session.proxy.async_vgpu_create(session.uuid, _vm != null ? _vm : "", _gpu_group != null ? _gpu_group : "", _device != null ? _device : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse());
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.vgpu_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vgpu_destroy(session.uuid, _self != null ? _self : "").parse());
    }

    public static List<XenRef<VGPU>> get_all(Session session)
    {
      return XenRef<VGPU>.Create(session.proxy.vgpu_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<VGPU>, VGPU> get_all_records(Session session)
    {
      return XenRef<VGPU>.Create<Proxy_VGPU>(session.proxy.vgpu_get_all_records(session.uuid).parse());
    }
  }
}
