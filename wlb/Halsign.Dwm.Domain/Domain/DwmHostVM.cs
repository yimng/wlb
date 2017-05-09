using System;
namespace Halsign.DWM.Domain
{
	public struct DwmHostVM
	{
		private int _hostId;
		private string _hostUuid;
		private string _hostName;
		private int _vmId;
		private string _vmUuid;
		private string _vmName;
		private DwmHost _host;
		private DwmVirtualMachine _vm;
		private int _recommendationId;
		public int HostId
		{
			get
			{
				return this._hostId;
			}
			set
			{
				this._hostId = value;
			}
		}
		public string HostUuid
		{
			get
			{
				return this._hostUuid;
			}
			set
			{
				this._hostUuid = value;
			}
		}
		public string HostName
		{
			get
			{
				return this._hostName;
			}
			set
			{
				this._hostName = value;
			}
		}
		public int VmId
		{
			get
			{
				return this._vmId;
			}
			set
			{
				this._vmId = value;
			}
		}
		public string VmUuid
		{
			get
			{
				return this._vmUuid;
			}
			set
			{
				this._vmUuid = value;
			}
		}
		public string VmName
		{
			get
			{
				return this._vmName;
			}
			set
			{
				this._vmName = value;
			}
		}
		public DwmHost Host
		{
			get
			{
				return this._host;
			}
			set
			{
				this._host = value;
			}
		}
		public DwmVirtualMachine VM
		{
			get
			{
				return this._vm;
			}
			set
			{
				this._vm = value;
			}
		}
		public int RecommendationId
		{
			get
			{
				return this._recommendationId;
			}
			set
			{
				this._recommendationId = value;
			}
		}
	}
}
