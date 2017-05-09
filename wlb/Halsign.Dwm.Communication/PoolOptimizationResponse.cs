using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class PoolOptimizationResponse : WorkloadBalanceResult
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int OptimizationId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public PoolOptimizationSeverity Severity;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public List<PoolOptimizationRecommendation> Recommendations;
	}
}
