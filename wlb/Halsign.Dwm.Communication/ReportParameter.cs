using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class ReportParameter : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string ParameterName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string ParameterValue;
	}
}
