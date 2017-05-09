using System;
namespace Halsign.DWM.Domain
{
	public enum DwmStatus
	{
		None,
		OperationsPending,
		Recommended,
		NotStarted,
		InProgress,
		Complete,
		Failed
	}
}
