// Decompiled with JetBrains decompiler
// Type: XenAPI.VM_appliance
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class VM_appliance : XenObject<VM_appliance>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private List<vm_appliance_operation> _allowed_operations;
    private Dictionary<string, vm_appliance_operation> _current_operations;
    private List<XenRef<VM>> _VMs;

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

    public List<vm_appliance_operation> allowed_operations
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

    public Dictionary<string, vm_appliance_operation> current_operations
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

    public List<XenRef<VM>> VMs
    {
      get
      {
        return this._VMs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._VMs))
          return;
        this._VMs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("VMs");
      }
    }

    public VM_appliance()
    {
    }

    public VM_appliance(string uuid, string name_label, string name_description, List<vm_appliance_operation> allowed_operations, Dictionary<string, vm_appliance_operation> current_operations, List<XenRef<VM>> VMs)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.allowed_operations = allowed_operations;
      this.current_operations = current_operations;
      this.VMs = VMs;
    }

    public VM_appliance(Proxy_VM_appliance proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public VM_appliance(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.allowed_operations = Helper.StringArrayToEnumList<vm_appliance_operation>(Marshalling.ParseStringArray(table, "allowed_operations"));
      this.current_operations = Maps.convert_from_proxy_string_vm_appliance_operation((object) Marshalling.ParseHashTable(table, "current_operations"));
      this.VMs = Marshalling.ParseSetRef<VM>(table, "VMs");
    }

    public override void UpdateFrom(VM_appliance update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.allowed_operations = update.allowed_operations;
      this.current_operations = update.current_operations;
      this.VMs = update.VMs;
    }

    internal void UpdateFromProxy(Proxy_VM_appliance proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.allowed_operations = proxy.allowed_operations == null ? (List<vm_appliance_operation>) null : Helper.StringArrayToEnumList<vm_appliance_operation>(proxy.allowed_operations);
      this.current_operations = proxy.current_operations == null ? (Dictionary<string, vm_appliance_operation>) null : Maps.convert_from_proxy_string_vm_appliance_operation(proxy.current_operations);
      this.VMs = proxy.VMs == null ? (List<XenRef<VM>>) null : XenRef<VM>.Create(proxy.VMs);
    }

    public Proxy_VM_appliance ToProxy()
    {
      return new Proxy_VM_appliance()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        allowed_operations = this.allowed_operations != null ? Helper.ObjectListToStringArray<vm_appliance_operation>(this.allowed_operations) : new string[0],
        current_operations = (object) Maps.convert_to_proxy_string_vm_appliance_operation(this.current_operations),
        VMs = this.VMs != null ? Helper.RefListToStringArray<VM>(this.VMs) : new string[0]
      };
    }

    public bool DeepEquals(VM_appliance other, bool ignoreCurrentOperations)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (!ignoreCurrentOperations && !Helper.AreEqual2<Dictionary<string, vm_appliance_operation>>(this.current_operations, other.current_operations) || (!Helper.AreEqual2<string>(this._uuid, other._uuid) || !Helper.AreEqual2<string>(this._name_label, other._name_label)) || (!Helper.AreEqual2<string>(this._name_description, other._name_description) || !Helper.AreEqual2<List<vm_appliance_operation>>(this._allowed_operations, other._allowed_operations)))
        return false;
      return Helper.AreEqual2<List<XenRef<VM>>>(this._VMs, other._VMs);
    }

    public override string SaveChanges(Session session, string opaqueRef, VM_appliance server)
    {
      if (opaqueRef == null)
      {
        Proxy_VM_appliance _record = this.ToProxy();
        return session.proxy.vm_appliance_create(session.uuid, _record).parse();
      }
      if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
        VM_appliance.set_name_label(session, opaqueRef, this._name_label);
      if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
        VM_appliance.set_name_description(session, opaqueRef, this._name_description);
      return (string) null;
    }

    public static VM_appliance get_record(Session session, string _vm_appliance)
    {
      return new VM_appliance(session.proxy.vm_appliance_get_record(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse());
    }

    public static XenRef<VM_appliance> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<VM_appliance>.Create(session.proxy.vm_appliance_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<VM_appliance> create(Session session, VM_appliance _record)
    {
      return XenRef<VM_appliance>.Create(session.proxy.vm_appliance_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, VM_appliance _record)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_appliance_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _vm_appliance)
    {
      session.proxy.vm_appliance_destroy(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _vm_appliance)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_appliance_destroy(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse());
    }

    public static List<XenRef<VM_appliance>> get_by_name_label(Session session, string _label)
    {
      return XenRef<VM_appliance>.Create(session.proxy.vm_appliance_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _vm_appliance)
    {
      return session.proxy.vm_appliance_get_uuid(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse();
    }

    public static string get_name_label(Session session, string _vm_appliance)
    {
      return session.proxy.vm_appliance_get_name_label(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse();
    }

    public static string get_name_description(Session session, string _vm_appliance)
    {
      return session.proxy.vm_appliance_get_name_description(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse();
    }

    public static List<vm_appliance_operation> get_allowed_operations(Session session, string _vm_appliance)
    {
      return Helper.StringArrayToEnumList<vm_appliance_operation>(session.proxy.vm_appliance_get_allowed_operations(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse());
    }

    public static Dictionary<string, vm_appliance_operation> get_current_operations(Session session, string _vm_appliance)
    {
      return Maps.convert_from_proxy_string_vm_appliance_operation(session.proxy.vm_appliance_get_current_operations(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse());
    }

    public static List<XenRef<VM>> get_VMs(Session session, string _vm_appliance)
    {
      return XenRef<VM>.Create(session.proxy.vm_appliance_get_vms(session.uuid, _vm_appliance != null ? _vm_appliance : "").parse());
    }

    public static void set_name_label(Session session, string _vm_appliance, string _label)
    {
      session.proxy.vm_appliance_set_name_label(session.uuid, _vm_appliance != null ? _vm_appliance : "", _label != null ? _label : "").parse();
    }

    public static void set_name_description(Session session, string _vm_appliance, string _description)
    {
      session.proxy.vm_appliance_set_name_description(session.uuid, _vm_appliance != null ? _vm_appliance : "", _description != null ? _description : "").parse();
    }

    public static void start(Session session, string _self, bool _paused)
    {
      session.proxy.vm_appliance_start(session.uuid, _self != null ? _self : "", _paused).parse();
    }

    public static XenRef<Task> async_start(Session session, string _self, bool _paused)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_appliance_start(session.uuid, _self != null ? _self : "", _paused).parse());
    }

    public static void clean_shutdown(Session session, string _self)
    {
      session.proxy.vm_appliance_clean_shutdown(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_clean_shutdown(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_appliance_clean_shutdown(session.uuid, _self != null ? _self : "").parse());
    }

    public static void hard_shutdown(Session session, string _self)
    {
      session.proxy.vm_appliance_hard_shutdown(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_hard_shutdown(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_appliance_hard_shutdown(session.uuid, _self != null ? _self : "").parse());
    }

    public static void shutdown(Session session, string _self)
    {
      session.proxy.vm_appliance_shutdown(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_shutdown(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_appliance_shutdown(session.uuid, _self != null ? _self : "").parse());
    }

    public static void assert_can_be_recovered(Session session, string _self, string _session_to)
    {
      session.proxy.vm_appliance_assert_can_be_recovered(session.uuid, _self != null ? _self : "", _session_to != null ? _session_to : "").parse();
    }

    public static XenRef<Task> async_assert_can_be_recovered(Session session, string _self, string _session_to)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_appliance_assert_can_be_recovered(session.uuid, _self != null ? _self : "", _session_to != null ? _session_to : "").parse());
    }

    public static void recover(Session session, string _self, string _session_to, bool _force)
    {
      session.proxy.vm_appliance_recover(session.uuid, _self != null ? _self : "", _session_to != null ? _session_to : "", _force).parse();
    }

    public static XenRef<Task> async_recover(Session session, string _self, string _session_to, bool _force)
    {
      return XenRef<Task>.Create(session.proxy.async_vm_appliance_recover(session.uuid, _self != null ? _self : "", _session_to != null ? _session_to : "", _force).parse());
    }

    public static List<XenRef<VM_appliance>> get_all(Session session)
    {
      return XenRef<VM_appliance>.Create(session.proxy.vm_appliance_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<VM_appliance>, VM_appliance> get_all_records(Session session)
    {
      return XenRef<VM_appliance>.Create<Proxy_VM_appliance>(session.proxy.vm_appliance_get_all_records(session.uuid).parse());
    }
  }
}
