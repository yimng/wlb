using Halsign.DWM.Domain;
using Halsign.DWM.Framework;
using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using XenAPI;
namespace Halsign.DWM.Collectors
{
	public class XenCollector : XenCollectorBase, ICollector
	{
		private const int XEN_5 = 5;
		private const int HOST_COMMUNICATION_RETRY_COUNT = 5;
		private const string XEN_COLLECTOR_NAME = "__XEN_DATA_COLLECTOR__";
		private static NumberFormatInfo _nfi = new CultureInfo("en-US", false).NumberFormat;
		private object _hostCacheLock = new object();
		private object _vmCacheLock = new object();
		private object _netCacheLock = new object();
		private object _pbdCacheLock = new object();
		private object _pifCacheLock = new object();
		private object _srCacheLock = new object();
		private object _vbdCacheLock = new object();
		private object _vdiCacheLock = new object();
		private object _vifCacheLock = new object();
		private Dictionary<string, HostCacheItem> _hostCache;
		private Dictionary<string, VmCacheItem> _vmCache;
		private Dictionary<string, VM> _vmCache2;
		private Dictionary<string, Network> _networkCache;
		private Dictionary<string, PBD> _pbdCache;
		private Dictionary<string, PIF> _pifCache;
		private Dictionary<string, SR> _srCache;
		private Dictionary<string, VBD> _vbdCache;
		private Dictionary<string, VDI> _vdiCache;
		private Dictionary<string, VIF> _vifCache;
		private static Dictionary<string, HostHttpError> _hostHttpErrors;
		private bool _isInitialRun = true;
		private DateTime _lastLicenseCheck = DateTime.MinValue;
		private DateTime _lastValidPoolCheck = DateTime.MinValue;
		public XenCollector()
		{
		}
		public XenCollector(string hostname, int port, string protocol, string username, string password) : base(hostname, port, protocol, username, password)
		{
		}
		protected void ClearCache()
		{
			Logger.Trace("Clearing collector cache...");
			this._hostCache.Clear();
			this._vmCache.Clear();
			this._networkCache.Clear();
			this._pbdCache.Clear();
			this._pifCache.Clear();
			this._srCache.Clear();
			this._vbdCache.Clear();
			this._vdiCache.Clear();
			this._vifCache.Clear();
		}
		protected override void ProcessVmEvent(string vmUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
		{
			bool flag = false;
			int recommendationId = 0;
			object vmCacheLock = this._vmCacheLock;
			Monitor.Enter(vmCacheLock);
			try
			{
				if (this._vmCache != null)
				{
					if (Localization.Compare(operation, "mod", true) == 0 || Localization.Compare(operation, "del", true) == 0)
					{
						VmCacheItem vmCacheItem;
						if (this._vmCache.TryGetValue(vmUuid, out vmCacheItem) && vmCacheItem != null && vmCacheItem.xenVM != null)
						{
							XenCollectorBase.TraceVmEvent(snapshot, vmCacheItem.xenVM);
							if (Localization.Compare(operation, "mod", true) == 0)
							{
								string text;
								base.GetProperty("resident_on", snapshot, out text);
								string text2;
								base.GetProperty("power_state", snapshot, out text2);
								if (text != null)
								{
									if (Localization.Compare(text, vmCacheItem.xenVM.resident_on.opaque_ref, true) != 0)
									{
										Logger.Trace("residentOn - snapshot: {0}  VM: {1}", new object[]
										{
											text,
											vmCacheItem.xenVM.resident_on.opaque_ref
										});
										if (XenCollectorBase.IsValidXenRef(text))
										{
											Host hostRecord = XenCollector.GetHostRecord(this._session, text);
											if (hostRecord != null && this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
											{
												DwmVirtualMachine.SetRunningOnHost(vmUuid, hostRecord.uuid, this._pool.Uuid, vmCacheItem.pendingRecommendationId);
												flag = true;
											}
										}
										else
										{
											if (text2 != null && Localization.Compare(text2, vmCacheItem.xenVM.power_state.ToString(), true) != 0 && this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
											{
												DwmVirtualMachine.SetRunningOnHost(vmUuid, null, this._pool.Uuid, vmCacheItem.pendingRecommendationId);
												flag = true;
											}
										}
									}
									else
									{
										if (Localization.Compare(text2, vmCacheItem.xenVM.power_state.ToString(), true) != 0 && Localization.Compare(text2, "Running", true) == 0)
										{
											flag = true;
										}
									}
								}
								if (!flag && !base.HasVmRebooted(snapshot, vmCacheItem.xenVM) && !base.HasPropertyChanged("name_label", snapshot, vmCacheItem.xenVM.name_label) && !base.HasPropertyChanged("name_description", snapshot, vmCacheItem.xenVM.name_description) && !base.HasPropertyChanged("memory_dynamic_min", snapshot, vmCacheItem.xenVM.memory_dynamic_min.ToString()) && !base.HasPropertyChanged("memory_dynamic_max", snapshot, vmCacheItem.xenVM.memory_dynamic_max.ToString()) && !base.HasPropertyChanged("memory_static_min", snapshot, vmCacheItem.xenVM.memory_static_min.ToString()) && !base.HasPropertyChanged("memory_static_max", snapshot, vmCacheItem.xenVM.memory_static_max.ToString()) && !base.HasPropertyChanged("memory_target", snapshot, vmCacheItem.xenVM.memory_target.ToString()) && !base.HasPropertyChanged("memory_overhead", snapshot, vmCacheItem.xenVM.memory_overhead.ToString()) && !base.HasPropertyChanged("VCPUs_at_startup", snapshot, vmCacheItem.xenVM.VCPUs_at_startup.ToString()) && !base.HasPropertyChanged<VBD>("VBDs", snapshot, vmCacheItem.xenVM.VBDs))
								{
									if (!base.HasPropertyChanged("is_a_template", snapshot, vmCacheItem.xenVM.is_a_template.ToString()))
									{
										goto IL_3CC;
									}
								}
								try
								{
									VM vmRecord = XenCollector.GetVmRecord(this._session, vmCacheItem.serverOpaqueRef);
									if (vmRecord != null)
									{
										this.DiscoverVm(this._session, vmCacheItem.serverOpaqueRef, vmRecord, true);
									}
								}
								catch (Failure)
								{
								}
								IL_3CC:;
							}
							else
							{
								if (Localization.Compare(operation, "del", true) == 0 && this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
								{
									DwmVirtualMachine.DeleteVM(vmUuid, this._pool.Uuid);
								}
							}
						}
					}
					else
					{
						if (Localization.Compare(operation, "add", true) == 0)
						{
							Logger.Trace("adding VM {0}", new object[]
							{
								serverOpaqueRef
							});
							string text3;
							base.GetProperty("resident_on", snapshot, out text3);
							string text4;
							base.GetProperty("power_state", snapshot, out text4);
							Logger.Trace("powerState - {0}", new object[]
							{
								text4
							});
							Logger.Trace("residentOn - {0}", new object[]
							{
								text3
							});
							VM vmRecord2 = XenCollector.GetVmRecord(this._session, serverOpaqueRef);
							if (vmRecord2 != null)
							{
								this.DiscoverVm(this._session, serverOpaqueRef, vmRecord2, true);
								if (XenCollectorBase.IsValidXenRef(text3) && Localization.Compare(text4, vm_power_state.Running.ToString(), true) == 0)
								{
									Host hostRecord2 = XenCollector.GetHostRecord(this._session, text3);
									if (hostRecord2 != null && this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
									{
										DwmVirtualMachine.SetRunningOnHost(vmUuid, hostRecord2.uuid, this._pool.Uuid, recommendationId);
									}
								}
								else
								{
									VmCacheItem vmCacheItem;
									if (this._vmCache.TryGetValue(vmUuid, out vmCacheItem) && vmCacheItem != null && vmCacheItem.xenVM != null && XenCollectorBase.IsValidXenRef<Host>(vmCacheItem.xenVM.resident_on) && vmCacheItem.xenVM.power_state == vm_power_state.Running)
									{
										Host hostRecord3 = XenCollector.GetHostRecord(this._session, vmCacheItem.xenVM.resident_on);
										if (hostRecord3 != null && this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
										{
											DwmVirtualMachine.SetRunningOnHost(vmUuid, hostRecord3.uuid, this._pool.Uuid, recommendationId);
										}
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				Monitor.Exit(vmCacheLock);
			}
		}
		protected override void ProcessHostEvent(string hostUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
		{
			if (this._hostCache != null)
			{
				if (Localization.Compare(operation, "mod", true) == 0)
				{
					this.ProcessHostModEvent(snapshot, hostUuid, serverOpaqueRef);
				}
				else
				{
					if (Localization.Compare(operation, "add", true) == 0)
					{
						if (!this.HostCacheContainsKey(hostUuid))
						{
							this.DiscoverHost(this._session, hostUuid, true, true);
						}
					}
					else
					{
						if (Localization.Compare(operation, "del", true) == 0)
						{
							object hostCacheLock = this._hostCacheLock;
							Monitor.Enter(hostCacheLock);
							try
							{
								if (this._hostCache.ContainsKey(hostUuid))
								{
									this._hostCache.Remove(hostUuid);
								}
							}
							finally
							{
								Monitor.Exit(hostCacheLock);
							}
							if (this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
							{
								DwmHost.DeleteHost(hostUuid, this._pool.Uuid);
							}
						}
					}
				}
			}
		}
		protected void ProcessHostModEvent(XmlRpcStruct snapshot, string hostUuid, string serverOpaqueRef)
		{
			bool flag = false;
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				HostCacheItem hostCacheItem;
				if (this._hostCache.TryGetValue(hostUuid, out hostCacheItem) && hostCacheItem != null && hostCacheItem.xenHost != null)
				{
					object key = "enabled";
					if (snapshot.ContainsKey(key))
					{
						bool flag2 = bool.Parse(snapshot[key].ToString());
						if (hostCacheItem.xenHost.enabled != flag2 && this._pool != null && !string.IsNullOrEmpty(this._pool.Uuid))
						{
							Logger.Trace("Processing enable changed to {0} for host {1}", new object[]
							{
								flag2,
								hostUuid
							});
							DwmHost.SetEnabled(hostUuid, this._pool.Uuid, flag2);
							hostCacheItem.xenHost.enabled = flag2;
							hostCacheItem.dwmHost.Enabled = flag2;
						}
					}
					bool flag3 = false;
					key = "host_CPUs";
					if (snapshot.ContainsKey(key))
					{
						object[] array = (object[])snapshot[key];
						if (array.Length != hostCacheItem.xenHost.host_CPUs.Count)
						{
							Logger.Trace("Processing CPUs changed to {0} for host {1}", new object[]
							{
								array.Length,
								hostUuid
							});
							flag3 = true;
						}
					}
					key = "PBDs";
					if (snapshot.ContainsKey(key))
					{
						object[] array2 = (object[])snapshot[key];
						if (array2.Length != hostCacheItem.xenHost.PBDs.Count)
						{
							Logger.Trace("Processing PBDs changed to {0} for host {1}", new object[]
							{
								array2.Length,
								hostUuid
							});
							flag3 = true;
						}
					}
					key = "PIFs";
					if (snapshot.ContainsKey(key))
					{
						object[] array3 = (object[])snapshot[key];
						if (array3.Length != hostCacheItem.xenHost.PIFs.Count)
						{
							Logger.Trace("Processing PIFs changed to {0} for host {1}", new object[]
							{
								array3.Length,
								hostUuid
							});
							flag3 = true;
						}
					}
					key = "software_version";
					if (snapshot.ContainsKey(key))
					{
						XmlRpcStruct xmlRpcStruct = (XmlRpcStruct)snapshot[key];
						if (xmlRpcStruct.Count != hostCacheItem.xenHost.software_version.Count)
						{
							Logger.Trace("Processing software version changed to {0} for host {1}", new object[]
							{
								xmlRpcStruct.Count,
								hostUuid
							});
							flag3 = true;
						}
					}
					key = "address";
					if (snapshot.ContainsKey(key))
					{
						string text = (string)snapshot[key];
						if (Localization.Compare(text, hostCacheItem.xenHost.address, true) != 0)
						{
							Logger.Trace("Processing adress changed to {0} for host {1}", new object[]
							{
								text,
								hostUuid
							});
							flag3 = true;
						}
					}
					key = "name_label";
					if (snapshot.ContainsKey(key))
					{
						string text2 = (string)snapshot[key];
						if (Localization.Compare(text2, hostCacheItem.xenHost.name_label, true) != 0)
						{
							Logger.Trace("Processing name_label changed to {0} for host {1}", new object[]
							{
								text2,
								hostUuid
							});
							flag3 = true;
						}
					}
					key = "name_description";
					if (snapshot.ContainsKey(key))
					{
						string text3 = (string)snapshot[key];
						if (Localization.Compare(text3, hostCacheItem.xenHost.name_description, true) != 0)
						{
							Logger.Trace("Processing name_description changed to {0} for host {1}", new object[]
							{
								text3,
								hostUuid
							});
							flag3 = true;
						}
					}
					PowerStatus powerStatus = (!Host_metrics.get_live(this._session, hostCacheItem.xenHost.metrics.opaque_ref)) ? PowerStatus.Off : PowerStatus.On;
					if (powerStatus != hostCacheItem.dwmHost.PowerState)
					{
						flag3 = true;
						flag = true;
					}
					if (flag3)
					{
						Host hostRecord;
						if (flag)
						{
							List<string> list = new List<string>();
							foreach (KeyValuePair<string, HostCacheItem> current in this._hostCache)
							{
								if (current.Value.serverOpaqueRef != serverOpaqueRef)
								{
									list.Add(current.Value.serverOpaqueRef);
								}
							}
							foreach (string current2 in list)
							{
								hostRecord = XenCollector.GetHostRecord(this._session, current2);
								if (hostRecord != null)
								{
									this.DiscoverHost(this._session, current2, hostRecord, true, false);
								}
							}
						}
						hostRecord = XenCollector.GetHostRecord(this._session, serverOpaqueRef);
						if (hostRecord != null)
						{
							DwmHost dwmHost = this.DiscoverHost(this._session, serverOpaqueRef, hostRecord, true, true);
							hostCacheItem.dwmHost = dwmHost;
							if (hostCacheItem.pendingPoweredOffByWlb && flag)
							{
								DwmHost.SetPoweredOffByWlb(dwmHost.Id, true);
							}
						}
					}
				}
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
		}
		protected override void ProcessTaskEvent(string taskUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
		{
			object key = "other_config";
			string text = string.Empty;
			int pendingRecommendationId = 0;
			if (XenCollectorBase._traceEventsEnabled)
			{
				object key2 = "name_label";
				object key3 = "status";
				XenCollectorBase.Trace(XenCollectorBase._traceEventsEnabled, "Task {0}.  Status={1}", new object[]
				{
					(string)snapshot[key2],
					(string)snapshot[key3]
				});
			}
			if (snapshot.ContainsKey(key))
			{
				XmlRpcStruct xmlRpcStruct = (XmlRpcStruct)snapshot[key];
				if (xmlRpcStruct.Count > 0)
				{
					key = "wlb_advised";
					if (xmlRpcStruct.ContainsKey(key) && int.TryParse((string)xmlRpcStruct[key], out pendingRecommendationId))
					{
						key = "wlb_action";
						if (xmlRpcStruct.ContainsKey(key))
						{
							text = (string)xmlRpcStruct[key];
						}
						key = "wlb_action_obj_type";
						if (xmlRpcStruct.ContainsKey(key))
						{
							string s = (string)xmlRpcStruct[key];
							if (Localization.Compare(s, "VM", true) == 0)
							{
								key = "wlb_action_obj_ref";
								if (xmlRpcStruct.ContainsKey(key))
								{
									string xenRef = (string)xmlRpcStruct[key];
									VmCacheItem cachedVmByXenRef = this.GetCachedVmByXenRef(xenRef);
									string s2;
									if (cachedVmByXenRef != null && base.GetProperty("status", snapshot, out s2))
									{
										if (Localization.Compare(s2, "pending", true) == 0)
										{
											cachedVmByXenRef.pendingRecommendationId = pendingRecommendationId;
										}
										else
										{
											cachedVmByXenRef.pendingRecommendationId = 0;
										}
									}
								}
							}
							else
							{
								if (Localization.Compare(s, "HOST", true) == 0)
								{
									key = "wlb_action_obj_ref";
									if (xmlRpcStruct.ContainsKey(key))
									{
										string xenRef = (string)xmlRpcStruct[key];
										HostCacheItem cachedHostByXenRef = this.GetCachedHostByXenRef(xenRef);
										string s2;
										if (base.GetProperty("status", snapshot, out s2) && Localization.Compare(s2, "pending", true) == 0)
										{
											cachedHostByXenRef.pendingRecommendationId = pendingRecommendationId;
											cachedHostByXenRef.pendingPoweredOffByWlb = true;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		protected override void ProcessPoolEvent(string poolUuid, string operation, string serverOpaqueRef, XmlRpcStruct snapshot)
		{
			object key = "wlb_enabled";
			if (Localization.Compare(operation, "mod", true) == 0 && snapshot != null && snapshot.ContainsKey(key))
			{
				bool flag = (bool)snapshot[key];
				if (this._pool != null && this._pool.Enabled != flag)
				{
					this._pool.SetEnabled(flag);
					Logger.Trace("Enabled state of pool {0} ({1}) set to {2}", new object[]
					{
						this._pool.Name,
						this._pool.Uuid,
						flag
					});
				}
			}
		}
		private bool OperationAllowed(object[] operations, string operationToFind)
		{
			bool result = false;
			for (int i = 0; i < operations.Length; i++)
			{
				if (Localization.Compare(operations[i].ToString(), operationToFind, true) == 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}
		private bool OperationAllowed(string[] operations, string operationToFind)
		{
			bool result = false;
			for (int i = 0; i < operations.Length; i++)
			{
				if (Localization.Compare(operations[i], operationToFind, true) == 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}
		private VmCacheItem GetCachedVmByXenRef(string xenRef)
		{
			VmCacheItem result = null;
			object vmCacheLock = this._vmCacheLock;
			Monitor.Enter(vmCacheLock);
			try
			{
				foreach (KeyValuePair<string, VmCacheItem> current in this._vmCache)
				{
					if (Localization.Compare(current.Value.serverOpaqueRef, xenRef, true) == 0)
					{
						result = current.Value;
						break;
					}
				}
			}
			finally
			{
				Monitor.Exit(vmCacheLock);
			}
			return result;
		}
		private HostCacheItem GetCachedHostByXenRef(string xenRef)
		{
			HostCacheItem result = null;
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				foreach (KeyValuePair<string, HostCacheItem> current in this._hostCache)
				{
					if (Localization.Compare(current.Value.serverOpaqueRef, xenRef, true) == 0)
					{
						result = current.Value;
						break;
					}
				}
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
			return result;
		}
		private void GetHosts()
		{
			try
			{
				object hostCacheLock = this._hostCacheLock;
				Monitor.Enter(hostCacheLock);
				try
				{
					if ((this._hostCache == null || this._hostCache.Count == 0) && this._session != null)
					{
						Dictionary<XenRef<Host>, Host> dictionary = Host.get_all_records(this._session);
						if (dictionary != null)
						{
							foreach (KeyValuePair<XenRef<Host>, Host> current in dictionary)
							{
								if (!this._hostCache.ContainsKey(current.Value.uuid))
								{
									this.DiscoverHost(this._session, current.Key.opaque_ref, current.Value, true, true);
								}
							}
						}
					}
				}
				finally
				{
					Monitor.Exit(hostCacheLock);
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
		private int GetHostVersion(Host xenHost, out int verMinor, out int verBuild)
		{
			int result = 5;
			verMinor = 0;
			verBuild = 0;
			if (xenHost != null && xenHost.software_version != null)
			{
				string text = null;
				if (xenHost.software_version.TryGetValue("product_version", out text) && !string.IsNullOrEmpty(text))
				{
					char[] separator = new char[]
					{
						'.'
					};
					string[] array = text.Split(separator);
                    if ((result = Localization.TryParse(array[0])) == 0)
                        result = 5;
					if (array.Length > 1)
					{
						verMinor = Localization.TryParse(array[1]);
					}
					if (array.Length > 2)
					{
						verBuild = Localization.TryParse(array[2]);
					}
				}
			}
			return result;
		}
		protected override void EndSession()
		{
			base.EndSession();
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				if (this._hostCache != null)
				{
					this._hostCache.Clear();
					if (this._pool != null)
					{
						this._pool.UpdateDiscoveryStatus(DiscoveryStatus.New);
					}
				}
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
		}
		protected override void FindNewMaster()
		{
			XenCollectorBase.SessionInitStatus sessionInitStatus = XenCollectorBase.SessionInitStatus.None;
			Logger.Trace("Trying to find new pool master ...");
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				if (this._hostCache != null)
				{
					foreach (KeyValuePair<string, HostCacheItem> current in this._hostCache)
					{
						if (current.Value != null && current.Value.xenHost != null)
						{
							Logger.Trace("  Trying {0} ...", new object[]
							{
								current.Value.xenHost.address
							});
							sessionInitStatus = base.InitializeSession(current.Value.xenHost.address, true, false);
							if (sessionInitStatus == XenCollectorBase.SessionInitStatus.Success)
							{
								break;
							}
						}
					}
				}
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
			if (sessionInitStatus != XenCollectorBase.SessionInitStatus.Success)
			{
				base.FindNewMaster();
			}
		}
		public override void StopCollection()
		{
			QueueManager.Flush();
			base.StopCollection();
		}
		protected override void OnInterval()
		{
			XenCollectorBase._traceEnabled = Configuration.GetValueAsBool(ConfigItems.DataCollectionTrace);
			bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.ForcePoolEnabled);
			if (!valueAsBool && this._pool != null && !this._pool.Enabled)
			{
				Configuration.ReloadConfiguration();
				valueAsBool = Configuration.GetValueAsBool(ConfigItems.ForcePoolEnabled);
			}
			int valueAsInt = Configuration.GetValueAsInt(ConfigItems.DiscoveryInterval, 24);
			if (this._session == null && base.InitializeSession(this._hostName, true, false) != XenCollectorBase.SessionInitStatus.Success)
			{
				base.OnConnectionLost(null);
			}
			if (this._session != null)
			{
				if (this._isInitialRun)
				{
					this.Discover();
					this._isInitialRun = false;
				}
				else
				{
					DateTime minValue = DateTime.MinValue;
					DiscoveryStatus discoveryStatus = DiscoveryStatus.New;
					if (this._pool != null && this._pool.Id > 0)
					{
						this._pool.GetDiscoveryStatus(out discoveryStatus, out minValue);
					}
					TimeSpan timeSpan = DateTime.UtcNow - minValue;
					if (minValue == DateTime.MinValue || timeSpan.TotalHours >= (double)valueAsInt || discoveryStatus == DiscoveryStatus.New)
					{
						if (timeSpan.TotalHours >= (double)valueAsInt && discoveryStatus != DiscoveryStatus.New)
						{
							Logger.Trace(string.Format("Running Discover() because it has not run for {0} hours.", valueAsInt.ToString()));
						}
						this.Discover();
					}
				}
				if (this._pool != null)
				{
					if (this._pool.IsLicensed)
					{
						if (base.Heartbeat() && (this._pool.Enabled || valueAsBool))
						{
							object hostCacheLock = this._hostCacheLock;
							Monitor.Enter(hostCacheLock);
							try
							{
								this.GetHosts();
								if (this._hostCache != null)
								{
									bool flag = false;
									string text = string.Empty;
									DwmHostMetricCollection dwmHostMetrics = new DwmHostMetricCollection();
									DwmVmMetricCollection dwmVmMetrics = new DwmVmMetricCollection();
									foreach (KeyValuePair<string, HostCacheItem> current in this._hostCache)
									{
										Host xenHost = current.Value.xenHost;
										flag = current.Value.dwmHost.IsPoolMaster;
										text = current.Key;
										try
										{
											int num = 0;
											int num2 = 0;
											int hostVersion = this.GetHostVersion(xenHost, out num, out num2);
											if (this._pool.VersionMajor == -1 || hostVersion != this._pool.VersionMajor || num != this._pool.VersionMinor || num2 != this._pool.VersionBuild)
											{
												this._pool.VersionMajor = hostVersion;
												this._pool.VersionMinor = num;
												this._pool.VersionBuild = num2;
												this._pool.Save();
											}
											if (hostVersion >= 5)
											{
												this.GetXen5Metrics(current.Value, dwmHostMetrics, dwmVmMetrics);
											}
											current.Value.errorCount = 0;
										}
										catch (Failure failure)
										{
											Logger.LogError("Caught Exception in XenCollector {0}:{1} -  {2}", new object[]
											{
												this._hostName,
												this._hostPort,
												failure.Message
											});
											Logger.LogException(failure);
											Thread.Sleep(30000);
										}
										catch (SocketException ex)
										{
											Logger.LogException(ex);
											Thread.Sleep(30000);
										}
										catch (WebException ex2)
										{
											if (flag)
											{
												Logger.LogError("Unable to contact pool master host \"{0}\" (attempt {1} of {2})\r\n{3}", new object[]
												{
													DwmHost.UuidToName(text, this._poolId),
													this._hostCache[text].errorCount,
													5,
													ex2.Message
												});
												this._hostCache[text].errorCount++;
												if (this._hostCache[text].errorCount > 5)
												{
													Logger.LogError("Ending session for pool \"{0}\" (Id: {2}) to trigger a rediscovery.", new object[]
													{
														this._pool.Name,
														this._poolId
													});
													this.EndSession();
												}
											}
											else
											{
												Logger.LogError("Unable to contact host \"{0}\" \r\n{1}", new object[]
												{
													DwmHost.UuidToName(text, this._poolId),
													ex2.Message
												});
											}
										}
										catch (Exception ex3)
										{
											Logger.LogException(ex3);
											Thread.Sleep(30000);
										}
									}
									XenCollector.EnqueueData(dwmHostMetrics, dwmVmMetrics);
								}
							}
							finally
							{
								Monitor.Exit(hostCacheLock);
							}
						}
					}
					else
					{
						if (this._lastLicenseCheck == DateTime.MinValue)
						{
							this._lastLicenseCheck = DateTime.UtcNow;
						}
						if ((DateTime.UtcNow - this._lastLicenseCheck).TotalSeconds >= 300.0)
						{
							bool flag2 = true;
							Dictionary<XenRef<Host>, Host> dictionary = Host.get_all_records(this._session);
							foreach (KeyValuePair<XenRef<Host>, Host> current2 in dictionary)
							{
								flag2 &= this.CheckHostLicense(current2.Value);
							}
							if (flag2)
							{
								if (this._pool != null)
								{
									this._pool.UpdateDiscoveryStatus(DiscoveryStatus.New);
								}
							}
							else
							{
								Logger.Trace("Pool {0} ({1}) does not have a valid license.  Checking again in 5 minutes.", new object[]
								{
									this._pool.Name,
									this._pool.Uuid
								});
							}
							this._lastLicenseCheck = DateTime.UtcNow;
						}
					}
				}
				else
				{
					if (this._lastValidPoolCheck == DateTime.MinValue)
					{
						this._lastValidPoolCheck = DateTime.UtcNow;
					}
					TimeSpan timeSpan2 = DateTime.UtcNow - this._lastValidPoolCheck;
					if (timeSpan2.TotalSeconds >= 600.0)
					{
						if (this._pool != null)
						{
							this._pool.UpdateDiscoveryStatus(DiscoveryStatus.New);
						}
					}
					else
					{
						if (timeSpan2.TotalSeconds < 5.0)
						{
							Logger.Trace("Don't have a valid pool.  Trying again in 10 minutes.");
						}
					}
				}
			}
		}
		private void GetXen5Metrics(HostCacheItem item, DwmHostMetricCollection dwmHostMetrics, DwmVmMetricCollection dwmVmMetrics)
		{
			if (item != null && item.dwmHost != null && item.dwmHost.Enabled)
			{
				DateTime hostServerTime = XenCollector.GetHostServerTime(this._session, item.serverOpaqueRef);
				double totalMinutes = (hostServerTime - DateTime.UtcNow).TotalMinutes;
				if (Math.Abs(totalMinutes) > 1.0)
				{
					string fmt = string.Format("The Host ({0}) server time is approximately {1} minutes {2} the WLB server time. Please synchronize the server clocks.", item.dwmHost.Name, Math.Round(totalMinutes, 0).ToString(), (totalMinutes <= 0.0) ? "behind" : "ahead of");
					Logger.LogWarning(fmt, new object[0]);
				}
				Stream hostData = this.GetHostData(this._session, item.dwmHost.IPAddress, item.dwmHost.MetricsLastRetrieved, totalMinutes);
				if (hostData != null)
				{
					DwmHostMetricCollection dwmHostMetricCollection;
					DwmVmMetricCollection dwmVmMetricCollection;
					DateTime dateTime = this.ParseMetrics(hostData, totalMinutes, out dwmHostMetricCollection, out dwmVmMetricCollection);
					if (dwmHostMetricCollection != null && dwmHostMetricCollection.Count > 0)
					{
						dwmHostMetrics.AddRange(dwmHostMetricCollection);
					}
					if (dwmVmMetricCollection != null && dwmVmMetricCollection.Count > 0)
					{
						dwmVmMetrics.AddRange(dwmVmMetricCollection);
					}
					if (!(dateTime == DateTime.MinValue) && !(dateTime > DateTime.UtcNow))
					{
						if ((DateTime.UtcNow - dateTime).TotalSeconds <= 60.0)
						{
							item.dwmHost.MetricsLastRetrieved = DateTime.UtcNow;
							return;
						}
					}
					try
					{
						item.dwmHost.MetricsLastRetrieved = Host.get_servertime(this._session, item.serverOpaqueRef);
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
			}
		}
		private double AverageMetric(List<DeviceMetric> metrics)
		{
			if (metrics == null || metrics.Count == 0)
			{
				throw new ArgumentException("Invalid argument passed.", "metrics");
			}
			double num = 0.0;
			foreach (DeviceMetric current in metrics)
			{
				num += current.MetricValue;
			}
			return num / (double)metrics.Count;
		}
		public DateTime ParseMetrics(Stream xmlStats, double hostServerOffsetMinutes, out DwmHostMetricCollection hostMetrics, out DwmVmMetricCollection vmMetrics)
		{
			XenMetricsIndex xenMetricsIndex = null;
			XenMetricsIndex xenMetricsIndex2 = null;
			Dictionary<int, XenMetricsIndex> dictionary = new Dictionary<int, XenMetricsIndex>();
			DateTime dateTime = DateTime.MinValue;
			DateTime tStamp = DateTime.MinValue;
			VmCacheItem vmCacheItem = null;
			string a = string.Empty;
			char c = ':';
			hostMetrics = new DwmHostMetricCollection();
			vmMetrics = new DwmVmMetricCollection();
			using (XmlReader xmlReader = XmlReader.Create(xmlStats))
			{
				xmlReader.MoveToContent();
				int num = 0;
				while (!xmlReader.EOF)
				{
					if (xmlReader.NodeType != XmlNodeType.Element || (xmlReader.Name != "entry" && xmlReader.Name != "row"))
					{
						xmlReader.Read();
					}
					else
					{
						if (xmlReader.Name == "entry")
						{
							string[] array = xmlReader.ReadElementContentAsString().Split(new char[]
							{
								c
							});
							if (array[1] == "host")
							{
								if (num == 0)
								{
									string text = array[2];
									if (!this.HostCacheContainsKey(text))
									{
										this.DiscoverHost(this._session, text, true, true);
									}
									xenMetricsIndex = new XenMetricsIndex(text);
								}
								string text2 = array[3];
								if (text2 == "memory_total_kib")
								{
									xenMetricsIndex.TotalMemRowIndex = num;
								}
								else
								{
									if (text2 == "memory_free_kib")
									{
										xenMetricsIndex.FreeMemRowIndex = num;
									}
									else
									{
										if (text2.StartsWith("pif_eth") && !text2.Contains("."))
										{
											int length = "pif_eth".Length;
											int length2 = text2.Length - length - "_rx".Length;
											if (text2.EndsWith("_rx"))
											{
												int deviceNumber;
												if (int.TryParse(text2.Substring(length, length2), out deviceNumber))
												{
													MetricItemIndex item = new MetricItemIndex(deviceNumber, num);
													xenMetricsIndex.PifIoReadRowIndex.Add(item);
												}
											}
											else
											{
												int deviceNumber2;
												if (text2.EndsWith("_tx") && int.TryParse(text2.Substring(length, length2), out deviceNumber2))
												{
													MetricItemIndex item2 = new MetricItemIndex(deviceNumber2, num);
													xenMetricsIndex.PifIoWriteRowIndex.Add(item2);
												}
											}
										}
										else
										{
											if (text2.StartsWith("cpu"))
											{
												int deviceNumber3;
												if (int.TryParse(text2.Substring(3), out deviceNumber3))
												{
													MetricItemIndex item3 = new MetricItemIndex(deviceNumber3, num);
													xenMetricsIndex.CpuUtilizationRowIndex.Add(item3);
												}
											}
											else
											{
												if (text2 == "loadavg")
												{
													xenMetricsIndex.LoadAverageRowIndex = num;
												}
											}
										}
									}
								}
							}
							else
							{
								if (array[1] == "vm")
								{
									string text3 = array[2];
									if (a != text3)
									{
										vmCacheItem = this.VmCacheGetValue(text3);
										if (vmCacheItem == null)
										{
											this.DiscoverVm(this._session, text3, true);
											vmCacheItem = this.VmCacheGetValue(text3);
										}
										xenMetricsIndex2 = new XenMetricsIndex(xenMetricsIndex.HostUuid, text3);
										dictionary.Add(num, xenMetricsIndex2);
										a = text3;
									}
									string text4 = array[3];
									if (text4.StartsWith("cpu"))
									{
										int deviceNumber4;
										if (int.TryParse(text4.Substring(3), out deviceNumber4))
										{
											MetricItemIndex item4 = new MetricItemIndex(deviceNumber4, num);
											xenMetricsIndex2.CpuUtilizationRowIndex.Add(item4);
										}
									}
									else
									{
										if (text4.StartsWith("vif_"))
										{
											int length3 = "vif_".Length;
											int length4 = text4.Length - length3 - "_rx".Length;
											if (text4.EndsWith("_rx"))
											{
												int deviceNumber5;
												if (int.TryParse(text4.Substring(length3, length4), out deviceNumber5))
												{
													MetricItemIndex item5 = new MetricItemIndex(deviceNumber5, num);
													xenMetricsIndex2.PifIoReadRowIndex.Add(item5);
												}
											}
											else
											{
												int deviceNumber6;
												if (text4.EndsWith("_tx") && int.TryParse(text4.Substring(length3, length4), out deviceNumber6))
												{
													MetricItemIndex item6 = new MetricItemIndex(deviceNumber6, num);
													xenMetricsIndex2.PifIoWriteRowIndex.Add(item6);
												}
											}
										}
										else
										{
											if (text4.StartsWith("vbd_"))
											{
												int length5 = "vbd_".Length;
												int length6 = text4.LastIndexOf('_') - length5;
												string text5 = text4.Substring(length5, length6);
												if (text5.StartsWith("hd") || text5.StartsWith("xvd"))
												{
													int deviceNumber7 = (int)(text5.ToCharArray()[text5.Length - 1] - 'a');
													MetricItemIndex item7 = new MetricItemIndex(deviceNumber7, num);
													item7.IsNetworkedDevice = this.IsVbdNetworked(vmCacheItem, text5);
													if (text4.EndsWith("_read"))
													{
														xenMetricsIndex2.VbdIoReadRowIndex.Add(item7);
													}
													else
													{
														if (text4.EndsWith("_write"))
														{
															xenMetricsIndex2.VbdIoWriteRowIndex.Add(item7);
														}
													}
												}
											}
											else
											{
												if (text4 == "memory")
												{
													xenMetricsIndex2.TotalMemRowIndex = num;
												}
												else
												{
													if (text4 == "memory_target")
													{
														xenMetricsIndex2.TargetMemRowIndex = num;
													}
													else
													{
														if (text4 == "memory_internal_free")
														{
															xenMetricsIndex2.FreeMemRowIndex = num;
														}
														else
														{
															if (text4.StartsWith("runstate_"))
															{
																string text6 = text4;
																switch (text6)
																{
																case "runstate_blocked":
																	xenMetricsIndex2.RunstateBlocked = num;
																	break;
																case "runstate_partial_run":
																	xenMetricsIndex2.RunstatePartialRun = num;
																	break;
																case "runstate_fullrun":
																	xenMetricsIndex2.RunstateFullRun = num;
																	break;
																case "runstate_partial_contention":
																	xenMetricsIndex2.RunstatePartialContention = num;
																	break;
																case "runstate_concurrency_hazard":
																	xenMetricsIndex2.RunstateConcurrencyHazard = num;
																	break;
																case "runstate_full_contention":
																	xenMetricsIndex2.RunstateFullContention = num;
																	break;
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
							num++;
						}
						else
						{
							if (xmlReader.Name == "row")
							{
								xmlReader.ReadToDescendant("t");
								dateTime = this.ConvertToDateTime(xmlReader.ReadElementContentAsDouble());
								tStamp = dateTime.AddMinutes(-hostServerOffsetMinutes);
								DwmHostMetric dwmHostMetric = null;
								a = string.Empty;
								DwmVmMetric dwmVmMetric = null;
								for (int i = 0; i < num; i++)
								{
									if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "v")
									{
										double num3 = xmlReader.ReadElementContentAsDouble();
										if (double.IsNaN(num3) || double.IsInfinity(num3) || num3 < 0.0)
										{
											if (i == 0)
											{
												break;
											}
											num3 = 0.0;
										}
										if (i == 0)
										{
											dwmHostMetric = new DwmHostMetric(xenMetricsIndex.HostUuid, this._pool.Uuid);
											hostMetrics.Add(dwmHostMetric);
											XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "Got metrics for host {0} ({1})", new object[]
											{
												dwmHostMetric.HostId,
												dwmHostMetric.HostUuid
											});
											XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "   TStamp={0}", new object[]
											{
												dateTime
											});
											dwmHostMetric.TStamp = tStamp;
										}
										bool flag = false;
										if (xenMetricsIndex.TotalMemRowIndex == i)
										{
											dwmHostMetric.TotalMem = (long)num3 * 1024L;
											XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "   Total mem={0}", new object[]
											{
												dwmHostMetric.TotalMem
											});
											flag = true;
										}
										if (xenMetricsIndex.FreeMemRowIndex == i)
										{
											dwmHostMetric.FreeMem = (long)num3 * 1024L;
											XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "   Free mem ={0}", new object[]
											{
												dwmHostMetric.FreeMem
											});
											flag = true;
										}
										if (!flag)
										{
											for (int j = 0; j < xenMetricsIndex.PifIoReadRowIndex.Count; j++)
											{
												int rowIndex = xenMetricsIndex.PifIoReadRowIndex[j].RowIndex;
												if (rowIndex == i)
												{
													int deviceNumber8 = xenMetricsIndex.PifIoReadRowIndex[j].DeviceNumber;
													DeviceMetric item8 = new DeviceMetric(deviceNumber8, num3);
													dwmHostMetric.PifIoRead.Add(item8);
													XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "   Pif {0} reads={1}", new object[]
													{
														deviceNumber8,
														num3
													});
													if (dwmHostMetric.PifIoRead.Count == xenMetricsIndex.PifIoReadRowIndex.Count)
													{
														dwmHostMetric.AvgPifIoReadPerSecond = this.AverageMetric(dwmHostMetric.PifIoRead);
													}
													flag = true;
												}
											}
										}
										if (!flag)
										{
											for (int k = 0; k < xenMetricsIndex.PifIoWriteRowIndex.Count; k++)
											{
												int rowIndex2 = xenMetricsIndex.PifIoWriteRowIndex[k].RowIndex;
												if (rowIndex2 == i)
												{
													int deviceNumber9 = xenMetricsIndex.PifIoWriteRowIndex[k].DeviceNumber;
													DeviceMetric item9 = new DeviceMetric(deviceNumber9, num3);
													dwmHostMetric.PifIoWrite.Add(item9);
													XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "   Pif {0} write={1}", new object[]
													{
														deviceNumber9,
														num3
													});
													if (dwmHostMetric.PifIoWrite.Count == xenMetricsIndex.PifIoWriteRowIndex.Count)
													{
														dwmHostMetric.AvgPifIoWritePerSecond = this.AverageMetric(dwmHostMetric.PifIoWrite);
													}
													flag = true;
												}
											}
										}
										if (!flag)
										{
											for (int l = 0; l < xenMetricsIndex.CpuUtilizationRowIndex.Count; l++)
											{
												int rowIndex3 = xenMetricsIndex.CpuUtilizationRowIndex[l].RowIndex;
												if (rowIndex3 == i)
												{
													int deviceNumber10 = xenMetricsIndex.CpuUtilizationRowIndex[l].DeviceNumber;
													DeviceMetric item10 = new DeviceMetric(deviceNumber10, num3);
													dwmHostMetric.CpuUtilization.Add(item10);
													XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "   CPU {0} utilization={1}", new object[]
													{
														deviceNumber10,
														num3
													});
													if (dwmHostMetric.CpuUtilization.Count == xenMetricsIndex.CpuUtilizationRowIndex.Count)
													{
														dwmHostMetric.AvgCpuUtilization = this.AverageMetric(dwmHostMetric.CpuUtilization);
													}
													flag = true;
												}
											}
										}
										if (xenMetricsIndex.LoadAverageRowIndex == i)
										{
											dwmHostMetric.LoadAverage = num3;
											XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "   Load Average={0}", new object[]
											{
												dwmHostMetric.LoadAverage
											});
											flag = true;
										}
										if (!flag)
										{
											if (dictionary.ContainsKey(i))
											{
												xenMetricsIndex2 = dictionary[i];
												if (a != xenMetricsIndex2.VmUuid)
												{
													dwmVmMetric = new DwmVmMetric(dwmHostMetric.HostUuid, this._pool.Uuid, xenMetricsIndex2.VmUuid);
													XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "   Got metrics for VM {0} ({1})", new object[]
													{
														dwmVmMetric.VMId,
														dwmVmMetric.VMUuid
													});
													a = xenMetricsIndex2.VmUuid;
													vmMetrics.Add(dwmVmMetric);
													dwmVmMetric.TStamp = dwmHostMetric.TStamp;
												}
											}
											if (xenMetricsIndex2.TotalMemRowIndex == i)
											{
												dwmVmMetric.TotalMem = (long)num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      TotalMem={0}", new object[]
												{
													dwmVmMetric.TotalMem
												});
												flag = true;
											}
											if (xenMetricsIndex2.FreeMemRowIndex == i)
											{
												dwmVmMetric.FreeMem = (long)num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      FreeMem ={0}", new object[]
												{
													dwmVmMetric.FreeMem
												});
												flag = true;
											}
											if (xenMetricsIndex2.TargetMemRowIndex == i)
											{
												dwmVmMetric.TargetMem = (long)num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      TargetMem ={0}", new object[]
												{
													dwmVmMetric.TargetMem
												});
												flag = true;
											}
											if (!flag)
											{
												for (int m = 0; m < xenMetricsIndex2.PifIoReadRowIndex.Count; m++)
												{
													int rowIndex4 = xenMetricsIndex2.PifIoReadRowIndex[m].RowIndex;
													if (rowIndex4 == i)
													{
														int deviceNumber11 = xenMetricsIndex2.PifIoReadRowIndex[m].DeviceNumber;
														DeviceMetric item11 = new DeviceMetric(deviceNumber11, num3);
														dwmVmMetric.PifIoRead.Add(item11);
														XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      Vif {0} reads={1}", new object[]
														{
															deviceNumber11,
															num3
														});
														if (dwmVmMetric.PifIoRead.Count == xenMetricsIndex2.PifIoReadRowIndex.Count)
														{
															dwmVmMetric.AvgPifIoReadPerSecond = this.AverageMetric(dwmVmMetric.PifIoRead);
														}
														flag = true;
													}
												}
											}
											if (!flag)
											{
												for (int n = 0; n < xenMetricsIndex2.PifIoWriteRowIndex.Count; n++)
												{
													int rowIndex5 = xenMetricsIndex2.PifIoWriteRowIndex[n].RowIndex;
													if (rowIndex5 == i)
													{
														int deviceNumber12 = xenMetricsIndex2.PifIoWriteRowIndex[n].DeviceNumber;
														DeviceMetric item12 = new DeviceMetric(deviceNumber12, num3);
														dwmVmMetric.PifIoWrite.Add(item12);
														XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      Vif {0} writes={1}", new object[]
														{
															deviceNumber12,
															num3
														});
														if (dwmVmMetric.PifIoWrite.Count == xenMetricsIndex2.PifIoWriteRowIndex.Count)
														{
															dwmVmMetric.AvgPifIoWritePerSecond = this.AverageMetric(dwmVmMetric.PifIoWrite);
														}
														flag = true;
													}
												}
											}
											if (!flag)
											{
												for (int num4 = 0; num4 < xenMetricsIndex2.VbdIoReadRowIndex.Count; num4++)
												{
													int rowIndex6 = xenMetricsIndex2.VbdIoReadRowIndex[num4].RowIndex;
													if (rowIndex6 == i)
													{
														int deviceNumber13 = xenMetricsIndex2.VbdIoReadRowIndex[num4].DeviceNumber;
														bool isNetworkedDevice = xenMetricsIndex2.VbdIoReadRowIndex[num4].IsNetworkedDevice;
														DeviceMetric item13 = new DeviceMetric(deviceNumber13, num3);
														dwmVmMetric.VbdIoRead.Add(item13);
														XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      Vbd {0} reads={1}", new object[]
														{
															deviceNumber13,
															num3
														});
														if (isNetworkedDevice)
														{
															dwmVmMetric.TotalVbdNetworkReadPerSecond += item13.MetricValue;
														}
														if (dwmVmMetric.VbdIoRead.Count == xenMetricsIndex2.VbdIoReadRowIndex.Count)
														{
															dwmVmMetric.AvgVbdIoReadPerSecond = this.AverageMetric(dwmVmMetric.VbdIoRead);
														}
														flag = true;
													}
												}
											}
											if (!flag)
											{
												for (int num5 = 0; num5 < xenMetricsIndex2.VbdIoWriteRowIndex.Count; num5++)
												{
													int rowIndex7 = xenMetricsIndex2.VbdIoWriteRowIndex[num5].RowIndex;
													if (rowIndex7 == i)
													{
														int deviceNumber14 = xenMetricsIndex2.VbdIoWriteRowIndex[num5].DeviceNumber;
														bool isNetworkedDevice2 = xenMetricsIndex2.VbdIoReadRowIndex[num5].IsNetworkedDevice;
														DeviceMetric item14 = new DeviceMetric(deviceNumber14, num3);
														dwmVmMetric.VbdIoWrite.Add(item14);
														XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      Vbd {0} writes={1}", new object[]
														{
															deviceNumber14,
															num3
														});
														if (isNetworkedDevice2)
														{
															dwmVmMetric.TotalVbdNetworkWritePerSecond += item14.MetricValue;
														}
														if (dwmVmMetric.VbdIoWrite.Count == xenMetricsIndex2.VbdIoWriteRowIndex.Count)
														{
															dwmVmMetric.AvgVbdIoWritePerSecond = this.AverageMetric(dwmVmMetric.VbdIoWrite);
														}
														flag = true;
													}
												}
											}
											if (!flag)
											{
												for (int num6 = 0; num6 < xenMetricsIndex2.CpuUtilizationRowIndex.Count; num6++)
												{
													int rowIndex8 = xenMetricsIndex2.CpuUtilizationRowIndex[num6].RowIndex;
													if (rowIndex8 == i)
													{
														int deviceNumber15 = xenMetricsIndex2.CpuUtilizationRowIndex[num6].DeviceNumber;
														DeviceMetric item15 = new DeviceMetric(deviceNumber15, num3);
														dwmVmMetric.CpuUtilization.Add(item15);
														XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      VCpu {0} utilization={1}", new object[]
														{
															deviceNumber15,
															num3
														});
														if (dwmVmMetric.CpuUtilization.Count == xenMetricsIndex2.CpuUtilizationRowIndex.Count)
														{
															dwmVmMetric.AvgCpuUtilization = this.AverageMetric(dwmVmMetric.CpuUtilization);
														}
													}
												}
											}
											if (xenMetricsIndex2.RunstateBlocked == i)
											{
												dwmVmMetric.RunstateBlocked = num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      RunStateBlocked={0}", new object[]
												{
													dwmVmMetric.RunstateBlocked
												});
											}
											if (xenMetricsIndex2.RunstatePartialRun == i)
											{
												dwmVmMetric.RunstatePartialRun = num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      RunstatePartialRun={0}", new object[]
												{
													dwmVmMetric.RunstatePartialRun
												});
											}
											if (xenMetricsIndex2.RunstateFullRun == i)
											{
												dwmVmMetric.RunstateFullRun = num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      RunstateFullRun={0}", new object[]
												{
													dwmVmMetric.RunstateFullRun
												});
											}
											if (xenMetricsIndex2.RunstatePartialContention == i)
											{
												dwmVmMetric.RunstatePartialContention = num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      RunstatePartialContention={0}", new object[]
												{
													dwmVmMetric.RunstatePartialContention
												});
											}
											if (xenMetricsIndex2.RunstateConcurrencyHazard == i)
											{
												dwmVmMetric.RunstateConcurrencyHazard = num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      RunstateConcurrencyHazard={0}", new object[]
												{
													dwmVmMetric.RunstateConcurrencyHazard
												});
											}
											if (xenMetricsIndex2.RunstateFullContention == i)
											{
												dwmVmMetric.RunstateFullContention = num3;
												XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "      RunstateFullContention={0}", new object[]
												{
													dwmVmMetric.RunstateFullContention
												});
											}
										}
									}
									else
									{
										xmlReader.Read();
									}
								}
							}
						}
					}
				}
			}
			return dateTime;
		}
		private static Host GetHostRecord(Session session, string host)
		{
			Host result = null;
			try
			{
				result = Host.get_record(session, host);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static VM GetVmRecord(Session session, string vm)
		{
			VM vM = null;
			try
			{
				vM = VM.get_record(session, vm);
				vM.opaque_ref = vm;
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return vM;
		}
		private static Host_cpu GetHostCpuRecord(Session session, string hostCpu)
		{
			Host_cpu result = null;
			try
			{
				result = Host_cpu.get_record(session, hostCpu);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static PIF GetPifRecord(Session session, string pif)
		{
			PIF result = null;
			try
			{
				result = PIF.get_record(session, pif);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static PBD GetPbdRecord(Session session, string pbd)
		{
			PBD result = null;
			try
			{
				result = PBD.get_record(session, pbd);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static SR GetSrRecord(Session session, string sr)
		{
			SR result = null;
			try
			{
				result = SR.get_record(session, sr);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static VDI GetVdiRecord(Session session, string vdi)
		{
			VDI result = null;
			try
			{
				result = VDI.get_record(session, vdi);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static Network GetNetworkRecord(Session session, string network)
		{
			Network result = null;
			try
			{
				result = Network.get_record(session, network);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static VIF GetVifRecord(Session session, string vif)
		{
			VIF result = null;
			try
			{
				result = VIF.get_record(session, vif);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static VBD GetVbdRecord(Session session, string vbd)
		{
			VBD result = null;
			try
			{
				result = VBD.get_record(session, vbd);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static DateTime GetHostServerTime(Session session, string host)
		{
			DateTime result = DateTime.MinValue;
			try
			{
				result = Host.get_servertime(session, host);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static Host_metrics GetHostMetrics(Session session, string host)
		{
			Host_metrics result = null;
			try
			{
				result = Host_metrics.get_record(session, host);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static VM_metrics GetVmMetrics(Session session, string vm)
		{
			VM_metrics result = null;
			try
			{
				result = VM_metrics.get_record(session, vm);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static VM_guest_metrics GetVmGuestMetrics(Session session, string vm)
		{
			VM_guest_metrics result = null;
			try
			{
				result = VM_guest_metrics.get_record(session, vm);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static XenRef<PIF_metrics> GetXrPifMetrics(Session session, string pif)
		{
			XenRef<PIF_metrics> result = null;
			try
			{
				result = PIF.get_metrics(session, pif);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static PIF_metrics GetPifMetrics(Session session, string pif)
		{
			PIF_metrics result = null;
			try
			{
				result = PIF_metrics.get_record(session, pif);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static XenRef<VIF_metrics> GetXrVifMetrics(Session session, string vif)
		{
			XenRef<VIF_metrics> result = null;
			try
			{
				result = VIF.get_metrics(session, vif);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static VIF_metrics GetVifMetrics(Session session, string vif)
		{
			VIF_metrics result = null;
			try
			{
				result = VIF_metrics.get_record(session, vif);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static XenRef<VBD_metrics> GetXrVbdMetrics(Session session, string vbd)
		{
			XenRef<VBD_metrics> result = null;
			try
			{
				result = VBD.get_metrics(session, vbd);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static VBD_metrics GetVbdMetrics(Session session, string vbd)
		{
			VBD_metrics result = null;
			try
			{
				result = VBD_metrics.get_record(session, vbd);
			}
			catch (Failure f)
			{
				XenCollector.HandleFailure(f);
			}
			return result;
		}
		private static void HandleFailure(Failure f)
		{
			if (f.ErrorDescription != null && f.ErrorDescription.Count > 0 && Localization.Compare(f.ErrorDescription[0], "HANDLE_INVALID", true) != 0)
			{
				throw f;
			}
		}
		private static void EnqueueData(DwmHostMetricCollection dwmHostMetrics, DwmVmMetricCollection dwmVmMetrics)
		{
			bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.QueueManagementTrace);
			if (dwmHostMetrics.Count > 0)
			{
				QueueManager.Enqueue(dwmHostMetrics);
				XenCollectorBase.Trace(valueAsBool, "Added DwmHostMetricCollection instance with {0} items to the queue", new object[]
				{
					dwmHostMetrics.Count
				});
			}
			if (dwmVmMetrics.Count > 0)
			{
				QueueManager.Enqueue(dwmVmMetrics);
				XenCollectorBase.Trace(valueAsBool, "Added DwmVmMetricCollection instance with {0} items to the queue", new object[]
				{
					dwmVmMetrics.Count
				});
			}
		}
		public void Discover()
		{
			if (this.IsInitialized)
			{
				DwmPool dwmPool = this.Discover(this._session);
				if (dwmPool != null)
				{
					dwmPool.Save();
					dwmPool.SavePoolChildren();
				}
				return;
			}
			throw new DwmException("Data collector is not properly initialized.  Call the Initialize method before calling the Discover method", DwmErrorCode.NotInitialized, null);
		}
		private double ConvertToTimestamp(DateTime value)
		{
			return (double)((long)(value - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
		}
		private DateTime ConvertToDateTime(double value)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return dateTime.AddSeconds(value);
		}
		private Stream GetHostData(Session session, string ipAddress, DateTime getDataSince, double hostServerOffsetMinutes)
		{
			double num = this.ConvertToTimestamp((!(getDataSince != DateTime.MinValue)) ? DateTime.UtcNow.AddMinutes(hostServerOffsetMinutes).AddMinutes(-2.0) : getDataSince);
			string uri = Localization.Format("{0}://{1}:{2}/rrd_updates?session_id={3}&cf=AVERAGE&start={4}&host=true", new object[]
			{
				this._protocol,
				ipAddress,
				this._hostPort,
				session.uuid,
				num
			});
			return XenCollector.GetPoolData(uri);
		}
		private Stream GetVmData(Session session, string ipAddress, DateTime getDataSince)
		{
			double num = this.ConvertToTimestamp((!(getDataSince != DateTime.MinValue)) ? DateTime.UtcNow.AddMinutes(-2.0) : getDataSince);
			string uri = Localization.Format("{0}://{1}:{2}/rrd_updates?session_id={3}&cf=AVERAGE&start={4}", new object[]
			{
				this._protocol,
				ipAddress,
				this._hostPort,
				session.uuid,
				num
			});
			return XenCollector.GetPoolData(uri);
		}
		private static Stream GetPoolData(string uri)
		{
			try
			{
				XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "Requesting data from {0}", new object[]
				{
					uri
				});
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
				httpWebRequest.UserAgent = "WlbDataCollector";
				httpWebRequest.MaximumAutomaticRedirections = 4;
				httpWebRequest.MaximumResponseHeadersLength = 4;
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "Content length is {0}", new object[]
				{
					httpWebResponse.ContentLength
				});
				XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "Content type is {0}", new object[]
				{
					httpWebResponse.ContentType
				});
				return httpWebResponse.GetResponseStream();
			}
			catch (WebException e)
			{
				XenCollector.LogGetPoolDataError(uri, e);
			}
			return null;
		}
		private static void LogGetPoolDataError(string uri, WebException e)
		{
			bool flag = true;
			if (XenCollector._hostHttpErrors == null)
			{
				XenCollector._hostHttpErrors = new Dictionary<string, HostHttpError>();
			}
			int num = uri.IndexOf("/rrd_updates?");
			string text = uri.Substring(0, num - 1);
			HostHttpError hostHttpError;
			if (!XenCollector._hostHttpErrors.TryGetValue(text, out hostHttpError))
			{
				hostHttpError = new HostHttpError();
				hostHttpError.HostUri = text;
				hostHttpError.ErrorCount = 0;
				XenCollector._hostHttpErrors.Add(text, hostHttpError);
			}
			else
			{
				hostHttpError.ErrorCount++;
				if ((DateTime.UtcNow - hostHttpError.LastLogged).TotalSeconds < 3600.0)
				{
					flag = false;
				}
			}
			if (flag)
			{
				Logger.Trace("Exception requesting data from {0}", new object[]
				{
					uri
				});
				Logger.Trace("This error occurred {0} times.", new object[]
				{
					hostHttpError.ErrorCount
				});
				hostHttpError.LastLogged = DateTime.UtcNow;
			}
		}
		private DwmPool Discover(Session session)
		{
			int num = 0;
			Logger.Trace("Discover() - ");
			object vmCacheLock = this._vmCacheLock;
			Monitor.Enter(vmCacheLock);
			try
			{
				this._vmCache = new Dictionary<string, VmCacheItem>();
				this._vmCache2 = new Dictionary<string, VM>();
			}
			finally
			{
				Monitor.Exit(vmCacheLock);
			}
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				this._hostCache = new Dictionary<string, HostCacheItem>();
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
			object srCacheLock = this._srCacheLock;
			Monitor.Enter(srCacheLock);
			try
			{
				this._srCache = new Dictionary<string, SR>();
			}
			finally
			{
				Monitor.Exit(srCacheLock);
			}
			object pbdCacheLock = this._pbdCacheLock;
			Monitor.Enter(pbdCacheLock);
			try
			{
				this._pbdCache = new Dictionary<string, PBD>();
			}
			finally
			{
				Monitor.Exit(pbdCacheLock);
			}
			object netCacheLock = this._netCacheLock;
			Monitor.Enter(netCacheLock);
			try
			{
				this._networkCache = new Dictionary<string, Network>();
			}
			finally
			{
				Monitor.Exit(netCacheLock);
			}
			object pifCacheLock = this._pifCacheLock;
			Monitor.Enter(pifCacheLock);
			try
			{
				this._pifCache = new Dictionary<string, PIF>();
			}
			finally
			{
				Monitor.Exit(pifCacheLock);
			}
			object vbdCacheLock = this._vbdCacheLock;
			Monitor.Enter(vbdCacheLock);
			try
			{
				this._vbdCache = new Dictionary<string, VBD>();
			}
			finally
			{
				Monitor.Exit(vbdCacheLock);
			}
			object vdiCacheLock = this._vdiCacheLock;
			Monitor.Enter(vdiCacheLock);
			try
			{
				this._vdiCache = new Dictionary<string, VDI>();
			}
			finally
			{
				Monitor.Exit(vdiCacheLock);
			}
			object vifCacheLock = this._vifCacheLock;
			Monitor.Enter(vifCacheLock);
			try
			{
				this._vifCache = new Dictionary<string, VIF>();
			}
			finally
			{
				Monitor.Exit(vifCacheLock);
			}
			try
			{
				Dictionary<XenRef<Pool>, Pool> dictionary = Pool.get_all_records(session);
				foreach (KeyValuePair<XenRef<Pool>, Pool> current in dictionary)
				{
					Pool value = current.Value;
					Dictionary<string, string> dictionary2 = Pool.get_ha_configuration(session, current.Key.opaque_ref);
					Logger.Trace("       HA - enabled = {0}", new object[]
					{
						value.ha_enabled
					});
					Logger.Trace("       HA - allow_overcommit = {0}", new object[]
					{
						value.ha_allow_overcommit
					});
					Logger.Trace("       HA - host_failures_to_tolerate = {0}", new object[]
					{
						value.ha_host_failures_to_tolerate
					});
					Logger.Trace("       HA - overcommitted = {0}", new object[]
					{
						value.ha_overcommitted
					});
					Logger.Trace("       HA - plan_exists_for = {0}", new object[]
					{
						value.ha_plan_exists_for
					});
					string[] ha_statefiles = value.ha_statefiles;
					Logger.Trace("       HA - statefiles = {0}", new object[]
					{
						(ha_statefiles == null || ha_statefiles.Length <= 0) ? "none" : ha_statefiles[0]
					});
					Host hostRecord = XenCollector.GetHostRecord(session, current.Value.master);
					Logger.Trace("Pool:  master = {0}", new object[]
					{
						hostRecord.name_label
					});
					Logger.Trace("       name   = {0}", new object[]
					{
						current.Value.name_label
					});
					Logger.Trace("       desc   = {0}", new object[]
					{
						current.Value.name_description
					});
					Logger.Trace("       uuid   = {0}", new object[]
					{
						current.Value.uuid
					});
					Dictionary<string, string> other_config = current.Value.other_config;
					foreach (KeyValuePair<string, string> current2 in other_config)
					{
						Logger.Trace("{0}={1}", new object[]
						{
							current2.Key,
							current2.Value
						});
					}
					this._pool = new DwmPool(current.Value.uuid, current.Value.name_label, DwmHypervisorType.XenServer);
					this._pool.Master = hostRecord.name_label;
					this._pool.Description = current.Value.name_description;
					this._pool.PrimaryPoolMasterAddr = this._hostName;
					this._pool.PrimaryPoolMasterPort = this._hostPort;
					this._pool.UserName = this._username;
					this._pool.Password = this._password;
					this._pool.TouchedBy = "__XEN_DATA_COLLECTOR__";
					this._pool.Enabled = this.GetWlbEnabled(session, value);
					this._pool.DefaultSR = current.Value.default_SR;
					int id = this._pool.Id;
					this._pool.Save();
					this._poolId = this._pool.Id;
					if (id != 0 && this._pool.Id != id)
					{
						DwmPoolBase.RefreshCache();
					}
					num++;
				}
				this._pool.UpdateDiscoveryStatus(DiscoveryStatus.InProgress);
				DwmStorageRepositoryCollection dwmStorageRepositoryCollection = new DwmStorageRepositoryCollection();
				Logger.Trace("Storage Repositories:");
				Dictionary<XenRef<SR>, SR> dictionary3 = SR.get_all_records(session);
				foreach (KeyValuePair<XenRef<SR>, SR> current3 in dictionary3)
				{
					DwmStorageRepository dwmStorageRepository = this.DiscoverSr(session, current3.Key.opaque_ref, current3.Value, false);
					if (dwmStorageRepository != null)
					{
						dwmStorageRepositoryCollection.Add(dwmStorageRepository);
					}
				}
				dwmStorageRepositoryCollection.Save();
				DwmPbdCollection dwmPbdCollection = new DwmPbdCollection();
				Logger.Trace("Physical Block Devices:");
				Dictionary<XenRef<PBD>, PBD> dictionary4 = PBD.get_all_records(session);
				foreach (KeyValuePair<XenRef<PBD>, PBD> current4 in dictionary4)
				{
					DwmPbd dwmPbd = this.DiscoverPbd(session, current4.Key.opaque_ref, current4.Value, false);
					if (dwmPbd != null)
					{
						dwmPbdCollection.Add(dwmPbd);
					}
				}
				dwmPbdCollection.Save();
				DwmNetworkCollection dwmNetworkCollection = new DwmNetworkCollection();
				Logger.Trace("Networks:");
				Dictionary<XenRef<Network>, Network> dictionary5 = Network.get_all_records(session);
				foreach (KeyValuePair<XenRef<Network>, Network> current5 in dictionary5)
				{
					DwmNetwork dwmNetwork = this.DiscoverNetwork(session, current5.Key.opaque_ref, current5.Value, false);
					if (dwmNetwork != null)
					{
						dwmNetworkCollection.Add(dwmNetwork);
					}
				}
				dwmNetworkCollection.Save();
				DwmPifCollection dwmPifCollection = new DwmPifCollection();
				Logger.Trace("Physical Interfaces:");
				Dictionary<XenRef<PIF>, PIF> dictionary6 = PIF.get_all_records(session);
				foreach (KeyValuePair<XenRef<PIF>, PIF> current6 in dictionary6)
				{
					DwmPif dwmPif = this.DiscoverPif(session, current6.Key.opaque_ref, current6.Value, false);
					if (dwmPif != null)
					{
						dwmPifCollection.Add(dwmPif);
					}
				}
				dwmPifCollection.Save();
				Logger.Trace("Virtual Device Interfaces:");
				Dictionary<XenRef<VDI>, VDI> dictionary7 = VDI.get_all_records(session);
				foreach (KeyValuePair<XenRef<VDI>, VDI> current7 in dictionary7)
				{
					this.DiscoverVdi(session, current7.Key.opaque_ref, current7.Value);
				}
				DwmVifCollection dwmVifCollection = new DwmVifCollection();
				Logger.Trace("Virtual Interfaces:");
				Dictionary<XenRef<VIF>, VIF> dictionary8 = VIF.get_all_records(session);
				foreach (KeyValuePair<XenRef<VIF>, VIF> current8 in dictionary8)
				{
					DwmVif dwmVif = this.DiscoverVif(session, current8.Key.opaque_ref, current8.Value, false);
					if (dwmVif != null)
					{
						dwmVifCollection.Add(dwmVif);
					}
				}
				dwmVifCollection.Save();
				DwmVbdCollection dwmVbdCollection = new DwmVbdCollection();
				Logger.Trace("Virtual Block Devices:");
				Dictionary<XenRef<VBD>, VBD> dictionary9 = VBD.get_all_records(session);
				foreach (KeyValuePair<XenRef<VBD>, VBD> current9 in dictionary9)
				{
					DwmVbd dwmVbd = this.DiscoverVbd(session, current9.Key.opaque_ref, current9.Value, false);
					if (dwmVbd != null)
					{
						dwmVbdCollection.Add(dwmVbd);
					}
				}
				dwmVbdCollection.Save();
				DwmVirtualMachineCollection dwmVirtualMachineCollection = new DwmVirtualMachineCollection();
				Logger.Trace("Virtual Machines:");
				Dictionary<XenRef<VM>, VM> dictionary10 = VM.get_all_records(session);
				foreach (KeyValuePair<XenRef<VM>, VM> current10 in dictionary10)
				{
					VM value2 = current10.Value;
					value2.opaque_ref = current10.Key.opaque_ref;
					DwmVirtualMachine dwmVirtualMachine = this.DiscoverVm(session, current10.Key.opaque_ref, value2, false);
					if (dwmVirtualMachine != null)
					{
						dwmVirtualMachineCollection.Add(dwmVirtualMachine);
					}
				}
				if (dwmVirtualMachineCollection.Count > 0)
				{
					dwmVirtualMachineCollection.Save(this._poolId);
				}
				Logger.Trace("Hosts:");
				object hostCacheLock2 = this._hostCacheLock;
				Monitor.Enter(hostCacheLock2);
				try
				{
					Dictionary<XenRef<Host>, Host> dictionary11 = Host.get_all_records(session);
					foreach (KeyValuePair<XenRef<Host>, Host> current11 in dictionary11)
					{
						DwmHost dwmHost = this.DiscoverHost(session, current11.Key.opaque_ref, current11.Value, false, false);
						if (dwmHost != null)
						{
							this._pool.Hosts.Add(dwmHost);
						}
					}
				}
				finally
				{
					Monitor.Exit(hostCacheLock2);
				}
				this._pool.UpdateDiscoveryStatus(DiscoveryStatus.Complete);
			}
			catch (Failure failure)
			{
				Logger.LogException(failure);
				this._pool = null;
				if (Localization.Compare(failure.ErrorDescription[0], "SESSION_INVALID", true) == 0)
				{
					this.EndSession();
				}
			}
			catch (SocketException ex)
			{
				Logger.LogException(ex);
				this._pool = null;
				this.EndSession();
			}
			catch (WebException ex2)
			{
				Logger.LogException(ex2);
				this._pool = null;
				if (ex2.Status != WebExceptionStatus.Timeout)
				{
					this.EndSession();
				}
			}
			catch (Exception ex3)
			{
				Logger.LogException(ex3);
				this._pool = null;
				this.EndSession();
			}
			Logger.Trace("Discover() complete.");
			return this._pool;
		}
		private DwmStorageRepository DiscoverSr(Session session, string serverOpaqueRef, SR sr, bool saveSr)
		{
			DwmStorageRepository dwmStorageRepository = null;
			XenCollectorBase.Trace(XenCollectorBase._traceEnabled, "    {0} {1} size={2} used={3}", new object[]
			{
				sr.uuid,
				sr.name_label,
				sr.physical_size,
				sr.physical_utilisation
			});
			dwmStorageRepository = new DwmStorageRepository(sr.uuid, sr.name_label, this._pool.Uuid, sr.physical_size, sr.physical_utilisation, serverOpaqueRef == this._pool.DefaultSR);
			object srCacheLock = this._srCacheLock;
			Monitor.Enter(srCacheLock);
			try
			{
				if (!this._srCache.ContainsKey(serverOpaqueRef))
				{
					this._srCache.Add(serverOpaqueRef, sr);
				}
				else
				{
					this._srCache[serverOpaqueRef] = sr;
				}
			}
			finally
			{
				Monitor.Exit(srCacheLock);
			}
			if (saveSr)
			{
				new DwmStorageRepositoryCollection
				{
					dwmStorageRepository
				}.Save();
			}
			return dwmStorageRepository;
		}
		private DwmPbd DiscoverPbd(Session session, string serverOpaqueRef, PBD pbd, bool savePbd)
		{
			DwmPbd dwmPbd = null;
			if (XenCollectorBase.IsValidXenRef<SR>(pbd.SR))
			{
				SR srFromCache = this.GetSrFromCache(pbd.SR.opaque_ref);
				if (srFromCache != null)
				{
					dwmPbd = new DwmPbd(pbd.uuid, srFromCache.name_label, srFromCache.uuid, this._pool.Uuid);
					dwmPbd.CurrentlyAttached = pbd.currently_attached;
					if (!this._pbdCache.ContainsKey(serverOpaqueRef))
					{
						this._pbdCache.Add(serverOpaqueRef, pbd);
					}
					else
					{
						this._pbdCache[serverOpaqueRef] = pbd;
					}
					if (savePbd)
					{
						new DwmPbdCollection
						{
							dwmPbd
						}.Save();
					}
				}
			}
			return dwmPbd;
		}
		private DwmNetwork DiscoverNetwork(Session session, string serverOpaqueRef, Network network, bool saveNetwork)
		{
			DwmNetwork dwmNetwork = new DwmNetwork(network.uuid, network.name_label, this._poolId);
			dwmNetwork.Bridge = network.bridge;
			dwmNetwork.Description = network.name_description;
			if (!this._networkCache.ContainsKey(serverOpaqueRef))
			{
				this._networkCache.Add(serverOpaqueRef, network);
			}
			else
			{
				this._networkCache[serverOpaqueRef] = network;
			}
			if (saveNetwork)
			{
				new DwmNetworkCollection
				{
					dwmNetwork
				}.Save();
			}
			return dwmNetwork;
		}
		private DwmPif DiscoverPif(Session session, string serverOpaqueRef, PIF pif, bool savePif)
		{
			Network networkFromCache = this.GetNetworkFromCache(pif.network.opaque_ref);
			DwmPif dwmPif = new DwmPif(pif.uuid, pif.device, (networkFromCache == null) ? null : networkFromCache.uuid, this._pool.Uuid);
			dwmPif.IsManagementInterface = pif.management;
			dwmPif.IsPhysical = pif.physical;
			dwmPif.MacAddress = pif.MAC;
			if (!this._pifCache.ContainsKey(serverOpaqueRef))
			{
				this._pifCache.Add(serverOpaqueRef, pif);
			}
			else
			{
				this._pifCache[serverOpaqueRef] = pif;
			}
			if (savePif)
			{
				new DwmPifCollection
				{
					dwmPif
				}.Save();
			}
			return dwmPif;
		}
		private DwmVif DiscoverVif(Session session, string serverOpaqueRef, VIF vif, bool saveVif)
		{
			Network networkFromCache = this.GetNetworkFromCache(vif.network.opaque_ref);
			DwmVif dwmVif = new DwmVif(vif.uuid, (networkFromCache == null) ? null : networkFromCache.uuid, this._pool.Uuid);
			dwmVif.MacAddress = vif.MAC;
			dwmVif.DeviceNumber = vif.device;
			if (!this._vifCache.ContainsKey(serverOpaqueRef))
			{
				this._vifCache.Add(serverOpaqueRef, vif);
			}
			else
			{
				this._vifCache[serverOpaqueRef] = vif;
			}
			if (saveVif)
			{
				new DwmVifCollection
				{
					dwmVif
				}.Save();
			}
			return dwmVif;
		}
		private void DiscoverVdi(Session session, string serverOpaqueRef, VDI vdi)
		{
			if (XenCollectorBase.IsValidXenRef(serverOpaqueRef) && Localization.Compare(serverOpaqueRef, "cd", true) != 0)
			{
				this._vdiCache.Add(serverOpaqueRef, vdi);
			}
		}
		private DwmVbd DiscoverVbd(Session session, string serverOpaqueRef, VBD vbd, bool saveVbd)
		{
			string storageUuid = string.Empty;
			string name = vbd.type.ToString();
			long size = 0L;
			long used = 0L;
			if (XenCollectorBase.IsValidXenRef<VDI>(vbd.VDI) && Localization.Compare(vbd.VDI.opaque_ref, "cd", true) != 0)
			{
				VDI vdiRecord = XenCollector.GetVdiRecord(session, vbd.VDI.opaque_ref);
				if (vdiRecord != null)
				{
					if (XenCollectorBase.IsValidXenRef<SR>(vdiRecord.SR))
					{
						SR srFromCache = this.GetSrFromCache(vdiRecord.SR.opaque_ref);
						if (srFromCache != null)
						{
							name = srFromCache.name_label;
							storageUuid = srFromCache.uuid;
						}
					}
					size = vdiRecord.virtual_size;
					used = vdiRecord.physical_utilisation;
				}
			}
			DwmVbd dwmVbd = new DwmVbd(vbd.uuid, name, storageUuid, this._pool.Uuid);
			dwmVbd.DeviceName = vbd.device;
			dwmVbd.DiskType = (int)vbd.type;
			int diskNumber = 0;
			int.TryParse(vbd.userdevice, out diskNumber);
			dwmVbd.DiskNumber = diskNumber;
			dwmVbd.Size = size;
			dwmVbd.Used = used;
			if (!this._vbdCache.ContainsKey(serverOpaqueRef))
			{
				this._vbdCache.Add(serverOpaqueRef, vbd);
			}
			else
			{
				this._vbdCache[serverOpaqueRef] = vbd;
			}
			if (saveVbd)
			{
				new DwmVbdCollection
				{
					dwmVbd
				}.Save();
			}
			return dwmVbd;
		}
		private DwmVirtualMachine DiscoverVm(Session session, string serverOpaqueRef, VM vm, bool saveVm)
		{
			DwmVirtualMachine dwmVirtualMachine = null;
			VmCacheItem vmCacheItem = null;
			bool flag = true;
			vmCacheItem = new VmCacheItem();
			vmCacheItem.xenVM = vm;
			vmCacheItem.serverOpaqueRef = serverOpaqueRef;
			dwmVirtualMachine = new DwmVirtualMachine(vm.uuid, vm.name_label, this._poolId);
			dwmVirtualMachine.Active = true;
			dwmVirtualMachine.Description = vm.name_description;
			vmCacheItem.dwmVM = dwmVirtualMachine;
			XenRef<Host> affinity = vm.affinity;
			dwmVirtualMachine.IsControlDomain = vm.is_control_domain;
			dwmVirtualMachine.MinimumDynamicMemory = vm.memory_dynamic_min;
			dwmVirtualMachine.MaximumDynamicMemory = vm.memory_dynamic_max;
			dwmVirtualMachine.MinimumStaticMemory = vm.memory_static_min;
			dwmVirtualMachine.MaximumStaticMemory = vm.memory_static_max;
			dwmVirtualMachine.TargetMemory = vm.memory_target;
			dwmVirtualMachine.MinimumCpus = (int)vm.VCPUs_at_startup;
			dwmVirtualMachine.HvMemoryMultiplier = vm.HVM_shadow_multiplier;
			dwmVirtualMachine.MemoryOverhead = XenCollector.GetMemoryOverhead(vm);
			List<XenRef<VIF>> vIFs = vm.VIFs;
			for (int i = 0; i < vIFs.Count; i++)
			{
				if (XenCollectorBase.IsValidXenRef<VIF>(vIFs[i]))
				{
					VIF vifFromCache = this.GetVifFromCache(vIFs[i].opaque_ref);
					if (vifFromCache != null)
					{
						Network networkFromCache = this.GetNetworkFromCache(vifFromCache.network.opaque_ref);
						dwmVirtualMachine.NetworkInterfaces.Add(new DwmVif(vifFromCache.uuid, (networkFromCache == null) ? null : networkFromCache.uuid, this._pool.Uuid));
					}
				}
			}
			List<XenRef<VBD>> vBDs = vm.VBDs;
			for (int j = 0; j < vBDs.Count; j++)
			{
				VBD vbdFromCache = this.GetVbdFromCache(vBDs[j].opaque_ref);
				if (vbdFromCache != null)
				{
					XenRef<VDI> vDI = vbdFromCache.VDI;
					if (XenCollectorBase.IsValidXenRef<VDI>(vDI))
					{
						DwmVbd dwmVbd = new DwmVbd(vbdFromCache.uuid, null, null, this._pool.Uuid);
						dwmVbd.DeviceName = vbdFromCache.device;
						dwmVirtualMachine.BlockDevices.Add(dwmVbd);
						VDI vdiFromCache = this.GetVdiFromCache(vDI.opaque_ref);
						if (vdiFromCache != null)
						{
							SR srFromCache = this.GetSrFromCache(vdiFromCache.SR.opaque_ref);
							if (srFromCache != null)
							{
								dwmVirtualMachine.RequiredStorage.Add(new DwmStorageRepository(srFromCache.uuid, srFromCache.name_label, this._pool.Uuid));
								if (!srFromCache.shared)
								{
									Logger.Trace("VM {0} ({1}) is not agile because it requires SR {2} ({3}) that is not shared.", new object[]
									{
										vm.name_label,
										vm.uuid,
										srFromCache.name_label,
										srFromCache.uuid
									});
								}
								flag &= srFromCache.shared;
								dwmVbd.IsNetworkStorage = srFromCache.shared;
							}
						}
					}
				}
			}
			try
			{
				VM.assert_agile(this._session, vm.opaque_ref);
			}
			catch (Failure failure)
			{
				flag = false;
				Logger.Trace("VM {0} ({1}) is not agile because VM.assert_agaile fails ({2}).", new object[]
				{
					vm.name_label,
					vm.uuid,
					failure.Message
				});
			}
			dwmVirtualMachine.IsAgile = flag;
			if (vm.power_state == vm_power_state.Running && !vm.is_control_domain)
			{
				try
				{
					VM.record_data_source(session, serverOpaqueRef, "runstate_fullrun");
					VM.record_data_source(session, serverOpaqueRef, "runstate_full_contention");
					VM.record_data_source(session, serverOpaqueRef, "runstate_concurrency_hazard");
					VM.record_data_source(session, serverOpaqueRef, "runstate_blocked");
					VM.record_data_source(session, serverOpaqueRef, "runstate_partial_run");
					VM.record_data_source(session, serverOpaqueRef, "runstate_partial_contention");
				}
				catch (Failure failure2)
				{
					Logger.Trace("Unable to turn on runstate metrics for vm '{0}'({1}) in pool {2}", new object[]
					{
						vm.name_label,
						vm.uuid,
						this._hostName
					});
					Logger.Trace(failure2.ToString());
				}
			}
			object vmCacheLock = this._vmCacheLock;
			Monitor.Enter(vmCacheLock);
			try
			{
				if (!this._vmCache.ContainsKey(vm.uuid))
				{
					this._vmCache.Add(vm.uuid, vmCacheItem);
				}
				else
				{
					this._vmCache[vm.uuid] = vmCacheItem;
				}
				if (!this._vmCache2.ContainsKey(serverOpaqueRef))
				{
					this._vmCache2.Add(serverOpaqueRef, vm);
				}
				else
				{
					this._vmCache2[serverOpaqueRef] = vm;
				}
			}
			finally
			{
				Monitor.Exit(vmCacheLock);
			}
			if (saveVm)
			{
				new DwmVirtualMachineCollection
				{
					dwmVirtualMachine
				}.Save();
			}
			return dwmVirtualMachine;
		}
		private static long GetMemoryOverhead(VM vm)
		{
			long num = vm.memory_overhead;
			if (num == 0L)
			{
				num = (long)(2.4 * (double)((float)vm.memory_dynamic_min / 4096f));
				num = (long)((double)num * vm.HVM_shadow_multiplier);
				num += num % 4096L;
			}
			return num;
		}
		private DwmVirtualMachine DiscoverVm(Session session, string vmUuid, bool saveVm)
		{
			DwmVirtualMachine result = null;
			Dictionary<XenRef<VM>, VM> dictionary = VM.get_all_records(session);
			foreach (KeyValuePair<XenRef<VM>, VM> current in dictionary)
			{
				if (Localization.Compare(current.Value.uuid, vmUuid, true) == 0)
				{
					current.Value.opaque_ref = current.Key.opaque_ref;
					result = this.DiscoverVm(session, current.Key.opaque_ref, current.Value, saveVm);
					break;
				}
			}
			return result;
		}
		private DwmHost DiscoverHost(Session session, string serverOpaqueRef, Host host, bool saveHostToCache, bool saveHostToDb)
		{
			DwmHost dwmHost = new DwmHost(host.uuid, host.name_label, this._poolId);
			dwmHost.Description = host.name_description;
			dwmHost.NumCpus = host.host_CPUs.Count;
			dwmHost.IsPoolMaster = (Localization.Compare(host.name_label, this._pool.Master, false) == 0);
			dwmHost.Enabled = host.enabled;
			dwmHost.IsEnterpriseOrHigher = this.CheckHostLicense(host);
			dwmHost.IPAddress = host.address;
			dwmHost.PowerState = ((!Host_metrics.get_live(session, host.metrics.opaque_ref)) ? PowerStatus.Off : PowerStatus.On);
			List<XenRef<Host_cpu>> host_CPUs = host.host_CPUs;
			if (host_CPUs != null && host_CPUs.Count > 0 && XenCollectorBase.IsValidXenRef<Host_cpu>(host_CPUs[0]))
			{
				Host_cpu hostCpuRecord = XenCollector.GetHostCpuRecord(session, host_CPUs[0].opaque_ref);
				if (hostCpuRecord != null)
				{
					dwmHost.CpuSpeed = (int)hostCpuRecord.speed;
				}
			}
			Logger.Trace("Host:");
			Logger.Trace("   Name:  {0}", new object[]
			{
				host.name_label
			});
			Logger.Trace("   Uuid:  {0}", new object[]
			{
				host.uuid
			});
			Logger.Trace("   ha_network_peers:  {0}", new object[]
			{
				(host.ha_network_peers == null || host.ha_network_peers.Length <= 0) ? "none" : host.ha_network_peers[0]
			});
			Logger.Trace("   ha_statefiles:     {0}", new object[]
			{
				(host.ha_statefiles == null || host.ha_statefiles.Length <= 0) ? "none" : host.ha_statefiles[0]
			});
			Logger.Trace("   Enabled            {0}", new object[]
			{
				dwmHost.Enabled
			});
			Logger.Trace("   PowerState         {0}", new object[]
			{
				dwmHost.PowerState
			});
			Logger.Trace(string.Empty);
			List<XenRef<PIF>> pIFs = host.PIFs;
			int num = 0;
			for (int i = 0; i < pIFs.Count; i++)
			{
				PIF pifFromCache = this.GetPifFromCache(pIFs[i].opaque_ref);
				if (pifFromCache != null)
				{
					Network networkFromCache = this.GetNetworkFromCache(pifFromCache.network.opaque_ref);
					dwmHost.PIFs.Add(new DwmPif(pifFromCache.uuid, pifFromCache.device, (networkFromCache == null) ? null : networkFromCache.uuid, this._pool.Uuid));
					if (pifFromCache.physical)
					{
						num++;
					}
				}
			}
			dwmHost.NumNics = num;
			List<XenRef<PBD>> pBDs = host.PBDs;
			for (int j = 0; j < pBDs.Count; j++)
			{
				PBD pbdFromCache = this.GetPbdFromCache(pBDs[j].opaque_ref);
				if (pbdFromCache != null)
				{
					SR srFromCache = this.GetSrFromCache(pbdFromCache.SR.opaque_ref);
					if (srFromCache != null)
					{
						dwmHost.AvailableStorage.Add(new DwmStorageRepository(srFromCache.uuid, srFromCache.name_label, this._pool.Uuid));
						DwmPbd dwmPbd = new DwmPbd(pbdFromCache.uuid, string.Empty, srFromCache.uuid, this._pool.Uuid);
						dwmPbd.CurrentlyAttached = pbdFromCache.currently_attached;
						dwmHost.PBDs.Add(dwmPbd);
					}
				}
			}
			long num2 = host.memory_overhead;
			List<XenRef<VM>> resident_VMs = host.resident_VMs;
			Logger.Trace("   Resident VMs:");
			for (int k = 0; k < resident_VMs.Count; k++)
			{
				VM vmFromCache = this.GetVmFromCache(resident_VMs[k].opaque_ref);
				if (vmFromCache != null)
				{
					Logger.Trace("   Name:  {0}", new object[]
					{
						vmFromCache.name_label
					});
					Logger.Trace("   Uuid:  {0}", new object[]
					{
						vmFromCache.uuid
					});
					Logger.Trace("   After crash:  {0}", new object[]
					{
						vmFromCache.actions_after_crash
					});
					Logger.Trace("   After reboot: {0}", new object[]
					{
						vmFromCache.actions_after_reboot
					});
					Logger.Trace("   After shutdown: {0}", new object[]
					{
						vmFromCache.actions_after_shutdown
					});
					Logger.Trace("   power_state:  {0}", new object[]
					{
						vmFromCache.power_state
					});
					DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(vmFromCache.uuid, vmFromCache.name_label, this._poolId);
					dwmVirtualMachine.Description = vmFromCache.name_description;
					dwmHost.VirtualMachines.Add(dwmVirtualMachine);
					if (num2 == 0L && vmFromCache.is_control_domain)
					{
						num2 = vmFromCache.memory_target;
					}
				}
			}
			dwmHost.MemoryOverhead = num2;
			HostCacheItem hostCacheItem = new HostCacheItem();
			hostCacheItem.serverOpaqueRef = serverOpaqueRef;
			hostCacheItem.xenHost = host;
			hostCacheItem.dwmHost = dwmHost;
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				if (!this._hostCache.ContainsKey(host.uuid))
				{
					this._hostCache.Add(host.uuid, hostCacheItem);
				}
				else
				{
					this._hostCache[host.uuid] = hostCacheItem;
				}
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
			if (saveHostToCache)
			{
				int index;
				if ((index = this._pool.Hosts.IndexOf(dwmHost.Uuid)) != -1)
				{
					this._pool.Hosts[index] = dwmHost;
				}
				else
				{
					this._pool.Hosts.Add(dwmHost);
				}
				if (saveHostToDb)
				{
					this._pool.SavePoolChildren();
				}
			}
			return dwmHost;
		}
		private DwmHost DiscoverHost(Session session, string hostUuid, bool saveHostToCache, bool saveHostToDb)
		{
			DwmHost result = null;
			Dictionary<XenRef<Host>, Host> dictionary = Host.get_all_records(session);
			foreach (KeyValuePair<XenRef<Host>, Host> current in dictionary)
			{
				if (Localization.Compare(current.Value.uuid, hostUuid, true) == 0)
				{
					result = this.DiscoverHost(session, current.Key.opaque_ref, current.Value, saveHostToCache, saveHostToDb);
					break;
				}
			}
			return result;
		}
		private bool HostCacheContainsKey(string key)
		{
			bool result = false;
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				result = this._hostCache.ContainsKey(key);
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
			return result;
		}
		private bool HostCacheTryGetValue(string key, out HostCacheItem value)
		{
			bool result = false;
			value = null;
			object hostCacheLock = this._hostCacheLock;
			Monitor.Enter(hostCacheLock);
			try
			{
				result = this._hostCache.TryGetValue(key, out value);
			}
			finally
			{
				Monitor.Exit(hostCacheLock);
			}
			return result;
		}
		private bool VmCacheContainsKey(string key)
		{
			bool result = false;
			object vmCacheLock = this._vmCacheLock;
			Monitor.Enter(vmCacheLock);
			try
			{
				result = this._vmCache.ContainsKey(key);
			}
			finally
			{
				Monitor.Exit(vmCacheLock);
			}
			return result;
		}
		private VmCacheItem VmCacheGetValue(string key)
		{
			VmCacheItem result = null;
			object vmCacheLock = this._vmCacheLock;
			Monitor.Enter(vmCacheLock);
			try
			{
				this._vmCache.TryGetValue(key, out result);
			}
			finally
			{
				Monitor.Exit(vmCacheLock);
			}
			return result;
		}
		private VM GetVmFromCache(string serverOpaqueRef)
		{
			VM vM = null;
			object vmCacheLock = this._vmCacheLock;
			Monitor.Enter(vmCacheLock);
			try
			{
				if (!this._vmCache2.TryGetValue(serverOpaqueRef, out vM))
				{
					vM = XenCollector.GetVmRecord(this._session, serverOpaqueRef);
					this.DiscoverVm(this._session, serverOpaqueRef, vM, true);
				}
			}
			finally
			{
				Monitor.Exit(vmCacheLock);
			}
			return vM;
		}
		private SR GetSrFromCache(string serverOpaqueRef)
		{
			SR sR = null;
			object srCacheLock = this._srCacheLock;
			Monitor.Enter(srCacheLock);
			try
			{
				if (!this._srCache.TryGetValue(serverOpaqueRef, out sR))
				{
					sR = XenCollector.GetSrRecord(this._session, serverOpaqueRef);
					this.DiscoverSr(this._session, serverOpaqueRef, sR, true);
				}
			}
			finally
			{
				Monitor.Exit(srCacheLock);
			}
			return sR;
		}
		private PBD GetPbdFromCache(string serverOpaqueRef)
		{
			PBD pBD = null;
			object pbdCacheLock = this._pbdCacheLock;
			Monitor.Enter(pbdCacheLock);
			try
			{
				if (!this._pbdCache.TryGetValue(serverOpaqueRef, out pBD))
				{
					pBD = XenCollector.GetPbdRecord(this._session, serverOpaqueRef);
					this.DiscoverPbd(this._session, serverOpaqueRef, pBD, true);
				}
			}
			finally
			{
				Monitor.Exit(pbdCacheLock);
			}
			return pBD;
		}
		private VDI GetVdiFromCache(string serverOpaqueRef)
		{
			VDI vDI = null;
			object vdiCacheLock = this._vdiCacheLock;
			Monitor.Enter(vdiCacheLock);
			try
			{
				if (!this._vdiCache.TryGetValue(serverOpaqueRef, out vDI))
				{
					vDI = XenCollector.GetVdiRecord(this._session, serverOpaqueRef);
					this.DiscoverVdi(this._session, serverOpaqueRef, vDI);
				}
			}
			finally
			{
				Monitor.Exit(vdiCacheLock);
			}
			return vDI;
		}
		private VBD GetVbdFromCache(string serverOpaqueRef)
		{
			VBD vBD = null;
			object vbdCacheLock = this._vbdCacheLock;
			Monitor.Enter(vbdCacheLock);
			try
			{
				if (!this._vbdCache.TryGetValue(serverOpaqueRef, out vBD))
				{
					vBD = XenCollector.GetVbdRecord(this._session, serverOpaqueRef);
					this.DiscoverVbd(this._session, serverOpaqueRef, vBD, true);
				}
			}
			finally
			{
				Monitor.Exit(vbdCacheLock);
			}
			return vBD;
		}
		private Network GetNetworkFromCache(string serverOpaqueRef)
		{
			Network network = null;
			object netCacheLock = this._netCacheLock;
			Monitor.Enter(netCacheLock);
			try
			{
				if (!this._networkCache.TryGetValue(serverOpaqueRef, out network))
				{
					network = XenCollector.GetNetworkRecord(this._session, serverOpaqueRef);
					this.DiscoverNetwork(this._session, serverOpaqueRef, network, true);
				}
			}
			finally
			{
				Monitor.Exit(netCacheLock);
			}
			return network;
		}
		private PIF GetPifFromCache(string serverOpaqueRef)
		{
			PIF pIF = null;
			object pifCacheLock = this._pifCacheLock;
			Monitor.Enter(pifCacheLock);
			try
			{
				if (!this._pifCache.TryGetValue(serverOpaqueRef, out pIF))
				{
					pIF = XenCollector.GetPifRecord(this._session, serverOpaqueRef);
					this.DiscoverPif(this._session, serverOpaqueRef, pIF, true);
				}
			}
			finally
			{
				Monitor.Exit(pifCacheLock);
			}
			return pIF;
		}
		private VIF GetVifFromCache(string serverOpaqueRef)
		{
			VIF vIF = null;
			object vifCacheLock = this._vifCacheLock;
			Monitor.Enter(vifCacheLock);
			try
			{
				if (!this._vifCache.TryGetValue(serverOpaqueRef, out vIF))
				{
					vIF = XenCollector.GetVifRecord(this._session, serverOpaqueRef);
					this.DiscoverVif(this._session, serverOpaqueRef, vIF, true);
				}
			}
			finally
			{
				Monitor.Exit(vifCacheLock);
			}
			return vIF;
		}
		private bool IsVbdNetworked(VmCacheItem item, string deviceName)
		{
			bool result = false;
			if (item != null && item.dwmVM != null && item.dwmVM.BlockDevices != null)
			{
				DwmVbd deviceByName = item.dwmVM.BlockDevices.GetDeviceByName(deviceName);
				if (deviceByName != null)
				{
					result = deviceByName.IsNetworkStorage;
				}
			}
			return result;
		}
		private bool GetWlbEnabled(Session session, Pool xenPool)
		{
			bool flag = xenPool.wlb_enabled;
			if (!flag)
			{
				Host hostRecord = XenCollector.GetHostRecord(session, xenPool.master.opaque_ref);
				if (hostRecord != null)
				{
					int num;
					int num2;
					int hostVersion = this.GetHostVersion(hostRecord, out num, out num2);
					if (hostVersion == 5 && num == 0)
					{
						flag = true;
					}
				}
			}
			return flag;
		}
		private bool CheckHostLicense(Host host)
		{
			bool result = false;
			if (host.license_params != null)
			{
				bool flag = true;
				if (host.license_params.ContainsKey("restrict_wlb"))
				{
					if (bool.TryParse(host.license_params["restrict_wlb"], out flag))
					{
						result = !flag;
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					Logger.LogWarning("Host {0}({1}) is not licensed.", new object[]
					{
						host.name_label,
						host.uuid
					});
				}
			}
			return result;
		}
        //bool ICollector.get_IsAlive()
        //{
        //    return this.IsAlive;
        //}
	}
}
