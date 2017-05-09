using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public enum PoolOptimizationReason
	{
        [EnumMember]
        Consolidate = 5,
        [EnumMember]
        Cpu = 1,
        [EnumMember]
        Disk = 3,
        [EnumMember]
        DiskRead = 6,
        [EnumMember]
        DiskWrite = 7,
        [EnumMember]
        LoadAverage = 10,
        [EnumMember]
        Memory = 2,
        [EnumMember]
        Network = 4,
        [EnumMember]
        NetworkRead = 8,
        [EnumMember]
        NetworkWrite = 9,
        [EnumMember]
        None = 0,
        [EnumMember]
        PowerOff = 12,
        [EnumMember]
        PowerOn = 11,
        [EnumMember]
        Runstate = 13
	}
}
