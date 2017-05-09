using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class XenServerRequest : WorkloadBalanceBase
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string vGateServerUrl;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Protocol;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string HostName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int Port;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string UserName;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Password;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int PoolId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PoolUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PoolName;
	}
}
