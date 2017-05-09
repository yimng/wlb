// Decompiled with JetBrains decompiler
// Type: XenAPI.after_apply_guidance_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class after_apply_guidance_helper
  {
    public static string ToString(after_apply_guidance x)
    {
      switch (x)
      {
        case after_apply_guidance.restartHVM:
          return "restartHVM";
        case after_apply_guidance.restartPV:
          return "restartPV";
        case after_apply_guidance.restartHost:
          return "restartHost";
        case after_apply_guidance.restartXAPI:
          return "restartXAPI";
        default:
          return "unknown";
      }
    }
  }
}
