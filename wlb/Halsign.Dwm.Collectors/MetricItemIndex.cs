using System;
namespace Halsign.DWM.Collectors
{
	internal struct MetricItemIndex
	{
		private int _deviceNumber;
		private int _rowIndex;
		private bool _isNetworkedDevice;
		internal int DeviceNumber
		{
			get
			{
				return this._deviceNumber;
			}
			set
			{
				this._deviceNumber = value;
			}
		}
		internal int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
			set
			{
				this._rowIndex = value;
			}
		}
		internal bool IsNetworkedDevice
		{
			get
			{
				return this._isNetworkedDevice;
			}
			set
			{
				this._isNetworkedDevice = value;
			}
		}
		internal MetricItemIndex(int deviceNumber, int rowIndex)
		{
			this._deviceNumber = deviceNumber;
			this._rowIndex = rowIndex;
			this._isNetworkedDevice = false;
		}
	}
}
