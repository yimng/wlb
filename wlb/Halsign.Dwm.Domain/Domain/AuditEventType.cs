using System;
namespace Halsign.DWM.Domain
{
	public enum AuditEventType
	{
		None,
		VmPlacementRecommendation,
		VmStart,
		VmMigrate,
		HostPlacementRecommendation,
		PoolOptimizationRecommendation,
		HostPowerOff,
		HostPowerOn
	}
}
