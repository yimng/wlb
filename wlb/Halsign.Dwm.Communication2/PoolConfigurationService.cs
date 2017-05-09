using Halsign.DWM.Domain;
using Halsign.DWM.Framework;
using ServiceStack.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
namespace Halsign.DWM.Communication2
{
	public class PoolConfigurationService : WlbServiceBase<PoolConfiguration>
	{
		protected List<string> _readOnlyConfigs = new List<string>();
		public PoolConfigurationService()
		{
			this._readOnlyConfigs.AddRange(new string[]
			{
				"VersionMajor",
				"VersionMinor",
				"VersionBuild",
				"CredentialsValid",
				"WlbVersion",
				"AutoBalanceEnabled"
			});
		}
		public override object OnGet(PoolConfiguration request)
		{
			base.CheckNullArguments<PoolConfiguration>(request);
			PoolConfigurationResponse poolConfigurationResponse = new PoolConfigurationResponse();
			DwmErrorCode dwmErrorCode = DwmErrorCode.None;
			base.Trace("PoolConfigurationService.OnGet:  PoolUuid={0}", new object[]
			{
				request.Uuid
			});
			try
			{
				DwmPool dwmPool = new DwmPool(request.Uuid, null, DwmHypervisorType.XenServer);
				if (dwmPool.Id == 0)
				{
					dwmErrorCode = DwmErrorCode.UnknownPool;
				}
				else
				{
					dwmPool.Load();
					poolConfigurationResponse.Add("HostCpuThresholdCritical", dwmPool.HostCpuThreshold.Critical.ToString());
					poolConfigurationResponse.Add("HostCpuThresholdHigh", dwmPool.HostCpuThreshold.High.ToString());
					poolConfigurationResponse.Add("HostCpuThresholdMedium", dwmPool.HostCpuThreshold.Medium.ToString());
					poolConfigurationResponse.Add("HostCpuThresholdLow", dwmPool.HostCpuThreshold.Low.ToString());
					poolConfigurationResponse.Add("HostMemoryThresholdCritical", dwmPool.HostMemoryThreshold.Critical.ToString());
					poolConfigurationResponse.Add("HostMemoryThresholdHigh", dwmPool.HostMemoryThreshold.High.ToString());
					poolConfigurationResponse.Add("HostMemoryThresholdMedium", dwmPool.HostMemoryThreshold.Medium.ToString());
					poolConfigurationResponse.Add("HostMemoryThresholdLow", dwmPool.HostMemoryThreshold.Low.ToString());
					poolConfigurationResponse.Add("HostPifReadThresholdCritical", dwmPool.HostPifReadThreshold.Critical.ToString());
					poolConfigurationResponse.Add("HostPifReadThresholdHigh", dwmPool.HostPifReadThreshold.High.ToString());
					poolConfigurationResponse.Add("HostPifReadThresholdMedium", dwmPool.HostPifReadThreshold.Medium.ToString());
					poolConfigurationResponse.Add("HostPifReadThresholdLow", dwmPool.HostPifReadThreshold.Low.ToString());
					poolConfigurationResponse.Add("HostPifWriteThresholdCritical", dwmPool.HostPifWriteThreshold.Critical.ToString());
					poolConfigurationResponse.Add("HostPifWriteThresholdHigh", dwmPool.HostPifWriteThreshold.High.ToString());
					poolConfigurationResponse.Add("HostPifWriteThresholdMedium", dwmPool.HostPifWriteThreshold.Medium.ToString());
					poolConfigurationResponse.Add("HostPifWriteThresholdLow", dwmPool.HostPifWriteThreshold.Low.ToString());
					poolConfigurationResponse.Add("HostPbdReadThresholdCritical", dwmPool.HostPbdReadThreshold.Critical.ToString());
					poolConfigurationResponse.Add("HostPbdReadThresholdHigh", dwmPool.HostPbdReadThreshold.High.ToString());
					poolConfigurationResponse.Add("HostPbdReadThresholdMedium", dwmPool.HostPbdReadThreshold.Medium.ToString());
					poolConfigurationResponse.Add("HostPbdReadThresholdLow", dwmPool.HostPbdReadThreshold.Low.ToString());
					poolConfigurationResponse.Add("HostPbdWriteThresholdCritical", dwmPool.HostPbdWriteThreshold.Critical.ToString());
					poolConfigurationResponse.Add("HostPbdWriteThresholdHigh", dwmPool.HostPbdWriteThreshold.High.ToString());
					poolConfigurationResponse.Add("HostPbdWriteThresholdMedium", dwmPool.HostPbdWriteThreshold.Medium.ToString());
					poolConfigurationResponse.Add("HostPbdWriteThresholdLow", dwmPool.HostPbdWriteThreshold.Low.ToString());
					poolConfigurationResponse.Add("HostLoadAverageThresholdCritical", dwmPool.HostLoadAverageThreshold.Critical.ToString());
					poolConfigurationResponse.Add("HostLoadAverageThresholdHigh", dwmPool.HostLoadAverageThreshold.High.ToString());
					poolConfigurationResponse.Add("HostLoadAverageThresholdMedium", dwmPool.HostLoadAverageThreshold.Medium.ToString());
					poolConfigurationResponse.Add("HostLoadAverageThresholdLow", dwmPool.HostLoadAverageThreshold.Low.ToString());
					poolConfigurationResponse.Add("WeightCurrentMetrics", dwmPool.WeightCurrentMetrics.ToString());
					poolConfigurationResponse.Add("WeightRecentMetrics", dwmPool.WeightRecentMetrics.ToString());
					poolConfigurationResponse.Add("WeightHistoricalMetrics", dwmPool.WeightHistoricMetrics.ToString());
					poolConfigurationResponse.Add("VmCpuUtilizationThresholdHigh", dwmPool.VmCpuUtilizationThreshold.High.ToString());
					poolConfigurationResponse.Add("VmCpuUtilizationThresholdMedium", dwmPool.VmCpuUtilizationThreshold.Medium.ToString());
					poolConfigurationResponse.Add("VmCpuUtilizationThresholdLow", dwmPool.VmCpuUtilizationThreshold.Low.ToString());
					poolConfigurationResponse.Add("VmCpuUtilizationWeightHigh", dwmPool.VmCpuUtilizationWeight.High.ToString());
					poolConfigurationResponse.Add("VmCpuUtilizationWeightMedium", dwmPool.VmCpuUtilizationWeight.Medium.ToString());
					poolConfigurationResponse.Add("VmCpuUtilizationWeightLow", dwmPool.VmCpuUtilizationWeight.Low.ToString());
					poolConfigurationResponse.Add("VmMemoryThresholdHigh", dwmPool.VmMemoryThreshold.High.ToString());
					poolConfigurationResponse.Add("VmMemoryThresholdMedium", dwmPool.VmMemoryThreshold.Medium.ToString());
					poolConfigurationResponse.Add("VmMemoryThresholdLow", dwmPool.VmMemoryThreshold.Low.ToString());
					poolConfigurationResponse.Add("VmMemoryWeightHigh", dwmPool.VmMemoryWeight.High.ToString());
					poolConfigurationResponse.Add("VmMemoryWeightMedium", dwmPool.VmMemoryWeight.Medium.ToString());
					poolConfigurationResponse.Add("VmMemoryWeightLow", dwmPool.VmMemoryWeight.Low.ToString());
					poolConfigurationResponse.Add("VmNetworkReadThresholdHigh", dwmPool.VmNetworkReadThreshold.High.ToString());
					poolConfigurationResponse.Add("VmNetworkReadThresholdMedium", dwmPool.VmNetworkReadThreshold.Medium.ToString());
					poolConfigurationResponse.Add("VmNetworkReadThresholdLow", dwmPool.VmNetworkReadThreshold.Low.ToString());
					poolConfigurationResponse.Add("VmNetworkReadWeightHigh", dwmPool.VmNetworkReadWeight.High.ToString());
					poolConfigurationResponse.Add("VmNetworkReadWeightMedium", dwmPool.VmNetworkReadWeight.Medium.ToString());
					poolConfigurationResponse.Add("VmNetworkReadWeightLow", dwmPool.VmNetworkReadWeight.Low.ToString());
					poolConfigurationResponse.Add("VmNetworkWriteThresholdHigh", dwmPool.VmNetworkWriteThreshold.High.ToString());
					poolConfigurationResponse.Add("VmNetworkWriteThresholdMedium", dwmPool.VmNetworkWriteThreshold.Medium.ToString());
					poolConfigurationResponse.Add("VmNetworkWriteThresholdLow", dwmPool.VmNetworkWriteThreshold.Low.ToString());
					poolConfigurationResponse.Add("VmNetworkWriteWeightHigh", dwmPool.VmNetworkWriteWeight.High.ToString());
					poolConfigurationResponse.Add("VmNetworkWriteWeightMedium", dwmPool.VmNetworkWriteWeight.Medium.ToString());
					poolConfigurationResponse.Add("VmNetworkWriteWeightLow", dwmPool.VmNetworkWriteWeight.Low.ToString());
					poolConfigurationResponse.Add("VmDiskReadThresholdHigh", dwmPool.VmDiskReadThreshold.High.ToString());
					poolConfigurationResponse.Add("VmDiskReadThresholdMedium", dwmPool.VmDiskReadThreshold.Medium.ToString());
					poolConfigurationResponse.Add("VmDiskReadThresholdLow", dwmPool.VmDiskReadThreshold.Low.ToString());
					poolConfigurationResponse.Add("VmDiskReadWeightHigh", dwmPool.VmDiskReadWeight.High.ToString());
					poolConfigurationResponse.Add("VmDiskReadWeightMedium", dwmPool.VmDiskReadWeight.Medium.ToString());
					poolConfigurationResponse.Add("VmDiskReadWeightLow", dwmPool.VmDiskReadWeight.Low.ToString());
					poolConfigurationResponse.Add("VmDiskWriteThresholdHigh", dwmPool.VmDiskWriteThreshold.High.ToString());
					poolConfigurationResponse.Add("VmDiskWriteThresholdMedium", dwmPool.VmDiskWriteThreshold.Medium.ToString());
					poolConfigurationResponse.Add("VmDiskWriteThresholdLow", dwmPool.VmDiskWriteThreshold.Low.ToString());
					poolConfigurationResponse.Add("VmDiskWriteWeightHigh", dwmPool.VmDiskWriteWeight.High.ToString());
					poolConfigurationResponse.Add("VmDiskWriteWeightMedium", dwmPool.VmDiskWriteWeight.Medium.ToString());
					poolConfigurationResponse.Add("VmDiskWriteWeightLow", dwmPool.VmDiskWriteWeight.Low.ToString());
					poolConfigurationResponse.Add("WlbVersion", Configuration.GetValueAsString(ConfigItems.WLBVersion));
					Dictionary<string, string> otherConfig = dwmPool.GetOtherConfig();
					foreach (KeyValuePair<string, string> current in otherConfig)
					{
						poolConfigurationResponse.Add(current.Key, current.Value);
					}
					if (Configuration.GetValueAsBool(ConfigItems.WlbWebServiceTrace))
					{
						foreach (KeyValuePair<string, string> current2 in otherConfig)
						{
							base.Trace("ConfigurationService.OnGet:  {0}={1}", new object[]
							{
								current2.Key,
								current2.Value
							});
						}
					}
				}
			}
			catch (HttpError ex)
			{
				Logger.LogException(ex);
				throw;
			}
			catch (Exception ex2)
			{
				Logger.LogException(ex2);
				throw new HttpError(HttpStatusCode.InternalServerError, base.GetExceptionErrorCode(ex2).ToString(), ex2.Message);
			}
			if (dwmErrorCode != DwmErrorCode.None)
			{
				throw new HttpError(HttpStatusCode.BadRequest, dwmErrorCode.ToString());
			}
			return poolConfigurationResponse;
		}
		public override object OnPut(PoolConfiguration request)
		{
			base.CheckNullArguments<PoolConfiguration>(request);
			DwmPool dwmPool = null;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			DwmErrorCode dwmErrorCode = DwmErrorCode.None;
			base.Trace("PoolConfigurationService.OnPut:  PoolUuid={0}", new object[]
			{
				request.Uuid
			});
			if (request.Configurations == null || request.Configurations.Count == 0)
			{
				throw new HttpError(HttpStatusCode.BadRequest, DwmErrorCode.InvalidParameter.ToString(), "Empty configuration passed.");
			}
			try
			{
				dwmPool = new DwmPool(request.Uuid, null, DwmHypervisorType.XenServer);
				if (dwmPool.Id == 0)
				{
					dwmErrorCode = DwmErrorCode.UnknownPool;
				}
				else
				{
					dwmPool.Load();
					foreach (Pair current in request.Configurations)
					{
						base.Trace("IWorkloadBalance.SetXenPoolConfiguration:  {0}={1}", new object[]
						{
							current.Name,
							current.Value
						});
						if (Localization.Compare(current.Name, "OptimizationMode", true) == 0)
						{
							int num = this.ParseInteger(current);
							if (num >= 0 && num <= 1)
							{
								dwmPool.OptMode = (OptimizationMode)num;
							}
						}
						else
						{
							if (Localization.Compare(current.Name, "OverCommitCpuRatio", true) == 0)
							{
								dwmPool.OverCommitCpuRatio = this.ParseInteger(current);
							}
							else
							{
								if (Localization.Compare(current.Name, "OverCommitCpuInPerfMode", true) == 0)
								{
									dwmPool.OverCommitCpusInPerfMode = this.ParseBoolean(current);
								}
								else
								{
									if (Localization.Compare(current.Name, "OverCommitCpuInDensityMode", true) == 0)
									{
										dwmPool.OverCommitCpusInDensityMode = this.ParseBoolean(current);
									}
									else
									{
										if (Localization.Compare(current.Name, "HostCpuThresholdCritical", true) == 0)
										{
											dwmPool.HostCpuThreshold.Critical = this.ParseDouble(current);
										}
										else
										{
											if (Localization.Compare(current.Name, "HostCpuThresholdHigh", true) == 0)
											{
												dwmPool.HostCpuThreshold.High = this.ParseDouble(current);
											}
											else
											{
												if (Localization.Compare(current.Name, "HostCpuThresholdMedium", true) == 0)
												{
													dwmPool.HostCpuThreshold.Medium = this.ParseDouble(current);
												}
												else
												{
													if (Localization.Compare(current.Name, "HostCpuThresholdLow", true) == 0)
													{
														dwmPool.HostCpuThreshold.Low = this.ParseDouble(current);
													}
													else
													{
														if (Localization.Compare(current.Name, "HostMemoryThresholdCritical", true) == 0)
														{
															dwmPool.HostMemoryThreshold.Critical = this.ParseDouble(current);
														}
														else
														{
															if (Localization.Compare(current.Name, "HostMemoryThresholdHigh", true) == 0)
															{
																dwmPool.HostMemoryThreshold.High = this.ParseDouble(current);
															}
															else
															{
																if (Localization.Compare(current.Name, "HostMemoryThresholdMedium", true) == 0)
																{
																	dwmPool.HostMemoryThreshold.Medium = this.ParseDouble(current);
																}
																else
																{
																	if (Localization.Compare(current.Name, "HostMemoryThresholdLow", true) == 0)
																	{
																		dwmPool.HostMemoryThreshold.Low = this.ParseDouble(current);
																	}
																	else
																	{
																		if (Localization.Compare(current.Name, "HostPifReadThresholdCritical", true) == 0)
																		{
																			dwmPool.HostPifReadThreshold.Critical = this.ParseDouble(current);
																		}
																		else
																		{
																			if (Localization.Compare(current.Name, "HostPifReadThresholdHigh", true) == 0)
																			{
																				dwmPool.HostPifReadThreshold.High = this.ParseDouble(current);
																			}
																			else
																			{
																				if (Localization.Compare(current.Name, "HostPifReadThresholdMedium", true) == 0)
																				{
																					dwmPool.HostPifReadThreshold.Medium = this.ParseDouble(current);
																				}
																				else
																				{
																					if (Localization.Compare(current.Name, "HostPifReadThresholdLow", true) == 0)
																					{
																						dwmPool.HostPifReadThreshold.Low = this.ParseDouble(current);
																					}
																					else
																					{
																						if (Localization.Compare(current.Name, "HostPifWriteThresholdCritical", true) == 0)
																						{
																							dwmPool.HostPifWriteThreshold.Critical = this.ParseDouble(current);
																						}
																						else
																						{
																							if (Localization.Compare(current.Name, "HostPifWriteThresholdHigh", true) == 0)
																							{
																								dwmPool.HostPifWriteThreshold.High = this.ParseDouble(current);
																							}
																							else
																							{
																								if (Localization.Compare(current.Name, "HostPifWriteThresholdMedium", true) == 0)
																								{
																									dwmPool.HostPifWriteThreshold.Medium = this.ParseDouble(current);
																								}
																								else
																								{
																									if (Localization.Compare(current.Name, "HostPifWriteThresholdLow", true) == 0)
																									{
																										dwmPool.HostPifWriteThreshold.Low = this.ParseDouble(current);
																									}
																									else
																									{
																										if (Localization.Compare(current.Name, "HostPbdReadThresholdCritical", true) == 0)
																										{
																											dwmPool.HostPbdReadThreshold.Critical = this.ParseDouble(current);
																										}
																										else
																										{
																											if (Localization.Compare(current.Name, "HostPbdReadThresholdHigh", true) == 0)
																											{
																												dwmPool.HostPbdReadThreshold.High = this.ParseDouble(current);
																											}
																											else
																											{
																												if (Localization.Compare(current.Name, "HostPbdReadThresholdMedium", true) == 0)
																												{
																													dwmPool.HostPbdReadThreshold.Medium = this.ParseDouble(current);
																												}
																												else
																												{
																													if (Localization.Compare(current.Name, "HostPbdReadThresholdLow", true) == 0)
																													{
																														dwmPool.HostPbdReadThreshold.Low = this.ParseDouble(current);
																													}
																													else
																													{
																														if (Localization.Compare(current.Name, "HostPbdWriteThresholdCritical", true) == 0)
																														{
																															dwmPool.HostPbdWriteThreshold.Critical = this.ParseDouble(current);
																														}
																														else
																														{
																															if (Localization.Compare(current.Name, "HostPbdWriteThresholdHigh", true) == 0)
																															{
																																dwmPool.HostPbdWriteThreshold.High = this.ParseDouble(current);
																															}
																															else
																															{
																																if (Localization.Compare(current.Name, "HostPbdWriteThresholdMedium", true) == 0)
																																{
																																	dwmPool.HostPbdWriteThreshold.Medium = this.ParseDouble(current);
																																}
																																else
																																{
																																	if (Localization.Compare(current.Name, "HostPbdWriteThresholdLow", true) == 0)
																																	{
																																		dwmPool.HostPbdWriteThreshold.Low = this.ParseDouble(current);
																																	}
																																	else
																																	{
																																		if (Localization.Compare(current.Name, "HostLoadAverageThresholdCritical", true) == 0)
																																		{
																																			dwmPool.HostLoadAverageThreshold.Critical = this.ParseDouble(current);
																																		}
																																		else
																																		{
																																			if (Localization.Compare(current.Name, "HostLoadAverageThresholdHigh", true) == 0)
																																			{
																																				dwmPool.HostLoadAverageThreshold.High = this.ParseDouble(current);
																																			}
																																			else
																																			{
																																				if (Localization.Compare(current.Name, "HostLoadAverageThresholdMedium", true) == 0)
																																				{
																																					dwmPool.HostLoadAverageThreshold.Medium = this.ParseDouble(current);
																																				}
																																				else
																																				{
																																					if (Localization.Compare(current.Name, "HostLoadAverageThresholdLow", true) == 0)
																																					{
																																						dwmPool.HostLoadAverageThreshold.Low = this.ParseDouble(current);
																																					}
																																					else
																																					{
																																						if (Localization.Compare(current.Name, "WeightCurrentMetrics", true) == 0)
																																						{
																																							dwmPool.WeightCurrentMetrics = this.ParseDouble(current);
																																						}
																																						else
																																						{
																																							if (Localization.Compare(current.Name, "WeightRecentMetrics", true) == 0)
																																							{
																																								dwmPool.WeightRecentMetrics = this.ParseDouble(current);
																																							}
																																							else
																																							{
																																								if (Localization.Compare(current.Name, "WeightHistoricalMetrics", true) == 0)
																																								{
																																									dwmPool.WeightHistoricMetrics = this.ParseDouble(current);
																																								}
																																								else
																																								{
																																									if (Localization.Compare(current.Name, "VmCpuUtilizationThresholdHigh", true) == 0)
																																									{
																																										dwmPool.VmCpuUtilizationThreshold.High = this.ParseDouble(current);
																																									}
																																									else
																																									{
																																										if (Localization.Compare(current.Name, "VmCpuUtilizationThresholdMedium", true) == 0)
																																										{
																																											dwmPool.VmCpuUtilizationThreshold.Medium = this.ParseDouble(current);
																																										}
																																										else
																																										{
																																											if (Localization.Compare(current.Name, "VmCpuUtilizationThresholdLow", true) == 0)
																																											{
																																												dwmPool.VmCpuUtilizationThreshold.Low = this.ParseDouble(current);
																																											}
																																											else
																																											{
																																												if (Localization.Compare(current.Name, "VmCpuUtilizationWeightHigh", true) == 0)
																																												{
																																													dwmPool.VmCpuUtilizationWeight.High = this.ParseDouble(current);
																																												}
																																												else
																																												{
																																													if (Localization.Compare(current.Name, "VmCpuUtilizationWeightMedium", true) == 0)
																																													{
																																														dwmPool.VmCpuUtilizationWeight.Medium = this.ParseDouble(current);
																																													}
																																													else
																																													{
																																														if (Localization.Compare(current.Name, "VmCpuUtilizationWeightLow", true) == 0)
																																														{
																																															dwmPool.VmCpuUtilizationWeight.Low = this.ParseDouble(current);
																																														}
																																														else
																																														{
																																															if (Localization.Compare(current.Name, "VmMemoryThresholdHigh", true) == 0)
																																															{
																																																dwmPool.VmMemoryThreshold.High = this.ParseDouble(current);
																																															}
																																															else
																																															{
																																																if (Localization.Compare(current.Name, "VmMemoryThresholdMedium", true) == 0)
																																																{
																																																	dwmPool.VmMemoryThreshold.Medium = this.ParseDouble(current);
																																																}
																																																else
																																																{
																																																	if (Localization.Compare(current.Name, "VmMemoryThresholdLow", true) == 0)
																																																	{
																																																		dwmPool.VmMemoryThreshold.Low = this.ParseDouble(current);
																																																	}
																																																	else
																																																	{
																																																		if (Localization.Compare(current.Name, "VmMemoryWeightHigh", true) == 0)
																																																		{
																																																			dwmPool.VmMemoryWeight.High = this.ParseDouble(current);
																																																		}
																																																		else
																																																		{
																																																			if (Localization.Compare(current.Name, "VmMemoryWeightMedium", true) == 0)
																																																			{
																																																				dwmPool.VmMemoryWeight.Medium = this.ParseDouble(current);
																																																			}
																																																			else
																																																			{
																																																				if (Localization.Compare(current.Name, "VmMemoryWeightLow", true) == 0)
																																																				{
																																																					dwmPool.VmMemoryWeight.Low = this.ParseDouble(current);
																																																				}
																																																				else
																																																				{
																																																					if (Localization.Compare(current.Name, "VmNetworkReadThresholdHigh", true) == 0)
																																																					{
																																																						dwmPool.VmNetworkReadThreshold.High = this.ParseDouble(current);
																																																					}
																																																					else
																																																					{
																																																						if (Localization.Compare(current.Name, "VmNetworkReadThresholdMedium", true) == 0)
																																																						{
																																																							dwmPool.VmNetworkReadThreshold.Medium = this.ParseDouble(current);
																																																						}
																																																						else
																																																						{
																																																							if (Localization.Compare(current.Name, "VmNetworkReadThresholdLow", true) == 0)
																																																							{
																																																								dwmPool.VmNetworkReadThreshold.Low = this.ParseDouble(current);
																																																							}
																																																							else
																																																							{
																																																								if (Localization.Compare(current.Name, "VmNetworkReadWeightHigh", true) == 0)
																																																								{
																																																									dwmPool.VmNetworkReadWeight.High = this.ParseDouble(current);
																																																								}
																																																								else
																																																								{
																																																									if (Localization.Compare(current.Name, "VmNetworkReadWeightMedium", true) == 0)
																																																									{
																																																										dwmPool.VmNetworkReadWeight.Medium = this.ParseDouble(current);
																																																									}
																																																									else
																																																									{
																																																										if (Localization.Compare(current.Name, "VmNetworkReadWeightLow", true) == 0)
																																																										{
																																																											dwmPool.VmNetworkReadWeight.Low = this.ParseDouble(current);
																																																										}
																																																										else
																																																										{
																																																											if (Localization.Compare(current.Name, "VmNetworkWriteThresholdHigh", true) == 0)
																																																											{
																																																												dwmPool.VmNetworkWriteThreshold.High = this.ParseDouble(current);
																																																											}
																																																											else
																																																											{
																																																												if (Localization.Compare(current.Name, "VmNetworkWriteThresholdMedium", true) == 0)
																																																												{
																																																													dwmPool.VmNetworkWriteThreshold.Medium = this.ParseDouble(current);
																																																												}
																																																												else
																																																												{
																																																													if (Localization.Compare(current.Name, "VmNetworkWriteThresholdLow", true) == 0)
																																																													{
																																																														dwmPool.VmNetworkWriteThreshold.Low = this.ParseDouble(current);
																																																													}
																																																													else
																																																													{
																																																														if (Localization.Compare(current.Name, "VmNetworkWriteWeightHigh", true) == 0)
																																																														{
																																																															dwmPool.VmNetworkWriteWeight.High = this.ParseDouble(current);
																																																														}
																																																														else
																																																														{
																																																															if (Localization.Compare(current.Name, "VmNetworkWriteWeightMedium", true) == 0)
																																																															{
																																																																dwmPool.VmNetworkWriteWeight.Medium = this.ParseDouble(current);
																																																															}
																																																															else
																																																															{
																																																																if (Localization.Compare(current.Name, "VmNetworkWriteWeightLow", true) == 0)
																																																																{
																																																																	dwmPool.VmNetworkWriteWeight.Low = this.ParseDouble(current);
																																																																}
																																																																else
																																																																{
																																																																	if (Localization.Compare(current.Name, "VmDiskReadThresholdHigh", true) == 0)
																																																																	{
																																																																		dwmPool.VmDiskReadThreshold.High = this.ParseDouble(current);
																																																																	}
																																																																	else
																																																																	{
																																																																		if (Localization.Compare(current.Name, "VmDiskReadThresholdMedium", true) == 0)
																																																																		{
																																																																			dwmPool.VmDiskReadThreshold.Medium = this.ParseDouble(current);
																																																																		}
																																																																		else
																																																																		{
																																																																			if (Localization.Compare(current.Name, "VmDiskReadThresholdLow", true) == 0)
																																																																			{
																																																																				dwmPool.VmDiskReadThreshold.Low = this.ParseDouble(current);
																																																																			}
																																																																			else
																																																																			{
																																																																				if (Localization.Compare(current.Name, "VmDiskReadWeightHigh", true) == 0)
																																																																				{
																																																																					dwmPool.VmDiskReadWeight.High = this.ParseDouble(current);
																																																																				}
																																																																				else
																																																																				{
																																																																					if (Localization.Compare(current.Name, "VmDiskReadWeightMedium", true) == 0)
																																																																					{
																																																																						dwmPool.VmDiskReadWeight.Medium = this.ParseDouble(current);
																																																																					}
																																																																					else
																																																																					{
																																																																						if (Localization.Compare(current.Name, "VmDiskReadWeightLow", true) == 0)
																																																																						{
																																																																							dwmPool.VmDiskReadWeight.Low = this.ParseDouble(current);
																																																																						}
																																																																						else
																																																																						{
																																																																							if (Localization.Compare(current.Name, "VmDiskWriteThresholdHigh", true) == 0)
																																																																							{
																																																																								dwmPool.VmDiskWriteThreshold.High = this.ParseDouble(current);
																																																																							}
																																																																							else
																																																																							{
																																																																								if (Localization.Compare(current.Name, "VmDiskWriteThresholdMedium", true) == 0)
																																																																								{
																																																																									dwmPool.VmDiskWriteThreshold.Medium = this.ParseDouble(current);
																																																																								}
																																																																								else
																																																																								{
																																																																									if (Localization.Compare(current.Name, "VmDiskWriteThresholdLow", true) == 0)
																																																																									{
																																																																										dwmPool.VmDiskWriteThreshold.Low = this.ParseDouble(current);
																																																																									}
																																																																									else
																																																																									{
																																																																										if (Localization.Compare(current.Name, "VmDiskWriteWeightHigh", true) == 0)
																																																																										{
																																																																											dwmPool.VmDiskWriteWeight.High = this.ParseDouble(current);
																																																																										}
																																																																										else
																																																																										{
																																																																											if (Localization.Compare(current.Name, "VmDiskWriteWeightMedium", true) == 0)
																																																																											{
																																																																												dwmPool.VmDiskWriteWeight.Medium = this.ParseDouble(current);
																																																																											}
																																																																											else
																																																																											{
																																																																												if (Localization.Compare(current.Name, "VmDiskWriteWeightLow", true) == 0)
																																																																												{
																																																																													dwmPool.VmDiskWriteWeight.Low = this.ParseDouble(current);
																																																																												}
																																																																												else
																																																																												{
																																																																													if (this._readOnlyConfigs.Contains(current.Name, StringComparer.OrdinalIgnoreCase))
																																																																													{
																																																																														throw new HttpError(HttpStatusCode.BadRequest, DwmErrorCode.InvalidOperation.ToString(), string.Format("{0} is read-only. ", current.Name));
																																																																													}
																																																																													try
																																																																													{
																																																																														if (!dictionary.ContainsKey(current.Name))
																																																																														{
																																																																															dictionary.Add(current.Name, current.Value);
																																																																														}
																																																																													}
																																																																													catch
																																																																													{
																																																																														Logger.Trace("Exception adding {0} to other config dictionary", new object[]
																																																																														{
																																																																															current.Name
																																																																														});
																																																																														dwmErrorCode = DwmErrorCode.InvalidOperation;
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
					if (dwmErrorCode == DwmErrorCode.None)
					{
						dwmPool.Save();
						dwmPool.SaveThresholds();
						dwmPool.SetOtherConfig(dictionary);
					}
				}
			}
			catch (HttpError ex)
			{
				Logger.LogException(ex);
				throw;
			}
			catch (Exception ex2)
			{
				Logger.LogException(ex2);
				throw new HttpError(HttpStatusCode.InternalServerError, base.GetExceptionErrorCode(ex2).ToString(), ex2.Message);
			}
			if (dwmErrorCode != DwmErrorCode.None)
			{
				throw new HttpError(HttpStatusCode.BadRequest, dwmErrorCode.ToString());
			}
			return new HttpResult
			{
				StatusCode = HttpStatusCode.OK
			};
		}
		protected double ParseDouble(Pair nvp)
		{
			string text = nvp.Value;
			if (text.Contains(","))
			{
				text = text.Replace(',', '.');
			}
			double result;
			try
			{
				result = double.Parse(text);
			}
			catch (Exception innerException)
			{
				throw new DwmException(string.Format("Error parsing value of {0}. Double value expected.", nvp.Name), DwmErrorCode.InvalidParameter, innerException);
			}
			return result;
		}
		protected int ParseInteger(Pair nvp)
		{
			int result;
			try
			{
				result = int.Parse(nvp.Value);
			}
			catch (Exception innerException)
			{
				throw new DwmException(string.Format("Error parsing value of {0}. Integer value expected.", nvp.Name), DwmErrorCode.InvalidParameter, innerException);
			}
			return result;
		}
		protected bool ParseBoolean(Pair nvp)
		{
			bool result;
			try
			{
				result = bool.Parse(nvp.Value);
			}
			catch (Exception innerException)
			{
				throw new DwmException(string.Format("Error parsing value of {0}. Boolean value expected.", nvp.Name), DwmErrorCode.InvalidParameter, innerException);
			}
			return result;
		}
	}
}
