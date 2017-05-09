using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
namespace Halsign.DWM.WlbConfig
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Messages
	{
		private static ResourceManager resourceMan;
		private static CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Messages.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Halsign.DWM.WlbConfig.Messages", typeof(Messages).Assembly);
					Messages.resourceMan = resourceManager;
				}
				return Messages.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Messages.resourceCulture;
			}
			set
			{
				Messages.resourceCulture = value;
			}
		}
		internal static string BANNER_SERVER
		{
			get
			{
				return Messages.ResourceManager.GetString("BANNER_SERVER", Messages.resourceCulture);
			}
		}
		internal static string BANNER_TOP
		{
			get
			{
				return Messages.ResourceManager.GetString("BANNER_TOP", Messages.resourceCulture);
			}
		}
		internal static string CHANGING_PASSWORD
		{
			get
			{
				return Messages.ResourceManager.GetString("CHANGING_PASSWORD", Messages.resourceCulture);
			}
		}
		internal static string CHANGING_USERNAME
		{
			get
			{
				return Messages.ResourceManager.GetString("CHANGING_USERNAME", Messages.resourceCulture);
			}
		}
		internal static string CONFIRM
		{
			get
			{
				return Messages.ResourceManager.GetString("CONFIRM", Messages.resourceCulture);
			}
		}
		internal static string CREDS_SAVED
		{
			get
			{
				return Messages.ResourceManager.GetString("CREDS_SAVED", Messages.resourceCulture);
			}
		}
		internal static string ERROR_CONNECTING_DB
		{
			get
			{
				return Messages.ResourceManager.GetString("ERROR_CONNECTING_DB", Messages.resourceCulture);
			}
		}
		internal static string ERROR_INVALID_CHAR_PASS
		{
			get
			{
				return Messages.ResourceManager.GetString("ERROR_INVALID_CHAR_PASS", Messages.resourceCulture);
			}
		}
		internal static string ERROR_INVALID_CHAR_USER
		{
			get
			{
				return Messages.ResourceManager.GetString("ERROR_INVALID_CHAR_USER", Messages.resourceCulture);
			}
		}
		internal static string ERROR_PASS_MISMATCH
		{
			get
			{
				return Messages.ResourceManager.GetString("ERROR_PASS_MISMATCH", Messages.resourceCulture);
			}
		}
		internal static string ERROR_PASSWORD_TOO_LONG
		{
			get
			{
				return Messages.ResourceManager.GetString("ERROR_PASSWORD_TOO_LONG", Messages.resourceCulture);
			}
		}
		internal static string ERROR_USERNAME_TOO_LONG
		{
			get
			{
				return Messages.ResourceManager.GetString("ERROR_USERNAME_TOO_LONG", Messages.resourceCulture);
			}
		}
		internal static string TRY_AGAIN
		{
			get
			{
				return Messages.ResourceManager.GetString("TRY_AGAIN", Messages.resourceCulture);
			}
		}
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal Messages()
		{
		}
	}
}
