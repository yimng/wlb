using System;
using System.Collections.Generic;
namespace Halsign.DWM.Collectors
{
	internal class XenMetricsIndex
	{
		private string _hostUuid;
		private string _vmUuid;
		internal int LoadAverageRowIndex = -1;
		internal int TotalMemRowIndex = -1;
		internal int FreeMemRowIndex = -1;
		internal int TargetMemRowIndex = -1;
		internal int RunstateBlocked = -1;
		internal int RunstatePartialRun = -1;
		internal int RunstateFullRun = -1;
		internal int RunstatePartialContention = -1;
		internal int RunstateConcurrencyHazard = -1;
		internal int RunstateFullContention = -1;
		private List<MetricItemIndex> _cpuUtilizationRowIndex;
		private List<MetricItemIndex> _pifIoReadRowIndex;
		private List<MetricItemIndex> _pifIoWriteRowIndex;
		private List<MetricItemIndex> _vbdIoReadRowIndex;
		private List<MetricItemIndex> _vbdIoWriteRowIndex;
		internal string HostUuid
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
		internal string VmUuid
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
		internal List<MetricItemIndex> CpuUtilizationRowIndex
		{
			get
			{
				return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._cpuUtilizationRowIndex);
			}
		}
		internal List<MetricItemIndex> PifIoReadRowIndex
		{
			get
			{
				return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._pifIoReadRowIndex);
			}
		}
		internal List<MetricItemIndex> PifIoWriteRowIndex
		{
			get
			{
				return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._pifIoWriteRowIndex);
			}
		}
		internal List<MetricItemIndex> VbdIoReadRowIndex
		{
			get
			{
				return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._vbdIoReadRowIndex);
			}
		}
		internal List<MetricItemIndex> VbdIoWriteRowIndex
		{
			get
			{
				return XenMetricsIndex.SafeGetItem<List<MetricItemIndex>>(ref this._vbdIoWriteRowIndex);
			}
		}
		internal XenMetricsIndex(string hostUuid)
		{
			this._hostUuid = hostUuid;
		}
		internal XenMetricsIndex(string hostUuid, string vmUuid)
		{
			this._hostUuid = hostUuid;
			this._vmUuid = vmUuid;
		}
		internal static T SafeGetItem<T>(ref T item) where T : new()
		{
			if (item == null)
			{
				item = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
			}
			return item;
		}
	}
}
