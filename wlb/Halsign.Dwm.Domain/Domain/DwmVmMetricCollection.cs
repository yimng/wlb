using Halsign.DWM.Framework;
using System;
using System.Globalization;
using System.Text;
namespace Halsign.DWM.Domain
{
	public class DwmVmMetricCollection : DwmBaseCollection<DwmVmMetric>
	{
		private static NumberFormatInfo _nfi = new CultureInfo("en-US", false).NumberFormat;
		public override void Save(DBAccess2 db)
		{
			if (db == null)
			{
				throw new DwmException("Cannot pass null DBAccess2 instance to Save", DwmErrorCode.NullReference, null);
			}
			if (base.IsNew)
			{
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("ROLLBACK; START TRANSACTION;");
				int valueAsInt = Configuration.GetValueAsInt(ConfigItems.MetricsPerBatch, 100);
				for (int i = 0; i < base.Count; i++)
				{
					if (i > 0 && i % valueAsInt == 0)
					{
						stringBuilder.AppendLine("END TRANSACTION;");
						db.ExecuteSql(stringBuilder.ToString());
						stringBuilder.Length = 0;
						stringBuilder.AppendLine("ROLLBACK; START TRANSACTION;");
					}
					DwmVmMetric dwmVmMetric = base[i];
					DwmHostMetricCollection.Trace("Starting Save for VM {0} ({1})", new object[]
					{
						dwmVmMetric.VMId,
						dwmVmMetric.VMUuid
					});
					if (dwmVmMetric.VMId > 0 && dwmVmMetric.HostId > 0)
					{
						stringBuilder.Append("SELECT * FROM VM_METRIC_ADD(");
						DateTime dateTime = new DateTime(dwmVmMetric.TStamp.Year, dwmVmMetric.TStamp.Month, dwmVmMetric.TStamp.Day, dwmVmMetric.TStamp.Hour, dwmVmMetric.TStamp.Minute, dwmVmMetric.TStamp.Second);
						int num2;
						int num3;
						int num4;
						int num5;
						Localization.GetIntervals(dwmVmMetric.TStamp, out num2, out num3, out num4, out num5);
						stringBuilder.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},", new object[]
						{
							num2,
							num3,
							num4,
							num5,
							dwmVmMetric.VMId,
							dwmVmMetric.HostId,
							dwmVmMetric.TotalMem,
							dwmVmMetric.FreeMem,
							dwmVmMetric.TargetMem,
							dwmVmMetric.AvgCpuUtilization,
							dwmVmMetric.AvgPifIoReadPerSecond,
							dwmVmMetric.AvgPifIoWritePerSecond,
							dwmVmMetric.AvgVbdIoReadPerSecond,
							dwmVmMetric.AvgVbdIoWritePerSecond,
							dwmVmMetric.TotalVbdNetworkReadPerSecond,
							dwmVmMetric.TotalVbdNetworkWritePerSecond,
							dwmVmMetric.RunstateBlocked,
							dwmVmMetric.RunstatePartialRun,
							dwmVmMetric.RunstateFullRun,
							dwmVmMetric.RunstatePartialContention,
							dwmVmMetric.RunstateConcurrencyHazard,
							dwmVmMetric.RunstateFullContention
						});
						string[] array = new string[dwmVmMetric.CpuUtilization.Count];
						string[] array2 = new string[dwmVmMetric.CpuUtilization.Count];
						for (int j = 0; j < dwmVmMetric.CpuUtilization.Count; j++)
						{
							if (!double.IsInfinity(dwmVmMetric.CpuUtilization[j].MetricValue) && !double.IsNaN(dwmVmMetric.CpuUtilization[j].MetricValue))
							{
								array[j] = dwmVmMetric.CpuUtilization[j].DeviceNumber.ToString();
								array2[j] = dwmVmMetric.CpuUtilization[j].MetricValue.ToString();
							}
						}
						stringBuilder.AppendFormat("array[{0}]::integer[],", string.Join(",", array));
						stringBuilder.AppendFormat("array[{0}]::numeric(9,8)[],", string.Join(",", array2));
						string[] array3 = new string[dwmVmMetric.PifIoRead.Count];
						string[] array4 = new string[dwmVmMetric.PifIoRead.Count];
						string[] array5 = new string[dwmVmMetric.PifIoRead.Count];
						for (int k = 0; k < dwmVmMetric.PifIoRead.Count; k++)
						{
							if (!double.IsInfinity(dwmVmMetric.VifIoRead[k].MetricValue) && !double.IsInfinity(dwmVmMetric.VifIoWrite[k].MetricValue) && !double.IsNaN(dwmVmMetric.VifIoRead[k].MetricValue) && !double.IsNaN(dwmVmMetric.VifIoWrite[k].MetricValue))
							{
								array3[k] = dwmVmMetric.VifIoRead[k].DeviceNumber.ToString();
								array4[k] = dwmVmMetric.VifIoRead[k].MetricValue.ToString();
								array5[k] = dwmVmMetric.VifIoWrite[k].MetricValue.ToString();
							}
						}
						stringBuilder.AppendFormat("array[{0}]::integer[],", string.Join(",", array3));
						stringBuilder.AppendFormat("array[{0}]::numeric(18,4)[],", string.Join(",", array4));
						stringBuilder.AppendFormat("array[{0}]::numeric(18,4)[],", string.Join(",", array5));
						string[] array6 = new string[dwmVmMetric.VbdIoRead.Count];
						string[] array7 = new string[dwmVmMetric.VbdIoRead.Count];
						string[] array8 = new string[dwmVmMetric.VbdIoRead.Count];
						for (int l = 0; l < dwmVmMetric.VbdIoRead.Count; l++)
						{
							if (!double.IsInfinity(dwmVmMetric.VbdIoRead[l].MetricValue) && !double.IsInfinity(dwmVmMetric.VbdIoWrite[l].MetricValue) && !double.IsNaN(dwmVmMetric.VbdIoRead[l].MetricValue) && !double.IsNaN(dwmVmMetric.VbdIoWrite[l].MetricValue))
							{
								array6[l] = dwmVmMetric.VbdIoRead[l].DeviceNumber.ToString();
								array7[l] = dwmVmMetric.VbdIoRead[l].MetricValue.ToString();
								array8[l] = dwmVmMetric.VbdIoWrite[l].MetricValue.ToString();
							}
						}
						stringBuilder.AppendFormat("array[{0}]::integer[],", string.Join(",", array6));
						stringBuilder.AppendFormat("array[{0}]::numeric(18,4)[],", string.Join(",", array7));
						stringBuilder.AppendFormat("array[{0}]::numeric(18,4)[],", string.Join(",", array8));
						stringBuilder.AppendFormat("'{0}');", dateTime.ToString());
						stringBuilder.AppendLine();
					}
					DwmHostMetricCollection.Trace("Done with Save {0} for VM {1} ({1})", new object[]
					{
						num,
						dwmVmMetric.VMId,
						dwmVmMetric.VMUuid
					});
				}
				stringBuilder.AppendLine("END TRANSACTION;");
				db.ExecuteSql(stringBuilder.ToString());
			}
		}
	}
}
