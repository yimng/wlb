// Decompiled with JetBrains decompiler
// Type: DwmWebSvc.DwmWebSvc
// Assembly: DwmWebSvc, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F88D341-0CDE-4C68-A384-20B87CB84096
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\DwmWebSvc.exe

using Citrix.DWM.Communication;
using Citrix.DWM.Framework;
using System;
using System.Reflection;
using System.ServiceProcess;

namespace DwmWebSvc
{
  public class DwmWebSvc : ServiceBase
  {
    private WorkloadBalanceHost _wlbHost;

    public DwmWebSvc()
    {
      this.ServiceName = "DwmWebSvc";
      this.AutoLog = false;
    }

    protected override void OnStart(string[] args)
    {
      try
      {
        string fmt = "{0}";
        object[] objArray = new object[1];
        int index = 0;
        string fullName = Assembly.GetExecutingAssembly().FullName;
        objArray[index] = (object) fullName;
        Logger.Trace(fmt, objArray);
        this._wlbHost = new WorkloadBalanceHost();
        this._wlbHost.Start(true);
        Logger.Trace("Started WCF hosts.");
      }
      catch (Exception ex)
      {
        Logger.LogException(ex);
      }
    }

    protected override void OnStop()
    {
      try
      {
        Logger.Trace("OnStop called.  Stopping the service.");
        this._wlbHost.Stop();
        this._wlbHost = (WorkloadBalanceHost) null;
        Logger.Trace("Stopped.");
      }
      catch (Exception ex)
      {
        Logger.LogException(ex);
      }
    }

    protected override void OnShutdown()
    {
      Logger.Trace("OnShutdown called.  Stopping the service.");
      this.OnStop();
      base.OnShutdown();
    }
  }
}
