using Halsign.DWM.Domain;
using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[DataContract]
	public class PoolInfo
	{
		[DataMember]
		public string Url
		{
			get;
			set;
		}
		[DataMember]
		public string UserName
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
		[DataMember]
		public string Name
		{
			get;
			set;
		}
		[DataMember]
		public DiscoveryStatus? DiscoveryStatus
		{
			get;
			set;
		}
	}
}
