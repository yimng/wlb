using ServiceStack.ServiceHost;
using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[RestService("/pool", "GET,POST,DELETE"), DataContract]
	public class Pool
	{
		[Required, DataMember]
		public string Url
		{
			get;
			set;
		}
		[Required, DataMember]
		public string UserName
		{
			get;
			set;
		}
		[Required, DataMember]
		public string Password
		{
			get;
			set;
		}
		[DataMember]
		public string Uuid
		{
			get;
			set;
		}
	}
}
