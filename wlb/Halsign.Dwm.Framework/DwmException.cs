using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Framework
{
	[Serializable]
	public class DwmException : Exception
	{
		public int Number
		{
			get
			{
				return base.HResult;
			}
			set
			{
				base.HResult = value;
			}
		}
		public DwmException(string message, DwmErrorCode errorCode, Exception innerException) : base(message, innerException)
		{
			this.Number = (int)errorCode;
		}
		public DwmException()
		{
			this.Number = 4000;
		}
		public DwmException(string message) : base(message)
		{
			this.Number = 4000;
		}
		public DwmException(string message, Exception innerException) : base(message, innerException)
		{
			this.Number = 4000;
		}
		protected DwmException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
