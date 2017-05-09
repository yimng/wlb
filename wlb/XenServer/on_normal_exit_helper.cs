// Decompiled with JetBrains decompiler
// Type: XenAPI.on_normal_exit_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class on_normal_exit_helper
  {
    public static string ToString(on_normal_exit x)
    {
      switch (x)
      {
        case on_normal_exit.destroy:
          return "destroy";
        case on_normal_exit.restart:
          return "restart";
        default:
          return "unknown";
      }
    }
  }
}
