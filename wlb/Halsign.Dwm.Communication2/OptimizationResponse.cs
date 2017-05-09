using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[DataContract]
	public class OptimizationResponse
	{
		[DataMember]
		public int RecommendationId
		{
			get;
			set;
		}
		[DataMember]
		public List<MoveVM> MoveVM
		{
			get;
			set;
		}
		public OptimizationResponse()
		{
			this.MoveVM = new List<MoveVM>();
		}
	}
}
