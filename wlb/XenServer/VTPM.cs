// Decompiled with JetBrains decompiler
// Type: XenAPI.VTPM
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class VTPM : XenObject<VTPM>
  {
    private string _uuid;
    private XenRef<XenAPI.VM> _VM;
    private XenRef<XenAPI.VM> _backend;

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

    public XenRef<XenAPI.VM> backend
    {
      get
      {
        return this._backend;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._backend))
          return;
        this._backend = value;
        this.Changed = true;
        this.NotifyPropertyChanged("backend");
      }
    }

    public VTPM()
    {
    }

    public VTPM(string uuid, XenRef<XenAPI.VM> VM, XenRef<XenAPI.VM> backend)
    {
      this.uuid = uuid;
      this.VM = VM;
      this.backend = backend;
    }

    public VTPM(Proxy_VTPM proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public VTPM(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.VM = Marshalling.ParseRef<XenAPI.VM>(table, "VM");
      this.backend = Marshalling.ParseRef<XenAPI.VM>(table, "backend");
    }

    public override void UpdateFrom(VTPM update)
    {
      this.uuid = update.uuid;
      this.VM = update.VM;
      this.backend = update.backend;
    }

    internal void UpdateFromProxy(Proxy_VTPM proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.VM = proxy.VM == null ? (XenRef<XenAPI.VM>) null : XenRef<XenAPI.VM>.Create(proxy.VM);
      this.backend = proxy.backend == null ? (XenRef<XenAPI.VM>) null : XenRef<XenAPI.VM>.Create(proxy.backend);
    }

    public Proxy_VTPM ToProxy()
    {
      return new Proxy_VTPM()
      {
        uuid = this.uuid != null ? this.uuid : "",
        VM = this.VM != null ? (string) this.VM : "",
        backend = this.backend != null ? (string) this.backend : ""
      };
    }

    public bool DeepEquals(VTPM other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<XenRef<XenAPI.VM>>(this._VM, other._VM))
        return Helper.AreEqual2<XenRef<XenAPI.VM>>(this._backend, other._backend);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, VTPM server)
    {
      if (opaqueRef != null)
        throw new InvalidOperationException("This type has no read/write properties");
      Proxy_VTPM _record = this.ToProxy();
      return session.proxy.vtpm_create(session.uuid, _record).parse();
    }

    public static VTPM get_record(Session session, string _vtpm)
    {
      return new VTPM(session.proxy.vtpm_get_record(session.uuid, _vtpm != null ? _vtpm : "").parse());
    }

    public static XenRef<VTPM> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<VTPM>.Create(session.proxy.vtpm_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static XenRef<VTPM> create(Session session, VTPM _record)
    {
      return XenRef<VTPM>.Create(session.proxy.vtpm_create(session.uuid, _record.ToProxy()).parse());
    }

    public static XenRef<Task> async_create(Session session, VTPM _record)
    {
      return XenRef<Task>.Create(session.proxy.async_vtpm_create(session.uuid, _record.ToProxy()).parse());
    }

    public static void destroy(Session session, string _vtpm)
    {
      session.proxy.vtpm_destroy(session.uuid, _vtpm != null ? _vtpm : "").parse();
    }

    public static XenRef<Task> async_destroy(Session session, string _vtpm)
    {
      return XenRef<Task>.Create(session.proxy.async_vtpm_destroy(session.uuid, _vtpm != null ? _vtpm : "").parse());
    }

    public static string get_uuid(Session session, string _vtpm)
    {
      return session.proxy.vtpm_get_uuid(session.uuid, _vtpm != null ? _vtpm : "").parse();
    }

    public static XenRef<XenAPI.VM> get_VM(Session session, string _vtpm)
    {
      return XenRef<XenAPI.VM>.Create(session.proxy.vtpm_get_vm(session.uuid, _vtpm != null ? _vtpm : "").parse());
    }

    public static XenRef<XenAPI.VM> get_backend(Session session, string _vtpm)
    {
      return XenRef<XenAPI.VM>.Create(session.proxy.vtpm_get_backend(session.uuid, _vtpm != null ? _vtpm : "").parse());
    }

    public static Dictionary<XenRef<VTPM>, VTPM> get_all_records(Session session)
    {
      return XenRef<VTPM>.Create<Proxy_VTPM>(session.proxy.vtpm_get_all_records(session.uuid).parse());
    }
  }
}
