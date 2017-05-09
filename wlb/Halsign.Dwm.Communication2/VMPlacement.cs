using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[RestService("/recommendation/vmplacement", "GET"), DataContract]
	public class VMPlacement
	{
		[Required, DataMember]
		public long? MinimumDynamicMemory
		{
			get;
			set;
		}
		[Required, DataMember]
		public long? MaximumDynamicMemory
		{
			get;
			set;
		}
		[Required, DataMember]
		public long? MinimumStaticMemory
		{
			get;
			set;
		}
		[Required, DataMember]
		public long? MaximumStaticMemory
		{
			get;
			set;
		}
		[Required, DataMember]
		public long? TargetMemory
		{
			get;
			set;
		}
		[Required, DataMember]
		public int? MinimumCpus
		{
			get;
			set;
		}
		[Required, DataMember]
		public long? MemoryOverhead
		{
			get;
			set;
		}
		[Required, DataMember]
		public long? RequiredStorage
		{
			get;
			set;
		}
		[Required, DataMember]
		public bool? SharedStorage
		{
			get;
			set;
		}
		[DataMember]
		public List<string> HostList
		{
			get;
			set;
		}
		[DataMember]
		public List<string> PoolList
		{
			get;
			set;
		}
	}
}
