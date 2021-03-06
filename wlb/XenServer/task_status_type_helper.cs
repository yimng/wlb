﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.task_status_type_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class task_status_type_helper
  {
    public static string ToString(task_status_type x)
    {
      switch (x)
      {
        case task_status_type.pending:
          return "pending";
        case task_status_type.success:
          return "success";
        case task_status_type.failure:
          return "failure";
        case task_status_type.cancelling:
          return "cancelling";
        case task_status_type.cancelled:
          return "cancelled";
        default:
          return "unknown";
      }
    }
  }
}
