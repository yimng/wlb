// Decompiled with JetBrains decompiler
// Type: XenAPI.Auth
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Auth : XenObject<Auth>
  {
    public Auth()
    {
    }

    public Auth(Proxy_Auth proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Auth(Hashtable table)
    {
    }

    public override void UpdateFrom(Auth update)
    {
    }

    internal void UpdateFromProxy(Proxy_Auth proxy)
    {
    }

    public Proxy_Auth ToProxy()
    {
      return new Proxy_Auth();
    }

    public bool DeepEquals(Auth other)
    {
      return !object.ReferenceEquals((object) null, (object) other) && object.ReferenceEquals((object) this, (object) other);
    }

    public override string SaveChanges(Session session, string opaqueRef, Auth server)
    {
      if (opaqueRef == null)
        return "";
      throw new InvalidOperationException("This type has no read/write properties");
    }

    public static string get_subject_identifier(Session session, string _subject_name)
    {
      return session.proxy.auth_get_subject_identifier(session.uuid, _subject_name != null ? _subject_name : "").parse();
    }

    public static Dictionary<string, string> get_subject_information_from_identifier(Session session, string _subject_identifier)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.auth_get_subject_information_from_identifier(session.uuid, _subject_identifier != null ? _subject_identifier : "").parse());
    }

    public static string[] get_group_membership(Session session, string _subject_identifier)
    {
      return session.proxy.auth_get_group_membership(session.uuid, _subject_identifier != null ? _subject_identifier : "").parse();
    }

    public static Dictionary<XenRef<Auth>, Auth> get_all_records(Session session)
    {
      return XenRef<Auth>.Create<Proxy_Auth>(session.proxy.auth_get_all_records(session.uuid).parse());
    }
  }
}
