// Decompiled with JetBrains decompiler
// Type: XenAPI.Helper
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  public static class Helper
  {
    public const string NullOpaqueRef = "OpaqueRef:NULL";

    public static string APIVersionString(API_Version v)
    {
      switch (v)
      {
        case API_Version.API_1_1:
          return "1.1";
        case API_Version.API_1_2:
          return "1.2";
        case API_Version.API_1_3:
          return "1.3";
        case API_Version.API_1_4:
          return "1.4";
        case API_Version.API_1_5:
          return "1.5";
        case API_Version.API_1_6:
          return "1.6";
        case API_Version.API_1_7:
          return "1.7";
        case API_Version.API_1_8:
          return "1.8";
        case API_Version.API_1_9:
          return "1.9";
        case API_Version.API_1_10:
          return "1.10";
        default:
          return "Unknown";
      }
    }

    public static API_Version GetAPIVersion(long major, long minor)
    {
      try
      {
        return (API_Version) Enum.Parse(typeof (API_Version), string.Format("API_{0}_{1}", (object) major, (object) minor));
      }
      catch (ArgumentException ex)
      {
        return API_Version.UNKNOWN;
      }
    }

    public static API_Version GetAPIVersion(string version)
    {
      if (version != null)
      {
        string[] strArray = version.Split('.');
        int result1;
        int result2;
        if (strArray.Length == 2 && int.TryParse(strArray[0], out result1) && int.TryParse(strArray[1], out result2))
          return Helper.GetAPIVersion((long) result1, (long) result2);
      }
      return API_Version.UNKNOWN;
    }

    internal static int APIVersionCompare(Session session, API_Version v)
    {
      return session.APIVersion - v;
    }

    internal static bool APIVersionMeets(Session session, API_Version v)
    {
      return Helper.APIVersionCompare(session, v) >= 0;
    }

    public static bool AreEqual(object o1, object o2)
    {
      if (o1 == null && o2 == null)
        return true;
      if (o1 == null || o2 == null)
        return false;
      if (o1 is IDictionary)
        return Helper.AreDictEqual((IDictionary) o1, (IDictionary) o2);
      if (o1 is ICollection)
        return Helper.AreCollectionsEqual((ICollection) o1, (ICollection) o2);
      return o1.Equals(o2);
    }

    public static bool AreEqual2<T>(T o1, T o2)
    {
      if ((object) o1 == null && (object) o2 == null)
        return true;
      if ((object) o1 == null || (object) o2 == null)
      {
        if ((object) o1 == null && Helper.IsEmptyCollection((object) o2))
          return true;
        if ((object) o2 == null)
          return Helper.IsEmptyCollection((object) o1);
        return false;
      }
      if (typeof (T) is IDictionary)
        return Helper.AreDictEqual((IDictionary) (object) o1, (IDictionary) (object) o2);
      if (typeof (T) is ICollection)
        return Helper.AreCollectionsEqual((ICollection) (object) o1, (ICollection) (object) o2);
      return o1.Equals((object) o2);
    }

    private static bool IsEmptyCollection(object obj)
    {
      ICollection collection = obj as ICollection;
      if (collection != null)
        return collection.Count == 0;
      return false;
    }

    private static bool AreDictEqual(IDictionary d1, IDictionary d2)
    {
      if (d1.Count != d2.Count)
        return false;
      foreach (object key in (IEnumerable) d1.Keys)
      {
        if (!d2.Contains(key) || !Helper.AreEqual(d2[key], d1[key]))
          return false;
      }
      return true;
    }

    private static bool AreCollectionsEqual(ICollection c1, ICollection c2)
    {
      if (c1.Count != c2.Count)
        return false;
      IEnumerator enumerator1 = c1.GetEnumerator();
      IEnumerator enumerator2 = c2.GetEnumerator();
      while (enumerator1.MoveNext() && enumerator2.MoveNext())
      {
        if (!Helper.AreEqual(enumerator1.Current, enumerator2.Current))
          return false;
      }
      return true;
    }

    public static bool DictEquals<K, V>(Dictionary<K, V> d1, Dictionary<K, V> d2)
    {
      if (d1 == null && d2 == null)
        return true;
      if (d1 == null || d2 == null || d1.Count != d2.Count)
        return false;
      foreach (K key in d1.Keys)
      {
        if (!d2.ContainsKey(key) || !Helper.EqualOrEquallyNull((object) d2[key], (object) d1[key]))
          return false;
      }
      return true;
    }

    internal static bool EqualOrEquallyNull(object o1, object o2)
    {
      if (o1 != null)
        return o1.Equals(o2);
      return o2 == null;
    }

    internal static string[] RefListToStringArray<T>(List<XenRef<T>> opaqueRefs) where T : XenObject<T>
    {
      string[] strArray = new string[opaqueRefs.Count];
      int num = 0;
      foreach (XenRef<T> xenRef in opaqueRefs)
        strArray[num++] = xenRef.opaque_ref;
      return strArray;
    }

    public static bool IsNullOrEmptyOpaqueRef(string opaqueRef)
    {
      if (!string.IsNullOrEmpty(opaqueRef))
        return string.Compare(opaqueRef, "OpaqueRef:NULL", true) == 0;
      return true;
    }

    internal static string[] ObjectListToStringArray<T>(List<T> list)
    {
      string[] strArray = new string[list.Count];
      int num = 0;
      foreach (T obj in list)
        strArray[num++] = obj.ToString();
      return strArray;
    }

    internal static List<T> StringArrayToEnumList<T>(string[] input)
    {
      List<T> list = new List<T>();
      foreach (string str in input)
      {
        try
        {
          list.Add((T) Enum.Parse(typeof (T), str));
        }
        catch (ArgumentException ex)
        {
        }
      }
      return list;
    }

    internal static List<T> ObjectArrayToEnumList<T>(object[] input)
    {
      List<T> list = new List<T>();
      foreach (object obj in input)
      {
        try
        {
          list.Add((T) Enum.Parse(typeof (T), obj.ToString()));
        }
        catch (ArgumentException ex)
        {
        }
      }
      return list;
    }

    internal static List<Message> Proxy_MessageArrayToMessageList(Proxy_Message[] input)
    {
      List<Message> list = new List<Message>();
      foreach (Proxy_Message proxy in input)
        list.Add(new Message(proxy));
      return list;
    }

    internal static List<Data_source> Proxy_Data_sourceArrayToData_sourceList(Proxy_Data_source[] input)
    {
      List<Data_source> list = new List<Data_source>();
      foreach (Proxy_Data_source proxy in input)
        list.Add(new Data_source(proxy));
      return list;
    }

    internal static object EnumParseDefault(Type t, string s)
    {
      try
      {
        return Enum.Parse(t, s == null ? (string) null : s.Replace('-', '_'));
      }
      catch (ArgumentException ex1)
      {
        try
        {
          return Enum.Parse(t, "unknown");
        }
        catch (ArgumentException ex2)
        {
          try
          {
            return Enum.Parse(t, "Unknown");
          }
          catch (ArgumentException ex3)
          {
            return (object) 0;
          }
        }
      }
    }
  }
}
