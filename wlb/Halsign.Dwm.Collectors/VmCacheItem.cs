using Halsign.DWM.Domain;
using System;
using XenAPI;
namespace Halsign.DWM.Collectors
{
	internal class VmCacheItem
	{
		internal VM xenVM;
		internal DwmVirtualMachine dwmVM;
		internal string serverOpaqueRef;
		internal int pendingRecommendationId;
	}
}
