﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.storage_operations_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class storage_operations_helper
  {
    public static string ToString(storage_operations x)
    {
      switch (x)
      {
        case storage_operations.scan:
          return "scan";
        case storage_operations.destroy:
          return "destroy";
        case storage_operations.forget:
          return "forget";
        case storage_operations.plug:
          return "plug";
        case storage_operations.unplug:
          return "unplug";
        case storage_operations.update:
          return "update";
        case storage_operations.vdi_create:
          return "vdi_create";
        case storage_operations.vdi_introduce:
          return "vdi_introduce";
        case storage_operations.vdi_destroy:
          return "vdi_destroy";
        case storage_operations.vdi_resize:
          return "vdi_resize";
        case storage_operations.vdi_clone:
          return "vdi_clone";
        case storage_operations.vdi_snapshot:
          return "vdi_snapshot";
        case storage_operations.pbd_create:
          return "pbd_create";
        case storage_operations.pbd_destroy:
          return "pbd_destroy";
        default:
          return "unknown";
      }
    }
  }
}
