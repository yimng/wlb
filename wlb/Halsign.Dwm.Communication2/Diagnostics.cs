using ServiceStack.ServiceHost;
using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[RestService("/diagnostics", "GET"), DataContract]
	public class Diagnostics
	{
	}
}
