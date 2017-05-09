// Decompiled with JetBrains decompiler
// Type: XenAPI.Pool
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Pool : XenObject<Pool>
  {
    private string _uuid;
    private string _name_label;
    private string _name_description;
    private XenRef<Host> _master;
    private XenRef<SR> _default_SR;
    private XenRef<SR> _suspend_image_SR;
    private XenRef<SR> _crash_dump_SR;
    private Dictionary<string, string> _other_config;
    private bool _ha_enabled;
    private Dictionary<string, string> _ha_configuration;
    private string[] _ha_statefiles;
    private long _ha_host_failures_to_tolerate;
    private long _ha_plan_exists_for;
    private bool _ha_allow_overcommit;
    private bool _ha_overcommitted;
    private Dictionary<string, XenRef<Blob>> _blobs;
    private string[] _tags;
    private Dictionary<string, string> _gui_config;
    private string _wlb_url;
    private string _wlb_username;
    private bool _wlb_enabled;
    private bool _wlb_verify_cert;
    private bool _redo_log_enabled;
    private XenRef<VDI> _redo_log_vdi;
    private string _vswitch_controller;
    private Dictionary<string, string> _restrictions;
    private List<XenRef<VDI>> _metadata_VDIs;

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

    public XenRef<Host> master
    {
      get
      {
        return this._master;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._master))
          return;
        this._master = value;
        this.Changed = true;
        this.NotifyPropertyChanged("master");
      }
    }

    public XenRef<SR> default_SR
    {
      get
      {
        return this._default_SR;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._default_SR))
          return;
        this._default_SR = value;
        this.Changed = true;
        this.NotifyPropertyChanged("default_SR");
      }
    }

    public XenRef<SR> suspend_image_SR
    {
      get
      {
        return this._suspend_image_SR;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._suspend_image_SR))
          return;
        this._suspend_image_SR = value;
        this.Changed = true;
        this.NotifyPropertyChanged("suspend_image_SR");
      }
    }

    public XenRef<SR> crash_dump_SR
    {
      get
      {
        return this._crash_dump_SR;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._crash_dump_SR))
          return;
        this._crash_dump_SR = value;
        this.Changed = true;
        this.NotifyPropertyChanged("crash_dump_SR");
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

    public bool ha_enabled
    {
      get
      {
        return this._ha_enabled;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._ha_enabled ? 1 : 0)))
          return;
        this._ha_enabled = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_enabled");
      }
    }

    public Dictionary<string, string> ha_configuration
    {
      get
      {
        return this._ha_configuration;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ha_configuration))
          return;
        this._ha_configuration = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_configuration");
      }
    }

    public string[] ha_statefiles
    {
      get
      {
        return this._ha_statefiles;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ha_statefiles))
          return;
        this._ha_statefiles = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_statefiles");
      }
    }

    public long ha_host_failures_to_tolerate
    {
      get
      {
        return this._ha_host_failures_to_tolerate;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ha_host_failures_to_tolerate))
          return;
        this._ha_host_failures_to_tolerate = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_host_failures_to_tolerate");
      }
    }

    public long ha_plan_exists_for
    {
      get
      {
        return this._ha_plan_exists_for;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._ha_plan_exists_for))
          return;
        this._ha_plan_exists_for = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_plan_exists_for");
      }
    }

    public bool ha_allow_overcommit
    {
      get
      {
        return this._ha_allow_overcommit;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._ha_allow_overcommit ? 1 : 0)))
          return;
        this._ha_allow_overcommit = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_allow_overcommit");
      }
    }

    public bool ha_overcommitted
    {
      get
      {
        return this._ha_overcommitted;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._ha_overcommitted ? 1 : 0)))
          return;
        this._ha_overcommitted = value;
        this.Changed = true;
        this.NotifyPropertyChanged("ha_overcommitted");
      }
    }

    public Dictionary<string, XenRef<Blob>> blobs
    {
      get
      {
        return this._blobs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._blobs))
          return;
        this._blobs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("blobs");
      }
    }

    public string[] tags
    {
      get
      {
        return this._tags;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._tags))
          return;
        this._tags = value;
        this.Changed = true;
        this.NotifyPropertyChanged("tags");
      }
    }

    public Dictionary<string, string> gui_config
    {
      get
      {
        return this._gui_config;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._gui_config))
          return;
        this._gui_config = value;
        this.Changed = true;
        this.NotifyPropertyChanged("gui_config");
      }
    }

    public string wlb_url
    {
      get
      {
        return this._wlb_url;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._wlb_url))
          return;
        this._wlb_url = value;
        this.Changed = true;
        this.NotifyPropertyChanged("wlb_url");
      }
    }

    public string wlb_username
    {
      get
      {
        return this._wlb_username;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._wlb_username))
          return;
        this._wlb_username = value;
        this.Changed = true;
        this.NotifyPropertyChanged("wlb_username");
      }
    }

    public bool wlb_enabled
    {
      get
      {
        return this._wlb_enabled;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._wlb_enabled ? 1 : 0)))
          return;
        this._wlb_enabled = value;
        this.Changed = true;
        this.NotifyPropertyChanged("wlb_enabled");
      }
    }

    public bool wlb_verify_cert
    {
      get
      {
        return this._wlb_verify_cert;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._wlb_verify_cert ? 1 : 0)))
          return;
        this._wlb_verify_cert = value;
        this.Changed = true;
        this.NotifyPropertyChanged("wlb_verify_cert");
      }
    }

    public bool redo_log_enabled
    {
      get
      {
        return this._redo_log_enabled;
      }
      set
      {
        if (Helper.AreEqual((object) (bool) (value ? 1 : 0), (object) (bool) (this._redo_log_enabled ? 1 : 0)))
          return;
        this._redo_log_enabled = value;
        this.Changed = true;
        this.NotifyPropertyChanged("redo_log_enabled");
      }
    }

    public XenRef<VDI> redo_log_vdi
    {
      get
      {
        return this._redo_log_vdi;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._redo_log_vdi))
          return;
        this._redo_log_vdi = value;
        this.Changed = true;
        this.NotifyPropertyChanged("redo_log_vdi");
      }
    }

    public string vswitch_controller
    {
      get
      {
        return this._vswitch_controller;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._vswitch_controller))
          return;
        this._vswitch_controller = value;
        this.Changed = true;
        this.NotifyPropertyChanged("vswitch_controller");
      }
    }

    public Dictionary<string, string> restrictions
    {
      get
      {
        return this._restrictions;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._restrictions))
          return;
        this._restrictions = value;
        this.Changed = true;
        this.NotifyPropertyChanged("restrictions");
      }
    }

    public List<XenRef<VDI>> metadata_VDIs
    {
      get
      {
        return this._metadata_VDIs;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._metadata_VDIs))
          return;
        this._metadata_VDIs = value;
        this.Changed = true;
        this.NotifyPropertyChanged("metadata_VDIs");
      }
    }

    public Pool()
    {
    }

    public Pool(string uuid, string name_label, string name_description, XenRef<Host> master, XenRef<SR> default_SR, XenRef<SR> suspend_image_SR, XenRef<SR> crash_dump_SR, Dictionary<string, string> other_config, bool ha_enabled, Dictionary<string, string> ha_configuration, string[] ha_statefiles, long ha_host_failures_to_tolerate, long ha_plan_exists_for, bool ha_allow_overcommit, bool ha_overcommitted, Dictionary<string, XenRef<Blob>> blobs, string[] tags, Dictionary<string, string> gui_config, string wlb_url, string wlb_username, bool wlb_enabled, bool wlb_verify_cert, bool redo_log_enabled, XenRef<VDI> redo_log_vdi, string vswitch_controller, Dictionary<string, string> restrictions, List<XenRef<VDI>> metadata_VDIs)
    {
      this.uuid = uuid;
      this.name_label = name_label;
      this.name_description = name_description;
      this.master = master;
      this.default_SR = default_SR;
      this.suspend_image_SR = suspend_image_SR;
      this.crash_dump_SR = crash_dump_SR;
      this.other_config = other_config;
      this.ha_enabled = ha_enabled;
      this.ha_configuration = ha_configuration;
      this.ha_statefiles = ha_statefiles;
      this.ha_host_failures_to_tolerate = ha_host_failures_to_tolerate;
      this.ha_plan_exists_for = ha_plan_exists_for;
      this.ha_allow_overcommit = ha_allow_overcommit;
      this.ha_overcommitted = ha_overcommitted;
      this.blobs = blobs;
      this.tags = tags;
      this.gui_config = gui_config;
      this.wlb_url = wlb_url;
      this.wlb_username = wlb_username;
      this.wlb_enabled = wlb_enabled;
      this.wlb_verify_cert = wlb_verify_cert;
      this.redo_log_enabled = redo_log_enabled;
      this.redo_log_vdi = redo_log_vdi;
      this.vswitch_controller = vswitch_controller;
      this.restrictions = restrictions;
      this.metadata_VDIs = metadata_VDIs;
    }

    public Pool(Proxy_Pool proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Pool(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name_label = Marshalling.ParseString(table, "name_label");
      this.name_description = Marshalling.ParseString(table, "name_description");
      this.master = Marshalling.ParseRef<Host>(table, "master");
      this.default_SR = Marshalling.ParseRef<SR>(table, "default_SR");
      this.suspend_image_SR = Marshalling.ParseRef<SR>(table, "suspend_image_SR");
      this.crash_dump_SR = Marshalling.ParseRef<SR>(table, "crash_dump_SR");
      this.other_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "other_config"));
      this.ha_enabled = Marshalling.ParseBool(table, "ha_enabled");
      this.ha_configuration = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "ha_configuration"));
      this.ha_statefiles = Marshalling.ParseStringArray(table, "ha_statefiles");
      this.ha_host_failures_to_tolerate = Marshalling.ParseLong(table, "ha_host_failures_to_tolerate");
      this.ha_plan_exists_for = Marshalling.ParseLong(table, "ha_plan_exists_for");
      this.ha_allow_overcommit = Marshalling.ParseBool(table, "ha_allow_overcommit");
      this.ha_overcommitted = Marshalling.ParseBool(table, "ha_overcommitted");
      this.blobs = Maps.convert_from_proxy_string_XenRefBlob((object) Marshalling.ParseHashTable(table, "blobs"));
      this.tags = Marshalling.ParseStringArray(table, "tags");
      this.gui_config = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "gui_config"));
      this.wlb_url = Marshalling.ParseString(table, "wlb_url");
      this.wlb_username = Marshalling.ParseString(table, "wlb_username");
      this.wlb_enabled = Marshalling.ParseBool(table, "wlb_enabled");
      this.wlb_verify_cert = Marshalling.ParseBool(table, "wlb_verify_cert");
      this.redo_log_enabled = Marshalling.ParseBool(table, "redo_log_enabled");
      this.redo_log_vdi = Marshalling.ParseRef<VDI>(table, "redo_log_vdi");
      this.vswitch_controller = Marshalling.ParseString(table, "vswitch_controller");
      this.restrictions = Maps.convert_from_proxy_string_string((object) Marshalling.ParseHashTable(table, "restrictions"));
      this.metadata_VDIs = Marshalling.ParseSetRef<VDI>(table, "metadata_VDIs");
    }

    public override void UpdateFrom(Pool update)
    {
      this.uuid = update.uuid;
      this.name_label = update.name_label;
      this.name_description = update.name_description;
      this.master = update.master;
      this.default_SR = update.default_SR;
      this.suspend_image_SR = update.suspend_image_SR;
      this.crash_dump_SR = update.crash_dump_SR;
      this.other_config = update.other_config;
      this.ha_enabled = update.ha_enabled;
      this.ha_configuration = update.ha_configuration;
      this.ha_statefiles = update.ha_statefiles;
      this.ha_host_failures_to_tolerate = update.ha_host_failures_to_tolerate;
      this.ha_plan_exists_for = update.ha_plan_exists_for;
      this.ha_allow_overcommit = update.ha_allow_overcommit;
      this.ha_overcommitted = update.ha_overcommitted;
      this.blobs = update.blobs;
      this.tags = update.tags;
      this.gui_config = update.gui_config;
      this.wlb_url = update.wlb_url;
      this.wlb_username = update.wlb_username;
      this.wlb_enabled = update.wlb_enabled;
      this.wlb_verify_cert = update.wlb_verify_cert;
      this.redo_log_enabled = update.redo_log_enabled;
      this.redo_log_vdi = update.redo_log_vdi;
      this.vswitch_controller = update.vswitch_controller;
      this.restrictions = update.restrictions;
      this.metadata_VDIs = update.metadata_VDIs;
    }

    internal void UpdateFromProxy(Proxy_Pool proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name_label = proxy.name_label == null ? (string) null : proxy.name_label;
      this.name_description = proxy.name_description == null ? (string) null : proxy.name_description;
      this.master = proxy.master == null ? (XenRef<Host>) null : XenRef<Host>.Create(proxy.master);
      this.default_SR = proxy.default_SR == null ? (XenRef<SR>) null : XenRef<SR>.Create(proxy.default_SR);
      this.suspend_image_SR = proxy.suspend_image_SR == null ? (XenRef<SR>) null : XenRef<SR>.Create(proxy.suspend_image_SR);
      this.crash_dump_SR = proxy.crash_dump_SR == null ? (XenRef<SR>) null : XenRef<SR>.Create(proxy.crash_dump_SR);
      this.other_config = proxy.other_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.other_config);
      this.ha_enabled = proxy.ha_enabled;
      this.ha_configuration = proxy.ha_configuration == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.ha_configuration);
      this.ha_statefiles = proxy.ha_statefiles == null ? new string[0] : proxy.ha_statefiles;
      this.ha_host_failures_to_tolerate = proxy.ha_host_failures_to_tolerate == null ? 0L : long.Parse(proxy.ha_host_failures_to_tolerate);
      this.ha_plan_exists_for = proxy.ha_plan_exists_for == null ? 0L : long.Parse(proxy.ha_plan_exists_for);
      this.ha_allow_overcommit = proxy.ha_allow_overcommit;
      this.ha_overcommitted = proxy.ha_overcommitted;
      this.blobs = proxy.blobs == null ? (Dictionary<string, XenRef<Blob>>) null : Maps.convert_from_proxy_string_XenRefBlob(proxy.blobs);
      this.tags = proxy.tags == null ? new string[0] : proxy.tags;
      this.gui_config = proxy.gui_config == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.gui_config);
      this.wlb_url = proxy.wlb_url == null ? (string) null : proxy.wlb_url;
      this.wlb_username = proxy.wlb_username == null ? (string) null : proxy.wlb_username;
      this.wlb_enabled = proxy.wlb_enabled;
      this.wlb_verify_cert = proxy.wlb_verify_cert;
      this.redo_log_enabled = proxy.redo_log_enabled;
      this.redo_log_vdi = proxy.redo_log_vdi == null ? (XenRef<VDI>) null : XenRef<VDI>.Create(proxy.redo_log_vdi);
      this.vswitch_controller = proxy.vswitch_controller == null ? (string) null : proxy.vswitch_controller;
      this.restrictions = proxy.restrictions == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(proxy.restrictions);
      this.metadata_VDIs = proxy.metadata_VDIs == null ? (List<XenRef<VDI>>) null : XenRef<VDI>.Create(proxy.metadata_VDIs);
    }

    public Proxy_Pool ToProxy()
    {
      return new Proxy_Pool()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name_label = this.name_label != null ? this.name_label : "",
        name_description = this.name_description != null ? this.name_description : "",
        master = this.master != null ? (string) this.master : "",
        default_SR = this.default_SR != null ? (string) this.default_SR : "",
        suspend_image_SR = this.suspend_image_SR != null ? (string) this.suspend_image_SR : "",
        crash_dump_SR = this.crash_dump_SR != null ? (string) this.crash_dump_SR : "",
        other_config = (object) Maps.convert_to_proxy_string_string(this.other_config),
        ha_enabled = this.ha_enabled,
        ha_configuration = (object) Maps.convert_to_proxy_string_string(this.ha_configuration),
        ha_statefiles = this.ha_statefiles,
        ha_host_failures_to_tolerate = this.ha_host_failures_to_tolerate.ToString(),
        ha_plan_exists_for = this.ha_plan_exists_for.ToString(),
        ha_allow_overcommit = this.ha_allow_overcommit,
        ha_overcommitted = this.ha_overcommitted,
        blobs = (object) Maps.convert_to_proxy_string_XenRefBlob(this.blobs),
        tags = this.tags,
        gui_config = (object) Maps.convert_to_proxy_string_string(this.gui_config),
        wlb_url = this.wlb_url != null ? this.wlb_url : "",
        wlb_username = this.wlb_username != null ? this.wlb_username : "",
        wlb_enabled = this.wlb_enabled,
        wlb_verify_cert = this.wlb_verify_cert,
        redo_log_enabled = this.redo_log_enabled,
        redo_log_vdi = this.redo_log_vdi != null ? (string) this.redo_log_vdi : "",
        vswitch_controller = this.vswitch_controller != null ? this.vswitch_controller : "",
        restrictions = (object) Maps.convert_to_proxy_string_string(this.restrictions),
        metadata_VDIs = this.metadata_VDIs != null ? Helper.RefListToStringArray<VDI>(this.metadata_VDIs) : new string[0]
      };
    }

    public bool DeepEquals(Pool other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._name_label, other._name_label) && (Helper.AreEqual2<string>(this._name_description, other._name_description) && Helper.AreEqual2<XenRef<Host>>(this._master, other._master)) && (Helper.AreEqual2<XenRef<SR>>(this._default_SR, other._default_SR) && Helper.AreEqual2<XenRef<SR>>(this._suspend_image_SR, other._suspend_image_SR) && (Helper.AreEqual2<XenRef<SR>>(this._crash_dump_SR, other._crash_dump_SR) && Helper.AreEqual2<Dictionary<string, string>>(this._other_config, other._other_config))) && (Helper.AreEqual2<bool>(this._ha_enabled, other._ha_enabled) && Helper.AreEqual2<Dictionary<string, string>>(this._ha_configuration, other._ha_configuration) && (Helper.AreEqual2<string[]>(this._ha_statefiles, other._ha_statefiles) && Helper.AreEqual2<long>(this._ha_host_failures_to_tolerate, other._ha_host_failures_to_tolerate)) && (Helper.AreEqual2<long>(this._ha_plan_exists_for, other._ha_plan_exists_for) && Helper.AreEqual2<bool>(this._ha_allow_overcommit, other._ha_allow_overcommit) && (Helper.AreEqual2<bool>(this._ha_overcommitted, other._ha_overcommitted) && Helper.AreEqual2<Dictionary<string, XenRef<Blob>>>(this._blobs, other._blobs)))) && (Helper.AreEqual2<string[]>(this._tags, other._tags) && Helper.AreEqual2<Dictionary<string, string>>(this._gui_config, other._gui_config) && (Helper.AreEqual2<string>(this._wlb_url, other._wlb_url) && Helper.AreEqual2<string>(this._wlb_username, other._wlb_username)) && (Helper.AreEqual2<bool>(this._wlb_enabled, other._wlb_enabled) && Helper.AreEqual2<bool>(this._wlb_verify_cert, other._wlb_verify_cert) && (Helper.AreEqual2<bool>(this._redo_log_enabled, other._redo_log_enabled) && Helper.AreEqual2<XenRef<VDI>>(this._redo_log_vdi, other._redo_log_vdi))) && (Helper.AreEqual2<string>(this._vswitch_controller, other._vswitch_controller) && Helper.AreEqual2<Dictionary<string, string>>(this._restrictions, other._restrictions))))
        return Helper.AreEqual2<List<XenRef<VDI>>>(this._metadata_VDIs, other._metadata_VDIs);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Pool server)
    {
      if (opaqueRef == null)
        return "";
      if (!Helper.AreEqual2<string>(this._name_label, server._name_label))
        Pool.set_name_label(session, opaqueRef, this._name_label);
      if (!Helper.AreEqual2<string>(this._name_description, server._name_description))
        Pool.set_name_description(session, opaqueRef, this._name_description);
      if (!Helper.AreEqual2<XenRef<SR>>(this._default_SR, server._default_SR))
        Pool.set_default_SR(session, opaqueRef, (string) this._default_SR);
      if (!Helper.AreEqual2<XenRef<SR>>(this._suspend_image_SR, server._suspend_image_SR))
        Pool.set_suspend_image_SR(session, opaqueRef, (string) this._suspend_image_SR);
      if (!Helper.AreEqual2<XenRef<SR>>(this._crash_dump_SR, server._crash_dump_SR))
        Pool.set_crash_dump_SR(session, opaqueRef, (string) this._crash_dump_SR);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._other_config, server._other_config))
        Pool.set_other_config(session, opaqueRef, this._other_config);
      if (!Helper.AreEqual2<bool>(this._ha_allow_overcommit, server._ha_allow_overcommit))
        Pool.set_ha_allow_overcommit(session, opaqueRef, this._ha_allow_overcommit);
      if (!Helper.AreEqual2<string[]>(this._tags, server._tags))
        Pool.set_tags(session, opaqueRef, this._tags);
      if (!Helper.AreEqual2<Dictionary<string, string>>(this._gui_config, server._gui_config))
        Pool.set_gui_config(session, opaqueRef, this._gui_config);
      if (!Helper.AreEqual2<bool>(this._wlb_enabled, server._wlb_enabled))
        Pool.set_wlb_enabled(session, opaqueRef, this._wlb_enabled);
      if (!Helper.AreEqual2<bool>(this._wlb_verify_cert, server._wlb_verify_cert))
        Pool.set_wlb_verify_cert(session, opaqueRef, this._wlb_verify_cert);
      return (string) null;
    }

    public static Pool get_record(Session session, string _pool)
    {
      return new Pool(session.proxy.pool_get_record(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static XenRef<Pool> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Pool>.Create(session.proxy.pool_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static string get_uuid(Session session, string _pool)
    {
      return session.proxy.pool_get_uuid(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static string get_name_label(Session session, string _pool)
    {
      return session.proxy.pool_get_name_label(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static string get_name_description(Session session, string _pool)
    {
      return session.proxy.pool_get_name_description(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static XenRef<Host> get_master(Session session, string _pool)
    {
      return XenRef<Host>.Create(session.proxy.pool_get_master(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static XenRef<SR> get_default_SR(Session session, string _pool)
    {
      return XenRef<SR>.Create(session.proxy.pool_get_default_sr(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static XenRef<SR> get_suspend_image_SR(Session session, string _pool)
    {
      return XenRef<SR>.Create(session.proxy.pool_get_suspend_image_sr(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static XenRef<SR> get_crash_dump_SR(Session session, string _pool)
    {
      return XenRef<SR>.Create(session.proxy.pool_get_crash_dump_sr(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static Dictionary<string, string> get_other_config(Session session, string _pool)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pool_get_other_config(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static bool get_ha_enabled(Session session, string _pool)
    {
      return session.proxy.pool_get_ha_enabled(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static Dictionary<string, string> get_ha_configuration(Session session, string _pool)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pool_get_ha_configuration(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static string[] get_ha_statefiles(Session session, string _pool)
    {
      return session.proxy.pool_get_ha_statefiles(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static long get_ha_host_failures_to_tolerate(Session session, string _pool)
    {
      return long.Parse(session.proxy.pool_get_ha_host_failures_to_tolerate(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static long get_ha_plan_exists_for(Session session, string _pool)
    {
      return long.Parse(session.proxy.pool_get_ha_plan_exists_for(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static bool get_ha_allow_overcommit(Session session, string _pool)
    {
      return session.proxy.pool_get_ha_allow_overcommit(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static bool get_ha_overcommitted(Session session, string _pool)
    {
      return session.proxy.pool_get_ha_overcommitted(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static Dictionary<string, XenRef<Blob>> get_blobs(Session session, string _pool)
    {
      return Maps.convert_from_proxy_string_XenRefBlob(session.proxy.pool_get_blobs(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static string[] get_tags(Session session, string _pool)
    {
      return session.proxy.pool_get_tags(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static Dictionary<string, string> get_gui_config(Session session, string _pool)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pool_get_gui_config(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static string get_wlb_url(Session session, string _pool)
    {
      return session.proxy.pool_get_wlb_url(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static string get_wlb_username(Session session, string _pool)
    {
      return session.proxy.pool_get_wlb_username(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static bool get_wlb_enabled(Session session, string _pool)
    {
      return session.proxy.pool_get_wlb_enabled(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static bool get_wlb_verify_cert(Session session, string _pool)
    {
      return session.proxy.pool_get_wlb_verify_cert(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static bool get_redo_log_enabled(Session session, string _pool)
    {
      return session.proxy.pool_get_redo_log_enabled(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static XenRef<VDI> get_redo_log_vdi(Session session, string _pool)
    {
      return XenRef<VDI>.Create(session.proxy.pool_get_redo_log_vdi(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static string get_vswitch_controller(Session session, string _pool)
    {
      return session.proxy.pool_get_vswitch_controller(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static Dictionary<string, string> get_restrictions(Session session, string _pool)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pool_get_restrictions(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static List<XenRef<VDI>> get_metadata_VDIs(Session session, string _pool)
    {
      return XenRef<VDI>.Create(session.proxy.pool_get_metadata_vdis(session.uuid, _pool != null ? _pool : "").parse());
    }

    public static void set_name_label(Session session, string _pool, string _name_label)
    {
      session.proxy.pool_set_name_label(session.uuid, _pool != null ? _pool : "", _name_label != null ? _name_label : "").parse();
    }

    public static void set_name_description(Session session, string _pool, string _name_description)
    {
      session.proxy.pool_set_name_description(session.uuid, _pool != null ? _pool : "", _name_description != null ? _name_description : "").parse();
    }

    public static void set_default_SR(Session session, string _pool, string _default_sr)
    {
      session.proxy.pool_set_default_sr(session.uuid, _pool != null ? _pool : "", _default_sr != null ? _default_sr : "").parse();
    }

    public static void set_suspend_image_SR(Session session, string _pool, string _suspend_image_sr)
    {
      session.proxy.pool_set_suspend_image_sr(session.uuid, _pool != null ? _pool : "", _suspend_image_sr != null ? _suspend_image_sr : "").parse();
    }

    public static void set_crash_dump_SR(Session session, string _pool, string _crash_dump_sr)
    {
      session.proxy.pool_set_crash_dump_sr(session.uuid, _pool != null ? _pool : "", _crash_dump_sr != null ? _crash_dump_sr : "").parse();
    }

    public static void set_other_config(Session session, string _pool, Dictionary<string, string> _other_config)
    {
      session.proxy.pool_set_other_config(session.uuid, _pool != null ? _pool : "", (object) Maps.convert_to_proxy_string_string(_other_config)).parse();
    }

    public static void add_to_other_config(Session session, string _pool, string _key, string _value)
    {
      session.proxy.pool_add_to_other_config(session.uuid, _pool != null ? _pool : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_other_config(Session session, string _pool, string _key)
    {
      session.proxy.pool_remove_from_other_config(session.uuid, _pool != null ? _pool : "", _key != null ? _key : "").parse();
    }

    public static void set_ha_allow_overcommit(Session session, string _pool, bool _ha_allow_overcommit)
    {
      session.proxy.pool_set_ha_allow_overcommit(session.uuid, _pool != null ? _pool : "", _ha_allow_overcommit).parse();
    }

    public static void set_tags(Session session, string _pool, string[] _tags)
    {
      session.proxy.pool_set_tags(session.uuid, _pool != null ? _pool : "", _tags).parse();
    }

    public static void add_tags(Session session, string _pool, string _value)
    {
      session.proxy.pool_add_tags(session.uuid, _pool != null ? _pool : "", _value != null ? _value : "").parse();
    }

    public static void remove_tags(Session session, string _pool, string _value)
    {
      session.proxy.pool_remove_tags(session.uuid, _pool != null ? _pool : "", _value != null ? _value : "").parse();
    }

    public static void set_gui_config(Session session, string _pool, Dictionary<string, string> _gui_config)
    {
      session.proxy.pool_set_gui_config(session.uuid, _pool != null ? _pool : "", (object) Maps.convert_to_proxy_string_string(_gui_config)).parse();
    }

    public static void add_to_gui_config(Session session, string _pool, string _key, string _value)
    {
      session.proxy.pool_add_to_gui_config(session.uuid, _pool != null ? _pool : "", _key != null ? _key : "", _value != null ? _value : "").parse();
    }

    public static void remove_from_gui_config(Session session, string _pool, string _key)
    {
      session.proxy.pool_remove_from_gui_config(session.uuid, _pool != null ? _pool : "", _key != null ? _key : "").parse();
    }

    public static void set_wlb_enabled(Session session, string _pool, bool _wlb_enabled)
    {
      session.proxy.pool_set_wlb_enabled(session.uuid, _pool != null ? _pool : "", _wlb_enabled).parse();
    }

    public static void set_wlb_verify_cert(Session session, string _pool, bool _wlb_verify_cert)
    {
      session.proxy.pool_set_wlb_verify_cert(session.uuid, _pool != null ? _pool : "", _wlb_verify_cert).parse();
    }

    public static void join(Session session, string _master_address, string _master_username, string _master_password)
    {
      session.proxy.pool_join(session.uuid, _master_address != null ? _master_address : "", _master_username != null ? _master_username : "", _master_password != null ? _master_password : "").parse();
    }

    public static XenRef<Task> async_join(Session session, string _master_address, string _master_username, string _master_password)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_join(session.uuid, _master_address != null ? _master_address : "", _master_username != null ? _master_username : "", _master_password != null ? _master_password : "").parse());
    }

    public static void join_force(Session session, string _master_address, string _master_username, string _master_password)
    {
      session.proxy.pool_join_force(session.uuid, _master_address != null ? _master_address : "", _master_username != null ? _master_username : "", _master_password != null ? _master_password : "").parse();
    }

    public static XenRef<Task> async_join_force(Session session, string _master_address, string _master_username, string _master_password)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_join_force(session.uuid, _master_address != null ? _master_address : "", _master_username != null ? _master_username : "", _master_password != null ? _master_password : "").parse());
    }

    public static void eject(Session session, string _host)
    {
      session.proxy.pool_eject(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_eject(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_eject(session.uuid, _host != null ? _host : "").parse());
    }

    public static void emergency_transition_to_master(Session session)
    {
      session.proxy.pool_emergency_transition_to_master(session.uuid).parse();
    }

    public static void emergency_reset_master(Session session, string _master_address)
    {
      session.proxy.pool_emergency_reset_master(session.uuid, _master_address != null ? _master_address : "").parse();
    }

    public static List<XenRef<Host>> recover_slaves(Session session)
    {
      return XenRef<Host>.Create(session.proxy.pool_recover_slaves(session.uuid).parse());
    }

    public static XenRef<Task> async_recover_slaves(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_recover_slaves(session.uuid).parse());
    }

    public static List<XenRef<PIF>> create_VLAN(Session session, string _device, string _network, long _vlan)
    {
      return XenRef<PIF>.Create(session.proxy.pool_create_vlan(session.uuid, _device != null ? _device : "", _network != null ? _network : "", _vlan.ToString()).parse());
    }

    public static XenRef<Task> async_create_VLAN(Session session, string _device, string _network, long _vlan)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_create_vlan(session.uuid, _device != null ? _device : "", _network != null ? _network : "", _vlan.ToString()).parse());
    }

    public static List<XenRef<PIF>> create_VLAN_from_PIF(Session session, string _pif, string _network, long _vlan)
    {
      return XenRef<PIF>.Create(session.proxy.pool_create_vlan_from_pif(session.uuid, _pif != null ? _pif : "", _network != null ? _network : "", _vlan.ToString()).parse());
    }

    public static XenRef<Task> async_create_VLAN_from_PIF(Session session, string _pif, string _network, long _vlan)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_create_vlan_from_pif(session.uuid, _pif != null ? _pif : "", _network != null ? _network : "", _vlan.ToString()).parse());
    }

    public static void enable_ha(Session session, List<XenRef<SR>> _heartbeat_srs, Dictionary<string, string> _configuration)
    {
      session.proxy.pool_enable_ha(session.uuid, _heartbeat_srs != null ? Helper.RefListToStringArray<SR>(_heartbeat_srs) : new string[0], (object) Maps.convert_to_proxy_string_string(_configuration)).parse();
    }

    public static XenRef<Task> async_enable_ha(Session session, List<XenRef<SR>> _heartbeat_srs, Dictionary<string, string> _configuration)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_enable_ha(session.uuid, _heartbeat_srs != null ? Helper.RefListToStringArray<SR>(_heartbeat_srs) : new string[0], (object) Maps.convert_to_proxy_string_string(_configuration)).parse());
    }

    public static void disable_ha(Session session)
    {
      session.proxy.pool_disable_ha(session.uuid).parse();
    }

    public static XenRef<Task> async_disable_ha(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_disable_ha(session.uuid).parse());
    }

    public static void sync_database(Session session)
    {
      session.proxy.pool_sync_database(session.uuid).parse();
    }

    public static XenRef<Task> async_sync_database(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_sync_database(session.uuid).parse());
    }

    public static void designate_new_master(Session session, string _host)
    {
      session.proxy.pool_designate_new_master(session.uuid, _host != null ? _host : "").parse();
    }

    public static XenRef<Task> async_designate_new_master(Session session, string _host)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_designate_new_master(session.uuid, _host != null ? _host : "").parse());
    }

    public static void ha_prevent_restarts_for(Session session, long _seconds)
    {
      session.proxy.pool_ha_prevent_restarts_for(session.uuid, _seconds.ToString()).parse();
    }

    public static bool ha_failover_plan_exists(Session session, long _n)
    {
      return session.proxy.pool_ha_failover_plan_exists(session.uuid, _n.ToString()).parse();
    }

    public static long ha_compute_max_host_failures_to_tolerate(Session session)
    {
      return long.Parse(session.proxy.pool_ha_compute_max_host_failures_to_tolerate(session.uuid).parse());
    }

    public static long ha_compute_hypothetical_max_host_failures_to_tolerate(Session session, Dictionary<XenRef<VM>, string> _configuration)
    {
      return long.Parse(session.proxy.pool_ha_compute_hypothetical_max_host_failures_to_tolerate(session.uuid, (object) Maps.convert_to_proxy_XenRefVM_string(_configuration)).parse());
    }

    public static Dictionary<XenRef<VM>, Dictionary<string, string>> ha_compute_vm_failover_plan(Session session, List<XenRef<Host>> _failed_hosts, List<XenRef<VM>> _failed_vms)
    {
      return Maps.convert_from_proxy_XenRefVM_Dictionary_string_string(session.proxy.pool_ha_compute_vm_failover_plan(session.uuid, _failed_hosts != null ? Helper.RefListToStringArray<Host>(_failed_hosts) : new string[0], _failed_vms != null ? Helper.RefListToStringArray<VM>(_failed_vms) : new string[0]).parse());
    }

    public static void set_ha_host_failures_to_tolerate(Session session, string _self, long _value)
    {
      session.proxy.pool_set_ha_host_failures_to_tolerate(session.uuid, _self != null ? _self : "", _value.ToString()).parse();
    }

    public static XenRef<Task> async_set_ha_host_failures_to_tolerate(Session session, string _self, long _value)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_set_ha_host_failures_to_tolerate(session.uuid, _self != null ? _self : "", _value.ToString()).parse());
    }

    public static XenRef<Blob> create_new_blob(Session session, string _pool, string _name, string _mime_type, bool _public)
    {
      return XenRef<Blob>.Create(session.proxy.pool_create_new_blob(session.uuid, _pool != null ? _pool : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _pool, string _name, string _mime_type, bool _public)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_create_new_blob(session.uuid, _pool != null ? _pool : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "", _public).parse());
    }

    public static void enable_external_auth(Session session, string _pool, Dictionary<string, string> _config, string _service_name, string _auth_type)
    {
      session.proxy.pool_enable_external_auth(session.uuid, _pool != null ? _pool : "", (object) Maps.convert_to_proxy_string_string(_config), _service_name != null ? _service_name : "", _auth_type != null ? _auth_type : "").parse();
    }

    public static void disable_external_auth(Session session, string _pool, Dictionary<string, string> _config)
    {
      session.proxy.pool_disable_external_auth(session.uuid, _pool != null ? _pool : "", (object) Maps.convert_to_proxy_string_string(_config)).parse();
    }

    public static void detect_nonhomogeneous_external_auth(Session session, string _pool)
    {
      session.proxy.pool_detect_nonhomogeneous_external_auth(session.uuid, _pool != null ? _pool : "").parse();
    }

    public static void initialize_wlb(Session session, string _wlb_url, string _wlb_username, string _wlb_password, string _xenserver_username, string _xenserver_password)
    {
      session.proxy.pool_initialize_wlb(session.uuid, _wlb_url != null ? _wlb_url : "", _wlb_username != null ? _wlb_username : "", _wlb_password != null ? _wlb_password : "", _xenserver_username != null ? _xenserver_username : "", _xenserver_password != null ? _xenserver_password : "").parse();
    }

    public static XenRef<Task> async_initialize_wlb(Session session, string _wlb_url, string _wlb_username, string _wlb_password, string _xenserver_username, string _xenserver_password)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_initialize_wlb(session.uuid, _wlb_url != null ? _wlb_url : "", _wlb_username != null ? _wlb_username : "", _wlb_password != null ? _wlb_password : "", _xenserver_username != null ? _xenserver_username : "", _xenserver_password != null ? _xenserver_password : "").parse());
    }

    public static void deconfigure_wlb(Session session)
    {
      session.proxy.pool_deconfigure_wlb(session.uuid).parse();
    }

    public static XenRef<Task> async_deconfigure_wlb(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_deconfigure_wlb(session.uuid).parse());
    }

    public static void send_wlb_configuration(Session session, Dictionary<string, string> _config)
    {
      session.proxy.pool_send_wlb_configuration(session.uuid, (object) Maps.convert_to_proxy_string_string(_config)).parse();
    }

    public static XenRef<Task> async_send_wlb_configuration(Session session, Dictionary<string, string> _config)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_send_wlb_configuration(session.uuid, (object) Maps.convert_to_proxy_string_string(_config)).parse());
    }

    public static Dictionary<string, string> retrieve_wlb_configuration(Session session)
    {
      return Maps.convert_from_proxy_string_string(session.proxy.pool_retrieve_wlb_configuration(session.uuid).parse());
    }

    public static XenRef<Task> async_retrieve_wlb_configuration(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_retrieve_wlb_configuration(session.uuid).parse());
    }

    public static Dictionary<XenRef<VM>, string[]> retrieve_wlb_recommendations(Session session)
    {
      return Maps.convert_from_proxy_XenRefVM_string_array(session.proxy.pool_retrieve_wlb_recommendations(session.uuid).parse());
    }

    public static XenRef<Task> async_retrieve_wlb_recommendations(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_retrieve_wlb_recommendations(session.uuid).parse());
    }

    public static string send_test_post(Session session, string _host, long _port, string _body)
    {
      return session.proxy.pool_send_test_post(session.uuid, _host != null ? _host : "", _port.ToString(), _body != null ? _body : "").parse();
    }

    public static XenRef<Task> async_send_test_post(Session session, string _host, long _port, string _body)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_send_test_post(session.uuid, _host != null ? _host : "", _port.ToString(), _body != null ? _body : "").parse());
    }

    public static void certificate_install(Session session, string _name, string _cert)
    {
      session.proxy.pool_certificate_install(session.uuid, _name != null ? _name : "", _cert != null ? _cert : "").parse();
    }

    public static XenRef<Task> async_certificate_install(Session session, string _name, string _cert)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_certificate_install(session.uuid, _name != null ? _name : "", _cert != null ? _cert : "").parse());
    }

    public static void certificate_uninstall(Session session, string _name)
    {
      session.proxy.pool_certificate_uninstall(session.uuid, _name != null ? _name : "").parse();
    }

    public static XenRef<Task> async_certificate_uninstall(Session session, string _name)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_certificate_uninstall(session.uuid, _name != null ? _name : "").parse());
    }

    public static string[] certificate_list(Session session)
    {
      return session.proxy.pool_certificate_list(session.uuid).parse();
    }

    public static XenRef<Task> async_certificate_list(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_certificate_list(session.uuid).parse());
    }

    public static void crl_install(Session session, string _name, string _cert)
    {
      session.proxy.pool_crl_install(session.uuid, _name != null ? _name : "", _cert != null ? _cert : "").parse();
    }

    public static XenRef<Task> async_crl_install(Session session, string _name, string _cert)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_crl_install(session.uuid, _name != null ? _name : "", _cert != null ? _cert : "").parse());
    }

    public static void crl_uninstall(Session session, string _name)
    {
      session.proxy.pool_crl_uninstall(session.uuid, _name != null ? _name : "").parse();
    }

    public static XenRef<Task> async_crl_uninstall(Session session, string _name)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_crl_uninstall(session.uuid, _name != null ? _name : "").parse());
    }

    public static string[] crl_list(Session session)
    {
      return session.proxy.pool_crl_list(session.uuid).parse();
    }

    public static XenRef<Task> async_crl_list(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_crl_list(session.uuid).parse());
    }

    public static void certificate_sync(Session session)
    {
      session.proxy.pool_certificate_sync(session.uuid).parse();
    }

    public static XenRef<Task> async_certificate_sync(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_certificate_sync(session.uuid).parse());
    }

    public static void enable_redo_log(Session session, string _sr)
    {
      session.proxy.pool_enable_redo_log(session.uuid, _sr != null ? _sr : "").parse();
    }

    public static XenRef<Task> async_enable_redo_log(Session session, string _sr)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_enable_redo_log(session.uuid, _sr != null ? _sr : "").parse());
    }

    public static void disable_redo_log(Session session)
    {
      session.proxy.pool_disable_redo_log(session.uuid).parse();
    }

    public static XenRef<Task> async_disable_redo_log(Session session)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_disable_redo_log(session.uuid).parse());
    }

    public static void set_vswitch_controller(Session session, string _address)
    {
      session.proxy.pool_set_vswitch_controller(session.uuid, _address != null ? _address : "").parse();
    }

    public static XenRef<Task> async_set_vswitch_controller(Session session, string _address)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_set_vswitch_controller(session.uuid, _address != null ? _address : "").parse());
    }

    public static string test_archive_target(Session session, string _self, Dictionary<string, string> _config)
    {
      return session.proxy.pool_test_archive_target(session.uuid, _self != null ? _self : "", (object) Maps.convert_to_proxy_string_string(_config)).parse();
    }

    public static void enable_local_storage_caching(Session session, string _self)
    {
      session.proxy.pool_enable_local_storage_caching(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_enable_local_storage_caching(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_enable_local_storage_caching(session.uuid, _self != null ? _self : "").parse());
    }

    public static void disable_local_storage_caching(Session session, string _self)
    {
      session.proxy.pool_disable_local_storage_caching(session.uuid, _self != null ? _self : "").parse();
    }

    public static XenRef<Task> async_disable_local_storage_caching(Session session, string _self)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_disable_local_storage_caching(session.uuid, _self != null ? _self : "").parse());
    }

    public static List<XenRef<Pool>> get_all(Session session)
    {
      return XenRef<Pool>.Create(session.proxy.pool_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Pool>, Pool> get_all_records(Session session)
    {
      return XenRef<Pool>.Create<Proxy_Pool>(session.proxy.pool_get_all_records(session.uuid).parse());
    }

    public static XenRef<Blob> create_new_blob(Session session, string _pool, string _name, string _mime_type)
    {
      return XenRef<Blob>.Create(session.proxy.pool_create_new_blob(session.uuid, _pool != null ? _pool : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }

    public static XenRef<Task> async_create_new_blob(Session session, string _pool, string _name, string _mime_type)
    {
      return XenRef<Task>.Create(session.proxy.async_pool_create_new_blob(session.uuid, _pool != null ? _pool : "", _name != null ? _name : "", _mime_type != null ? _mime_type : "").parse());
    }
  }
}
