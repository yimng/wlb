// Decompiled with JetBrains decompiler
// Type: XenAPI.DR_task
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class DR_task : XenObject<DR_task>
  {
    private string _uuid;
    private List<XenRef<SR>> _introduced_SRs;

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

    public List<XenRef<SR>> introduced_SRs
    {
      get
      {
        return this._introduced_SRs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._introduced_SRs))
          return;
        this._introduced_SRs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("introduced_SRs");
      }
    }

    public DR_task()
    {
    }

    public DR_task(string uuid, List<XenRef<SR>> introduced_SRs)
    {
      this.uuid = uuid;
      this.introduced_SRs = introduced_SRs;
    }

    public DR_task(Proxy_DR_task proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public DR_task(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.introduced_SRs = Marshalling.ParseSetRef<SR>(table, "introduced_SRs");
    }

    public override void UpdateFrom(DR_task update)
    {
      this.uuid = update.uuid;
      this.introduced_SRs = update.introduced_SRs;
    }

    internal void UpdateFromProxy(Proxy_DR_task proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.introduced_SRs = proxy.introduced_SRs == null ? (List<XenRef<SR>>) null : XenRef<SR>.Create(proxy.introduced_SRs);
    }

    public Proxy_DR_task ToProxy()
    {
      return new Proxy_DR_task()
      {
        uuid = this.uuid != null ? this.uuid : "",
        introduced_SRs = this.introduced_SRs != null ? Helper.RefListToStringArray<SR>(this.introduced_SRs) : new string[0]
      };
    }

    public bool DeepEquals(DR_task other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid))
        return Helper.AreEqual2<List<XenRef<SR>>>(this._introduced_SRs, other._introduced_SRs);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, DR_task server)
    {
      if (opaqueRef == null)
        return "";
      throw new InvalidOperationException("This type has no read/write properties");
    }

    public static DR_task get_record(Session session, string _dr_task)
    {
      return new DR_task(session.proxy.dr_task_get_record(session.uuid, _dr_task != null ? _dr_task : "").parse());
    }

    public static XenRef<DR_task> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<DR_task>.Create(session.proxy.dr_task_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _dr_task)
    {
      return session.proxy.dr_task_get_uuid(session.uuid, _dr_task != null ? _dr_task : "").parse();
    }

    public static List<XenRef<SR>> get_introduced_SRs(Session session, string _dr_task)
    {
      return XenRef<SR>.Create(session.proxy.dr_task_get_introduced_srs(session.uuid, _dr_task != null ? _dr_task : "").parse());
    }

    public static XenRef<DR_task> create(Session session, string _type, Dictionary<string, string> _device_config, string[] _whitelist)
    {
      return XenRef<DR_task>.Create(session.proxy.dr_task_create(session.uuid, _type != null ? _type : "", (object) Maps.convert_to_proxy_string_string(_device_config), _whitelist).parse());
    }

    public static XenRef<Task> async_create(Session session, string _type, Dictionary<string, string> _device_config, string[] _whitelist)
    {
      return XenRef<Task>.Create(session.proxy.async_dr_task_create(session.uuid, _type != null ? _type : "", (object) Maps.convert_to_proxy_string_string(_device_config), _whitelist).parse());
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.dr_task_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_dr_task_destroy(session.uuid, _self != null ? _self : "").parse());
    }

    public static List<XenRef<DR_task>> get_all(Session session)
    {
      return XenRef<DR_task>.Create(session.proxy.dr_task_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<DR_task>, DR_task> get_all_records(Session session)
    {
      return XenRef<DR_task>.Create<Proxy_DR_task>(session.proxy.dr_task_get_all_records(session.uuid).parse());
    }
  }
}
