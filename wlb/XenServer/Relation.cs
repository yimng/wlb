// Decompiled with JetBrains decompiler
// Type: XenAPI.Relation
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections.Generic;

namespace XenAPI
{
  public class Relation
  {
    public readonly string field;
    public readonly string manyType;
    public readonly string manyField;

    public Relation(string field, string manyType, string manyField)
    {
      this.field = field;
      this.manyField = manyField;
      this.manyType = manyType;
    }

    public static Dictionary<Type, Relation[]> GetRelations()
    {
      return new Dictionary<Type, Relation[]>()
      {
        {
          typeof (Proxy_Role),
          new Relation[1]
          {
            new Relation("subroles", "role", "subroles")
          }
        },
        {
          typeof (Proxy_Network),
          new Relation[2]
          {
            new Relation("PIFs", "PIF", "network"),
            new Relation("VIFs", "VIF", "network")
          }
        },
        {
          typeof (Proxy_VMPP),
          new Relation[1]
          {
            new Relation("VMs", "VM", "protection_policy")
          }
        },
        {
          typeof (Proxy_VDI),
          new Relation[3]
          {
            new Relation("crash_dumps", "crashdump", "VDI"),
            new Relation("VBDs", "VBD", "VDI"),
            new Relation("snapshots", "VDI", "snapshot_of")
          }
        },
        {
          typeof (Proxy_VM),
          new Relation[9]
          {
            new Relation("attached_PCIs", "PCI", "attached_VMs"),
            new Relation("VGPUs", "VGPU", "VM"),
            new Relation("consoles", "console", "VM"),
            new Relation("VTPMs", "VTPM", "VM"),
            new Relation("VIFs", "VIF", "VM"),
            new Relation("crash_dumps", "crashdump", "VM"),
            new Relation("VBDs", "VBD", "VM"),
            new Relation("children", "VM", "parent"),
            new Relation("snapshots", "VM", "snapshot_of")
          }
        },
        {
          typeof (Proxy_DR_task),
          new Relation[1]
          {
            new Relation("introduced_SRs", "SR", "introduced_by")
          }
        },
        {
          typeof (Proxy_VM_appliance),
          new Relation[1]
          {
            new Relation("VMs", "VM", "appliance")
          }
        },
        {
          typeof (Proxy_Task),
          new Relation[1]
          {
            new Relation("subtasks", "task", "subtask_of")
          }
        },
        {
          typeof (Proxy_GPU_group),
          new Relation[2]
          {
            new Relation("VGPUs", "VGPU", "GPU_group"),
            new Relation("PGPUs", "PGPU", "GPU_group")
          }
        },
        {
          typeof (Proxy_Bond),
          new Relation[1]
          {
            new Relation("slaves", "PIF", "bond_slave_of")
          }
        },
        {
          typeof (Proxy_Pool),
          new Relation[1]
          {
            new Relation("metadata_VDIs", "VDI", "metadata_of_pool")
          }
        },
        {
          typeof (Proxy_PIF),
          new Relation[4]
          {
            new Relation("tunnel_transport_PIF_of", "tunnel", "transport_PIF"),
            new Relation("tunnel_access_PIF_of", "tunnel", "access_PIF"),
            new Relation("VLAN_slave_of", "VLAN", "tagged_PIF"),
            new Relation("bond_master_of", "Bond", "master")
          }
        },
        {
          typeof (Proxy_Subject),
          new Relation[1]
          {
            new Relation("roles", "subject", "roles")
          }
        },
        {
          typeof (Proxy_Host),
          new Relation[8]
          {
            new Relation("PGPUs", "PGPU", "host"),
            new Relation("PCIs", "PCI", "host"),
            new Relation("patches", "host_patch", "host"),
            new Relation("crashdumps", "host_crashdump", "host"),
            new Relation("host_CPUs", "host_cpu", "host"),
            new Relation("resident_VMs", "VM", "resident_on"),
            new Relation("PIFs", "PIF", "host"),
            new Relation("PBDs", "PBD", "host")
          }
        },
        {
          typeof (Proxy_Session),
          new Relation[1]
          {
            new Relation("tasks", "task", "session")
          }
        },
        {
          typeof (Proxy_Pool_patch),
          new Relation[1]
          {
            new Relation("host_patches", "host_patch", "pool_patch")
          }
        },
        {
          typeof (Proxy_SR),
          new Relation[2]
          {
            new Relation("VDIs", "VDI", "SR"),
            new Relation("PBDs", "PBD", "SR")
          }
        }
      };
    }
  }
}
