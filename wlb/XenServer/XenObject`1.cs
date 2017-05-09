// Decompiled with JetBrains decompiler
// Type: XenAPI.XenObject`1
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.ComponentModel;

namespace XenAPI
{
  public abstract class XenObject<S> : IXenObject, INotifyPropertyChanged where S : XenObject<S>
  {
    private string _opaque_ref;
    private bool _changed;

    public string opaque_ref
    {
      get
      {
        return this._opaque_ref;
      }
      set
      {
        this._opaque_ref = value;
      }
    }

    public bool Changed
    {
      get
      {
        return this._changed;
      }
      set
      {
        this._changed = value;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public abstract void UpdateFrom(S update);

    public abstract string SaveChanges(Session session, string serverOpaqueRef, S serverObject);

    public void NotifyPropertyChanged(string info)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(info));
    }

    public void ClearEventListeners()
    {
      this.PropertyChanged = (PropertyChangedEventHandler) null;
    }
  }
}
