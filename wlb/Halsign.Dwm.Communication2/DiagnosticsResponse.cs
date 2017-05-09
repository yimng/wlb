using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[DataContract]
	public class DiagnosticsResponse : IHasResponseStatus
	{
		[DataMember]
		public string WlbDiagnostics
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
	}
}
