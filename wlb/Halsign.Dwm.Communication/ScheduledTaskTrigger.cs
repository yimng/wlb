using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schema.halsign.com/DWM")]
	public class ScheduledTaskTrigger : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int Id;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ScheduledTriggerType TriggerType;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ScheduledTaskAction Action;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ScheduledTriggerDaysOfWeek DaysOfWeek;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public DateTime ExecuteTimeOfDay;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public DateTime LastRun;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public DateTime EnableDate;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public DateTime DisableDate;
	}
}
