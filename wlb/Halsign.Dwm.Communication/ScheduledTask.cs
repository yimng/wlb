using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class ScheduledTask : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int TaskId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string TaskName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string TaskDescription;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public bool TaskEnabled;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string TaskOwner;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string LastTouchedBy;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public DateTime LastTouched;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public bool LastRunResult;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ScheduledTaskTrigger Trigger;
	}
}
