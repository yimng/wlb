using Halsign.DWM.Collectors;
using Halsign.DWM.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
namespace Halsign.DWM.Framework
{
	public class DataCollectionManager
	{
		private const int StopEvent = 0;
		private const int ConsolidationIntervalMinutes = 5;
		private const int HistoricConsolidationIntervalMinutes = 60;
		private bool _isRunning;
		private Thread _managerThread;
		private WaitHandle[] _waitHandles = new WaitHandle[1];
		private int _pollInterval = 5000;
		private Dictionary<int, ICollector> _dataCollectors;
		private DateTime _lastHostMetricConsolidation;
		private DateTime _lastVmMetricConsolidation;
		private DateTime _lastHostMetricHistoricConsolidation;
		private DateTime _lastVmMetricHistoricConsolidation;
		private DateTime _lastDBGrooming;
		private string _vacuumTables;
		private WlbScheduleProcessor _scheduler;
		private WlbAuditLogProcessor _auditLog;
		public void Start()
		{
			if (!this._isRunning)
			{
				this._waitHandles[0] = new AutoResetEvent(false);
				this._managerThread = new Thread(new ThreadStart(this.ThreadProc));
				this._managerThread.Name = "DataCollectionManagerThread";
				this._managerThread.Start();
				this._isRunning = true;
			}
		}
		public void Stop()
		{
			if (this._isRunning)
			{
				((AutoResetEvent)this._waitHandles[0]).Set();
				if (this._dataCollectors != null)
				{
					foreach (KeyValuePair<int, ICollector> current in this._dataCollectors)
					{
						current.Value.StopCollection();
					}
				}
				if (this._auditLog != null)
				{
					this._auditLog.Stop();
				}
				if (this._scheduler != null)
				{
					this._scheduler.Stop();
				}
				this._isRunning = false;
			}
		}
		private void ThreadProc()
		{
			bool flag = false;
			this.InitializeCollectionManager();
			while (!flag)
			{
				int num = WaitHandle.WaitAny(this._waitHandles, this._pollInterval, false);
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 == 258)
					{
						try
						{
							this.OnInterval();
						}
						catch (Exception ex)
						{
							Logger.LogError("An error was encountered in the OnInterval process of the data collection thread", new object[0]);
							Logger.LogException(ex);
						}
					}
				}
				else
				{
					if (this._scheduler != null)
					{
						this._scheduler.Stop();
						Logger.Trace("Stopped the Scheduler thread");
						this._scheduler = null;
					}
					if (this._auditLog != null)
					{
						this._auditLog.Stop();
						Logger.Trace("Stopped the Audit Log thread");
						this._auditLog = null;
					}
					flag = true;
				}
			}
		}
		private void OnInterval()
		{
			try
			{
				this.ResesitateDeadCollectors();
				DwmPoolCollection databasePools = DwmPoolCollection.LoadPoolsForDataCollection();
				this.AddNewPools(databasePools);
				this.RemoveStalePools(databasePools);
				this.ConsolidateToFiveMinuteMetrics();
				this.ConsolidateToOneHourMetrics();
				this.GroomHistoricalRecords();
			}
			catch (Exception ex)
			{
				Logger.LogError("An error was encountered in the OnInterval process of the data collection thread", new object[0]);
				Logger.LogException(ex);
			}
		}
		private void ConsolidateToOneHourMetrics()
		{
			if ((DateTime.UtcNow - this._lastHostMetricHistoricConsolidation).TotalMinutes >= 60.0)
			{
				this.ConsolidateMetrics("\"WorkloadBalancing\".consolidate_host_metrics_history");
				this._lastHostMetricHistoricConsolidation = DateTime.UtcNow;
			}
			else
			{
				if ((DateTime.UtcNow - this._lastVmMetricHistoricConsolidation).TotalMinutes >= 60.0)
				{
					this.ConsolidateMetrics("\"WorkloadBalancing\".consolidate_vm_metrics_history");
					this._lastVmMetricHistoricConsolidation = DateTime.UtcNow;
				}
			}
		}
		private void ConsolidateToFiveMinuteMetrics()
		{
			if ((DateTime.UtcNow - this._lastHostMetricConsolidation).Minutes >= 5)
			{
				this.ConsolidateMetrics("\"WorkloadBalancing\".consolidate_host_metrics");
				this._lastHostMetricConsolidation = DateTime.UtcNow;
			}
			else
			{
				if ((DateTime.UtcNow - this._lastVmMetricConsolidation).Minutes >= 5)
				{
					this.ConsolidateMetrics("\"WorkloadBalancing\".consolidate_vm_metrics");
					this._lastVmMetricConsolidation = DateTime.UtcNow;
				}
			}
		}
		private void ResesitateDeadCollectors()
		{
			if (this._dataCollectors != null)
			{
				foreach (KeyValuePair<int, ICollector> current in this._dataCollectors)
				{
					if (!current.Value.IsAlive)
					{
						Logger.Trace("Data collector for pool with {0} is not alive!  Restarting", new object[]
						{
							current.Key
						});
						this.RestartCollector(current.Value);
					}
				}
			}
		}
		private void ReleaseUnusedDiskSpace(bool tableSpecific)
		{
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				Logger.Trace("Releasing unused disk space from database...");
				string valueAsString = Configuration.GetValueAsString(ConfigItems.DBName);
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.Timeout = 0;
					stopwatch.Start();
					if (tableSpecific && !string.IsNullOrEmpty(this._vacuumTables))
					{
						string[] array = this._vacuumTables.Split(new char[]
						{
							','
						});
						Logger.Trace("Starting table vacuuming...");
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string text = array2[i];
							this.TraceGrooming("Vacuuming table {0}...", new object[]
							{
								text
							});
							dBAccess.ExecuteNonQuery(string.Format("vacuum full {0};", text));
							this.TraceGrooming("Reindexing table {0}...", new object[]
							{
								text
							});
							dBAccess.ExecuteNonQuery(string.Format("reindex table {0}; ", text));
						}
					}
					else
					{
						Logger.Trace("Starting database vacuuming...");
						dBAccess.ExecuteNonQuery("vacuum full;");
						Logger.Trace("Starting database reindexing...");
						dBAccess.ExecuteNonQuery("reindex database \"" + valueAsString + "\";");
					}
					stopwatch.Stop();
					Logger.Trace("Vacuuming and reindexing completed in {0} ms", new object[]
					{
						stopwatch.ElapsedMilliseconds
					});
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
			}
		}
		private void InitializeCollectionManager()
		{
			this._lastHostMetricConsolidation = DateTime.UtcNow;
			this._lastVmMetricConsolidation = DateTime.UtcNow.AddMinutes(2.0);
			this._lastHostMetricHistoricConsolidation = DateTime.UtcNow.AddMinutes(4.0);
			this._lastVmMetricHistoricConsolidation = DateTime.UtcNow.AddMinutes(6.0);
			this._lastDBGrooming = DateTime.UtcNow.AddMinutes(8.0);
			if (Configuration.GetValueAsBool(ConfigItems.DBVacuumAtStartup))
			{
				this.ReleaseUnusedDiskSpace(false);
			}
			if (this._scheduler != null)
			{
				this._scheduler.Stop();
			}
			this._scheduler = new WlbScheduleProcessor();
			this._scheduler.Start();
			Logger.Trace("Started the Scheduling thread");
			if (this._auditLog != null)
			{
				this._auditLog.Stop();
			}
			this._auditLog = new WlbAuditLogProcessor();
			this._auditLog.Start();
			Logger.Trace("Started the AuditLog thread");
		}
		private void ConsolidateMetrics(string sqlStatement)
		{
			try
			{
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.Timeout = Configuration.GetValueAsInt(ConfigItems.ConsolidationDBTimeout, 300);
					DateTime now = DateTime.Now;
					this.TraceCompaction("Consolidating {0}{1} metrics...", new object[]
					{
						(!sqlStatement.Contains("host")) ? "vm" : "host",
						(!sqlStatement.Contains("hist")) ? string.Empty : " historical"
					});
					dBAccess.ExecuteNonQuery(sqlStatement);
					TimeSpan timeSpan = DateTime.Now - now;
					this.TraceCompaction("Consolidated {0}{1} metrics in {2} milliseconds.", new object[]
					{
						(!sqlStatement.Contains("host")) ? "vm" : "host",
						(!sqlStatement.Contains("hist")) ? string.Empty : " historical",
						timeSpan.TotalMilliseconds
					});
				}
			}
			catch (InvalidOperationException ex)
			{
				this.TraceCompaction("Consolidating {0} metrics...", new object[]
				{
					(!sqlStatement.Contains("host")) ? "vm" : "host"
				});
				Logger.LogException(ex);
			}
			catch (Exception ex2)
			{
				this.TraceCompaction("Consolidating {0} metrics...", new object[]
				{
					(!sqlStatement.Contains("host")) ? "vm" : "host"
				});
				Logger.LogException(ex2);
			}
		}
		private void GroomHistoricalRecords()
		{
			int num = 60 * Configuration.GetValueAsInt(ConfigItems.GroomingIntervalInHour);
			if (num == 0)
			{
				num = 60;
			}
			TimeSpan timeSpan = DateTime.UtcNow - this._lastDBGrooming;
			if (timeSpan.TotalMinutes >= (double)num)
			{
				this.TraceGrooming("Grooming...", new object[0]);
				DriveInfo driveInfo = new DriveInfo("/");
				long availableFreeSpace = driveInfo.AvailableFreeSpace;
				long num2 = Configuration.GetValueAsLong(ConfigItems.GroomingRequiredMinimumDiskSizeInMB) * 1024L * 1024L;
				if (num2 == 0L)
				{
					num2 = 524288000L;
				}
				int num3 = Configuration.GetValueAsInt(ConfigItems.GroomingDBDataTrimDays);
				if (num3 == 0)
				{
					num3 = 1;
				}
				this.TraceGrooming(string.Format("Grooming: Total available disk space: {0} bytes", driveInfo.AvailableFreeSpace.ToString()), new object[0]);
				this.TraceGrooming(string.Format("Grooming: Required minimum disk space: {0} bytes", num2.ToString()), new object[0]);
				this.TraceGrooming(string.Format("Grooming: Grooming interval is {0} minutes", num.ToString()), new object[0]);
				this.TraceGrooming(string.Format("Grooming: {0} Minutes has been past since last grooming run", timeSpan.TotalMinutes.ToString()), new object[0]);
				if (num2 >= availableFreeSpace)
				{
					long num4 = 0L;
					DateTime value = DateTime.MinValue;
					DateTime dateTime = DateTime.Now;
					try
					{
						DateTime now = DateTime.Now;
						this.TraceGrooming("Grooming historical data...", new object[0]);
						using (DBAccess dBAccess = new DBAccess())
						{
							this.TraceGrooming("Grooming: Getting database information...", new object[0]);
							dBAccess.UseTransaction = true;
							dBAccess.Timeout = Configuration.GetValueAsInt(ConfigItems.GroomingDBTimeoutInMinute) * 60;
							using (IDataReader dataReader = dBAccess.ExecuteReader("\"WorkloadBalancing\".groom_get_db_info"))
							{
								if (dataReader != null)
								{
									if (dataReader.Read())
									{
										value = DBAccess.GetDateTime(dataReader, "earliest_date");
									}
									if (dataReader.NextResult() && dataReader.Read())
									{
										dateTime = DBAccess.GetDateTime(dataReader, "latest_date");
									}
									if (dataReader.NextResult() && dataReader.Read())
									{
										num4 = DBAccess.GetInt64(dataReader, "current_history_table_size");
									}
								}
							}
						}
						this.TraceGrooming(string.Format("Grooming: The earlist data stored in the database is on {0}.", value.ToString()), new object[0]);
						this.TraceGrooming(string.Format("Grooming: The latest data stored in the database is on {0}.", dateTime.ToString()), new object[0]);
						this.TraceGrooming(string.Format("Grooming: Current history tables size is {0} bytes.", num4.ToString()), new object[0]);
						TimeSpan timeSpan2 = dateTime.Subtract(value);
						long num5 = num2 - availableFreeSpace;
						this.TraceGrooming(string.Format("Grooming: Required database grooming size is {0} bytes.", num5.ToString()), new object[0]);
						this.TraceGrooming(string.Format("Grooming: There is(are) {0} day(s) of data stored in the database.", timeSpan2.Days.ToString()), new object[0]);
						if (num3 >= timeSpan2.Days)
						{
							this.TraceGrooming(string.Format("Grooming: The number of days ({0}) used to groom data is greater than or equal to the days ({1}) of data stored in the database.", num3.ToString(), timeSpan2.Days.ToString()), new object[0]);
							this.TraceGrooming("Grooming: Not perform grooming.", new object[0]);
							Logger.Trace("Grooming: Warning - The available disk space is below the required disk space, there is not enough days of data in database to be deleted and free up disk space.");
							Logger.Trace("                    Please review your disk usage, and remove any unused files to free up disk space!");
						}
						else
						{
							if (num4 <= num5)
							{
								this.TraceGrooming(string.Format("Grooming: Possible database grooming size ({0}) is less than or equal to required database grooming size ({1}).", num4.ToString(), num5.ToString()), new object[0]);
								this.TraceGrooming("Grooming: Not perform grooming.", new object[0]);
								Logger.Trace("Gooming: Warning - The available disk space is below the required disk space, delete data will not free up enough disk space.");
								Logger.Trace("                   Please review your disk usage, and remove any unused files to free up disk space!");
							}
							else
							{
								this.TraceGrooming("Grooming: validate loop counter, data trim days and available disk size...", new object[0]);
								int num6 = 0;
								DateTime dateTime2 = value.AddDays((double)num3);
								int valueAsInt = Configuration.GetValueAsInt(ConfigItems.GroomingRetryCounter);
								StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
								while (num6 <= valueAsInt && num3 < timeSpan2.Days && availableFreeSpace <= num2)
								{
									this.TraceGrooming("Grooming: {0} day(s) of data will be deleted in round {1}", new object[]
									{
										num3.ToString(),
										(num6 + 1).ToString()
									});
									this.TraceGrooming("Grooming: Deleting data...", new object[0]);
									storedProcParamCollection.Clear();
									storedProcParamCollection.Add(new StoredProcParam("@groom_date", dateTime2));
									using (DBAccess dBAccess2 = new DBAccess())
									{
										dBAccess2.UseTransaction = true;
										dBAccess2.Timeout = Configuration.GetValueAsInt(ConfigItems.GroomingDBTimeoutInMinute) * 60;
										this._vacuumTables = dBAccess2.ExecuteScalarString("\"WorkloadBalancing\".groom_historical_data_by_date", storedProcParamCollection);
										this.TraceGrooming("Grooming: Total of {0} table(s) will be vacuumed and reindexed", new object[]
										{
											this._vacuumTables.Split(new char[]
											{
												','
											}).Length.ToString()
										});
									}
									if (Configuration.GetValueAsBool(ConfigItems.GroomingReleaseDiskSpace, true))
									{
										this.ReleaseUnusedDiskSpace(true);
									}
									driveInfo = new DriveInfo("/");
									availableFreeSpace = driveInfo.AvailableFreeSpace;
									timeSpan2 = dateTime.Subtract(dateTime2);
									dateTime2 = dateTime2.AddDays((double)num3);
									num6++;
									this.TraceGrooming(string.Format("Grooming: After grooming round {0}, current total available disk space is: {1}", num6.ToString(), driveInfo.AvailableFreeSpace.ToString()), new object[0]);
								}
								this.TraceGrooming(string.Format("Grooming: Removed {0} day(s) data.", (num6 * num3).ToString()), new object[0]);
								this.TraceGrooming(string.Format("Grooming: Groomed historical data in {0} milliseconds.", (DateTime.Now - now).Milliseconds.ToString()), new object[0]);
								this.TraceGrooming(string.Format("Grooming: Total available disk space is: {0} bytes", driveInfo.AvailableFreeSpace.ToString()), new object[0]);
							}
						}
					}
					catch (InvalidOperationException ex)
					{
						Logger.Trace("Grooming historical data...");
						Logger.LogException(ex);
					}
					catch (Exception ex2)
					{
						Logger.Trace("Grooming historical data...");
						Logger.LogException(ex2);
					}
					this.TraceGrooming("Grooming completed.", new object[0]);
				}
				else
				{
					this.TraceGrooming("Grooming: There is still plenty available disk space, no need grooming", new object[0]);
				}
				this._lastDBGrooming = DateTime.UtcNow;
			}
		}
		private void AddNewPools(DwmPoolCollection databasePools)
		{
			if (this._dataCollectors == null)
			{
				this._dataCollectors = new Dictionary<int, ICollector>();
			}
			foreach (DwmPool current in databasePools)
			{
				if (!this._dataCollectors.ContainsKey(current.Id))
				{
					XenCollector xenCollector = this.StartXenPoolCollector(current);
					if (xenCollector != null)
					{
						this._dataCollectors.Add(current.Id, xenCollector);
					}
				}
			}
		}
		private XenCollector StartXenPoolCollector(DwmPool pool)
		{
			XenCollector xenCollector = new XenCollector();
			try
			{
				Logger.Trace("**** Starting collection for Pool {0}", new object[]
				{
					pool.Id.ToString()
				});
				xenCollector.Initialize(pool.PrimaryPoolMasterAddr, pool.PrimaryPoolMasterPort, pool.Protocol, pool.UserName, pool.Password, pool.Id);
				xenCollector.StartCollection();
			}
			catch (ApplicationException ex)
			{
				Logger.LogException(ex);
				xenCollector = null;
			}
			catch (WebException ex2)
			{
				Logger.Trace("Caught WebException initializing {0}://{1}:{2} {3}", new object[]
				{
					pool.Protocol,
					pool.PrimaryPoolMasterAddr,
					pool.PrimaryPoolMasterPort,
					pool.UserName
				});
				Logger.LogException(ex2);
				xenCollector = null;
			}
			catch (DwmException ex3)
			{
				Logger.Trace("Caught DwmException initializing {0}://{1}:{2} {3}", new object[]
				{
					pool.Protocol,
					pool.PrimaryPoolMasterAddr,
					pool.PrimaryPoolMasterPort,
					pool.UserName
				});
				Logger.LogException(ex3);
				xenCollector = null;
			}
			return xenCollector;
		}
		private void RemoveStalePools(DwmPoolCollection databasePools)
		{
			List<int> list = new List<int>();
			foreach (int current in this._dataCollectors.Keys)
			{
				if (!databasePools.ContainsKey(current))
				{
					list.Add(current);
				}
			}
			if (list.Count > 0)
			{
				Logger.Trace("Found {0} deleted pool(s)...", new object[]
				{
					list.Count.ToString()
				});
				foreach (int current2 in list)
				{
					if (this._dataCollectors.ContainsKey(current2))
					{
						Logger.Trace("Stopping and removing collector for pool {0}.", new object[]
						{
							current2.ToString()
						});
						this._dataCollectors[current2].StopCollection();
						this._dataCollectors.Remove(current2);
					}
				}
			}
		}
		private DwmPoolCollection GetMyPools()
		{
			return DwmPoolCollection.LoadPoolsForDataCollection();
		}
		private static DwmPoolCollection GetPools(int[] poolIDs)
		{
			DwmPoolCollection dwmPoolCollection = null;
			if (poolIDs != null && poolIDs.Length > 0)
			{
				dwmPoolCollection = new DwmPoolCollection();
				for (int i = 0; i < poolIDs.Length; i++)
				{
					DwmPool dwmPool = DwmPool.SimpleLoadByPoolId(poolIDs[i]);
					if (dwmPool != null)
					{
						dwmPoolCollection.Add(dwmPool);
					}
				}
			}
			return dwmPoolCollection;
		}
		private void RestartCollector(ICollector collector)
		{
			try
			{
				collector.StopCollection();
				collector.StartCollection();
			}
			catch (ApplicationException ex)
			{
				Logger.LogException(ex);
			}
			catch (WebException ex2)
			{
				Logger.LogException(ex2);
			}
			catch (DwmException ex3)
			{
				Logger.LogException(ex3);
			}
		}
		protected void TraceCompaction(string fmt, params object[] args)
		{
			bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.DataCompactionTrace);
			if (valueAsBool)
			{
				Logger.Trace(fmt, args);
			}
		}
		protected void TraceGrooming(string fmt, params object[] args)
		{
			bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.DataGroomingTrace);
			if (valueAsBool)
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
