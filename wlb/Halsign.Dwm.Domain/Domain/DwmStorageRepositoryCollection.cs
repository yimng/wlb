using Halsign.DWM.Framework;
using System;
namespace Halsign.DWM.Domain
{
	public class DwmStorageRepositoryCollection : DwmBaseCollection<DwmStorageRepository>
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
		public DwmStorageRepositoryCollection Copy()
		{
			DwmStorageRepositoryCollection dwmStorageRepositoryCollection = new DwmStorageRepositoryCollection();
			for (int i = 0; i < base.Count; i++)
			{
				dwmStorageRepositoryCollection.Add(base[i].Copy());
			}
			return dwmStorageRepositoryCollection;
		}
	}
}
