using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schema.halsign.com/DWM")]
	public class ScheduledTaskAction : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int Id;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int Type;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Name;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Description;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public Dictionary<string, string> Parameters;
	}
}
