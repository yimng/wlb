using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;

namespace Halsign.DWM.Domain
{
  /// <summary>
  /// Abstract base class for lists of DWM classes.
  /// 
  /// </summary>
  /// <typeparam name="T">Type of item in the list.</typeparam>
  public abstract class DwmBaseCollection<T> : List<T>
  {
    /// <summary>
    /// Flag to indicate if the data in the collection is new or has been
    ///             loaded from the database.
    /// 
    /// </summary>
    private bool _isNew = true;

    /// <summary>
    /// Get or set a flag to indicate if the data in the collection is new
    ///             or has been loaded from the database.
    /// 
    /// </summary>
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

    /// <summary>
    /// Write the list to the database.
    /// 
    /// </summary>
    public void Save()
    {
      using (DBAccess db = new DBAccess())
        this.Save(db);
    }

    /// <summary>
    /// Write the list to the database.
    /// 
    /// </summary>
    /// <param name="db">DBAccess instance with a connection to the
    ///             database.</param>
    /// <remarks>
    /// This method must be implemented by derived classes.
    /// </remarks>
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
