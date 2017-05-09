using Halsign.DWM.Framework;
using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
namespace Halsign.DWM.Communication
{
	internal class Authentication
	{
		private static bool _traceEnabled;
		public static void Validate(string username, string password)
		{
			Authentication._traceEnabled = Configuration.GetValueAsBool(ConfigItems.WlbWebServiceTrace);
			Authentication.Trace("Authentication.Validate:  caller is {0}", username);
			if (username == null || password == null)
			{
				throw new ArgumentNullException();
			}
			username = Localization.ToUtf8(username).ToLower();
			password = Localization.ToUtf8(password);
			string valueAsString = Configuration.GetValueAsString(ConfigItems.WlbUsername);
			string valueAsString2 = Configuration.GetValueAsString(ConfigItems.WlbPassword);
			if (Localization.Compare(username, valueAsString, true) != 0 || Localization.Compare(Authentication.HashPassword(password, username), valueAsString2, false) != 0)
			{
				throw new SecurityException("Unknown Username or Password");
			}
		}
		private static string HashPassword(string password, string salt)
		{
			SHA384Managed sHA384Managed = new SHA384Managed();
			byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
			byte[] inArray = sHA384Managed.ComputeHash(bytes);
			sHA384Managed.Clear();
			return Convert.ToBase64String(inArray);
		}
		private static void Trace(string msg)
		{
			if (Authentication._traceEnabled)
			{
				Logger.Trace(msg);
			}
		}
		private static void Trace(string fmt, object arg)
		{
			if (Authentication._traceEnabled)
			{
				Logger.Trace(fmt, new object[]
				{
					arg
				});
			}
		}
	}
}
