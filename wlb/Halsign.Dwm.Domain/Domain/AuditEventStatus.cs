using System;
namespace Halsign.DWM.Domain
{
	public enum AuditEventStatus
	{
		None,
		Recommended,
		WouldHave,
		NotStarted,
		InProgress,
		Complete,
		Failed
	}
}
