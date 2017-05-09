using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	public class WlbAuditLogEntry
	{
		private int _auditId;
		private int _poolId;
		private DateTime _timeStamp;
		private string _userId;
		private string _userName;
		private bool _accessAllowed;
		private bool _successed;
		private string _errorInfo;
		private string _eventObject;
		private string _eventObjectName;
		private string _eventObjectType;
		private string _eventObjectUuid;
		private string _eventObjectOpaqueref;
		private string _eventAction;
		private Dictionary<string, string> _eventParameters;
		private string _logType;
		private string _taskNameId;
		private string _sessionId;
		private string _callType;
		private static bool _traceEnabled = Configuration.GetValueAsBool(ConfigItems.AuditLogTrace);
		public int AuditId
		{
			get
			{
				return this._auditId;
			}
			set
			{
				this._auditId = value;
			}
		}
		public int PoolId
		{
			get
			{
				return this._poolId;
			}
			set
			{
				this._poolId = value;
			}
		}
		public DateTime TimeStamp
		{
			get
			{
				return this._timeStamp;
			}
			set
			{
				this._timeStamp = value;
			}
		}
		public string UserId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}
		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this._userName = value;
			}
		}
		public bool AccessAllowed
		{
			get
			{
				return this._accessAllowed;
			}
			set
			{
				this._accessAllowed = value;
			}
		}
		public bool Succeeded
		{
			get
			{
				return this._successed;
			}
			set
			{
				this._successed = value;
			}
		}
		public string ErrorInfo
		{
			get
			{
				return this._errorInfo;
			}
			set
			{
				this._errorInfo = ((value.Length >= 1024) ? value.Substring(0, 1023) : value);
			}
		}
		public string EventObject
		{
			get
			{
				return this._eventObject;
			}
			set
			{
				this._eventObject = value;
			}
		}
		public string EventObjectName
		{
			get
			{
				return this._eventObjectName;
			}
			set
			{
				this._eventObjectName = ((value.Length >= 1024) ? value.Substring(0, 1023) : value);
			}
		}
		public string EventObjectType
		{
			get
			{
				return this._eventObjectType;
			}
			set
			{
				this._eventObjectType = value;
			}
		}
		public string EventObjectUuid
		{
			get
			{
				return this._eventObjectUuid;
			}
			set
			{
				this._eventObjectUuid = value;
			}
		}
		public string EventObjectOpaqueref
		{
			get
			{
				return this._eventObjectOpaqueref;
			}
			set
			{
				this._eventObjectOpaqueref = value;
			}
		}
		public string EventAction
		{
			get
			{
				return this._eventAction;
			}
			set
			{
				this._eventAction = value;
			}
		}
		public Dictionary<string, string> EventParameters
		{
			get
			{
				if (this._eventParameters == null)
				{
					this._eventParameters = new Dictionary<string, string>();
				}
				return this._eventParameters;
			}
		}
		public string LogType
		{
			get
			{
				return this._logType;
			}
			set
			{
				this._logType = value;
			}
		}
		public string TaskNameId
		{
			get
			{
				return this._taskNameId;
			}
			set
			{
				this._taskNameId = value;
			}
		}
		public string SessionId
		{
			get
			{
				return this._sessionId;
			}
			set
			{
				this._sessionId = value;
			}
		}
		public string CallType
		{
			get
			{
				return this._callType;
			}
			set
			{
				this._callType = value;
			}
		}
	}
}
