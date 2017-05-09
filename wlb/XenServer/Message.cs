// Decompiled with JetBrains decompiler
// Type: XenAPI.Message
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Message : XenObject<Message>
  {
    private string _uuid;
    private string _name;
    private long _priority;
    private cls _cls;
    private string _obj_uuid;
    private DateTime _timestamp;
    private string _body;

    public Message.MessageType Type
    {
      get
      {
        switch (this.name)
        {
          case "BOND_STATUS_CHANGED":
            return Message.MessageType.BOND_STATUS_CHANGED;
          case "VMPP_SNAPSHOT_ARCHIVE_ALREADY_EXISTS":
            return Message.MessageType.VMPP_SNAPSHOT_ARCHIVE_ALREADY_EXISTS;
          case "VMPP_SNAPSHOT_FAILED":
            return Message.MessageType.VMPP_SNAPSHOT_FAILED;
          case "VMPP_ARCHIVE_MISSED_EVENT":
            return Message.MessageType.VMPP_ARCHIVE_MISSED_EVENT;
          case "VMPP_SNAPSHOT_MISSED_EVENT":
            return Message.MessageType.VMPP_SNAPSHOT_MISSED_EVENT;
          case "VMPP_XAPI_LOGON_FAILURE":
            return Message.MessageType.VMPP_XAPI_LOGON_FAILURE;
          case "VMPP_LICENSE_ERROR":
            return Message.MessageType.VMPP_LICENSE_ERROR;
          case "VMPP_ARCHIVE_TARGET_UNMOUNT_FAILED":
            return Message.MessageType.VMPP_ARCHIVE_TARGET_UNMOUNT_FAILED;
          case "VMPP_ARCHIVE_TARGET_MOUNT_FAILED":
            return Message.MessageType.VMPP_ARCHIVE_TARGET_MOUNT_FAILED;
          case "VMPP_ARCHIVE_SUCCEEDED":
            return Message.MessageType.VMPP_ARCHIVE_SUCCEEDED;
          case "VMPP_ARCHIVE_FAILED_0":
            return Message.MessageType.VMPP_ARCHIVE_FAILED_0;
          case "VMPP_ARCHIVE_LOCK_FAILED":
            return Message.MessageType.VMPP_ARCHIVE_LOCK_FAILED;
          case "VMPP_SNAPSHOT_SUCCEEDED":
            return Message.MessageType.VMPP_SNAPSHOT_SUCCEEDED;
          case "VMPP_SNAPSHOT_LOCK_FAILED":
            return Message.MessageType.VMPP_SNAPSHOT_LOCK_FAILED;
          case "LICENSE_SERVER_UNREACHABLE":
            return Message.MessageType.LICENSE_SERVER_UNREACHABLE;
          case "LICENSE_NOT_AVAILABLE":
            return Message.MessageType.LICENSE_NOT_AVAILABLE;
          case "GRACE_LICENSE":
            return Message.MessageType.GRACE_LICENSE;
          case "LICENSE_EXPIRED":
            return Message.MessageType.LICENSE_EXPIRED;
          case "LICENSE_SERVER_UNAVAILABLE":
            return Message.MessageType.LICENSE_SERVER_UNAVAILABLE;
          case "LICENSE_SERVER_CONNECTED":
            return Message.MessageType.LICENSE_SERVER_CONNECTED;
          case "MULTIPATH_PERIODIC_ALERT":
            return Message.MessageType.MULTIPATH_PERIODIC_ALERT;
          case "EXTAUTH_IN_POOL_IS_NON_HOMOGENEOUS":
            return Message.MessageType.EXTAUTH_IN_POOL_IS_NON_HOMOGENEOUS;
          case "EXTAUTH_INIT_IN_HOST_FAILED":
            return Message.MessageType.EXTAUTH_INIT_IN_HOST_FAILED;
          case "WLB_OPTIMIZATION_ALERT":
            return Message.MessageType.WLB_OPTIMIZATION_ALERT;
          case "WLB_CONSULTATION_FAILED":
            return Message.MessageType.WLB_CONSULTATION_FAILED;
          case "ALARM":
            return Message.MessageType.ALARM;
          case "PBD_PLUG_FAILED_ON_SERVER_START":
            return Message.MessageType.PBD_PLUG_FAILED_ON_SERVER_START;
          case "POOL_MASTER_TRANSITION":
            return Message.MessageType.POOL_MASTER_TRANSITION;
          case "HOST_CLOCK_WENT_BACKWARDS":
            return Message.MessageType.HOST_CLOCK_WENT_BACKWARDS;
          case "HOST_CLOCK_SKEW_DETECTED":
            return Message.MessageType.HOST_CLOCK_SKEW_DETECTED;
          case "HOST_SYNC_DATA_FAILED":
            return Message.MessageType.HOST_SYNC_DATA_FAILED;
          case "VM_CLONED":
            return Message.MessageType.VM_CLONED;
          case "VM_CRASHED":
            return Message.MessageType.VM_CRASHED;
          case "VM_RESUMED":
            return Message.MessageType.VM_RESUMED;
          case "VM_SUSPENDED":
            return Message.MessageType.VM_SUSPENDED;
          case "VM_REBOOTED":
            return Message.MessageType.VM_REBOOTED;
          case "VM_SHUTDOWN":
            return Message.MessageType.VM_SHUTDOWN;
          case "VM_STARTED":
            return Message.MessageType.VM_STARTED;
          case "VCPU_QOS_FAILED":
            return Message.MessageType.VCPU_QOS_FAILED;
          case "VBD_QOS_FAILED":
            return Message.MessageType.VBD_QOS_FAILED;
          case "VIF_QOS_FAILED":
            return Message.MessageType.VIF_QOS_FAILED;
          case "IP_CONFIGURED_PIF_CAN_UNPLUG":
            return Message.MessageType.IP_CONFIGURED_PIF_CAN_UNPLUG;
          case "METADATA_LUN_BROKEN":
            return Message.MessageType.METADATA_LUN_BROKEN;
          case "METADATA_LUN_HEALTHY":
            return Message.MessageType.METADATA_LUN_HEALTHY;
          case "HA_HOST_WAS_FENCED":
            return Message.MessageType.HA_HOST_WAS_FENCED;
          case "HA_HOST_FAILED":
            return Message.MessageType.HA_HOST_FAILED;
          case "HA_PROTECTED_VM_RESTART_FAILED":
            return Message.MessageType.HA_PROTECTED_VM_RESTART_FAILED;
          case "HA_POOL_DROP_IN_PLAN_EXISTS_FOR":
            return Message.MessageType.HA_POOL_DROP_IN_PLAN_EXISTS_FOR;
          case "HA_POOL_OVERCOMMITTED":
            return Message.MessageType.HA_POOL_OVERCOMMITTED;
          case "HA_NETWORK_BONDING_ERROR":
            return Message.MessageType.HA_NETWORK_BONDING_ERROR;
          case "HA_XAPI_HEALTHCHECK_APPROACHING_TIMEOUT":
            return Message.MessageType.HA_XAPI_HEALTHCHECK_APPROACHING_TIMEOUT;
          case "HA_STATEFILE_APPROACHING_TIMEOUT":
            return Message.MessageType.HA_STATEFILE_APPROACHING_TIMEOUT;
          case "HA_HEARTBEAT_APPROACHING_TIMEOUT":
            return Message.MessageType.HA_HEARTBEAT_APPROACHING_TIMEOUT;
          case "HA_STATEFILE_LOST":
            return Message.MessageType.HA_STATEFILE_LOST;
          case "LICENSE_EXPIRES_SOON":
            return Message.MessageType.LICENSE_EXPIRES_SOON;
          case "LICENSE_DOES_NOT_SUPPORT_POOLING":
            return Message.MessageType.LICENSE_DOES_NOT_SUPPORT_POOLING;
          default:
            return Message.MessageType.unknown;
        }
      }
    }

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

    public string name
    {
      get
      {
        return this._name;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._name))
          return;
        this._name = value;
        this.Changed = true;
        this.NotifyPropertyChanged("name");
      }
    }

    public long priority
    {
      get
      {
        return this._priority;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._priority))
          return;
        this._priority = value;
        this.Changed = true;
        this.NotifyPropertyChanged("priority");
      }
    }

    public cls cls
    {
      get
      {
        return this._cls;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._cls))
          return;
        this._cls = value;
        this.Changed = true;
        this.NotifyPropertyChanged("cls");
      }
    }

    public string obj_uuid
    {
      get
      {
        return this._obj_uuid;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._obj_uuid))
          return;
        this._obj_uuid = value;
        this.Changed = true;
        this.NotifyPropertyChanged("obj_uuid");
      }
    }

    public DateTime timestamp
    {
      get
      {
        return this._timestamp;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._timestamp))
          return;
        this._timestamp = value;
        this.Changed = true;
        this.NotifyPropertyChanged("timestamp");
      }
    }

    public string body
    {
      get
      {
        return this._body;
      }
      set
      {
        if (Helper.AreEqual((object) value, (object) this._body))
          return;
        this._body = value;
        this.Changed = true;
        this.NotifyPropertyChanged("body");
      }
    }

    public Message()
    {
    }

    public Message(string uuid, string name, long priority, cls cls, string obj_uuid, DateTime timestamp, string body)
    {
      this.uuid = uuid;
      this.name = name;
      this.priority = priority;
      this.cls = cls;
      this.obj_uuid = obj_uuid;
      this.timestamp = timestamp;
      this.body = body;
    }

    public Message(Proxy_Message proxy)
    {
      this.UpdateFromProxy(proxy);
    }

    public Message(Hashtable table)
    {
      this.uuid = Marshalling.ParseString(table, "uuid");
      this.name = Marshalling.ParseString(table, "name");
      this.priority = Marshalling.ParseLong(table, "priority");
      this.cls = (cls) Helper.EnumParseDefault(typeof (cls), Marshalling.ParseString(table, "cls"));
      this.obj_uuid = Marshalling.ParseString(table, "obj_uuid");
      this.timestamp = Marshalling.ParseDateTime(table, "timestamp");
      this.body = Marshalling.ParseString(table, "body");
    }

    public override void UpdateFrom(Message update)
    {
      this.uuid = update.uuid;
      this.name = update.name;
      this.priority = update.priority;
      this.cls = update.cls;
      this.obj_uuid = update.obj_uuid;
      this.timestamp = update.timestamp;
      this.body = update.body;
    }

    internal void UpdateFromProxy(Proxy_Message proxy)
    {
      this.uuid = proxy.uuid == null ? (string) null : proxy.uuid;
      this.name = proxy.name == null ? (string) null : proxy.name;
      this.priority = proxy.priority == null ? 0L : long.Parse(proxy.priority);
      this.cls = proxy.cls == null ? cls.VM : (cls) Helper.EnumParseDefault(typeof (cls), proxy.cls);
      this.obj_uuid = proxy.obj_uuid == null ? (string) null : proxy.obj_uuid;
      this.timestamp = proxy.timestamp;
      this.body = proxy.body == null ? (string) null : proxy.body;
    }

    public Proxy_Message ToProxy()
    {
      return new Proxy_Message()
      {
        uuid = this.uuid != null ? this.uuid : "",
        name = this.name != null ? this.name : "",
        priority = this.priority.ToString(),
        cls = cls_helper.ToString(this.cls),
        obj_uuid = this.obj_uuid != null ? this.obj_uuid : "",
        timestamp = this.timestamp,
        body = this.body != null ? this.body : ""
      };
    }

    public bool DeepEquals(Message other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (Helper.AreEqual2<string>(this._uuid, other._uuid) && Helper.AreEqual2<string>(this._name, other._name) && (Helper.AreEqual2<long>(this._priority, other._priority) && Helper.AreEqual2<cls>(this._cls, other._cls)) && (Helper.AreEqual2<string>(this._obj_uuid, other._obj_uuid) && Helper.AreEqual2<DateTime>(this._timestamp, other._timestamp)))
        return Helper.AreEqual2<string>(this._body, other._body);
      return false;
    }

    public override string SaveChanges(Session session, string opaqueRef, Message server)
    {
      if (opaqueRef == null)
        return "";
      throw new InvalidOperationException("This type has no read/write properties");
    }

    public static XenRef<Message> create(Session session, string _name, long _priority, cls _cls, string _obj_uuid, string _body)
    {
      return XenRef<Message>.Create(session.proxy.message_create(session.uuid, _name != null ? _name : "", _priority.ToString(), cls_helper.ToString(_cls), _obj_uuid != null ? _obj_uuid : "", _body != null ? _body : "").parse());
    }

    public static void destroy(Session session, string _self)
    {
      session.proxy.message_destroy(session.uuid, _self != null ? _self : "").parse();
    }

    public static Dictionary<XenRef<Message>, Message> get(Session session, cls _cls, string _obj_uuid, DateTime _since)
    {
      return XenRef<Message>.Create<Proxy_Message>(session.proxy.message_get(session.uuid, cls_helper.ToString(_cls), _obj_uuid != null ? _obj_uuid : "", _since).parse());
    }

    public static List<XenRef<Message>> get_all(Session session)
    {
      return XenRef<Message>.Create(session.proxy.message_get_all(session.uuid).parse());
    }

    public static Dictionary<XenRef<Message>, Message> get_since(Session session, DateTime _since)
    {
      return XenRef<Message>.Create<Proxy_Message>(session.proxy.message_get_since(session.uuid, _since).parse());
    }

    public static Message get_record(Session session, string _self)
    {
      return new Message(session.proxy.message_get_record(session.uuid, _self != null ? _self : "").parse());
    }

    public static XenRef<Message> get_by_uuid(Session session, string _uuid)
    {
      return XenRef<Message>.Create(session.proxy.message_get_by_uuid(session.uuid, _uuid != null ? _uuid : "").parse());
    }

    public static Dictionary<XenRef<Message>, Message> get_all_records(Session session)
    {
      return XenRef<Message>.Create<Proxy_Message>(session.proxy.message_get_all_records(session.uuid).parse());
    }

    public enum MessageType
    {
      BOND_STATUS_CHANGED,
      VMPP_SNAPSHOT_ARCHIVE_ALREADY_EXISTS,
      VMPP_SNAPSHOT_FAILED,
      VMPP_ARCHIVE_MISSED_EVENT,
      VMPP_SNAPSHOT_MISSED_EVENT,
      VMPP_XAPI_LOGON_FAILURE,
      VMPP_LICENSE_ERROR,
      VMPP_ARCHIVE_TARGET_UNMOUNT_FAILED,
      VMPP_ARCHIVE_TARGET_MOUNT_FAILED,
      VMPP_ARCHIVE_SUCCEEDED,
      VMPP_ARCHIVE_FAILED_0,
      VMPP_ARCHIVE_LOCK_FAILED,
      VMPP_SNAPSHOT_SUCCEEDED,
      VMPP_SNAPSHOT_LOCK_FAILED,
      LICENSE_SERVER_UNREACHABLE,
      LICENSE_NOT_AVAILABLE,
      GRACE_LICENSE,
      LICENSE_EXPIRED,
      LICENSE_SERVER_UNAVAILABLE,
      LICENSE_SERVER_CONNECTED,
      MULTIPATH_PERIODIC_ALERT,
      EXTAUTH_IN_POOL_IS_NON_HOMOGENEOUS,
      EXTAUTH_INIT_IN_HOST_FAILED,
      WLB_OPTIMIZATION_ALERT,
      WLB_CONSULTATION_FAILED,
      ALARM,
      PBD_PLUG_FAILED_ON_SERVER_START,
      POOL_MASTER_TRANSITION,
      HOST_CLOCK_WENT_BACKWARDS,
      HOST_CLOCK_SKEW_DETECTED,
      HOST_SYNC_DATA_FAILED,
      VM_CLONED,
      VM_CRASHED,
      VM_RESUMED,
      VM_SUSPENDED,
      VM_REBOOTED,
      VM_SHUTDOWN,
      VM_STARTED,
      VCPU_QOS_FAILED,
      VBD_QOS_FAILED,
      VIF_QOS_FAILED,
      IP_CONFIGURED_PIF_CAN_UNPLUG,
      METADATA_LUN_BROKEN,
      METADATA_LUN_HEALTHY,
      HA_HOST_WAS_FENCED,
      HA_HOST_FAILED,
      HA_PROTECTED_VM_RESTART_FAILED,
      HA_POOL_DROP_IN_PLAN_EXISTS_FOR,
      HA_POOL_OVERCOMMITTED,
      HA_NETWORK_BONDING_ERROR,
      HA_XAPI_HEALTHCHECK_APPROACHING_TIMEOUT,
      HA_STATEFILE_APPROACHING_TIMEOUT,
      HA_HEARTBEAT_APPROACHING_TIMEOUT,
      HA_STATEFILE_LOST,
      LICENSE_EXPIRES_SOON,
      LICENSE_DOES_NOT_SUPPORT_POOLING,
      unknown,
    }
  }
}
