using System;
namespace Halsign.DWM.Domain
{
	public class DwmAverageMetricHost : DwmAverageMetric
	{
		private long _averageFreeMemory;
		private double _averageLoadAverage;
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
		public double AverageLoadAverage
		{
			get
			{
				return this._averageLoadAverage;
			}
			set
			{
				this._averageLoadAverage = value;
			}
		}
	}
}
