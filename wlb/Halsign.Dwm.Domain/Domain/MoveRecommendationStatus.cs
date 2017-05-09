using System;
namespace Halsign.DWM.Domain
{
	public class MoveRecommendationStatus : MoveRecommendation
	{
		private AuditEventStatus _status;
		private DateTime _statusTimeStamp;
		public AuditEventStatus Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}
		public DateTime StatusTimeStamp
		{
			get
			{
				return this._statusTimeStamp;
			}
			set
			{
				this._statusTimeStamp = value;
			}
		}
	}
}
