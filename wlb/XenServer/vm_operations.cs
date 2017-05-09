// Decompiled with JetBrains decompiler
// Type: XenAPI.vm_operations
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

namespace XenAPI
{
  public enum vm_operations
  {
    snapshot,
    clone,
    copy,
    create_template,
    revert,
    checkpoint,
    snapshot_with_quiesce,
    provision,
    start,
    start_on,
    pause,
    unpause,
    clean_shutdown,
    clean_reboot,
    hard_shutdown,
    power_state_reset,
    hard_reboot,
    suspend,
    csvm,
    resume,
    resume_on,
    pool_migrate,
    migrate_send,
    get_boot_record,
    send_sysrq,
    send_trigger,
    query_services,
    changing_memory_live,
    awaiting_memory_live,
    changing_dynamic_range,
    changing_static_range,
    changing_memory_limits,
    changing_shadow_memory,
    changing_shadow_memory_live,
    changing_VCPUs,
    changing_VCPUs_live,
    assert_operation_valid,
    data_source_op,
    update_allowed_operations,
    make_into_template,
    import,
    export,
    metadata_export,
    reverting,
    destroy,
    unknown,
  }
}
