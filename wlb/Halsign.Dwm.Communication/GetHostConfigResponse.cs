using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class GetHostConfigResponse : WorkloadBalanceResult
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public Dictionary<string, string> Config;
	}
}
