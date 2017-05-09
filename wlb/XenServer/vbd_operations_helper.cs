// Decompiled with JetBrains decompiler
// Type: XenAPI.vbd_operations_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vbd_operations_helper
  {
    public static string ToString(vbd_operations x)
    {
      switch (x)
      {
        case vbd_operations.attach:
          return "attach";
        case vbd_operations.eject:
          return "eject";
        case vbd_operations.insert:
          return "insert";
        case vbd_operations.plug:
          return "plug";
        case vbd_operations.unplug:
          return "unplug";
        case vbd_operations.unplug_force:
          return "unplug_force";
        case vbd_operations.pause:
          return "pause";
        case vbd_operations.unpause:
          return "unpause";
        default:
          return "unknown";
      }
    }
  }
}
