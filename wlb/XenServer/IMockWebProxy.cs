﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.IMockWebProxy
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.IO;
using System.Net;

namespace XenAPI
{
  public interface IMockWebProxy : IWebProxy
  {
    Stream GetStream(Uri uri);
  }
}
