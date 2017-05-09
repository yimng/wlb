// Decompiled with JetBrains decompiler
// Type: Citrix.DWM.Collectors.MetricItemIndex
// Assembly: Citrix.Dwm.Collectors, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0844E477-F94E-4593-A883-69DEC5AD079C
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\Citrix.Dwm.Collectors.dll

namespace Citrix.DWM.Collectors
{
  /// <summary>
  /// Structure to describe where the row number of a devices's metric data
  ///             within the XML of Xen 5 metrics.
  /// 
  /// </summary>
  internal struct MetricItemIndex
  {
    private int _deviceNumber;
    private int _rowIndex;
    private bool _isNetworkedDevice;

    /// <summary>
    /// The device number - cpu0, cpu1, vbd_hda, vbd_hdb, etc.
    /// 
    /// </summary>
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

    /// <summary>
    /// The row number of the device's metrics within the XML.
    /// 
    /// </summary>
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

    /// <summary>
    /// Flag to indicate if the device is routed through the network.
    ///             This is used with VBDs to indicate if the IO is placing a network
    ///             load on the host.
    /// 
    /// </summary>
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

    /// <summary>
    /// Initialize a new instane of the MetricItemIndex structure.
    /// 
    /// </summary>
    /// <param name="deviceNumber">Value for the DeviceNumber field.</param><param name="rowIndex">Value for the RowIndex field.</param>
    internal MetricItemIndex(int deviceNumber, int rowIndex)
    {
      this._deviceNumber = deviceNumber;
      this._rowIndex = rowIndex;
      this._isNetworkedDevice = false;
    }
  }
}
