using Halsign.DWM.Domain;
using Halsign.DWM.Framework;
using ServiceStack.Common.Web;
using System;
using System.Collections.Generic;
using System.Net;
namespace Halsign.DWM.Communication2
{
	public class RecommendationService : WlbServiceBase<VMPlacement>
	{
		public override object OnGet(VMPlacement request)
		{
			base.Trace("RecommendationService.OnGet called", new object[0]);
			base.CheckNullArguments<VMPlacement>(request);
			if (this.IsNullOrEmpty<string>(request.PoolList) && this.IsNullOrEmpty<string>(request.HostList))
			{
				throw new HttpError(HttpStatusCode.BadRequest, DwmErrorCode.InvalidParameter.ToString(), string.Format("Either HostList or PoolList should be present.", new object[0]));
			}
			VMPlacementResponse vMPlacementResponse = new VMPlacementResponse();
			DwmErrorCode dwmErrorCode = DwmErrorCode.None;
			try
			{
				DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(-1);
				dwmVirtualMachine.MaximumDynamicMemory = request.MaximumDynamicMemory.Value;
				dwmVirtualMachine.MinimumDynamicMemory = request.MinimumDynamicMemory.Value;
				dwmVirtualMachine.MaximumStaticMemory = request.MaximumStaticMemory.Value;
				dwmVirtualMachine.MinimumStaticMemory = request.MinimumStaticMemory.Value;
				dwmVirtualMachine.TargetMemory = request.TargetMemory.Value;
				dwmVirtualMachine.MemoryOverhead = request.MemoryOverhead.Value;
				dwmVirtualMachine.MinimumCpus = request.MinimumCpus.Value;
				DwmStorageRepository dwmStorageRepository = new DwmStorageRepository(-1);
				dwmStorageRepository.Size = request.RequiredStorage.Value;
				dwmVirtualMachine.RequiredStorage.Add(dwmStorageRepository);
				DwmHostCollection allHosts = CloudVMPlacement.GetAllHosts(dwmVirtualMachine, request.SharedStorage.Value, request.PoolList, request.HostList, out dwmErrorCode);
				if (allHosts != null)
				{
					vMPlacementResponse.RecommendedHosts = new List<Host>(allHosts.Count);
					for (int i = 0; i < allHosts.Count; i++)
					{
						Host host = new Host();
						host.HostUuid = allHosts[i].Uuid;
						host.HostName = allHosts[i].Name;
						if (allHosts[i].PowerState == PowerStatus.On)
						{
							host.Score = (double)allHosts[i].Metrics.Score;
							host.Stars = allHosts[i].Metrics.Stars;
							host.ZeroScoreReason = ((allHosts[i].Metrics.ZeroScoreReasons.Count <= 0) ? ZeroScoreReason.None.ToString() : allHosts[i].Metrics.ZeroScoreReasons[0].ToString());
						}
						else
						{
							host.Score = 0.0;
							host.Stars = 0.0;
							host.ZeroScoreReason = ((allHosts[i].PowerState != PowerStatus.None) ? ZeroScoreReason.HostOffline.ToString() : allHosts[i].Metrics.ZeroScoreReasons[0].ToString());
						}
						vMPlacementResponse.RecommendedHosts.Add(host);
						base.Trace("RecommendationResources.VMPlacement response:  HostName={0}", new object[]
						{
							host.HostName
						});
						base.Trace("RecommendationResources.VMPlacement response:  HostUuid={0}", new object[]
						{
							host.HostUuid
						});
						base.Trace("RecommendationResources.VMPlacement response:  Score={0}", new object[]
						{
							host.Score
						});
						base.Trace("RecommendationResources.VMPlacement response:  ZeroScoreReason={0}", new object[]
						{
							host.ZeroScoreReason
						});
					}
					vMPlacementResponse.RecommendedHosts.Sort(new Comparison<Host>(this.CompareHostsByScore));
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
			return vMPlacementResponse;
		}
		public bool IsNullOrEmpty<T>(List<T> list)
		{
			return list == null || list.Count == 0;
		}
		private int CompareHostsByScore(Host x, Host y)
		{
			double num = y.Score - x.Score;
			if (num < 0.0)
			{
				return -1;
			}
			if (num > 0.0)
			{
				return 1;
			}
			return 0;
		}
	}
}
