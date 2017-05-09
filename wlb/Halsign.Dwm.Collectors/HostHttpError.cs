using System;
namespace Halsign.DWM.Collectors
{
	internal class HostHttpError
	{
		internal string HostUri;
		internal int ErrorCount;
		internal DateTime LastLogged;
	}
}
