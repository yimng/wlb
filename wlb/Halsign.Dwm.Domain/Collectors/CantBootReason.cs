using System;
namespace Halsign.DWM.Collectors
{
	public enum CantBootReason
	{
		None,
		Unknown = 2000,
		HandleInvalid,
		NoHAPlan,
		HAOperationWouldBreakPlan,
		HostOffline,
		HostStillBooting,
		InternalError,
		NoHostAvailable,
		AuthenticationFailure,
		InvalidSession,
		SrHasNoPbds,
		VmBadPowerState,
		VmRequiresSr,
		VmRequiresNetwork,
		VmMissingDrivers,
		HostNotEnoughFreeMemory,
		BackendFailure72,
		BackendFailure140,
		BackendFailure222,
		BackendFailure225,
		SocketException,
		WebException,
		GeneralException
	}
}
