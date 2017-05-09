// Decompiled with JetBrains decompiler
// Type: XenAPI.vif_locking_mode_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vif_locking_mode_helper
  {
    public static string ToString(vif_locking_mode x)
    {
      switch (x)
      {
        case vif_locking_mode.network_default:
          return "network_default";
        case vif_locking_mode.locked:
          return "locked";
        case vif_locking_mode.unlocked:
          return "unlocked";
        case vif_locking_mode.disabled:
          return "disabled";
        default:
          return "unknown";
      }
    }
  }
}
