using System;
namespace Halsign.DWM.Domain
{
	public class DwmAverageMetricVM : DwmAverageMetric
	{
		private long _averageTotalMemory;
		private long _averageFreeMemory;
		private long _averageTargetMemory;
		private long _averageUsedMemory;
		private double _totalVbdNetReadsPerSecond;
		private double _totalVbdNetWritesPerSecond;
		private double _averageRunstateFullContention;
		private double _averageRunstateConcurrencyHazard;
		private double _averageRunstatePartialContention;
		private double _averageRunstateFullRun;
		private double _averageRunstatePartialRun;
		private double _averageRunstateBlocked;
		public long AverageTotalMemory
		{
			get
			{
				return this._averageTotalMemory;
			}
			set
			{
				this._averageTotalMemory = value;
			}
		}
		public long AverageFreeMemory
		{
			get
			{
				return this._averageFreeMemory;
			}
			set
			{
				this._averageFreeMemory = value;
			}
		}
		public long AverageTargetMemory
		{
			get
			{
				return this._averageTargetMemory;
			}
			set
			{
				this._averageTargetMemory = value;
			}
		}
		public long AverageUsedMemory
		{
			get
			{
				return this._averageUsedMemory;
			}
			set
			{
				this._averageUsedMemory = value;
			}
		}
		public double TotalVbdNetReadsPerSecond
		{
			get
			{
				return this._totalVbdNetReadsPerSecond;
			}
			set
			{
				this._totalVbdNetReadsPerSecond = value;
			}
		}
		public double TotalVbdNetWritesPerSecond
		{
			get
			{
				return this._totalVbdNetWritesPerSecond;
			}
			set
			{
				this._totalVbdNetWritesPerSecond = value;
			}
		}
		public double AverageRunstateFullContention
		{
			get
			{
				return this._averageRunstateFullContention;
			}
			set
			{
				this._averageRunstateFullContention = value;
			}
		}
		public double AverageRunstateConcurrencyHazard
		{
			get
			{
				return this._averageRunstateConcurrencyHazard;
			}
			set
			{
				this._averageRunstateConcurrencyHazard = value;
			}
		}
		public double AverageRunstatePartialContention
		{
			get
			{
				return this._averageRunstatePartialContention;
			}
			set
			{
				this._averageRunstatePartialContention = value;
			}
		}
		public double AverageRunstateFullRun
		{
			get
			{
				return this._averageRunstateFullRun;
			}
			set
			{
				this._averageRunstateFullRun = value;
			}
		}
		public double AverageRunstatePartialRun
		{
			get
			{
				return this._averageRunstatePartialRun;
			}
			set
			{
				this._averageRunstatePartialRun = value;
			}
		}
		public double AverageRunstateBlocked
		{
			get
			{
				return this._averageRunstateBlocked;
			}
			set
			{
				this._averageRunstateBlocked = value;
			}
		}
	}
}
