using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class VmPlacementRecommendation : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int RecommendationId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int HostId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string HostName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string HostUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int Score;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public double Stars;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public bool CanBootVM;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public VMZeroScoreReason ZeroScoreReason;
	}
}
