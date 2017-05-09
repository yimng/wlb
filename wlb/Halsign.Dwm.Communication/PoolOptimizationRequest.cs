using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class PoolOptimizationRequest : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int PoolID;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PoolUuid;
	}
}
