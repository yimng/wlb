using System;
namespace Halsign.DWM.Domain
{
	public abstract class DwmAverageMetric
	{
		private DateTime _startTime;
		private DateTime _endTime;
		private double _averageCpuUtilization;
		private double _averagePifReadsPerSecond;
		private double _averagePifWritesPerSecond;
		private double _averagePbdReadsPerSecond;
		private double _averagePbdWritesPerSecond;
		public DateTime StartTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}
		public DateTime EndTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}
		public double AverageCpuUtilization
		{
			get
			{
				return this._averageCpuUtilization;
			}
			set
			{
				this._averageCpuUtilization = value;
			}
		}
		public double AveragePifReadsPerSecond
		{
			get
			{
				return this._averagePifReadsPerSecond;
			}
			set
			{
				this._averagePifReadsPerSecond = value;
			}
		}
		public double AveragePifWritesPerSecond
		{
			get
			{
				return this._averagePifWritesPerSecond;
			}
			set
			{
				this._averagePifWritesPerSecond = value;
			}
		}
		public double AveragePbdReadsPerSecond
		{
			get
			{
				return this._averagePbdReadsPerSecond;
			}
			set
			{
				this._averagePbdReadsPerSecond = value;
			}
		}
		public double AveragePbdWritesPerSecond
		{
			get
			{
				return this._averagePbdWritesPerSecond;
			}
			set
			{
				this._averagePbdWritesPerSecond = value;
			}
		}
	}
}
