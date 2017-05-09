using ServiceStack.ServiceHost;
using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[RestService("/recommendation/optimization", "GET"), DataContract]
	public class Optimization
	{
	}
}
