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
	public class XenCollectorActions : XenCollectorBase, ICollectorActions
	{
		private Dictionary<string, string> _vmCache;
		private Dictionary<string, string> _hostCache;
		private object _vmCacheLock = new object();
		private object _hostCacheLock = new object();
		public XenCollectorActions()
		{
		}
		public XenCollectorActions(string hostname, int port, string username, string password) : base(hostname, port, "http", username, password)
		{
		}
		public void Start()
		{
			base.StartCollection();
		}
		public void Stop()
		{
			base.StopCollection();
		}
		protected override void ProcessVmEvent(string vmUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
		{
			if (this._vmCache != null)
			{
				if (Localization.Compare(operation, "del", true) == 0)
				{
					if (this._vmCache.ContainsKey(vmUuid))
					{
						this._vmCache.Remove(vmUuid);
					}
				}
				else
				{
					if (Localization.Compare(operation, "add", true) == 0)
					{
						if (!this._vmCache.ContainsKey(vmUuid))
						{
							this._vmCache.Add(vmUuid, serverOpaqueRef);
						}
						else
						{
							this._vmCache[vmUuid] = serverOpaqueRef;
						}
					}
				}
			}
		}
		protected override void ProcessHostEvent(string hostUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
		{
			if (this._hostCache != null)
			{
				if (Localization.Compare(operation, "del", true) == 0)
				{
					if (this._hostCache.ContainsKey(hostUuid))
					{
						this._hostCache.Remove(hostUuid);
					}
				}
				else
				{
					if (Localization.Compare(operation, "add", true) == 0)
					{
						if (!this._hostCache.ContainsKey(hostUuid))
						{
							this._hostCache.Add(hostUuid, serverOpaqueRef);
						}
						else
						{
							this._hostCache[hostUuid] = serverOpaqueRef;
						}
					}
				}
			}
		}
		protected override void EndSession()
		{
			base.EndSession();
			if (this._hostCache != null)
			{
				this._hostCache.Clear();
				this._hostCache = null;
			}
			if (this._vmCache != null)
			{
				this._vmCache.Clear();
				this._vmCache = null;
			}
		}
		public string GetPoolName()
		{
			string result = string.Empty;
			if (this.IsInitialized)
			{
				Dictionary<XenRef<Pool>, Pool> dictionary = Pool.get_all_records(this._session);
				foreach (KeyValuePair<XenRef<Pool>, Pool> current in dictionary)
				{
					result = current.Value.name_label;
				}
				return result;
			}
			throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the GetPoolName method", DwmErrorCode.NotInitialized, null);
		}
		public string GetPoolUniqueIdentifier()
		{
			string result = string.Empty;
			if (this.IsInitialized)
			{
				Dictionary<XenRef<Pool>, Pool> dictionary = Pool.get_all_records(this._session);
				foreach (KeyValuePair<XenRef<Pool>, Pool> current in dictionary)
				{
					result = current.Value.uuid;
				}
				return result;
			}
			throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the GetPoolUniqueIdentifier method", DwmErrorCode.NotInitialized, null);
		}
		public void GetCerts()
		{
			Session session = null;
			try
			{
				session = new Session(this._hostName, this._hostPort);
				session.login_with_password(this._username, this._password);
			}
			catch (Failure failure)
			{
				throw new DwmException(failure.Message, DwmErrorCode.XenCannotLogIn, failure);
			}
			catch (SocketException ex)
			{
				throw new DwmException(ex.Message, DwmErrorCode.XenCannotLogIn, ex);
			}
			catch (WebException innerException)
			{
				throw new DwmException(Localization.Format("Cannot connect to Xen {0}:{1} hypervisor.  The most likely causes include the machine being offline or an incorrect TCP/IP address.", this._hostName, this._hostPort), DwmErrorCode.XenCannotConnect, innerException);
			}
			try
			{
				Dictionary<XenRef<Host>, Host> dictionary = Host.get_all_records(session);
				foreach (KeyValuePair<XenRef<Host>, Host> current in dictionary)
				{
					Host value = current.Value;
					string value2 = Host.get_server_certificate(session, current.Key);
					string text = string.Format("{0}\\{1}.cer", Logger.GetLogDirectory(), current.Value.name_label);
					char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
					for (int i = 0; i < invalidFileNameChars.Length; i++)
					{
						if (text.IndexOf(invalidFileNameChars[i]) != -1)
						{
							text.Replace(invalidFileNameChars[i], '_');
						}
					}
					using (StreamWriter streamWriter = new StreamWriter(text))
					{
						streamWriter.Write(value2);
						streamWriter.Close();
					}
				}
				session.logout();
			}
			catch (Failure ex2)
			{
				Logger.LogException(ex2);
			}
			catch (SocketException ex3)
			{
				Logger.LogException(ex3);
			}
			catch (WebException ex4)
			{
				Logger.LogException(ex4);
			}
		}
		protected override void OnInterval()
		{
			if (this._session == null && base.InitializeSession(this._hostName, true, false) != XenCollectorBase.SessionInitStatus.Success)
			{
				base.OnConnectionLost(null);
			}
			if (this._session != null && base.Heartbeat())
			{
				object hostCacheLock = this._hostCacheLock;
				Monitor.Enter(hostCacheLock);
				try
				{
					if (this._hostCache == null)
					{
						this.GetHosts();
					}
				}
				finally
				{
					Monitor.Exit(hostCacheLock);
				}
				object vmCacheLock = this._vmCacheLock;
				Monitor.Enter(vmCacheLock);
				try
				{
					if (this._vmCache == null)
					{
						this.GetVMs();
					}
				}
				finally
				{
					Monitor.Exit(vmCacheLock);
				}
			}
		}
		private void GetHosts()
		{
			try
			{
				if (this._session != null)
				{
					Dictionary<XenRef<Host>, Host> dictionary = Host.get_all_records(this._session);
					if (dictionary != null)
					{
						this._hostCache = new Dictionary<string, string>();
						foreach (KeyValuePair<XenRef<Host>, Host> current in dictionary)
						{
							this._hostCache.Add(current.Value.uuid, current.Key.opaque_ref);
						}
					}
				}
			}
			catch (Failure ex)
			{
				Logger.LogException(ex);
			}
			catch (SocketException ex2)
			{
				Logger.LogException(ex2);
			}
			catch (WebException ex3)
			{
				Logger.LogException(ex3);
			}
		}
		private void GetVMs()
		{
			try
			{
				if (this._session != null)
				{
					Dictionary<XenRef<VM>, VM> dictionary = VM.get_all_records(this._session);
					if (dictionary != null)
					{
						this._vmCache = new Dictionary<string, string>();
						foreach (KeyValuePair<XenRef<VM>, VM> current in dictionary)
						{
							this._vmCache.Add(current.Value.uuid, current.Key.opaque_ref);
						}
					}
				}
			}
			catch (Failure ex)
			{
				Logger.LogException(ex);
			}
			catch (SocketException ex2)
			{
				Logger.LogException(ex2);
			}
			catch (WebException ex3)
			{
				Logger.LogException(ex3);
			}
		}
		public CantBootReason CanStartVM(string vmUuid, string hostUuid)
		{
			CantBootReason result = CantBootReason.None;
			Logger.Trace("ICollectorActions.CanStartVM called for VM {0}, Host {1}", new object[]
			{
				vmUuid,
				hostUuid
			});
			if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(hostUuid))
			{
				if (this.IsInitialized)
				{
					string hostRefForUuid = this.GetHostRefForUuid(hostUuid);
					string text = null;
					if (hostRefForUuid != null)
					{
						text = this.GetVmRefForUuid(vmUuid);
					}
					if (text != null && hostRefForUuid != null)
					{
						try
						{
							VM.assert_can_boot_here(this._session, text, hostRefForUuid);
							Logger.Trace("VM.assert_can_boot_here returns true.  VM={0}  Host={1}", new object[]
							{
								vmUuid,
								hostUuid
							});
						}
						catch (Failure failure)
						{
							Logger.Trace("VM.assert_can_boot_here fails with {0}.  VM={1}  Host={2}", new object[]
							{
								failure.Message,
								vmUuid,
								hostUuid
							});
							result = base.FailureToReason(failure);
						}
						catch (SocketException)
						{
							result = CantBootReason.SocketException;
						}
						catch (WebException)
						{
							result = CantBootReason.WebException;
						}
						catch (Exception)
						{
							result = CantBootReason.GeneralException;
						}
					}
					else
					{
						Logger.Trace("ICollectorActions.CanStartVM - failure - vmServerRef or hostServerRef is not initialized properly");
					}
				}
				else
				{
					Logger.Trace("ICollectorActions.CanStartVM - Data collector not properly initialized");
				}
			}
			return result;
		}
		public int StartVM(string vmUuid, string hostUuid, bool startPaused)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(hostUuid))
			{
				if (this.IsInitialized)
				{
					string hostRefForUuid = this.GetHostRefForUuid(hostUuid);
					string text = null;
					if (hostRefForUuid != null)
					{
						text = this.GetVmRefForUuid(vmUuid);
					}
					if (text != null && hostRefForUuid != null)
					{
						try
						{
							VM.start_on(this._session, text, hostRefForUuid, startPaused, true);
						}
						catch (Failure ex)
						{
							Logger.LogError("Exception starting VM {0} to host {1}", new object[]
							{
								vmUuid,
								hostUuid
							});
							Logger.LogException(ex);
							result = 1;
						}
					}
					else
					{
						result = 4007;
					}
				}
				else
				{
					result = 4012;
				}
			}
			else
			{
				result = 4007;
			}
			return result;
		}
		public int MigrateVM(string vmUuid, string migrateToHostUuid, int recommendationId, bool liveMigration)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(migrateToHostUuid))
			{
				if (this.IsInitialized)
				{
					string hostRefForUuid = this.GetHostRefForUuid(migrateToHostUuid);
					string text = null;
					if (hostRefForUuid != null)
					{
						text = this.GetVmRefForUuid(vmUuid);
					}
					if (text != null && hostRefForUuid != null)
					{
						int timeout = this._session.proxy.Timeout;
						try
						{
							int num = 120;
							int num2 = Configuration.GetValueAsInt(ConfigItems.VmMigrateTimeout, num);
							num2 = ((num2 <= 0) ? num : num2);
							this._session.proxy.Timeout = num2 * 1000;
							Dictionary<string, string> dictionary = new Dictionary<string, string>();
							dictionary["live"] = "true";
							XenRef<Task> xenRef = VM.async_pool_migrate(this._session, text, hostRefForUuid, dictionary);
							if (recommendationId != 0)
							{
								Task.add_to_other_config(this._session, xenRef.opaque_ref, "wlb_advised", recommendationId.ToString());
								Task.add_to_other_config(this._session, xenRef.opaque_ref, "wlb_action", "vm_migrate");
								Task.add_to_other_config(this._session, xenRef.opaque_ref, "wlb_action_obj_ref", text);
								Task.add_to_other_config(this._session, xenRef.opaque_ref, "wlb_action_obj_type", "vm");
							}
							task_status_type status;
							for (status = Task.get_record(this._session, xenRef.opaque_ref).status; status == task_status_type.pending; status = Task.get_record(this._session, xenRef.opaque_ref).status)
							{
								Thread.Sleep(2000);
							}
							if (status != task_status_type.success)
							{
								throw new WebException();
							}
							Logger.Trace("Auto Migrate VM: migrated vm - {0} to host - {1}, recommendation id - {2}", new object[]
							{
								VM.get_name_label(this._session, text),
								Host.get_name_label(this._session, hostRefForUuid),
								recommendationId.ToString()
							});
						}
						catch (Failure failure)
						{
							Logger.LogError("Exception moving VM {0} to host {1}", new object[]
							{
								vmUuid,
								migrateToHostUuid
							});
							Logger.LogException(failure);
							if (Localization.Compare(failure.Message, "NOT_IMPLEMENTED", true) == 0)
							{
								result = 4006;
							}
							else
							{
								result = 1;
							}
						}
						catch (WebException ex)
						{
							Logger.LogError("Exception moving VM {0} to host {1}", new object[]
							{
								vmUuid,
								migrateToHostUuid
							});
							Logger.LogException(ex);
							result = 1;
						}
						finally
						{
							this._session.proxy.Timeout = timeout;
						}
					}
					else
					{
						result = 4007;
					}
				}
				else
				{
					result = 4012;
				}
			}
			else
			{
				result = 4007;
			}
			return result;
		}
		public void SendMessage(string category, string message)
		{
		}
		public int PowerHostOff(string hostUuid)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(hostUuid))
			{
				if (this.IsInitialized)
				{
					string hostRefForUuid = this.GetHostRefForUuid(hostUuid);
					string uuid = this._session.uuid;
					if (hostUuid == uuid)
					{
						Logger.LogError("Host Power off cancelled. The specified host {0} is the master.", new object[]
						{
							hostUuid
						});
					}
					else
					{
						if (hostRefForUuid != null)
						{
							bool flag = false;
							try
							{
								Host.disable(this._session, hostRefForUuid);
								Host.shutdown(this._session, hostRefForUuid);
								flag = true;
							}
							catch (Failure failure)
							{
								Logger.LogError("Failure Exception shutting down host {0}", new object[]
								{
									hostUuid
								});
								Logger.LogException(failure);
								result = (int)base.FailureToReason(failure);
							}
							catch (Exception ex)
							{
								Logger.LogError("Exception shutting down host {0}", new object[]
								{
									hostUuid
								});
								Logger.LogException(ex);
							}
							finally
							{
								if (!flag)
								{
									try
									{
										Host.enable(this._session, hostRefForUuid);
									}
									catch (Failure)
									{
									}
								}
							}
						}
						else
						{
							result = 4007;
						}
					}
				}
				else
				{
					result = 4012;
				}
			}
			else
			{
				result = 4007;
			}
			return result;
		}
		public int PowerHostOn(string hostUuid)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(hostUuid))
			{
				if (this.IsInitialized)
				{
					string hostRefForUuid = this.GetHostRefForUuid(hostUuid);
					if (hostRefForUuid != null)
					{
						int timeout = this._session.proxy.Timeout;
						try
						{
							int num = 600;
							int num2 = Configuration.GetValueAsInt(ConfigItems.HostPowerOnTimeout, num);
							num2 = ((num2 <= 0) ? num : num2);
							this._session.proxy.Timeout = num2 * 1000;
							XenRef<Task> xenRef = Host.async_power_on(this._session, hostRefForUuid);
							task_status_type status = Task.get_record(this._session, xenRef.opaque_ref).status;
							int num3 = 0;
							while (status == task_status_type.pending && num3 != 120)
							{
								Thread.Sleep(5000);
								status = Task.get_record(this._session, xenRef.opaque_ref).status;
								num3++;
							}
						}
						catch (Failure failure)
						{
							Logger.LogError("Failure Exception powering on host {0}", new object[]
							{
								hostUuid
							});
							Logger.LogException(failure);
							result = (int)base.FailureToReason(failure);
						}
						catch (Exception ex)
						{
							Logger.LogError("Exception powering on host {0}", new object[]
							{
								hostUuid
							});
							Logger.LogException(ex);
							result = 4014;
						}
						bool flag = false;
						int num4 = 0;
						while (!flag && num4 <= 5)
						{
							try
							{
								num4++;
								Host.enable(this._session, hostRefForUuid);
								flag = true;
							}
							catch (Failure failure2)
							{
								if (Localization.Compare(failure2.Message, "HOST_STILL_BOOTING", true) == 0 && num4 < 5)
								{
									Logger.LogWarning("Unable to enable host {0} becuase it is still booting.  Retrying in 5 seconds...", new object[0]);
									Thread.Sleep(5000);
								}
								else
								{
									Logger.LogError("Failure Exception powering on host {0}", new object[]
									{
										hostUuid
									});
									Logger.LogException(failure2);
									result = (int)base.FailureToReason(failure2);
								}
							}
							catch (Exception ex2)
							{
								Logger.LogError("Exception powering on host {0}", new object[]
								{
									hostUuid
								});
								Logger.LogException(ex2);
								result = 4014;
							}
						}
						this._session.proxy.Timeout = timeout;
					}
					else
					{
						result = 4007;
					}
				}
				else
				{
					result = 4012;
				}
			}
			else
			{
				result = 4007;
			}
			return result;
		}
		private string GetHostRefForUuid(string uuid)
		{
			string result = null;
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				if (this._hostCache == null)
				{
					this.GetHosts();
				}
				if (this._hostCache != null)
				{
					this._hostCache.TryGetValue(uuid, out result);
				}
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
			return result;
		}
		private string GetVmRefForUuid(string uuid)
		{
			string result = null;
			object vmCacheLock = this._vmCacheLock;
			Monitor.Enter(vmCacheLock);
			try
			{
				if (this._vmCache == null)
				{
					this.GetVMs();
				}
				if (this._vmCache != null)
				{
					this._vmCache.TryGetValue(uuid, out result);
				}
			}
			finally
			{
				Monitor.Exit(vmCacheLock);
			}
			return result;
		}
	}
}
