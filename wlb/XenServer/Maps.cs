// Decompiled with JetBrains decompiler
// Type: XenAPI.Maps
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace XenAPI
{
  internal class Maps
  {
    internal static Dictionary<string, string> convert_from_proxy_string_string(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (hashtable != null)
      {
        foreach (string str1 in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str1;
            string str2 = hashtable[(object) str1] == null ? (string) null : (string) hashtable[(object) str1];
            dictionary[index] = str2;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_string(Dictionary<string, string> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = table[index] != null ? table[index] : "";
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, host_allowed_operations> convert_from_proxy_string_host_allowed_operations(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, host_allowed_operations> dictionary = new Dictionary<string, host_allowed_operations>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            host_allowed_operations allowedOperations = hashtable[(object) str] == null ? host_allowed_operations.provision : (host_allowed_operations) Helper.EnumParseDefault(typeof (host_allowed_operations), (string) hashtable[(object) str]);
            dictionary[index] = allowedOperations;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_host_allowed_operations(Dictionary<string, host_allowed_operations> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = host_allowed_operations_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, network_operations> convert_from_proxy_string_network_operations(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, network_operations> dictionary = new Dictionary<string, network_operations>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            network_operations networkOperations = hashtable[(object) str] == null ? network_operations.attaching : (network_operations) Helper.EnumParseDefault(typeof (network_operations), (string) hashtable[(object) str]);
            dictionary[index] = networkOperations;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_network_operations(Dictionary<string, network_operations> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = network_operations_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, storage_operations> convert_from_proxy_string_storage_operations(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, storage_operations> dictionary = new Dictionary<string, storage_operations>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            storage_operations storageOperations = hashtable[(object) str] == null ? storage_operations.scan : (storage_operations) Helper.EnumParseDefault(typeof (storage_operations), (string) hashtable[(object) str]);
            dictionary[index] = storageOperations;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_storage_operations(Dictionary<string, storage_operations> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = storage_operations_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, task_allowed_operations> convert_from_proxy_string_task_allowed_operations(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, task_allowed_operations> dictionary = new Dictionary<string, task_allowed_operations>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            task_allowed_operations allowedOperations = hashtable[(object) str] == null ? task_allowed_operations.cancel : (task_allowed_operations) Helper.EnumParseDefault(typeof (task_allowed_operations), (string) hashtable[(object) str]);
            dictionary[index] = allowedOperations;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_task_allowed_operations(Dictionary<string, task_allowed_operations> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = task_allowed_operations_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, vbd_operations> convert_from_proxy_string_vbd_operations(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, vbd_operations> dictionary = new Dictionary<string, vbd_operations>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            vbd_operations vbdOperations = hashtable[(object) str] == null ? vbd_operations.attach : (vbd_operations) Helper.EnumParseDefault(typeof (vbd_operations), (string) hashtable[(object) str]);
            dictionary[index] = vbdOperations;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_vbd_operations(Dictionary<string, vbd_operations> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = vbd_operations_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, vdi_operations> convert_from_proxy_string_vdi_operations(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, vdi_operations> dictionary = new Dictionary<string, vdi_operations>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            vdi_operations vdiOperations = hashtable[(object) str] == null ? vdi_operations.scan : (vdi_operations) Helper.EnumParseDefault(typeof (vdi_operations), (string) hashtable[(object) str]);
            dictionary[index] = vdiOperations;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_vdi_operations(Dictionary<string, vdi_operations> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = vdi_operations_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, vif_operations> convert_from_proxy_string_vif_operations(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, vif_operations> dictionary = new Dictionary<string, vif_operations>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            vif_operations vifOperations = hashtable[(object) str] == null ? vif_operations.attach : (vif_operations) Helper.EnumParseDefault(typeof (vif_operations), (string) hashtable[(object) str]);
            dictionary[index] = vifOperations;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_vif_operations(Dictionary<string, vif_operations> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = vif_operations_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, vm_appliance_operation> convert_from_proxy_string_vm_appliance_operation(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, vm_appliance_operation> dictionary = new Dictionary<string, vm_appliance_operation>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            vm_appliance_operation applianceOperation = hashtable[(object) str] == null ? vm_appliance_operation.start : (vm_appliance_operation) Helper.EnumParseDefault(typeof (vm_appliance_operation), (string) hashtable[(object) str]);
            dictionary[index] = applianceOperation;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_vm_appliance_operation(Dictionary<string, vm_appliance_operation> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = vm_appliance_operation_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, vm_operations> convert_from_proxy_string_vm_operations(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, vm_operations> dictionary = new Dictionary<string, vm_operations>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            vm_operations vmOperations = hashtable[(object) str] == null ? vm_operations.snapshot : (vm_operations) Helper.EnumParseDefault(typeof (vm_operations), (string) hashtable[(object) str]);
            dictionary[index] = vmOperations;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_vm_operations(Dictionary<string, vm_operations> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = vm_operations_helper.ToString(table[index]);
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<string, XenRef<Blob>> convert_from_proxy_string_XenRefBlob(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<string, XenRef<Blob>> dictionary = new Dictionary<string, XenRef<Blob>>();
      if (hashtable != null)
      {
        foreach (string str in (IEnumerable) hashtable.Keys)
        {
          try
          {
            string index = str;
            XenRef<Blob> xenRef = hashtable[(object) str] == null ? (XenRef<Blob>) null : XenRef<Blob>.Create(hashtable[(object) str]);
            dictionary[index] = xenRef;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_string_XenRefBlob(Dictionary<string, XenRef<Blob>> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (string index in table.Keys)
        {
          try
          {
            string str1 = index ?? "";
            string str2 = table[index] != null ? (string) table[index] : "";
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<long, long> convert_from_proxy_long_long(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<long, long> dictionary = new Dictionary<long, long>();
      if (hashtable != null)
      {
        foreach (string s in (IEnumerable) hashtable.Keys)
        {
          try
          {
            long index = long.Parse(s);
            long num = hashtable[(object) s] == null ? 0L : long.Parse((string) hashtable[(object) s]);
            dictionary[index] = num;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_long_long(Dictionary<long, long> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (long index in table.Keys)
        {
          try
          {
            string str1 = index.ToString();
            string str2 = table[index].ToString();
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<long, double> convert_from_proxy_long_double(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<long, double> dictionary = new Dictionary<long, double>();
      if (hashtable != null)
      {
        foreach (string s in (IEnumerable) hashtable.Keys)
        {
          try
          {
            long index = long.Parse(s);
            double num = Convert.ToDouble(hashtable[(object) s]);
            dictionary[index] = num;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_long_double(Dictionary<long, double> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (long index in table.Keys)
        {
          try
          {
            string str = index.ToString();
            double num = table[index];
            xmlRpcStruct[(object) str] = (object) num;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<long, string[]> convert_from_proxy_long_string_array(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<long, string[]> dictionary = new Dictionary<long, string[]>();
      if (hashtable != null)
      {
        foreach (string s in (IEnumerable) hashtable.Keys)
        {
          try
          {
            long index = long.Parse(s);
            string[] strArray = hashtable[(object) s] == null ? new string[0] : Array.ConvertAll<object, string>((object[]) hashtable[(object) s], new Converter<object, string>(Convert.ToString));
            dictionary[index] = strArray;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_long_string_array(Dictionary<long, string[]> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (long index in table.Keys)
        {
          try
          {
            string str = index.ToString();
            string[] strArray = table[index];
            xmlRpcStruct[(object) str] = (object) strArray;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<vm_operations, string> convert_from_proxy_vm_operations_string(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<vm_operations, string> dictionary = new Dictionary<vm_operations, string>();
      if (hashtable != null)
      {
        foreach (string s in (IEnumerable) hashtable.Keys)
        {
          try
          {
            vm_operations index = (vm_operations) Helper.EnumParseDefault(typeof (vm_operations), s);
            string str = hashtable[(object) s] == null ? (string) null : (string) hashtable[(object) s];
            dictionary[index] = str;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_vm_operations_string(Dictionary<vm_operations, string> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (vm_operations x in table.Keys)
        {
          try
          {
            string str1 = vm_operations_helper.ToString(x);
            string str2 = table[x] != null ? table[x] : "";
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<XenRef<VDI>, XenRef<SR>> convert_from_proxy_XenRefVDI_XenRefSR(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<XenRef<VDI>, XenRef<SR>> dictionary = new Dictionary<XenRef<VDI>, XenRef<SR>>();
      if (hashtable != null)
      {
        foreach (string opaqueRef in (IEnumerable) hashtable.Keys)
        {
          try
          {
            XenRef<VDI> index = XenRef<VDI>.Create(opaqueRef);
            XenRef<SR> xenRef = hashtable[(object) opaqueRef] == null ? (XenRef<SR>) null : XenRef<SR>.Create(hashtable[(object) opaqueRef]);
            dictionary[index] = xenRef;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_XenRefVDI_XenRefSR(Dictionary<XenRef<VDI>, XenRef<SR>> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (XenRef<VDI> index in table.Keys)
        {
          try
          {
            string str1 = index != null ? (string) index : "";
            string str2 = table[index] != null ? (string) table[index] : "";
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<XenRef<VIF>, XenRef<Network>> convert_from_proxy_XenRefVIF_XenRefNetwork(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<XenRef<VIF>, XenRef<Network>> dictionary = new Dictionary<XenRef<VIF>, XenRef<Network>>();
      if (hashtable != null)
      {
        foreach (string opaqueRef in (IEnumerable) hashtable.Keys)
        {
          try
          {
            XenRef<VIF> index = XenRef<VIF>.Create(opaqueRef);
            XenRef<Network> xenRef = hashtable[(object) opaqueRef] == null ? (XenRef<Network>) null : XenRef<Network>.Create(hashtable[(object) opaqueRef]);
            dictionary[index] = xenRef;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_XenRefVIF_XenRefNetwork(Dictionary<XenRef<VIF>, XenRef<Network>> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (XenRef<VIF> index in table.Keys)
        {
          try
          {
            string str1 = index != null ? (string) index : "";
            string str2 = table[index] != null ? (string) table[index] : "";
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<XenRef<VM>, string> convert_from_proxy_XenRefVM_string(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<XenRef<VM>, string> dictionary = new Dictionary<XenRef<VM>, string>();
      if (hashtable != null)
      {
        foreach (string opaqueRef in (IEnumerable) hashtable.Keys)
        {
          try
          {
            XenRef<VM> index = XenRef<VM>.Create(opaqueRef);
            string str = hashtable[(object) opaqueRef] == null ? (string) null : (string) hashtable[(object) opaqueRef];
            dictionary[index] = str;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_XenRefVM_string(Dictionary<XenRef<VM>, string> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (XenRef<VM> index in table.Keys)
        {
          try
          {
            string str1 = index != null ? (string) index : "";
            string str2 = table[index] != null ? table[index] : "";
            xmlRpcStruct[(object) str1] = (object) str2;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<XenRef<VM>, string[]> convert_from_proxy_XenRefVM_string_array(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<XenRef<VM>, string[]> dictionary = new Dictionary<XenRef<VM>, string[]>();
      if (hashtable != null)
      {
        foreach (string opaqueRef in (IEnumerable) hashtable.Keys)
        {
          try
          {
            XenRef<VM> index = XenRef<VM>.Create(opaqueRef);
            string[] strArray = hashtable[(object) opaqueRef] == null ? new string[0] : Array.ConvertAll<object, string>((object[]) hashtable[(object) opaqueRef], new Converter<object, string>(Convert.ToString));
            dictionary[index] = strArray;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_XenRefVM_string_array(Dictionary<XenRef<VM>, string[]> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (XenRef<VM> index in table.Keys)
        {
          try
          {
            string str = index != null ? (string) index : "";
            string[] strArray = table[index];
            xmlRpcStruct[(object) str] = (object) strArray;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<XenRef<VM>, Dictionary<string, string>> convert_from_proxy_XenRefVM_Dictionary_string_string(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<XenRef<VM>, Dictionary<string, string>> dictionary1 = new Dictionary<XenRef<VM>, Dictionary<string, string>>();
      if (hashtable != null)
      {
        foreach (string opaqueRef in (IEnumerable) hashtable.Keys)
        {
          try
          {
            XenRef<VM> index = XenRef<VM>.Create(opaqueRef);
            Dictionary<string, string> dictionary2 = hashtable[(object) opaqueRef] == null ? (Dictionary<string, string>) null : Maps.convert_from_proxy_string_string(hashtable[(object) opaqueRef]);
            dictionary1[index] = dictionary2;
          }
          catch
          {
          }
        }
      }
      return dictionary1;
    }

    internal static Hashtable convert_to_proxy_XenRefVM_Dictionary_string_string(Dictionary<XenRef<VM>, Dictionary<string, string>> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (XenRef<VM> index in table.Keys)
        {
          try
          {
            string str = index != null ? (string) index : "";
            object obj = (object) Maps.convert_to_proxy_string_string(table[index]);
            xmlRpcStruct[(object) str] = obj;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }

    internal static Dictionary<XenRef<Host>, string[]> convert_from_proxy_XenRefHost_string_array(object o)
    {
      Hashtable hashtable = (Hashtable) o;
      Dictionary<XenRef<Host>, string[]> dictionary = new Dictionary<XenRef<Host>, string[]>();
      if (hashtable != null)
      {
        foreach (string opaqueRef in (IEnumerable) hashtable.Keys)
        {
          try
          {
            XenRef<Host> index = XenRef<Host>.Create(opaqueRef);
            string[] strArray = hashtable[(object) opaqueRef] == null ? new string[0] : Array.ConvertAll<object, string>((object[]) hashtable[(object) opaqueRef], new Converter<object, string>(Convert.ToString));
            dictionary[index] = strArray;
          }
          catch
          {
          }
        }
      }
      return dictionary;
    }

    internal static Hashtable convert_to_proxy_XenRefHost_string_array(Dictionary<XenRef<Host>, string[]> table)
    {
      XmlRpcStruct xmlRpcStruct = new XmlRpcStruct();
      if (table != null)
      {
        foreach (XenRef<Host> index in table.Keys)
        {
          try
          {
            string str = index != null ? (string) index : "";
            string[] strArray = table[index];
            xmlRpcStruct[(object) str] = (object) strArray;
          }
          catch
          {
          }
        }
      }
      return (Hashtable) xmlRpcStruct;
    }
  }
}
