using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	internal class DwmAEHostScorer
	{
		private MemoryScoringType _memoryScoringType;
		private DwmVirtualMachine _vm;
		private DwmPool _pool;
		private double _maxCpuScore;
		private double _maxCpuOverCommitScore;
		private double _maxMemoryScore;
		private double _maxStorageScore;
		private double _maxNetReadScore;
		private double _maxNetWriteScore;
		private double _maxDiskReadScore;
		private double _maxDiskWriteScore;
		private double _maxLoadAvgScore;
		private double _maxPowerScore;
		private double _maxRunstateScore;
		private bool _getsZeroScore;
		private bool _traceEnabled;
		private bool _isCloud;
		internal DwmAEHostScorer(DwmVirtualMachine vm, DwmPool pool, MemoryScoringType memScoringType)
		{
			this._vm = vm;
			this._pool = pool;
			this._memoryScoringType = memScoringType;
		}
		internal DwmAEHostScorer(DwmVirtualMachine vm, DwmPool pool, MemoryScoringType memScoringType, bool isCloud)
		{
			this._vm = vm;
			this._pool = pool;
			this._memoryScoringType = memScoringType;
			this._isCloud = isCloud;
		}
		internal int ComputeScore(DwmHost host)
		{
			string text = string.Empty;
			this._getsZeroScore = false;
			bool flag = false;
			host.Metrics.ZeroScoreReasons = new List<ZeroScoreReason>();
			this._traceEnabled = Configuration.GetValueAsBool(ConfigItems.ScoreHostTrace);
			if (this._vm == null)
			{
				this._vm = new DwmVirtualMachine(0);
			}
			DwmVmAverageMetric.VerifyValidInstance(this._vm.Metrics, this._pool);
			double num = this.ScoreHostMemory(host);
			double num2 = this.ScoreHostRunState(host);
			double num3 = this.ScoreHostCpu(host);
			double num4 = this.ScoreHostCpuOvercommit(host, out flag);
			if (flag && !this._isCloud)
			{
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.NotEnoughCpus);
				text = Localization.Format("Host has cpu overcommitted.", new object[0]);
				this.Trace(text, host.Name, this._vm.Id);
			}
			double num5 = this.ScoreLoadAverage(host);
			double num6 = this.ScoreHostNetRead(host);
			double num7 = this.ScoreHostNetWrite(host);
			if (!this._isCloud)
			{
				double num8 = this.ScoreHostPower(host);
			}
			double num9 = this.ScoreHostStorage(host);
			double num10;
			double num11;
			if (this._isCloud)
			{
				num10 = this.ScoreHostDiskRead(host);
				num11 = this.ScoreHostDiskWrite(host);
			}
			else
			{
				num10 = 0.0;
				this._maxDiskReadScore = 0.0;
				num11 = 0.0;
				this._maxDiskWriteScore = 0.0;
				text = "Disk score is 0.  Not yet implemented";
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			host.Metrics.VM = this._vm;
			int num12 = (int)(num2 + num3 + num4 + num + num9 + num6 + num7 + num10 + num11 + num5);
			host.Metrics.MaxPossibleScore = (int)(this._maxRunstateScore + this._maxCpuScore + this._maxCpuOverCommitScore + this._maxMemoryScore + this._maxStorageScore + this._maxNetReadScore + this._maxNetWriteScore + this._maxDiskReadScore + this._maxDiskWriteScore + this._maxLoadAvgScore);
			text = Localization.Format("Score for host is {0}/{1}", num12, host.Metrics.MaxPossibleScore);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			if (this._getsZeroScore)
			{
				if (this._isCloud && host.Metrics.ZeroScoreReasons.Contains(ZeroScoreReason.NotEnoughCpus))
				{
					text = Localization.Format("Not enought physical CPU to support VM.  Forcing score to 0.", new object[0]);
				}
				else
				{
					text = Localization.Format("Critical threshold exceeded.  Forcing score to 0.", new object[0]);
				}
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
				num12 = 0;
			}
			if (this._pool.OptMode == OptimizationMode.MaxDensity)
			{
				num12 -= host.Metrics.FillOrder * 10;
				text = Localization.Format("Adjusted score for Max Density by accounting for fill order ({0})", -host.Metrics.FillOrder * 10);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
				host.Metrics.MaxPossibleScore -= 10;
				text = Localization.Format("New score is {0}/{1}", num12, host.Metrics.MaxPossibleScore);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			return num12;
		}
		private double ScoreHostRunState(DwmHost host)
		{
			double num = 0.0;
			string text = Localization.Format("Host has {0} vCpus exceeding the pool's RunstateFullContentionHigh threshold\nHost has {1} vCpus exceeding the pool's RunstateConcurrencyHazardHigh threshold\nHost has {2} vCpus exceeding the pool's RunstatePartialContentionHigh threshold\nHost has {3} vCpus exceeding the pool's RunstateFullRunHigh threshold\nHost has {4} vCpus exceeding the pool's RunstatePartialRunHigh threshold\nHost has {5} vCpus exceeding the pool's RunstateBlockedHigh threshold", new object[]
			{
				host.Metrics.NumHighFullContentionVCpus,
				host.Metrics.NumHighConcurrencyHazardVCpus,
				host.Metrics.NumHighPartialContentionVCpus,
				host.Metrics.NumHighFullrunVCpus,
				host.Metrics.NumHighPartialRunVCpus,
				host.Metrics.NumHighBlockedVCpus
			});
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			if (host.Metrics.NumHighFullContentionVCpus > 0)
			{
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.RunstateFullContention);
				text = Localization.Format("Host score is being forced to zero because of {0} vCpus exceeding the RunstateFullContentionHigh threshold.", host.Metrics.NumHighFullContentionVCpus);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			else
			{
				if (host.Metrics.NumHighConcurrencyHazardVCpus > 0)
				{
					text = Localization.Format("Host is receiving a zero Runstate score because of {0} vCpus exceeding the RunstateConcurrencyHazardHigh threshold.", host.Metrics.NumHighConcurrencyHazardVCpus);
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
				}
				else
				{
					if (host.Metrics.NumHighPartialContentionVCpus > 0)
					{
						text = Localization.Format("Host is receiving a zero Runstate score because of {0} vCpus exceeding the RunstatePartialContentionHigh threshold.", host.Metrics.NumHighPartialContentionVCpus);
						this.Trace(text, host.Name, this._vm.Id);
						host.Metrics.AddMessage(text);
					}
					else
					{
						num = ((double)(host.NumCpus - host.Metrics.NumHighFullrunVCpus) - 0.5 * (double)host.Metrics.NumHighPartialRunVCpus) / (double)host.NumCpus * 100.0;
						text = Localization.Format("The unweighted RunstateScore is {0}", num);
						this.Trace(text, host.Name, this._vm.Id);
						host.Metrics.AddMessage(text);
						num *= this._vm.Metrics.RunstateWeight;
						text = Localization.Format("The weighted RunstateScore is {0}", num);
						this.Trace(text, host.Name, this._vm.Id);
						host.Metrics.AddMessage(text);
					}
				}
			}
			if (num < 0.0)
			{
				num = 0.0;
			}
			this._maxRunstateScore = 100.0 * this._vm.Metrics.RunstateWeight;
			return num;
		}
		private double ScoreHostCpu(DwmHost host)
		{
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = (double)this._vm.MinimumCpus / (double)host.NumCpus;
			double num6 = this._vm.Metrics.MetricsNow.AverageCpuUtilization * num5;
			double num7 = (double)(host.NumCpus * host.CpuSpeed);
			string text = string.Format("CPU rating is {0}", num7);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			text = string.Format("Max CPU rating for the pool is {0}", this._pool.MaxCpuRating);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			if (host.Metrics.MetricsNow.AverageCpuUtilization + num6 > this._pool.HostCpuThreshold.Critical)
			{
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.Cpu);
				text = string.Format("CPU threshold would be exceeded {0} ({1}).  Score will be forced to 0.", host.Metrics.MetricsNow.AverageCpuUtilization + num6, this._pool.HostCpuThreshold.Critical);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			else
			{
				if (DwmAEHost.PoolMasterExceedsCpuLimit(host, this._vm, this._pool))
				{
					num4 = 0.0;
					text = string.Format("CPU threshold of the pool master would be exceeded {0} ({1}).  CPU score set to 0.", host.Metrics.MetricsNow.AverageCpuUtilization + num6, this._pool.PoolMasterCpuLimit);
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
				}
				else
				{
					num = num7 * (1.0 - host.Metrics.MetricsNow.AverageCpuUtilization - this._vm.Metrics.MetricsNow.AverageCpuUtilization * num5);
					num2 = num7 * (1.0 - host.Metrics.MetricsLast30Minutes.AverageCpuUtilization - this._vm.Metrics.MetricsLast30Minutes.AverageCpuUtilization * num5);
					num3 = num7 * (1.0 - host.Metrics.MetricsYesterday.AverageCpuUtilization - this._vm.Metrics.MetricsYesterday.AverageCpuUtilization * num5);
					num4 = num * this._pool.WeightCurrentMetrics + num2 * this._pool.WeightRecentMetrics + num3 * this._pool.WeightHistoricMetrics;
				}
			}
			text = Localization.Format("Raw CPU score is {0} ({1} {2} {3})", new object[]
			{
				(int)num4,
				(int)num,
				(int)num2,
				(int)num3
			});
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			num4 = ((num4 <= 0.0) ? 0.0 : num4);
			num4 = num4 / (double)this._pool.MaxCpuRating * 100.0;
			text = Localization.Format("CPU score before weighting with factor of {0} is {1}.", this._vm.Metrics.CpuWeight, (int)num4);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			num4 *= this._vm.Metrics.CpuWeight;
			text = Localization.Format("Final CPU score is {0}.", (int)num4);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			this._maxCpuScore = 100.0 * (this._pool.WeightCurrentMetrics + this._pool.WeightRecentMetrics + this._pool.WeightHistoricMetrics) * this._vm.Metrics.CpuWeight;
			text = Localization.Format("Max possible CPU score is {0}.", this._maxCpuScore);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			return num4;
		}
		private double ScoreHostCpuOvercommit(DwmHost host, out bool cpusOverCommitted)
		{
			double num = 0.0;
			this._maxCpuOverCommitScore = 0.0;
			cpusOverCommitted = false;
			bool flag = (this._pool.OptMode != OptimizationMode.MaxPerformance) ? this._pool.OverCommitCpusInDensityMode : this._pool.OverCommitCpusInPerfMode;
			if (!flag || (flag && this._isCloud))
			{
				string text;
				if (!flag)
				{
					text = Localization.Format("Free CPUs = {0}", host.Metrics.FreeCPUs);
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
					if (host.Metrics.FreeCPUs > 0)
					{
						num = (double)(host.Metrics.FreeCPUs * 10);
					}
					else
					{
						cpusOverCommitted = true;
					}
					this._maxCpuOverCommitScore = (double)(host.NumCpus * 10) * this._pool.WeightCurrentMetrics * this._vm.Metrics.CpuWeight;
				}
				else
				{
					text = Localization.Format("Free CPUs = {0}, Over Commit CPU Ratio = {1}:1", host.Metrics.FreeCPUs, this._pool.OverCommitCpuRatio.ToString());
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
					num = this.ScoreOverCommitCpu(host);
				}
				text = Localization.Format("Raw overcommit CPU score is {0}", num);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
				num *= this._vm.Metrics.CpuWeight * this._pool.WeightCurrentMetrics;
				text = Localization.Format("Final overcommit CPU score is {0}.", (int)num);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
				text = Localization.Format("Max possible CPU score is {0}.", this._maxCpuOverCommitScore);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			return num;
		}
		private double ScoreOverCommitCpu(DwmHost host)
		{
			double result = 0.0;
			double num = (double)((host.NumVCpus + this._vm.MinimumCpus) / host.NumCpus);
			if (this._isCloud && num > (double)this._pool.OverCommitCpuRatio)
			{
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.NotEnoughCpus);
				return result;
			}
			if (this._pool.OverCommitCpuRatio == 1 || (num < 1.0 && this._pool.OverCommitCpuRatio > 1))
			{
				result = (double)(host.Metrics.FreeCPUs * 10);
				this._maxCpuOverCommitScore = (double)(host.NumCpus * 10) * this._pool.WeightCurrentMetrics * this._vm.Metrics.CpuWeight;
			}
			else
			{
				if (this._pool.OverCommitCpuRatio > 1)
				{
					double num2 = num / (double)this._pool.OverCommitCpuRatio;
					if (num2 <= 0.25)
					{
						result = 7.5;
					}
					else
					{
						if (num2 <= 0.5)
						{
							result = 5.0;
						}
						else
						{
							if (num2 <= 0.75)
							{
								result = 2.5;
							}
							else
							{
								if (num2 <= 1.0)
								{
									result = 0.0;
								}
							}
						}
					}
					this._maxCpuOverCommitScore = 10.0 * this._pool.WeightCurrentMetrics * this._vm.Metrics.CpuWeight;
				}
			}
			return result;
		}
		private double ScoreHostMemory(DwmHost host)
		{
			double num = 0.0;
			long num2 = (this._vm.RequiredMemory <= 0L) ? (this._vm.MaximumDynamicMemory + this._vm.MemoryOverhead) : this._vm.RequiredMemory;
			string text;
			if (this._memoryScoringType == MemoryScoringType.StaticMax || this._memoryScoringType == MemoryScoringType.DynamicMax)
			{
				if ((double)(host.Metrics.FreeMemory - num2) <= this._pool.HostMemoryThreshold.Critical)
				{
					this._getsZeroScore = true;
					host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.Memory);
					text = Localization.Format("Memory threshold would be exceeded {0} ({1}).  Score will be forced to 0.", host.Metrics.FreeMemory - num2, this._pool.HostMemoryThreshold.Critical);
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
				}
				else
				{
					if (host.Metrics.FreeMemory > 0L)
					{
						num = (1.0 - (double)num2 / (double)host.Metrics.FreeMemory) * 100.0;
						num = ((num <= 0.0) ? 0.0 : num);
						if (this._memoryScoringType == MemoryScoringType.StaticMax && host.Metrics.FreeMemory > this._vm.MaximumStaticMemory + this._vm.MemoryOverhead)
						{
							num *= 1.2;
							num = ((num > 100.0) ? 100.0 : num);
						}
						text = Localization.Format("Memory score before weighting with factor of {0} is {1}", this._vm.Metrics.MemoryWeight, num);
						this.Trace(text, host.Name, this._vm.Id);
						host.Metrics.AddMessage(text);
						num *= this._vm.Metrics.MemoryWeight;
					}
				}
			}
			else
			{
				if (host.Metrics.PotentialFreeMemory >= this._vm.MaximumDynamicMemory + this._vm.MemoryOverhead)
				{
					num = 100.0;
				}
				else
				{
					if (host.Metrics.PotentialFreeMemory < this._vm.MinimumDynamicMemory + this._vm.MemoryOverhead)
					{
						this._getsZeroScore = true;
						host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.Memory);
						num = 0.0;
					}
					else
					{
						num = (double)host.Metrics.PotentialFreeMemory / (double)num2 * 100.0;
					}
				}
			}
			text = Localization.Format("Final memory score is {0}.", (int)num);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			this._maxMemoryScore = 100.0 * this._vm.Metrics.MemoryWeight;
			text = Localization.Format("Max possible memory score is {0}", this._maxMemoryScore);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			return num;
		}
		private double ScoreHostNetRead(DwmHost host)
		{
			double num = 0.0;
			string text;
			if (host.Metrics.MetricsNow.AveragePifReadsPerSecond + this._vm.Metrics.MetricsNow.AveragePifReadsPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond >= this._pool.HostPifReadThreshold.Critical)
			{
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.NetworkRead);
				text = Localization.Format("Network read threshold would be exceeded {0} ({1}).  Score will be forced to 0.", host.Metrics.MetricsNow.AveragePifReadsPerSecond + this._vm.Metrics.MetricsNow.AveragePifReadsPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond, this._pool.HostPifReadThreshold.Critical);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			else
			{
				if (DwmAEHost.PoolMasterExceedsNetIoLimit(host, this._vm, this._pool))
				{
					num = 0.0;
					text = Localization.Format("Network IO threshold for pool master would be exceeded {0} ({1}).  Net read score will be set to 0", host.Metrics.MetricsNow.AveragePifReadsPerSecond + host.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.AveragePifReadsPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond + this._vm.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond, this._pool.PoolMasterNetIoLimit);
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
				}
				else
				{
					double num2 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsNow.AveragePifReadsPerSecond, this._vm.Metrics.MetricsNow.AveragePifReadsPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond, this._pool.HostPifReadThreshold);
					double num3 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsLast30Minutes.AveragePifReadsPerSecond, this._vm.Metrics.MetricsLast30Minutes.AveragePifReadsPerSecond + this._vm.Metrics.MetricsLast30Minutes.TotalVbdNetReadsPerSecond, this._pool.HostPifReadThreshold);
					double num4 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsYesterday.AveragePifReadsPerSecond, this._vm.Metrics.MetricsYesterday.AveragePifReadsPerSecond + this._vm.Metrics.MetricsYesterday.TotalVbdNetReadsPerSecond, this._pool.HostPifReadThreshold);
					num = num2 * this._pool.WeightCurrentMetrics + num3 * this._pool.WeightRecentMetrics + num4 * this._pool.WeightHistoricMetrics;
					text = Localization.Format("Net read score before weighting with factor of {0} is {1} ({2} {3} {4})", new object[]
					{
						this._vm.Metrics.PifReadWeight,
						num,
						num2,
						num3,
						num4
					});
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
					num *= this._vm.Metrics.PifReadWeight;
				}
			}
			text = Localization.Format("Final network read score is {0}.", (int)num);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			this._maxNetReadScore = 50.0 * (this._pool.WeightCurrentMetrics + this._pool.WeightRecentMetrics + this._pool.WeightHistoricMetrics) * this._vm.Metrics.PifReadWeight;
			text = Localization.Format("Max possible net read score is {0}", this._maxNetReadScore);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			return num;
		}
		private double ScoreHostNetWrite(DwmHost host)
		{
			double num = 0.0;
			string text;
			if (host.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetWritesPerSecond >= this._pool.HostPifWriteThreshold.Critical)
			{
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.NetworkWrite);
				text = Localization.Format("Network write threshold would be exceeded {0} ({1}).  Score will be forced to 0.", host.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetWritesPerSecond, this._pool.HostPifWriteThreshold.Critical);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			else
			{
				if (DwmAEHost.PoolMasterExceedsNetIoLimit(host, this._vm, this._pool))
				{
					num = 0.0;
					text = Localization.Format("Network IO threshold for pool master would be exceeded {0} ({1}).  Net write score will be set to 0", host.Metrics.MetricsNow.AveragePifReadsPerSecond + host.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.AveragePifReadsPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond + this._vm.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond, this._pool.PoolMasterNetIoLimit);
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
				}
				else
				{
					double num2 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsNow.AveragePifWritesPerSecond, this._vm.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetWritesPerSecond, this._pool.HostPifWriteThreshold);
					double num3 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsLast30Minutes.AveragePifWritesPerSecond, this._vm.Metrics.MetricsLast30Minutes.AveragePifWritesPerSecond + this._vm.Metrics.MetricsLast30Minutes.TotalVbdNetWritesPerSecond, this._pool.HostPifWriteThreshold);
					double num4 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsYesterday.AveragePifWritesPerSecond, this._vm.Metrics.MetricsYesterday.AveragePifWritesPerSecond + this._vm.Metrics.MetricsYesterday.TotalVbdNetWritesPerSecond, this._pool.HostPifWriteThreshold);
					num = num2 * this._pool.WeightCurrentMetrics + num3 * this._pool.WeightRecentMetrics + num4 * this._pool.WeightHistoricMetrics;
					text = Localization.Format("Net write score before weighting with factor of {0} is {1} ({2} {3} {4})", new object[]
					{
						this._vm.Metrics.PifWriteWeight,
						num,
						num2,
						num3,
						num4
					});
					this.Trace(text, host.Name, this._vm.Id);
					host.Metrics.AddMessage(text);
					num *= this._vm.Metrics.PifWriteWeight;
				}
			}
			text = Localization.Format("Final network write score is {0}.", (int)num);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			this._maxNetWriteScore = 50.0 * (this._pool.WeightCurrentMetrics + this._pool.WeightRecentMetrics + this._pool.WeightHistoricMetrics) * this._vm.Metrics.PifWriteWeight;
			text = Localization.Format("Max possible net write score is {0}", this._maxNetWriteScore);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			return num;
		}
		private double ScoreHostDiskRead(DwmHost host)
		{
			double num = 0.0;
			string text;
			if (host.Metrics.MetricsNow.AveragePbdReadsPerSecond + this._vm.Metrics.MetricsNow.AveragePbdReadsPerSecond >= this._pool.HostPbdReadThreshold.Critical)
			{
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.NetworkRead);
				text = Localization.Format("Disk read threshold would be exceeded {0} ({1}).  Score will be forced to 0.", host.Metrics.MetricsNow.AveragePbdReadsPerSecond + this._vm.Metrics.MetricsNow.AveragePbdReadsPerSecond, this._pool.HostPbdReadThreshold.Critical);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			else
			{
				double num2 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsNow.AveragePbdReadsPerSecond, 0.0, this._pool.HostPbdReadThreshold);
				double num3 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsLast30Minutes.AveragePbdReadsPerSecond, 0.0, this._pool.HostPbdReadThreshold);
				double num4 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsYesterday.AveragePbdReadsPerSecond, 0.0, this._pool.HostPbdReadThreshold);
				num = num2 * this._pool.WeightCurrentMetrics + num3 * this._pool.WeightRecentMetrics + num4 * this._pool.WeightHistoricMetrics;
				text = Localization.Format("Disk read score before weighting with factor of {0} is {1} ({2} {3} {4})", new object[]
				{
					this._vm.Metrics.PbdReadWeight,
					num,
					num2,
					num3,
					num4
				});
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
				num *= this._vm.Metrics.PbdReadWeight;
			}
			text = Localization.Format("Final disk read score is {0}.", (int)num);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			this._maxDiskReadScore = 50.0 * (this._pool.WeightCurrentMetrics + this._pool.WeightRecentMetrics + this._pool.WeightHistoricMetrics) * this._vm.Metrics.PbdReadWeight;
			text = Localization.Format("Max possible disk read score is {0}", this._maxDiskReadScore);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			return num;
		}
		private double ScoreHostDiskWrite(DwmHost host)
		{
			double num = 0.0;
			string text;
			if (host.Metrics.MetricsNow.AveragePbdWritesPerSecond + this._vm.Metrics.MetricsNow.AveragePbdWritesPerSecond >= this._pool.HostPbdWriteThreshold.Critical)
			{
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.NetworkWrite);
				text = Localization.Format("Disk write threshold would be exceeded {0} ({1}).  Score will be forced to 0.", host.Metrics.MetricsNow.AveragePbdWritesPerSecond + this._vm.Metrics.MetricsNow.AveragePbdWritesPerSecond, this._pool.HostPbdWriteThreshold.Critical);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			else
			{
				double num2 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsNow.AveragePbdWritesPerSecond, this._vm.Metrics.MetricsNow.AveragePifWritesPerSecond + this._vm.Metrics.MetricsNow.TotalVbdNetWritesPerSecond, this._pool.HostPbdWriteThreshold);
				double num3 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsLast30Minutes.AveragePbdWritesPerSecond, this._vm.Metrics.MetricsLast30Minutes.AveragePbdWritesPerSecond, this._pool.HostPbdWriteThreshold);
				double num4 = DwmAEHostScorer.ScoreUseHostThreshold(host.Metrics.MetricsYesterday.AveragePbdWritesPerSecond, this._vm.Metrics.MetricsYesterday.AveragePbdWritesPerSecond, this._pool.HostPbdWriteThreshold);
				num = num2 * this._pool.WeightCurrentMetrics + num3 * this._pool.WeightRecentMetrics + num4 * this._pool.WeightHistoricMetrics;
				text = Localization.Format("Disk write score before weighting with factor of {0} is {1} ({2} {3} {4})", new object[]
				{
					this._vm.Metrics.PbdWriteWeight,
					num,
					num2,
					num3,
					num4
				});
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
				num *= this._vm.Metrics.PbdWriteWeight;
			}
			text = Localization.Format("Final disk write score is {0}.", (int)num);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			this._maxDiskWriteScore = 50.0 * (this._pool.WeightCurrentMetrics + this._pool.WeightRecentMetrics + this._pool.WeightHistoricMetrics) * this._vm.Metrics.PbdWriteWeight;
			text = Localization.Format("Max possible disk write score is {0}", this._maxDiskWriteScore);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			return num;
		}
		private static double ScoreUseHostThreshold(double hostMetric, double vmMetric, HostThreshold hostThreshold)
		{
			double result = 0.0;
			double num = hostMetric + vmMetric;
			if (num <= hostThreshold.Low)
			{
				result = 50.0;
			}
			else
			{
				if (num <= hostThreshold.Medium)
				{
					result = 25.0;
				}
				else
				{
					if (num <= hostThreshold.High)
					{
						result = 5.0;
					}
				}
			}
			return result;
		}
		private double ScoreLoadAverage(DwmHost host)
		{
			this._maxLoadAvgScore = 100.0;
			double num = DwmAEHostScorer.InternalScoreLoadAve(host.Metrics.MetricsNow.AverageLoadAverage, 0.0, this._pool.HostLoadAverageThreshold);
			double num4;
			if (num > 0.0)
			{
				double num2 = DwmAEHostScorer.InternalScoreLoadAve(host.Metrics.MetricsLast30Minutes.AverageLoadAverage, 0.0, this._pool.HostLoadAverageThreshold);
				double num3 = DwmAEHostScorer.InternalScoreLoadAve(host.Metrics.MetricsYesterday.AverageLoadAverage, 0.0, this._pool.HostLoadAverageThreshold);
				string text = Localization.Format("Load average is {0} {1} {2}", new object[]
				{
					host.Metrics.MetricsNow.AverageLoadAverage,
					host.Metrics.MetricsLast30Minutes.AverageLoadAverage,
					host.Metrics.MetricsYesterday.AverageLoadAverage
				});
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
				num4 = (num * this._pool.WeightCurrentMetrics + num2 * this._pool.WeightRecentMetrics + num3 * this._pool.WeightHistoricMetrics) / (this._pool.WeightCurrentMetrics + this._pool.WeightRecentMetrics + this._pool.WeightHistoricMetrics);
				text = Localization.Format("Load average score is {0} ({1} {2} {3})", new object[]
				{
					num4,
					num,
					num2,
					num3
				});
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
				text = Localization.Format("Max possible Load average score is {0}", this._maxLoadAvgScore);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			else
			{
				num4 = 0.0;
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.LoadAverage);
				string text = Localization.Format("LoadAverage threshold is exceeded {0} ({1}).  Score will be forced to 0.", host.Metrics.MetricsNow.AverageLoadAverage, this._pool.HostLoadAverageThreshold.Critical);
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			return num4;
		}
		private static double InternalScoreLoadAve(double hostMetric, double vmMetric, HostThreshold hostThreshold)
		{
			double result;
			if (hostMetric <= hostThreshold.Low)
			{
				result = 100.0;
			}
			else
			{
				if (hostMetric <= hostThreshold.Medium)
				{
					result = 50.0;
				}
				else
				{
					if (hostMetric < hostThreshold.High)
					{
						result = 10.0;
					}
					else
					{
						if (hostMetric < hostThreshold.Critical)
						{
							result = 5.0;
						}
						else
						{
							result = 0.0;
						}
					}
				}
			}
			return result;
		}
		private double ScoreHostPower(DwmHost host)
		{
			double num;
			string text;
			if (this._vm.IsAgile)
			{
				if (host.ParticipatesInPowerManagement)
				{
					if (this._pool.AutoBalance)
					{
						if (this._pool.ManagePower)
						{
							num = 50.0;
							text = Localization.Format("Raw power score is {0} because VM is agile, host participates, auto balance and power management is on", num);
						}
						else
						{
							num = 37.5;
							text = Localization.Format("Raw power score is {0} because VM is agile, host participates, auto balance is on but power management off", num);
						}
					}
					else
					{
						num = 25.0;
						text = Localization.Format("Raw power score is {0} because VM is agile, host participates but auto balance off", num);
					}
				}
				else
				{
					if (this._pool.AutoBalance)
					{
						if (this._pool.ManagePower)
						{
							num = 0.0;
							text = Localization.Format("Raw power score is {0} because VM is agile, auto balance and power management are on but does not host participates", num);
						}
						else
						{
							num = 12.5;
							text = Localization.Format("Raw power score is {0} because VM is agile, auto balance is on, power management is off and does not host participates", num);
						}
					}
					else
					{
						num = 25.0;
						text = Localization.Format("Raw power score is {0} because VM is agile, host participates but auto balance off", num);
					}
				}
			}
			else
			{
				if (!host.ParticipatesInPowerManagement)
				{
					if (this._pool.AutoBalance)
					{
						if (this._pool.ManagePower)
						{
							num = 50.0;
							text = Localization.Format("Raw power score is {0} because VM is not agile and host does not participates but auto balance and power management are on", num);
						}
						else
						{
							num = 45.0;
							text = Localization.Format("Raw power score is {0} because VM is not agile and host does not participates but auto balance is on and power management if off", num);
						}
					}
					else
					{
						num = 40.0;
						text = Localization.Format("Raw power score is {0} because VM is not agile and host does not participates and auto balance is off", num);
					}
				}
				else
				{
					if (this._pool.AutoBalance)
					{
						if (this._pool.ManagePower)
						{
							num = 0.0;
							text = Localization.Format("Raw power score is {0} because VM is not agile and host participates, auto balance is on and power management is on", num);
						}
						else
						{
							num = 10.0;
							text = Localization.Format("Raw power score is {0} because VM is not agile and host participates, auto balance is on and power management if off", num);
						}
					}
					else
					{
						num = 20.0;
						text = Localization.Format("Raw power score is {0} because VM is not agile and host participates and auto balance is off", num);
					}
				}
			}
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			text = Localization.Format("Power score before weighting with factor of {0} is {1})", this._vm.Metrics.PowerManagementWeight, num);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			num *= this._vm.Metrics.PowerManagementWeight;
			text = Localization.Format("Final power score is {0}.", (int)num);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			this._maxPowerScore = 50.0 * this._vm.Metrics.PowerManagementWeight;
			text = Localization.Format("Max possible power score is {0}", this._maxPowerScore);
			this.Trace(text, host.Name, this._vm.Id);
			host.Metrics.AddMessage(text);
			return num;
		}
		private double ScoreHostStorage(DwmHost host)
		{
			double result;
			if ((this._isCloud && host.AvailableStorage != null && host.AvailableStorage.Count > 0 && host.AvailableStorage[0].Size - host.AvailableStorage[0].Used > this._vm.RequiredStorage[0].Size) || (!this._isCloud && host.HasRequiredStorage(this._vm.RequiredStorage)))
			{
				result = 100.0;
			}
			else
			{
				result = 0.0;
				this._getsZeroScore = true;
				host.Metrics.ZeroScoreReasons.Add(ZeroScoreReason.VmRequiresSr);
				string text = "Host does not have the SRs reqyired by the VM";
				this.Trace(text, host.Name, this._vm.Id);
				host.Metrics.AddMessage(text);
			}
			this._maxStorageScore = 100.0;
			return result;
		}
		private void Trace(string msg, string hostName, int vmId)
		{
			if (this._traceEnabled)
			{
				Logger.Trace("Scoring host {0} for VM {1} - {2}", new object[]
				{
					hostName,
					vmId,
					msg
				});
			}
		}
	}
}
