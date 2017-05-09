using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class SetSchedTaskRequest : GetConfigurationRequest
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public List<ScheduledTask> Tasks;
	}
}
