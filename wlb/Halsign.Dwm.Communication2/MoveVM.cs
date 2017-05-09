using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[DataContract]
	public class MoveVM
	{
		[DataMember]
		public string VMUuid
		{
			get;
			set;
		}
		[DataMember]
		public string FromHostUuid
		{
			get;
			set;
		}
		[DataMember]
		public string ToHostUuid
		{
			get;
			set;
		}
		[DataMember]
		public string Reason
		{
			get;
			set;
		}
	}
}
