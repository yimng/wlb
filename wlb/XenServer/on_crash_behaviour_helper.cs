// Decompiled with JetBrains decompiler
// Type: XenAPI.on_crash_behaviour_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class on_crash_behaviour_helper
  {
    public static string ToString(on_crash_behaviour x)
    {
      switch (x)
      {
        case on_crash_behaviour.destroy:
          return "destroy";
        case on_crash_behaviour.coredump_and_destroy:
          return "coredump_and_destroy";
        case on_crash_behaviour.restart:
          return "restart";
        case on_crash_behaviour.coredump_and_restart:
          return "coredump_and_restart";
        case on_crash_behaviour.preserve:
          return "preserve";
        case on_crash_behaviour.rename_restart:
          return "rename_restart";
        default:
          return "unknown";
      }
    }
  }
}
