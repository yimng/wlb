using Halsign.DWM.Domain;
using System;
using XenAPI;
namespace Halsign.DWM.Collectors
{
	internal class HostCacheItem
	{
		internal Host xenHost;
		internal DwmHost dwmHost;
		internal string serverOpaqueRef;
		internal int pendingRecommendationId;
		internal bool pendingPoweredOffByWlb;
		internal int errorCount;
	}
}
