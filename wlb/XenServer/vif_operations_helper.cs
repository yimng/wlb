// Decompiled with JetBrains decompiler
// Type: XenAPI.vif_operations_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vif_operations_helper
  {
    public static string ToString(vif_operations x)
    {
      switch (x)
      {
        case vif_operations.attach:
          return "attach";
        case vif_operations.plug:
          return "plug";
        case vif_operations.unplug:
          return "unplug";
        default:
          return "unknown";
      }
    }
  }
}
