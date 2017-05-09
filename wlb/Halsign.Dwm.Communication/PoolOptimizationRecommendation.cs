using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class PoolOptimizationRecommendation : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int PoolId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int VmToMoveId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string VmToMoveUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string VmToMoveName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int MoveToHostId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string MoveToHostUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string MoveToHostName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int MoveFromHostId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string MoveFromHostUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string MoveFromHostName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public PoolOptimizationReason Reason;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public DateTime TimeStamp;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int RecommendationId;
	}
}
