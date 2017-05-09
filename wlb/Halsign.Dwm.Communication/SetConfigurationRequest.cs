using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class SetConfigurationRequest : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int PoolId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PoolUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PoolName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public Dictionary<string, string> OptimizationParms;
	}
}
