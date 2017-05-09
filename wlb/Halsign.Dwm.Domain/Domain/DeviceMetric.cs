using System;
namespace Halsign.DWM.Domain
{
	public struct DeviceMetric
	{
		public int DeviceNumber;
		public double MetricValue;
		public DeviceMetric(int deviceNumber, double metricValue)
		{
			this.DeviceNumber = deviceNumber;
			this.MetricValue = metricValue;
		}
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj != null && obj is DeviceMetric)
			{
				DeviceMetric deviceMetric = (DeviceMetric)obj;
				result = (this.DeviceNumber == deviceMetric.DeviceNumber && this.MetricValue == deviceMetric.MetricValue);
			}
			return result;
		}
		public override int GetHashCode()
		{
			return this.MetricValue.GetHashCode() ^ this.DeviceNumber;
		}
		public static bool operator ==(DeviceMetric x, DeviceMetric y)
		{
			return x.MetricValue == y.MetricValue && x.DeviceNumber == y.DeviceNumber;
		}
		public static bool operator !=(DeviceMetric x, DeviceMetric y)
		{
			return !(x == y);
		}
	}
}
