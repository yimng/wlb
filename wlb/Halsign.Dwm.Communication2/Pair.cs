using System;
using System.Runtime.Serialization;
namespace Halsign.DWM.Communication2
{
	[DataContract]
	[Serializable]
	public class Pair
	{
		[DataMember]
		public string Name
		{
			get;
			set;
		}
		[DataMember]
		public string Value
		{
			get;
			set;
		}
		public Pair(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
