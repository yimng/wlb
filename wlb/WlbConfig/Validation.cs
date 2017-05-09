using System;
namespace Halsign.DWM.WlbConfig
{
	public class Validation
	{
		private const int MaxUsernameLength = 48;
		private const int MaxPasswordLength = 48;
		public static bool IsUserNameValid(string userName, out string reason)
		{
			reason = string.Empty;
			if (userName.Length > 48)
			{
				reason = string.Format(Messages.ERROR_USERNAME_TOO_LONG, 48);
			}
			else
			{
				if (userName.Contains(":"))
				{
					reason = string.Format(Messages.ERROR_INVALID_CHAR_USER, ":");
				}
				else
				{
					if (!userName.Contains(" "))
					{
						return true;
					}
					reason = string.Format(Messages.ERROR_INVALID_CHAR_USER, "space");
				}
			}
			return false;
		}
		public static bool IsPasswordValid(string password, out string reason)
		{
			reason = string.Empty;
			if (password.Length > 48)
			{
				reason = string.Format(Messages.ERROR_PASSWORD_TOO_LONG, 48);
			}
			else
			{
				if (!password.Contains(":"))
				{
					return true;
				}
				reason = string.Format(Messages.ERROR_INVALID_CHAR_PASS, ":");
			}
			return false;
		}
	}
}
