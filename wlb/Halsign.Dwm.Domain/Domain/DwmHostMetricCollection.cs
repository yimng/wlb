using Halsign.DWM.Framework;
using System;
using System.Globalization;
namespace Halsign.DWM.Domain
{
	public class DwmHostMetricCollection : DwmBaseCollection<DwmHostMetric>
	{
		private static NumberFormatInfo _nfi = new CultureInfo("en-US", false).NumberFormat;
		public override void Save(DBAccess2 db)
		{
			if (base.IsNew)
			{
				int num = 0;
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				for (int i = 0; i < base.Count; i++)
				{
					DwmHostMetric dwmHostMetric = base[i];
					DwmHostMetricCollection.Trace("Starting Save for host {0} ({1})", new object[]
					{
						dwmHostMetric.HostId,
						dwmHostMetric.HostUuid
					});
					if (dwmHostMetric.HostId > 0)
					{
						DateTime dateTime = new DateTime(dwmHostMetric.TStamp.Year, dwmHostMetric.TStamp.Month, dwmHostMetric.TStamp.Day, dwmHostMetric.TStamp.Hour, dwmHostMetric.TStamp.Minute, dwmHostMetric.TStamp.Second);
						int num2;
						int num3;
						int num4;
						int num5;
						Localization.GetIntervals(dwmHostMetric.TStamp, out num2, out num3, out num4, out num5);
						storedProcParamCollection.Clear();
						storedProcParamCollection.Add(new StoredProcParam("_hour_interval", num2));
						storedProcParamCollection.Add(new StoredProcParam("_five_minute_interval", num3));
						storedProcParamCollection.Add(new StoredProcParam("_day_of_year", num4));
						storedProcParamCollection.Add(new StoredProcParam("_year", num5));
						storedProcParamCollection.Add(new StoredProcParam("_hostid", int.Parse(dwmHostMetric.HostId.ToString(DwmHostMetricCollection._nfi))));
						storedProcParamCollection.Add(new StoredProcParam("_total_mem", long.Parse(dwmHostMetric.TotalMem.ToString(DwmHostMetricCollection._nfi))));
						storedProcParamCollection.Add(new StoredProcParam("_free_mem", long.Parse(dwmHostMetric.FreeMem.ToString(DwmHostMetricCollection._nfi))));
						storedProcParamCollection.Add(new StoredProcParam("_load_average", double.Parse(dwmHostMetric.LoadAverage.ToString(DwmHostMetricCollection._nfi))));
						storedProcParamCollection.Add(new StoredProcParam("_avg_cpu_utilization", double.Parse(dwmHostMetric.AvgCpuUtilization.ToString(DwmHostMetricCollection._nfi))));
						storedProcParamCollection.Add(new StoredProcParam("_avg_pif_read_per_sec", double.Parse(dwmHostMetric.AvgPifIoReadPerSecond.ToString(DwmHostMetricCollection._nfi))));
						storedProcParamCollection.Add(new StoredProcParam("_avg_pif_write_per_sec", double.Parse(dwmHostMetric.AvgPifIoWritePerSecond.ToString(DwmHostMetricCollection._nfi))));
						int[] array = new int[dwmHostMetric.CpuUtilization.Count];
						double[] array2 = new double[dwmHostMetric.CpuUtilization.Count];
						for (int j = 0; j < dwmHostMetric.CpuUtilization.Count; j++)
						{
							if (!double.IsInfinity(dwmHostMetric.CpuUtilization[j].MetricValue) && !double.IsNaN(dwmHostMetric.CpuUtilization[j].MetricValue))
							{
								array[j] = int.Parse(dwmHostMetric.CpuUtilization[j].DeviceNumber.ToString(DwmHostMetricCollection._nfi));
								array2[j] = double.Parse(dwmHostMetric.CpuUtilization[j].MetricValue.ToString(DwmHostMetricCollection._nfi));
							}
						}
						storedProcParamCollection.Add(new StoredProcParam("_cpu", array, (StoredProcParam.DataTypes)(-2147483639)));
						storedProcParamCollection.Add(new StoredProcParam("_utilization", array2, (StoredProcParam.DataTypes)(-2147483635)));
						int[] array3 = new int[dwmHostMetric.PifIoRead.Count];
						double[] array4 = new double[dwmHostMetric.PifIoRead.Count];
						double[] array5 = new double[dwmHostMetric.PifIoRead.Count];
						for (int k = 0; k < dwmHostMetric.PifIoRead.Count; k++)
						{
							if (!double.IsInfinity(dwmHostMetric.PifIoRead[k].MetricValue) && !double.IsInfinity(dwmHostMetric.PifIoWrite[k].MetricValue) && !double.IsNaN(dwmHostMetric.PifIoRead[k].MetricValue) && !double.IsNaN(dwmHostMetric.PifIoWrite[k].MetricValue))
							{
								array3[k] = int.Parse(dwmHostMetric.PifIoRead[k].DeviceNumber.ToString(DwmHostMetricCollection._nfi));
								array4[k] = double.Parse(dwmHostMetric.PifIoRead[k].MetricValue.ToString(DwmHostMetricCollection._nfi));
								array5[k] = double.Parse(dwmHostMetric.PifIoWrite[k].MetricValue.ToString(DwmHostMetricCollection._nfi));
							}
						}
						storedProcParamCollection.Add(new StoredProcParam("_pif", array3, (StoredProcParam.DataTypes)(-2147483639)));
						storedProcParamCollection.Add(new StoredProcParam("_avg_pif_read_per_sec", array4, (StoredProcParam.DataTypes)(-2147483635)));
						storedProcParamCollection.Add(new StoredProcParam("_avg_pif_write_per_sec", array5, (StoredProcParam.DataTypes)(-2147483635)));
						storedProcParamCollection.Add(new StoredProcParam("_tstamp", dateTime));
						num = db.ExecuteScalar<int>("host_metric_add", storedProcParamCollection);
					}
					DwmHostMetricCollection.Trace("Done with Save {0} for host {1} ({2})", new object[]
					{
						num.ToString(),
						dwmHostMetric.HostId,
						dwmHostMetric.HostUuid
					});
				}
			}
		}
		internal static void Trace(string fmt, params object[] args)
		{
			if (Configuration.GetValueAsBool(ConfigItems.DataSaveTrace))
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
