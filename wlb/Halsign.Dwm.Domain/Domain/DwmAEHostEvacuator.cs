using Halsign.DWM.Collectors;
using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	public class DwmAEHostEvacuator
	{
		private DwmVirtualMachineCollection _vms;
		private DwmHostCollection _hosts;
		private DwmHostCollection _hostSnapShot;
		private DwmPool _pool;
		private List<DwmHostVM> _hostVMs;
		private bool _attemptedPowerOn;
		private MemoryScoringType _memScoringType;
		private List<DwmHostVM> Recommendations
		{
			get
			{
				if (this._hostVMs == null)
				{
					this._hostVMs = new List<DwmHostVM>();
				}
				return this._hostVMs;
			}
		}
		internal DwmAEHostEvacuator(DwmVirtualMachineCollection vms, DwmHostCollection hosts, DwmPool pool)
		{
			this._vms = vms;
			this._hosts = hosts;
			this._pool = pool;
		}
		internal bool Evacuate(out List<DwmHostVM> hostVMs)
		{
			hostVMs = null;
			bool result = this.PlaceVMs();
			hostVMs = this.Recommendations;
			return result;
		}
		private bool PlaceVMs()
		{
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			if (this._vms.Count > 0)
			{
				this._hostSnapShot = this._hosts.Copy();
				this._vms.Sort(new Comparison<DwmVirtualMachine>(DwmAEVirtualMachine.CompareVmMemory));
				while (!flag2 && num < 4)
				{
					switch (num)
					{
					case 0:
						this._memScoringType = MemoryScoringType.DynamicMax;
						flag = this.InternalPlaceVMs();
						num++;
						flag2 = flag;
						break;
					case 1:
						if (this._pool.PreferPowerOnOverCompression)
						{
							if (!this._attemptedPowerOn && this.FindHostToPowerOn())
							{
								num = 0;
							}
							else
							{
								num++;
							}
						}
						else
						{
							num++;
						}
						break;
					case 2:
						this._memScoringType = MemoryScoringType.VmCurrent;
						flag = this.InternalPlaceVMs();
						num++;
						flag2 = flag;
						break;
					case 3:
						this._memScoringType = MemoryScoringType.PotentialFree;
						flag = this.InternalPlaceVMs();
						if (!flag)
						{
							if (!this._attemptedPowerOn && this.FindHostToPowerOn())
							{
								num = 0;
							}
							else
							{
								num++;
							}
						}
						flag2 = flag;
						break;
					}
				}
			}
			return flag;
		}
		private bool InternalPlaceVMs()
		{
			bool result = true;
			for (int i = 0; i < this._vms.Count; i++)
			{
				DwmVirtualMachine dwmVirtualMachine = this._vms[i];
				DwmHost dwmHost = null;
				bool flag = false;
				if (dwmVirtualMachine.IsAgile && (dwmVirtualMachine.Flags & 1) == 0)
				{
					if (this._memScoringType == MemoryScoringType.VmCurrent)
					{
						if (dwmVirtualMachine.Metrics.TotalMemory > 0L)
						{
							dwmVirtualMachine.RequiredMemory = dwmVirtualMachine.Metrics.TotalMemory;
						}
					}
					else
					{
						if (this._memScoringType == MemoryScoringType.PotentialFree)
						{
							if (dwmVirtualMachine.MinimumDynamicMemory > 0L && this._pool.OptMode == OptimizationMode.MaxDensity)
							{
								dwmVirtualMachine.RequiredMemory = dwmVirtualMachine.MinimumDynamicMemory + dwmVirtualMachine.MemoryOverhead;
							}
							else
							{
								if (dwmVirtualMachine.Metrics.TotalMemory > 0L)
								{
									dwmVirtualMachine.RequiredMemory = dwmVirtualMachine.Metrics.TotalMemory;
								}
							}
						}
					}
					if (this._pool.OptMode == OptimizationMode.MaxPerformance)
					{
						DwmAEHostScorer dwmAEHostScorer = new DwmAEHostScorer(dwmVirtualMachine, this._pool, this._memScoringType);
						int num = 0;
						while (!flag && num < this._hosts.Count)
						{
							dwmHost = this._hosts[num];
							dwmHost.Metrics.Score = dwmAEHostScorer.ComputeScore(dwmHost);
							num++;
						}
						this._hosts.Sort(new Comparison<DwmHost>(DwmAEHost.CompareHostMetrics));
					}
					bool[] expr_155 = new bool[4];
					expr_155[0] = true;
					bool[] array = expr_155;
					bool[] array2 = new bool[]
					{
						true,
						true,
						true,
						false
					};
					bool[] expr_174 = new bool[4];
					expr_174[0] = true;
					expr_174[1] = true;
					bool[] array3 = expr_174;
					int num2 = 0;
					while (!flag && num2 < 4)
					{
						int num3 = 0;
						while (!flag && num3 < this._hosts.Count)
						{
							dwmHost = this._hosts[num3];
							flag = this.TryPlaceVmOnHost(dwmVirtualMachine, dwmHost, array[num2], array2[num2], array3[num2]);
							num3++;
						}
						num2++;
					}
					if (flag)
					{
						this.PlaceVmOnHost(dwmVirtualMachine, dwmHost);
						dwmVirtualMachine.Flags = 1;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}
		private bool TryPlaceVmOnHost(DwmVirtualMachine vm, DwmHost host, bool usePower, bool useCurrentMetrics, bool use30MinuteMetrics)
		{
			bool result = false;
			double num = (!useCurrentMetrics && !use30MinuteMetrics) ? 0.0 : this._pool.HostMemoryThreshold.Critical;
			int num2 = (!useCurrentMetrics && !use30MinuteMetrics) ? 0 : vm.MinimumCpus;
			double num3;
			double num4;
			double num5;
			double num6;
			double num7;
			double num8;
			if (this._pool.OptMode == OptimizationMode.MaxPerformance)
			{
				num3 = this._pool.HostCpuThreshold.High;
				num4 = this._pool.HostPifReadThreshold.High;
				num5 = this._pool.HostPifWriteThreshold.High;
				num6 = this._pool.HostPbdReadThreshold.High;
				num7 = this._pool.HostPbdWriteThreshold.High;
				num8 = this._pool.HostLoadAverageThreshold.High;
			}
			else
			{
				num3 = this._pool.HostCpuThreshold.Critical;
				num4 = this._pool.HostPifReadThreshold.Critical;
				num5 = this._pool.HostPifWriteThreshold.Critical;
				num6 = this._pool.HostPbdReadThreshold.Critical;
				num7 = this._pool.HostPbdWriteThreshold.Critical;
				num8 = this._pool.HostLoadAverageThreshold.Critical;
			}
			double num9 = (double)vm.RequiredMemory;
			long num10;
			if (this._memScoringType != MemoryScoringType.PotentialFree)
			{
				num10 = host.Metrics.FreeMemory;
			}
			else
			{
				num10 = host.Metrics.PotentialFreeMemory;
			}
			if (host.NumCpus >= num2 && (double)num10 - num9 >= num && host.HasRequiredStorage(vm.RequiredStorage) && (!usePower || (vm.IsAgile && host.ParticipatesInPowerManagement) || (!vm.IsAgile && !host.ParticipatesInPowerManagement)))
			{
				double num11 = vm.Metrics.MetricsNow.AverageCpuUtilization * ((double)vm.MinimumCpus / (double)host.NumCpus);
				if (!useCurrentMetrics || (!DwmAEHost.PoolMasterExceedsLimits(host, vm, this._pool) && host.Metrics.MetricsNow.AverageCpuUtilization + num11 < num3 && host.Metrics.MetricsNow.AveragePifReadsPerSecond + vm.Metrics.MetricsNow.AveragePifReadsPerSecond + vm.Metrics.MetricsNow.TotalVbdNetReadsPerSecond < num4 && host.Metrics.MetricsNow.AveragePifWritesPerSecond + vm.Metrics.MetricsNow.AveragePifWritesPerSecond + vm.Metrics.MetricsNow.TotalVbdNetWritesPerSecond < num5 && host.Metrics.MetricsNow.AveragePbdReadsPerSecond + vm.Metrics.MetricsNow.AveragePbdReadsPerSecond < num6 && host.Metrics.MetricsNow.AveragePbdWritesPerSecond + vm.Metrics.MetricsNow.AveragePbdWritesPerSecond < num7 && host.Metrics.MetricsNow.AverageLoadAverage < num8 && host.Metrics.NumHighFullContentionVCpus == 0 && host.Metrics.NumHighConcurrencyHazardVCpus == 0))
				{
					double num12 = vm.Metrics.MetricsLast30Minutes.AverageCpuUtilization * ((double)vm.MinimumCpus / (double)host.NumCpus);
					if (!use30MinuteMetrics || (host.Metrics.MetricsLast30Minutes.AverageCpuUtilization + num12 < num3 && host.Metrics.MetricsLast30Minutes.AveragePifReadsPerSecond + vm.Metrics.MetricsLast30Minutes.AveragePifReadsPerSecond + vm.Metrics.MetricsLast30Minutes.TotalVbdNetReadsPerSecond < num4 && host.Metrics.MetricsLast30Minutes.AveragePifWritesPerSecond + vm.Metrics.MetricsLast30Minutes.AveragePifWritesPerSecond + vm.Metrics.MetricsLast30Minutes.TotalVbdNetWritesPerSecond < num5 && host.Metrics.MetricsLast30Minutes.AveragePbdReadsPerSecond + vm.Metrics.MetricsLast30Minutes.AveragePbdReadsPerSecond < num6 && host.Metrics.MetricsLast30Minutes.AveragePbdWritesPerSecond + vm.Metrics.MetricsLast30Minutes.AveragePbdWritesPerSecond < num7))
					{
						if (host.PowerState == PowerStatus.On)
						{
							ICollectorActions collector = DwmPool.GetCollector(this._pool.Id);
							if (collector != null && collector.CanStartVM(vm.Uuid, host.Uuid) == CantBootReason.None)
							{
								result = true;
							}
						}
						else
						{
							result = true;
						}
					}
				}
			}
			return result;
		}
		private void PlaceVmOnHost(DwmVirtualMachine vm, DwmHost host)
		{
			DwmHostVM item = default(DwmHostVM);
			item.HostId = host.Id;
			item.HostName = host.Name;
			item.HostUuid = host.Uuid;
			item.VmId = vm.Id;
			item.VmName = vm.Name;
			item.VmUuid = vm.Uuid;
			item.Host = host;
			item.Host.Metrics.VM = vm;
			item.VM = vm;
			this.Recommendations.Add(item);
			if (this._memScoringType != MemoryScoringType.PotentialFree)
			{
				host.Metrics.FreeMemory -= vm.RequiredMemory;
				host.Metrics.PotentialFreeMemory -= vm.RequiredMemory;
			}
			else
			{
				host.Metrics.PotentialFreeMemory -= vm.RequiredMemory;
			}
			host.Metrics.MetricsNow.AverageCpuUtilization += vm.Metrics.MetricsNow.AverageCpuUtilization * ((double)vm.MinimumCpus / (double)host.NumCpus);
			host.Metrics.MetricsNow.AveragePbdReadsPerSecond += vm.Metrics.MetricsNow.AveragePbdReadsPerSecond;
			host.Metrics.MetricsNow.AveragePbdWritesPerSecond += vm.Metrics.MetricsNow.AveragePbdWritesPerSecond;
			host.Metrics.MetricsNow.AveragePifReadsPerSecond += vm.Metrics.MetricsNow.AveragePifReadsPerSecond;
			host.Metrics.MetricsNow.AveragePifWritesPerSecond += vm.Metrics.MetricsNow.AveragePifWritesPerSecond;
			host.Metrics.MetricsLast30Minutes.AverageFreeMemory -= vm.Metrics.MetricsLast30Minutes.AverageTotalMemory;
			host.Metrics.MetricsLast30Minutes.AverageCpuUtilization += vm.Metrics.MetricsLast30Minutes.AverageCpuUtilization * ((double)vm.MinimumCpus / (double)host.NumCpus);
			host.Metrics.MetricsLast30Minutes.AveragePbdReadsPerSecond += vm.Metrics.MetricsLast30Minutes.AveragePbdReadsPerSecond;
			host.Metrics.MetricsLast30Minutes.AveragePbdWritesPerSecond += vm.Metrics.MetricsLast30Minutes.AveragePbdWritesPerSecond;
			host.Metrics.MetricsLast30Minutes.AveragePifReadsPerSecond += vm.Metrics.MetricsLast30Minutes.AveragePifReadsPerSecond;
			host.Metrics.MetricsLast30Minutes.AveragePifWritesPerSecond += vm.Metrics.MetricsLast30Minutes.AveragePifWritesPerSecond;
			host.Metrics.MetricsYesterday.AverageFreeMemory -= vm.Metrics.MetricsYesterday.AverageTotalMemory;
			host.Metrics.MetricsYesterday.AverageCpuUtilization += vm.Metrics.MetricsYesterday.AverageCpuUtilization * ((double)vm.MinimumCpus / (double)host.NumCpus);
			host.Metrics.MetricsYesterday.AveragePbdReadsPerSecond += vm.Metrics.MetricsYesterday.AveragePbdReadsPerSecond;
			host.Metrics.MetricsYesterday.AveragePbdWritesPerSecond += vm.Metrics.MetricsYesterday.AveragePbdWritesPerSecond;
			host.Metrics.MetricsYesterday.AveragePifReadsPerSecond += vm.Metrics.MetricsYesterday.AveragePifReadsPerSecond;
			host.Metrics.MetricsYesterday.AveragePifWritesPerSecond += vm.Metrics.MetricsYesterday.AveragePifWritesPerSecond;
		}
		private bool FindHostToPowerOn()
		{
			DwmVirtualMachineCollection dwmVirtualMachineCollection = null;
			DwmHostCollection dwmHostCollection = null;
			for (int i = 0; i < this._vms.Count; i++)
			{
				if (this._vms[i].IsAgile && this._vms[i].Flags != 1)
				{
					if (dwmVirtualMachineCollection == null)
					{
						dwmVirtualMachineCollection = new DwmVirtualMachineCollection();
					}
					dwmVirtualMachineCollection.Add(this._vms[i]);
				}
			}
			if (dwmVirtualMachineCollection != null && dwmVirtualMachineCollection.Count > 0)
			{
				bool flag = (this._pool.OptMode != OptimizationMode.MaxDensity) ? this._pool.OverCommitCpusInPerfMode : this._pool.OverCommitCpusInDensityMode;
				dwmHostCollection = DwmAEHost.SelectHostsToPowerOn(this._pool, dwmVirtualMachineCollection, (!flag) ? new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByCpuRating) : new Comparison<DwmHost>(DwmAEOptimizer.CompareHostsByTotalMemory));
				if (dwmHostCollection != null && dwmHostCollection.Count > 0)
				{
					this._hosts.Clear();
					this._hosts = this._hostSnapShot;
					for (int j = 0; j < dwmHostCollection.Count; j++)
					{
						dwmHostCollection[j].SimpleLoad();
						dwmHostCollection[j].Enabled = true;
						dwmHostCollection[j].Metrics.FreeMemory = dwmHostCollection[j].Metrics.TotalMemory - dwmHostCollection[j].MemoryOverhead;
					}
					this._hosts.AddRange(dwmHostCollection);
					for (int k = 0; k < this._vms.Count; k++)
					{
						this._vms[k].Flags = 0;
					}
					this.Recommendations.Clear();
					for (int l = 0; l < dwmHostCollection.Count; l++)
					{
						DwmHostVM item = default(DwmHostVM);
						item.Host = dwmHostCollection[l];
						item.HostId = dwmHostCollection[l].Id;
						item.HostName = dwmHostCollection[l].Name;
						item.HostUuid = dwmHostCollection[l].Uuid;
						this.Recommendations.Add(item);
					}
				}
				this._attemptedPowerOn = true;
			}
			return dwmHostCollection != null && dwmHostCollection.Count > 0;
		}
	}
}
