using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	public interface ITaskProcessor
	{
		void Execute(int TaskId, Dictionary<string, string> Parameters);
	}
}
