using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class HostRecommendationsResponse : WorkloadBalanceResult
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public List<HostEvacuationRecommendation> Recommendations;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public bool CanPlaceAllVMs;
	}
}
