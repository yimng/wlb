// Decompiled with JetBrains decompiler
// Type: XenAPI.host_allowed_operations
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public enum host_allowed_operations
  {
    provision,
    evacuate,
    shutdown,
    reboot,
    power_on,
    vm_start,
    vm_resume,
    vm_migrate,
    unknown,
  }
}
