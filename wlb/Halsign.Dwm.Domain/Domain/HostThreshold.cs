using System;
namespace Halsign.DWM.Domain
{
	public class HostThreshold : Threshold
	{
		private double _critical;
		public double Critical
		{
			get
			{
				return this._critical;
			}
			set
			{
				this._critical = base.CheckValue(value);
			}
		}
		public HostThreshold()
		{
		}
		public HostThreshold(double min, double max) : base(min, max)
		{
		}
		public HostThreshold(double min, double max, double critical, double high, double medium, double low) : base(min, max, high, medium, low)
		{
			this.Critical = critical;
		}
		public void Copy(HostThreshold copyFrom)
		{
			if (copyFrom != null)
			{
				this.Critical = copyFrom.Critical;
				base.High = copyFrom.High;
				base.Medium = copyFrom.Medium;
				base.Low = copyFrom.Low;
			}
		}
	}
}
