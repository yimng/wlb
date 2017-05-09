using System;
namespace Halsign.DWM.Domain
{
	[Flags]
	public enum TriggerDaysOfWeek
	{
		None = 0,
		Sunday = 1,
		Monday = 2,
		Tuesday = 4,
		Wednesday = 8,
		Thursday = 16,
		Friday = 32,
		Saturday = 64,
		Weekends = 65,
		Weekdays = 62,
		All = 127
	}
}
