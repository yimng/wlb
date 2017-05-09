// Decompiled with JetBrains decompiler
// Type: XenAPI.network_default_locking_mode_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class network_default_locking_mode_helper
  {
    public static string ToString(network_default_locking_mode x)
    {
      switch (x)
      {
        case network_default_locking_mode.unlocked:
          return "unlocked";
        case network_default_locking_mode.disabled:
          return "disabled";
        default:
          return "unknown";
      }
    }
  }
}
