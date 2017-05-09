using Citrix.DWM.Framework;
using System;
using System.Collections.Generic;
namespace Citrix.DWM.Domain
{
	public abstract class DwmBaseCollection<T> : List<T>
	{
		private bool _isNew = true;
		protected bool IsNew
		{
			get
			{
				return this._isNew;
			}
			set
			{
				this._isNew = value;
			}
		}
		public void Save()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				this.Save(dBAccess);
			}
		}
		public virtual void Save(DBAccess db)
		{
			throw new NotImplementedException("This method must be overridden. Not implemented in base class");
		}
		public virtual void Save(DBAccess2 db)
		{
			throw new NotImplementedException("This method must be overridden. Not implemented in base class");
		}
	}
}
