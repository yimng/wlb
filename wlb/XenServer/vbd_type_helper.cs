﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.vbd_type_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class vbd_type_helper
  {
    public static string ToString(vbd_type x)
    {
      switch (x)
      {
        case vbd_type.CD:
          return "CD";
        case vbd_type.Disk:
          return "Disk";
        default:
          return "unknown";
      }
    }
  }
}
