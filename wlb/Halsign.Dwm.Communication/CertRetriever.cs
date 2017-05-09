using Halsign.DWM.Collectors;
using Halsign.DWM.Domain;
using System;
using System.Threading;
namespace Halsign.DWM.Communication
{
	internal class CertRetriever
	{
		internal DwmPool _pool;
		internal CertRetriever(DwmPool pool)
		{
			this._pool = pool;
		}
		internal void GetCerts()
		{
			new Thread(new ThreadStart(this.ThreadProc))
			{
				Name = "GetCertsThread"
			}.Start();
		}
		private void ThreadProc()
		{
			if (this._pool != null)
			{
				ICollectorActions collectorActions = this._pool.CollectorActions;
				if (collectorActions != null)
				{
					collectorActions.GetCerts();
				}
			}
		}
	}
}
