// Decompiled with JetBrains decompiler
// Type: XenAPI.vdi_type_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vdi_type_helper
  {
    public static string ToString(vdi_type x)
    {
      switch (x)
      {
        case vdi_type.system:
          return "system";
        case vdi_type.user:
          return "user";
        case vdi_type.ephemeral:
          return "ephemeral";
        case vdi_type.suspend:
          return "suspend";
        case vdi_type.crashdump:
          return "crashdump";
        case vdi_type.ha_statefile:
          return "ha_statefile";
        case vdi_type.metadata:
          return "metadata";
        case vdi_type.redo_log:
          return "redo_log";
        default:
          return "unknown";
      }
    }
  }
}
