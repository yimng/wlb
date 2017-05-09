// Decompiled with JetBrains decompiler
// Type: XenAPI.XenRef`1
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace XenAPI
{
  public class XenRef<T> where T : XenObject<T>
  {
    private readonly string opaqueRef;

    public string opaque_ref
    {
      get
      {
        return this.opaqueRef;
      }
    }

    public XenRef(string opaqueRef)
    {
      Trace.Assert(opaqueRef != null, "'opaqueRef' parameter must not be null");
      this.opaqueRef = opaqueRef;
    }

    public XenRef(T obj)
      : this(obj.opaque_ref)
    {
    }

    public static implicit operator string(XenRef<T> xenRef)
    {
      if (xenRef != null)
        return xenRef.opaque_ref;
      return (string) null;
    }

    public static XenRef<T> Create(object opaqueRef)
    {
      return XenRef<T>.Create((string) opaqueRef);
    }

    public static XenRef<T> Create(string opaqueRef)
    {
      return new XenRef<T>(opaqueRef);
    }

    public static List<XenRef<T>> Create(string[] opaqueRefs)
    {
      List<XenRef<T>> list = new List<XenRef<T>>(opaqueRefs.Length);
      foreach (string opaqueRef in opaqueRefs)
        list.Add(new XenRef<T>(opaqueRef));
      return list;
    }

    public static List<XenRef<T>> Create(object[] opaqueRefs)
    {
      List<XenRef<T>> list = new List<XenRef<T>>(opaqueRefs.Length);
      foreach (object obj in opaqueRefs)
        list.Add(new XenRef<T>(obj.ToString()));
      return list;
    }

    public static Dictionary<XenRef<T>, T> Create<S>(object o)
    {
      if (o == null)
        throw new ArgumentNullException("o");
      Hashtable hashtable = (Hashtable) o;
      Dictionary<XenRef<T>, T> dictionary = new Dictionary<XenRef<T>, T>();
      foreach (object index1 in (IEnumerable) hashtable.Keys)
      {
        XenRef<T> index2 = new XenRef<T>((string) index1);
        Hashtable table = (Hashtable) hashtable[index1];
        dictionary[index2] = (T) Marshalling.convertStruct(typeof (T), table);
      }
      return dictionary;
    }

    public override bool Equals(object obj)
    {
      XenRef<T> xenRef = obj as XenRef<T>;
      if (xenRef == null)
        return false;
      return this.opaqueRef.Equals(xenRef.opaqueRef);
    }

    public override int GetHashCode()
    {
      return this.opaqueRef.GetHashCode();
    }
  }
}
