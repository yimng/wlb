// Decompiled with JetBrains decompiler
// Type: XenAPI.cls_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class cls_helper
  {
    public static string ToString(cls x)
    {
      switch (x)
      {
        case cls.VM:
          return "VM";
        case cls.Host:
          return "Host";
        case cls.SR:
          return "SR";
        case cls.Pool:
          return "Pool";
        case cls.VMPP:
          return "VMPP";
        default:
          return "unknown";
      }
    }
  }
}
