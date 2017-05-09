using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication
{
	[DataContract(Namespace = "http://schemas.halsign.com/ARDS")]
	public class SetHostConfigResponse : WorkloadBalanceResult
	{
	}
}
