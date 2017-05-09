using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class GetConfigurationRequest : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int PoolId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PoolUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PoolName;
	}
}
