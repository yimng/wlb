using System;
namespace Halsign.DWM.Domain
{
	public class DwmVmMovement
	{
		private int _vmId;
		private int _moveToHostId;
		private int _moveFromHostId;
		private string _vmName;
		private string _moveToHostName;
		private string _moveFromHostName;
		private DateTime _timeOfMove;
		public int VmId
		{
			get
			{
				return this._vmId;
			}
			internal set
			{
				this._vmId = value;
			}
		}
		public int MoveToHostId
		{
			get
			{
				return this._moveToHostId;
			}
			internal set
			{
				this._moveToHostId = value;
			}
		}
		public int MoveFromHostId
		{
			get
			{
				return this._moveFromHostId;
			}
			internal set
			{
				this._moveFromHostId = value;
			}
		}
		public string VmName
		{
			get
			{
				return this._vmName;
			}
			internal set
			{
				this._vmName = value;
			}
		}
		public string MoveToHostName
		{
			get
			{
				return this._moveToHostName;
			}
			internal set
			{
				this._moveToHostName = value;
			}
		}
		public string MoveFromHostName
		{
			get
			{
				return this._moveFromHostName;
			}
			internal set
			{
				this._moveFromHostName = value;
			}
		}
		public DateTime TimeOfMove
		{
			get
			{
				return this._timeOfMove;
			}
			internal set
			{
				this._timeOfMove = value;
			}
		}
	}
}
