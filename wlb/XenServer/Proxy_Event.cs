// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy_Event
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;

namespace XenAPI
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class Proxy_Event
  {
    public string id;
    public string timestamp;
    [XmlRpcMember("class")]
    public string class_;
    public string operation;
    [XmlRpcMember("ref")]
    public string opaqueRef;
    [XmlRpcMember("snapshot")]
    public object snapshot;
  }
}
