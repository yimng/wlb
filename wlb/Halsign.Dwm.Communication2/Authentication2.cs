using Halsign.DWM.Framework;
using System;
using System.Security.Cryptography;
using System.Text;
namespace Halsign.DWM.Communication2
{
	internal class Authentication2
	{
		public static bool AreValid(string username, string password)
		{
			Authentication2.Trace("Authentication.AreValid:  caller is {0}", new object[]
			{
				username
			});
			username = Localization.ToUtf8(username).ToLower();
			password = Localization.ToUtf8(password);
			string valueAsString = Configuration.GetValueAsString(ConfigItems.WlbUsername);
			string valueAsString2 = Configuration.GetValueAsString(ConfigItems.WlbPassword);
			return Localization.Compare(username, valueAsString, true) == 0 && Localization.Compare(Authentication2.HashPassword(password, username), valueAsString2, false) == 0;
		}
		private static string HashPassword(string password, string salt)
		{
			SHA384Managed sHA384Managed = new SHA384Managed();
			byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
			byte[] inArray = sHA384Managed.ComputeHash(bytes);
			sHA384Managed.Clear();
			return Convert.ToBase64String(inArray);
		}
		private static void Trace(string fmt, params object[] args)
		{
			if (Configuration.GetValueAsBool(ConfigItems.WlbWebServiceTrace))
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
