using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
namespace Halsign.DWM.Domain
{
	public class WlbReports : Dictionary<string, WlbReport>
	{
		public void Load(string reportVersion)
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader("report_load_reports", new StoredProcParamCollection
				{
					new StoredProcParam("report_version", reportVersion)
				}))
				{
					while (dataReader.Read())
					{
						WlbReport wlbReport = new WlbReport();
						wlbReport.ReportId = Convert.ToInt32(dataReader["repid"]);
						wlbReport.ReportFile = dataReader["report_file"].ToString();
						wlbReport.StoredProcedureName = dataReader["report_proc"].ToString();
						wlbReport.PhysicalPath = dataReader["report_path"].ToString();
						wlbReport.ReportRdlc = dataReader["report_rdlc"].ToString();
						wlbReport.QueryParameters = WlbReport.StringToDictionary(dataReader["query_parameters"].ToString());
						string[] array = dataReader["report_parameters"].ToString().Split(new char[]
						{
							','
						});
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string text = array2[i];
							if (!string.IsNullOrEmpty(text))
							{
								wlbReport.ReportParameters.Add(text);
							}
						}
						base.Add(wlbReport.ReportFile, wlbReport);
					}
					if (dataReader.NextResult())
					{
						while (dataReader.Read())
						{
							if (base.ContainsKey(dataReader["report_file"].ToString()))
							{
								WlbReport wlbReport2 = base[dataReader["report_file"].ToString()];
								wlbReport2.LocalizedNames.Add(dataReader["two_letter_code"].ToString(), dataReader["report_name_localized"].ToString());
							}
						}
					}
					if (dataReader.NextResult())
					{
						while (dataReader.Read())
						{
							if (base.ContainsKey(dataReader["report_file"].ToString()))
							{
								WlbReport wlbReport3 = base[dataReader["report_file"].ToString()];
								wlbReport3.Labels.Add(dataReader["label_name"].ToString());
							}
						}
					}
				}
			}
		}
		public void Save()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				foreach (string current in base.Keys)
				{
					WlbReport wlbReport = base[current];
					dBAccess.ExecuteNonQuery("report_upload_report", new StoredProcParamCollection
					{
						new StoredProcParam("report_file", wlbReport.ReportFile),
						new StoredProcParam("report_proc", wlbReport.StoredProcedureName),
						new StoredProcParam("report_path", wlbReport.PhysicalPath),
						new StoredProcParam("query_parameters", WlbReports.GetSafeDBValue(WlbReport.DictionaryToString(wlbReport.QueryParameters))),
						new StoredProcParam("report_parameters", WlbReports.GetSafeDBValue(WlbReport.ListToString(wlbReport.ReportParameters))),
						new StoredProcParam("report_rdlc", wlbReport.ReportRdlc),
						new StoredProcParam("report_name_label", wlbReport.ReportNameLabel),
						new StoredProcParam("report_labels", WlbReports.GetSafeDBValue(WlbReport.ListToString(wlbReport.Labels))),
						new StoredProcParam("report_version", wlbReport.ReportVersion)
					});
				}
			}
		}
		private static object GetSafeDBValue(string value)
		{
            if (string.IsNullOrEmpty(value))
                return (object)DBNull.Value;
            return (object)value;
		}
	}
}
