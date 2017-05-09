using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class WorkloadBalanceBase : IExtensibleDataObject
	{
		private ExtensionDataObject theData;
		public virtual ExtensionDataObject ExtensionData
		{
			get
			{
				return this.theData;
			}
			set
			{
				this.theData = value;
			}
		}
	}
}
