using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schema.halsign.com/DWM")]
	public class ScheduledTaskActionType : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int ActionTypeId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string ActionTypeName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string ActionTypeDescription;
	}
}
