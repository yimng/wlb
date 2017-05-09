// Decompiled with JetBrains decompiler
// Type: XenAPI.vm_appliance_operation_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vm_appliance_operation_helper
  {
    public static string ToString(vm_appliance_operation x)
    {
      switch (x)
      {
        case vm_appliance_operation.start:
          return "start";
        case vm_appliance_operation.clean_shutdown:
          return "clean_shutdown";
        case vm_appliance_operation.hard_shutdown:
          return "hard_shutdown";
        case vm_appliance_operation.shutdown:
          return "shutdown";
        default:
          return "unknown";
      }
    }
  }
}
