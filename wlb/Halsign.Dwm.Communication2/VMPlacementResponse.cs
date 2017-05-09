using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[DataContract]
	public class VMPlacementResponse : IHasResponseStatus
	{
		[DataMember]
		public List<Host> RecommendedHosts
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
		public VMPlacementResponse()
		{
			this.RecommendedHosts = new List<Host>();
		}
	}
}
