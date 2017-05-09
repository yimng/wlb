// Decompiled with JetBrains decompiler
// Type: DwmWebSvc.Program
// Assembly: DwmWebSvc, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F88D341-0CDE-4C68-A384-20B87CB84096
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\DwmWebSvc.exe

using Citrix.DWM.Communication;
using Citrix.DWM.Framework;
using System;
using System.ServiceProcess;

namespace DwmWebSvc
{
  internal static class Program
  {
    private static void Main(string[] arguments)
    {
      Environment.SetEnvironmentVariable("ProcName", "DwmWebSvc", EnvironmentVariableTarget.Process);
      CmdLineArgs cmdLineArgs = new CmdLineArgs(arguments);
      if (!cmdLineArgs.AreArgsValid)
      {
        cmdLineArgs.ShowUsage();
      }
      else
      {
        DBAccess.PollUntilDbAcceptsConnection();
        if (cmdLineArgs.IsDebug)
        {
          Logger.ConsoleLogEnabled = true;
          new WorkloadBalanceHost().Start(true);
        }
        else
          ServiceBase.Run((ServiceBase) new DwmWebSvc());
      }
    }
  }
}
