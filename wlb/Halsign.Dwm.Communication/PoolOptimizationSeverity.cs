using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public enum PoolOptimizationSeverity
	{
        [EnumMember]
        Critical = 4,
        [EnumMember]
        High = 3,
        [EnumMember]
        Low = 1,
        [EnumMember]
        Medium = 2,
        [EnumMember]
        None = 0    
	}
}
