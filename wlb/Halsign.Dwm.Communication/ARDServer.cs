using Halsign.DWM.Domain;
using Halsign.DWM.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;
using System.ServiceModel;
using System.Text;
namespace Halsign.DWM.Communication
{
	public class ARDServer : CommunicationBase, IScheduledTask, IARDServer
	{
		private bool authenticateTests = true;
		private static string ScheduledTaskSpecialKey = "set_scheduled_task";
		private static string HostConfigurationSpecialKey = "set_host_configuration";
		private static string GetReportDefinitionsKey = "get_report_definitions";
		private bool ValidWLBAuthenticationToken()
		{
			bool result = false;
			try
			{
				object obj;
				OperationContext.Current.IncomingMessageProperties.TryGetValue("WLBAuthenticated", out obj);
				bool.TryParse(obj.ToString(), out result);
			}
			catch
			{
				if (this.authenticateTests)
				{
					result = true;
				}
				else
				{
					CommunicationBase.Trace("Invalid or missing authentication token", new object[0]);
					result = false;
				}
			}
			return result;
		}
		internal static void LoadCache()
		{
			DwmPool.LoadCache();
		}
		internal static void UnloadCache()
		{
			DwmPool.UnloadCache();
		}
		public XenServerResponse AddvGateServer(XenServerRequest request)
		{
			XenServerResponse xenServerResponse = new XenServerResponse();
			xenServerResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.AddXenServer called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						DwmPool dwmPool;
						if (string.IsNullOrEmpty(request.PoolName) && string.IsNullOrEmpty(request.PoolUuid))
						{
							dwmPool = new DwmPool(null, Guid.NewGuid().ToString(), DwmHypervisorType.XenServer);
						}
						else
						{
							dwmPool = new DwmPool(request.PoolUuid, request.PoolName, DwmHypervisorType.XenServer);
							dwmPool.Load();
						}
						CommunicationBase.Trace("IWorkloadBalance.AddXenServer:  PoolUuid={0}", new object[]
						{
							request.PoolUuid
						});
						CommunicationBase.Trace("IWorkloadBalance.AddXenServer:  PoolName={0}", new object[]
						{
							request.PoolName
						});
						CommunicationBase.Trace("IWorkloadBalance.AddXenServer:  Protocol={0}", new object[]
						{
							request.Protocol
						});
						CommunicationBase.Trace("IWorkloadBalance.AddXenServer:  HostName={0}", new object[]
						{
							request.HostName
						});
						CommunicationBase.Trace("IWorkloadBalance.AddXenServer:  Port    ={0}", new object[]
						{
							request.Port
						});
						CommunicationBase.Trace("IWorkloadBalance.AddXenServer:  UserName={0}", new object[]
						{
							request.UserName
						});
						CommunicationBase.Trace("IWorkloadBalance.AddXenServer:  vGateServerUrl={0}", new object[]
						{
							request.vGateServerUrl
						});
						if (!string.IsNullOrEmpty(request.Protocol) && !string.IsNullOrEmpty(request.HostName) && request.Port != 0)
						{
							dwmPool.Protocol = request.Protocol;
							dwmPool.PrimaryPoolMasterAddr = request.HostName;
							dwmPool.PrimaryPoolMasterPort = request.Port;
							dwmPool.UserName = request.UserName;
							dwmPool.Password = request.Password;
						}
						else
						{
							if (!string.IsNullOrEmpty(request.vGateServerUrl))
							{
								Uri uri = null;
								if (Uri.TryCreate(request.vGateServerUrl, UriKind.RelativeOrAbsolute, out uri) && uri != null && uri.PathAndQuery == "/")
								{
									IPAddress iPAddress;
									if (IPAddress.TryParse(uri.Host, out iPAddress))
									{
										dwmPool.Protocol = uri.Scheme;
										dwmPool.PrimaryPoolMasterAddr = uri.Host;
										dwmPool.PrimaryPoolMasterPort = uri.Port;
										dwmPool.UserName = request.UserName;
										dwmPool.Password = request.Password;
									}
									else
									{
										xenServerResponse.ResultCode = 4007;
									}
								}
								else
								{
									xenServerResponse.ResultCode = 4007;
								}
							}
							else
							{
								xenServerResponse.ResultCode = 4007;
							}
						}
						if (xenServerResponse.ResultCode == 0)
						{
							if (DwmPool.IsValidPool(dwmPool.PrimaryPoolMasterAddr, dwmPool.PrimaryPoolMasterPort, dwmPool.UserName, dwmPool.Password, DwmHypervisorType.XenServer))
							{
								dwmPool.Save();
								dwmPool.SaveThresholds();
								xenServerResponse.Id = dwmPool.Id;
							}
							else
							{
								xenServerResponse.ResultCode = 4002;
								xenServerResponse.ErrorMessage = "Cannot initialize a hypervisor session using the supplied address and credentials";
							}
						}
					}
					else
					{
						xenServerResponse.ResultCode = 4004;
					}
				}
				else
				{
					xenServerResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				xenServerResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				xenServerResponse.ErrorMessage = ex.Message;
			}
			return xenServerResponse;
		}
		public XenServerResponse RemovevGateServer(XenServerRequest request)
		{
			XenServerResponse xenServerResponse = new XenServerResponse();
			xenServerResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.RemoveXenServer called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						CommunicationBase.Trace("IWorkloadBalance.RemoveXenServer:  PoolUuid={0}", new object[]
						{
							request.PoolUuid
						});
						CommunicationBase.Trace("IWorkloadBalance.RemoveXenServer:  PoolId  ={0}", new object[]
						{
							request.PoolId
						});
						DwmPool dwmPool = null;
						if (request.PoolId > 0)
						{
							dwmPool = new DwmPool(request.PoolId);
						}
						else
						{
							if (!string.IsNullOrEmpty(request.PoolUuid))
							{
								dwmPool = new DwmPool(request.PoolUuid, null, DwmHypervisorType.XenServer);
							}
						}
						if (dwmPool != null)
						{
							dwmPool.Delete();
						}
						else
						{
							xenServerResponse.ResultCode = 4007;
						}
					}
					else
					{
						xenServerResponse.ResultCode = 4004;
					}
				}
				else
				{
					xenServerResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				xenServerResponse.ErrorMessage = ex.Message;
				xenServerResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
			}
			return xenServerResponse;
		}
		private SetConfigurationResponse InternalSetScheduledTask(SetConfigurationRequest request)
		{
			SetConfigurationResponse setConfigurationResponse = new SetConfigurationResponse();
			setConfigurationResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.SetScheduledTask called via SetXenPoolConfiguration", new object[0]);
			try
			{
				DwmPool dwmPool = null;
				if (request.PoolId > 0)
				{
					dwmPool = new DwmPool(request.PoolId);
				}
				else
				{
					if (!string.IsNullOrEmpty(request.PoolUuid) || !string.IsNullOrEmpty(request.PoolName))
					{
						dwmPool = new DwmPool(request.PoolUuid, request.PoolName, DwmHypervisorType.XenServer);
					}
					else
					{
						setConfigurationResponse.ResultCode = 4007;
					}
				}
				WlbTaskCollection wlbTaskCollection = new WlbTaskCollection();
				wlbTaskCollection.Load(dwmPool.Id);
				List<string> list = new List<string>();
				list.Add(ARDServer.ScheduledTaskSpecialKey);
				foreach (KeyValuePair<string, string> current in request.OptimizationParms)
				{
					if (Localization.StringStartsWith(current.Key, "schedTask_", true))
					{
						string[] array = current.Key.Split(new char[]
						{
							'_'
						});
						if (array.Length == 3)
						{
							WlbTask wlbTask = null;
							string text = array[2];
							int num;
							if (int.TryParse(array[1], out num))
							{
								wlbTask = wlbTaskCollection.GetItem(num);
								if (wlbTask == null && num <= 0)
								{
									wlbTask = new WlbTask(num);
									wlbTask.PoolId = dwmPool.Id;
									wlbTaskCollection.Add(wlbTask);
								}
							}
							if (wlbTask != null)
							{
								if (Localization.Compare(text, "TaskDelete", false) == 0)
								{
									bool flag = false;
									if (bool.TryParse(current.Value, out flag) && flag)
									{
										wlbTask.Delete();
										wlbTaskCollection.Remove(wlbTask);
									}
								}
								if (Localization.Compare(text, "TaskName", false) == 0)
								{
									wlbTask.Name = current.Value;
								}
								else
								{
									if (Localization.Compare(text, "TaskDescription", false) == 0)
									{
										wlbTask.Description = current.Value;
									}
									else
									{
										if (Localization.Compare(text, "TaskEnabled", false) == 0)
										{
											bool enabled = false;
											bool.TryParse(current.Value, out enabled);
											wlbTask.Enabled = enabled;
										}
										else
										{
											if (Localization.Compare(text, "TaskOwner", false) == 0)
											{
												wlbTask.Owner = current.Value;
											}
											else
											{
												if (Localization.Compare(text, "TaskLastTouchedBy", false) == 0)
												{
													wlbTask.LastTouchedBy = current.Value;
												}
												else
												{
													if (Localization.Compare(text, "TaskLastTouched", false) == 0)
													{
														wlbTask.LastTouched = Localization.ParseDateTime(current.Value);
													}
													else
													{
														if (Localization.Compare(text, "TriggerType", false) == 0)
														{
															wlbTask.Trigger.Type = (WlbTriggerType)Enum.Parse(typeof(WlbTriggerType), current.Value);
														}
														else
														{
															if (Localization.Compare(text, "TriggerDaysOfWeek", false) == 0)
															{
																wlbTask.Trigger.DaysOfWeek = (TriggerDaysOfWeek)Enum.Parse(typeof(TriggerDaysOfWeek), current.Value);
															}
															else
															{
																if (Localization.Compare(text, "TriggerExecuteTime", false) == 0)
																{
																	wlbTask.Trigger.ExecuteTime = Localization.ParseDateTime(current.Value);
																}
																else
																{
																	if (Localization.Compare(text, "TriggerEndabledDate", false) == 0)
																	{
																		wlbTask.Trigger.EnableDate = Localization.ParseDateTime(current.Value);
																	}
																	else
																	{
																		if (Localization.Compare(text, "TriggerDisabledDate", false) == 0)
																		{
																			wlbTask.Trigger.DisableDate = Localization.ParseDateTime(current.Value);
																		}
																		else
																		{
																			if (Localization.Compare(text, "ActionType", false) == 0)
																			{
																				int type = 0;
																				int.TryParse(current.Value, out type);
																				wlbTask.Trigger.Action.Type = type;
																			}
																			else
																			{
																				if (Localization.Compare(text, "TaskLastRunResult", false) != 0)
																				{
																					if (Localization.Compare(text, "TriggerLastRun", false) != 0)
																					{
																						if (wlbTask.Trigger.Action.Parameters.ContainsKey(text))
																						{
																							wlbTask.Trigger.Action.Parameters[text] = current.Value;
																						}
																						else
																						{
																							wlbTask.Trigger.Action.Parameters.Add(text, current.Value);
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
						list.Add(current.Key);
					}
				}
				wlbTaskCollection.Save();
				foreach (string current2 in list)
				{
					request.OptimizationParms.Remove(current2);
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				setConfigurationResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				setConfigurationResponse.ErrorMessage = ex.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.SetScheduledTask return {0} on behalf of SetXenPoolConfiguration", new object[]
			{
				setConfigurationResponse.ResultCode
			});
			return setConfigurationResponse;
		}
		private Dictionary<string, string> InternalGetScheduledTask(GetConfigurationRequest request)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (request != null)
			{
				WlbTaskCollection wlbTaskCollection = new WlbTaskCollection();
				if (!string.IsNullOrEmpty(request.PoolUuid))
				{
					List<WlbActionType> taskActionTypes = WlbTask.GetTaskActionTypes();
					foreach (WlbActionType current in taskActionTypes)
					{
						if (Localization.Compare(current.Name, "SetOptimizationMode", true) == 0)
						{
							wlbTaskCollection.Load(request.PoolUuid, current.Type);
							break;
						}
					}
				}
				else
				{
					if (request.PoolId > 0)
					{
						wlbTaskCollection.Load(request.PoolId, "SetOptimizationMode");
					}
				}
				foreach (WlbTask current2 in wlbTaskCollection)
				{
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TaskName"), current2.Name);
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TaskDescription"), current2.Description);
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TaskEnabled"), current2.Enabled.ToString());
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TaskOwner"), current2.Owner);
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TaskLastRunResult"), (!current2.LastRunResult) ? "0" : "1");
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TaskLastTouchedBy"), current2.LastTouchedBy);
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TaskLastTouched"), Localization.DateTimeToSqlString(current2.LastTouched));
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TriggerType"), ((int)current2.Trigger.Type).ToString());
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TriggerDaysOfWeek"), ((int)current2.Trigger.DaysOfWeek).ToString());
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TriggerExecuteTime"), Localization.DateTimeToSqlString(current2.Trigger.ExecuteTime));
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TriggerLastRun"), Localization.DateTimeToSqlString(current2.Trigger.LastRun));
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TriggerEndabledDate"), Localization.DateTimeToSqlString(current2.Trigger.EnableDate));
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "TriggerDisabledDate"), Localization.DateTimeToSqlString(current2.Trigger.DisableDate));
					dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), "ActionType"), current2.Trigger.Action.Type.ToString());
					foreach (string current3 in current2.Trigger.Action.Parameters.Keys)
					{
						dictionary.Add(string.Format("{0}_{1}_{2}", "schedTask", current2.Id.ToString(), current3), current2.Trigger.Action.Parameters[current3]);
					}
				}
			}
			return dictionary;
		}
		private SetConfigurationResponse InternalSetHostConfiguration(SetConfigurationRequest request)
		{
			SetConfigurationResponse setConfigurationResponse = new SetConfigurationResponse();
			setConfigurationResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.SetHostConfiguration called via SetXenPoolConfiguration", new object[0]);
			if (request != null && request.OptimizationParms != null)
			{
				List<string> list = new List<string>();
				list.Add(ARDServer.HostConfigurationSpecialKey);
				foreach (KeyValuePair<string, string> current in request.OptimizationParms)
				{
					CommunicationBase.Trace("IWorkloadBalance.SetHostConfiguration:  {0}={1}", new object[]
					{
						current.Key,
						current.Value
					});
					string key = current.Key;
					if (key.StartsWith("host_") && key.IndexOf('_', 6) == 41)
					{
						string[] array = key.Split(new char[]
						{
							'_'
						});
						if (array.Length == 3)
						{
							string uuid = array[1];
							string name = array[2];
							DwmHost dwmHost = null;
							if (!string.IsNullOrEmpty(request.PoolUuid))
							{
								dwmHost = new DwmHost(uuid, null, request.PoolUuid);
							}
							else
							{
								if (request.PoolId > 0)
								{
									dwmHost = new DwmHost(uuid, null, request.PoolId);
								}
								else
								{
									setConfigurationResponse.ResultCode = 4007;
								}
							}
							if (dwmHost != null)
							{
								try
								{
									dwmHost.SetOtherConfig(name, current.Value);
								}
								catch
								{
									setConfigurationResponse.ResultCode = 4023;
								}
							}
							list.Add(current.Key);
						}
					}
				}
				foreach (string current2 in list)
				{
					request.OptimizationParms.Remove(current2);
				}
			}
			CommunicationBase.Trace("IWorkloadBalance.SetHostConfiguration return {0} on behalf of SetXenPoolConfiguration", new object[]
			{
				setConfigurationResponse.ResultCode
			});
			return setConfigurationResponse;
		}
		private Dictionary<string, string> InternalGetHostConfiguration(GetConfigurationRequest request)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (request != null)
			{
				DwmHostCollection dwmHostCollection = new DwmHostCollection();
				if (!string.IsNullOrEmpty(request.PoolUuid))
				{
					dwmHostCollection.Load(request.PoolUuid);
				}
				else
				{
					if (request.PoolId > 0)
					{
						dwmHostCollection.Load(request.PoolId);
					}
				}
				for (int i = 0; i < dwmHostCollection.Count; i++)
				{
					Dictionary<string, string> otherConfig = dwmHostCollection[i].GetOtherConfig();
					if (otherConfig != null && otherConfig.Count > 0)
					{
						foreach (KeyValuePair<string, string> current in otherConfig)
						{
							dictionary.Add(string.Format("host_{0}_{1}", dwmHostCollection[i].Uuid, current.Key), current.Value);
						}
					}
				}
			}
			return dictionary;
		}
		public SetConfigurationResponse SetXenPoolConfiguration(SetConfigurationRequest request)
		{
			SetConfigurationResponse setConfigurationResponse = new SetConfigurationResponse();
			setConfigurationResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.SetXenPoolConfiguration called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						if (request.OptimizationParms != null && request.OptimizationParms.Count > 0)
						{
							if (request.OptimizationParms.ContainsKey(ARDServer.ScheduledTaskSpecialKey))
							{
								SetConfigurationResponse setConfigurationResponse2 = this.InternalSetScheduledTask(request);
								if (setConfigurationResponse2.ResultCode != 0)
								{
									SetConfigurationResponse result = setConfigurationResponse2;
									return result;
								}
							}
							if (request.OptimizationParms.ContainsKey(ARDServer.HostConfigurationSpecialKey))
							{
								SetConfigurationResponse setConfigurationResponse3 = this.InternalSetHostConfiguration(request);
								if (setConfigurationResponse3.ResultCode != 0)
								{
									SetConfigurationResponse result = setConfigurationResponse3;
									return result;
								}
							}
						}
						CommunicationBase.Trace("IWorkloadBalance.SetXenPoolConfiguration:  PoolUuid={0}", new object[]
						{
							request.PoolUuid
						});
						CommunicationBase.Trace("IWorkloadBalance.SetXenPoolConfiguration:  PoolId  ={0}", new object[]
						{
							request.PoolId
						});
						DwmPool dwmPool = null;
						Dictionary<string, string> dictionary = null;
						if (request.PoolId > 0)
						{
							dwmPool = new DwmPool(request.PoolId);
						}
						else
						{
							if (!string.IsNullOrEmpty(request.PoolUuid) || !string.IsNullOrEmpty(request.PoolName))
							{
								dwmPool = new DwmPool(request.PoolUuid, request.PoolName, DwmHypervisorType.XenServer);
							}
							else
							{
								setConfigurationResponse.ResultCode = 4007;
							}
						}
						if (setConfigurationResponse.ResultCode == 0 && request.OptimizationParms != null && request.OptimizationParms.Count > 0)
						{
							if (dwmPool != null)
							{
								dwmPool.Load();
							}
							foreach (KeyValuePair<string, string> current in request.OptimizationParms)
							{
								try
								{
									CommunicationBase.Trace("IWorkloadBalance.SetXenPoolConfiguration:  {0}={1}", new object[]
									{
										current.Key,
										current.Value
									});
									if (Localization.Compare(current.Key, "OptimizationMode", true) == 0)
									{
										int num;
										if (int.TryParse(current.Value, out num) && num >= 0 && num <= 1)
										{
											dwmPool.OptMode = (OptimizationMode)num;
										}
									}
									else
									{
										if (Localization.Compare(current.Key, "OverCommitCpuInPerfMode", true) == 0)
										{
											bool flag;
											if (bool.TryParse(current.Value, out flag))
											{
												dwmPool.OverCommitCpusInPerfMode = flag;
											}
										}
										else
										{
											if (Localization.Compare(current.Key, "OverCommitCpuInDensityMode", true) == 0)
											{
												bool flag;
												if (bool.TryParse(current.Value, out flag))
												{
													dwmPool.OverCommitCpusInDensityMode = flag;
												}
											}
											else
											{
												if (Localization.Compare(current.Key, "HostCpuThresholdCritical", true) == 0)
												{
													dwmPool.HostCpuThreshold.Critical = ARDServer.ParseDouble(current.Value, dwmPool.HostCpuThreshold.Critical, setConfigurationResponse);
												}
												else
												{
													if (Localization.Compare(current.Key, "HostCpuThresholdHigh", true) == 0)
													{
														dwmPool.HostCpuThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.HostCpuThreshold.High, setConfigurationResponse);
													}
													else
													{
														if (Localization.Compare(current.Key, "HostCpuThresholdMedium", true) == 0)
														{
															dwmPool.HostCpuThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.HostCpuThreshold.Medium, setConfigurationResponse);
														}
														else
														{
															if (Localization.Compare(current.Key, "HostCpuThresholdLow", true) == 0)
															{
																dwmPool.HostCpuThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.HostCpuThreshold.Low, setConfigurationResponse);
															}
															else
															{
																if (Localization.Compare(current.Key, "HostMemoryThresholdCritical", true) == 0)
																{
																	dwmPool.HostMemoryThreshold.Critical = ARDServer.ParseDouble(current.Value, dwmPool.HostMemoryThreshold.Critical, setConfigurationResponse);
																}
																else
																{
																	if (Localization.Compare(current.Key, "HostMemoryThresholdHigh", true) == 0)
																	{
																		dwmPool.HostMemoryThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.HostMemoryThreshold.High, setConfigurationResponse);
																	}
																	else
																	{
																		if (Localization.Compare(current.Key, "HostMemoryThresholdMedium", true) == 0)
																		{
																			dwmPool.HostMemoryThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.HostMemoryThreshold.Medium, setConfigurationResponse);
																		}
																		else
																		{
																			if (Localization.Compare(current.Key, "HostMemoryThresholdLow", true) == 0)
																			{
																				dwmPool.HostMemoryThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.HostMemoryThreshold.Low, setConfigurationResponse);
																			}
																			else
																			{
																				if (Localization.Compare(current.Key, "HostPifReadThresholdCritical", true) == 0)
																				{
																					dwmPool.HostPifReadThreshold.Critical = ARDServer.ParseDouble(current.Value, dwmPool.HostPifReadThreshold.Critical, setConfigurationResponse);
																				}
																				else
																				{
																					if (Localization.Compare(current.Key, "HostPifReadThresholdHigh", true) == 0)
																					{
																						dwmPool.HostPifReadThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.HostPifReadThreshold.High, setConfigurationResponse);
																					}
																					else
																					{
																						if (Localization.Compare(current.Key, "HostPifReadThresholdMedium", true) == 0)
																						{
																							dwmPool.HostPifReadThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.HostPifReadThreshold.Medium, setConfigurationResponse);
																						}
																						else
																						{
																							if (Localization.Compare(current.Key, "HostPifReadThresholdLow", true) == 0)
																							{
																								dwmPool.HostPifReadThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.HostPifReadThreshold.Low, setConfigurationResponse);
																							}
																							else
																							{
																								if (Localization.Compare(current.Key, "HostPifWriteThresholdCritical", true) == 0)
																								{
																									dwmPool.HostPifWriteThreshold.Critical = ARDServer.ParseDouble(current.Value, dwmPool.HostPifWriteThreshold.Critical, setConfigurationResponse);
																								}
																								else
																								{
																									if (Localization.Compare(current.Key, "HostPifWriteThresholdHigh", true) == 0)
																									{
																										dwmPool.HostPifWriteThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.HostPifWriteThreshold.High, setConfigurationResponse);
																									}
																									else
																									{
																										if (Localization.Compare(current.Key, "HostPifWriteThresholdMedium", true) == 0)
																										{
																											dwmPool.HostPifWriteThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.HostPifWriteThreshold.Medium, setConfigurationResponse);
																										}
																										else
																										{
																											if (Localization.Compare(current.Key, "HostPifWriteThresholdLow", true) == 0)
																											{
																												dwmPool.HostPifWriteThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.HostPifWriteThreshold.Low, setConfigurationResponse);
																											}
																											else
																											{
																												if (Localization.Compare(current.Key, "HostPbdReadThresholdCritical", true) == 0)
																												{
																													dwmPool.HostPbdReadThreshold.Critical = ARDServer.ParseDouble(current.Value, dwmPool.HostPbdReadThreshold.Critical, setConfigurationResponse);
																												}
																												else
																												{
																													if (Localization.Compare(current.Key, "HostPbdReadThresholdHigh", true) == 0)
																													{
																														dwmPool.HostPbdReadThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.HostPbdReadThreshold.High, setConfigurationResponse);
																													}
																													else
																													{
																														if (Localization.Compare(current.Key, "HostPbdReadThresholdMedium", true) == 0)
																														{
																															dwmPool.HostPbdReadThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.HostPbdReadThreshold.Medium, setConfigurationResponse);
																														}
																														else
																														{
																															if (Localization.Compare(current.Key, "HostPbdReadThresholdLow", true) == 0)
																															{
																																dwmPool.HostPbdReadThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.HostPbdReadThreshold.Low, setConfigurationResponse);
																															}
																															else
																															{
																																if (Localization.Compare(current.Key, "HostPbdWriteThresholdCritical", true) == 0)
																																{
																																	dwmPool.HostPbdWriteThreshold.Critical = ARDServer.ParseDouble(current.Value, dwmPool.HostPbdWriteThreshold.Critical, setConfigurationResponse);
																																}
																																else
																																{
																																	if (Localization.Compare(current.Key, "HostPbdWriteThresholdHigh", true) == 0)
																																	{
																																		dwmPool.HostPbdWriteThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.HostPbdWriteThreshold.High, setConfigurationResponse);
																																	}
																																	else
																																	{
																																		if (Localization.Compare(current.Key, "HostPbdWriteThresholdMedium", true) == 0)
																																		{
																																			dwmPool.HostPbdWriteThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.HostPbdWriteThreshold.Medium, setConfigurationResponse);
																																		}
																																		else
																																		{
																																			if (Localization.Compare(current.Key, "HostPbdWriteThresholdLow", true) == 0)
																																			{
																																				dwmPool.HostPbdWriteThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.HostPbdWriteThreshold.Low, setConfigurationResponse);
																																			}
																																			else
																																			{
																																				if (Localization.Compare(current.Key, "HostLoadAverageThresholdCritical", true) == 0)
																																				{
																																					dwmPool.HostLoadAverageThreshold.Critical = ARDServer.ParseDouble(current.Value, dwmPool.HostLoadAverageThreshold.Critical, setConfigurationResponse);
																																				}
																																				else
																																				{
																																					if (Localization.Compare(current.Key, "HostLoadAverageThresholdHigh", true) == 0)
																																					{
																																						dwmPool.HostLoadAverageThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.HostLoadAverageThreshold.High, setConfigurationResponse);
																																					}
																																					else
																																					{
																																						if (Localization.Compare(current.Key, "HostLoadAverageThresholdMedium", true) == 0)
																																						{
																																							dwmPool.HostLoadAverageThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.HostLoadAverageThreshold.Medium, setConfigurationResponse);
																																						}
																																						else
																																						{
																																							if (Localization.Compare(current.Key, "HostLoadAverageThresholdLow", true) == 0)
																																							{
																																								dwmPool.HostLoadAverageThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.HostLoadAverageThreshold.Low, setConfigurationResponse);
																																							}
																																							else
																																							{
																																								if (Localization.Compare(current.Key, "WeightCurrentMetrics", true) == 0)
																																								{
																																									dwmPool.WeightCurrentMetrics = ARDServer.ParseDouble(current.Value, dwmPool.WeightCurrentMetrics, setConfigurationResponse);
																																								}
																																								else
																																								{
																																									if (Localization.Compare(current.Key, "WeightRecentMetrics", true) == 0)
																																									{
																																										dwmPool.WeightRecentMetrics = ARDServer.ParseDouble(current.Value, dwmPool.WeightRecentMetrics, setConfigurationResponse);
																																									}
																																									else
																																									{
																																										if (Localization.Compare(current.Key, "WeightHistoricalMetrics", true) == 0)
																																										{
																																											dwmPool.WeightHistoricMetrics = ARDServer.ParseDouble(current.Value, dwmPool.WeightHistoricMetrics, setConfigurationResponse);
																																										}
																																										else
																																										{
																																											if (Localization.Compare(current.Key, "VmCpuUtilizationThresholdHigh", true) == 0)
																																											{
																																												dwmPool.VmCpuUtilizationThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.VmCpuUtilizationThreshold.High, setConfigurationResponse);
																																											}
																																											else
																																											{
																																												if (Localization.Compare(current.Key, "VmCpuUtilizationThresholdMedium", true) == 0)
																																												{
																																													dwmPool.VmCpuUtilizationThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmCpuUtilizationThreshold.Medium, setConfigurationResponse);
																																												}
																																												else
																																												{
																																													if (Localization.Compare(current.Key, "VmCpuUtilizationThresholdLow", true) == 0)
																																													{
																																														dwmPool.VmCpuUtilizationThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmCpuUtilizationThreshold.Low, setConfigurationResponse);
																																													}
																																													else
																																													{
																																														if (Localization.Compare(current.Key, "VmCpuUtilizationWeightHigh", true) == 0)
																																														{
																																															dwmPool.VmCpuUtilizationWeight.High = ARDServer.ParseDouble(current.Value, dwmPool.VmCpuUtilizationWeight.High, setConfigurationResponse);
																																														}
																																														else
																																														{
																																															if (Localization.Compare(current.Key, "VmCpuUtilizationWeightMedium", true) == 0)
																																															{
																																																dwmPool.VmCpuUtilizationWeight.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmCpuUtilizationWeight.Medium, setConfigurationResponse);
																																															}
																																															else
																																															{
																																																if (Localization.Compare(current.Key, "VmCpuUtilizationWeightLow", true) == 0)
																																																{
																																																	dwmPool.VmCpuUtilizationWeight.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmCpuUtilizationWeight.Low, setConfigurationResponse);
																																																}
																																																else
																																																{
																																																	if (Localization.Compare(current.Key, "VmMemoryThresholdHigh", true) == 0)
																																																	{
																																																		dwmPool.VmMemoryThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.VmMemoryThreshold.High, setConfigurationResponse);
																																																	}
																																																	else
																																																	{
																																																		if (Localization.Compare(current.Key, "VmMemoryThresholdMedium", true) == 0)
																																																		{
																																																			dwmPool.VmMemoryThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmMemoryThreshold.Medium, setConfigurationResponse);
																																																		}
																																																		else
																																																		{
																																																			if (Localization.Compare(current.Key, "VmMemoryThresholdLow", true) == 0)
																																																			{
																																																				dwmPool.VmMemoryThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmMemoryThreshold.Low, setConfigurationResponse);
																																																			}
																																																			else
																																																			{
																																																				if (Localization.Compare(current.Key, "VmMemoryWeightHigh", true) == 0)
																																																				{
																																																					dwmPool.VmMemoryWeight.High = ARDServer.ParseDouble(current.Value, dwmPool.VmMemoryWeight.High, setConfigurationResponse);
																																																				}
																																																				else
																																																				{
																																																					if (Localization.Compare(current.Key, "VmMemoryWeightMedium", true) == 0)
																																																					{
																																																						dwmPool.VmMemoryWeight.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmMemoryWeight.Medium, setConfigurationResponse);
																																																					}
																																																					else
																																																					{
																																																						if (Localization.Compare(current.Key, "VmMemoryWeightLow", true) == 0)
																																																						{
																																																							dwmPool.VmMemoryWeight.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmMemoryWeight.Low, setConfigurationResponse);
																																																						}
																																																						else
																																																						{
																																																							if (Localization.Compare(current.Key, "VmNetworkReadThresholdHigh", true) == 0)
																																																							{
																																																								dwmPool.VmNetworkReadThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkReadThreshold.High, setConfigurationResponse);
																																																							}
																																																							else
																																																							{
																																																								if (Localization.Compare(current.Key, "VmNetworkReadThresholdMedium", true) == 0)
																																																								{
																																																									dwmPool.VmNetworkReadThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkReadThreshold.Medium, setConfigurationResponse);
																																																								}
																																																								else
																																																								{
																																																									if (Localization.Compare(current.Key, "VmNetworkReadThresholdLow", true) == 0)
																																																									{
																																																										dwmPool.VmNetworkReadThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkReadThreshold.Low, setConfigurationResponse);
																																																									}
																																																									else
																																																									{
																																																										if (Localization.Compare(current.Key, "VmNetworkReadWeightHigh", true) == 0)
																																																										{
																																																											dwmPool.VmNetworkReadWeight.High = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkReadWeight.High, setConfigurationResponse);
																																																										}
																																																										else
																																																										{
																																																											if (Localization.Compare(current.Key, "VmNetworkReadWeightMedium", true) == 0)
																																																											{
																																																												dwmPool.VmNetworkReadWeight.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkReadWeight.Medium, setConfigurationResponse);
																																																											}
																																																											else
																																																											{
																																																												if (Localization.Compare(current.Key, "VmNetworkReadWeightLow", true) == 0)
																																																												{
																																																													dwmPool.VmNetworkReadWeight.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkReadWeight.Low, setConfigurationResponse);
																																																												}
																																																												else
																																																												{
																																																													if (Localization.Compare(current.Key, "VmNetworkWriteThresholdHigh", true) == 0)
																																																													{
																																																														dwmPool.VmNetworkWriteThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkWriteThreshold.High, setConfigurationResponse);
																																																													}
																																																													else
																																																													{
																																																														if (Localization.Compare(current.Key, "VmNetworkWriteThresholdMedium", true) == 0)
																																																														{
																																																															dwmPool.VmNetworkWriteThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkWriteThreshold.Medium, setConfigurationResponse);
																																																														}
																																																														else
																																																														{
																																																															if (Localization.Compare(current.Key, "VmNetworkWriteThresholdLow", true) == 0)
																																																															{
																																																																dwmPool.VmNetworkWriteThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkWriteThreshold.Low, setConfigurationResponse);
																																																															}
																																																															else
																																																															{
																																																																if (Localization.Compare(current.Key, "VmNetworkWriteWeightHigh", true) == 0)
																																																																{
																																																																	dwmPool.VmNetworkWriteWeight.High = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkWriteWeight.High, setConfigurationResponse);
																																																																}
																																																																else
																																																																{
																																																																	if (Localization.Compare(current.Key, "VmNetworkWriteWeightMedium", true) == 0)
																																																																	{
																																																																		dwmPool.VmNetworkWriteWeight.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkWriteWeight.Medium, setConfigurationResponse);
																																																																	}
																																																																	else
																																																																	{
																																																																		if (Localization.Compare(current.Key, "VmNetworkWriteWeightLow", true) == 0)
																																																																		{
																																																																			dwmPool.VmNetworkWriteWeight.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmNetworkWriteWeight.Low, setConfigurationResponse);
																																																																		}
																																																																		else
																																																																		{
																																																																			if (Localization.Compare(current.Key, "VmDiskReadThresholdHigh", true) == 0)
																																																																			{
																																																																				dwmPool.VmDiskReadThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskReadThreshold.High, setConfigurationResponse);
																																																																			}
																																																																			else
																																																																			{
																																																																				if (Localization.Compare(current.Key, "VmDiskReadThresholdMedium", true) == 0)
																																																																				{
																																																																					dwmPool.VmDiskReadThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskReadThreshold.Medium, setConfigurationResponse);
																																																																				}
																																																																				else
																																																																				{
																																																																					if (Localization.Compare(current.Key, "VmDiskReadThresholdLow", true) == 0)
																																																																					{
																																																																						dwmPool.VmDiskReadThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskReadThreshold.Low, setConfigurationResponse);
																																																																					}
																																																																					else
																																																																					{
																																																																						if (Localization.Compare(current.Key, "VmDiskReadWeightHigh", true) == 0)
																																																																						{
																																																																							dwmPool.VmDiskReadWeight.High = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskReadWeight.High, setConfigurationResponse);
																																																																						}
																																																																						else
																																																																						{
																																																																							if (Localization.Compare(current.Key, "VmDiskReadWeightMedium", true) == 0)
																																																																							{
																																																																								dwmPool.VmDiskReadWeight.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskReadWeight.Medium, setConfigurationResponse);
																																																																							}
																																																																							else
																																																																							{
																																																																								if (Localization.Compare(current.Key, "VmDiskReadWeightLow", true) == 0)
																																																																								{
																																																																									dwmPool.VmDiskReadWeight.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskReadWeight.Low, setConfigurationResponse);
																																																																								}
																																																																								else
																																																																								{
																																																																									if (Localization.Compare(current.Key, "VmDiskWriteThresholdHigh", true) == 0)
																																																																									{
																																																																										dwmPool.VmDiskWriteThreshold.High = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskWriteThreshold.High, setConfigurationResponse);
																																																																									}
																																																																									else
																																																																									{
																																																																										if (Localization.Compare(current.Key, "VmDiskWriteThresholdMedium", true) == 0)
																																																																										{
																																																																											dwmPool.VmDiskWriteThreshold.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskWriteThreshold.Medium, setConfigurationResponse);
																																																																										}
																																																																										else
																																																																										{
																																																																											if (Localization.Compare(current.Key, "VmDiskWriteThresholdLow", true) == 0)
																																																																											{
																																																																												dwmPool.VmDiskWriteThreshold.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskWriteThreshold.Low, setConfigurationResponse);
																																																																											}
																																																																											else
																																																																											{
																																																																												if (Localization.Compare(current.Key, "VmDiskWriteWeightHigh", true) == 0)
																																																																												{
																																																																													dwmPool.VmDiskWriteWeight.High = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskWriteWeight.High, setConfigurationResponse);
																																																																												}
																																																																												else
																																																																												{
																																																																													if (Localization.Compare(current.Key, "VmDiskWriteWeightMedium", true) == 0)
																																																																													{
																																																																														dwmPool.VmDiskWriteWeight.Medium = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskWriteWeight.Medium, setConfigurationResponse);
																																																																													}
																																																																													else
																																																																													{
																																																																														if (Localization.Compare(current.Key, "VmDiskWriteWeightLow", true) == 0)
																																																																														{
																																																																															dwmPool.VmDiskWriteWeight.Low = ARDServer.ParseDouble(current.Value, dwmPool.VmDiskWriteWeight.Low, setConfigurationResponse);
																																																																														}
																																																																														else
																																																																														{
																																																																															if (Localization.Compare(current.Key, "CredentialsValid", true) != 0 && Localization.Compare(current.Key, "WlbVersion", true) != 0)
																																																																															{
																																																																																if (Localization.Compare(current.Key, "PoolAuditLogGranularity", true) == 0)
																																																																																{
																																																																																	dwmPool.PoolAuditLogGranularity = current.Value;
																																																																																}
																																																																																else
																																																																																{
																																																																																	if (dictionary == null)
																																																																																	{
																																																																																		dictionary = new Dictionary<string, string>();
																																																																																	}
																																																																																	try
																																																																																	{
																																																																																		dictionary.Add(current.Key, current.Value);
																																																																																	}
																																																																																	catch (Exception ex)
																																																																																	{
																																																																																		Logger.Trace("Exception adding {0} to other config dictionary", new object[]
																																																																																		{
																																																																																			current.Key
																																																																																		});
																																																																																		setConfigurationResponse.ResultCode = 4010;
																																																																																		setConfigurationResponse.ErrorMessage = ex.Message;
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
								catch (DwmException)
								{
									string text = string.Format("The value of {0} is not valid ({1})", current.Key, current.Value);
									Logger.Trace("WorkloadBalance.SetXenPoolConfig: {0}", new object[]
									{
										text
									});
									setConfigurationResponse.ResultCode = 4007;
									setConfigurationResponse.ErrorMessage = text;
								}
							}
							if (setConfigurationResponse.ResultCode == 0)
							{
								dwmPool.Save();
								dwmPool.SaveThresholds();
								dwmPool.SetOtherConfig(dictionary);
							}
						}
					}
					else
					{
						setConfigurationResponse.ResultCode = 4004;
					}
				}
				else
				{
					setConfigurationResponse.ResultCode = 5;
				}
			}
			catch (Exception ex2)
			{
				Logger.LogException(ex2);
				setConfigurationResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex2);
				setConfigurationResponse.ErrorMessage = ex2.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.SetXenPoolConfiguration:  ResultCode={0}", new object[]
			{
				setConfigurationResponse.ResultCode
			});
			return setConfigurationResponse;
		}
		public GetConfigurationResponse GetXenPoolConfiguration(GetConfigurationRequest request)
		{
			GetConfigurationResponse getConfigurationResponse = new GetConfigurationResponse();
			getConfigurationResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.GetXenPoolConfiguration called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						DwmPool dwmPool = null;
						CommunicationBase.Trace("IWorkloadBalance.GetXenPoolConfiguration:  PoolUuid={0}", new object[]
						{
							request.PoolUuid
						});
						CommunicationBase.Trace("IWorkloadBalance.GetXenPoolConfiguration:  PoolId  ={0}", new object[]
						{
							request.PoolId
						});
						if (request.PoolId > 0)
						{
							dwmPool = new DwmPool(request.PoolId);
						}
						else
						{
							if (!string.IsNullOrEmpty(request.PoolUuid) || !string.IsNullOrEmpty(request.PoolName))
							{
								dwmPool = new DwmPool(request.PoolUuid, request.PoolName, DwmHypervisorType.XenServer);
								if (dwmPool.Id == 0 && Localization.Compare(request.PoolUuid, "default", true) != 0)
								{
									getConfigurationResponse.ResultCode = 4015;
									dwmPool = null;
								}
							}
							else
							{
								getConfigurationResponse.ResultCode = 4007;
							}
						}
						if (dwmPool != null)
						{
							dwmPool.Load();
							getConfigurationResponse.OptimizationParms = new Dictionary<string, string>();
							getConfigurationResponse.OptimizationParms.Add("HostCpuThresholdCritical", dwmPool.HostCpuThreshold.Critical.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostCpuThresholdHigh", dwmPool.HostCpuThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostCpuThresholdMedium", dwmPool.HostCpuThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostCpuThresholdLow", dwmPool.HostCpuThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostMemoryThresholdCritical", dwmPool.HostMemoryThreshold.Critical.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostMemoryThresholdHigh", dwmPool.HostMemoryThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostMemoryThresholdMedium", dwmPool.HostMemoryThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostMemoryThresholdLow", dwmPool.HostMemoryThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPifReadThresholdCritical", dwmPool.HostPifReadThreshold.Critical.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPifReadThresholdHigh", dwmPool.HostPifReadThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPifReadThresholdMedium", dwmPool.HostPifReadThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPifReadThresholdLow", dwmPool.HostPifReadThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPifWriteThresholdCritical", dwmPool.HostPifWriteThreshold.Critical.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPifWriteThresholdHigh", dwmPool.HostPifWriteThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPifWriteThresholdMedium", dwmPool.HostPifWriteThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPifWriteThresholdLow", dwmPool.HostPifWriteThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPbdReadThresholdCritical", dwmPool.HostPbdReadThreshold.Critical.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPbdReadThresholdHigh", dwmPool.HostPbdReadThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPbdReadThresholdMedium", dwmPool.HostPbdReadThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPbdReadThresholdLow", dwmPool.HostPbdReadThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPbdWriteThresholdCritical", dwmPool.HostPbdWriteThreshold.Critical.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPbdWriteThresholdHigh", dwmPool.HostPbdWriteThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPbdWriteThresholdMedium", dwmPool.HostPbdWriteThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostPbdWriteThresholdLow", dwmPool.HostPbdWriteThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostLoadAverageThresholdCritical", dwmPool.HostLoadAverageThreshold.Critical.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostLoadAverageThresholdHigh", dwmPool.HostLoadAverageThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostLoadAverageThresholdMedium", dwmPool.HostLoadAverageThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("HostLoadAverageThresholdLow", dwmPool.HostLoadAverageThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("WeightCurrentMetrics", dwmPool.WeightCurrentMetrics.ToString());
							getConfigurationResponse.OptimizationParms.Add("WeightRecentMetrics", dwmPool.WeightRecentMetrics.ToString());
							getConfigurationResponse.OptimizationParms.Add("WeightHistoricalMetrics", dwmPool.WeightHistoricMetrics.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmCpuUtilizationThresholdHigh", dwmPool.VmCpuUtilizationThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmCpuUtilizationThresholdMedium", dwmPool.VmCpuUtilizationThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmCpuUtilizationThresholdLow", dwmPool.VmCpuUtilizationThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmCpuUtilizationWeightHigh", dwmPool.VmCpuUtilizationWeight.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmCpuUtilizationWeightMedium", dwmPool.VmCpuUtilizationWeight.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmCpuUtilizationWeightLow", dwmPool.VmCpuUtilizationWeight.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmMemoryThresholdHigh", dwmPool.VmMemoryThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmMemoryThresholdMedium", dwmPool.VmMemoryThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmMemoryThresholdLow", dwmPool.VmMemoryThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmMemoryWeightHigh", dwmPool.VmMemoryWeight.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmMemoryWeightMedium", dwmPool.VmMemoryWeight.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmMemoryWeightLow", dwmPool.VmMemoryWeight.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkReadThresholdHigh", dwmPool.VmNetworkReadThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkReadThresholdMedium", dwmPool.VmNetworkReadThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkReadThresholdLow", dwmPool.VmNetworkReadThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkReadWeightHigh", dwmPool.VmNetworkReadWeight.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkReadWeightMedium", dwmPool.VmNetworkReadWeight.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkReadWeightLow", dwmPool.VmNetworkReadWeight.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkWriteThresholdHigh", dwmPool.VmNetworkWriteThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkWriteThresholdMedium", dwmPool.VmNetworkWriteThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkWriteThresholdLow", dwmPool.VmNetworkWriteThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkWriteWeightHigh", dwmPool.VmNetworkWriteWeight.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkWriteWeightMedium", dwmPool.VmNetworkWriteWeight.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmNetworkWriteWeightLow", dwmPool.VmNetworkWriteWeight.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskReadThresholdHigh", dwmPool.VmDiskReadThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskReadThresholdMedium", dwmPool.VmDiskReadThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskReadThresholdLow", dwmPool.VmDiskReadThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskReadWeightHigh", dwmPool.VmDiskReadWeight.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskReadWeightMedium", dwmPool.VmDiskReadWeight.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskReadWeightLow", dwmPool.VmDiskReadWeight.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskWriteThresholdHigh", dwmPool.VmDiskWriteThreshold.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskWriteThresholdMedium", dwmPool.VmDiskWriteThreshold.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskWriteThresholdLow", dwmPool.VmDiskWriteThreshold.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskWriteWeightHigh", dwmPool.VmDiskWriteWeight.High.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskWriteWeightMedium", dwmPool.VmDiskWriteWeight.Medium.ToString());
							getConfigurationResponse.OptimizationParms.Add("VmDiskWriteWeightLow", dwmPool.VmDiskWriteWeight.Low.ToString());
							getConfigurationResponse.OptimizationParms.Add("WlbVersion", Configuration.GetValueAsString(ConfigItems.WLBVersion));
							Dictionary<string, string> otherConfig = dwmPool.GetOtherConfig();
							if (otherConfig != null)
							{
								foreach (KeyValuePair<string, string> current in otherConfig)
								{
									try
									{
										getConfigurationResponse.OptimizationParms.Add(current.Key, current.Value);
									}
									catch (ArgumentException ex)
									{
										Logger.Trace("Error {0} adding other config item {1} to response.OptimizationParms", new object[]
										{
											ex.Message,
											current.Key
										});
									}
								}
							}
							Dictionary<string, string> dictionary = this.InternalGetScheduledTask(request);
							foreach (KeyValuePair<string, string> current2 in dictionary)
							{
								getConfigurationResponse.OptimizationParms.Add(current2.Key, current2.Value);
							}
							Dictionary<string, string> dictionary2 = this.InternalGetHostConfiguration(request);
							foreach (KeyValuePair<string, string> current3 in dictionary2)
							{
								getConfigurationResponse.OptimizationParms.Add(current3.Key, current3.Value);
							}
							foreach (KeyValuePair<string, string> current4 in getConfigurationResponse.OptimizationParms)
							{
								CommunicationBase.Trace("IWorkloadBalance.GetXenPoolConfiguration:  {0}={1}", new object[]
								{
									current4.Key,
									current4.Value
								});
							}
						}
					}
					else
					{
						getConfigurationResponse.ResultCode = 4004;
					}
				}
				else
				{
					getConfigurationResponse.ResultCode = 5;
				}
			}
			catch (Exception ex2)
			{
				Logger.LogException(ex2);
				getConfigurationResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex2);
				getConfigurationResponse.ErrorMessage = ex2.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.GetXenPoolConfiguration response:  ResultCode={0}", new object[]
			{
				getConfigurationResponse.ResultCode
			});
			return getConfigurationResponse;
		}
		public GetHostConfigResponse GetHostConfiguration(GetHostConfigRequest request)
		{
			GetHostConfigResponse getHostConfigResponse = new GetHostConfigResponse();
			getHostConfigResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.GetHostConfiguration called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						if ((request.HostId > 0 || !string.IsNullOrEmpty(request.HostUuid)) && (request.PoolId > 0 || !string.IsNullOrEmpty(request.PoolUuid)))
						{
							CommunicationBase.Trace("IWorkloadBalance.GetHostConfiguration:  HostUuid={0}", new object[]
							{
								request.HostUuid
							});
							CommunicationBase.Trace("IWorkloadBalance.GetHostConfiguration:  PoolUuid={0}", new object[]
							{
								request.PoolUuid
							});
							DwmHost dwmHost;
							if (request.HostId > 0)
							{
								dwmHost = new DwmHost(request.HostId);
							}
							else
							{
								if (request.PoolId > 0)
								{
									dwmHost = new DwmHost(request.HostUuid, string.Empty, request.PoolId);
								}
								else
								{
									dwmHost = new DwmHost(request.HostUuid, string.Empty, request.PoolUuid);
								}
							}
							if (dwmHost != null)
							{
								getHostConfigResponse.Config = dwmHost.GetOtherConfig();
							}
							else
							{
								getHostConfigResponse.ResultCode = 4007;
							}
						}
					}
					else
					{
						getHostConfigResponse.ResultCode = 4004;
					}
				}
				else
				{
					getHostConfigResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				getHostConfigResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				getHostConfigResponse.ErrorMessage = ex.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.GetHostConfiguration response:  ResultCode={0}", new object[]
			{
				getHostConfigResponse.ResultCode
			});
			return getHostConfigResponse;
		}
		public SetHostConfigResponse SetHostConfiguration(SetHostConfigRequest request)
		{
			SetHostConfigResponse setHostConfigResponse = new SetHostConfigResponse();
			setHostConfigResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.SetHostConfiguration called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						if ((request.HostId > 0 || !string.IsNullOrEmpty(request.HostUuid)) && (request.PoolId > 0 || !string.IsNullOrEmpty(request.PoolUuid)) && request.Config != null)
						{
							DwmHost dwmHost = null;
							CommunicationBase.Trace("IWorkloadBalance.SetHostConfiguration:  HostUuid={0}", new object[]
							{
								request.HostUuid
							});
							CommunicationBase.Trace("IWorkloadBalance.SetHostConfiguration:  PoolUuid={0}", new object[]
							{
								request.PoolUuid
							});
							if (request.HostId > 0)
							{
								dwmHost = new DwmHost(request.HostId);
							}
							else
							{
								if (request.PoolId > 0)
								{
									dwmHost = new DwmHost(request.HostUuid, string.Empty, request.PoolId);
								}
								else
								{
									dwmHost = new DwmHost(request.HostUuid, string.Empty, request.PoolUuid);
								}
							}
							if (dwmHost != null)
							{
								if (Configuration.GetValueAsBool(ConfigItems.WlbWebServiceTrace))
								{
									foreach (KeyValuePair<string, string> current in request.Config)
									{
										CommunicationBase.Trace("IWorkloadBalance.SetHostConfiguration:  {0}={1}", new object[]
										{
											current.Key,
											current.Value
										});
									}
								}
								dwmHost.SetOtherConfig(request.Config);
							}
							else
							{
								setHostConfigResponse.ResultCode = 4007;
							}
						}
					}
					else
					{
						setHostConfigResponse.ResultCode = 4004;
					}
				}
				else
				{
					setHostConfigResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				setHostConfigResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				setHostConfigResponse.ErrorMessage = ex.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.SetHostConfiguration response:  ResultCode={0}", new object[]
			{
				setHostConfigResponse.ResultCode
			});
			return setHostConfigResponse;
		}
		public VMRecommendationsResponse VMGetRecommendations(VMRecommendationsRequest request)
		{
			VMRecommendationsResponse vMRecommendationsResponse = new VMRecommendationsResponse();
			vMRecommendationsResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations:  PoolUuid={0}", new object[]
						{
							request.PoolUuid
						});
						CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations:  VmUuid  ={0}", new object[]
						{
							request.VmUuid
						});
						DwmHostCollection dwmHostCollection = null;
						DwmErrorCode dwmErrorCode = DwmErrorCode.None;
						if (request.VmID > 0)
						{
							dwmHostCollection = DwmAEVirtualMachine.GetAllHosts(request.VmID, out dwmErrorCode);
						}
						else
						{
							if (!string.IsNullOrEmpty(request.VmUuid) && !string.IsNullOrEmpty(request.PoolUuid))
							{
								dwmHostCollection = DwmAEVirtualMachine.GetAllHosts(request.VmUuid, request.PoolUuid, out dwmErrorCode);
							}
							else
							{
								if (!string.IsNullOrEmpty(request.VmName) && !string.IsNullOrEmpty(request.PoolName))
								{
									dwmHostCollection = DwmAEVirtualMachine.GetAllHostsByName(request.VmName, request.PoolName, out dwmErrorCode);
								}
								else
								{
									dwmErrorCode = DwmErrorCode.InvalidParameter;
								}
							}
						}
						if (dwmErrorCode != DwmErrorCode.None)
						{
							vMRecommendationsResponse.ResultCode = (int)dwmErrorCode;
						}
						if (dwmHostCollection != null)
						{
							vMRecommendationsResponse.Recommendations = new List<VmPlacementRecommendation>(dwmHostCollection.Count);
							for (int i = 0; i < dwmHostCollection.Count; i++)
							{
								if (dwmHostCollection[i].PowerState == PowerStatus.On)
								{
									VmPlacementRecommendation vmPlacementRecommendation = new VmPlacementRecommendation();
									vmPlacementRecommendation.HostId = dwmHostCollection[i].Id;
									vmPlacementRecommendation.HostName = dwmHostCollection[i].Name;
									vmPlacementRecommendation.HostUuid = dwmHostCollection[i].Uuid;
									vmPlacementRecommendation.Score = dwmHostCollection[i].Metrics.Score;
									vmPlacementRecommendation.Stars = dwmHostCollection[i].Metrics.Stars;
									vmPlacementRecommendation.CanBootVM = dwmHostCollection[i].Metrics.CanBootVM;
									vmPlacementRecommendation.RecommendationId = dwmHostCollection[i].Metrics.RecommendationId;
									vmPlacementRecommendation.ZeroScoreReason = (VMZeroScoreReason)((dwmHostCollection[i].Metrics.ZeroScoreReasons.Count <= 0) ? ZeroScoreReason.None : dwmHostCollection[i].Metrics.ZeroScoreReasons[0]);
									vMRecommendationsResponse.Recommendations.Add(vmPlacementRecommendation);
									CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations response:  RecIc={0}", new object[]
									{
										vmPlacementRecommendation.RecommendationId
									});
									CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations response:  HostName={0}", new object[]
									{
										vmPlacementRecommendation.HostName
									});
									CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations response:  Score={0}", new object[]
									{
										vmPlacementRecommendation.Score
									});
									CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations response:  Stars={0}", new object[]
									{
										vmPlacementRecommendation.Stars
									});
									CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations response:  ZeroScoreReason={0}", new object[]
									{
										vmPlacementRecommendation.ZeroScoreReason
									});
								}
							}
						}
					}
					else
					{
						vMRecommendationsResponse.ResultCode = 4004;
					}
				}
				else
				{
					vMRecommendationsResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				vMRecommendationsResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				vMRecommendationsResponse.ErrorMessage = ex.Message;
			}
			if (request != null && vMRecommendationsResponse.ResultCode != 0)
			{
				WorkloadBalanceResult discoveryStatusResult = ARDServer.GetDiscoveryStatusResult(request.PoolId, request.PoolUuid, request.PoolName);
				if (discoveryStatusResult != null)
				{
					vMRecommendationsResponse.ResultCode = discoveryStatusResult.ResultCode;
					vMRecommendationsResponse.ErrorMessage = discoveryStatusResult.ErrorMessage;
				}
			}
			CommunicationBase.Trace("IWorkloadBalance.VMGetRecommendations response:  ResultCode={0}", new object[]
			{
				vMRecommendationsResponse.ResultCode
			});
			return vMRecommendationsResponse;
		}
		public HostRecommendationsResponse HostGetRecommendations(HostRecommendationsRequest request)
		{
			HostRecommendationsResponse hostRecommendationsResponse = new HostRecommendationsResponse();
			hostRecommendationsResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations:  PoolUuid={0}", new object[]
						{
							request.PoolUuid
						});
						CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations:  HostUuid={0}", new object[]
						{
							request.HostUuid
						});
						DwmAEHostRelocateRecommendation dwmAEHostRelocateRecommendation = null;
						if (request.HostId > 0)
						{
							dwmAEHostRelocateRecommendation = DwmAEHost.PlaceVMs(request.HostId);
						}
						else
						{
							if (!string.IsNullOrEmpty(request.HostUuid) && !string.IsNullOrEmpty(request.PoolUuid))
							{
								dwmAEHostRelocateRecommendation = DwmAEHost.PlaceVMs(request.HostUuid, request.PoolUuid);
							}
							else
							{
								if (!string.IsNullOrEmpty(request.HostName) && !string.IsNullOrEmpty(request.PoolName))
								{
									dwmAEHostRelocateRecommendation = DwmAEHost.PlaceVMsByName(request.HostName, request.PoolName);
								}
								else
								{
									hostRecommendationsResponse.ResultCode = 4007;
								}
							}
						}
						if (dwmAEHostRelocateRecommendation != null)
						{
							hostRecommendationsResponse.CanPlaceAllVMs = dwmAEHostRelocateRecommendation.CanPlaceAllVMs;
							if (dwmAEHostRelocateRecommendation.MoveRecs != null && dwmAEHostRelocateRecommendation.MoveRecs.Count > 0)
							{
								hostRecommendationsResponse.Recommendations = new List<HostEvacuationRecommendation>(dwmAEHostRelocateRecommendation.MoveRecs.Count);
								for (int i = 0; i < dwmAEHostRelocateRecommendation.MoveRecs.Count; i++)
								{
									HostEvacuationRecommendation hostEvacuationRecommendation = new HostEvacuationRecommendation();
									hostEvacuationRecommendation.HostId = dwmAEHostRelocateRecommendation.MoveRecs[i].MoveToHostId;
									hostEvacuationRecommendation.HostName = dwmAEHostRelocateRecommendation.MoveRecs[i].MoveToHostName;
									hostEvacuationRecommendation.HostUuid = dwmAEHostRelocateRecommendation.MoveRecs[i].MoveToHostUuid;
									hostEvacuationRecommendation.VmId = dwmAEHostRelocateRecommendation.MoveRecs[i].VmId;
									hostEvacuationRecommendation.VmName = dwmAEHostRelocateRecommendation.MoveRecs[i].VmName;
									hostEvacuationRecommendation.VmUuid = dwmAEHostRelocateRecommendation.MoveRecs[i].VmUuid;
									hostEvacuationRecommendation.RecommendationId = dwmAEHostRelocateRecommendation.MoveRecs[i].RecommendationId;
									hostRecommendationsResponse.Recommendations.Add(hostEvacuationRecommendation);
									CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations response:  RecId={0}", new object[]
									{
										hostEvacuationRecommendation.RecommendationId
									});
									CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations response:  VMUuid={0}", new object[]
									{
										hostEvacuationRecommendation.VmUuid
									});
									CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations response:  VMName={0}", new object[]
									{
										hostEvacuationRecommendation.VmName
									});
									CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations response:  MoveToHostUuid={0}", new object[]
									{
										hostEvacuationRecommendation.HostUuid
									});
									CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations response:  MoveToHostName={0}", new object[]
									{
										hostEvacuationRecommendation.HostName
									});
								}
							}
							else
							{
								if (dwmAEHostRelocateRecommendation.ResultCode != DwmErrorCode.None)
								{
									hostRecommendationsResponse.ResultCode = (int)dwmAEHostRelocateRecommendation.ResultCode;
								}
							}
						}
					}
					else
					{
						hostRecommendationsResponse.ResultCode = 4004;
					}
				}
				else
				{
					hostRecommendationsResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				hostRecommendationsResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				hostRecommendationsResponse.ErrorMessage = ex.Message;
			}
			if (request != null && hostRecommendationsResponse.ResultCode != 0)
			{
				WorkloadBalanceResult discoveryStatusResult = ARDServer.GetDiscoveryStatusResult(request.PoolId, request.PoolUuid, request.PoolName);
				if (discoveryStatusResult != null)
				{
					hostRecommendationsResponse.ResultCode = discoveryStatusResult.ResultCode;
					hostRecommendationsResponse.ErrorMessage = discoveryStatusResult.ErrorMessage;
				}
			}
			CommunicationBase.Trace("IWorkloadBalance.HostGetRecommendations response:  ResultCode={0}", new object[]
			{
				hostRecommendationsResponse.ResultCode
			});
			return hostRecommendationsResponse;
		}
		public PoolOptimizationResponse GetOptimizationRecommendations(PoolOptimizationRequest request)
		{
			PoolOptimizationResponse poolOptimizationResponse = new PoolOptimizationResponse();
			poolOptimizationResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations:  PoolUuid={0}", new object[]
						{
							request.PoolUuid
						});
						List<MoveRecommendation> list = null;
						if (request.PoolID > 0)
						{
							list = DwmPool.GetOptimizations(request.PoolID);
						}
						else
						{
							if (!string.IsNullOrEmpty(request.PoolUuid))
							{
								try
								{
									list = DwmPool.GetOptimizations(request.PoolUuid);
								}
								catch (DwmException ex)
								{
									poolOptimizationResponse.ResultCode = ex.Number;
									poolOptimizationResponse.ErrorMessage = ex.Message;
								}
							}
							else
							{
								poolOptimizationResponse.ResultCode = 4007;
							}
						}
						if (list != null && list.Count > 0)
						{
							poolOptimizationResponse.Recommendations = new List<PoolOptimizationRecommendation>(list.Count);
							for (int i = 0; i < list.Count; i++)
							{
								PoolOptimizationRecommendation poolOptimizationRecommendation = new PoolOptimizationRecommendation();
								poolOptimizationRecommendation.MoveFromHostId = list[i].MoveFromHostId;
								poolOptimizationRecommendation.MoveFromHostName = list[i].MoveFromHostName;
								poolOptimizationRecommendation.MoveFromHostUuid = list[i].MoveFromHostUuid;
								poolOptimizationRecommendation.MoveToHostId = list[i].MoveToHostId;
								poolOptimizationRecommendation.MoveToHostName = list[i].MoveToHostName;
								poolOptimizationRecommendation.MoveToHostUuid = list[i].MoveToHostUuid;
								poolOptimizationRecommendation.VmToMoveId = list[i].VmId;
								poolOptimizationRecommendation.VmToMoveName = list[i].VmName;
								poolOptimizationRecommendation.VmToMoveUuid = list[i].VmUuid;
								poolOptimizationRecommendation.RecommendationId = list[i].RecommendationId;
								poolOptimizationRecommendation.Reason = (PoolOptimizationReason)list[i].Reason;
								poolOptimizationResponse.Recommendations.Add(poolOptimizationRecommendation);
								CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations response:  RecId={0}", new object[]
								{
									poolOptimizationRecommendation.RecommendationId
								});
								CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations response:  VMUuid={0}", new object[]
								{
									poolOptimizationRecommendation.VmToMoveUuid
								});
								CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations response:  MoveFromHostUuid={0}", new object[]
								{
									poolOptimizationRecommendation.MoveFromHostUuid
								});
								CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations response:  MoveToHostUuid={0}", new object[]
								{
									poolOptimizationRecommendation.MoveToHostUuid
								});
								CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations response:  Reason={0}", new object[]
								{
									poolOptimizationRecommendation.Reason
								});
							}
							poolOptimizationResponse.OptimizationId = list[0].RecommendationSetId;
							poolOptimizationResponse.Severity = (PoolOptimizationSeverity)list[0].Severity;
							CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations response:  OptimizationId={0}", new object[]
							{
								poolOptimizationResponse.OptimizationId
							});
							CommunicationBase.Trace("IWorkloadBalance.GetOptimizationRecommendations response:  Severity={0}", new object[]
							{
								poolOptimizationResponse.Severity
							});
						}
					}
					else
					{
						poolOptimizationResponse.ResultCode = 4004;
					}
				}
				else
				{
					poolOptimizationResponse.ResultCode = 5;
				}
			}
			catch (Exception ex2)
			{
				Logger.LogException(ex2);
				poolOptimizationResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex2);
				poolOptimizationResponse.ErrorMessage = ex2.Message;
			}
			if (request != null && poolOptimizationResponse.ResultCode != 0)
			{
				WorkloadBalanceResult discoveryStatusResult = ARDServer.GetDiscoveryStatusResult(request.PoolID, request.PoolUuid, null);
				if (discoveryStatusResult != null)
				{
					poolOptimizationResponse.ResultCode = discoveryStatusResult.ResultCode;
					poolOptimizationResponse.ErrorMessage = discoveryStatusResult.ErrorMessage;
				}
			}
			return poolOptimizationResponse;
		}
		public ReportResponse ExecuteReport(ReportRequest request)
		{
			ReportResponse reportResponse = new ReportResponse();
			reportResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.ExecuteReport called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						string text = string.Empty;
						string text2 = string.Empty;
						string text3 = "tampa";
						string poolUuid = string.Empty;
						for (int i = 0; i < request.ReportParms.Count; i++)
						{
							ReportParameter reportParameter = request.ReportParms[i];
							CommunicationBase.Trace("IWorkloadBalance.ExecuteReport request.ReportParms[{0}] ParameterName=\"{1}\" ParameterValue=\"{2}\"", new object[]
							{
								i,
								reportParameter.ParameterName,
								reportParameter.ParameterValue
							});
							string text4 = reportParameter.ParameterName.ToLower();
							if (text4 != null)
							{
                                Dictionary<string, int> dic = null;
								if (dic == null)
								{
									dic = new Dictionary<string, int>(2)
									{

										{
											"reportversion",
											0
										},

										{
											"poolid",
											1
										}
									};
								}
								int num;
								if (dic.TryGetValue(text4, out num))
								{
									if (num != 0)
									{
										if (num == 1)
										{
											poolUuid = reportParameter.ParameterValue.ToString();
										}
									}
									else
									{
										text3 = reportParameter.ParameterValue.ToString();
									}
								}
							}
						}
						if (!string.IsNullOrEmpty(request.ReportName))
						{
							if (Localization.Compare(request.ReportName, ARDServer.GetReportDefinitionsKey, true) == 0)
							{
								string localCode = request.ReportParms[0].ParameterValue.ToString();
								CommunicationBase.Trace("IWorkloadBalance.ExecuteReport: Retrieving report definitions.", new object[0]);
								reportResponse.XmlDataSet = string.Format("<![CDATA[{0}]]>", this.BuildReportDefinitionXML(localCode, text3, poolUuid));
							}
							else
							{
								CommunicationBase.Trace("IWorkloadBalance.ExecuteReport:  ReportName={0}", new object[]
								{
									request.ReportName
								});
								StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
								storedProcParamCollection.Add(new StoredProcParam("@report_file", request.ReportName));
								storedProcParamCollection.Add(new StoredProcParam("@report_version", text3));
								using (DBAccess dBAccess = new DBAccess())
								{
									text = dBAccess.ExecuteScalarString("report_get_sql_by_filename", storedProcParamCollection);
								}
							}
						}
						else
						{
							reportResponse.ResultCode = 4007;
						}
						if (!string.IsNullOrEmpty(text))
						{
							StoredProcParamCollection storedProcParamCollection2 = null;
							if (request.ReportParms != null && request.ReportParms.Count > 0)
							{
								storedProcParamCollection2 = new StoredProcParamCollection();
								for (int j = 0; j < request.ReportParms.Count; j++)
								{
									ReportParameter reportParameter2 = request.ReportParms[j];
									CommunicationBase.Trace("IWorkloadBalance.ExecuteReport:  Parm({0}) {1}={2}", new object[]
									{
										j,
										reportParameter2.ParameterName,
										reportParameter2.ParameterValue
									});
									if (reportParameter2.ParameterName == "LocaleCode")
									{
										text2 = reportParameter2.ParameterValue;
									}
									else
									{
										string name = string.Format("{0}{1}", reportParameter2.ParameterName.StartsWith("@") ? string.Empty : "@", reportParameter2.ParameterName);
										storedProcParamCollection2.Add(new StoredProcParam(name, reportParameter2.ParameterValue));
									}
								}
							}
							using (DBAccess dBAccess2 = new DBAccess())
							{
								DataSet dataSet = new DataSet();
								DataTable dataTable = new DataTable();
								try
								{
									dBAccess2.Timeout = Configuration.GetValueAsInt(ConfigItems.DBReportsTimeout, 600);
									dataSet = dBAccess2.ExecuteDataSet(text, storedProcParamCollection2);
									try
									{
										dataTable = ARDServer.GetReportLabels(request.ReportName.ToString(), text2.ToString());
										if (dataTable != null)
										{
											dataSet.Tables.Add(dataTable);
										}
									}
									catch (Exception ex)
									{
										Logger.Trace("Caught exception '{0}' executing SQL {1}", new object[]
										{
											ex.Message,
											"rp_get_report_labels"
										});
									}
									if (dataSet != null)
									{
										MemoryStream memoryStream = new MemoryStream();
										dataSet.WriteXml(memoryStream, XmlWriteMode.WriteSchema);
										memoryStream.Position = 0L;
										StreamReader streamReader = new StreamReader(memoryStream);
										char[] array = new char[memoryStream.Length];
										streamReader.Read(array, 0, (int)memoryStream.Length);
										reportResponse.XmlDataSet = "<![CDATA[" + new string(array).Replace("\0", string.Empty) + "]]>";
									}
									else
									{
										reportResponse.XmlDataSet = null;
									}
								}
								catch (Exception ex2)
								{
									Logger.Trace("Caught exception '{0}' executing SQL {1}", new object[]
									{
										ex2.Message,
										text
									});
									reportResponse.ResultCode = 4010;
									reportResponse.ErrorMessage = ex2.Message;
									dataTable.Dispose();
									dataSet.Dispose();
									reportResponse.XmlDataSet = null;
								}
							}
						}
						else
						{
							reportResponse.ResultCode = 4007;
						}
					}
					else
					{
						reportResponse.ResultCode = 4004;
					}
				}
				else
				{
					reportResponse.ResultCode = 5;
				}
			}
			catch (Exception ex3)
			{
				Logger.LogException(ex3);
				reportResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex3);
				reportResponse.ErrorMessage = ex3.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.ExecuteReport return {0}", new object[]
			{
				reportResponse.ResultCode
			});
			return reportResponse;
		}
		private string BuildReportDefinitionXML(string localCode, string version, string poolUuid)
		{
			CommunicationBase.Trace("IWorkloadBalance.BuildReportDefinitionXML for localCode='{0}' version='{1}' poolUuid='{2}'", new object[]
			{
				localCode,
				version,
				poolUuid
			});
			StringBuilder stringBuilder = new StringBuilder();
			WlbReports wlbReports = new WlbReports();
			wlbReports.Load(version);
			stringBuilder.Append("<Reports>");
			foreach (string current in wlbReports.Keys)
			{
				WlbReport wlbReport = wlbReports[current];
				string text = wlbReport.PhysicalPath;
				int num = wlbReport.PhysicalPath.LastIndexOf("/") + 1;
				if (num <= 0)
				{
					num = wlbReport.PhysicalPath.LastIndexOf("\\") + 1;
				}
				if (num >= 0)
				{
					text = text.Substring(num);
				}
				if (!wlbReport.LocalizedNames.ContainsKey(localCode))
				{
					localCode = "en";
				}
				string arg = wlbReport.LocalizedNames[localCode].ToString();
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder.AppendFormat("<Report NameLabel=\"WLBREPORT_{0}\" File=\"{1}\" Name=\"{2}\">", wlbReport.ReportFile.ToUpper(), text, arg);
				stringBuilder.Append("<QueryParameters>");
				IEnumerator enumerator2 = wlbReport.QueryParameters.Keys.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						string text2 = (string)enumerator2.Current;
						string text3 = (string)wlbReport.QueryParameters[text2];
						stringBuilder.AppendFormat("<QueryParameter Name=\"{0}\"/>", text2);
						if (text3 != null && text3.Length > 0)
						{
							List<string> list = new List<string>();
							using (DBAccess dBAccess = new DBAccess())
							{
								DataSet dataSet = dBAccess.ExecuteDataSet(text3, new StoredProcParamCollection
								{
									new StoredProcParam("_pool_uuid", poolUuid)
								});
								IEnumerator enumerator3 = dataSet.Tables[0].Rows.GetEnumerator();
								try
								{
									while (enumerator3.MoveNext())
									{
										DataRow dataRow = (DataRow)enumerator3.Current;
										list.Add(dataRow["name"].ToString());
									}
								}
								finally
								{
									IDisposable disposable;
									if ((disposable = (enumerator3 as IDisposable)) != null)
									{
										disposable.Dispose();
									}
								}
							}
							stringBuilder2.AppendFormat("<{0}>{1}</{0}>", text2, string.Join(",", list.ToArray()));
						}
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
				stringBuilder.Append("</QueryParameters>");
				stringBuilder.Append("<Code>");
				stringBuilder.Append("Function Custom() As String\r\n");
				stringBuilder.AppendFormat("  Return \"<root><Version value=\"{0}\"><QueryParameters>{1}</QueryParameters></Version></root>\"", version, stringBuilder2.ToString());
				stringBuilder.Append("\r\nEnd Function");
				stringBuilder.Append("</Code>");
				stringBuilder.Append("<Rdlc>");
				stringBuilder.Append(SecurityElement.Escape(wlbReport.ReportRdlc));
				stringBuilder.Append("</Rdlc>");
				stringBuilder.Append("</Report>");
			}
			stringBuilder.Append("</Reports>\n");
			return stringBuilder.ToString();
		}
		public DiagnosticResponse GetDiagnostics(DiagnosticRequest request)
		{
			DiagnosticResponse diagnosticResponse = new DiagnosticResponse();
			CommunicationBase.Trace("IWorkloadBalance.GetDiagnostics called", new object[0]);
			try
			{
				diagnosticResponse.ResultCode = 0;
				diagnosticResponse.DiagnosticData = ARDServer.InternalGetDiagnostics();
				CommunicationBase.Trace("IWorkloadBalance.GetDiagnostics returning {0} bytes", new object[]
				{
					diagnosticResponse.DiagnosticData.Length
				});
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				diagnosticResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				diagnosticResponse.ErrorMessage = ex.Message;
			}
			return diagnosticResponse;
		}
		private static string GetLastBytesFromFile(string filename, int bytes)
		{
			FileStream fileStream = null;
			string @string;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open);
				if (fileStream.Length < (long)bytes)
				{
					bytes = (int)fileStream.Length;
				}
				byte[] array = new byte[bytes];
				fileStream.Seek((long)(-(long)bytes), SeekOrigin.End);
				fileStream.Read(array, 0, array.Length);
				@string = Encoding.ASCII.GetString(array);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return @string;
		}
		public static string InternalGetDiagnostics()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("WLB diagnostic data:  {0} ({1} UTC)", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss"));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Database version:", new object[0]);
			stringBuilder.AppendLine();
			string valueAsString = Configuration.GetValueAsString(ConfigItems.DBSchemaVersion);
			stringBuilder.AppendFormat("   version = {0}", valueAsString);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			stringBuilder.AppendFormat("Files in {0}", directoryInfo.FullName);
			stringBuilder.AppendLine();
			for (int i = 0; i < fileSystemInfos.Length; i++)
			{
				if ((fileSystemInfos[i].Attributes & FileAttributes.Directory) == (FileAttributes)0 && (Localization.Compare(fileSystemInfos[i].Extension, ".exe", true) == 0 || Localization.Compare(fileSystemInfos[i].Extension, ".dll", true) == 0))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fileSystemInfos[i].FullName);
					stringBuilder.AppendFormat("   {0}:  version={1}", fileSystemInfos[i].Name, versionInfo.FileVersion);
					stringBuilder.AppendLine();
				}
			}
			stringBuilder.AppendLine();
			string text = Path.Combine(Logger.GetLogDirectory(), Logger.LogFileName);
			stringBuilder.AppendFormat("Contents of log file '{0}':", text);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			try
			{
				FileInfo fileInfo = new FileInfo(text);
				int num = Configuration.GetValueAsInt(ConfigItems.DiagnosticsSizeKB) * 1000;
				if (fileInfo.Length < (long)num)
				{
					if (!string.IsNullOrEmpty(Logger.LastBackupLogFileName))
					{
						stringBuilder.AppendLine(ARDServer.GetLastBytesFromFile(Logger.LastBackupLogFileName, num - (int)fileInfo.Length));
					}
					stringBuilder.AppendLine(ARDServer.GetLastBytesFromFile(text, (int)fileInfo.Length));
				}
				else
				{
					stringBuilder.AppendLine(ARDServer.GetLastBytesFromFile(text, num));
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendFormat("Error reading log file: {0}", ex.Message);
				stringBuilder.AppendLine();
				Logger.LogException(ex);
			}
			return stringBuilder.ToString();
		}
		public GetSchedTaskResponse GetScheduledTask(GetSchedTaskRequest request)
		{
			GetSchedTaskResponse getSchedTaskResponse = new GetSchedTaskResponse();
			getSchedTaskResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.GetScheduledTask called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						CommunicationBase.Trace("IWorkloadBalance.GetScheduledTask: TaskId={0}", new object[]
						{
							request.TaskId
						});
						WlbTaskCollection wlbTaskCollection = new WlbTaskCollection();
						int actionType = 0;
						int.TryParse(request.TaskActionType, out actionType);
						if (!string.IsNullOrEmpty(request.TaskId))
						{
							WlbTask item = WlbTask.Load(int.Parse(request.TaskId));
							wlbTaskCollection.Add(item);
						}
						else
						{
							if (!string.IsNullOrEmpty(request.PoolUuid))
							{
								wlbTaskCollection.Load(request.PoolUuid, actionType);
							}
							else
							{
								if (request.PoolId > 0)
								{
									wlbTaskCollection.Load(request.PoolId, actionType);
								}
							}
						}
						getSchedTaskResponse.Tasks = new List<ScheduledTask>();
						foreach (WlbTask current in wlbTaskCollection)
						{
							ScheduledTask scheduledTask = new ScheduledTask();
							scheduledTask.TaskId = current.Id;
							scheduledTask.TaskName = current.Name;
							scheduledTask.TaskDescription = current.Description;
							scheduledTask.TaskEnabled = current.Enabled;
							scheduledTask.LastRunResult = current.LastRunResult;
							scheduledTask.LastTouched = current.LastTouched;
							scheduledTask.LastTouchedBy = current.LastTouchedBy;
							scheduledTask.Trigger.DaysOfWeek = (ScheduledTriggerDaysOfWeek)current.Trigger.DaysOfWeek;
							scheduledTask.Trigger.DisableDate = current.Trigger.DisableDate;
							scheduledTask.Trigger.EnableDate = current.Trigger.EnableDate;
							scheduledTask.Trigger.ExecuteTimeOfDay = current.Trigger.ExecuteTime;
							scheduledTask.Trigger.TriggerType = (ScheduledTriggerType)current.Trigger.Type;
							scheduledTask.Trigger.Id = current.Trigger.TriggerId;
							scheduledTask.Trigger.Action.Id = current.Trigger.Action.Id;
							scheduledTask.Trigger.Action.Type = current.Trigger.Action.Type;
							scheduledTask.Trigger.Action.Name = current.Trigger.Action.Name;
							foreach (string current2 in current.Trigger.Action.Parameters.Keys)
							{
								scheduledTask.Trigger.Action.Parameters.Add(current2, current.Trigger.Action.Parameters[current2]);
							}
							getSchedTaskResponse.Tasks.Add(scheduledTask);
						}
					}
					else
					{
						getSchedTaskResponse.ResultCode = 4004;
					}
				}
				else
				{
					getSchedTaskResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				getSchedTaskResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				getSchedTaskResponse.ErrorMessage = ex.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.GetScheduledTask return {0}", new object[]
			{
				getSchedTaskResponse.ResultCode
			});
			return getSchedTaskResponse;
		}
		public SetSchedTaskResponse SetScheduledTask(SetSchedTaskRequest request)
		{
			SetSchedTaskResponse setSchedTaskResponse = new SetSchedTaskResponse();
			setSchedTaskResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.SetScheduledTask called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						foreach (ScheduledTask current in request.Tasks)
						{
							WlbTask wlbTask;
							if (current.TaskId <= 0)
							{
								wlbTask = new WlbTask();
							}
							else
							{
								wlbTask = WlbTask.Load(current.TaskId);
							}
							DwmPool dwmPool = null;
							if (request.PoolId > 0)
							{
								dwmPool = new DwmPool(request.PoolId);
							}
							else
							{
								if (!string.IsNullOrEmpty(request.PoolUuid) || !string.IsNullOrEmpty(request.PoolName))
								{
									dwmPool = new DwmPool(request.PoolUuid, request.PoolName, DwmHypervisorType.XenServer);
								}
								else
								{
									setSchedTaskResponse.ResultCode = 4007;
								}
							}
							wlbTask.PoolId = dwmPool.Id;
							wlbTask.Name = current.TaskName;
							wlbTask.Description = current.TaskDescription;
							wlbTask.Enabled = current.TaskEnabled;
							wlbTask.Owner = current.TaskOwner;
							wlbTask.LastTouchedBy = current.LastTouchedBy;
							wlbTask.LastTouched = current.LastTouched;
							wlbTask.Trigger.Type = (WlbTriggerType)current.Trigger.TriggerType;
							wlbTask.Trigger.ExecuteTime = current.Trigger.ExecuteTimeOfDay;
							wlbTask.Trigger.EnableDate = current.Trigger.EnableDate;
							wlbTask.Trigger.DisableDate = current.Trigger.DisableDate;
							wlbTask.Trigger.DaysOfWeek = (TriggerDaysOfWeek)current.Trigger.DaysOfWeek;
							wlbTask.Trigger.Action.Type = current.Trigger.Action.Type;
							foreach (string current2 in current.Trigger.Action.Parameters.Keys)
							{
								if (wlbTask.Trigger.Action.Parameters.ContainsKey(current2))
								{
									wlbTask.Trigger.Action.Parameters[current2] = current.Trigger.Action.Parameters[current2];
								}
								else
								{
									wlbTask.Trigger.Action.Parameters.Add(current2, current.Trigger.Action.Parameters[current2]);
								}
							}
							wlbTask.Save();
						}
					}
					else
					{
						setSchedTaskResponse.ResultCode = 4004;
					}
				}
				else
				{
					setSchedTaskResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				setSchedTaskResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				setSchedTaskResponse.ErrorMessage = ex.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.SetScheduledTask return {0}", new object[]
			{
				setSchedTaskResponse.ResultCode
			});
			return setSchedTaskResponse;
		}
		public DeleteSchedTaskResponse DeleteScheduledTask(DeleteSchedTaskRequest request)
		{
			DeleteSchedTaskResponse deleteSchedTaskResponse = new DeleteSchedTaskResponse();
			deleteSchedTaskResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.DeleteScheduledTask called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						deleteSchedTaskResponse.ResultCode = 4006;
					}
					else
					{
						deleteSchedTaskResponse.ResultCode = 4004;
					}
				}
				else
				{
					deleteSchedTaskResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				deleteSchedTaskResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				deleteSchedTaskResponse.ErrorMessage = ex.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.DeleteScheduledTask return {0}", new object[]
			{
				deleteSchedTaskResponse.ResultCode
			});
			return deleteSchedTaskResponse;
		}
		public GetSchedTaskActionTypeResponse GetScheduledTaskActionTypes(GetSchedTaskActionTypeRequest request)
		{
			GetSchedTaskActionTypeResponse getSchedTaskActionTypeResponse = new GetSchedTaskActionTypeResponse();
			getSchedTaskActionTypeResponse.ResultCode = 0;
			CommunicationBase.Trace("IWorkloadBalance.GetActionTypes called", new object[0]);
			try
			{
				if (this.ValidWLBAuthenticationToken())
				{
					if (request != null)
					{
						getSchedTaskActionTypeResponse.ResultCode = 4006;
					}
					else
					{
						getSchedTaskActionTypeResponse.ResultCode = 4004;
					}
				}
				else
				{
					getSchedTaskActionTypeResponse.ResultCode = 5;
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				getSchedTaskActionTypeResponse.ResultCode = ARDServer.GetExceptionErrorCode(ex);
				getSchedTaskActionTypeResponse.ErrorMessage = ex.Message;
			}
			CommunicationBase.Trace("IWorkloadBalance.GetActionTypes return {0}", new object[]
			{
				getSchedTaskActionTypeResponse.ResultCode
			});
			return getSchedTaskActionTypeResponse;
		}
		private static double ParseDouble(string stringValue, double defaultValue, WorkloadBalanceResult result)
		{
			double result2 = defaultValue;
			if (stringValue.Contains(","))
			{
				stringValue = stringValue.Replace(',', '.');
			}
			if (!double.TryParse(stringValue, out result2))
			{
				result.ResultCode = 4007;
			}
			return result2;
		}
		private static WorkloadBalanceResult GetDiscoveryStatusResult(int poolId, string poolUuid, string poolName)
		{
			DiscoveryStatus discoveryStatus = DiscoveryStatus.Complete;
			WorkloadBalanceResult result = null;
			try
			{
				DwmPool dwmPool;
				if (poolId > 0)
				{
					dwmPool = new DwmPool(poolId);
				}
				else
				{
					dwmPool = new DwmPool(poolUuid, poolName, DwmHypervisorType.XenServer);
				}
				DateTime dateTime;
				dwmPool.GetDiscoveryStatus(out discoveryStatus, out dateTime);
				if (discoveryStatus != DiscoveryStatus.Unknown && discoveryStatus != DiscoveryStatus.Complete)
				{
					result = new WorkloadBalanceResult
					{
						ResultCode = 4010,
						ErrorMessage = "Pool discovery has not been completed."
					};
				}
			}
			catch
			{
			}
			return result;
		}
		private static int ParseInt(string stringValue, int defaultValue, WorkloadBalanceResult result)
		{
			int result2 = defaultValue;
			if (!int.TryParse(stringValue, out result2))
			{
				result.ResultCode = 4007;
			}
			return result2;
		}
		private static int GetExceptionErrorCode(Exception e)
		{
			int result;
			if (e is DwmException)
			{
				result = ((DwmException)e).Number;
			}
			else
			{
				result = 4014;
			}
			return result;
		}
		private static DataTable GetReportLabels(string reportName, string localeCode)
		{
			string sqlStatement = string.Format("rp_get_report_labels('{0}','{1}')", reportName, localeCode);
			DataSet dataSet;
			using (DBAccess dBAccess = new DBAccess())
			{
				dataSet = dBAccess.ExecuteDataSet(sqlStatement);
			}
			dataSet.Tables[0].TableName = "LabelTable";
			return dataSet.Tables[0].Copy();
		}
	}
}
