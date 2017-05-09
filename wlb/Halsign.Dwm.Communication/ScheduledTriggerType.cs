using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public enum ScheduledTriggerType
	{
        [EnumMember]
        Daily = 1,
        [EnumMember]
        Once = 0,
        [EnumMember]
        Weekly = 2
	}
}
