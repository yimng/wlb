using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	public class PoolConfigurationResponse : IHasResponseStatus
	{
		[DataMember]
		public List<Pair> Configurations
		{
			get;
			set;
		}
		public ResponseStatus ResponseStatus
		{
			get;
			set;
		}
		public PoolConfigurationResponse()
		{
			this.Configurations = new List<Pair>();
		}
		internal void Add(string name, string value)
		{
			this.Configurations.Add(new Pair(name, value));
		}
	}
}
