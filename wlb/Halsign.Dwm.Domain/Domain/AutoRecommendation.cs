using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	internal class AutoRecommendation
	{
		internal int PoolId;
		internal int PollIntervals;
		internal List<MoveRecommendation> MoveRecs;
		internal DwmHostCollection HostToTurnOff;
		internal DwmHostCollection HostToTurnOn;
	}
}
