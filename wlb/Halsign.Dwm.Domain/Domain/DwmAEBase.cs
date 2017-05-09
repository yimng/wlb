using Halsign.DWM.Framework;
using System;
namespace Halsign.DWM.Domain
{
	public abstract class DwmAEBase
	{
		private static bool _verboseTraceEnabled;
		protected static bool VerboseTraceEnabled
		{
			get
			{
				return DwmAEBase._verboseTraceEnabled;
			}
			set
			{
				DwmAEBase._verboseTraceEnabled = value;
			}
		}
		protected static void Trace(string fmt, params object[] args)
		{
			if (Configuration.GetValueAsBool(ConfigItems.AnalEngTrace))
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
