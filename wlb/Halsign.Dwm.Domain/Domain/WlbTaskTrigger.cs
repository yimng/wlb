using System;
namespace Halsign.DWM.Domain
{
	public class WlbTaskTrigger
	{
		private int _id;
		private WlbTriggerType _triggerType;
		private WlbTaskAction _action;
		private TriggerDaysOfWeek _daysOfWeek;
		private DateTime _utcExecuteTime;
		private DateTime _lastRun;
		private WlbTriggerState _state;
		private DateTime _utcEnableDate;
		private DateTime _utcDisableDate;
		public int TriggerId
		{
			get
			{
				return this._id;
			}
			internal set
			{
				this._id = value;
			}
		}
		public WlbTriggerType Type
		{
			get
			{
				return this._triggerType;
			}
			set
			{
				this._triggerType = value;
			}
		}
		public WlbTaskAction Action
		{
			get
			{
				return DwmBase.SafeGetItem<WlbTaskAction>(ref this._action);
			}
			set
			{
				this._action = value;
			}
		}
		public TriggerDaysOfWeek DaysOfWeek
		{
			get
			{
				return this._daysOfWeek;
			}
			set
			{
				this._daysOfWeek = value;
			}
		}
		public DateTime ExecuteTime
		{
			get
			{
				return this._utcExecuteTime;
			}
			set
			{
				if (DateTime.Compare(DateTime.MinValue, value) != 0)
				{
					this._utcExecuteTime = value;
				}
				else
				{
					this._utcExecuteTime = default(DateTime);
				}
			}
		}
		public DateTime LastRun
		{
			get
			{
				return this._lastRun;
			}
			set
			{
				this._lastRun = value;
			}
		}
		public WlbTriggerState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}
		public DateTime EnableDate
		{
			get
			{
				return this._utcEnableDate;
			}
			set
			{
				if (DateTime.Compare(DateTime.MinValue, value) != 0)
				{
					this._utcEnableDate = value;
				}
			}
		}
		public DateTime DisableDate
		{
			get
			{
				return this._utcDisableDate;
			}
			set
			{
				if (DateTime.Compare(DateTime.MinValue, value) != 0)
				{
					this._utcDisableDate = value;
				}
			}
		}
	}
}
