using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[DataContract]
	public class PoolResponse : IHasResponseStatus
	{
		[DataMember]
		public List<PoolInfo> Pools
		{
			get;
			set;
		}
		[DataMember]
		public ResponseStatus ResponseStatus
		{
			get;
			set;
		}
		public PoolResponse()
		{
			this.Pools = new List<PoolInfo>();
		}
	}
}
