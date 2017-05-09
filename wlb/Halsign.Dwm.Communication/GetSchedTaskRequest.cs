using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class GetSchedTaskRequest : GetConfigurationRequest
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string TaskId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string TaskActionType;
	}
}
