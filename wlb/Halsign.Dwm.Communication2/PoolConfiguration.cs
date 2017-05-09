using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[RestService("/poolconfiguration", "GET,PUT"), DataContract]
	public class PoolConfiguration
	{
		[Required, DataMember]
		public string Uuid
		{
			get;
			set;
		}
		[DataMember]
		public List<Pair> Configurations
		{
			get;
			set;
		}
		public PoolConfiguration()
		{
			this.Configurations = new List<Pair>();
		}
	}
}
