using Halsign.DWM.Framework;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using XenAPI;
namespace Halsign.DWM.Domain
{
	public class WlbAuditLogProcessor
	{
		private const int StopEvent = 0;
		private const int DefaultSessionTimeout = 20000;
		private bool _isRunning;
		private Thread _thread;
		private WaitHandle[] _waitHandles = new WaitHandle[1];
		private DwmPoolCollection _poolCollection;
		private Session _session;
		private int _pollInterval = 120000;
		private static bool _traceEnabled;
		public virtual void Start()
		{
			WlbAuditLogProcessor.Trace("Starting WlbAuditLogProcessor...", new object[0]);
			if (!this._isRunning)
			{
				this._waitHandles[0] = new AutoResetEvent(false);
				this._thread = new Thread(new ThreadStart(this.ThreadProc));
				this._thread.Name = "WlbAuditLogProcessorThread";
				this._thread.Start();
				this._isRunning = true;
			}
			WlbAuditLogProcessor.Trace("WlbAuditLogProcessor started.", new object[0]);
		}
		public virtual void Stop()
		{
			WlbAuditLogProcessor.Trace("Stopping WlbAuditLogProcessor...", new object[0]);
			if (this._isRunning)
			{
				((AutoResetEvent)this._waitHandles[0]).Set();
				this._isRunning = false;
			}
			WlbAuditLogProcessor.Trace("WlbAuditLogProcessor stopped", new object[0]);
		}
		private void ThreadProc()
		{
			bool flag = false;
			while (!flag)
			{
				this._pollInterval = WlbAuditLogProcessor.RetrievePollInterval();
				int num = WaitHandle.WaitAny(this._waitHandles, this._pollInterval, false);
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 == 258)
					{
						this.RetrieveAndStoreAuditLog();
					}
				}
				else
				{
					flag = true;
				}
			}
		}
		public void RetrieveAndStoreAuditLog()
		{
			WlbAuditLogProcessor.Trace("Retrieving XenServer audit log...", new object[0]);
			this._poolCollection = DwmPoolCollection.LoadPoolsForDataCollection();
			if (this._poolCollection == null || this._poolCollection.Count == 0)
			{
				WlbAuditLogProcessor.Trace("No XenServer is monitored, retrieving XenServer audit log is done.", new object[0]);
			}
			else
			{
				foreach (DwmPool current in this._poolCollection)
				{
					this._session = null;
					if (current != null && current.Id > 0 && current.HVType == DwmHypervisorType.XenServer)
					{
						int num;
						int num2;
						if (int.TryParse(current.GetOtherConfigItem("VersionMajor"), out num) && int.TryParse(current.GetOtherConfigItem("VersionMinor"), out num2))
						{
							if ((num == 5 && num2 >= 6) || num >= 6)
							{
								string otherConfigItem = current.GetOtherConfigItem("RetrieveAuditLog");
								if (string.IsNullOrEmpty(otherConfigItem) || otherConfigItem.ToLower() != "false")
								{
									WlbAuditLogProcessor.Trace("Retrieving XenServer audit log for pool: {0}:{1}", new object[]
									{
										current.PrimaryPoolMasterAddr,
										current.PrimaryPoolMasterPort
									});
									WlbAuditLog wlbAuditLog = new WlbAuditLog(current.Id);
									XenRef<Task> xenRef = null;
									if (this.InitializeSession(current))
									{
										try
										{
											if (wlbAuditLog != null && !string.IsNullOrEmpty(current.PrimaryPoolMasterAddr) && this._session != null)
											{
												xenRef = Task.create(this._session, "GetAuditLogTask", "GetAuditLogTaskId");
												if (!string.IsNullOrEmpty(xenRef.opaque_ref))
												{
													HttpWebResponse auditLogResponse = this.GetAuditLogResponse(this._session, current, wlbAuditLog.AuditLogLastRetrieved, xenRef.opaque_ref);
													Task.destroy(this._session, xenRef.opaque_ref);
													xenRef = null;
													if (auditLogResponse != null)
													{
														WlbAuditLogProcessor.Trace("Saving audit log data...", new object[0]);
														wlbAuditLog.SaveLog(auditLogResponse);
														WlbAuditLogProcessor.Trace("Finished saving audit log data.", new object[0]);
													}
												}
											}
										}
										catch (Exception ex)
										{
											Logger.LogException(ex);
											if (xenRef != null)
											{
												try
												{
													Task.destroy(this._session, xenRef.opaque_ref);
												}
												catch (Exception ex2)
												{
													Logger.LogException(ex2);
												}
											}
										}
										finally
										{
											xenRef = null;
											this.EndSession();
										}
										WlbAuditLogProcessor.Trace("Done retriving audit log for pool {0}:{1}", new object[]
										{
											current.PrimaryPoolMasterAddr,
											current.PrimaryPoolMasterPort
										});
									}
								}
								else
								{
									WlbAuditLogProcessor.Trace("Audit log retrieval has been turned off for pool: {0}:{1}", new object[]
									{
										current.PrimaryPoolMasterAddr,
										current.PrimaryPoolMasterPort
									});
								}
							}
							else
							{
								WlbAuditLogProcessor.Trace("The version of XenServer ({0}.{1}) for pool {2}:{3} does not support Audit Log retrieval.", new object[]
								{
									num,
									num2,
									current.PrimaryPoolMasterAddr,
									current.PrimaryPoolMasterPort
								});
							}
						}
						else
						{
							WlbAuditLogProcessor.Trace("Audit Log will not be retrieved because we cannot determine the version of the XenServer pool {0}:{1}.", new object[]
							{
								current.PrimaryPoolMasterAddr,
								current.PrimaryPoolMasterPort
							});
						}
					}
				}
			}
		}
		public static int RetrievePollInterval()
		{
			int num;
			try
			{
				num = Configuration.GetValueAsInt(ConfigItems.AuditLogRetrievalInterval, 120);
				if (num > 0)
				{
					num *= 1000;
				}
				else
				{
					num = 120000;
				}
			}
			catch (DwmException ex)
			{
				Logger.LogException(ex);
				num = 120000;
			}
			return num;
		}
		private bool InitializeSession(DwmPool pool)
		{
			WlbAuditLogProcessor.Trace("Initialize retrieving audit log session for pool {0}:{1}", new object[]
			{
				pool.PrimaryPoolMasterAddr,
				pool.PrimaryPoolMasterPort
			});
			if (this._session != null)
			{
				Logger.LogError("InitializeSession called and _session != null!", new object[0]);
				return false;
			}
			bool result;
			try
			{
				Session.UserAgent = "WlbDataCollector";
				this._session = new Session(20000, pool.PrimaryPoolMasterAddr, pool.PrimaryPoolMasterPort);
				this._session.login_with_password(pool.UserName, pool.Password);
				string arg_8C_0 = this._session.get_this_host();
				result = true;
			}
			catch (Failure failure)
			{
				WlbAuditLogProcessor.Trace("Failure exception {0} trying start session for retrieving Audit Log on {1}:{2}", new object[]
				{
					(failure.ErrorDescription == null || failure.ErrorDescription.Count <= 0) ? failure.Message : failure.ErrorDescription[0],
					pool.PrimaryPoolMasterAddr,
					pool.PrimaryPoolMasterPort
				});
				Logger.LogException(failure);
				this.EndSession();
				result = false;
			}
			catch (SocketException ex)
			{
				WlbAuditLogProcessor.Trace("SocketException exception {0} trying start session for retrieving Audit Log on {1}:{2}", new object[]
				{
					ex.Message,
					pool.PrimaryPoolMasterAddr,
					pool.PrimaryPoolMasterPort
				});
				Logger.LogException(ex);
				this.EndSession();
				result = false;
			}
			catch (WebException ex2)
			{
				WlbAuditLogProcessor.Trace("WebException exception {0} trying start session for retrieving Audit Log on {1}:{2}", new object[]
				{
					ex2.Message,
					pool.PrimaryPoolMasterAddr,
					pool.PrimaryPoolMasterPort
				});
				Logger.LogException(ex2);
				this.EndSession();
				result = false;
			}
			catch (Exception ex3)
			{
				WlbAuditLogProcessor.Trace("General System exception {0} trying start session for retrieving Audit Log on {1}:{2}", new object[]
				{
					ex3.Message,
					pool.PrimaryPoolMasterAddr,
					pool.PrimaryPoolMasterPort
				});
				Logger.LogException(ex3);
				this.EndSession();
				result = false;
			}
			return result;
		}
		private void EndSession()
		{
			WlbAuditLogProcessor.Trace("Ending retrieving Audit Log session {0}", new object[]
			{
				(this._session == null) ? "null" : this._session.Url
			});
			if (this._session != null)
			{
				try
				{
					this._session.logout();
					WlbAuditLogProcessor.Trace("Retrieving Audit Log session.logout {0} succeeds", new object[]
					{
						(this._session.Url == null) ? string.Empty : this._session.Url
					});
				}
				catch (Exception)
				{
				}
				finally
				{
					this._session = null;
				}
			}
		}
		private HttpWebResponse GetAuditLogResponse(Session session, DwmPool pool, DateTime getDataSince, string taskId)
		{
			string text = Localization.Format("{0}://{1}:{2}/audit_log?session_id={3}&task_id={4}&since={5}", new object[]
			{
				pool.Protocol,
				pool.PrimaryPoolMasterAddr,
				pool.PrimaryPoolMasterPort,
				session.uuid,
				taskId,
				getDataSince.ToString(WlbAuditLog._dateTimeFormat)
			});
			WlbAuditLogProcessor.Trace("Retrieving audit log data with uri {0}", new object[]
			{
				text
			});
			return WlbAuditLogProcessor.GetPoolResponse(text);
		}
		public static HttpWebResponse GetPoolResponse(string uri)
		{
			HttpWebResponse httpWebResponse = null;
			try
			{
				WlbAuditLogProcessor.Trace("Requesting data from {0}", new object[]
				{
					uri
				});
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
				httpWebRequest.UserAgent = "WlbDataCollector";
				httpWebRequest.MaximumAutomaticRedirections = 4;
				httpWebRequest.MaximumResponseHeadersLength = 4;
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				WlbAuditLogProcessor.Trace("Content status is {0}", new object[]
				{
					httpWebResponse.StatusCode
				});
				WlbAuditLogProcessor.Trace("Content length is {0}", new object[]
				{
					httpWebResponse.ContentLength
				});
				WlbAuditLogProcessor.Trace("Content type is {0}", new object[]
				{
					httpWebResponse.ContentType
				});
			}
			catch (WebException ex)
			{
				WlbAuditLogProcessor.Trace("Exception requesting data from {0}", new object[]
				{
					uri
				});
				Logger.LogException(ex);
			}
			return httpWebResponse;
		}
		internal static void Trace(string fmt, params object[] args)
		{
			WlbAuditLogProcessor._traceEnabled = Configuration.GetValueAsBool(ConfigItems.AuditLogTrace);
			if (WlbAuditLogProcessor._traceEnabled)
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
