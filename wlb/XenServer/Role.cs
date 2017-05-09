// Decompiled with JetBrains decompiler
// Type: XenAPI.Role
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Role : XenObject<Role>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private List<XenRef<Role>> _subroles;

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

    public List<XenRef<Role>> subroles
    {
      get
      {
        return this._subroles;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._subroles))
          return;
        this._subroles = value;
        this.Changed = true;
        this.NotifyPropertyChanged("subroles");
      }
    }

    public Role()
    {
    }

    public Role(string uuid, string name_label, string name_description, List<XenRef<Role>> subroles)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.subroles = subroles;
    }

    public Role(Proxy_Role proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Role(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.subroles = Marshalling.ParseSetRef<Role>(table, "subroles");
    }

    public override void UpdateFrom(Role update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.subroles = update.subroles;
    }

    internal void UpdateFromProxy(Proxy_Role proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.subroles = proxy.subroles == null ? (List<XenRef<Role>>) null : XenRef<Role>.Create(proxy.subroles);
    }

    public Proxy_Role ToProxy()
    {
      return new Proxy_Role()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        subroles = this.subroles != null ? Helper.RefListToStringArray<Role>(this.subroles) : new string[0]
      };
    }

    public bool DeepEquals(Role other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._name_label, other._name_label) && Helper.AreEqual2<string>(this._name_description, other._name_description))
        return Helper.AreEqual2<List<XenRef<Role>>>(this._subroles, other._subroles);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Role server)
    {
      if (opaqueRef == null)
        return "";
      throw new InvalidOperationException("This type has no read/write properties");
    }

    public static Role get_record(Session session, string _role)
    {
      return new Role(session.proxy.role_get_record(session.uuid, _role != null ? _role : "").parse());
    }

    public static XenRef<Role> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Role>.Create(session.proxy.role_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static List<XenRef<Role>> get_by_name_label(Session session, string _label)
    {
      return XenRef<Role>.Create(session.proxy.role_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _role)
    {
      return session.proxy.role_get_uuid(session.uuid, _role != null ? _role : "").parse();
    }

    public static string get_name_label(Session session, string _role)
    {
      return session.proxy.role_get_name_label(session.uuid, _role != null ? _role : "").parse();
    }

    public static string get_name_description(Session session, string _role)
    {
      return session.proxy.role_get_name_description(session.uuid, _role != null ? _role : "").parse();
    }

    public static List<XenRef<Role>> get_subroles(Session session, string _role)
    {
      return XenRef<Role>.Create(session.proxy.role_get_subroles(session.uuid, _role != null ? _role : "").parse());
    }

    public static List<XenRef<Role>> get_permissions(Session session, string _self)
    {
      return XenRef<Role>.Create(session.proxy.role_get_permissions(session.uuid, _self != null ? _self : "").parse());
    }

    public static string[] get_permissions_name_label(Session session, string _self)
    {
      return session.proxy.role_get_permissions_name_label(session.uuid, _self != null ? _self : "").parse();
    }

    public static List<XenRef<Role>> get_by_permission(Session session, string _permission)
    {
      return XenRef<Role>.Create(session.proxy.role_get_by_permission(session.uuid, _permission != null ? _permission : "").parse());
    }

    public static List<XenRef<Role>> get_by_permission_name_label(Session session, string _label)
    {
      return XenRef<Role>.Create(session.proxy.role_get_by_permission_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static List<XenRef<Role>> get_all(Session session)
    {
      return XenRef<Role>.Create(session.proxy.role_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Role>, Role> get_all_records(Session session)
    {
      return XenRef<Role>.Create<Proxy_Role>(session.proxy.role_get_all_records(session.uuid).parse());
    }
  }
}
