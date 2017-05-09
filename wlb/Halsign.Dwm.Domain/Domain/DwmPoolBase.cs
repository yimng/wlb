using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public abstract class DwmPoolBase : DwmBase
	{
		private const double MaxPerformanceMaxCompressionRatioMaxValue = 0.5;
		private const double MaxDensityMaxCompressionRatioMaxValue = 0.75;
		public const double DefaultHostCpuThresholdCritical = 0.9;
		public const double DefaultHostCpuThresholdHigh = 0.76;
		public const double DefaultHostCpuThresholdMedium = 0.45;
		public const double DefaultHostCpuThresholdLow = 0.22;
		public const double DefaultHostMemoryThresholdCritical = 53477376.0;
		public const double DefaultHostMemoryThresholdHigh = 66846720.0;
		public const double DefaultHostMemoryThresholdMedium = 534773760.0;
		public const double DefaultHostMemoryThresholdLow = 1069547520.0;
		public const double DefaultVmMemoryWeightHigh = 1.0;
		public const double DefaultVmMemoryWeightMedium = 0.6;
		public const double DefaultVmMemoryWeightLow = 0.3;
		public const double DefaultVmWeightHigh = 1.0;
		public const double DefaultVmWeightMedium = 0.6;
		public const double DefaultVmWeightLow = 0.3;
		public const double DefaultThresholdCritical = 26214400.0;
		public const double DefaultThresholdHigh = 22282240.0;
		public const double DefaultThresholdMedium = 13107200.0;
		public const double DefaultThresholdLow = 3932160.0;
		public const double DefaultRunstateThresholdFullContentionCritical = 0.5;
		public const double DefaultRunstateThresholdFullContentionHigh = 0.25;
		public const double DefaultRunstateThresholdFullContentionMedium = 0.1;
		public const double DefaultRunstateThresholdFullContentionLow = 0.05;
		public const double DefaultRunstateThresholdConcurrencyHazardCritical = 0.5;
		public const double DefaultRunstateThresholdConcurrencyHazardHigh = 0.25;
		public const double DefaultRunstateThresholdConcurrencyHazardMedium = 0.1;
		public const double DefaultRunstateThresholdConcurrencyHazardLow = 0.05;
		public const double DefaultRunstateThresholdPartialContentionCritical = 0.5;
		public const double DefaultRunstateThresholdPartialContentionHigh = 0.25;
		public const double DefaultRunstateThresholdPartialContentionMedium = 0.1;
		public const double DefaultRunstateThresholdPartialContentionLow = 0.05;
		public const double DefaultRunstateThresholdFullRunCritical = 0.9;
		public const double DefaultRunstateThresholdFullRunHigh = 0.8;
		public const double DefaultRunstateThresholdFullRunMedium = 0.5;
		public const double DefaultRunstateThresholdFullRunLow = 0.25;
		public const double DefaultRunstateThresholdPartialRunCritical = 0.9;
		public const double DefaultRunstateThresholdPartialRunHigh = 0.8;
		public const double DefaultRunstateThresholdPartialRunMedium = 0.5;
		public const double DefaultRunstateThresholdPartialRunLow = 0.25;
		public const double DefaultRunstateThresholdBlockedCritical = 0.9;
		public const double DefaultRunstateThresholdBlockedHigh = 0.8;
		public const double DefaultRunstateThresholdBlockedMedium = 0.5;
		public const double DefaultRunstateThresholdBlockedLow = 0.25;
		private string _poolMaster;
		private string _poolProtocol;
		private string _poolMaster1Addr;
		private string _poolMaster2Addr;
		private int _poolMaster1Port;
		private int _poolMaster2Port;
		private string _poolDefaultSR;
		private string _touchedBy;
		private DateTime _timeStamp;
		private DwmHypervisorType _hypervisorType;
		private bool _active = true;
		private bool _enabled = true;
		private HostThreshold _hostCpuThreshold;
		private HostThreshold _hostMemoryThreshold;
		private HostThreshold _hostPifReadThreshold;
		private HostThreshold _hostPifWriteThreshold;
		private HostThreshold _hostPbdReadThreshold;
		private HostThreshold _hostPbdWriteThreshold;
		private HostThreshold _hostLoadAverageThreshold;
		private HostThreshold _hostRunstateFullContentionThreshold;
		private HostThreshold _hostRunstateConcurrencyHazardThreshold;
		private HostThreshold _hostRunstatePartialContentionThreshold;
		private HostThreshold _hostRunstateFullRunThreshold;
		private HostThreshold _hostRunstatePartialRunThreshold;
		private HostThreshold _hostRunstateBlockedThreshold;
		private double _weightCurrentMetrics = 0.7;
		private double _weightRecentMetrics = 0.25;
		private double _weightHistoricMetrics = 0.05;
		private Threshold _vmCpuUtilizationThreshold;
		private Threshold _vmCpuUtilizationWeight;
		private Threshold _vmMemoryThreshold;
		private Threshold _vmMemoryWeight;
		private Threshold _vmNetworkReadThreshold;
		private Threshold _vmNetworkReadWeight;
		private Threshold _vmNetworkWriteThreshold;
		private Threshold _vmNetworkWriteWeight;
		private Threshold _vmDiskReadThreshold;
		private Threshold _vmDiskReadWeight;
		private Threshold _vmDiskWriteThreshold;
		private Threshold _vmDiskWriteWeight;
		private Threshold _vmPowerManagementWeight;
		private Threshold _vmRunstateWeight;
		protected OptimizationMode _optMode = OptimizationMode.None;
		protected int _overCommitCpusInPerfMode = -1;
		protected int _overCommitCpusInDensityMode = -1;
		protected int _overCommitCpuRatio = 1;
		protected int _autoBalance = -1;
		protected int _managePower = -1;
		protected double _poolMasterCpuLimit = -1.0;
		protected double _poolMasterIoLimit = -1.0;
		protected bool _preferPowerOnOverCompression;
		protected bool _powerOnHostIfNoMemory;
		protected bool _powerOnHostIfNoSR;
		protected bool _compressGuestsToRelievePressure;
		protected bool _compressGuestsToPreservePower;
		protected double _maxPerformanceMaxCompressionRatio;
		protected double _maxDensityMaxCompressionRatio;
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static Dictionary<string, int> _nameCache = new Dictionary<string, int>();
		private static object _uuidCacheLock = new object();
		private static object _nameCacheLock = new object();
		protected bool _isLicensed = true;
		public static string Version
		{
			get
			{
				return Configuration.GetValueAsString(ConfigItems.WLBVersion);
			}
		}
		public string Master
		{
			get
			{
				return this._poolMaster;
			}
			set
			{
				this._poolMaster = value;
			}
		}
		public string Protocol
		{
			get
			{
				return this._poolProtocol;
			}
			set
			{
				this._poolProtocol = value;
			}
		}
		public string PrimaryPoolMasterAddr
		{
			get
			{
				return this._poolMaster1Addr;
			}
			set
			{
				this._poolMaster1Addr = value;
			}
		}
		public int PrimaryPoolMasterPort
		{
			get
			{
				return this._poolMaster1Port;
			}
			set
			{
				this._poolMaster1Port = value;
			}
		}
		public string SecondaryPoolMasterAddr
		{
			get
			{
				return this._poolMaster2Addr;
			}
			set
			{
				this._poolMaster2Addr = value;
			}
		}
		public int SecondaryPoolMasterPort
		{
			get
			{
				return this._poolMaster2Port;
			}
			set
			{
				this._poolMaster2Port = value;
			}
		}
		public DwmHypervisorType HVType
		{
			get
			{
				return this._hypervisorType;
			}
			internal set
			{
				this._hypervisorType = value;
			}
		}
		public string DefaultSR
		{
			get
			{
				return this._poolDefaultSR;
			}
			set
			{
				this._poolDefaultSR = value;
			}
		}
		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}
		public bool IsLicensed
		{
			get
			{
				return this._isLicensed;
			}
			internal set
			{
				this._isLicensed = value;
			}
		}
		public OptimizationMode OptMode
		{
			get
			{
				return (this._optMode != OptimizationMode.None) ? this._optMode : OptimizationMode.MaxPerformance;
			}
			set
			{
				this._optMode = value;
			}
		}
		public bool OverCommitCpusInPerfMode
		{
			get
			{
				return this._overCommitCpusInPerfMode == 1;
			}
			set
			{
				this._overCommitCpusInPerfMode = ((!value) ? 0 : 1);
			}
		}
		public bool OverCommitCpusInDensityMode
		{
			get
			{
				return this._overCommitCpusInDensityMode == 1;
			}
			set
			{
				this._overCommitCpusInDensityMode = ((!value) ? 0 : 1);
			}
		}
		public int OverCommitCpuRatio
		{
			get
			{
				return this._overCommitCpuRatio;
			}
			set
			{
				if (value < 1)
				{
					this._overCommitCpuRatio = 1;
				}
				else
				{
					this._overCommitCpuRatio = value;
				}
			}
		}
		internal bool AutoBalance
		{
			get
			{
				return this._autoBalance == 1;
			}
			set
			{
				this._autoBalance = ((!value) ? 0 : 1);
			}
		}
		internal bool ManagePower
		{
			get
			{
				return this._managePower == 1;
			}
			set
			{
				this._managePower = ((!value) ? 0 : 1);
			}
		}
		internal double PoolMasterCpuLimit
		{
			get
			{
				return this._poolMasterCpuLimit;
			}
			set
			{
				this._poolMasterCpuLimit = value;
			}
		}
		internal double PoolMasterNetIoLimit
		{
			get
			{
				return this._poolMasterIoLimit;
			}
			set
			{
				this._poolMasterIoLimit = value;
			}
		}
		internal bool PreferPowerOnOverCompression
		{
			get
			{
				return this._preferPowerOnOverCompression;
			}
			set
			{
				this._preferPowerOnOverCompression = value;
			}
		}
		protected internal bool PowerOnHostIfNoMemory
		{
			get
			{
				return this._powerOnHostIfNoMemory;
			}
			set
			{
				this._powerOnHostIfNoMemory = value;
			}
		}
		internal bool PowerOnHostIfNoSR
		{
			get
			{
				return this._powerOnHostIfNoSR;
			}
			set
			{
				this._powerOnHostIfNoSR = value;
			}
		}
		internal bool CompressGuestsToRelievePressure
		{
			get
			{
				return this._compressGuestsToRelievePressure;
			}
			set
			{
				this._compressGuestsToRelievePressure = value;
			}
		}
		internal bool CompressGuestsToPreservePower
		{
			get
			{
				return this._compressGuestsToPreservePower;
			}
			set
			{
				this._compressGuestsToPreservePower = value;
			}
		}
		internal double MaxPerformanceMaxCompressionRatio
		{
			get
			{
				return this._maxPerformanceMaxCompressionRatio;
			}
			set
			{
				this._maxPerformanceMaxCompressionRatio = value;
			}
		}
		internal double MaxDensityMaxCompressionRatio
		{
			get
			{
				return this._maxDensityMaxCompressionRatio;
			}
			set
			{
				this._maxDensityMaxCompressionRatio = value;
			}
		}
		public DiscoveryStatus PoolDiscoveryStatus
		{
			get;
			set;
		}
		public DateTime LastDiscoveryCompleted
		{
			get;
			set;
		}
		public string Url
		{
			get
			{
				return string.Format("{0}://{1}:{2}", this.Protocol.ToLower(), this.PrimaryPoolMasterAddr, this.PrimaryPoolMasterPort);
			}
		}
		public HostThreshold HostCpuThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostCpuThreshold, 0.0, 1.0, 0.9, 0.76, 0.45, 0.22);
			}
		}
		public HostThreshold HostMemoryThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostMemoryThreshold, 0.0, 1.7976931348623157E+308, 53477376.0, 66846720.0, 534773760.0, 1069547520.0);
			}
		}
		public HostThreshold HostPifReadThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostPifReadThreshold, 0.0, 1.7976931348623157E+308, 26214400.0, 22282240.0, 13107200.0, 3932160.0);
			}
		}
		public HostThreshold HostPifWriteThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostPifWriteThreshold, 0.0, 1.7976931348623157E+308, 26214400.0, 22282240.0, 13107200.0, 3932160.0);
			}
		}
		public HostThreshold HostPbdReadThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostPbdReadThreshold, 0.0, 1.7976931348623157E+308, 26214400.0, 22282240.0, 13107200.0, 3932160.0);
			}
		}
		public HostThreshold HostPbdWriteThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostPbdWriteThreshold, 0.0, 1.7976931348623157E+308, 26214400.0, 22282240.0, 13107200.0, 3932160.0);
			}
		}
		public HostThreshold HostLoadAverageThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostLoadAverageThreshold, 0.0, 50.0, 20.0, 10.0, 5.0, 1.0);
			}
		}
		public HostThreshold HostRunstateFullContentionThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostRunstateFullContentionThreshold, 0.0, 1.0, 0.5, 0.25, 0.1, 0.05);
			}
		}
		public HostThreshold HostRunstateConcurrencyHazardThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostRunstateConcurrencyHazardThreshold, 0.0, 1.0, 0.5, 0.25, 0.1, 0.05);
			}
		}
		public HostThreshold HostRunstatePartialContentionThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostRunstatePartialContentionThreshold, 0.0, 1.0, 0.5, 0.25, 0.1, 0.05);
			}
		}
		public HostThreshold HostRunstateFullRunThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostRunstateFullRunThreshold, 0.0, 1.0, 0.9, 0.8, 0.5, 0.25);
			}
		}
		public HostThreshold HostRunstatePartialRunThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostRunstatePartialRunThreshold, 0.0, 1.0, 0.9, 0.8, 0.5, 0.25);
			}
		}
		public HostThreshold HostRunstateBlockedThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._hostRunstateBlockedThreshold, 0.0, 1.0, 0.9, 0.8, 0.5, 0.25);
			}
		}
		public double WeightCurrentMetrics
		{
			get
			{
				return this._weightCurrentMetrics;
			}
			set
			{
				this._weightCurrentMetrics = DwmPoolBase.CheckRange(value, 0.0, 1.0);
			}
		}
		public double WeightRecentMetrics
		{
			get
			{
				return this._weightRecentMetrics;
			}
			set
			{
				this._weightRecentMetrics = DwmPoolBase.CheckRange(value, 0.0, 1.0);
			}
		}
		public double WeightHistoricMetrics
		{
			get
			{
				return this._weightHistoricMetrics;
			}
			set
			{
				this._weightHistoricMetrics = DwmPoolBase.CheckRange(value, 0.0, 1.0);
			}
		}
		public Threshold VmCpuUtilizationThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmCpuUtilizationThreshold, 0.0, 1.0, 0.8, 0.5, 0.2);
			}
		}
		public Threshold VmCpuUtilizationWeight
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmCpuUtilizationWeight, 0.0, 1.0, 1.0, 0.6, 0.3);
			}
		}
		public Threshold VmMemoryThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmMemoryThreshold, 0.0, 1.7976931348623157E+308, 2147483648.0, 1073741824.0, 536870912.0);
			}
		}
		public Threshold VmMemoryWeight
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmMemoryWeight, 0.0, 1.0, 1.0, 0.6, 0.3);
			}
		}
		public Threshold VmNetworkReadThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmNetworkReadThreshold, 0.0, 1.7976931348623157E+308, 1000000.0, 500000.0, 100000.0);
			}
		}
		public Threshold VmNetworkReadWeight
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmNetworkReadWeight, 0.0, 1.0, 1.0, 0.6, 0.3);
			}
		}
		public Threshold VmNetworkWriteThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmNetworkWriteThreshold, 0.0, 1.7976931348623157E+308, 1000000.0, 500000.0, 100000.0);
			}
		}
		public Threshold VmNetworkWriteWeight
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmNetworkWriteWeight, 0.0, 1.0, 1.0, 0.6, 0.3);
			}
		}
		public Threshold VmDiskReadThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmDiskReadThreshold, 0.0, 1.7976931348623157E+308, 1000000.0, 500000.0, 100000.0);
			}
		}
		public Threshold VmDiskReadWeight
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmDiskReadWeight, 0.0, 1.0, 1.0, 0.6, 0.3);
			}
		}
		public Threshold VmDiskWriteThreshold
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmDiskWriteThreshold, 0.0, 1.7976931348623157E+308, 1000000.0, 500000.0, 100000.0);
			}
		}
		public Threshold VmDiskWriteWeight
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmDiskWriteWeight, 0.0, 1.0, 1.0, 0.6, 0.3);
			}
		}
		public Threshold VmPowerManagementWeight
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmPowerManagementWeight, 0.0, 1.0, 1.0, 0.6, 0.3);
			}
		}
		public Threshold VmRunstateWeight
		{
			get
			{
				return DwmPoolBase.GetThreshold(ref this._vmRunstateWeight, 0.0, 1.0, 1.0, 0.6, 0.3);
			}
		}
		public virtual string TouchedBy
		{
			get
			{
				return this._touchedBy;
			}
			set
			{
				this._touchedBy = value;
			}
		}
		public virtual DateTime TimeStamp
		{
			get
			{
				return this._timeStamp;
			}
			internal set
			{
				this._timeStamp = value;
			}
		}
		protected DwmPoolBase(string uuid, string name, DwmHypervisorType hypervisorType) : base(uuid, name)
		{
			if (hypervisorType == DwmHypervisorType.None)
			{
				throw new DwmException("Hypervisor type must be specified", DwmErrorCode.InvalidParameter, null);
			}
			if (string.IsNullOrEmpty(uuid) && string.IsNullOrEmpty(name))
			{
				throw new DwmException("Uuid or name must be specified", DwmErrorCode.InvalidParameter, null);
			}
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmPoolBase.UuidToId(uuid);
			}
			else
			{
				base.Id = DwmPoolBase.NameToId(name);
			}
			this._hypervisorType = hypervisorType;
		}
		protected DwmPoolBase(int poolId) : base(poolId)
		{
		}
		internal static int UuidToId(string uuid)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(uuid) && !DwmPoolBase._uuidCache.TryGetValue(uuid, out num))
			{
				using (DBAccess dBAccess = new DBAccess())
				{
					num = dBAccess.ExecuteScalarInt("get_pool_id_by_pool_uuid", new StoredProcParamCollection
					{
						new StoredProcParam("@pool_uuid", uuid)
					});
					if (num != 0)
					{
						object uuidCacheLock = DwmPoolBase._uuidCacheLock;
						Monitor.Enter(uuidCacheLock);
						try
						{
							if (!DwmPoolBase._uuidCache.ContainsKey(uuid))
							{
								DwmPoolBase._uuidCache.Add(uuid, num);
							}
						}
						finally
						{
							Monitor.Exit(uuidCacheLock);
						}
					}
				}
			}
			return num;
		}
		internal static int NameToId(string name)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(name) && !DwmPoolBase._nameCache.TryGetValue(name, out num))
			{
				using (DBAccess dBAccess = new DBAccess())
				{
					num = dBAccess.ExecuteScalarInt("get_pool_id_by_pool_name", new StoredProcParamCollection
					{
						new StoredProcParam("@pool_name", name)
					});
					if (num != 0)
					{
						object nameCacheLock = DwmPoolBase._nameCacheLock;
						Monitor.Enter(nameCacheLock);
						try
						{
							if (!DwmPoolBase._nameCache.ContainsKey(name))
							{
								DwmPoolBase._nameCache.Add(name, num);
							}
						}
						finally
						{
							Monitor.Exit(nameCacheLock);
						}
					}
				}
			}
			return num;
		}
		public static void RefreshCache()
		{
			object uuidCacheLock = DwmPoolBase._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				DwmPoolBase._uuidCache.Clear();
			}
			finally
			{
				Monitor.Exit(uuidCacheLock);
			}
			object nameCacheLock = DwmPoolBase._nameCacheLock;
			Monitor.Enter(nameCacheLock);
			try
			{
				DwmPoolBase._nameCache.Clear();
			}
			finally
			{
				Monitor.Exit(nameCacheLock);
			}
			DwmHost.RefreshCache();
			DwmVirtualMachine.RefreshCache();
			DwmPbd.RefreshCache();
			DwmPif.RefreshCache();
			DwmVbd.RefreshCache();
		}
		protected static void RemovePoolFromCache(int poolId)
		{
			object nameCacheLock = DwmPoolBase._nameCacheLock;
			Monitor.Enter(nameCacheLock);
			try
			{
				foreach (KeyValuePair<string, int> current in DwmPoolBase._nameCache)
				{
					if (current.Value == poolId)
					{
						bool flag = DwmPoolBase._nameCache.Remove(current.Key);
						Logger.Trace("{0} pool with id={1} from _nameCache", new object[]
						{
							(!flag) ? "Could not removed" : "Removed",
							poolId
						});
						DwmHost.RefreshCache();
						DwmVirtualMachine.RefreshCache();
						DwmPbd.RefreshCache();
						DwmPif.RefreshCache();
						DwmVbd.RefreshCache();
						break;
					}
				}
			}
			finally
			{
				Monitor.Exit(nameCacheLock);
			}
			object uuidCacheLock = DwmPoolBase._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				foreach (KeyValuePair<string, int> current2 in DwmPoolBase._uuidCache)
				{
					if (current2.Value == poolId)
					{
						bool flag = DwmPoolBase._uuidCache.Remove(current2.Key);
						Logger.Trace("{0} pool with id={1} from _uuidCache", new object[]
						{
							(!flag) ? "Could not removed" : "Removed",
							poolId
						});
						break;
					}
				}
			}
			finally
			{
				Monitor.Exit(uuidCacheLock);
			}
		}
		protected static void RemovePoolFromCache(string poolUuid)
		{
			int num = 0;
			object uuidCacheLock = DwmPoolBase._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				if (DwmPoolBase._uuidCache.ContainsKey(poolUuid))
				{
					num = DwmPoolBase._uuidCache[poolUuid];
					bool flag = DwmPoolBase._uuidCache.Remove(poolUuid);
					Logger.Trace("{0} pool with uuid={1} from _uuidCache", new object[]
					{
						(!flag) ? "Could not removed" : "Removed",
						poolUuid
					});
					DwmHost.RefreshCache();
					DwmVirtualMachine.RefreshCache();
					DwmPbd.RefreshCache();
					DwmPif.RefreshCache();
					DwmVbd.RefreshCache();
				}
			}
			finally
			{
				Monitor.Exit(uuidCacheLock);
			}
			if (num > 0)
			{
				object nameCacheLock = DwmPoolBase._nameCacheLock;
				Monitor.Enter(nameCacheLock);
				try
				{
					foreach (KeyValuePair<string, int> current in DwmPoolBase._nameCache)
					{
						if (current.Value == num)
						{
							bool flag = DwmPoolBase._nameCache.Remove(current.Key);
							Logger.Trace("{0} pool with id={1} from _nameCache", new object[]
							{
								(!flag) ? "Could not removed" : "Removed",
								num
							});
							break;
						}
					}
				}
				finally
				{
					Monitor.Exit(nameCacheLock);
				}
			}
		}
		public void LoadThresholdsAndWeights(IDataReader reader)
		{
			this.OptMode = (OptimizationMode)DBAccess.GetInt(reader, "opt_mode", 0);
			this.HostCpuThreshold.Critical = DBAccess.GetDouble(reader, "host_cpu_threshold_critical", this.HostCpuThreshold.Critical);
			this.HostCpuThreshold.High = DBAccess.GetDouble(reader, "host_cpu_threshold_high", this.HostCpuThreshold.High);
			this.HostCpuThreshold.Medium = DBAccess.GetDouble(reader, "host_cpu_threshold_medium", this.HostCpuThreshold.Medium);
			this.HostCpuThreshold.Low = DBAccess.GetDouble(reader, "host_cpu_threshold_low", this.HostCpuThreshold.Low);
			this.HostMemoryThreshold.Critical = DBAccess.GetDouble(reader, "host_memory_threshold_critical", this.HostMemoryThreshold.Critical);
			this.HostMemoryThreshold.High = DBAccess.GetDouble(reader, "host_memory_threshold_high", this.HostMemoryThreshold.High);
			this.HostMemoryThreshold.Medium = DBAccess.GetDouble(reader, "host_memory_threshold_medium", this.HostMemoryThreshold.Medium);
			this.HostMemoryThreshold.Low = DBAccess.GetDouble(reader, "host_memory_threshold_low", this.HostMemoryThreshold.Low);
			this.HostPifReadThreshold.Critical = DBAccess.GetDouble(reader, "host_net_read_threshold_critical", this.HostPifReadThreshold.Critical);
			this.HostPifReadThreshold.High = DBAccess.GetDouble(reader, "host_net_read_threshold_high", this.HostPifReadThreshold.High);
			this.HostPifReadThreshold.Medium = DBAccess.GetDouble(reader, "host_net_read_threshold_medium", this.HostPifReadThreshold.Medium);
			this.HostPifReadThreshold.Low = DBAccess.GetDouble(reader, "host_net_read_threshold_low", this.HostPifReadThreshold.Low);
			this.HostPifWriteThreshold.Critical = DBAccess.GetDouble(reader, "host_net_write_threshold_critical", this.HostPifWriteThreshold.Critical);
			this.HostPifWriteThreshold.High = DBAccess.GetDouble(reader, "host_net_write_threshold_high", this.HostPifWriteThreshold.High);
			this.HostPifWriteThreshold.Medium = DBAccess.GetDouble(reader, "host_net_write_threshold_medium", this.HostPifWriteThreshold.Medium);
			this.HostPifWriteThreshold.Low = DBAccess.GetDouble(reader, "host_net_write_threshold_low", this.HostPifWriteThreshold.Low);
			this.HostPbdReadThreshold.Critical = DBAccess.GetDouble(reader, "host_disk_read_threshold_critical", this.HostPbdReadThreshold.Critical);
			this.HostPbdReadThreshold.High = DBAccess.GetDouble(reader, "host_disk_read_threshold_high", this.HostPbdReadThreshold.High);
			this.HostPbdReadThreshold.Medium = DBAccess.GetDouble(reader, "host_disk_read_threshold_medium", this.HostPbdReadThreshold.Medium);
			this.HostPbdReadThreshold.Low = DBAccess.GetDouble(reader, "host_disk_read_threshold_low", this.HostPbdReadThreshold.Low);
			this.HostPbdWriteThreshold.Critical = DBAccess.GetDouble(reader, "host_disk_write_threshold_critical", this.HostPbdWriteThreshold.Critical);
			this.HostPbdWriteThreshold.High = DBAccess.GetDouble(reader, "host_disk_write_threshold_high", this.HostPbdWriteThreshold.High);
			this.HostPbdWriteThreshold.Medium = DBAccess.GetDouble(reader, "host_disk_write_threshold_medium", this.HostPbdWriteThreshold.Medium);
			this.HostPbdWriteThreshold.Low = DBAccess.GetDouble(reader, "host_disk_write_threshold_low", this.HostPbdWriteThreshold.Low);
			this.HostLoadAverageThreshold.Critical = DBAccess.GetDouble(reader, "host_load_avg_threshold_critical", this.HostLoadAverageThreshold.Critical);
			this.HostLoadAverageThreshold.High = DBAccess.GetDouble(reader, "host_load_avg_threshold_high", this.HostLoadAverageThreshold.High);
			this.HostLoadAverageThreshold.Medium = DBAccess.GetDouble(reader, "host_load_avg_threshold_medium", this.HostLoadAverageThreshold.Medium);
			this.HostLoadAverageThreshold.Low = DBAccess.GetDouble(reader, "host_load_avg_threshold_low", this.HostLoadAverageThreshold.Low);
			this.HostRunstateFullContentionThreshold.Critical = DBAccess.GetDouble(reader, "host_runstate_full_contention_threshold_critical", this.HostRunstateFullContentionThreshold.Critical);
			this.HostRunstateFullContentionThreshold.High = DBAccess.GetDouble(reader, "host_runstate_full_contention_threshold_high", this.HostRunstateFullContentionThreshold.High);
			this.HostRunstateFullContentionThreshold.Medium = DBAccess.GetDouble(reader, "host_runstate_full_contention_threshold_medium", this.HostRunstateFullContentionThreshold.Medium);
			this.HostRunstateFullContentionThreshold.Low = DBAccess.GetDouble(reader, "host_runstate_full_contention_threshold_low", this.HostRunstateFullContentionThreshold.Low);
			this.HostRunstateConcurrencyHazardThreshold.Critical = DBAccess.GetDouble(reader, "host_runstate_concurrency_hazard_threshold_critical", this.HostRunstateConcurrencyHazardThreshold.Critical);
			this.HostRunstateConcurrencyHazardThreshold.High = DBAccess.GetDouble(reader, "host_runstate_concurrency_hazard_threshold_high", this.HostRunstateConcurrencyHazardThreshold.High);
			this.HostRunstateConcurrencyHazardThreshold.Medium = DBAccess.GetDouble(reader, "host_runstate_concurrency_hazard_threshold_medium", this.HostRunstateConcurrencyHazardThreshold.Medium);
			this.HostRunstateConcurrencyHazardThreshold.Low = DBAccess.GetDouble(reader, "host_runstate_concurrency_hazard_threshold_low", this.HostRunstateConcurrencyHazardThreshold.Low);
			this.HostRunstatePartialContentionThreshold.Critical = DBAccess.GetDouble(reader, "host_runstate_partial_contention_threshold_critical", this.HostRunstatePartialContentionThreshold.Critical);
			this.HostRunstatePartialContentionThreshold.High = DBAccess.GetDouble(reader, "host_runstate_partial_contention_threshold_high", this.HostRunstatePartialContentionThreshold.High);
			this.HostRunstatePartialContentionThreshold.Medium = DBAccess.GetDouble(reader, "host_runstate_partial_contention_threshold_medium", this.HostRunstatePartialContentionThreshold.Medium);
			this.HostRunstatePartialContentionThreshold.Low = DBAccess.GetDouble(reader, "host_runstate_partial_contention_threshold_low", this.HostRunstatePartialContentionThreshold.Low);
			this.HostRunstateFullRunThreshold.Critical = DBAccess.GetDouble(reader, "host_runstate_fullrun_threshold_critical", this.HostRunstateFullRunThreshold.Critical);
			this.HostRunstateFullRunThreshold.High = DBAccess.GetDouble(reader, "host_runstate_fullrun_threshold_high", this.HostRunstateFullRunThreshold.High);
			this.HostRunstateFullRunThreshold.Medium = DBAccess.GetDouble(reader, "host_runstate_fullrun_threshold_medium", this.HostRunstateFullRunThreshold.Medium);
			this.HostRunstateFullRunThreshold.Low = DBAccess.GetDouble(reader, "host_runstate_fullrun_threshold_low", this.HostRunstateFullRunThreshold.Low);
			this.HostRunstatePartialRunThreshold.Critical = DBAccess.GetDouble(reader, "host_runstate_partial_run_threshold_critical", this.HostRunstatePartialRunThreshold.Critical);
			this.HostRunstatePartialRunThreshold.High = DBAccess.GetDouble(reader, "host_runstate_partial_run_threshold_high", this.HostRunstatePartialRunThreshold.High);
			this.HostRunstatePartialRunThreshold.Medium = DBAccess.GetDouble(reader, "host_runstate_partial_run_threshold_medium", this.HostRunstatePartialRunThreshold.Medium);
			this.HostRunstatePartialRunThreshold.Low = DBAccess.GetDouble(reader, "host_runstate_partial_run_threshold_low", this.HostRunstatePartialRunThreshold.Low);
			this.HostRunstateBlockedThreshold.Critical = DBAccess.GetDouble(reader, "host_runstate_blocked_threshold_critical", this.HostRunstateBlockedThreshold.Critical);
			this.HostRunstateBlockedThreshold.High = DBAccess.GetDouble(reader, "host_runstate_blocked_threshold_high", this.HostRunstateBlockedThreshold.High);
			this.HostRunstateBlockedThreshold.Medium = DBAccess.GetDouble(reader, "host_runstate_blocked_threshold_medium", this.HostRunstateBlockedThreshold.Medium);
			this.HostRunstateBlockedThreshold.Low = DBAccess.GetDouble(reader, "host_runstate_blocked_threshold_low", this.HostRunstateBlockedThreshold.Low);
			this.WeightCurrentMetrics = DBAccess.GetDouble(reader, "weight_current_metrics", this.WeightCurrentMetrics);
			this.WeightRecentMetrics = DBAccess.GetDouble(reader, "weight_last_30_metrics", this.WeightRecentMetrics);
			this.WeightHistoricMetrics = DBAccess.GetDouble(reader, "weight_yesterday_metrics", this.WeightHistoricMetrics);
			this.OverCommitCpusInPerfMode = DBAccess.GetBool(reader, "over_commit_cpu_in_perf_mode", this.OverCommitCpusInPerfMode);
			this.OverCommitCpusInDensityMode = DBAccess.GetBool(reader, "over_commit_cpu_in_density_mode", this.OverCommitCpusInDensityMode);
			this.OverCommitCpuRatio = DBAccess.GetInt(reader, "over_commit_cpu_ratio", this.OverCommitCpuRatio);
			this.AutoBalance = DBAccess.GetBool(reader, "auto_balance_enabled", this.AutoBalance);
			this.ManagePower = DBAccess.GetBool(reader, "power_management_enabled", this.ManagePower);
			string @string = DBAccess.GetString(reader, "pool_master_cpu_limit", "-1");
			if (!double.TryParse(@string, out this._poolMasterCpuLimit))
			{
				this._poolMasterCpuLimit = -1.0;
			}
			@string = DBAccess.GetString(reader, "pool_master_net_io_limit", "-1");
			if (!double.TryParse(@string, out this._poolMasterIoLimit))
			{
				this._poolMasterIoLimit = -1.0;
			}
			if (this._poolMasterIoLimit > 0.0)
			{
				this._poolMasterIoLimit *= 1048576.0;
			}
			@string = DBAccess.GetString(reader, "prefer_power_on_over_compression");
			bool.TryParse(@string, out this._preferPowerOnOverCompression);
			@string = DBAccess.GetString(reader, "power_on_host_if_no_memory");
			bool.TryParse(@string, out this._powerOnHostIfNoMemory);
			@string = DBAccess.GetString(reader, "power_on_host_if_no_sr");
			bool.TryParse(@string, out this._powerOnHostIfNoSR);
			@string = DBAccess.GetString(reader, "compress_guests_to_relieve_pressure");
			bool.TryParse(@string, out this._compressGuestsToRelievePressure);
			@string = DBAccess.GetString(reader, "compress_guests_to_preserve_power");
			bool.TryParse(@string, out this._compressGuestsToPreservePower);
			@string = DBAccess.GetString(reader, "max_performance_max_compression_ratio");
			if (double.TryParse(@string, out this._maxPerformanceMaxCompressionRatio))
			{
				if (this._maxPerformanceMaxCompressionRatio > 0.5)
				{
					this._maxPerformanceMaxCompressionRatio = 0.5;
				}
				else
				{
					if (this._maxPerformanceMaxCompressionRatio < 0.0)
					{
						this._maxPerformanceMaxCompressionRatio = 0.0;
					}
				}
			}
			@string = DBAccess.GetString(reader, "max_density_max_compression_ratio");
			if (double.TryParse(@string, out this._maxDensityMaxCompressionRatio))
			{
				if (this._maxDensityMaxCompressionRatio > 0.75)
				{
					this._maxDensityMaxCompressionRatio = 0.75;
				}
				else
				{
					if (this._maxDensityMaxCompressionRatio < 0.0)
					{
						this._maxDensityMaxCompressionRatio = 0.0;
					}
				}
			}
			this.VmCpuUtilizationThreshold.High = DBAccess.GetDouble(reader, "vm_cpu_threshold_high", this.VmCpuUtilizationThreshold.High);
			this.VmCpuUtilizationThreshold.Medium = DBAccess.GetDouble(reader, "vm_cpu_threshold_medium", this.VmCpuUtilizationThreshold.Medium);
			this.VmCpuUtilizationThreshold.Low = DBAccess.GetDouble(reader, "vm_cpu_threshold_low", this.VmCpuUtilizationThreshold.Low);
			this.VmCpuUtilizationWeight.High = DBAccess.GetDouble(reader, "vm_cpu_weight_high", this.VmCpuUtilizationWeight.High);
			this.VmCpuUtilizationWeight.Medium = DBAccess.GetDouble(reader, "vm_cpu_weight_medium", this.VmCpuUtilizationWeight.Medium);
			this.VmCpuUtilizationWeight.Low = DBAccess.GetDouble(reader, "vm_cpu_weight_low", this.VmCpuUtilizationWeight.Low);
			this.VmMemoryThreshold.High = DBAccess.GetDouble(reader, "vm_memory_threshold_high", this.VmMemoryThreshold.High);
			this.VmMemoryThreshold.Medium = DBAccess.GetDouble(reader, "vm_memory_threshold_medium", this.VmMemoryThreshold.Medium);
			this.VmMemoryThreshold.Low = DBAccess.GetDouble(reader, "vm_memory_threshold_low", this.VmMemoryThreshold.Low);
			this.VmMemoryWeight.High = DBAccess.GetDouble(reader, "vm_memory_weight_high", this.VmMemoryWeight.High);
			this.VmMemoryWeight.Medium = DBAccess.GetDouble(reader, "vm_memory_weight_medium", this.VmMemoryWeight.Medium);
			this.VmMemoryWeight.Low = DBAccess.GetDouble(reader, "vm_memory_weight_low", this.VmMemoryWeight.Low);
			this.VmNetworkReadThreshold.High = DBAccess.GetDouble(reader, "vm_net_read_threshold_high", this.VmNetworkReadThreshold.High);
			this.VmNetworkReadThreshold.Medium = DBAccess.GetDouble(reader, "vm_net_read_threshold_medium", this.VmNetworkReadThreshold.Medium);
			this.VmNetworkReadThreshold.Low = DBAccess.GetDouble(reader, "vm_net_read_threshold_low", this.VmNetworkReadThreshold.Low);
			this.VmNetworkReadWeight.High = DBAccess.GetDouble(reader, "vm_net_read_weight_high", this.VmNetworkReadWeight.High);
			this.VmNetworkReadWeight.Medium = DBAccess.GetDouble(reader, "vm_net_read_weight_medium", this.VmNetworkReadWeight.Medium);
			this.VmNetworkReadWeight.Low = DBAccess.GetDouble(reader, "vm_net_read_weight_low", this.VmNetworkReadWeight.Low);
			this.VmNetworkWriteThreshold.High = DBAccess.GetDouble(reader, "vm_net_write_threshold_high", this.VmNetworkWriteThreshold.High);
			this.VmNetworkWriteThreshold.Medium = DBAccess.GetDouble(reader, "vm_net_write_threshold_medium", this.VmNetworkWriteThreshold.Medium);
			this.VmNetworkWriteThreshold.Low = DBAccess.GetDouble(reader, "vm_net_write_threshold_low", this.VmNetworkWriteThreshold.Low);
			this.VmNetworkWriteWeight.High = DBAccess.GetDouble(reader, "vm_net_write_weight_high", this.VmNetworkWriteWeight.High);
			this.VmNetworkWriteWeight.Medium = DBAccess.GetDouble(reader, "vm_net_write_weight_medium", this.VmNetworkWriteWeight.Medium);
			this.VmNetworkWriteWeight.Low = DBAccess.GetDouble(reader, "vm_net_write_weight_low", this.VmNetworkWriteWeight.Low);
			this.VmDiskReadThreshold.High = DBAccess.GetDouble(reader, "vm_disk_read_threshold_high", this.VmDiskReadThreshold.High);
			this.VmDiskReadThreshold.Medium = DBAccess.GetDouble(reader, "vm_disk_read_threshold_medium", this.VmDiskReadThreshold.Medium);
			this.VmDiskReadThreshold.Low = DBAccess.GetDouble(reader, "vm_disk_read_threshold_low", this.VmDiskReadThreshold.Low);
			this.VmDiskReadWeight.High = DBAccess.GetDouble(reader, "vm_disk_read_weight_high", this.VmDiskReadWeight.High);
			this.VmDiskReadWeight.Medium = DBAccess.GetDouble(reader, "vm_disk_read_weight_medium", this.VmDiskReadWeight.Medium);
			this.VmDiskReadWeight.Low = DBAccess.GetDouble(reader, "vm_disk_read_weight_low", this.VmDiskReadWeight.Low);
			this.VmDiskWriteThreshold.High = DBAccess.GetDouble(reader, "vm_disk_write_threshold_high", this.VmDiskWriteThreshold.High);
			this.VmDiskWriteThreshold.Medium = DBAccess.GetDouble(reader, "vm_disk_write_threshold_medium", this.VmDiskWriteThreshold.Medium);
			this.VmDiskWriteThreshold.Low = DBAccess.GetDouble(reader, "vm_disk_write_threshold_low", this.VmDiskWriteThreshold.Low);
			this.VmDiskWriteWeight.High = DBAccess.GetDouble(reader, "vm_disk_write_weight_high", this.VmDiskWriteWeight.High);
			this.VmDiskWriteWeight.Medium = DBAccess.GetDouble(reader, "vm_disk_write_weight_medium", this.VmDiskWriteWeight.Medium);
			this.VmDiskWriteWeight.Low = DBAccess.GetDouble(reader, "vm_disk_write_weight_low", this.VmDiskWriteWeight.Low);
			this.VmRunstateWeight.High = DBAccess.GetDouble(reader, "vm_runstate_weight_high", this.VmRunstateWeight.High);
			this.VmRunstateWeight.Medium = DBAccess.GetDouble(reader, "vm_runstate_weight_medium", this.VmRunstateWeight.Medium);
			this.VmRunstateWeight.Low = DBAccess.GetDouble(reader, "vm_runstate_weight_low", this.VmRunstateWeight.Low);
		}
		private static HostThreshold GetThreshold(ref HostThreshold threshold, double min, double max, double critical, double high, double medium, double low)
		{
			if (threshold == null)
			{
				threshold = new HostThreshold(min, max, critical, high, medium, low);
			}
			return threshold;
		}
		private static Threshold GetThreshold(ref Threshold threshold, double min, double max, double high, double medium, double low)
		{
			if (threshold == null)
			{
				threshold = new Threshold(min, max, high, medium, low);
			}
			return threshold;
		}
		private static double CheckRange(double value, double min, double max)
		{
			if (value > max || value < min)
			{
				throw new DwmException(Localization.Format("Value must be in the range {0} to {1}", min, max), DwmErrorCode.ArgumentOutOfRange, null);
			}
			return value;
		}
	}
}
