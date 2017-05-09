using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class GetHostConfigRequest : GetConfigurationRequest
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public int HostId;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string HostUuid;
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string HostName;
	}
}
