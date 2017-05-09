using Halsign.DWM.Framework;
using System;
namespace Halsign.DWM.Domain
{
	public class DwmVmMovementCollection : DwmBaseCollection<DwmVmMovement>
	{
		public override void Save(DBAccess db)
		{
		}
		public bool ContainsVm(int vmId)
		{
			bool result = false;
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].VmId == vmId)
				{
					result = true;
					break;
				}
			}
			return result;
		}
	}
}
