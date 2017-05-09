using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class SetHostConfigRequest : GetHostConfigRequest
	{
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public Dictionary<string, string> Config;
	}
}
