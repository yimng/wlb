using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class GetSchedTaskActionTypeResponse : WorkloadBalanceResult
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public List<ScheduledTaskActionType> ActionTypes;
	}
}
