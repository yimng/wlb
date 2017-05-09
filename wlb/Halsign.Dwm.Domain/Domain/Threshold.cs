using Halsign.DWM.Framework;
using System;
namespace Halsign.DWM.Domain
{
	public class Threshold
	{
		private double _high;
		private double _medium;
		private double _low;
		private double _min;
		private double _max;
		public double High
		{
			get
			{
				return this._high;
			}
			set
			{
				this._high = this.CheckValue(value);
			}
		}
		public double Medium
		{
			get
			{
				return this._medium;
			}
			set
			{
				this._medium = this.CheckValue(value);
			}
		}
		public double Low
		{
			get
			{
				return this._low;
			}
			set
			{
				this._low = this.CheckValue(value);
			}
		}
		public Threshold()
		{
			this._min = -1.7976931348623157E+308;
			this._max = 1.7976931348623157E+308;
		}
		public Threshold(double min, double max)
		{
			this._min = min;
			this._max = max;
		}
		public Threshold(double min, double max, double high, double medium, double low)
		{
			this._min = min;
			this._max = max;
			this.High = high;
			this.Medium = medium;
			this.Low = low;
		}
		public virtual void Copy(Threshold copyFrom)
		{
			if (copyFrom != null)
			{
				this.High = copyFrom.High;
				this.Medium = copyFrom.Medium;
				this.Low = copyFrom.Low;
			}
		}
		protected double CheckValue(double value)
		{
			if (value > this._max || value < this._min)
			{
				throw new DwmException(Localization.Format("Threshold must be in the range {0} to {1}", this._min, this._max), DwmErrorCode.ArgumentOutOfRange, null);
			}
			return value;
		}
	}
}
