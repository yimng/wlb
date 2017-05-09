using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	public class DwmHostAverageMetric
	{
		private DwmAverageMetricHost _metricsNow;
		private DwmAverageMetricHost _metricsLast30Minutes;
		private DwmAverageMetricHost _metricsYesterday;
		private List<ZeroScoreReason> _zeroScoreReasons;
		private long _totalMemory;
		private long _memoryActual;
		private long _controlMemoryOverhead;
		private long _freeMemory;
		private int _numVMs;
		private int _freeCPUs;
		private long _potentialFreeMemory;
		private int _numHighFullContentionVCpus;
		private int _numHighPartialContentionVCpus;
		private int _numHighConcurrencyHazardVCpus;
		private int _numHighFullrunVCpus;
		private int _numHighPartialRunVCpus;
		private int _numHighBlockedVCpus;
		private int _fillOrder;
		private int _score;
		private double _stars;
		private bool _canBootVM = true;
		private int _maxPossibleScore;
		private List<string> _messages;
		private DwmVirtualMachine _vm;
		private int _recommendationId;
		private int _recommendationSetId;
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
		public long MemoryActual
		{
			get
			{
				return this._memoryActual;
			}
			set
			{
				this._memoryActual = value;
			}
		}
		public long ControlMemoryOverhead
		{
			get
			{
				return this._controlMemoryOverhead;
			}
			set
			{
				this._controlMemoryOverhead = value;
			}
		}
		public long FreeMemory
		{
			get
			{
				return this._freeMemory;
			}
			set
			{
				this._freeMemory = value;
			}
		}
		public int NumVMs
		{
			get
			{
				return this._numVMs;
			}
			set
			{
				this._numVMs = value;
			}
		}
		public int FreeCPUs
		{
			get
			{
				return this._freeCPUs;
			}
			set
			{
				this._freeCPUs = value;
			}
		}
		public long PotentialFreeMemory
		{
			get
			{
				return this._potentialFreeMemory;
			}
			set
			{
				this._potentialFreeMemory = value;
			}
		}
		public int NumHighFullContentionVCpus
		{
			get
			{
				return this._numHighFullContentionVCpus;
			}
			set
			{
				this._numHighFullContentionVCpus = value;
			}
		}
		public int NumHighPartialContentionVCpus
		{
			get
			{
				return this._numHighPartialContentionVCpus;
			}
			set
			{
				this._numHighPartialContentionVCpus = value;
			}
		}
		public int NumHighConcurrencyHazardVCpus
		{
			get
			{
				return this._numHighConcurrencyHazardVCpus;
			}
			set
			{
				this._numHighConcurrencyHazardVCpus = value;
			}
		}
		public int NumHighFullrunVCpus
		{
			get
			{
				return this._numHighFullrunVCpus;
			}
			set
			{
				this._numHighFullrunVCpus = value;
			}
		}
		public int NumHighPartialRunVCpus
		{
			get
			{
				return this._numHighPartialRunVCpus;
			}
			set
			{
				this._numHighPartialRunVCpus = value;
			}
		}
		public int NumHighBlockedVCpus
		{
			get
			{
				return this._numHighBlockedVCpus;
			}
			set
			{
				this._numHighBlockedVCpus = value;
			}
		}
		public DwmAverageMetricHost MetricsNow
		{
			get
			{
				return DwmBase.SafeGetItem<DwmAverageMetricHost>(ref this._metricsNow);
			}
		}
		public DwmAverageMetricHost MetricsLast30Minutes
		{
			get
			{
				return DwmBase.SafeGetItem<DwmAverageMetricHost>(ref this._metricsLast30Minutes);
			}
		}
		public DwmAverageMetricHost MetricsYesterday
		{
			get
			{
				return DwmBase.SafeGetItem<DwmAverageMetricHost>(ref this._metricsYesterday);
			}
		}
		public int FillOrder
		{
			get
			{
				return this._fillOrder;
			}
			set
			{
				this._fillOrder = value;
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
		public double Stars
		{
			get
			{
				return this._stars;
			}
			set
			{
				this._stars = value;
			}
		}
		public List<ZeroScoreReason> ZeroScoreReasons
		{
			get
			{
				return DwmBase.SafeGetItem<List<ZeroScoreReason>>(ref this._zeroScoreReasons);
			}
			internal set
			{
				this._zeroScoreReasons = value;
			}
		}
		public bool CanBootVM
		{
			get
			{
				return this._canBootVM;
			}
			set
			{
				this._canBootVM = value;
			}
		}
		public int MaxPossibleScore
		{
			get
			{
				return this._maxPossibleScore;
			}
			set
			{
				this._maxPossibleScore = value;
			}
		}
		public List<string> Messages
		{
			get
			{
				if (this._messages == null)
				{
					this._messages = new List<string>();
				}
				return this._messages;
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
		public int RecommendationSetId
		{
			get
			{
				return this._recommendationSetId;
			}
			set
			{
				this._recommendationSetId = value;
			}
		}
		public DwmHostAverageMetric Copy()
		{
			return new DwmHostAverageMetric
			{
				FillOrder = this.FillOrder,
				NumVMs = this.NumVMs,
				Score = this.Score,
				TotalMemory = this.TotalMemory,
				MemoryActual = this.MemoryActual,
				ControlMemoryOverhead = this.ControlMemoryOverhead,
				FreeMemory = this.FreeMemory,
				PotentialFreeMemory = this.PotentialFreeMemory,
				NumHighFullContentionVCpus = this.NumHighFullContentionVCpus,
				NumHighConcurrencyHazardVCpus = this.NumHighConcurrencyHazardVCpus,
				NumHighPartialContentionVCpus = this.NumHighPartialContentionVCpus,
				NumHighFullrunVCpus = this.NumHighFullrunVCpus,
				NumHighPartialRunVCpus = this.NumHighPartialRunVCpus,
				NumHighBlockedVCpus = this.NumHighBlockedVCpus,
				MetricsNow = 
				{
					AverageCpuUtilization = this.MetricsNow.AverageCpuUtilization,
					AverageFreeMemory = this.MetricsNow.AverageFreeMemory,
					AveragePbdReadsPerSecond = this.MetricsNow.AveragePbdReadsPerSecond,
					AveragePbdWritesPerSecond = this.MetricsNow.AveragePbdWritesPerSecond,
					AveragePifReadsPerSecond = this.MetricsNow.AveragePifReadsPerSecond,
					AveragePifWritesPerSecond = this.MetricsNow.AveragePifWritesPerSecond,
					EndTime = this.MetricsNow.EndTime,
					StartTime = this.MetricsNow.StartTime
				},
				MetricsLast30Minutes = 
				{
					AverageCpuUtilization = this.MetricsLast30Minutes.AverageCpuUtilization,
					AverageFreeMemory = this.MetricsLast30Minutes.AverageFreeMemory,
					AveragePbdReadsPerSecond = this.MetricsLast30Minutes.AveragePbdReadsPerSecond,
					AveragePbdWritesPerSecond = this.MetricsLast30Minutes.AveragePbdWritesPerSecond,
					AveragePifReadsPerSecond = this.MetricsLast30Minutes.AveragePifReadsPerSecond,
					AveragePifWritesPerSecond = this.MetricsLast30Minutes.AveragePifWritesPerSecond,
					EndTime = this.MetricsLast30Minutes.EndTime,
					StartTime = this.MetricsLast30Minutes.StartTime
				},
				MetricsYesterday = 
				{
					AverageCpuUtilization = this.MetricsYesterday.AverageCpuUtilization,
					AverageFreeMemory = this.MetricsYesterday.AverageFreeMemory,
					AveragePbdReadsPerSecond = this.MetricsYesterday.AveragePbdReadsPerSecond,
					AveragePbdWritesPerSecond = this.MetricsYesterday.AveragePbdWritesPerSecond,
					AveragePifReadsPerSecond = this.MetricsYesterday.AveragePifReadsPerSecond,
					AveragePifWritesPerSecond = this.MetricsYesterday.AveragePifWritesPerSecond,
					EndTime = this.MetricsYesterday.EndTime,
					StartTime = this.MetricsYesterday.StartTime
				}
			};
		}
		public void AddMessage(string message)
		{
			this.Messages.Add(message);
		}
		public void AddMessage(string fmt, params object[] args)
		{
			this.Messages.Add(Localization.Format(fmt, args));
		}
		public static void VerifyValidInstance(ref DwmHostAverageMetric instance)
		{
			if (instance == null)
			{
				instance = new DwmHostAverageMetric();
			}
		}
	}
}
