using System;
namespace Halsign.DWM.Framework
{
	public enum DwmErrorCode
	{
		None,
		AccessDenied = 5,
		RBACPermissionDenied,
		Unknown = 4000,
		CannotInitWmi,
		XenCannotLogIn,
		XenCannotConnect,
		NullReference,
		RegistryKeyMissing,
		NotImplemented,
		InvalidParameter,
		HyperVWmi,
		NotConfigured,
		InvalidOperation,
		ArgumentOutOfRange,
		NotInitialized,
		CannotGetCollector,
		GenericException,
		UnknownPool,
		NoMetrics,
		NoStorageRepositories,
		NoFreeCpus,
		NoFreeMemory,
		NotLicensed,
		AuthenicationFailure,
		HostExcluded,
		HostNotFound
	}
}
