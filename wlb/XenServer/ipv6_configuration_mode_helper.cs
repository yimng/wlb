// Decompiled with JetBrains decompiler
// Type: XenAPI.ipv6_configuration_mode_helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public static class ipv6_configuration_mode_helper
  {
    public static string ToString(ipv6_configuration_mode x)
    {
      switch (x)
      {
        case ipv6_configuration_mode.None:
          return "None";
        case ipv6_configuration_mode.DHCP:
          return "DHCP";
        case ipv6_configuration_mode.Static:
          return "Static";
        case ipv6_configuration_mode.Autoconf:
          return "Autoconf";
        default:
          return "unknown";
      }
    }
  }
}
