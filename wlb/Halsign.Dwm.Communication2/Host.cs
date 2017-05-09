using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[DataContract]
	public class Host
	{
		[DataMember]
		public string HostUuid
		{
			get;
			set;
		}
		[DataMember]
		public string HostName
		{
			get;
			set;
		}
		[DataMember]
		public double Score
		{
			get;
			set;
		}
		[DataMember]
		public double Stars
		{
			get;
			set;
		}
		[DataMember]
		public string ZeroScoreReason
		{
			get;
			set;
		}
	}
}
