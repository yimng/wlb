using Halsign.DWM.Framework;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
namespace Halsign.DWM.Communication2
{
	public abstract class WlbServiceBase<TRequest> : RestServiceBase<TRequest>
	{
		public WlbServiceBase()
		{
			this.AppHost.Config.DebugMode = Configuration.GetValueAsBool(ConfigItems.RestDebugMode, false);
		}
		protected void Trace(string fmt, params object[] args)
		{
			if (Configuration.GetValueAsBool(ConfigItems.WlbWebServiceTrace))
			{
				Logger.Trace(fmt, args);
			}
		}
		protected void CheckNullArguments<T>(T request)
		{
			List<string> list = new List<string>();
			PropertyInfo[] properties = typeof(T).GetProperties();
			PropertyInfo[] array = properties;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo propertyInfo = array[i];
				object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);
				if (customAttributes.Length > 0 && propertyInfo.GetValue(request, null) == null)
				{
					list.Add(propertyInfo.Name);
				}
			}
			if (list.Count > 0)
			{
				throw new HttpError(HttpStatusCode.BadRequest, DwmErrorCode.InvalidParameter.ToString(), string.Format("Required argument(s) empty: {0}", string.Join(", ", list.ToArray())));
			}
		}
		protected void CheckNullArguments(object arg, string name)
		{
			if (arg == null)
			{
				throw new HttpError(HttpStatusCode.BadRequest, DwmErrorCode.InvalidParameter.ToString(), string.Format("Required argument empty: {0}", name));
			}
		}
		protected DwmErrorCode GetExceptionErrorCode(Exception e)
		{
			DwmErrorCode result;
			if (e is DwmException)
			{
				result = (DwmErrorCode)(e as DwmException).Number;
			}
			else
			{
				result = DwmErrorCode.GenericException;
			}
			return result;
		}
	}
}
