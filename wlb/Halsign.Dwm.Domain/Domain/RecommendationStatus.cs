using System;
namespace Halsign.DWM.Domain
{
	public enum RecommendationStatus
	{
		None,
		AutomaticallyApplied,
		AutomaticalApplicationFailed,
		WouldApplyIfAutobalanceEnabled,
		WouldApplyIfPowerManagementEnabled,
		WouldApplyIfSeverityConfigured
	}
}
