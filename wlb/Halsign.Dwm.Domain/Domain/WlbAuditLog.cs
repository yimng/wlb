using Halsign.DWM.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
namespace Halsign.DWM.Domain
{
	public class WlbAuditLog
	{
		private const char VERTICAL_BAR = '|';
		private const string EQUALS = "=";
		private const string LEFT_SQ_BRACKET = "[";
		private const string RIGHT_SQ_BRACKET = "]";
		private const string OK = "OK";
		private const string ALLOWED = "ALLOWED";
		private DateTime _auditLogLastRetrieved = DateTime.MinValue;
		private int _poolId;
		private int _saveLogInterval = 10;
		private HashSet<string> _auditLogEventObjects;
		private HashSet<string> _auditLogEventActions;
		public static string _dateTimeFormat = "yyyyMMddTHH:mm:ss.fffZ";
		public DateTime AuditLogLastRetrieved
		{
			get
			{
				WlbAuditLogProcessor.Trace("get_audit_log_data_retrieve_info for pool_id={0}", new object[]
				{
					this.PoolId
				});
				string sqlStatement = "get_audit_log_data_retrieve_info";
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("@pool_id", this.PoolId));
				int num = 0;
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.UseTransaction = true;
					using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
					{
						if (dataReader.Read())
						{
							this._auditLogLastRetrieved = (DateTime)dataReader["log_tstamp"];
						}
						else
						{
							this._auditLogLastRetrieved = DateTime.UtcNow.AddDays(-7.0);
						}
						if (dataReader.NextResult() && dataReader.Read())
						{
							num = DBAccess.GetInt(dataReader, "auditLogRetrieveBackDays");
						}
					}
					if (num > 0)
					{
						sqlStatement = "reset_audit_log_data_retrieve_info";
						dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
						this._auditLogLastRetrieved = DateTime.UtcNow.AddDays((double)(-(double)num));
					}
				}
				return this._auditLogLastRetrieved;
			}
			set
			{
				this._auditLogLastRetrieved = value;
			}
		}
		public int PoolId
		{
			get
			{
				return this._poolId;
			}
			set
			{
				this._poolId = value;
			}
		}
		public WlbAuditLog(int poolId)
		{
			this.PoolId = poolId;
			this._auditLogEventObjects = new HashSet<string>();
			this._auditLogEventActions = new HashSet<string>();
			using (DBAccess dBAccess = new DBAccess())
			{
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("_pool_id", poolId));
				DataSet dataSet = dBAccess.ExecuteDataSet("hv_audit_log_get_event_objects_by_pool_id", storedProcParamCollection);
				DataSet dataSet2 = dBAccess.ExecuteDataSet("hv_audit_log_get_event_actions_by_pool_id", storedProcParamCollection);
				IEnumerator enumerator = dataSet.Tables[0].Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataRow dataRow = (DataRow)enumerator.Current;
						this._auditLogEventObjects.Add(dataRow["name"].ToString().ToLower());
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				IEnumerator enumerator2 = dataSet2.Tables[0].Rows.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						DataRow dataRow2 = (DataRow)enumerator2.Current;
						this._auditLogEventActions.Add(dataRow2["name"].ToString().ToLower());
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator2 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
			}
		}
		public void SaveLog(HttpWebResponse auditLogResponse)
		{
			this.ParseAndSaveAuditLog(auditLogResponse);
		}
		private void ParseAndSaveAuditLog(HttpWebResponse auditLogResponse)
		{
			WlbAuditLogEntryCollection wlbAuditLogEntryCollection = new WlbAuditLogEntryCollection();
			DateTime utcNow = DateTime.UtcNow;
			DateTime t = this._auditLogLastRetrieved;
			Regex regex = new Regex("(?<object>[\\w]*)[\\./](?<action>[\\w/]*)\\s");
			Regex regex2 = new Regex("'(?<sq>[^'\\\\]*(?:\\\\.[^'\\\\]*)*)'");
			try
			{
				using (TextReader textReader = new StreamReader(auditLogResponse.GetResponseStream(), Encoding.UTF8))
				{
					string text;
					while ((text = textReader.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(text) && text.Contains("]") && text.Contains("["))
						{
							string text2 = text.Remove(text.IndexOf("]")).Substring(text.IndexOf("[") + 1);
							string[] array;
							if (!string.IsNullOrEmpty(text2) && (array = text2.Split(new char[]
							{
								'|'
							})).Length >= 5)
							{
								DateTime dateTime = Localization.ParseExactDateTime(array[0], WlbAuditLog._dateTimeFormat).ToUniversalTime();
								string input = array[4];
								Match match = regex.Match(input);
								string text3 = match.Groups["object"].Value.ToLower();
								string text4 = match.Groups["action"].Value.ToLower();
								string text5 = text.Remove(0, text.IndexOf("]") + 2);
								if (this._auditLogEventObjects.Contains(text3) && this._auditLogEventActions.Contains(text4) && !string.IsNullOrEmpty(text5) && !(dateTime <= t))
								{
									t = dateTime;
									WlbAuditLogProcessor.Trace("Audit log entry: {0}", new object[]
									{
										text
									});
									WlbAuditLogEntry wlbAuditLogEntry = new WlbAuditLogEntry();
									wlbAuditLogEntry.PoolId = this.PoolId;
									wlbAuditLogEntry.TimeStamp = dateTime;
									wlbAuditLogEntry.LogType = array[1].Trim();
									wlbAuditLogEntry.TaskNameId = array[4].Trim();
									wlbAuditLogEntry.EventObject = text3;
									wlbAuditLogEntry.EventAction = text4;
									MatchCollection matchCollection = regex2.Matches(text5);
									wlbAuditLogEntry.SessionId = matchCollection[0].Groups["sq"].Value.Substring(matchCollection[0].Groups["sq"].Value.IndexOf("=") + 1);
									wlbAuditLogEntry.UserId = matchCollection[1].Groups["sq"].Value;
									wlbAuditLogEntry.UserName = matchCollection[2].Groups["sq"].Value;
									wlbAuditLogEntry.AccessAllowed = (Localization.Compare(matchCollection[3].Groups["sq"].Value, "ALLOWED", true) == 0);
									wlbAuditLogEntry.Succeeded = (Localization.Compare(matchCollection[4].Groups["sq"].Value, "OK", true) == 0);
									wlbAuditLogEntry.ErrorInfo = ((!wlbAuditLogEntry.Succeeded) ? matchCollection[4].Groups["sq"].Value : string.Empty);
									wlbAuditLogEntry.CallType = matchCollection[5].Groups["sq"].Value;
									if (matchCollection.Count >= 11)
									{
										wlbAuditLogEntry.EventObjectType = matchCollection[7].Groups["sq"].Value;
										wlbAuditLogEntry.EventObjectName = Regex.Unescape(matchCollection[8].Groups["sq"].Value);
										wlbAuditLogEntry.EventObjectUuid = matchCollection[9].Groups["sq"].Value;
										wlbAuditLogEntry.EventObjectOpaqueref = matchCollection[10].Groups["sq"].Value;
									}
									wlbAuditLogEntryCollection.Add(wlbAuditLogEntry);
									if (DateTime.UtcNow.Subtract(utcNow).TotalSeconds >= (double)this._saveLogInterval)
									{
										wlbAuditLogEntryCollection.Save();
										wlbAuditLogEntryCollection.Clear();
										utcNow = DateTime.UtcNow;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				WlbAuditLogProcessor.Trace("ParseAndSaveAuditLog Error: {0}", new object[]
				{
					ex.ToString()
				});
			}
			finally
			{
				if (auditLogResponse != null)
				{
					auditLogResponse.Close();
					auditLogResponse = null;
				}
			}
			if (wlbAuditLogEntryCollection.Count > 0)
			{
				wlbAuditLogEntryCollection.Save();
			}
			wlbAuditLogEntryCollection = null;
		}
	}
}
