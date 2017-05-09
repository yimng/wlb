using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class ReportRequest : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int ReportId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string ReportName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public List<ReportParameter> ReportParms;
	}
}
