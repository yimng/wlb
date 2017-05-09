using Halsign.DWM.Framework;
using Funq;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.WebHost.Endpoints.Formats;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Halsign.DWM.Communication2
{
	public class RestWebServiceHost : AppHostHttpListenerBase
	{
		public RestWebServiceHost() : base("Halsign Workload Balancing", new Assembly[]
		{
			typeof(PoolService).Assembly
		})
		{
		}
		public override void Configure(Container container)
		{
			Feature enableFeatures = (Feature)2147483623;
			base.SetConfig(new EndpointHostConfig
			{
				GlobalResponseHeaders = 
				{

					{
						"Access-Control-Allow-Origin",
						"*"
					},

					{
						"Access-Control-Allow-Methods",
						"GET, POST, PUT, DELETE"
					}
				},
				EnableFeatures = enableFeatures
			});
			HtmlFormat.HtmlTitleFormat = HtmlFormat.HtmlTitleFormat.Replace("<a href=\"http://www.servicestack.net\">ServiceStack</a>", "Halsign Workload Balancing");
			base.RequestFilters.Add(delegate(IHttpRequest req, IHttpResponse res, object dto)
			{
				KeyValuePair<string, string>? basicAuthUserAndPassword = req.GetBasicAuthUserAndPassword();
				if (!basicAuthUserAndPassword.HasValue || !basicAuthUserAndPassword.HasValue)
				{
					res.ReturnAuthRequired();
					return;
				}
				if (!Authentication2.AreValid(basicAuthUserAndPassword.Value.Key, basicAuthUserAndPassword.Value.Value))
				{
					res.ReturnAuthRequired();
					return;
				}
			});
		}
		public override void Start(string urlBase)
		{
			base.Start(urlBase);
			Logger.Trace("REST Web Service running on {0}", new object[]
			{
				urlBase
			});
		}
	}
}
