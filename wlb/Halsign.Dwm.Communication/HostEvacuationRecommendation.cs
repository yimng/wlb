using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class HostEvacuationRecommendation
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int RecommendationId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int VmId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string VmName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string VmUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int HostId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string HostUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string HostName;
	}
}
