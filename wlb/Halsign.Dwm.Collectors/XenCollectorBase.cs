using Halsign.DWM.Domain;
using Halsign.DWM.Framework;
using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using XenAPI;
namespace Halsign.DWM.Collectors
{
	public abstract class XenCollectorBase : CollectorBase
	{
		protected enum SessionInitStatus
		{
			None,
			Success,
			HostIsSlave,
			HostStillBooting,
			HostOffline,
			AuthenticationFailure,
			WebException,
			SocketException,
			Failure
		}
		private const int DefaultSessionTimeout = 20000;
		private const int DefaultSessionTimeoutRetryInterval = 30000;
		private const int DefaultSessionTimeoutMax = 60000;
		protected Session _session;
		protected Session _threadSession;
		private int _sessionTimeout = 20000;
		private int _sessionTimeoutMax = 60000;
		private int _sessionTimeoutRetryInterval = 30000;
		private int _sessionTimeoutCount;
		private bool _externalStop;
		protected DwmPool _pool;
		protected bool _retryLostConnection;
		protected string _masterServerOpaqueRef;
		protected Thread _eventThread;
		protected WaitHandle[] _eventThreadWaitHandles;
		protected string[] _xenEventClasses = new string[]
		{
			"*"
		};
		protected static bool _traceEnabled;
		protected static bool _traceEventsEnabled;
		public XenCollectorBase()
		{
		}
		public XenCollectorBase(string hostname, int port, string protocol, string username, string password)
		{
			this.Initialize(hostname, port, protocol, username, password, 0);
		}
		public override void Initialize(string hostname, int port, string protocol, string username, string password, int poolId)
		{
			base.Initialize(hostname, port, protocol, username, password, poolId);
			this.InitializeSession();
		}
		private void InitializeSession()
		{
			this.IsInitialized = false;
			this.InitializeSession(this._hostName, false, true);
			this.IsInitialized = true;
		}
		protected XenCollectorBase.SessionInitStatus InitializeSession(string hostName, bool registerForEvents, bool rethrowExceptions)
		{
			XenCollectorBase.SessionInitStatus sessionInitStatus = XenCollectorBase.SessionInitStatus.None;
			try
			{
				this._sessionTimeout = Configuration.GetValueAsInt(ConfigItems.SessionTimeout);
				this._sessionTimeoutRetryInterval = Configuration.GetValueAsInt(ConfigItems.SessionTimeoutRetryInterval);
				this._sessionTimeoutMax = Configuration.GetValueAsInt(ConfigItems.SessionTimeoutMax);
				this._sessionTimeout = ((this._sessionTimeout <= 0 || this._sessionTimeout >= this._sessionTimeoutMax) ? 20000 : (this._sessionTimeout * 1000));
				this._sessionTimeoutMax = ((this._sessionTimeoutMax <= 0 || this._sessionTimeout >= this._sessionTimeoutMax) ? 60000 : (this._sessionTimeoutMax * 1000));
				this._sessionTimeoutRetryInterval = ((this._sessionTimeoutRetryInterval <= 0 || this._sessionTimeout >= this._sessionTimeoutMax) ? 30000 : (this._sessionTimeoutRetryInterval * 1000));
				this._sessionTimeout += 10 * this._sessionTimeoutCount * 1000;
				this._sessionTimeout = ((this._sessionTimeout >= this._sessionTimeoutMax) ? this._sessionTimeoutMax : this._sessionTimeout);
				Logger.Trace("Data collection session timeout set to {0} seconds", new object[]
				{
					this._sessionTimeout / 1000
				});
				Logger.Trace("Data collection session timeout max set to {0} seconds", new object[]
				{
					this._sessionTimeoutMax / 1000
				});
				Logger.Trace("Data collection session timeout retry interval set to {0} seconds", new object[]
				{
					this._sessionTimeoutRetryInterval / 1000
				});
				if (this._session != null)
				{
					throw new InvalidOperationException("InitializeSession called and _session != null!");
				}
				Session.UserAgent = "WlbDataCollector";
				this._session = new Session(this._sessionTimeout, hostName, this._hostPort);
				this._session.login_with_password(this._username, this._password);
				if (this._pool != null && this._pool.Id > 0)
				{
					this._pool.AreCredentialsValid = true;
				}
				else
				{
					if (this._poolId > 0)
					{
						DwmPool dwmPool = new DwmPool(this._poolId);
						dwmPool.AreCredentialsValid = true;
					}
				}
				this._hostName = hostName;
				this._masterServerOpaqueRef = this._session.get_this_host();
				if (registerForEvents)
				{
					this.RegisterForEvents();
				}
				this._sessionTimeoutCount = 0;
				this._retryLostConnection = false;
				Logger.Trace("Started Xen session on {0}", new object[]
				{
					this._session.Url
				});
				sessionInitStatus = XenCollectorBase.SessionInitStatus.Success;
			}
			catch (Failure failure)
			{
				Logger.Trace("Failure exception {0} trying start session on {1}:{2}", new object[]
				{
					(failure.ErrorDescription == null || failure.ErrorDescription.Count <= 0) ? failure.Message : failure.ErrorDescription[0],
					hostName,
					this._hostPort
				});
				this.EndSession();
				if (Localization.Compare(failure.ErrorDescription[0], "HOST_IS_SLAVE", true) == 0)
				{
					sessionInitStatus = XenCollectorBase.SessionInitStatus.HostIsSlave;
				}
				else
				{
					if (Localization.Compare(failure.ErrorDescription[0], "HOST_STILL_BOOTING", true) == 0)
					{
						sessionInitStatus = XenCollectorBase.SessionInitStatus.HostStillBooting;
					}
					else
					{
						if (Localization.Compare(failure.ErrorDescription[0], "HOST_OFFLINE", true) == 0)
						{
							sessionInitStatus = XenCollectorBase.SessionInitStatus.HostOffline;
						}
						else
						{
							if (Localization.Compare(failure.ErrorDescription[0], "SESSION_AUTHENTICATION_FAILED", true) == 0)
							{
								sessionInitStatus = XenCollectorBase.SessionInitStatus.AuthenticationFailure;
								if (this._pool != null && this._pool.Id > 0)
								{
									this._pool.AreCredentialsValid = false;
								}
								else
								{
									if (this._poolId > 0)
									{
										DwmPool dwmPool2 = new DwmPool(this._poolId);
										dwmPool2.AreCredentialsValid = false;
									}
								}
							}
							else
							{
								sessionInitStatus = XenCollectorBase.SessionInitStatus.Failure;
							}
						}
					}
				}
				if (rethrowExceptions)
				{
					if (sessionInitStatus == XenCollectorBase.SessionInitStatus.HostIsSlave)
					{
						this.FindNewMaster();
						if (this._session != null)
						{
							XenCollectorBase.SessionInitStatus result;
							sessionInitStatus = (result = XenCollectorBase.SessionInitStatus.Success);
							return result;
						}
					}
					throw new DwmException(failure.Message, (sessionInitStatus != XenCollectorBase.SessionInitStatus.AuthenticationFailure) ? DwmErrorCode.XenCannotLogIn : DwmErrorCode.AuthenicationFailure, failure);
				}
			}
			catch (SocketException ex)
			{
				Logger.Trace("SocketException exception {0} trying start session on {1}:{2}", new object[]
				{
					ex.Message,
					hostName,
					this._hostPort
				});
				sessionInitStatus = XenCollectorBase.SessionInitStatus.SocketException;
				if (rethrowExceptions)
				{
					this.EndSession();
					throw new DwmException(ex.Message, DwmErrorCode.XenCannotLogIn, ex);
				}
				this.OnConnectionLost(ex);
			}
			catch (WebException ex2)
			{
				Logger.Trace("SocketException exception {0} trying start session on {1}:{2}", new object[]
				{
					ex2.Message,
					hostName,
					this._hostPort
				});
				sessionInitStatus = XenCollectorBase.SessionInitStatus.WebException;
				if (rethrowExceptions)
				{
					this.EndSession();
					throw new DwmException(Localization.Format("Cannot connect to Xen {0}:{1} hypervisor.  The most likely causes include the machine being offline or an incorrect TCP/IP address.", this._hostName, this._hostPort), DwmErrorCode.XenCannotConnect, ex2);
				}
				this.OnConnectionLost(ex2);
			}
			return sessionInitStatus;
		}
		public override void UnInitialize()
		{
			this.EndSession();
			base.UnInitialize();
		}
		public override void StartCollection()
		{
			base.StartCollection();
			this.RegisterForEvents();
		}
		public override void StopCollection()
		{
			base.StopCollection();
			this.EndSession();
		}
		private void RegisterForEvents()
		{
			bool flag = true;
			try
			{
				this._threadSession = new Session(this._session, 86400000);
				Event.register(this._threadSession, this._xenEventClasses);
			}
			catch (Failure failure)
			{
				Logger.Trace("Threw exception {0} registering for Xen events", new object[]
				{
					failure.Message
				});
				Logger.LogException(failure);
				flag = false;
			}
			if (flag)
			{
				if (this._eventThreadWaitHandles == null)
				{
					this._eventThreadWaitHandles = new WaitHandle[1];
					this._eventThreadWaitHandles[0] = new AutoResetEvent(false);
				}
				else
				{
					((AutoResetEvent)this._eventThreadWaitHandles[0]).Reset();
				}
				this._eventThread = new Thread(new ThreadStart(this.EventThreadProc));
				this._eventThread.Name = string.Format("XenCollectorEventThread(Pool {0})", this._poolId);
				this._eventThread.IsBackground = true;
				this._eventThread.Start();
			}
		}
		private void UnregisterForEvents()
		{
			if (this._eventThread != null && this._eventThreadWaitHandles != null && this._eventThreadWaitHandles[0] != null)
			{
				((AutoResetEvent)this._eventThreadWaitHandles[0]).Set();
			}
		}
		private void EventThreadProc()
		{
			bool flag = false;
			string[] array = new string[this._xenEventClasses.Length];
			this._xenEventClasses.CopyTo(array, 0);
			Logger.Trace("Handling events for {0}", new object[]
			{
				this._threadSession.Url
			});
			string token = string.Empty;
			bool flag2 = false;
			while (!flag)
			{
				try
				{
					XenCollectorBase._traceEventsEnabled = Configuration.GetValueAsBool(ConfigItems.DataEventTrace);
					int millisecondsTimeout = 1;
					if (WaitHandle.WaitAny(this._eventThreadWaitHandles, millisecondsTimeout, false) == 0)
					{
						flag = true;
					}
					else
					{
						flag2 = (this._threadSession.APIVersion <= API_Version.API_1_9);
						Proxy_Event[] array2;
						if (flag2)
						{
							array2 = Event.next(this._threadSession);
						}
						else
						{
							Events events = Event.from(this._threadSession, array, token, (double)this._sessionTimeout);
							token = events.token;
							array2 = events.events;
						}
						for (int i = 0; i < array2.Length; i++)
						{
							if (Localization.Compare(array2[i].class_, "host", true) == 0 || Localization.Compare(array2[i].class_, "vm", true) == 0 || Localization.Compare(array2[i].class_, "pool", true) == 0 || Localization.Compare(array2[i].class_, "task", true) == 0)
							{
								XenCollectorBase.Trace(XenCollectorBase._traceEventsEnabled, "XEN EVENT:  class: {0}   operation: {1}", new object[]
								{
									array2[i].class_,
									array2[i].operation
								});
								object key = "uuid";
								if (array2[i].snapshot != null)
								{
									XmlRpcStruct xmlRpcStruct = (XmlRpcStruct)array2[i].snapshot;
									if (xmlRpcStruct.ContainsKey(key))
									{
										string text = xmlRpcStruct[key].ToString();
										if (Localization.Compare(array2[i].class_, "host", true) == 0)
										{
											this.ProcessHostEvent(text, array2[i].operation, array2[i].opaqueRef, xmlRpcStruct);
										}
										else
										{
											if (Localization.Compare(array2[i].class_, "pool", true) == 0)
											{
												this.ProcessPoolEvent(text, array2[i].operation, array2[i].opaqueRef, xmlRpcStruct);
											}
											else
											{
												if (Localization.Compare(array2[i].class_, "vm", true) == 0)
												{
													this.ProcessVmEvent(text, array2[i].operation, array2[i].opaqueRef, xmlRpcStruct);
												}
												else
												{
													if (Localization.Compare(array2[i].class_, "task", true) == 0)
													{
														this.ProcessTaskEvent(text, array2[i].operation, array2[i].opaqueRef, xmlRpcStruct);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				catch (Failure failure)
				{
					Logger.Trace("Failure Exception in EventThreadProc for {0}", new object[]
					{
						(this._threadSession == null) ? "null session" : this._threadSession.Url
					});
					Logger.Trace("EventThreadProc using event.{0} method.", new object[]
					{
						(!flag2) ? "from" : "next"
					});
					Logger.LogException(failure);
					if (this.FailureToReason(failure) == CantBootReason.InvalidSession)
					{
						break;
					}
				}
				catch (IOException ex)
				{
					Logger.Trace("IOException in EventThreadProc for {0}", new object[]
					{
						(this._threadSession == null) ? "null session" : this._threadSession.Url
					});
					Logger.LogException(ex);
				}
				catch (SocketException ex2)
				{
					Logger.Trace("SocketException in EventThreadProc for {0}", new object[]
					{
						(this._threadSession == null) ? "null session" : this._threadSession.Url
					});
					Logger.LogException(ex2);
				}
				catch (WebException ex3)
				{
					if (ex3.Status == WebExceptionStatus.Timeout)
					{
						XenCollectorBase.Trace(XenCollectorBase._traceEventsEnabled, "Timeout exception in EventThreadProc.  Session={0}", new object[]
						{
							(this._threadSession == null) ? "null session" : this._threadSession.Url
						});
					}
					else
					{
						Logger.Trace("WebException in EventThreadProc for {0}", new object[]
						{
							(this._threadSession == null) ? "null session" : this._threadSession.Url
						});
						Logger.LogException(ex3);
					}
				}
				catch (ThreadAbortException)
				{
					Logger.Trace("Caught expected ThreadAbortException for {0}", new object[]
					{
						(this._threadSession == null) ? "null session" : this._threadSession.Url
					});
					Logger.Trace("No longer handling events for {0}", new object[]
					{
						(this._threadSession == null) ? "null session" : this._threadSession.Url
					});
					if (this._threadSession != null)
					{
						try
						{
							this._threadSession.logout();
							Logger.Trace("Event thread session.logout {0} succeeds", new object[]
							{
								(this._threadSession.Url == null) ? string.Empty : this._threadSession.Url
							});
						}
						catch (Exception ex4)
						{
							Logger.Trace("Event thread exception {0} calling _threadSession.logout", new object[]
							{
								ex4.Message
							});
						}
					}
					array = null;
					break;
				}
				catch (Exception ex5)
				{
					Logger.LogException(ex5);
				}
			}
			Logger.Trace("No longer handling events for {0}", new object[]
			{
				(this._threadSession == null) ? "null session" : this._threadSession.Url
			});
			if (this._threadSession != null)
			{
				try
				{
					this._threadSession.logout();
					Logger.Trace("Event thread session.logout {0} succeeds", new object[]
					{
						(this._threadSession.Url == null) ? string.Empty : this._threadSession.Url
					});
				}
				catch (Exception ex6)
				{
					Logger.Trace("Event thread exception {0} calling _threadSession.logout", new object[]
					{
						ex6.Message
					});
				}
			}
		}
		protected abstract void ProcessVmEvent(string vmUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot);
		protected abstract void ProcessHostEvent(string hostUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot);
		protected virtual void ProcessTaskEvent(string taskUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
		{
		}
		protected virtual void ProcessPoolEvent(string poolUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
		{
		}
		protected bool GetProperty(string propName, XmlRpcStruct snapshot, out string propValue)
		{
			bool result = true;
			if (snapshot.ContainsKey(propName))
			{
				propValue = snapshot[propName].ToString();
			}
			else
			{
				propValue = null;
				result = false;
			}
			return result;
		}
		protected bool HasPropertyChanged(string propName, XmlRpcStruct snapshot, string oldValue)
		{
			bool result = false;
			if (snapshot.ContainsKey(propName) && Localization.Compare(snapshot[propName].ToString(), oldValue, true) != 0)
			{
				result = true;
			}
			return result;
		}
		protected bool HasPropertyChanged<T>(string propName, XmlRpcStruct snapshot, List<XenRef<T>> oldValue) where T : XenObject<T>
		{
			bool result = false;
			if (snapshot.ContainsKey(propName))
			{
				object[] array = (object[])snapshot[propName];
				if (array.Length == oldValue.Count)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (Localization.Compare(array[i].ToString(), oldValue[i].opaque_ref, true) != 0)
						{
							result = true;
							break;
						}
					}
				}
				else
				{
					result = true;
				}
			}
			return result;
		}
		protected bool HasVmRebooted(XmlRpcStruct snapshot, VM xenVM)
		{
			bool flag = false;
			string s;
			int num;
			if (this.GetProperty("domid", snapshot, out s) && int.TryParse(s, out num))
			{
				object key = "guest_metrics";
				string text = (string)snapshot[key];
				if (XenCollectorBase.IsValidXenRef(text))
				{
					if (XenCollectorBase.IsValidXenRef<VM_guest_metrics>(xenVM.guest_metrics))
					{
						if (string.Compare(text, xenVM.guest_metrics.opaque_ref, true) != 0)
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				Logger.Trace("Refreshing cache for VM {0} ({1}) due to reboot.", new object[]
				{
					xenVM.name_label,
					xenVM.uuid
				});
			}
			if (XenCollectorBase._traceEventsEnabled)
			{
				XenCollectorBase.Trace(XenCollectorBase._traceEventsEnabled, "domid={0}", new object[]
				{
					(string)snapshot["domid"]
				});
				XenCollectorBase.Trace(XenCollectorBase._traceEventsEnabled, "guest_metrics={0}", new object[]
				{
					(string)snapshot["guest_metrics"]
				});
			}
			return flag;
		}
		protected static void TraceVmEvent(XmlRpcStruct snapshot, VM vm)
		{
		}
		protected override void ThreadProc()
		{
			bool flag = false;
			if (this._session != null)
			{
				Logger.Trace("Starting data collection for Xen Pool {0}.", new object[]
				{
					this._hostName
				});
				while (!flag && !this._externalStop)
				{
					int arg_57_0 = (this._pool != null && !this._pool.IsLicensed) ? this._noLicensePollInterval : this._pollInterval;
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
								Logger.LogException(ex);
							}
						}
					}
					else
					{
						flag = true;
					}
				}
				this.EndSession();
				Logger.Trace("Stopped data collection for Xen Pool {0}.", new object[]
				{
					this._hostName
				});
			}
		}
		protected bool Heartbeat()
		{
			try
			{
				if (this._session != null)
				{
					DateTime dateTime = Host.get_servertime(this._session, this._masterServerOpaqueRef);
					this._retryLostConnection = false;
					this._sessionTimeoutCount = 0;
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Trace("HEARTBEAT ({0}) - {1} Exception {2}", new object[]
				{
					this._session.Url,
					ex.GetType().Name,
					ex.Message
				});
				this.OnConnectionLost(ex);
			}
			return false;
		}
		protected void OnConnectionLost(Exception e)
		{
			int num = this._sessionTimeoutRetryInterval;
			if (e == null)
			{
				this._sessionTimeoutCount++;
				this._retryLostConnection = true;
			}
			else
			{
				if (e is WebException)
				{
					WebException ex = (WebException)e;
					if (ex.Status == WebExceptionStatus.Timeout)
					{
						this._sessionTimeoutCount++;
						Logger.Trace("OnConnectionLost:  _sessionTimeoutCount={0}", new object[]
						{
							this._sessionTimeoutCount
						});
					}
				}
				else
				{
					if (e is SocketException)
					{
						SocketException ex2 = (SocketException)e;
						if (ex2.SocketErrorCode == SocketError.TimedOut)
						{
							this._sessionTimeoutCount++;
							Logger.Trace("OnConnectionLost:  _sessionTimeoutCount={0}", new object[]
							{
								this._sessionTimeoutCount
							});
						}
					}
				}
			}
			if (this._sessionTimeoutCount != 1 || this._retryLostConnection)
			{
				this.EndSession();
			}
			if (this._sessionTimeoutCount > 10)
			{
				num = 10 * this._sessionTimeoutRetryInterval;
			}
			else
			{
				if (this._sessionTimeoutCount > 5)
				{
					num = 5 * this._sessionTimeoutRetryInterval;
				}
				else
				{
					if (this._sessionTimeoutCount > 2)
					{
						num = 2 * this._sessionTimeoutRetryInterval;
					}
					else
					{
						num = this._sessionTimeoutRetryInterval;
					}
				}
			}
			Logger.Trace("OnConnectionLost:  waiting={0} seconds before retrying", new object[]
			{
				num / 1000
			});
			int num2 = WaitHandle.WaitAny(this._waitHandles, num, false);
			if (num2 == 0)
			{
				this._externalStop = true;
			}
			else
			{
				if (num2 == 258)
				{
					if (this._retryLostConnection)
					{
						this._retryLostConnection = false;
						this.FindNewMaster();
					}
					else
					{
						this._retryLostConnection = true;
					}
				}
			}
		}
		protected virtual void EndSession()
		{
			Logger.Trace("Ending session {0}", new object[]
			{
				(this._session == null) ? "null" : this._session.Url
			});
			this.UnregisterForEvents();
			if (this._session != null)
			{
				try
				{
					this._session.logout();
					Logger.Trace("session.logout {0} succeeds", new object[]
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
				try
				{
					this._threadSession.logout();
					Logger.Trace("_threadSession.logout {0} succeeds", new object[]
					{
						(this._session.Url == null) ? string.Empty : this._session.Url
					});
				}
				catch (Exception)
				{
				}
				finally
				{
					this._threadSession = null;
				}
			}
		}
		protected virtual void FindNewMaster()
		{
			Logger.Trace("Trying to find new pool master ...");
			if (this._poolId > 0)
			{
				DwmPool dwmPool = new DwmPool(this._poolId);
				dwmPool.Load();
				foreach (DwmHost current in dwmPool.Hosts)
				{
					Logger.Trace("  Trying {0} ...", new object[]
					{
						current.IPAddress
					});
					XenCollectorBase.SessionInitStatus sessionInitStatus = this.InitializeSession(current.IPAddress, true, false);
					if (sessionInitStatus == XenCollectorBase.SessionInitStatus.Success)
					{
						break;
					}
					if (sessionInitStatus == XenCollectorBase.SessionInitStatus.HostIsSlave && dwmPool.Hosts.Count == 1)
					{
						Logger.Trace("Deleting pool {0} ({1}) ({2}) because the last host has left the pool", new object[]
						{
							dwmPool.Name,
							dwmPool.Id,
							dwmPool.Uuid
						});
						dwmPool.Delete();
					}
				}
			}
		}
		protected abstract void OnInterval();
		protected static bool IsValidXenRef<T>(XenRef<T> xenRef) where T : XenObject<T>
		{
			return xenRef != null && !string.IsNullOrEmpty(xenRef.opaque_ref) && Localization.Compare(xenRef.opaque_ref, "OpaqueRef:NULL", true) != 0;
		}
		protected static bool IsValidXenRef(string serverOpaqueRef)
		{
			return !string.IsNullOrEmpty(serverOpaqueRef) && Localization.Compare(serverOpaqueRef, "OpaqueRef:NULL", true) != 0;
		}
		protected CantBootReason FailureToReason(Failure f)
		{
			string s = f.ErrorDescription[0];
			CantBootReason result;
			if (Localization.Compare(s, "HANDLE_INVALID", true) == 0)
			{
				result = CantBootReason.HandleInvalid;
			}
			else
			{
				if (Localization.Compare(s, "HA_NO_PLAN", true) == 0)
				{
					result = CantBootReason.NoHAPlan;
				}
				else
				{
					if (Localization.Compare(s, "HA_OPERATION_WOULD_BREAK_FAILOVER_PLAN", true) == 0)
					{
						result = CantBootReason.HAOperationWouldBreakPlan;
					}
					else
					{
						if (Localization.Compare(s, "HOST_OFFLINE", true) == 0)
						{
							result = CantBootReason.HostOffline;
						}
						else
						{
							if (Localization.Compare(s, "HOST_STILL_BOOTING", true) == 0)
							{
								result = CantBootReason.HostStillBooting;
							}
							else
							{
								if (Localization.Compare(s, "INTERNAL_ERROR", true) == 0)
								{
									result = CantBootReason.InternalError;
								}
								else
								{
									if (Localization.Compare(s, "NO_HOSTS_AVAILABLE", true) == 0)
									{
										result = CantBootReason.NoHostAvailable;
									}
									else
									{
										if (Localization.Compare(s, "SESSION_AUTHENTICATION_FAILED", true) == 0)
										{
											result = CantBootReason.AuthenticationFailure;
										}
										else
										{
											if (Localization.Compare(s, "SESSION_INVALID", true) == 0)
											{
												result = CantBootReason.InvalidSession;
											}
											else
											{
												if (Localization.Compare(s, "SR_HAS_NO_PBDS", true) == 0)
												{
													result = CantBootReason.SrHasNoPbds;
												}
												else
												{
													if (Localization.Compare(s, "VM_BAD_POWER_STATE", true) == 0)
													{
														result = CantBootReason.VmBadPowerState;
													}
													else
													{
														if (Localization.Compare(s, "VM_REQUIRES_SR", true) == 0)
														{
															result = CantBootReason.VmRequiresSr;
														}
														else
														{
															if (Localization.Compare(s, "VM_REQUIRES_NETWORK", true) == 0)
															{
																result = CantBootReason.VmRequiresNetwork;
															}
															else
															{
																if (Localization.Compare(s, "VM_MISSING_PV_DRIVERS", true) == 0)
																{
																	result = CantBootReason.VmMissingDrivers;
																}
																else
																{
																	if (Localization.Compare(s, "HOST_NOT_ENOUGH_FREE_MEMORY", true) == 0)
																	{
																		result = CantBootReason.HostNotEnoughFreeMemory;
																	}
																	else
																	{
																		if (Localization.Compare(s, "SR_BACKEND_FAILURE_72", true) == 0)
																		{
																			result = CantBootReason.BackendFailure72;
																		}
																		else
																		{
																			if (Localization.Compare(s, "SR_BACKEND_FAILURE_140", true) == 0)
																			{
																				result = CantBootReason.BackendFailure140;
																			}
																			else
																			{
																				if (Localization.Compare(s, "SR_BACKEND_FAILURE_222", true) == 0)
																				{
																					result = CantBootReason.BackendFailure222;
																				}
																				else
																				{
																					if (Localization.Compare(s, "SR_BACKEND_FAILURE_225", true) == 0)
																					{
																						result = CantBootReason.BackendFailure225;
																					}
																					else
																					{
																						result = CantBootReason.Unknown;
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}
		protected static void Trace(bool traceFlag, string fmt, params object[] args)
		{
			if (traceFlag)
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
