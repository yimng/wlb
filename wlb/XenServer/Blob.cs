// Decompiled with JetBrains decompiler
// Type: XenAPI.Blob
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Blob : XenObject<Blob>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private long _size;
    private bool _pubblic;
    private DateTime _last_updated;
    private string _mime_type;

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

    public bool pubblic
    {
      get
      {
        return this._pubblic;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._pubblic ? 1 : 0)))
          return;
        this._pubblic = value;
        this.Changed = true;
        this.NotifyPropertyChanged("pubblic");
      }
    }

    public DateTime last_updated
    {
      get
      {
        return this._last_updated;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._last_updated))
          return;
        this._last_updated = value;
        this.Changed = true;
        this.NotifyPropertyChanged("last_updated");
      }
    }

    public string mime_type
    {
      get
      {
        return this._mime_type;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._mime_type))
          return;
        this._mime_type = value;
        this.Changed = true;
        this.NotifyPropertyChanged("mime_type");
      }
    }

    public Blob()
    {
    }

    public Blob(string uuid, string name_label, string name_description, long size, bool pubblic, DateTime last_updated, string mime_type)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.size = size;
      this.pubblic = pubblic;
      this.last_updated = last_updated;
      this.mime_type = mime_type;
    }

    public Blob(Proxy_Blob proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Blob(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.size = Marshalling.ParseLong(table, "size");
      this.pubblic = Marshalling.ParseBool(table, "pubblic");
      this.last_updated = Marshalling.ParseDateTime(table, "last_updated");
      this.mime_type = Marshalling.ParseString(table, "mime_type");
    }

    public override void UpdateFrom(Blob update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.size = update.size;
      this.pubblic = update.pubblic;
      this.last_updated = update.last_updated;
      this.mime_type = update.mime_type;
    }

    internal void UpdateFromProxy(Proxy_Blob proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.size = proxy.size == null ? 0L : long.Parse(proxy.size);
      this.pubblic = proxy.pubblic;
      this.last_updated = proxy.last_updated;
      this.mime_type = proxy.mime_type == null ? (string) null : proxy.mime_type;
    }

    public Proxy_Blob ToProxy()
    {
      return new Proxy_Blob()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        size = this.size.ToString(),
        pubblic = this.pubblic,
        last_updated = this.last_updated,
        mime_type = this.mime_type != null ? this.mime_type : ""
      };
    }

    public bool DeepEquals(Blob other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._name_label, other._name_label) && (Helper.AreEqual2<string>(this._name_description, other._name_description) && Helper.AreEqual2<long>(this._size, other._size)) && (Helper.AreEqual2<bool>(this._pubblic, other._pubblic) && Helper.AreEqual2<DateTime>(this._last_updated, other._last_updated)))
        return Helper.AreEqual2<string>(this._mime_type, other._mime_type);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Blob server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
        Blob.set_name_label(session, opaqueRef, this._name_label);
      if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
        Blob.set_name_description(session, opaqueRef, this._name_description);
      return (string) null;
    }

    public static Blob get_record(Session session, string _blob)
    {
      return new Blob(session.proxy.blob_get_record(session.uuid, _blob != null ? _blob : "").parse());
    }

    public static XenRef<Blob> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Blob>.Create(session.proxy.blob_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static List<XenRef<Blob>> get_by_name_label(Session session, string _label)
    {
      return XenRef<Blob>.Create(session.proxy.blob_get_by_name_label(session.uuid, _label != null ? _label : "").parse());
    }

    public static string get_uuid(Session session, string _blob)
    {
      return session.proxy.blob_get_uuid(session.uuid, _blob != null ? _blob : "").parse();
    }

    public static string get_name_label(Session session, string _blob)
    {
      return session.proxy.blob_get_name_label(session.uuid, _blob != null ? _blob : "").parse();
    }

    public static string get_name_description(Session session, string _blob)
    {
      return session.proxy.blob_get_name_description(session.uuid, _blob != null ? _blob : "").parse();
    }

    public static long get_size(Session session, string _blob)
    {
      return long.Parse(session.proxy.blob_get_size(session.uuid, _blob != null ? _blob : "").parse());
    }

    public static bool get_public(Session session, string _blob)
    {
      return session.proxy.blob_get_public(session.uuid, _blob != null ? _blob : "").parse();
    }

    public static DateTime get_last_updated(Session session, string _blob)
    {
      return session.proxy.blob_get_last_updated(session.uuid, _blob != null ? _blob : "").parse();
    }

    public static string get_mime_type(Session session, string _blob)
    {
      return session.proxy.blob_get_mime_type(session.uuid, _blob != null ? _blob : "").parse();
    }

    public static void set_name_label(Session session, string _blob, string _label)
    {
      session.proxy.blob_set_name_label(session.uuid, _blob != null ? _blob : "", _label != null ? _label : "").parse();
    }

    public static void set_name_description(Session session, string _blob, string _description)
    {
      session.proxy.blob_set_name_description(session.uuid, _blob != null ? _blob : "", _description != null ? _description : "").parse();
    }

    public static void set_public(Session session, string _blob, bool _public)
    {
      session.proxy.blob_set_public(session.uuid, _blob != null ? _blob : "", _public).parse();
    }

    public static XenRef<Blob> create(Session session, string _mime_type, bool _public)
    {
      return XenRef<Blob>.Create(session.proxy.blob_create(session.uuid, _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.blob_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static List<XenRef<Blob>> get_all(Session session)
    {
      return XenRef<Blob>.Create(session.proxy.blob_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Blob>, Blob> get_all_records(Session session)
    {
      return XenRef<Blob>.Create<Proxy_Blob>(session.proxy.blob_get_all_records(session.uuid).parse());
    }

    public static XenRef<Blob> create(Session session, string _mime_type)
    {
      return XenRef<Blob>.Create(session.proxy.blob_create(session.uuid, _mime_type != null ? _mime_type : "").parse());
    }
  }
}
