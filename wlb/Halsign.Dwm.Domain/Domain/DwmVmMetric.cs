using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	public class DwmVmMetric : DwmHostMetric
	{
		private int _vmID;
		private string _vmUuid;
		private long _targetMem;
		private double _avgVbdIoRead;
		private double _avgVbdIoWrite;
		private double _totalVbdNetRead;
		private double _totalVbdNetWrite;
		private double _runstateBlocked;
		private double _runstatePartialRun;
		private double _runstateFullRun;
		private double _runstatePartialContention;
		private double _runstateConcurrencyHazard;
		private double _runstateFullContention;
		private List<DeviceMetric> _listVbdIoRead;
		private List<DeviceMetric> _listVbdIoWrite;
		public int VMId
		{
			get
			{
				return this._vmID;
			}
			set
			{
				this._vmID = value;
			}
		}
		public string VMUuid
		{
			get
			{
				return this._vmUuid;
			}
			set
			{
				this._vmUuid = value;
			}
		}
		public long TargetMem
		{
			get
			{
				return this._targetMem;
			}
			set
			{
				this._targetMem = ((value <= 0L) ? 0L : value);
			}
		}
		public List<DeviceMetric> VifIoRead
		{
			get
			{
				return base.PifIoRead;
			}
		}
		public List<DeviceMetric> VifIoWrite
		{
			get
			{
				return base.PifIoWrite;
			}
		}
		public List<DeviceMetric> VbdIoRead
		{
			get
			{
				return DwmBase.SafeGetItem<List<DeviceMetric>>(ref this._listVbdIoRead);
			}
		}
		public List<DeviceMetric> VbdIoWrite
		{
			get
			{
				return DwmBase.SafeGetItem<List<DeviceMetric>>(ref this._listVbdIoWrite);
			}
		}
		public double AvgVbdIoReadPerSecond
		{
			get
			{
				return this._avgVbdIoRead;
			}
			set
			{
				this._avgVbdIoRead = value;
			}
		}
		public double AvgVbdIoWritePerSecond
		{
			get
			{
				return this._avgVbdIoWrite;
			}
			set
			{
				this._avgVbdIoWrite = value;
			}
		}
		public double TotalVbdNetworkReadPerSecond
		{
			get
			{
				return this._totalVbdNetRead;
			}
			set
			{
				this._totalVbdNetRead = value;
			}
		}
		public double TotalVbdNetworkWritePerSecond
		{
			get
			{
				return this._totalVbdNetWrite;
			}
			set
			{
				this._totalVbdNetWrite = value;
			}
		}
		public double RunstateBlocked
		{
			get
			{
				return this._runstateBlocked;
			}
			set
			{
				this._runstateBlocked = DwmVmMetric.Validate(value);
			}
		}
		public double RunstatePartialRun
		{
			get
			{
				return this._runstatePartialRun;
			}
			set
			{
				this._runstatePartialRun = DwmVmMetric.Validate(value);
			}
		}
		public double RunstateFullRun
		{
			get
			{
				return this._runstateFullRun;
			}
			set
			{
				this._runstateFullRun = DwmVmMetric.Validate(value);
			}
		}
		public double RunstatePartialContention
		{
			get
			{
				return this._runstatePartialContention;
			}
			set
			{
				this._runstatePartialContention = DwmVmMetric.Validate(value);
			}
		}
		public double RunstateConcurrencyHazard
		{
			get
			{
				return this._runstateConcurrencyHazard;
			}
			set
			{
				this._runstateConcurrencyHazard = DwmVmMetric.Validate(value);
			}
		}
		public double RunstateFullContention
		{
			get
			{
				return this._runstateFullContention;
			}
			set
			{
				this._runstateFullContention = DwmVmMetric.Validate(value);
			}
		}
		public DwmVmMetric(string hostUuid, string poolUuid, string vmUuid) : base(hostUuid, poolUuid)
		{
			this.VMId = DwmVirtualMachine.UuidToId(vmUuid, DwmPoolBase.UuidToId(poolUuid));
			this.VMUuid = vmUuid;
		}
		private static double Validate(double value)
		{
			if (value >= 0.0 && value <= 1.0)
			{
				return value;
			}
			return 0.0;
		}
	}
}
