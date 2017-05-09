using Halsign.DWM.Framework;
using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class WorkloadBalanceResult : WorkloadBalanceBase
	{
		private int resultCode;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string ErrorMessage;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int ResultCode
		{
			get
			{
				return this.resultCode;
			}
			set
			{
				this.resultCode = value;
				string text = null;
				try
				{
					text = Messages.ResourceManager.GetString("WLB_ERROR_" + value);
				}
				catch
				{
				}
				finally
				{
					if (this.resultCode != 0)
					{
						this.ErrorMessage = ((text != null) ? text : ((DwmErrorCode)value).ToString());
					}
				}
			}
		}
	}
}
