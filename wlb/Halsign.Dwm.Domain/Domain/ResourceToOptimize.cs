using System;
namespace Halsign.DWM.Domain
{
	public enum ResourceToOptimize
	{
		None,
		Cpu,
		Memory,
		Disk,
		Network,
		Consolidate,
		DiskRead,
		DiskWrite,
		NetworkRead,
		NetworkWrite,
		LoadAverage,
		PowerOn,
		PowerOff,
		Runstate
	}
}
