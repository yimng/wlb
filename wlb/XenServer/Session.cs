// Decompiled with JetBrains decompiler
// Type: XenAPI.Session
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace XenAPI
{
  public class Session : XenObject<Session>
  {
    public static string UserAgent = string.Format("XenAPI/{0}", (object) Helper.APIVersionString(API_Version.API_1_10));
    public static IWebProxy Proxy = (IWebProxy) null;
    public API_Version APIVersion = API_Version.API_1_1;
    private bool _isLocalSuperuser = true;
    private List<Role> roles = new List<Role>();
    public const int STANDARD_TIMEOUT = 86400000;
    private XenAPI.Proxy _proxy;
    private string _uuid;
    public object Tag;
    private XenRef<XenAPI.Subject> _subject;
    private string _userSid;
    private string[] permissions;

    public virtual UserDetails CurrentUserDetails
    {
      get
      {
        return UserDetails.Sid_To_UserDetails[this._userSid];
      }
    }

    public XenAPI.Proxy proxy
    {
      get
      {
        return this._proxy;
      }
    }

    public string uuid
    {
      get
      {
        return this._uuid;
      }
    }

    public string Url
    {
      get
      {
        return this._proxy.Url;
      }
    }

    public virtual bool IsLocalSuperuser
    {
      get
      {
        return this._isLocalSuperuser;
      }
    }

    public XenRef<XenAPI.Subject> Subject
    {
      get
      {
        return this._subject;
      }
    }

    public string UserSid
    {
      get
      {
        return this._userSid;
      }
    }

    public string[] Permissions
    {
      get
      {
        return this.permissions;
      }
    }

    public List<Role> Roles
    {
      get
      {
        return this.roles;
      }
    }

    public Session(int timeout, string url)
    {
      this._proxy = XmlRpcProxyGen.Create<XenAPI.Proxy>();
      this._proxy.Url = url;
      this._proxy.NonStandard = XmlRpcNonStandard.All;
      this._proxy.Timeout = timeout;
      this._proxy.UseIndentation = false;
      this._proxy.UserAgent = Session.UserAgent;
      this._proxy.KeepAlive = true;
      this._proxy.Proxy = Session.Proxy;
    }

    public Session(string url)
      : this(86400000, url)
    {
    }

    public Session(int timeout, string host, int port)
      : this(timeout, Session.GetUrl(host, port))
    {
    }

    public Session(string host, int port)
      : this(86400000, host, port)
    {
    }

    public Session(string url, string opaque_ref)
      : this(86400000, url)
    {
      this._uuid = opaque_ref;
      this.SetAPIVersion();
      if (this.APIVersion < API_Version.API_1_6)
        return;
      this.SetADDetails();
    }

    public Session(Session session, int timeout)
      : this(timeout, session.Url)
    {
      this._uuid = session.uuid;
      this.APIVersion = session.APIVersion;
      this._isLocalSuperuser = session._isLocalSuperuser;
      this._subject = session._subject;
      this._userSid = session._userSid;
    }

    public static Session get_record(Session session, string _session)
    {
      Session session1 = new Session(86400000, session.proxy.Url);
      session1._uuid = _session;
      session1.SetAPIVersion();
      return session1;
    }

    private void SetADDetails()
    {
      this._isLocalSuperuser = this.get_is_local_superuser();
      if (this.IsLocalSuperuser)
        return;
      this._subject = this.get_subject();
      this._userSid = this.get_auth_user_sid();
      UserDetails.UpdateDetails(this._userSid, this);
      if (this.APIVersion <= API_Version.API_1_6)
        return;
      this.permissions = Session.get_rbac_permissions(this, this.uuid);
      Dictionary<XenRef<Role>, Role> allRecords = Role.get_all_records(this);
      foreach (string str in this.permissions)
      {
        foreach (XenRef<Role> index in allRecords.Keys)
        {
          Role role = allRecords[index];
          if (role.subroles.Count > 0 && role.name_label == str)
          {
            role.opaque_ref = index.opaque_ref;
            this.roles.Add(role);
            break;
          }
        }
      }
    }

    public override void UpdateFrom(Session update)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override string SaveChanges(Session session, string _serverOpaqueRef, Session serverObject)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public void login_with_password(string username, string password)
    {
      this._uuid = this.proxy.session_login_with_password(username, password).parse();
      this.SetAPIVersion();
    }

    public void login_with_password(string username, string password, string version)
    {
      try
      {
        this._uuid = this.proxy.session_login_with_password(username, password, version).parse();
        this.SetAPIVersion();
        if (this.APIVersion < API_Version.API_1_6)
          return;
        this.SetADDetails();
      }
      catch (Failure ex)
      {
        if (ex.ErrorDescription[0] == "MESSAGE_PARAMETER_COUNT_MISMATCH")
          this.login_with_password(username, password);
        else
          throw;
      }
    }

    public void login_with_password(string username, string password, API_Version version)
    {
      this.login_with_password(username, password, Helper.APIVersionString(version));
    }

    private void SetAPIVersion()
    {
      using (Dictionary<XenRef<Pool>, Pool>.ValueCollection.Enumerator enumerator = Pool.get_all_records(this).Values.GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return;
        Host record = Host.get_record(this, (string) enumerator.Current.master);
        this.APIVersion = Helper.GetAPIVersion(record.API_version_major, record.API_version_minor);
      }
    }

    public void slave_local_login_with_password(string username, string password)
    {
      this._uuid = this.proxy.session_slave_local_login_with_password(username, password).parse();
      this.APIVersion = API_Version.API_1_10;
    }

    public void logout()
    {
      this.logout(this);
    }

    public void logout(Session session2)
    {
      this.logout(session2._uuid);
      session2._uuid = (string) null;
    }

    public void logout(string _self)
    {
      if (_self == null)
        return;
      this.proxy.session_logout(_self).parse();
    }

    public void local_logout()
    {
      this.local_logout(this);
    }

    public void local_logout(Session session2)
    {
      this.local_logout(session2._uuid);
      session2._uuid = (string) null;
    }

    public void local_logout(string session_uuid)
    {
      if (session_uuid == null)
        return;
      this.proxy.session_local_logout(session_uuid).parse();
    }

    public void change_password(string oldPassword, string newPassword)
    {
      this.change_password(this, oldPassword, newPassword);
    }

    public void change_password(Session session2, string oldPassword, string newPassword)
    {
      this.proxy.session_change_password(session2.uuid, oldPassword, newPassword).parse();
    }

    public string get_this_host()
    {
      return Session.get_this_host(this, this.uuid);
    }

    public static string get_this_host(Session session, string _self)
    {
      return session.proxy.session_get_this_host(session.uuid, _self != null ? _self : "").parse();
    }

    public string get_this_user()
    {
      return Session.get_this_user(this, this.uuid);
    }

    public static string get_this_user(Session session, string _self)
    {
      return session.proxy.session_get_this_user(session.uuid, _self != null ? _self : "").parse();
    }

    public bool get_is_local_superuser()
    {
      return Session.get_is_local_superuser(this, this.uuid);
    }

    public static bool get_is_local_superuser(Session session, string _self)
    {
      return session.proxy.session_get_is_local_superuser(session.uuid, _self != null ? _self : "").parse();
    }

    public static string[] get_rbac_permissions(Session session, string _self)
    {
      return session.proxy.session_get_rbac_permissions(session.uuid, _self != null ? _self : "").parse();
    }

    public DateTime get_last_active()
    {
      return Session.get_last_active(this, this.uuid);
    }

    public static DateTime get_last_active(Session session, string _self)
    {
      return session.proxy.session_get_last_active(session.uuid, _self != null ? _self : "").parse();
    }

    public bool get_pool()
    {
      return Session.get_pool(this, this.uuid);
    }

    public static bool get_pool(Session session, string _self)
    {
      return session.proxy.session_get_pool(session.uuid, _self != null ? _self : "").parse();
    }

    public XenRef<XenAPI.Subject> get_subject()
    {
      return Session.get_subject(this, this.uuid);
    }

    public static XenRef<XenAPI.Subject> get_subject(Session session, string _self)
    {
      return new XenRef<XenAPI.Subject>(session.proxy.session_get_subject(session.uuid, _self != null ? _self : "").parse());
    }

    public string get_auth_user_sid()
    {
      return Session.get_auth_user_sid(this, this.uuid);
    }

    public static string get_auth_user_sid(Session session, string _self)
    {
      return session.proxy.session_get_auth_user_sid(session.uuid, _self != null ? _self : "").parse();
    }

    public string[] get_all_subject_identifiers()
    {
      return Session.get_all_subject_identifiers(this);
    }

    public static string[] get_all_subject_identifiers(Session session)
    {
      return session.proxy.session_get_all_subject_identifiers(session.uuid).parse();
    }

    public XenRef<Task> async_get_all_subject_identifiers()
    {
      return Session.async_get_all_subject_identifiers(this);
    }

    public static XenRef<Task> async_get_all_subject_identifiers(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_session_get_all_subject_identifiers(session.uuid).parse());
    }

    public string logout_subject_identifier(string subject_identifier)
    {
      return Session.logout_subject_identifier(this, subject_identifier);
    }

    public static string logout_subject_identifier(Session session, string subject_identifier)
    {
      return session.proxy.session_logout_subject_identifier(session.uuid, subject_identifier).parse();
    }

    public XenRef<Task> async_logout_subject_identifier(string subject_identifier)
    {
      return Session.async_logout_subject_identifier(this, subject_identifier);
    }

    public static XenRef<Task> async_logout_subject_identifier(Session session, string subject_identifier)
    {
      return XenRef<Task>.Create(session.proxy.async_session_logout_subject_identifier(session.uuid, subject_identifier).parse());
    }

    public Dictionary<string, string> get_other_config()
    {
      return Session.get_other_config(this, this.uuid);
    }

    public static Dictionary<string, string> get_other_config(Session session, string _self)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.session_get_other_config(session.uuid, _self != null ? _self : "").parse());
    }

    public void set_other_config(Dictionary<string, string> _other_config)
    {
      Session.set_other_config(this, this.uuid, _other_config);
    }

    public static void set_other_config(Session session, string _self, Dictionary<string, string> _other_config)
    {
      session.proxy.session_set_other_config(session.uuid, _self != null ? _self : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public void add_to_other_config(string _key, string _value)
    {
      Session.add_to_other_config(this, this.uuid, _key, _value);
    }

    public static void add_to_other_config(Session session, string _self, string _key, string _value)
    {
      session.proxy.session_add_to_other_config(session.uuid, _self != null ? _self : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public void remove_from_other_config(string _key)
    {
      Session.remove_from_other_config(this, this.uuid, _key);
    }

    public static void remove_from_other_config(Session session, string _self, string _key)
    {
      session.proxy.session_remove_from_other_config(session.uuid, _self != null ? _self : "", _key != null ? _key : "").parse();
    }

    private static string GetUrl(string hostname, int port)
    {
      return string.Format("{0}://{1}:{2}", port == 8080 || port == 80 ? (object) "http" : (object) "https", (object) hostname, (object) port);
    }

    private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
      return true;
    }
  }
}
