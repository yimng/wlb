// Decompiled with JetBrains decompiler
// Type: Citrix.DWM.Collectors.VmCacheItem
// Assembly: Citrix.Dwm.Collectors, Version=6.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0844E477-F94E-4593-A883-69DEC5AD079C
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\Citrix.Dwm.Collectors.dll

using Citrix.DWM.Domain;
using XenAPI;

namespace Citrix.DWM.Collectors
{
  internal class VmCacheItem
  {
    internal VM xenVM;
    internal DwmVirtualMachine dwmVM;
    internal string serverOpaqueRef;
    internal int pendingRecommendationId;
  }
}
