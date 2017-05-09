// Decompiled with JetBrains decompiler
// Type: XenAPI.host_allowed_operations_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class host_allowed_operations_helper
  {
    public static string ToString(host_allowed_operations x)
    {
      switch (x)
      {
        case host_allowed_operations.provision:
          return "provision";
        case host_allowed_operations.evacuate:
          return "evacuate";
        case host_allowed_operations.shutdown:
          return "shutdown";
        case host_allowed_operations.reboot:
          return "reboot";
        case host_allowed_operations.power_on:
          return "power_on";
        case host_allowed_operations.vm_start:
          return "vm_start";
        case host_allowed_operations.vm_resume:
          return "vm_resume";
        case host_allowed_operations.vm_migrate:
          return "vm_migrate";
        default:
          return "unknown";
      }
    }
  }
}
