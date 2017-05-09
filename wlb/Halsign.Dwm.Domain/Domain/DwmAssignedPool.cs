using System;
namespace Halsign.DWM.Domain
{
	public class DwmAssignedPool : DwmPoolBase
	{
		private string _state = string.Empty;
		public override string TouchedBy
		{
			get
			{
				return base.TouchedBy;
			}
		}
		public override DateTime TimeStamp
		{
			get
			{
				return base.TimeStamp;
			}
		}
		public string State
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
		public DwmAssignedPool(string uuid, string name, DwmHypervisorType hypervisorType) : base(uuid, name, hypervisorType)
		{
		}
	}
}
