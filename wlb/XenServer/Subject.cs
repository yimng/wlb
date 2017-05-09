// Decompiled with JetBrains decompiler
// Type: XenAPI.Subject
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Subject : XenObject<Subject>
  {
    public static readonly string SUBJECT_NAME_KEY = "subject-name";
    public static readonly string SUBJECT_DISPLAYNAME_KEY = "subject-displayname";
    public static readonly string SUBJECT_IS_GROUP_KEY = "subject-is-group";
    private string _uuid;
    private string _subject_identifier;
    private Dictionary<string, string> _other_config;
    private List<XenRef<Role>> _roles;

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

    public string subject_identifier
    {
      get
      {
        return this._subject_identifier;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._subject_identifier))
          return;
        this._subject_identifier = value;
        this.Changed = true;
        this.NotifyPropertyChanged("subject_identifier");
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

    public List<XenRef<Role>> roles
    {
      get
      {
        return this._roles;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._roles))
          return;
        this._roles = value;
        this.Changed = true;
        this.NotifyPropertyChanged("roles");
      }
    }

    public string DisplayName
    {
      get
      {
        string str;
        if (this.other_config.TryGetValue(Subject.SUBJECT_DISPLAYNAME_KEY, out str))
          return str;
        return (string) null;
      }
    }

    public string SubjectName
    {
      get
      {
        string str;
        if (this.other_config.TryGetValue(Subject.SUBJECT_NAME_KEY, out str))
          return str;
        return (string) null;
      }
    }

    public bool IsGroup
    {
      get
      {
        string str;
        bool result;
        if (this.other_config.TryGetValue(Subject.SUBJECT_IS_GROUP_KEY, out str) && bool.TryParse(str, out result))
          return result;
        return false;
      }
    }

    public Subject()
    {
    }

    public Subject(string uuid, string subject_identifier, Dictionary<string, string> other_config, List<XenRef<Role>> roles)
    {
      this.uuid = uuid;
      this.subject_identifier = subject_identifier;
      this.other_config = other_config;
      this.roles = roles;
    }

    public Subject(Proxy_Subject proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Subject(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.subject_identifier = Marshalling.ParseString(table, "subject_identifier");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
      this.roles = Marshalling.ParseSetRef<Role>(table, "roles");
    }

    public override void UpdateFrom(Subject update)
    {
      this.uuid = update.uuid;
      this.subject_identifier = update.subject_identifier;
      this.other_config = update.other_config;
      this.roles = update.roles;
    }

    internal void UpdateFromProxy(Proxy_Subject proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.subject_identifier = proxy.subject_identifier == null ? (string) null : proxy.subject_identifier;
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
      this.roles = proxy.roles == null ? (List<XenRef<Role>>) null : XenRef<Role>.Create(proxy.roles);
    }

    public Proxy_Subject ToProxy()
    {
      return new Proxy_Subject()
      {
        uuid = this.uuid != null ? this.uuid : "",
        subject_identifier = this.subject_identifier != null ? this.subject_identifier : "",
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config),
        roles = this.roles != null ? Helper.RefListToStringArray<Role>(this.roles) : new string[0]
      };
    }

    public bool DeepEquals(Subject other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._subject_identifier, other._subject_identifier) && Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config))
        return Helper.AreEqual2<List<XenRef<Role>>>(this._roles, other._roles);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Subject server)
    {
      if (opaqueRef != null)
        throw new InvalidOperationException("This type has no read/write properties");
      Proxy_Subject _record = this.ToProxy();
      return session.proxy.subject_create(session.uuid, _record).parse();
    }

    public static Subject get_record(Session session, string _subject)
    {
      return new Subject(session.proxy.subject_get_record(session.uuid, _subject != null ? _subject : "").parse());
    }

    public static XenRef<Subject> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Subject>.Create(session.proxy.subject_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<Subject> create(Session session, Subject _record)
    {
      return XenRef<Subject>.Create(session.proxy.subject_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, Subject _record)
    {
      return XenRef<Task>.Create(session.proxy.async_subject_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _subject)
    {
      session.proxy.subject_destroy(session.uuid, _subject != null ? _subject : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _subject)
    {
      return XenRef<Task>.Create(session.proxy.async_subject_destroy(session.uuid, _subject != null ? _subject : "").parse());
    }

    public static string get_uuid(Session session, string _subject)
    {
      return session.proxy.subject_get_uuid(session.uuid, _subject != null ? _subject : "").parse();
    }

    public static string get_subject_identifier(Session session, string _subject)
    {
      return session.proxy.subject_get_subject_identifier(session.uuid, _subject != null ? _subject : "").parse();
    }

    public static Dictionary<string, string> get_other_config(Session session, string _subject)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.subject_get_other_config(session.uuid, _subject != null ? _subject : "").parse());
    }

    public static List<XenRef<Role>> get_roles(Session session, string _subject)
    {
      return XenRef<Role>.Create(session.proxy.subject_get_roles(session.uuid, _subject != null ? _subject : "").parse());
    }

    public static void add_to_roles(Session session, string _self, string _role)
    {
      session.proxy.subject_add_to_roles(session.uuid, _self != null ? _self : "", _role != null ? _role : "").parse();
    }

    public static void remove_from_roles(Session session, string _self, string _role)
    {
      session.proxy.subject_remove_from_roles(session.uuid, _self != null ? _self : "", _role != null ? _role : "").parse();
    }

    public static string[] get_permissions_name_label(Session session, string _self)
    {
      return session.proxy.subject_get_permissions_name_label(session.uuid, _self != null ? _self : "").parse();
    }

    public static List<XenRef<Subject>> get_all(Session session)
    {
      return XenRef<Subject>.Create(session.proxy.subject_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Subject>, Subject> get_all_records(Session session)
    {
      return XenRef<Subject>.Create<Proxy_Subject>(session.proxy.subject_get_all_records(session.uuid).parse());
    }
  }
}
