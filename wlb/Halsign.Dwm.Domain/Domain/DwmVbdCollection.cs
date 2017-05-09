using Halsign.DWM.Framework;
using System;
namespace Halsign.DWM.Domain
{
	public class DwmVbdCollection : DwmBaseCollection<DwmVbd>
	{
		public override void Save(DBAccess db)
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Save(db);
			}
		}
		public bool ContainsKey(int key)
		{
			bool result = false;
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Id == key)
				{
					result = true;
					break;
				}
			}
			return result;
		}
		public DwmVbd GetDeviceByName(string deviceName)
		{
			DwmVbd result = null;
			for (int i = 0; i < base.Count; i++)
			{
				if (Localization.Compare(base[i].DeviceName, deviceName, true) == 0)
				{
					result = base[i];
					break;
				}
			}
			return result;
		}
	}
}
