// Decompiled with JetBrains decompiler
// Type: XenAPI.Marshalling
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public class Marshalling
  {
    public static object convertStruct(Type t, Hashtable table)
    {
      return t.GetConstructor(new Type[1]
      {
        typeof (Hashtable)
      }).Invoke(new object[1]
      {
        (object) table
      });
    }

    public static Type GetXenAPIType(string name)
    {
      return Type.GetType(string.Format("XenAPI.{0}", (object) name), false, true);
    }

    public static bool ParseBool(Hashtable table, string key)
    {
      if (!table.ContainsKey((object) key))
        return false;
      return (bool) table[(object) key];
    }

    public static DateTime ParseDateTime(Hashtable table, string key)
    {
      if (!table.ContainsKey((object) key))
        return DateTime.MinValue;
      return (DateTime) table[(object) key];
    }

    public static double ParseDouble(Hashtable table, string key)
    {
      if (!table.ContainsKey((object) key))
        return 0.0;
      return (double) table[(object) key];
    }

    public static Hashtable ParseHashTable(Hashtable table, string key)
    {
      if (!table.ContainsKey((object) key))
        return (Hashtable) null;
      return (Hashtable) table[(object) key];
    }

    public static long ParseLong(Hashtable table, string key)
    {
      string s = table.ContainsKey((object) key) ? (string) table[(object) key] : (string) null;
      if (s != null)
        return long.Parse(s);
      return 0L;
    }

    public static string ParseString(Hashtable table, string key)
    {
      if (!table.ContainsKey((object) key))
        return (string) null;
      return (string) table[(object) key];
    }

    public static string[] ParseStringArray(Hashtable table, string key)
    {
      object[] array = table.ContainsKey((object) key) ? (object[]) table[(object) key] : (object[]) null;
      if (array == null)
        return new string[0];
      return Array.ConvertAll<object, string>(array, (Converter<object, string>) (o => o.ToString()));
    }

    public static XenRef<T> ParseRef<T>(Hashtable table, string key) where T : XenObject<T>
    {
      string opaqueRef = Marshalling.ParseString(table, key);
      if (opaqueRef != null)
        return XenRef<T>.Create(opaqueRef);
      return (XenRef<T>) null;
    }

    public static List<XenRef<T>> ParseSetRef<T>(Hashtable table, string key) where T : XenObject<T>
    {
      string[] opaqueRefs = Marshalling.ParseStringArray(table, key);
      if (opaqueRefs != null)
        return XenRef<T>.Create(opaqueRefs);
      return (List<XenRef<T>>) null;
    }

    public static Dictionary<XenRef<T>, T> ParseMapRefRecord<T, U>(Hashtable table, string key) where T : XenObject<T>
    {
      Hashtable hashtable = Marshalling.ParseHashTable(table, key);
      if (hashtable != null)
        return XenRef<T>.Create<U>((object) hashtable);
      return (Dictionary<XenRef<T>, T>) null;
    }
  }
}
