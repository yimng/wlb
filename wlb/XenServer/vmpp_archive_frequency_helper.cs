// Decompiled with JetBrains decompiler
// Type: XenAPI.vmpp_archive_frequency_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vmpp_archive_frequency_helper
  {
    public static string ToString(vmpp_archive_frequency x)
    {
      switch (x)
      {
        case vmpp_archive_frequency.never:
          return "never";
        case vmpp_archive_frequency.always_after_backup:
          return "always_after_backup";
        case vmpp_archive_frequency.daily:
          return "daily";
        case vmpp_archive_frequency.weekly:
          return "weekly";
        default:
          return "unknown";
      }
    }
  }
}
