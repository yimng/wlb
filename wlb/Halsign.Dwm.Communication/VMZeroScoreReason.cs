using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public enum VMZeroScoreReason
	{
        [EnumMember]
        None = 0,
        [EnumMember]
        Cpu = 0x3e8,
        [EnumMember]
        Memory = 0x3e9,
        [EnumMember]
        Disk = 0x3ea,
        [EnumMember]
        Network = 0x3eb,
        [EnumMember]
        DiskRead = 0x3ec,
        [EnumMember]
        DiskWrite = 0x3ed,
        NetworkRead = 0x3ee,
        [EnumMember]
        NetworkWrite = 0x3ef,
        [EnumMember]
        LoadAverage = 0x3f0,
        [EnumMember]
        CpuOvercommit = 0x3f1,
        [EnumMember]
        NotEnoughCpus = 0x3f2,
        [EnumMember]
        HostExcluded = 0x3f3,
        [EnumMember]
		RunstateFullContention,
        [EnumMember]
		RunstateConcurrencyHazard,
        [EnumMember]
		RunstatePartialContention,
        [EnumMember]
        Unknown = 0x7d0,
        [EnumMember]
        HandleInvalid = 0x7d1,
        [EnumMember]
        NoHAPlan = 0x7d2,
        [EnumMember]
        HAOperationWouldBreakPlan = 0x7d3,
        [EnumMember]
        HostOffline = 0x7d4,
        [EnumMember]
        HostStillBooting = 0x7d5,
        [EnumMember]
        InternalError = 0x7d6,
        [EnumMember]
        NoHostAvailable = 0x7d7,
        [EnumMember]
        AuthenticationFailure = 0x7d8,
        [EnumMember]
        InvalidSession = 0x7d9,
        [EnumMember]
        SrHasNoPbds = 0x7da,
        [EnumMember]
        VmBadPowerState = 0x7db,
        [EnumMember]
        VmRequiresSr = 0x7dc,
        [EnumMember]
        VmRequiresNetwork = 0x7dd,
        [EnumMember]
        VmMissingDrivers = 0x7de,
        [EnumMember]
        HostNotEnoughFreeMemory = 0x7df,
        [EnumMember]
        BackendFailure72 = 0x7e0,
        [EnumMember]
        BackendFailure140 = 0x7e1,
        [EnumMember]
        BackendFailure222 = 0x7e2,
        [EnumMember]
        BackendFailure225 = 0x7e3,
        [EnumMember]
        SocketException = 0x7e4,
		[EnumMember]
        WebException = 0x7e5,
		[EnumMember]
        GeneralException = 0x7e6,
		HostDoesNotExist,
        [EnumMember]
        NoMetrics = 0xbb8,
        [EnumMember]
        NotLicensed = 0xbb9
	}
}
