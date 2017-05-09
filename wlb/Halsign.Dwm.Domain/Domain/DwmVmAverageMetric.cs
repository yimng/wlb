using System;
namespace Halsign.DWM.Domain
{
	public class DwmVmAverageMetric
	{
		private DwmAverageMetricVM _metricsNow;
		private DwmAverageMetricVM _metricsLast30Minutes;
		private DwmAverageMetricVM _metricsYesterday;
		private int _score;
		private long _averageUsedMemory;
		private long _totalMemory;
		private double _vmCpuUtil;
		private double _vmMemory;
		private double _vmVifRead;
		private double _vmVifWrite;
		private double _vmVbdRead;
		private double _vmVbdWrite;
		private bool _haveCurrentMetrics;
		private bool _haveLast30Metrics;
		private bool _haveYesterdayMetrics;
		private double _cpuWeight;
		private double _pifReadWeight;
		private double _memoryWeight;
		private double _pbdWriteWeight;
		private double _pbdReadWeight;
		private double _pifWriteWeight;
		private double _powerManagementWeight;
		private double _vmRunstateWeight;
		private double _vmRunstateFullContention;
		private double _vmRunstateConcurrencyHazard;
		private double _vmRunstatePartialContention;
		private double _vmRunstateFullRun;
		private double _vmRunstatePartialRun;
		private double _vmRunstateBlocked;
		public DwmAverageMetricVM MetricsNow
		{
			get
			{
				return DwmBase.SafeGetItem<DwmAverageMetricVM>(ref this._metricsNow);
			}
			internal set
			{
				this._metricsNow = value;
			}
		}
		public DwmAverageMetricVM MetricsLast30Minutes
		{
			get
			{
				return DwmBase.SafeGetItem<DwmAverageMetricVM>(ref this._metricsLast30Minutes);
			}
		}
		public DwmAverageMetricVM MetricsYesterday
		{
			get
			{
				return DwmBase.SafeGetItem<DwmAverageMetricVM>(ref this._metricsYesterday);
			}
		}
		public int Score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}
		public long AverageUsedMemory
		{
			get
			{
				return this._averageUsedMemory;
			}
			set
			{
				this._averageUsedMemory = value;
			}
		}
		public long TotalMemory
		{
			get
			{
				return this._totalMemory;
			}
			set
			{
				this._totalMemory = value;
			}
		}
		public double VmCpuUtil
		{
			get
			{
				return this._vmCpuUtil;
			}
			set
			{
				this._vmCpuUtil = value;
			}
		}
		public double VmMemory
		{
			get
			{
				return this._vmMemory;
			}
			set
			{
				this._vmMemory = value;
			}
		}
		public double VmVifRead
		{
			get
			{
				return this._vmVifRead;
			}
			set
			{
				this._vmVifRead = value;
			}
		}
		public double VmVifWrite
		{
			get
			{
				return this._vmVifWrite;
			}
			set
			{
				this._vmVifWrite = value;
			}
		}
		public double VmVbdRead
		{
			get
			{
				return this._vmVbdRead;
			}
			set
			{
				this._vmVbdRead = value;
			}
		}
		public double VmVbdWrite
		{
			get
			{
				return this._vmVbdWrite;
			}
			set
			{
				this._vmVbdWrite = value;
			}
		}
		public double VmRunstateFullContention
		{
			get
			{
				return this._vmRunstateFullContention;
			}
			set
			{
				this._vmRunstateFullContention = value;
			}
		}
		public double VmRunstateConcurrencyHazard
		{
			get
			{
				return this._vmRunstateConcurrencyHazard;
			}
			set
			{
				this._vmRunstateConcurrencyHazard = value;
			}
		}
		public double VmRunstatePartialContention
		{
			get
			{
				return this._vmRunstatePartialContention;
			}
			set
			{
				this._vmRunstatePartialContention = value;
			}
		}
		public double VmRunstateFullRun
		{
			get
			{
				return this._vmRunstateFullRun;
			}
			set
			{
				this._vmRunstateFullRun = value;
			}
		}
		public double VmRunstatePartialRun
		{
			get
			{
				return this._vmRunstatePartialRun;
			}
			set
			{
				this._vmRunstatePartialRun = value;
			}
		}
		public double VmRunstateBlocked
		{
			get
			{
				return this._vmRunstateBlocked;
			}
			set
			{
				this._vmRunstateBlocked = value;
			}
		}
		public double CpuWeight
		{
			get
			{
				return this._cpuWeight;
			}
			set
			{
				this._cpuWeight = value;
			}
		}
		public double PifReadWeight
		{
			get
			{
				return this._pifReadWeight;
			}
			set
			{
				this._pifReadWeight = value;
			}
		}
		public double PifWriteWeight
		{
			get
			{
				return this._pifWriteWeight;
			}
			set
			{
				this._pifWriteWeight = value;
			}
		}
		public double PbdReadWeight
		{
			get
			{
				return this._pbdReadWeight;
			}
			set
			{
				this._pbdReadWeight = value;
			}
		}
		public double PbdWriteWeight
		{
			get
			{
				return this._pbdWriteWeight;
			}
			set
			{
				this._pbdWriteWeight = value;
			}
		}
		public double MemoryWeight
		{
			get
			{
				return this._memoryWeight;
			}
			set
			{
				this._memoryWeight = value;
			}
		}
		public double PowerManagementWeight
		{
			get
			{
				return this._powerManagementWeight;
			}
			set
			{
				this._powerManagementWeight = value;
			}
		}
		public double RunstateWeight
		{
			get
			{
				return this._vmRunstateWeight;
			}
			set
			{
				this._vmRunstateWeight = value;
			}
		}
		public bool HaveCurrentMetrics
		{
			get
			{
				return this._haveCurrentMetrics;
			}
			set
			{
				this._haveCurrentMetrics = value;
			}
		}
		public bool HaveLast30Metrics
		{
			get
			{
				return this._haveLast30Metrics;
			}
			set
			{
				this._haveLast30Metrics = value;
			}
		}
		public bool HaveYesterdayMetrics
		{
			get
			{
				return this._haveYesterdayMetrics;
			}
			set
			{
				this._haveYesterdayMetrics = value;
			}
		}
		public DwmVmAverageMetric Copy()
		{
			return new DwmVmAverageMetric
			{
				Score = this.Score,
				AverageUsedMemory = this.AverageUsedMemory,
				TotalMemory = this.TotalMemory,
				CpuWeight = this.CpuWeight,
				MemoryWeight = this.MemoryWeight,
				PbdReadWeight = this.PbdReadWeight,
				PbdWriteWeight = this.PbdWriteWeight,
				PifReadWeight = this.PifReadWeight,
				PifWriteWeight = this.PifWriteWeight,
				RunstateWeight = this.RunstateWeight,
				PowerManagementWeight = this.PowerManagementWeight,
				VmRunstateFullContention = this.VmRunstateFullContention,
				VmRunstateConcurrencyHazard = this.VmRunstateConcurrencyHazard,
				VmRunstatePartialContention = this.VmRunstatePartialContention,
				VmRunstateFullRun = this.VmRunstateFullRun,
				VmRunstatePartialRun = this.VmRunstatePartialRun,
				VmRunstateBlocked = this.VmRunstateBlocked,
				VmCpuUtil = this.VmCpuUtil,
				VmMemory = this.VmMemory,
				VmVbdRead = this.VmVbdRead,
				VmVbdWrite = this.VmVbdWrite,
				VmVifRead = this.VmVifRead,
				VmVifWrite = this.VmVifWrite,
				HaveCurrentMetrics = this.HaveCurrentMetrics,
				HaveLast30Metrics = this.HaveLast30Metrics,
				HaveYesterdayMetrics = this.HaveYesterdayMetrics,
				MetricsNow = 
				{
					AverageFreeMemory = this.MetricsNow.AverageFreeMemory,
					AverageTargetMemory = this.MetricsNow.AverageTargetMemory,
					AverageTotalMemory = this.MetricsNow.AverageTotalMemory,
					AverageUsedMemory = this.MetricsNow.AverageUsedMemory,
					AverageRunstateFullContention = this.MetricsNow.AverageRunstateFullContention,
					AverageRunstateConcurrencyHazard = this.MetricsNow.AverageRunstateConcurrencyHazard,
					AverageRunstatePartialContention = this.MetricsNow.AverageRunstatePartialContention,
					AverageRunstateFullRun = this.MetricsNow.AverageRunstateFullRun,
					AverageRunstatePartialRun = this.MetricsNow.AverageRunstatePartialRun,
					AverageRunstateBlocked = this.MetricsNow.AverageRunstateBlocked,
					AverageCpuUtilization = this.MetricsNow.AverageCpuUtilization,
					TotalVbdNetReadsPerSecond = this.MetricsNow.TotalVbdNetReadsPerSecond,
					TotalVbdNetWritesPerSecond = this.MetricsNow.TotalVbdNetWritesPerSecond,
					AveragePbdReadsPerSecond = this.MetricsNow.AveragePbdReadsPerSecond,
					AveragePbdWritesPerSecond = this.MetricsNow.AveragePbdWritesPerSecond,
					AveragePifReadsPerSecond = this.MetricsNow.AveragePifReadsPerSecond,
					AveragePifWritesPerSecond = this.MetricsNow.AveragePifWritesPerSecond,
					EndTime = this.MetricsNow.EndTime,
					StartTime = this.MetricsNow.StartTime
				},
				MetricsLast30Minutes = 
				{
					AverageFreeMemory = this.MetricsLast30Minutes.AverageFreeMemory,
					AverageTargetMemory = this.MetricsLast30Minutes.AverageTargetMemory,
					AverageTotalMemory = this.MetricsLast30Minutes.AverageTotalMemory,
					AverageUsedMemory = this.MetricsLast30Minutes.AverageUsedMemory,
					AverageRunstateFullContention = this.MetricsLast30Minutes.AverageRunstateFullContention,
					AverageRunstateConcurrencyHazard = this.MetricsLast30Minutes.AverageRunstateConcurrencyHazard,
					AverageRunstatePartialContention = this.MetricsLast30Minutes.AverageRunstatePartialContention,
					AverageRunstateFullRun = this.MetricsLast30Minutes.AverageRunstateFullRun,
					AverageRunstatePartialRun = this.MetricsLast30Minutes.AverageRunstatePartialRun,
					AverageRunstateBlocked = this.MetricsLast30Minutes.AverageRunstateBlocked,
					AverageCpuUtilization = this.MetricsLast30Minutes.AverageCpuUtilization,
					TotalVbdNetReadsPerSecond = this.MetricsLast30Minutes.TotalVbdNetReadsPerSecond,
					TotalVbdNetWritesPerSecond = this.MetricsLast30Minutes.TotalVbdNetWritesPerSecond,
					AveragePbdReadsPerSecond = this.MetricsLast30Minutes.AveragePbdReadsPerSecond,
					AveragePbdWritesPerSecond = this.MetricsLast30Minutes.AveragePbdWritesPerSecond,
					AveragePifReadsPerSecond = this.MetricsLast30Minutes.AveragePifReadsPerSecond,
					AveragePifWritesPerSecond = this.MetricsLast30Minutes.AveragePifWritesPerSecond,
					EndTime = this.MetricsLast30Minutes.EndTime,
					StartTime = this.MetricsLast30Minutes.StartTime
				},
				MetricsYesterday = 
				{
					AverageFreeMemory = this.MetricsYesterday.AverageFreeMemory,
					AverageTargetMemory = this.MetricsYesterday.AverageTargetMemory,
					AverageTotalMemory = this.MetricsYesterday.AverageTotalMemory,
					AverageUsedMemory = this.MetricsYesterday.AverageUsedMemory,
					AverageRunstateFullContention = this.MetricsYesterday.AverageRunstateFullContention,
					AverageRunstateConcurrencyHazard = this.MetricsYesterday.AverageRunstateConcurrencyHazard,
					AverageRunstatePartialContention = this.MetricsYesterday.AverageRunstatePartialContention,
					AverageRunstateFullRun = this.MetricsYesterday.AverageRunstateFullRun,
					AverageRunstatePartialRun = this.MetricsYesterday.AverageRunstatePartialRun,
					AverageRunstateBlocked = this.MetricsYesterday.AverageRunstateBlocked,
					AverageCpuUtilization = this.MetricsYesterday.AverageCpuUtilization,
					TotalVbdNetReadsPerSecond = this.MetricsYesterday.TotalVbdNetReadsPerSecond,
					TotalVbdNetWritesPerSecond = this.MetricsYesterday.TotalVbdNetWritesPerSecond,
					AveragePbdReadsPerSecond = this.MetricsYesterday.AveragePbdReadsPerSecond,
					AveragePbdWritesPerSecond = this.MetricsYesterday.AveragePbdWritesPerSecond,
					AveragePifReadsPerSecond = this.MetricsYesterday.AveragePifReadsPerSecond,
					AveragePifWritesPerSecond = this.MetricsYesterday.AveragePifWritesPerSecond,
					EndTime = this.MetricsYesterday.EndTime,
					StartTime = this.MetricsYesterday.StartTime
				}
			};
		}
		public static void VerifyValidInstance(DwmVmAverageMetric instance, DwmPool pool)
		{
			if (instance != null && pool != null && instance.CpuWeight == 0.0 && instance.MemoryWeight == 0.0 && instance.PifReadWeight == 0.0 && instance.PifWriteWeight == 0.0 && instance.PbdReadWeight == 0.0 && instance.PbdWriteWeight == 0.0)
			{
				instance.CpuWeight = pool.VmCpuUtilizationWeight.High;
				instance.MemoryWeight = pool.VmMemoryWeight.High;
				instance.PifReadWeight = pool.VmNetworkReadWeight.High;
				instance.PifWriteWeight = pool.VmNetworkWriteWeight.High;
				instance.PbdReadWeight = pool.VmDiskReadWeight.High;
				instance.PbdWriteWeight = pool.VmDiskWriteWeight.High;
				instance.RunstateWeight = pool.VmRunstateWeight.High;
			}
		}
	}
}
