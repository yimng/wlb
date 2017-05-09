// Decompiled with JetBrains decompiler
// Type: XenAPI.vm_power_state_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vm_power_state_helper
  {
    public static string ToString(vm_power_state x)
    {
      switch (x)
      {
        case vm_power_state.Halted:
          return "Halted";
        case vm_power_state.Paused:
          return "Paused";
        case vm_power_state.Running:
          return "Running";
        case vm_power_state.Suspended:
          return "Suspended";
        default:
          return "unknown";
      }
    }
  }
}
