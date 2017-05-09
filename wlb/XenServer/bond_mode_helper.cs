// Decompiled with JetBrains decompiler
// Type: XenAPI.bond_mode_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class bond_mode_helper
  {
    public static string ToString(bond_mode x)
    {
      switch (x)
      {
        case bond_mode.balance_slb:
          return "balance-slb";
        case bond_mode.active_backup:
          return "active-backup";
        case bond_mode.lacp:
          return "lacp";
        default:
          return "unknown";
      }
    }
  }
}
