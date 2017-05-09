// Decompiled with JetBrains decompiler
// Type: XenAPI.vmpp_backup_type_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vmpp_backup_type_helper
  {
    public static string ToString(vmpp_backup_type x)
    {
      switch (x)
      {
        case vmpp_backup_type.snapshot:
          return "snapshot";
        case vmpp_backup_type.checkpoint:
          return "checkpoint";
        default:
          return "unknown";
      }
    }
  }
}
