// Decompiled with JetBrains decompiler
// Type: XenAPI.Response`1
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System.Collections.Generic;
using System.Diagnostics;

namespace XenAPI
{
  public struct Response<ValType>
  {
    public string Status;
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public ValType Value;
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public string[] ErrorDescription;

    public Response(ValType Value)
    {
      this.Status = "Success";
      this.Value = Value;
      this.ErrorDescription = new string[0];
    }

    public Response(bool Failure, string[] error)
    {
      this.Status = Failure ? "Failure" : "Success";
      this.Value = default (ValType);
      this.ErrorDescription = error;
    }

    internal ValType parse()
    {
      if ("Success".Equals(this.Status))
      {
        Trace.Assert((object) this.Value != null, "Value must not be null");
        return this.Value;
      }
      if (this.ErrorDescription == null)
        throw new Failure(new List<string>()
        {
          "INTERNAL_ERROR",
          "Null ErrorDescription in response"
        });
      throw new Failure(new List<string>((IEnumerable<string>) this.ErrorDescription));
    }
  }
}
