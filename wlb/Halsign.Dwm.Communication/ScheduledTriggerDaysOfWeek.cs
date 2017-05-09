using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[Flags, DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public enum ScheduledTriggerDaysOfWeek
	{
        [EnumMember]
        All = 0x7f,
        [EnumMember]
        Friday = 0x20,
        [EnumMember]
        Monday = 2,
        None = 0,
        [EnumMember]
        Saturday = 0x40,
        [EnumMember]
        Sunday = 1,
        [EnumMember]
        Thursday = 0x10,
        [EnumMember]
        Tuesday = 4,
        [EnumMember]
        Wednesday = 8,
        [EnumMember]
        Weekdays = 0x3e,
        [EnumMember]
        Weekends = 0x41
	}
}
