using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	public class WlbTaskAction : WlbActionType
	{
		private int _id;
		private Dictionary<string, string> _parameters;
		public int Id
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
		public Dictionary<string, string> Parameters
		{
			get
			{
				return DwmBase.SafeGetItem<Dictionary<string, string>>(ref this._parameters);
			}
			set
			{
				this._parameters = value;
			}
		}
	}
}
