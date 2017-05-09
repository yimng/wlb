using Halsign.DWM.Domain;
using Halsign.DWM.Framework;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using System;
using System.Net;
namespace Halsign.DWM.Communication2
{
	public class PoolService : WlbServiceBase<Pool>
	{
		public override object OnGet(Pool request)
		{
			PoolResponse poolResponse = new PoolResponse();
			try
			{
				if (!string.IsNullOrEmpty(request.Uuid))
				{
					base.Trace("PoolService.OnGet:  PoolUuid={0}", new object[]
					{
						request.Uuid
					});
					DwmPool dwmPool = new DwmPool(request.Uuid, null, DwmHypervisorType.XenServer);
					dwmPool.Load();
					poolResponse.Pools.Add(new PoolInfo
					{
						DiscoveryStatus = new DiscoveryStatus?(dwmPool.PoolDiscoveryStatus),
						Name = dwmPool.Name,
						Url = dwmPool.Url,
						UserName = dwmPool.UserName,
						Uuid = dwmPool.Uuid
					});
				}
				else
				{
					base.Trace("PoolService.OnGet:  Get all the pools that monitored by WLB", new object[0]);
					DwmPoolCollection dwmPoolCollection = new DwmPoolCollection();
					dwmPoolCollection.Load();
					foreach (DwmPool current in dwmPoolCollection)
					{
						poolResponse.Pools.Add(new PoolInfo
						{
							DiscoveryStatus = new DiscoveryStatus?(current.PoolDiscoveryStatus),
							Name = current.Name,
							Url = current.Url,
							UserName = current.UserName,
							Uuid = current.Uuid
						});
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				throw new HttpError(HttpStatusCode.InternalServerError, base.GetExceptionErrorCode(ex).ToString(), ex.Message);
			}
			return poolResponse;
		}
		public override object OnPost(Pool request)
		{
			base.CheckNullArguments<Pool>(request);
			DwmPool dwmPool = null;
			try
			{
				if (string.IsNullOrEmpty(request.Uuid))
				{
					dwmPool = new DwmPool(null, Guid.NewGuid().ToString(), DwmHypervisorType.XenServer);
				}
				else
				{
					base.Trace("PoolService.OnPost:  PoolUuid={0}", new object[]
					{
						request.Uuid
					});
					dwmPool = new DwmPool(request.Uuid, null, DwmHypervisorType.XenServer);
					dwmPool.Load();
				}
				base.Trace("PoolService.OnPost:  UserName={0}", new object[]
				{
					request.UserName
				});
				base.Trace("PoolService.OnPost:  XenServerUrl={0}", new object[]
				{
					request.Url
				});
				Uri uri = new Uri(request.Url);
				dwmPool.Protocol = uri.Scheme;
				dwmPool.PrimaryPoolMasterAddr = uri.Host;
				dwmPool.PrimaryPoolMasterPort = uri.Port;
				dwmPool.UserName = request.UserName;
				dwmPool.Password = request.Password;
				if (!DwmPool.IsValidPool(dwmPool.PrimaryPoolMasterAddr, dwmPool.PrimaryPoolMasterPort, dwmPool.UserName, dwmPool.Password, DwmHypervisorType.XenServer))
				{
					throw new HttpError(HttpStatusCode.BadRequest, DwmErrorCode.XenCannotLogIn.ToString(), "Cannot initialize a hypervisor session using the supplied address and credentials");
				}
				dwmPool.Save();
				dwmPool.SaveThresholds();
			}
			catch (UriFormatException ex)
			{
				Logger.LogException(ex);
				throw new HttpError(HttpStatusCode.BadRequest, DwmErrorCode.InvalidParameter.ToString(), ex.Message);
			}
			catch (HttpError ex2)
			{
				Logger.LogException(ex2);
				throw;
			}
			catch (Exception ex3)
			{
				Logger.LogException(ex3);
				throw new HttpError(HttpStatusCode.InternalServerError, base.GetExceptionErrorCode(ex3).ToString(), ex3.Message);
			}
			return new PoolResponse
			{
				Pools = 
				{
					new PoolInfo
					{
						DiscoveryStatus = new DiscoveryStatus?(dwmPool.PoolDiscoveryStatus),
						Name = dwmPool.Name,
						Url = dwmPool.Url,
						UserName = dwmPool.UserName,
						Uuid = dwmPool.Uuid
					}
				}
			};
		}
		public override object OnDelete(Pool request)
		{
			base.CheckNullArguments(request.Uuid, "Uuid");
			DwmErrorCode dwmErrorCode = DwmErrorCode.None;
			try
			{
				base.Trace("PoolService.OnDelete:  PoolUuid={0}", new object[]
				{
					request.Uuid
				});
				DwmPool dwmPool = new DwmPool(request.Uuid, null, DwmHypervisorType.XenServer);
				if (dwmPool != null && dwmPool.Id > 0)
				{
					dwmPool.Delete();
				}
				else
				{
					dwmErrorCode = DwmErrorCode.UnknownPool;
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
		protected override void OnBeforeExecute(Pool request)
		{
			IHttpRequest httpRequest = base.RequestContext.Get<IHttpRequest>();
			if (httpRequest.HttpMethod == "POST")
			{
				try
				{
					string value = httpRequest.QueryString["from_xapi"];
					bool flag;
					bool.TryParse(value, out flag);
					if (!flag && !Configuration.GetValueAsBool(ConfigItems.ForcePoolEnabled))
					{
						Configuration.SetValue(ConfigItems.ForcePoolEnabled, true);
					}
				}
				catch
				{
				}
			}
			base.OnBeforeExecute(request);
		}
	}
}
