using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	public class DwmHostMetric
	{
		private string _hostUuid;
		private int _hostId;
		private long _totalMem;
		private long _freeMem;
		private DateTime _timeStamp;
		private double _avgCpuUtilization;
		private double _avgPifIoRead;
		private double _avgPifIoWrite;
		private double _loadAverage;
		private List<DeviceMetric> _listCpuUtilization;
		private List<DeviceMetric> _listPifIoRead;
		private List<DeviceMetric> _listPifIoWrite;
		public int HostId
		{
			get
			{
				return this._hostId;
			}
			set
			{
				this._hostId = value;
			}
		}
		public string HostUuid
		{
			get
			{
				return this._hostUuid;
			}
			set
			{
				this._hostUuid = value;
			}
		}
		public double LoadAverage
		{
			get
			{
				return this._loadAverage;
			}
			set
			{
				this._loadAverage = value;
			}
		}
		public long TotalMem
		{
			get
			{
				return this._totalMem;
			}
			set
			{
				this._totalMem = ((value <= 0L) ? 0L : value);
			}
		}
		public long FreeMem
		{
			get
			{
				return this._freeMem;
			}
			set
			{
				this._freeMem = ((value <= 0L) ? 0L : value);
			}
		}
		public double AvgCpuUtilization
		{
			get
			{
				return this._avgCpuUtilization;
			}
			set
			{
				this._avgCpuUtilization = value;
			}
		}
		public double AvgPifIoReadPerSecond
		{
			get
			{
				return this._avgPifIoRead;
			}
			set
			{
				this._avgPifIoRead = value;
			}
		}
		public double AvgPifIoWritePerSecond
		{
			get
			{
				return this._avgPifIoWrite;
			}
			set
			{
				this._avgPifIoWrite = value;
			}
		}
		public DateTime TStamp
		{
			get
			{
				return this._timeStamp;
			}
			set
			{
				this._timeStamp = value;
			}
		}
		public List<DeviceMetric> CpuUtilization
		{
			get
			{
				return DwmBase.SafeGetItem<List<DeviceMetric>>(ref this._listCpuUtilization);
			}
		}
		public List<DeviceMetric> PifIoRead
		{
			get
			{
				return DwmBase.SafeGetItem<List<DeviceMetric>>(ref this._listPifIoRead);
			}
		}
		public List<DeviceMetric> PifIoWrite
		{
			get
			{
				return DwmBase.SafeGetItem<List<DeviceMetric>>(ref this._listPifIoWrite);
			}
		}
		protected DwmHostMetric()
		{
			DwmBase.TimeBomb();
		}
		public DwmHostMetric(string hostUuid, string poolUuid)
		{
			DwmBase.TimeBomb();
			int poolId = DwmBase.PoolUuidToId(poolUuid);
			this._hostUuid = hostUuid;
			this._hostId = DwmHost.UuidToId(this._hostUuid, poolId);
		}
	}
}
