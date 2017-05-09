﻿// Decompiled with JetBrains decompiler
// Type: XenAPI.Proxy
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using CookComputing.XmlRpc;
using System;

namespace XenAPI
{
  public interface Proxy : IXmlRpcProxy
  {
    [XmlRpcMethod("session.login_with_password")]
    Response<string> session_login_with_password(string username, string password);

    [XmlRpcMethod("event.get_record")]
    Response<Proxy_Event> event_get_record(string session, string _event);

    [XmlRpcMethod("event.get_by_uuid")]
    Response<string> event_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("event.get_id")]
    Response<string> event_get_id(string session, string _event);

    [XmlRpcMethod("event.set_id")]
    Response<string> event_set_id(string session, string _event, string _id);

    [XmlRpcMethod("event.register")]
    Response<string> event_register(string session, string[] _classes);

    [XmlRpcMethod("event.unregister")]
    Response<string> event_unregister(string session, string[] _classes);

    [XmlRpcMethod("event.next")]
    Response<Proxy_Event[]> event_next(string session);

    [XmlRpcMethod("event.from")]
    Response<Events> event_from(string session, string[] _classes, string _token, double _timeout);

    [XmlRpcMethod("SR.create")]
    Response<string> sr_create(string session, string _host, object _device_config, string _physical_size, string _name_label, string _name_description, string _type, string _content_type, bool _shared);

    [XmlRpcMethod("Async.SR.create")]
    Response<string> async_sr_create(string session, string _host, object _device_config, string _physical_size, string _name_label, string _name_description, string _type, string _content_type, bool _shared);

    [XmlRpcMethod("SR.introduce")]
    Response<string> sr_introduce(string session, string _uuid, string _name_label, string _name_description, string _type, string _content_type, bool _shared);

    [XmlRpcMethod("Async.SR.introduce")]
    Response<string> async_sr_introduce(string session, string _uuid, string _name_label, string _name_description, string _type, string _content_type, bool _shared);

    [XmlRpcMethod("SR.make")]
    Response<string> sr_make(string session, string _host, object _device_config, string _physical_size, string _name_label, string _name_description, string _type, string _content_type);

    [XmlRpcMethod("Async.SR.make")]
    Response<string> async_sr_make(string session, string _host, object _device_config, string _physical_size, string _name_label, string _name_description, string _type, string _content_type);

    [XmlRpcMethod("VDI.snapshot")]
    Response<string> vdi_snapshot(string session, string _vdi);

    [XmlRpcMethod("Async.VDI.snapshot")]
    Response<string> async_vdi_snapshot(string session, string _vdi);

    [XmlRpcMethod("VDI.clone")]
    Response<string> vdi_clone(string session, string _vdi);

    [XmlRpcMethod("Async.VDI.clone")]
    Response<string> async_vdi_clone(string session, string _vdi);

    [XmlRpcMethod("Bond.create")]
    Response<string> bond_create(string session, string _network, string[] _members, string _mac);

    [XmlRpcMethod("Async.Bond.create")]
    Response<string> async_bond_create(string session, string _network, string[] _members, string _mac);

    [XmlRpcMethod("Bond.create")]
    Response<string> bond_create(string session, string _network, string[] _members, string _mac, string _mode);

    [XmlRpcMethod("Async.Bond.create")]
    Response<string> async_bond_create(string session, string _network, string[] _members, string _mac, string _mode);

    [XmlRpcMethod("pool.create_new_blob")]
    Response<string> pool_create_new_blob(string session, string _pool, string _name, string _mime_type);

    [XmlRpcMethod("Async.pool.create_new_blob")]
    Response<string> async_pool_create_new_blob(string session, string _pool, string _name, string _mime_type);

    [XmlRpcMethod("VM.create_new_blob")]
    Response<string> vm_create_new_blob(string session, string _vm, string _name, string _mime_type);

    [XmlRpcMethod("Async.VM.create_new_blob")]
    Response<string> async_vm_create_new_blob(string session, string _vm, string _name, string _mime_type);

    [XmlRpcMethod("host.create_new_blob")]
    Response<string> host_create_new_blob(string session, string _host, string _name, string _mime_type);

    [XmlRpcMethod("Async.host.create_new_blob")]
    Response<string> async_host_create_new_blob(string session, string _host, string _name, string _mime_type);

    [XmlRpcMethod("network.create_new_blob")]
    Response<string> network_create_new_blob(string session, string _network, string _name, string _mime_type);

    [XmlRpcMethod("Async.network.create_new_blob")]
    Response<string> async_network_create_new_blob(string session, string _network, string _name, string _mime_type);

    [XmlRpcMethod("PIF.db_introduce")]
    Response<string> pif_db_introduce(string session, string _device, string _network, string _host, string _mac, string _mtu, string _vlan, bool _physical, string _ip_configuration_mode, string _ip, string _netmask, string _gateway, string _dns, string _bond_slave_of, string _vlan_master_of, bool _management, object _other_config, bool _disallow_unplug);

    [XmlRpcMethod("Async.PIF.db_introduce")]
    Response<string> async_pif_db_introduce(string session, string _device, string _network, string _host, string _mac, string _mtu, string _vlan, bool _physical, string _ip_configuration_mode, string _ip, string _netmask, string _gateway, string _dns, string _bond_slave_of, string _vlan_master_of, bool _management, object _other_config, bool _disallow_unplug);

    [XmlRpcMethod("SR.create_new_blob")]
    Response<string> sr_create_new_blob(string session, string _sr, string _name, string _mime_type);

    [XmlRpcMethod("Async.SR.create_new_blob")]
    Response<string> async_sr_create_new_blob(string session, string _sr, string _name, string _mime_type);

    [XmlRpcMethod("VDI.introduce")]
    Response<string> vdi_introduce(string session, string _uuid, string _name_label, string _name_description, string _sr, string _type, bool _sharable, bool _read_only, object _other_config, string _location, object _xenstore_data, object _sm_config);

    [XmlRpcMethod("Async.VDI.introduce")]
    Response<string> async_vdi_introduce(string session, string _uuid, string _name_label, string _name_description, string _sr, string _type, bool _sharable, bool _read_only, object _other_config, string _location, object _xenstore_data, object _sm_config);

    [XmlRpcMethod("VDI.db_introduce")]
    Response<string> vdi_db_introduce(string session, string _uuid, string _name_label, string _name_description, string _sr, string _type, bool _sharable, bool _read_only, object _other_config, string _location, object _xenstore_data, object _sm_config);

    [XmlRpcMethod("Async.VDI.db_introduce")]
    Response<string> async_vdi_db_introduce(string session, string _uuid, string _name_label, string _name_description, string _sr, string _type, bool _sharable, bool _read_only, object _other_config, string _location, object _xenstore_data, object _sm_config);

    [XmlRpcMethod("blob.create")]
    Response<string> blob_create(string session, string _mime_type);

    [XmlRpcMethod("session.get_record")]
    Response<Proxy_Session> session_get_record(string session, string _session);

    [XmlRpcMethod("session.get_by_uuid")]
    Response<string> session_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("session.get_uuid")]
    Response<string> session_get_uuid(string session, string _session);

    [XmlRpcMethod("session.get_this_host")]
    Response<string> session_get_this_host(string session, string _session);

    [XmlRpcMethod("session.get_this_user")]
    Response<string> session_get_this_user(string session, string _session);

    [XmlRpcMethod("session.get_last_active")]
    Response<DateTime> session_get_last_active(string session, string _session);

    [XmlRpcMethod("session.get_pool")]
    Response<bool> session_get_pool(string session, string _session);

    [XmlRpcMethod("session.get_other_config")]
    Response<object> session_get_other_config(string session, string _session);

    [XmlRpcMethod("session.get_is_local_superuser")]
    Response<bool> session_get_is_local_superuser(string session, string _session);

    [XmlRpcMethod("session.get_subject")]
    Response<string> session_get_subject(string session, string _session);

    [XmlRpcMethod("session.get_validation_time")]
    Response<DateTime> session_get_validation_time(string session, string _session);

    [XmlRpcMethod("session.get_auth_user_sid")]
    Response<string> session_get_auth_user_sid(string session, string _session);

    [XmlRpcMethod("session.get_auth_user_name")]
    Response<string> session_get_auth_user_name(string session, string _session);

    [XmlRpcMethod("session.get_rbac_permissions")]
    Response<string[]> session_get_rbac_permissions(string session, string _session);

    [XmlRpcMethod("session.get_tasks")]
    Response<string[]> session_get_tasks(string session, string _session);

    [XmlRpcMethod("session.get_parent")]
    Response<string> session_get_parent(string session, string _session);

    [XmlRpcMethod("session.set_other_config")]
    Response<string> session_set_other_config(string session, string _session, object _other_config);

    [XmlRpcMethod("session.add_to_other_config")]
    Response<string> session_add_to_other_config(string session, string _session, string _key, string _value);

    [XmlRpcMethod("session.remove_from_other_config")]
    Response<string> session_remove_from_other_config(string session, string _session, string _key);

    [XmlRpcMethod("session.login_with_password")]
    Response<string> session_login_with_password(string _uname, string _pwd, string _version);

    [XmlRpcMethod("session.logout")]
    Response<string> session_logout(string session);

    [XmlRpcMethod("session.change_password")]
    Response<string> session_change_password(string session, string _old_pwd, string _new_pwd);

    [XmlRpcMethod("session.slave_local_login_with_password")]
    Response<string> session_slave_local_login_with_password(string _uname, string _pwd);

    [XmlRpcMethod("session.local_logout")]
    Response<string> session_local_logout(string session);

    [XmlRpcMethod("session.get_all_subject_identifiers")]
    Response<string[]> session_get_all_subject_identifiers(string session);

    [XmlRpcMethod("Async.session.get_all_subject_identifiers")]
    Response<string> async_session_get_all_subject_identifiers(string session);

    [XmlRpcMethod("session.logout_subject_identifier")]
    Response<string> session_logout_subject_identifier(string session, string _subject_identifier);

    [XmlRpcMethod("Async.session.logout_subject_identifier")]
    Response<string> async_session_logout_subject_identifier(string session, string _subject_identifier);

    [XmlRpcMethod("session.get_all_records")]
    Response<object> session_get_all_records(string session);

    [XmlRpcMethod("auth.get_subject_identifier")]
    Response<string> auth_get_subject_identifier(string session, string _subject_name);

    [XmlRpcMethod("auth.get_subject_information_from_identifier")]
    Response<object> auth_get_subject_information_from_identifier(string session, string _subject_identifier);

    [XmlRpcMethod("auth.get_group_membership")]
    Response<string[]> auth_get_group_membership(string session, string _subject_identifier);

    [XmlRpcMethod("auth.get_all_records")]
    Response<object> auth_get_all_records(string session);

    [XmlRpcMethod("subject.get_record")]
    Response<Proxy_Subject> subject_get_record(string session, string _subject);

    [XmlRpcMethod("subject.get_by_uuid")]
    Response<string> subject_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("subject.create")]
    Response<string> subject_create(string session, Proxy_Subject _record);

    [XmlRpcMethod("Async.subject.create")]
    Response<string> async_subject_create(string session, Proxy_Subject _record);

    [XmlRpcMethod("subject.destroy")]
    Response<string> subject_destroy(string session, string _subject);

    [XmlRpcMethod("Async.subject.destroy")]
    Response<string> async_subject_destroy(string session, string _subject);

    [XmlRpcMethod("subject.get_uuid")]
    Response<string> subject_get_uuid(string session, string _subject);

    [XmlRpcMethod("subject.get_subject_identifier")]
    Response<string> subject_get_subject_identifier(string session, string _subject);

    [XmlRpcMethod("subject.get_other_config")]
    Response<object> subject_get_other_config(string session, string _subject);

    [XmlRpcMethod("subject.get_roles")]
    Response<string[]> subject_get_roles(string session, string _subject);

    [XmlRpcMethod("subject.add_to_roles")]
    Response<string> subject_add_to_roles(string session, string _self, string _role);

    [XmlRpcMethod("subject.remove_from_roles")]
    Response<string> subject_remove_from_roles(string session, string _self, string _role);

    [XmlRpcMethod("subject.get_permissions_name_label")]
    Response<string[]> subject_get_permissions_name_label(string session, string _self);

    [XmlRpcMethod("subject.get_all")]
    Response<string[]> subject_get_all(string session);

    [XmlRpcMethod("subject.get_all_records")]
    Response<object> subject_get_all_records(string session);

    [XmlRpcMethod("role.get_record")]
    Response<Proxy_Role> role_get_record(string session, string _role);

    [XmlRpcMethod("role.get_by_uuid")]
    Response<string> role_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("role.get_by_name_label")]
    Response<string[]> role_get_by_name_label(string session, string _label);

    [XmlRpcMethod("role.get_uuid")]
    Response<string> role_get_uuid(string session, string _role);

    [XmlRpcMethod("role.get_name_label")]
    Response<string> role_get_name_label(string session, string _role);

    [XmlRpcMethod("role.get_name_description")]
    Response<string> role_get_name_description(string session, string _role);

    [XmlRpcMethod("role.get_subroles")]
    Response<string[]> role_get_subroles(string session, string _role);

    [XmlRpcMethod("role.get_permissions")]
    Response<string[]> role_get_permissions(string session, string _self);

    [XmlRpcMethod("role.get_permissions_name_label")]
    Response<string[]> role_get_permissions_name_label(string session, string _self);

    [XmlRpcMethod("role.get_by_permission")]
    Response<string[]> role_get_by_permission(string session, string _permission);

    [XmlRpcMethod("role.get_by_permission_name_label")]
    Response<string[]> role_get_by_permission_name_label(string session, string _label);

    [XmlRpcMethod("role.get_all")]
    Response<string[]> role_get_all(string session);

    [XmlRpcMethod("role.get_all_records")]
    Response<object> role_get_all_records(string session);

    [XmlRpcMethod("task.get_record")]
    Response<Proxy_Task> task_get_record(string session, string _task);

    [XmlRpcMethod("task.get_by_uuid")]
    Response<string> task_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("task.get_by_name_label")]
    Response<string[]> task_get_by_name_label(string session, string _label);

    [XmlRpcMethod("task.get_uuid")]
    Response<string> task_get_uuid(string session, string _task);

    [XmlRpcMethod("task.get_name_label")]
    Response<string> task_get_name_label(string session, string _task);

    [XmlRpcMethod("task.get_name_description")]
    Response<string> task_get_name_description(string session, string _task);

    [XmlRpcMethod("task.get_allowed_operations")]
    Response<string[]> task_get_allowed_operations(string session, string _task);

    [XmlRpcMethod("task.get_current_operations")]
    Response<object> task_get_current_operations(string session, string _task);

    [XmlRpcMethod("task.get_created")]
    Response<DateTime> task_get_created(string session, string _task);

    [XmlRpcMethod("task.get_finished")]
    Response<DateTime> task_get_finished(string session, string _task);

    [XmlRpcMethod("task.get_status")]
    Response<string> task_get_status(string session, string _task);

    [XmlRpcMethod("task.get_resident_on")]
    Response<string> task_get_resident_on(string session, string _task);

    [XmlRpcMethod("task.get_progress")]
    Response<double> task_get_progress(string session, string _task);

    [XmlRpcMethod("task.get_type")]
    Response<string> task_get_type(string session, string _task);

    [XmlRpcMethod("task.get_result")]
    Response<string> task_get_result(string session, string _task);

    [XmlRpcMethod("task.get_error_info")]
    Response<string[]> task_get_error_info(string session, string _task);

    [XmlRpcMethod("task.get_other_config")]
    Response<object> task_get_other_config(string session, string _task);

    [XmlRpcMethod("task.get_subtask_of")]
    Response<string> task_get_subtask_of(string session, string _task);

    [XmlRpcMethod("task.get_subtasks")]
    Response<string[]> task_get_subtasks(string session, string _task);

    [XmlRpcMethod("task.set_other_config")]
    Response<string> task_set_other_config(string session, string _task, object _other_config);

    [XmlRpcMethod("task.add_to_other_config")]
    Response<string> task_add_to_other_config(string session, string _task, string _key, string _value);

    [XmlRpcMethod("task.remove_from_other_config")]
    Response<string> task_remove_from_other_config(string session, string _task, string _key);

    [XmlRpcMethod("task.create")]
    Response<string> task_create(string session, string _label, string _description);

    [XmlRpcMethod("task.destroy")]
    Response<string> task_destroy(string session, string _self);

    [XmlRpcMethod("task.cancel")]
    Response<string> task_cancel(string session, string _task);

    [XmlRpcMethod("Async.task.cancel")]
    Response<string> async_task_cancel(string session, string _task);

    [XmlRpcMethod("task.get_all")]
    Response<string[]> task_get_all(string session);

    [XmlRpcMethod("task.get_all_records")]
    Response<object> task_get_all_records(string session);

    [XmlRpcMethod("pool.get_record")]
    Response<Proxy_Pool> pool_get_record(string session, string _pool);

    [XmlRpcMethod("pool.get_by_uuid")]
    Response<string> pool_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("pool.get_uuid")]
    Response<string> pool_get_uuid(string session, string _pool);

    [XmlRpcMethod("pool.get_name_label")]
    Response<string> pool_get_name_label(string session, string _pool);

    [XmlRpcMethod("pool.get_name_description")]
    Response<string> pool_get_name_description(string session, string _pool);

    [XmlRpcMethod("pool.get_master")]
    Response<string> pool_get_master(string session, string _pool);

    [XmlRpcMethod("pool.get_default_SR")]
    Response<string> pool_get_default_sr(string session, string _pool);

    [XmlRpcMethod("pool.get_suspend_image_SR")]
    Response<string> pool_get_suspend_image_sr(string session, string _pool);

    [XmlRpcMethod("pool.get_crash_dump_SR")]
    Response<string> pool_get_crash_dump_sr(string session, string _pool);

    [XmlRpcMethod("pool.get_other_config")]
    Response<object> pool_get_other_config(string session, string _pool);

    [XmlRpcMethod("pool.get_ha_enabled")]
    Response<bool> pool_get_ha_enabled(string session, string _pool);

    [XmlRpcMethod("pool.get_ha_configuration")]
    Response<object> pool_get_ha_configuration(string session, string _pool);

    [XmlRpcMethod("pool.get_ha_statefiles")]
    Response<string[]> pool_get_ha_statefiles(string session, string _pool);

    [XmlRpcMethod("pool.get_ha_host_failures_to_tolerate")]
    Response<string> pool_get_ha_host_failures_to_tolerate(string session, string _pool);

    [XmlRpcMethod("pool.get_ha_plan_exists_for")]
    Response<string> pool_get_ha_plan_exists_for(string session, string _pool);

    [XmlRpcMethod("pool.get_ha_allow_overcommit")]
    Response<bool> pool_get_ha_allow_overcommit(string session, string _pool);

    [XmlRpcMethod("pool.get_ha_overcommitted")]
    Response<bool> pool_get_ha_overcommitted(string session, string _pool);

    [XmlRpcMethod("pool.get_blobs")]
    Response<object> pool_get_blobs(string session, string _pool);

    [XmlRpcMethod("pool.get_tags")]
    Response<string[]> pool_get_tags(string session, string _pool);

    [XmlRpcMethod("pool.get_gui_config")]
    Response<object> pool_get_gui_config(string session, string _pool);

    [XmlRpcMethod("pool.get_wlb_url")]
    Response<string> pool_get_wlb_url(string session, string _pool);

    [XmlRpcMethod("pool.get_wlb_username")]
    Response<string> pool_get_wlb_username(string session, string _pool);

    [XmlRpcMethod("pool.get_wlb_enabled")]
    Response<bool> pool_get_wlb_enabled(string session, string _pool);

    [XmlRpcMethod("pool.get_wlb_verify_cert")]
    Response<bool> pool_get_wlb_verify_cert(string session, string _pool);

    [XmlRpcMethod("pool.get_redo_log_enabled")]
    Response<bool> pool_get_redo_log_enabled(string session, string _pool);

    [XmlRpcMethod("pool.get_redo_log_vdi")]
    Response<string> pool_get_redo_log_vdi(string session, string _pool);

    [XmlRpcMethod("pool.get_vswitch_controller")]
    Response<string> pool_get_vswitch_controller(string session, string _pool);

    [XmlRpcMethod("pool.get_restrictions")]
    Response<object> pool_get_restrictions(string session, string _pool);

    [XmlRpcMethod("pool.get_metadata_VDIs")]
    Response<string[]> pool_get_metadata_vdis(string session, string _pool);

    [XmlRpcMethod("pool.set_name_label")]
    Response<string> pool_set_name_label(string session, string _pool, string _name_label);

    [XmlRpcMethod("pool.set_name_description")]
    Response<string> pool_set_name_description(string session, string _pool, string _name_description);

    [XmlRpcMethod("pool.set_default_SR")]
    Response<string> pool_set_default_sr(string session, string _pool, string _default_sr);

    [XmlRpcMethod("pool.set_suspend_image_SR")]
    Response<string> pool_set_suspend_image_sr(string session, string _pool, string _suspend_image_sr);

    [XmlRpcMethod("pool.set_crash_dump_SR")]
    Response<string> pool_set_crash_dump_sr(string session, string _pool, string _crash_dump_sr);

    [XmlRpcMethod("pool.set_other_config")]
    Response<string> pool_set_other_config(string session, string _pool, object _other_config);

    [XmlRpcMethod("pool.add_to_other_config")]
    Response<string> pool_add_to_other_config(string session, string _pool, string _key, string _value);

    [XmlRpcMethod("pool.remove_from_other_config")]
    Response<string> pool_remove_from_other_config(string session, string _pool, string _key);

    [XmlRpcMethod("pool.set_ha_allow_overcommit")]
    Response<string> pool_set_ha_allow_overcommit(string session, string _pool, bool _ha_allow_overcommit);

    [XmlRpcMethod("pool.set_tags")]
    Response<string> pool_set_tags(string session, string _pool, string[] _tags);

    [XmlRpcMethod("pool.add_tags")]
    Response<string> pool_add_tags(string session, string _pool, string _value);

    [XmlRpcMethod("pool.remove_tags")]
    Response<string> pool_remove_tags(string session, string _pool, string _value);

    [XmlRpcMethod("pool.set_gui_config")]
    Response<string> pool_set_gui_config(string session, string _pool, object _gui_config);

    [XmlRpcMethod("pool.add_to_gui_config")]
    Response<string> pool_add_to_gui_config(string session, string _pool, string _key, string _value);

    [XmlRpcMethod("pool.remove_from_gui_config")]
    Response<string> pool_remove_from_gui_config(string session, string _pool, string _key);

    [XmlRpcMethod("pool.set_wlb_enabled")]
    Response<string> pool_set_wlb_enabled(string session, string _pool, bool _wlb_enabled);

    [XmlRpcMethod("pool.set_wlb_verify_cert")]
    Response<string> pool_set_wlb_verify_cert(string session, string _pool, bool _wlb_verify_cert);

    [XmlRpcMethod("pool.join")]
    Response<string> pool_join(string session, string _master_address, string _master_username, string _master_password);

    [XmlRpcMethod("Async.pool.join")]
    Response<string> async_pool_join(string session, string _master_address, string _master_username, string _master_password);

    [XmlRpcMethod("pool.join_force")]
    Response<string> pool_join_force(string session, string _master_address, string _master_username, string _master_password);

    [XmlRpcMethod("Async.pool.join_force")]
    Response<string> async_pool_join_force(string session, string _master_address, string _master_username, string _master_password);

    [XmlRpcMethod("pool.eject")]
    Response<string> pool_eject(string session, string _host);

    [XmlRpcMethod("Async.pool.eject")]
    Response<string> async_pool_eject(string session, string _host);

    [XmlRpcMethod("pool.emergency_transition_to_master")]
    Response<string> pool_emergency_transition_to_master(string session);

    [XmlRpcMethod("pool.emergency_reset_master")]
    Response<string> pool_emergency_reset_master(string session, string _master_address);

    [XmlRpcMethod("pool.recover_slaves")]
    Response<string[]> pool_recover_slaves(string session);

    [XmlRpcMethod("Async.pool.recover_slaves")]
    Response<string> async_pool_recover_slaves(string session);

    [XmlRpcMethod("pool.create_VLAN")]
    Response<string[]> pool_create_vlan(string session, string _device, string _network, string _vlan);

    [XmlRpcMethod("Async.pool.create_VLAN")]
    Response<string> async_pool_create_vlan(string session, string _device, string _network, string _vlan);

    [XmlRpcMethod("pool.create_VLAN_from_PIF")]
    Response<string[]> pool_create_vlan_from_pif(string session, string _pif, string _network, string _vlan);

    [XmlRpcMethod("Async.pool.create_VLAN_from_PIF")]
    Response<string> async_pool_create_vlan_from_pif(string session, string _pif, string _network, string _vlan);

    [XmlRpcMethod("pool.enable_ha")]
    Response<string> pool_enable_ha(string session, string[] _heartbeat_srs, object _configuration);

    [XmlRpcMethod("Async.pool.enable_ha")]
    Response<string> async_pool_enable_ha(string session, string[] _heartbeat_srs, object _configuration);

    [XmlRpcMethod("pool.disable_ha")]
    Response<string> pool_disable_ha(string session);

    [XmlRpcMethod("Async.pool.disable_ha")]
    Response<string> async_pool_disable_ha(string session);

    [XmlRpcMethod("pool.sync_database")]
    Response<string> pool_sync_database(string session);

    [XmlRpcMethod("Async.pool.sync_database")]
    Response<string> async_pool_sync_database(string session);

    [XmlRpcMethod("pool.designate_new_master")]
    Response<string> pool_designate_new_master(string session, string _host);

    [XmlRpcMethod("Async.pool.designate_new_master")]
    Response<string> async_pool_designate_new_master(string session, string _host);

    [XmlRpcMethod("pool.ha_prevent_restarts_for")]
    Response<string> pool_ha_prevent_restarts_for(string session, string _seconds);

    [XmlRpcMethod("pool.ha_failover_plan_exists")]
    Response<bool> pool_ha_failover_plan_exists(string session, string _n);

    [XmlRpcMethod("pool.ha_compute_max_host_failures_to_tolerate")]
    Response<string> pool_ha_compute_max_host_failures_to_tolerate(string session);

    [XmlRpcMethod("pool.ha_compute_hypothetical_max_host_failures_to_tolerate")]
    Response<string> pool_ha_compute_hypothetical_max_host_failures_to_tolerate(string session, object _configuration);

    [XmlRpcMethod("pool.ha_compute_vm_failover_plan")]
    Response<object> pool_ha_compute_vm_failover_plan(string session, string[] _failed_hosts, string[] _failed_vms);

    [XmlRpcMethod("pool.set_ha_host_failures_to_tolerate")]
    Response<string> pool_set_ha_host_failures_to_tolerate(string session, string _self, string _value);

    [XmlRpcMethod("Async.pool.set_ha_host_failures_to_tolerate")]
    Response<string> async_pool_set_ha_host_failures_to_tolerate(string session, string _self, string _value);

    [XmlRpcMethod("pool.create_new_blob")]
    Response<string> pool_create_new_blob(string session, string _pool, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("Async.pool.create_new_blob")]
    Response<string> async_pool_create_new_blob(string session, string _pool, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("pool.enable_external_auth")]
    Response<string> pool_enable_external_auth(string session, string _pool, object _config, string _service_name, string _auth_type);

    [XmlRpcMethod("pool.disable_external_auth")]
    Response<string> pool_disable_external_auth(string session, string _pool, object _config);

    [XmlRpcMethod("pool.detect_nonhomogeneous_external_auth")]
    Response<string> pool_detect_nonhomogeneous_external_auth(string session, string _pool);

    [XmlRpcMethod("pool.initialize_wlb")]
    Response<string> pool_initialize_wlb(string session, string _wlb_url, string _wlb_username, string _wlb_password, string _xenserver_username, string _xenserver_password);

    [XmlRpcMethod("Async.pool.initialize_wlb")]
    Response<string> async_pool_initialize_wlb(string session, string _wlb_url, string _wlb_username, string _wlb_password, string _xenserver_username, string _xenserver_password);

    [XmlRpcMethod("pool.deconfigure_wlb")]
    Response<string> pool_deconfigure_wlb(string session);

    [XmlRpcMethod("Async.pool.deconfigure_wlb")]
    Response<string> async_pool_deconfigure_wlb(string session);

    [XmlRpcMethod("pool.send_wlb_configuration")]
    Response<string> pool_send_wlb_configuration(string session, object _config);

    [XmlRpcMethod("Async.pool.send_wlb_configuration")]
    Response<string> async_pool_send_wlb_configuration(string session, object _config);

    [XmlRpcMethod("pool.retrieve_wlb_configuration")]
    Response<object> pool_retrieve_wlb_configuration(string session);

    [XmlRpcMethod("Async.pool.retrieve_wlb_configuration")]
    Response<string> async_pool_retrieve_wlb_configuration(string session);

    [XmlRpcMethod("pool.retrieve_wlb_recommendations")]
    Response<object> pool_retrieve_wlb_recommendations(string session);

    [XmlRpcMethod("Async.pool.retrieve_wlb_recommendations")]
    Response<string> async_pool_retrieve_wlb_recommendations(string session);

    [XmlRpcMethod("pool.send_test_post")]
    Response<string> pool_send_test_post(string session, string _host, string _port, string _body);

    [XmlRpcMethod("Async.pool.send_test_post")]
    Response<string> async_pool_send_test_post(string session, string _host, string _port, string _body);

    [XmlRpcMethod("pool.certificate_install")]
    Response<string> pool_certificate_install(string session, string _name, string _cert);

    [XmlRpcMethod("Async.pool.certificate_install")]
    Response<string> async_pool_certificate_install(string session, string _name, string _cert);

    [XmlRpcMethod("pool.certificate_uninstall")]
    Response<string> pool_certificate_uninstall(string session, string _name);

    [XmlRpcMethod("Async.pool.certificate_uninstall")]
    Response<string> async_pool_certificate_uninstall(string session, string _name);

    [XmlRpcMethod("pool.certificate_list")]
    Response<string[]> pool_certificate_list(string session);

    [XmlRpcMethod("Async.pool.certificate_list")]
    Response<string> async_pool_certificate_list(string session);

    [XmlRpcMethod("pool.crl_install")]
    Response<string> pool_crl_install(string session, string _name, string _cert);

    [XmlRpcMethod("Async.pool.crl_install")]
    Response<string> async_pool_crl_install(string session, string _name, string _cert);

    [XmlRpcMethod("pool.crl_uninstall")]
    Response<string> pool_crl_uninstall(string session, string _name);

    [XmlRpcMethod("Async.pool.crl_uninstall")]
    Response<string> async_pool_crl_uninstall(string session, string _name);

    [XmlRpcMethod("pool.crl_list")]
    Response<string[]> pool_crl_list(string session);

    [XmlRpcMethod("Async.pool.crl_list")]
    Response<string> async_pool_crl_list(string session);

    [XmlRpcMethod("pool.certificate_sync")]
    Response<string> pool_certificate_sync(string session);

    [XmlRpcMethod("Async.pool.certificate_sync")]
    Response<string> async_pool_certificate_sync(string session);

    [XmlRpcMethod("pool.enable_redo_log")]
    Response<string> pool_enable_redo_log(string session, string _sr);

    [XmlRpcMethod("Async.pool.enable_redo_log")]
    Response<string> async_pool_enable_redo_log(string session, string _sr);

    [XmlRpcMethod("pool.disable_redo_log")]
    Response<string> pool_disable_redo_log(string session);

    [XmlRpcMethod("Async.pool.disable_redo_log")]
    Response<string> async_pool_disable_redo_log(string session);

    [XmlRpcMethod("pool.set_vswitch_controller")]
    Response<string> pool_set_vswitch_controller(string session, string _address);

    [XmlRpcMethod("Async.pool.set_vswitch_controller")]
    Response<string> async_pool_set_vswitch_controller(string session, string _address);

    [XmlRpcMethod("pool.test_archive_target")]
    Response<string> pool_test_archive_target(string session, string _self, object _config);

    [XmlRpcMethod("pool.enable_local_storage_caching")]
    Response<string> pool_enable_local_storage_caching(string session, string _self);

    [XmlRpcMethod("Async.pool.enable_local_storage_caching")]
    Response<string> async_pool_enable_local_storage_caching(string session, string _self);

    [XmlRpcMethod("pool.disable_local_storage_caching")]
    Response<string> pool_disable_local_storage_caching(string session, string _self);

    [XmlRpcMethod("Async.pool.disable_local_storage_caching")]
    Response<string> async_pool_disable_local_storage_caching(string session, string _self);

    [XmlRpcMethod("pool.get_all")]
    Response<string[]> pool_get_all(string session);

    [XmlRpcMethod("pool.get_all_records")]
    Response<object> pool_get_all_records(string session);

    [XmlRpcMethod("pool_patch.get_record")]
    Response<Proxy_Pool_patch> pool_patch_get_record(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_by_uuid")]
    Response<string> pool_patch_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("pool_patch.get_by_name_label")]
    Response<string[]> pool_patch_get_by_name_label(string session, string _label);

    [XmlRpcMethod("pool_patch.get_uuid")]
    Response<string> pool_patch_get_uuid(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_name_label")]
    Response<string> pool_patch_get_name_label(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_name_description")]
    Response<string> pool_patch_get_name_description(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_version")]
    Response<string> pool_patch_get_version(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_size")]
    Response<string> pool_patch_get_size(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_pool_applied")]
    Response<bool> pool_patch_get_pool_applied(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_host_patches")]
    Response<string[]> pool_patch_get_host_patches(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_after_apply_guidance")]
    Response<string[]> pool_patch_get_after_apply_guidance(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.get_other_config")]
    Response<object> pool_patch_get_other_config(string session, string _pool_patch);

    [XmlRpcMethod("pool_patch.set_other_config")]
    Response<string> pool_patch_set_other_config(string session, string _pool_patch, object _other_config);

    [XmlRpcMethod("pool_patch.add_to_other_config")]
    Response<string> pool_patch_add_to_other_config(string session, string _pool_patch, string _key, string _value);

    [XmlRpcMethod("pool_patch.remove_from_other_config")]
    Response<string> pool_patch_remove_from_other_config(string session, string _pool_patch, string _key);

    [XmlRpcMethod("pool_patch.apply")]
    Response<string> pool_patch_apply(string session, string _self, string _host);

    [XmlRpcMethod("Async.pool_patch.apply")]
    Response<string> async_pool_patch_apply(string session, string _self, string _host);

    [XmlRpcMethod("pool_patch.pool_apply")]
    Response<string> pool_patch_pool_apply(string session, string _self);

    [XmlRpcMethod("Async.pool_patch.pool_apply")]
    Response<string> async_pool_patch_pool_apply(string session, string _self);

    [XmlRpcMethod("pool_patch.precheck")]
    Response<string> pool_patch_precheck(string session, string _self, string _host);

    [XmlRpcMethod("Async.pool_patch.precheck")]
    Response<string> async_pool_patch_precheck(string session, string _self, string _host);

    [XmlRpcMethod("pool_patch.clean")]
    Response<string> pool_patch_clean(string session, string _self);

    [XmlRpcMethod("Async.pool_patch.clean")]
    Response<string> async_pool_patch_clean(string session, string _self);

    [XmlRpcMethod("pool_patch.pool_clean")]
    Response<string> pool_patch_pool_clean(string session, string _self);

    [XmlRpcMethod("Async.pool_patch.pool_clean")]
    Response<string> async_pool_patch_pool_clean(string session, string _self);

    [XmlRpcMethod("pool_patch.destroy")]
    Response<string> pool_patch_destroy(string session, string _self);

    [XmlRpcMethod("Async.pool_patch.destroy")]
    Response<string> async_pool_patch_destroy(string session, string _self);

    [XmlRpcMethod("pool_patch.clean_on_host")]
    Response<string> pool_patch_clean_on_host(string session, string _self, string _host);

    [XmlRpcMethod("Async.pool_patch.clean_on_host")]
    Response<string> async_pool_patch_clean_on_host(string session, string _self, string _host);

    [XmlRpcMethod("pool_patch.get_all")]
    Response<string[]> pool_patch_get_all(string session);

    [XmlRpcMethod("pool_patch.get_all_records")]
    Response<object> pool_patch_get_all_records(string session);

    [XmlRpcMethod("VM.get_record")]
    Response<Proxy_VM> vm_get_record(string session, string _vm);

    [XmlRpcMethod("VM.get_by_uuid")]
    Response<string> vm_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VM.create")]
    Response<string> vm_create(string session, Proxy_VM _record);

    [XmlRpcMethod("Async.VM.create")]
    Response<string> async_vm_create(string session, Proxy_VM _record);

    [XmlRpcMethod("VM.destroy")]
    Response<string> vm_destroy(string session, string _vm);

    [XmlRpcMethod("Async.VM.destroy")]
    Response<string> async_vm_destroy(string session, string _vm);

    [XmlRpcMethod("VM.get_by_name_label")]
    Response<string[]> vm_get_by_name_label(string session, string _label);

    [XmlRpcMethod("VM.get_uuid")]
    Response<string> vm_get_uuid(string session, string _vm);

    [XmlRpcMethod("VM.get_allowed_operations")]
    Response<string[]> vm_get_allowed_operations(string session, string _vm);

    [XmlRpcMethod("VM.get_current_operations")]
    Response<object> vm_get_current_operations(string session, string _vm);

    [XmlRpcMethod("VM.get_power_state")]
    Response<string> vm_get_power_state(string session, string _vm);

    [XmlRpcMethod("VM.get_name_label")]
    Response<string> vm_get_name_label(string session, string _vm);

    [XmlRpcMethod("VM.get_name_description")]
    Response<string> vm_get_name_description(string session, string _vm);

    [XmlRpcMethod("VM.get_user_version")]
    Response<string> vm_get_user_version(string session, string _vm);

    [XmlRpcMethod("VM.get_is_a_template")]
    Response<bool> vm_get_is_a_template(string session, string _vm);

    [XmlRpcMethod("VM.get_suspend_VDI")]
    Response<string> vm_get_suspend_vdi(string session, string _vm);

    [XmlRpcMethod("VM.get_resident_on")]
    Response<string> vm_get_resident_on(string session, string _vm);

    [XmlRpcMethod("VM.get_affinity")]
    Response<string> vm_get_affinity(string session, string _vm);

    [XmlRpcMethod("VM.get_memory_overhead")]
    Response<string> vm_get_memory_overhead(string session, string _vm);

    [XmlRpcMethod("VM.get_memory_target")]
    Response<string> vm_get_memory_target(string session, string _vm);

    [XmlRpcMethod("VM.get_memory_static_max")]
    Response<string> vm_get_memory_static_max(string session, string _vm);

    [XmlRpcMethod("VM.get_memory_dynamic_max")]
    Response<string> vm_get_memory_dynamic_max(string session, string _vm);

    [XmlRpcMethod("VM.get_memory_dynamic_min")]
    Response<string> vm_get_memory_dynamic_min(string session, string _vm);

    [XmlRpcMethod("VM.get_memory_static_min")]
    Response<string> vm_get_memory_static_min(string session, string _vm);

    [XmlRpcMethod("VM.get_VCPUs_params")]
    Response<object> vm_get_vcpus_params(string session, string _vm);

    [XmlRpcMethod("VM.get_VCPUs_max")]
    Response<string> vm_get_vcpus_max(string session, string _vm);

    [XmlRpcMethod("VM.get_VCPUs_at_startup")]
    Response<string> vm_get_vcpus_at_startup(string session, string _vm);

    [XmlRpcMethod("VM.get_actions_after_shutdown")]
    Response<string> vm_get_actions_after_shutdown(string session, string _vm);

    [XmlRpcMethod("VM.get_actions_after_reboot")]
    Response<string> vm_get_actions_after_reboot(string session, string _vm);

    [XmlRpcMethod("VM.get_actions_after_crash")]
    Response<string> vm_get_actions_after_crash(string session, string _vm);

    [XmlRpcMethod("VM.get_consoles")]
    Response<string[]> vm_get_consoles(string session, string _vm);

    [XmlRpcMethod("VM.get_VIFs")]
    Response<string[]> vm_get_vifs(string session, string _vm);

    [XmlRpcMethod("VM.get_VBDs")]
    Response<string[]> vm_get_vbds(string session, string _vm);

    [XmlRpcMethod("VM.get_crash_dumps")]
    Response<string[]> vm_get_crash_dumps(string session, string _vm);

    [XmlRpcMethod("VM.get_VTPMs")]
    Response<string[]> vm_get_vtpms(string session, string _vm);

    [XmlRpcMethod("VM.get_PV_bootloader")]
    Response<string> vm_get_pv_bootloader(string session, string _vm);

    [XmlRpcMethod("VM.get_PV_kernel")]
    Response<string> vm_get_pv_kernel(string session, string _vm);

    [XmlRpcMethod("VM.get_PV_ramdisk")]
    Response<string> vm_get_pv_ramdisk(string session, string _vm);

    [XmlRpcMethod("VM.get_PV_args")]
    Response<string> vm_get_pv_args(string session, string _vm);

    [XmlRpcMethod("VM.get_PV_bootloader_args")]
    Response<string> vm_get_pv_bootloader_args(string session, string _vm);

    [XmlRpcMethod("VM.get_PV_legacy_args")]
    Response<string> vm_get_pv_legacy_args(string session, string _vm);

    [XmlRpcMethod("VM.get_HVM_boot_policy")]
    Response<string> vm_get_hvm_boot_policy(string session, string _vm);

    [XmlRpcMethod("VM.get_HVM_boot_params")]
    Response<object> vm_get_hvm_boot_params(string session, string _vm);

    [XmlRpcMethod("VM.get_HVM_shadow_multiplier")]
    Response<double> vm_get_hvm_shadow_multiplier(string session, string _vm);

    [XmlRpcMethod("VM.get_platform")]
    Response<object> vm_get_platform(string session, string _vm);

    [XmlRpcMethod("VM.get_PCI_bus")]
    Response<string> vm_get_pci_bus(string session, string _vm);

    [XmlRpcMethod("VM.get_other_config")]
    Response<object> vm_get_other_config(string session, string _vm);

    [XmlRpcMethod("VM.get_domid")]
    Response<string> vm_get_domid(string session, string _vm);

    [XmlRpcMethod("VM.get_domarch")]
    Response<string> vm_get_domarch(string session, string _vm);

    [XmlRpcMethod("VM.get_last_boot_CPU_flags")]
    Response<object> vm_get_last_boot_cpu_flags(string session, string _vm);

    [XmlRpcMethod("VM.get_is_control_domain")]
    Response<bool> vm_get_is_control_domain(string session, string _vm);

    [XmlRpcMethod("VM.get_metrics")]
    Response<string> vm_get_metrics(string session, string _vm);

    [XmlRpcMethod("VM.get_guest_metrics")]
    Response<string> vm_get_guest_metrics(string session, string _vm);

    [XmlRpcMethod("VM.get_last_booted_record")]
    Response<string> vm_get_last_booted_record(string session, string _vm);

    [XmlRpcMethod("VM.get_recommendations")]
    Response<string> vm_get_recommendations(string session, string _vm);

    [XmlRpcMethod("VM.get_xenstore_data")]
    Response<object> vm_get_xenstore_data(string session, string _vm);

    [XmlRpcMethod("VM.get_ha_always_run")]
    Response<bool> vm_get_ha_always_run(string session, string _vm);

    [XmlRpcMethod("VM.get_ha_restart_priority")]
    Response<string> vm_get_ha_restart_priority(string session, string _vm);

    [XmlRpcMethod("VM.get_is_a_snapshot")]
    Response<bool> vm_get_is_a_snapshot(string session, string _vm);

    [XmlRpcMethod("VM.get_snapshot_of")]
    Response<string> vm_get_snapshot_of(string session, string _vm);

    [XmlRpcMethod("VM.get_snapshots")]
    Response<string[]> vm_get_snapshots(string session, string _vm);

    [XmlRpcMethod("VM.get_snapshot_time")]
    Response<DateTime> vm_get_snapshot_time(string session, string _vm);

    [XmlRpcMethod("VM.get_transportable_snapshot_id")]
    Response<string> vm_get_transportable_snapshot_id(string session, string _vm);

    [XmlRpcMethod("VM.get_blobs")]
    Response<object> vm_get_blobs(string session, string _vm);

    [XmlRpcMethod("VM.get_tags")]
    Response<string[]> vm_get_tags(string session, string _vm);

    [XmlRpcMethod("VM.get_blocked_operations")]
    Response<object> vm_get_blocked_operations(string session, string _vm);

    [XmlRpcMethod("VM.get_snapshot_info")]
    Response<object> vm_get_snapshot_info(string session, string _vm);

    [XmlRpcMethod("VM.get_snapshot_metadata")]
    Response<string> vm_get_snapshot_metadata(string session, string _vm);

    [XmlRpcMethod("VM.get_parent")]
    Response<string> vm_get_parent(string session, string _vm);

    [XmlRpcMethod("VM.get_children")]
    Response<string[]> vm_get_children(string session, string _vm);

    [XmlRpcMethod("VM.get_bios_strings")]
    Response<object> vm_get_bios_strings(string session, string _vm);

    [XmlRpcMethod("VM.get_protection_policy")]
    Response<string> vm_get_protection_policy(string session, string _vm);

    [XmlRpcMethod("VM.get_is_snapshot_from_vmpp")]
    Response<bool> vm_get_is_snapshot_from_vmpp(string session, string _vm);

    [XmlRpcMethod("VM.get_appliance")]
    Response<string> vm_get_appliance(string session, string _vm);

    [XmlRpcMethod("VM.get_start_delay")]
    Response<string> vm_get_start_delay(string session, string _vm);

    [XmlRpcMethod("VM.get_shutdown_delay")]
    Response<string> vm_get_shutdown_delay(string session, string _vm);

    [XmlRpcMethod("VM.get_order")]
    Response<string> vm_get_order(string session, string _vm);

    [XmlRpcMethod("VM.get_VGPUs")]
    Response<string[]> vm_get_vgpus(string session, string _vm);

    [XmlRpcMethod("VM.get_attached_PCIs")]
    Response<string[]> vm_get_attached_pcis(string session, string _vm);

    [XmlRpcMethod("VM.get_suspend_SR")]
    Response<string> vm_get_suspend_sr(string session, string _vm);

    [XmlRpcMethod("VM.get_version")]
    Response<string> vm_get_version(string session, string _vm);

    [XmlRpcMethod("VM.set_name_label")]
    Response<string> vm_set_name_label(string session, string _vm, string _label);

    [XmlRpcMethod("VM.set_name_description")]
    Response<string> vm_set_name_description(string session, string _vm, string _description);

    [XmlRpcMethod("VM.set_user_version")]
    Response<string> vm_set_user_version(string session, string _vm, string _user_version);

    [XmlRpcMethod("VM.set_is_a_template")]
    Response<string> vm_set_is_a_template(string session, string _vm, bool _is_a_template);

    [XmlRpcMethod("VM.set_affinity")]
    Response<string> vm_set_affinity(string session, string _vm, string _affinity);

    [XmlRpcMethod("VM.set_VCPUs_params")]
    Response<string> vm_set_vcpus_params(string session, string _vm, object _params);

    [XmlRpcMethod("VM.add_to_VCPUs_params")]
    Response<string> vm_add_to_vcpus_params(string session, string _vm, string _key, string _value);

    [XmlRpcMethod("VM.remove_from_VCPUs_params")]
    Response<string> vm_remove_from_vcpus_params(string session, string _vm, string _key);

    [XmlRpcMethod("VM.set_actions_after_shutdown")]
    Response<string> vm_set_actions_after_shutdown(string session, string _vm, string _after_shutdown);

    [XmlRpcMethod("VM.set_actions_after_reboot")]
    Response<string> vm_set_actions_after_reboot(string session, string _vm, string _after_reboot);

    [XmlRpcMethod("VM.set_actions_after_crash")]
    Response<string> vm_set_actions_after_crash(string session, string _vm, string _after_crash);

    [XmlRpcMethod("VM.set_PV_bootloader")]
    Response<string> vm_set_pv_bootloader(string session, string _vm, string _bootloader);

    [XmlRpcMethod("VM.set_PV_kernel")]
    Response<string> vm_set_pv_kernel(string session, string _vm, string _kernel);

    [XmlRpcMethod("VM.set_PV_ramdisk")]
    Response<string> vm_set_pv_ramdisk(string session, string _vm, string _ramdisk);

    [XmlRpcMethod("VM.set_PV_args")]
    Response<string> vm_set_pv_args(string session, string _vm, string _args);

    [XmlRpcMethod("VM.set_PV_bootloader_args")]
    Response<string> vm_set_pv_bootloader_args(string session, string _vm, string _bootloader_args);

    [XmlRpcMethod("VM.set_PV_legacy_args")]
    Response<string> vm_set_pv_legacy_args(string session, string _vm, string _legacy_args);

    [XmlRpcMethod("VM.set_HVM_boot_policy")]
    Response<string> vm_set_hvm_boot_policy(string session, string _vm, string _boot_policy);

    [XmlRpcMethod("VM.set_HVM_boot_params")]
    Response<string> vm_set_hvm_boot_params(string session, string _vm, object _boot_params);

    [XmlRpcMethod("VM.add_to_HVM_boot_params")]
    Response<string> vm_add_to_hvm_boot_params(string session, string _vm, string _key, string _value);

    [XmlRpcMethod("VM.remove_from_HVM_boot_params")]
    Response<string> vm_remove_from_hvm_boot_params(string session, string _vm, string _key);

    [XmlRpcMethod("VM.set_platform")]
    Response<string> vm_set_platform(string session, string _vm, object _platform);

    [XmlRpcMethod("VM.add_to_platform")]
    Response<string> vm_add_to_platform(string session, string _vm, string _key, string _value);

    [XmlRpcMethod("VM.remove_from_platform")]
    Response<string> vm_remove_from_platform(string session, string _vm, string _key);

    [XmlRpcMethod("VM.set_PCI_bus")]
    Response<string> vm_set_pci_bus(string session, string _vm, string _pci_bus);

    [XmlRpcMethod("VM.set_other_config")]
    Response<string> vm_set_other_config(string session, string _vm, object _other_config);

    [XmlRpcMethod("VM.add_to_other_config")]
    Response<string> vm_add_to_other_config(string session, string _vm, string _key, string _value);

    [XmlRpcMethod("VM.remove_from_other_config")]
    Response<string> vm_remove_from_other_config(string session, string _vm, string _key);

    [XmlRpcMethod("VM.set_recommendations")]
    Response<string> vm_set_recommendations(string session, string _vm, string _recommendations);

    [XmlRpcMethod("VM.set_xenstore_data")]
    Response<string> vm_set_xenstore_data(string session, string _vm, object _xenstore_data);

    [XmlRpcMethod("VM.add_to_xenstore_data")]
    Response<string> vm_add_to_xenstore_data(string session, string _vm, string _key, string _value);

    [XmlRpcMethod("VM.remove_from_xenstore_data")]
    Response<string> vm_remove_from_xenstore_data(string session, string _vm, string _key);

    [XmlRpcMethod("VM.set_tags")]
    Response<string> vm_set_tags(string session, string _vm, string[] _tags);

    [XmlRpcMethod("VM.add_tags")]
    Response<string> vm_add_tags(string session, string _vm, string _value);

    [XmlRpcMethod("VM.remove_tags")]
    Response<string> vm_remove_tags(string session, string _vm, string _value);

    [XmlRpcMethod("VM.set_blocked_operations")]
    Response<string> vm_set_blocked_operations(string session, string _vm, object _blocked_operations);

    [XmlRpcMethod("VM.add_to_blocked_operations")]
    Response<string> vm_add_to_blocked_operations(string session, string _vm, string _key, string _value);

    [XmlRpcMethod("VM.remove_from_blocked_operations")]
    Response<string> vm_remove_from_blocked_operations(string session, string _vm, string _key);

    [XmlRpcMethod("VM.set_suspend_SR")]
    Response<string> vm_set_suspend_sr(string session, string _vm, string _suspend_sr);

    [XmlRpcMethod("VM.snapshot")]
    Response<string> vm_snapshot(string session, string _vm, string _new_name);

    [XmlRpcMethod("Async.VM.snapshot")]
    Response<string> async_vm_snapshot(string session, string _vm, string _new_name);

    [XmlRpcMethod("VM.snapshot_with_quiesce")]
    Response<string> vm_snapshot_with_quiesce(string session, string _vm, string _new_name);

    [XmlRpcMethod("Async.VM.snapshot_with_quiesce")]
    Response<string> async_vm_snapshot_with_quiesce(string session, string _vm, string _new_name);

    [XmlRpcMethod("VM.clone")]
    Response<string> vm_clone(string session, string _vm, string _new_name);

    [XmlRpcMethod("Async.VM.clone")]
    Response<string> async_vm_clone(string session, string _vm, string _new_name);

    [XmlRpcMethod("VM.copy")]
    Response<string> vm_copy(string session, string _vm, string _new_name, string _sr);

    [XmlRpcMethod("Async.VM.copy")]
    Response<string> async_vm_copy(string session, string _vm, string _new_name, string _sr);

    [XmlRpcMethod("VM.revert")]
    Response<string> vm_revert(string session, string _snapshot);

    [XmlRpcMethod("Async.VM.revert")]
    Response<string> async_vm_revert(string session, string _snapshot);

    [XmlRpcMethod("VM.checkpoint")]
    Response<string> vm_checkpoint(string session, string _vm, string _new_name);

    [XmlRpcMethod("Async.VM.checkpoint")]
    Response<string> async_vm_checkpoint(string session, string _vm, string _new_name);

    [XmlRpcMethod("VM.provision")]
    Response<string> vm_provision(string session, string _vm);

    [XmlRpcMethod("Async.VM.provision")]
    Response<string> async_vm_provision(string session, string _vm);

    [XmlRpcMethod("VM.start")]
    Response<string> vm_start(string session, string _vm, bool _start_paused, bool _force);

    [XmlRpcMethod("Async.VM.start")]
    Response<string> async_vm_start(string session, string _vm, bool _start_paused, bool _force);

    [XmlRpcMethod("VM.start_on")]
    Response<string> vm_start_on(string session, string _vm, string _host, bool _start_paused, bool _force);

    [XmlRpcMethod("Async.VM.start_on")]
    Response<string> async_vm_start_on(string session, string _vm, string _host, bool _start_paused, bool _force);

    [XmlRpcMethod("VM.pause")]
    Response<string> vm_pause(string session, string _vm);

    [XmlRpcMethod("Async.VM.pause")]
    Response<string> async_vm_pause(string session, string _vm);

    [XmlRpcMethod("VM.unpause")]
    Response<string> vm_unpause(string session, string _vm);

    [XmlRpcMethod("Async.VM.unpause")]
    Response<string> async_vm_unpause(string session, string _vm);

    [XmlRpcMethod("VM.clean_shutdown")]
    Response<string> vm_clean_shutdown(string session, string _vm);

    [XmlRpcMethod("Async.VM.clean_shutdown")]
    Response<string> async_vm_clean_shutdown(string session, string _vm);

    [XmlRpcMethod("VM.clean_reboot")]
    Response<string> vm_clean_reboot(string session, string _vm);

    [XmlRpcMethod("Async.VM.clean_reboot")]
    Response<string> async_vm_clean_reboot(string session, string _vm);

    [XmlRpcMethod("VM.hard_shutdown")]
    Response<string> vm_hard_shutdown(string session, string _vm);

    [XmlRpcMethod("Async.VM.hard_shutdown")]
    Response<string> async_vm_hard_shutdown(string session, string _vm);

    [XmlRpcMethod("VM.power_state_reset")]
    Response<string> vm_power_state_reset(string session, string _vm);

    [XmlRpcMethod("Async.VM.power_state_reset")]
    Response<string> async_vm_power_state_reset(string session, string _vm);

    [XmlRpcMethod("VM.hard_reboot")]
    Response<string> vm_hard_reboot(string session, string _vm);

    [XmlRpcMethod("Async.VM.hard_reboot")]
    Response<string> async_vm_hard_reboot(string session, string _vm);

    [XmlRpcMethod("VM.suspend")]
    Response<string> vm_suspend(string session, string _vm);

    [XmlRpcMethod("Async.VM.suspend")]
    Response<string> async_vm_suspend(string session, string _vm);

    [XmlRpcMethod("VM.resume")]
    Response<string> vm_resume(string session, string _vm, bool _start_paused, bool _force);

    [XmlRpcMethod("Async.VM.resume")]
    Response<string> async_vm_resume(string session, string _vm, bool _start_paused, bool _force);

    [XmlRpcMethod("VM.resume_on")]
    Response<string> vm_resume_on(string session, string _vm, string _host, bool _start_paused, bool _force);

    [XmlRpcMethod("Async.VM.resume_on")]
    Response<string> async_vm_resume_on(string session, string _vm, string _host, bool _start_paused, bool _force);

    [XmlRpcMethod("VM.pool_migrate")]
    Response<string> vm_pool_migrate(string session, string _vm, string _host, object _options);

    [XmlRpcMethod("Async.VM.pool_migrate")]
    Response<string> async_vm_pool_migrate(string session, string _vm, string _host, object _options);

    [XmlRpcMethod("VM.set_VCPUs_number_live")]
    Response<string> vm_set_vcpus_number_live(string session, string _self, string _nvcpu);

    [XmlRpcMethod("Async.VM.set_VCPUs_number_live")]
    Response<string> async_vm_set_vcpus_number_live(string session, string _self, string _nvcpu);

    [XmlRpcMethod("VM.add_to_VCPUs_params_live")]
    Response<string> vm_add_to_vcpus_params_live(string session, string _self, string _key, string _value);

    [XmlRpcMethod("Async.VM.add_to_VCPUs_params_live")]
    Response<string> async_vm_add_to_vcpus_params_live(string session, string _self, string _key, string _value);

    [XmlRpcMethod("VM.set_ha_restart_priority")]
    Response<string> vm_set_ha_restart_priority(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_ha_always_run")]
    Response<string> vm_set_ha_always_run(string session, string _self, bool _value);

    [XmlRpcMethod("VM.compute_memory_overhead")]
    Response<string> vm_compute_memory_overhead(string session, string _vm);

    [XmlRpcMethod("Async.VM.compute_memory_overhead")]
    Response<string> async_vm_compute_memory_overhead(string session, string _vm);

    [XmlRpcMethod("VM.set_memory_dynamic_max")]
    Response<string> vm_set_memory_dynamic_max(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_memory_dynamic_min")]
    Response<string> vm_set_memory_dynamic_min(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_memory_dynamic_range")]
    Response<string> vm_set_memory_dynamic_range(string session, string _self, string _min, string _max);

    [XmlRpcMethod("Async.VM.set_memory_dynamic_range")]
    Response<string> async_vm_set_memory_dynamic_range(string session, string _self, string _min, string _max);

    [XmlRpcMethod("VM.set_memory_static_max")]
    Response<string> vm_set_memory_static_max(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_memory_static_min")]
    Response<string> vm_set_memory_static_min(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_memory_static_range")]
    Response<string> vm_set_memory_static_range(string session, string _self, string _min, string _max);

    [XmlRpcMethod("Async.VM.set_memory_static_range")]
    Response<string> async_vm_set_memory_static_range(string session, string _self, string _min, string _max);

    [XmlRpcMethod("VM.set_memory_limits")]
    Response<string> vm_set_memory_limits(string session, string _self, string _static_min, string _static_max, string _dynamic_min, string _dynamic_max);

    [XmlRpcMethod("Async.VM.set_memory_limits")]
    Response<string> async_vm_set_memory_limits(string session, string _self, string _static_min, string _static_max, string _dynamic_min, string _dynamic_max);

    [XmlRpcMethod("VM.set_memory_target_live")]
    Response<string> vm_set_memory_target_live(string session, string _self, string _target);

    [XmlRpcMethod("Async.VM.set_memory_target_live")]
    Response<string> async_vm_set_memory_target_live(string session, string _self, string _target);

    [XmlRpcMethod("VM.wait_memory_target_live")]
    Response<string> vm_wait_memory_target_live(string session, string _self);

    [XmlRpcMethod("Async.VM.wait_memory_target_live")]
    Response<string> async_vm_wait_memory_target_live(string session, string _self);

    [XmlRpcMethod("VM.set_HVM_shadow_multiplier")]
    Response<string> vm_set_hvm_shadow_multiplier(string session, string _self, double _value);

    [XmlRpcMethod("VM.set_shadow_multiplier_live")]
    Response<string> vm_set_shadow_multiplier_live(string session, string _self, double _multiplier);

    [XmlRpcMethod("Async.VM.set_shadow_multiplier_live")]
    Response<string> async_vm_set_shadow_multiplier_live(string session, string _self, double _multiplier);

    [XmlRpcMethod("VM.set_VCPUs_max")]
    Response<string> vm_set_vcpus_max(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_VCPUs_at_startup")]
    Response<string> vm_set_vcpus_at_startup(string session, string _self, string _value);

    [XmlRpcMethod("VM.send_sysrq")]
    Response<string> vm_send_sysrq(string session, string _vm, string _key);

    [XmlRpcMethod("Async.VM.send_sysrq")]
    Response<string> async_vm_send_sysrq(string session, string _vm, string _key);

    [XmlRpcMethod("VM.send_trigger")]
    Response<string> vm_send_trigger(string session, string _vm, string _trigger);

    [XmlRpcMethod("Async.VM.send_trigger")]
    Response<string> async_vm_send_trigger(string session, string _vm, string _trigger);

    [XmlRpcMethod("VM.maximise_memory")]
    Response<string> vm_maximise_memory(string session, string _self, string _total, bool _approximate);

    [XmlRpcMethod("Async.VM.maximise_memory")]
    Response<string> async_vm_maximise_memory(string session, string _self, string _total, bool _approximate);

    [XmlRpcMethod("VM.migrate_send")]
    Response<string> vm_migrate_send(string session, string _vm, object _dest, bool _live, object _vdi_map, object _vif_map, object _options);

    [XmlRpcMethod("Async.VM.migrate_send")]
    Response<string> async_vm_migrate_send(string session, string _vm, object _dest, bool _live, object _vdi_map, object _vif_map, object _options);

    [XmlRpcMethod("VM.assert_can_migrate")]
    Response<string> vm_assert_can_migrate(string session, string _vm, object _dest, bool _live, object _vdi_map, object _vif_map, object _options);

    [XmlRpcMethod("Async.VM.assert_can_migrate")]
    Response<string> async_vm_assert_can_migrate(string session, string _vm, object _dest, bool _live, object _vdi_map, object _vif_map, object _options);

    [XmlRpcMethod("VM.get_boot_record")]
    Response<Proxy_VM> vm_get_boot_record(string session, string _self);

    [XmlRpcMethod("VM.get_data_sources")]
    Response<Proxy_Data_source[]> vm_get_data_sources(string session, string _self);

    [XmlRpcMethod("VM.record_data_source")]
    Response<string> vm_record_data_source(string session, string _self, string _data_source);

    [XmlRpcMethod("VM.query_data_source")]
    Response<double> vm_query_data_source(string session, string _self, string _data_source);

    [XmlRpcMethod("VM.forget_data_source_archives")]
    Response<string> vm_forget_data_source_archives(string session, string _self, string _data_source);

    [XmlRpcMethod("VM.assert_operation_valid")]
    Response<string> vm_assert_operation_valid(string session, string _self, string _op);

    [XmlRpcMethod("Async.VM.assert_operation_valid")]
    Response<string> async_vm_assert_operation_valid(string session, string _self, string _op);

    [XmlRpcMethod("VM.update_allowed_operations")]
    Response<string> vm_update_allowed_operations(string session, string _self);

    [XmlRpcMethod("Async.VM.update_allowed_operations")]
    Response<string> async_vm_update_allowed_operations(string session, string _self);

    [XmlRpcMethod("VM.get_allowed_VBD_devices")]
    Response<string[]> vm_get_allowed_vbd_devices(string session, string _vm);

    [XmlRpcMethod("VM.get_allowed_VIF_devices")]
    Response<string[]> vm_get_allowed_vif_devices(string session, string _vm);

    [XmlRpcMethod("VM.get_possible_hosts")]
    Response<string[]> vm_get_possible_hosts(string session, string _vm);

    [XmlRpcMethod("Async.VM.get_possible_hosts")]
    Response<string> async_vm_get_possible_hosts(string session, string _vm);

    [XmlRpcMethod("VM.assert_can_boot_here")]
    Response<string> vm_assert_can_boot_here(string session, string _self, string _host);

    [XmlRpcMethod("Async.VM.assert_can_boot_here")]
    Response<string> async_vm_assert_can_boot_here(string session, string _self, string _host);

    [XmlRpcMethod("VM.create_new_blob")]
    Response<string> vm_create_new_blob(string session, string _vm, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("Async.VM.create_new_blob")]
    Response<string> async_vm_create_new_blob(string session, string _vm, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("VM.assert_agile")]
    Response<string> vm_assert_agile(string session, string _self);

    [XmlRpcMethod("Async.VM.assert_agile")]
    Response<string> async_vm_assert_agile(string session, string _self);

    [XmlRpcMethod("VM.retrieve_wlb_recommendations")]
    Response<object> vm_retrieve_wlb_recommendations(string session, string _vm);

    [XmlRpcMethod("Async.VM.retrieve_wlb_recommendations")]
    Response<string> async_vm_retrieve_wlb_recommendations(string session, string _vm);

    [XmlRpcMethod("VM.copy_bios_strings")]
    Response<string> vm_copy_bios_strings(string session, string _vm, string _host);

    [XmlRpcMethod("Async.VM.copy_bios_strings")]
    Response<string> async_vm_copy_bios_strings(string session, string _vm, string _host);

    [XmlRpcMethod("VM.set_protection_policy")]
    Response<string> vm_set_protection_policy(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_start_delay")]
    Response<string> vm_set_start_delay(string session, string _self, string _value);

    [XmlRpcMethod("Async.VM.set_start_delay")]
    Response<string> async_vm_set_start_delay(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_shutdown_delay")]
    Response<string> vm_set_shutdown_delay(string session, string _self, string _value);

    [XmlRpcMethod("Async.VM.set_shutdown_delay")]
    Response<string> async_vm_set_shutdown_delay(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_order")]
    Response<string> vm_set_order(string session, string _self, string _value);

    [XmlRpcMethod("Async.VM.set_order")]
    Response<string> async_vm_set_order(string session, string _self, string _value);

    [XmlRpcMethod("VM.set_suspend_VDI")]
    Response<string> vm_set_suspend_vdi(string session, string _self, string _value);

    [XmlRpcMethod("Async.VM.set_suspend_VDI")]
    Response<string> async_vm_set_suspend_vdi(string session, string _self, string _value);

    [XmlRpcMethod("VM.assert_can_be_recovered")]
    Response<string> vm_assert_can_be_recovered(string session, string _self, string _session_to);

    [XmlRpcMethod("Async.VM.assert_can_be_recovered")]
    Response<string> async_vm_assert_can_be_recovered(string session, string _self, string _session_to);

    [XmlRpcMethod("VM.recover")]
    Response<string> vm_recover(string session, string _self, string _session_to, bool _force);

    [XmlRpcMethod("Async.VM.recover")]
    Response<string> async_vm_recover(string session, string _self, string _session_to, bool _force);

    [XmlRpcMethod("VM.import_convert")]
    Response<string> vm_import_convert(string session, string _type, string _username, string _password, string _sr, object _remote_config);

    [XmlRpcMethod("Async.VM.import_convert")]
    Response<string> async_vm_import_convert(string session, string _type, string _username, string _password, string _sr, object _remote_config);

    [XmlRpcMethod("VM.set_appliance")]
    Response<string> vm_set_appliance(string session, string _self, string _value);

    [XmlRpcMethod("Async.VM.set_appliance")]
    Response<string> async_vm_set_appliance(string session, string _self, string _value);

    [XmlRpcMethod("VM.query_services")]
    Response<object> vm_query_services(string session, string _self);

    [XmlRpcMethod("Async.VM.query_services")]
    Response<string> async_vm_query_services(string session, string _self);

    [XmlRpcMethod("VM.get_all")]
    Response<string[]> vm_get_all(string session);

    [XmlRpcMethod("VM.get_all_records")]
    Response<object> vm_get_all_records(string session);

    [XmlRpcMethod("VM_metrics.get_record")]
    Response<Proxy_VM_metrics> vm_metrics_get_record(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_by_uuid")]
    Response<string> vm_metrics_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VM_metrics.get_uuid")]
    Response<string> vm_metrics_get_uuid(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_memory_actual")]
    Response<string> vm_metrics_get_memory_actual(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_VCPUs_number")]
    Response<string> vm_metrics_get_vcpus_number(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_VCPUs_utilisation")]
    Response<object> vm_metrics_get_vcpus_utilisation(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_VCPUs_CPU")]
    Response<object> vm_metrics_get_vcpus_cpu(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_VCPUs_params")]
    Response<object> vm_metrics_get_vcpus_params(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_VCPUs_flags")]
    Response<object> vm_metrics_get_vcpus_flags(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_state")]
    Response<string[]> vm_metrics_get_state(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_start_time")]
    Response<DateTime> vm_metrics_get_start_time(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_install_time")]
    Response<DateTime> vm_metrics_get_install_time(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_last_updated")]
    Response<DateTime> vm_metrics_get_last_updated(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.get_other_config")]
    Response<object> vm_metrics_get_other_config(string session, string _vm_metrics);

    [XmlRpcMethod("VM_metrics.set_other_config")]
    Response<string> vm_metrics_set_other_config(string session, string _vm_metrics, object _other_config);

    [XmlRpcMethod("VM_metrics.add_to_other_config")]
    Response<string> vm_metrics_add_to_other_config(string session, string _vm_metrics, string _key, string _value);

    [XmlRpcMethod("VM_metrics.remove_from_other_config")]
    Response<string> vm_metrics_remove_from_other_config(string session, string _vm_metrics, string _key);

    [XmlRpcMethod("VM_metrics.get_all")]
    Response<string[]> vm_metrics_get_all(string session);

    [XmlRpcMethod("VM_metrics.get_all_records")]
    Response<object> vm_metrics_get_all_records(string session);

    [XmlRpcMethod("VM_guest_metrics.get_record")]
    Response<Proxy_VM_guest_metrics> vm_guest_metrics_get_record(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_by_uuid")]
    Response<string> vm_guest_metrics_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VM_guest_metrics.get_uuid")]
    Response<string> vm_guest_metrics_get_uuid(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_os_version")]
    Response<object> vm_guest_metrics_get_os_version(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_PV_drivers_version")]
    Response<object> vm_guest_metrics_get_pv_drivers_version(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_PV_drivers_up_to_date")]
    Response<bool> vm_guest_metrics_get_pv_drivers_up_to_date(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_memory")]
    Response<object> vm_guest_metrics_get_memory(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_disks")]
    Response<object> vm_guest_metrics_get_disks(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_networks")]
    Response<object> vm_guest_metrics_get_networks(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_other")]
    Response<object> vm_guest_metrics_get_other(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_last_updated")]
    Response<DateTime> vm_guest_metrics_get_last_updated(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_other_config")]
    Response<object> vm_guest_metrics_get_other_config(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.get_live")]
    Response<bool> vm_guest_metrics_get_live(string session, string _vm_guest_metrics);

    [XmlRpcMethod("VM_guest_metrics.set_other_config")]
    Response<string> vm_guest_metrics_set_other_config(string session, string _vm_guest_metrics, object _other_config);

    [XmlRpcMethod("VM_guest_metrics.add_to_other_config")]
    Response<string> vm_guest_metrics_add_to_other_config(string session, string _vm_guest_metrics, string _key, string _value);

    [XmlRpcMethod("VM_guest_metrics.remove_from_other_config")]
    Response<string> vm_guest_metrics_remove_from_other_config(string session, string _vm_guest_metrics, string _key);

    [XmlRpcMethod("VM_guest_metrics.get_all")]
    Response<string[]> vm_guest_metrics_get_all(string session);

    [XmlRpcMethod("VM_guest_metrics.get_all_records")]
    Response<object> vm_guest_metrics_get_all_records(string session);

    [XmlRpcMethod("VMPP.get_record")]
    Response<Proxy_VMPP> vmpp_get_record(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_by_uuid")]
    Response<string> vmpp_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VMPP.create")]
    Response<string> vmpp_create(string session, Proxy_VMPP _record);

    [XmlRpcMethod("Async.VMPP.create")]
    Response<string> async_vmpp_create(string session, Proxy_VMPP _record);

    [XmlRpcMethod("VMPP.destroy")]
    Response<string> vmpp_destroy(string session, string _vmpp);

    [XmlRpcMethod("Async.VMPP.destroy")]
    Response<string> async_vmpp_destroy(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_by_name_label")]
    Response<string[]> vmpp_get_by_name_label(string session, string _label);

    [XmlRpcMethod("VMPP.get_uuid")]
    Response<string> vmpp_get_uuid(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_name_label")]
    Response<string> vmpp_get_name_label(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_name_description")]
    Response<string> vmpp_get_name_description(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_is_policy_enabled")]
    Response<bool> vmpp_get_is_policy_enabled(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_backup_type")]
    Response<string> vmpp_get_backup_type(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_backup_retention_value")]
    Response<string> vmpp_get_backup_retention_value(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_backup_frequency")]
    Response<string> vmpp_get_backup_frequency(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_backup_schedule")]
    Response<object> vmpp_get_backup_schedule(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_is_backup_running")]
    Response<bool> vmpp_get_is_backup_running(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_backup_last_run_time")]
    Response<DateTime> vmpp_get_backup_last_run_time(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_archive_target_type")]
    Response<string> vmpp_get_archive_target_type(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_archive_target_config")]
    Response<object> vmpp_get_archive_target_config(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_archive_frequency")]
    Response<string> vmpp_get_archive_frequency(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_archive_schedule")]
    Response<object> vmpp_get_archive_schedule(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_is_archive_running")]
    Response<bool> vmpp_get_is_archive_running(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_archive_last_run_time")]
    Response<DateTime> vmpp_get_archive_last_run_time(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_VMs")]
    Response<string[]> vmpp_get_vms(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_is_alarm_enabled")]
    Response<bool> vmpp_get_is_alarm_enabled(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_alarm_config")]
    Response<object> vmpp_get_alarm_config(string session, string _vmpp);

    [XmlRpcMethod("VMPP.get_recent_alerts")]
    Response<string[]> vmpp_get_recent_alerts(string session, string _vmpp);

    [XmlRpcMethod("VMPP.set_name_label")]
    Response<string> vmpp_set_name_label(string session, string _vmpp, string _label);

    [XmlRpcMethod("VMPP.set_name_description")]
    Response<string> vmpp_set_name_description(string session, string _vmpp, string _description);

    [XmlRpcMethod("VMPP.set_is_policy_enabled")]
    Response<string> vmpp_set_is_policy_enabled(string session, string _vmpp, bool _is_policy_enabled);

    [XmlRpcMethod("VMPP.set_backup_type")]
    Response<string> vmpp_set_backup_type(string session, string _vmpp, string _backup_type);

    [XmlRpcMethod("VMPP.protect_now")]
    Response<string> vmpp_protect_now(string session, string _vmpp);

    [XmlRpcMethod("VMPP.archive_now")]
    Response<string> vmpp_archive_now(string session, string _snapshot);

    [XmlRpcMethod("VMPP.get_alerts")]
    Response<string[]> vmpp_get_alerts(string session, string _vmpp, string _hours_from_now);

    [XmlRpcMethod("VMPP.set_backup_retention_value")]
    Response<string> vmpp_set_backup_retention_value(string session, string _self, string _value);

    [XmlRpcMethod("VMPP.set_backup_frequency")]
    Response<string> vmpp_set_backup_frequency(string session, string _self, string _value);

    [XmlRpcMethod("VMPP.set_backup_schedule")]
    Response<string> vmpp_set_backup_schedule(string session, string _self, object _value);

    [XmlRpcMethod("VMPP.set_archive_frequency")]
    Response<string> vmpp_set_archive_frequency(string session, string _self, string _value);

    [XmlRpcMethod("VMPP.set_archive_schedule")]
    Response<string> vmpp_set_archive_schedule(string session, string _self, object _value);

    [XmlRpcMethod("VMPP.set_archive_target_type")]
    Response<string> vmpp_set_archive_target_type(string session, string _self, string _value);

    [XmlRpcMethod("VMPP.set_archive_target_config")]
    Response<string> vmpp_set_archive_target_config(string session, string _self, object _value);

    [XmlRpcMethod("VMPP.set_is_alarm_enabled")]
    Response<string> vmpp_set_is_alarm_enabled(string session, string _self, bool _value);

    [XmlRpcMethod("VMPP.set_alarm_config")]
    Response<string> vmpp_set_alarm_config(string session, string _self, object _value);

    [XmlRpcMethod("VMPP.add_to_backup_schedule")]
    Response<string> vmpp_add_to_backup_schedule(string session, string _self, string _key, string _value);

    [XmlRpcMethod("VMPP.add_to_archive_target_config")]
    Response<string> vmpp_add_to_archive_target_config(string session, string _self, string _key, string _value);

    [XmlRpcMethod("VMPP.add_to_archive_schedule")]
    Response<string> vmpp_add_to_archive_schedule(string session, string _self, string _key, string _value);

    [XmlRpcMethod("VMPP.add_to_alarm_config")]
    Response<string> vmpp_add_to_alarm_config(string session, string _self, string _key, string _value);

    [XmlRpcMethod("VMPP.remove_from_backup_schedule")]
    Response<string> vmpp_remove_from_backup_schedule(string session, string _self, string _key);

    [XmlRpcMethod("VMPP.remove_from_archive_target_config")]
    Response<string> vmpp_remove_from_archive_target_config(string session, string _self, string _key);

    [XmlRpcMethod("VMPP.remove_from_archive_schedule")]
    Response<string> vmpp_remove_from_archive_schedule(string session, string _self, string _key);

    [XmlRpcMethod("VMPP.remove_from_alarm_config")]
    Response<string> vmpp_remove_from_alarm_config(string session, string _self, string _key);

    [XmlRpcMethod("VMPP.set_backup_last_run_time")]
    Response<string> vmpp_set_backup_last_run_time(string session, string _self, DateTime _value);

    [XmlRpcMethod("VMPP.set_archive_last_run_time")]
    Response<string> vmpp_set_archive_last_run_time(string session, string _self, DateTime _value);

    [XmlRpcMethod("VMPP.get_all")]
    Response<string[]> vmpp_get_all(string session);

    [XmlRpcMethod("VMPP.get_all_records")]
    Response<object> vmpp_get_all_records(string session);

    [XmlRpcMethod("VM_appliance.get_record")]
    Response<Proxy_VM_appliance> vm_appliance_get_record(string session, string _vm_appliance);

    [XmlRpcMethod("VM_appliance.get_by_uuid")]
    Response<string> vm_appliance_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VM_appliance.create")]
    Response<string> vm_appliance_create(string session, Proxy_VM_appliance _record);

    [XmlRpcMethod("Async.VM_appliance.create")]
    Response<string> async_vm_appliance_create(string session, Proxy_VM_appliance _record);

    [XmlRpcMethod("VM_appliance.destroy")]
    Response<string> vm_appliance_destroy(string session, string _vm_appliance);

    [XmlRpcMethod("Async.VM_appliance.destroy")]
    Response<string> async_vm_appliance_destroy(string session, string _vm_appliance);

    [XmlRpcMethod("VM_appliance.get_by_name_label")]
    Response<string[]> vm_appliance_get_by_name_label(string session, string _label);

    [XmlRpcMethod("VM_appliance.get_uuid")]
    Response<string> vm_appliance_get_uuid(string session, string _vm_appliance);

    [XmlRpcMethod("VM_appliance.get_name_label")]
    Response<string> vm_appliance_get_name_label(string session, string _vm_appliance);

    [XmlRpcMethod("VM_appliance.get_name_description")]
    Response<string> vm_appliance_get_name_description(string session, string _vm_appliance);

    [XmlRpcMethod("VM_appliance.get_allowed_operations")]
    Response<string[]> vm_appliance_get_allowed_operations(string session, string _vm_appliance);

    [XmlRpcMethod("VM_appliance.get_current_operations")]
    Response<object> vm_appliance_get_current_operations(string session, string _vm_appliance);

    [XmlRpcMethod("VM_appliance.get_VMs")]
    Response<string[]> vm_appliance_get_vms(string session, string _vm_appliance);

    [XmlRpcMethod("VM_appliance.set_name_label")]
    Response<string> vm_appliance_set_name_label(string session, string _vm_appliance, string _label);

    [XmlRpcMethod("VM_appliance.set_name_description")]
    Response<string> vm_appliance_set_name_description(string session, string _vm_appliance, string _description);

    [XmlRpcMethod("VM_appliance.start")]
    Response<string> vm_appliance_start(string session, string _self, bool _paused);

    [XmlRpcMethod("Async.VM_appliance.start")]
    Response<string> async_vm_appliance_start(string session, string _self, bool _paused);

    [XmlRpcMethod("VM_appliance.clean_shutdown")]
    Response<string> vm_appliance_clean_shutdown(string session, string _self);

    [XmlRpcMethod("Async.VM_appliance.clean_shutdown")]
    Response<string> async_vm_appliance_clean_shutdown(string session, string _self);

    [XmlRpcMethod("VM_appliance.hard_shutdown")]
    Response<string> vm_appliance_hard_shutdown(string session, string _self);

    [XmlRpcMethod("Async.VM_appliance.hard_shutdown")]
    Response<string> async_vm_appliance_hard_shutdown(string session, string _self);

    [XmlRpcMethod("VM_appliance.shutdown")]
    Response<string> vm_appliance_shutdown(string session, string _self);

    [XmlRpcMethod("Async.VM_appliance.shutdown")]
    Response<string> async_vm_appliance_shutdown(string session, string _self);

    [XmlRpcMethod("VM_appliance.assert_can_be_recovered")]
    Response<string> vm_appliance_assert_can_be_recovered(string session, string _self, string _session_to);

    [XmlRpcMethod("Async.VM_appliance.assert_can_be_recovered")]
    Response<string> async_vm_appliance_assert_can_be_recovered(string session, string _self, string _session_to);

    [XmlRpcMethod("VM_appliance.recover")]
    Response<string> vm_appliance_recover(string session, string _self, string _session_to, bool _force);

    [XmlRpcMethod("Async.VM_appliance.recover")]
    Response<string> async_vm_appliance_recover(string session, string _self, string _session_to, bool _force);

    [XmlRpcMethod("VM_appliance.get_all")]
    Response<string[]> vm_appliance_get_all(string session);

    [XmlRpcMethod("VM_appliance.get_all_records")]
    Response<object> vm_appliance_get_all_records(string session);

    [XmlRpcMethod("DR_task.get_record")]
    Response<Proxy_DR_task> dr_task_get_record(string session, string _dr_task);

    [XmlRpcMethod("DR_task.get_by_uuid")]
    Response<string> dr_task_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("DR_task.get_uuid")]
    Response<string> dr_task_get_uuid(string session, string _dr_task);

    [XmlRpcMethod("DR_task.get_introduced_SRs")]
    Response<string[]> dr_task_get_introduced_srs(string session, string _dr_task);

    [XmlRpcMethod("DR_task.create")]
    Response<string> dr_task_create(string session, string _type, object _device_config, string[] _whitelist);

    [XmlRpcMethod("Async.DR_task.create")]
    Response<string> async_dr_task_create(string session, string _type, object _device_config, string[] _whitelist);

    [XmlRpcMethod("DR_task.destroy")]
    Response<string> dr_task_destroy(string session, string _self);

    [XmlRpcMethod("Async.DR_task.destroy")]
    Response<string> async_dr_task_destroy(string session, string _self);

    [XmlRpcMethod("DR_task.get_all")]
    Response<string[]> dr_task_get_all(string session);

    [XmlRpcMethod("DR_task.get_all_records")]
    Response<object> dr_task_get_all_records(string session);

    [XmlRpcMethod("host.get_record")]
    Response<Proxy_Host> host_get_record(string session, string _host);

    [XmlRpcMethod("host.get_by_uuid")]
    Response<string> host_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("host.get_by_name_label")]
    Response<string[]> host_get_by_name_label(string session, string _label);

    [XmlRpcMethod("host.get_uuid")]
    Response<string> host_get_uuid(string session, string _host);

    [XmlRpcMethod("host.get_name_label")]
    Response<string> host_get_name_label(string session, string _host);

    [XmlRpcMethod("host.get_name_description")]
    Response<string> host_get_name_description(string session, string _host);

    [XmlRpcMethod("host.get_memory_overhead")]
    Response<string> host_get_memory_overhead(string session, string _host);

    [XmlRpcMethod("host.get_allowed_operations")]
    Response<string[]> host_get_allowed_operations(string session, string _host);

    [XmlRpcMethod("host.get_current_operations")]
    Response<object> host_get_current_operations(string session, string _host);

    [XmlRpcMethod("host.get_API_version_major")]
    Response<string> host_get_api_version_major(string session, string _host);

    [XmlRpcMethod("host.get_API_version_minor")]
    Response<string> host_get_api_version_minor(string session, string _host);

    [XmlRpcMethod("host.get_API_version_vendor")]
    Response<string> host_get_api_version_vendor(string session, string _host);

    [XmlRpcMethod("host.get_API_version_vendor_implementation")]
    Response<object> host_get_api_version_vendor_implementation(string session, string _host);

    [XmlRpcMethod("host.get_enabled")]
    Response<bool> host_get_enabled(string session, string _host);

    [XmlRpcMethod("host.get_software_version")]
    Response<object> host_get_software_version(string session, string _host);

    [XmlRpcMethod("host.get_other_config")]
    Response<object> host_get_other_config(string session, string _host);

    [XmlRpcMethod("host.get_capabilities")]
    Response<string[]> host_get_capabilities(string session, string _host);

    [XmlRpcMethod("host.get_cpu_configuration")]
    Response<object> host_get_cpu_configuration(string session, string _host);

    [XmlRpcMethod("host.get_sched_policy")]
    Response<string> host_get_sched_policy(string session, string _host);

    [XmlRpcMethod("host.get_supported_bootloaders")]
    Response<string[]> host_get_supported_bootloaders(string session, string _host);

    [XmlRpcMethod("host.get_resident_VMs")]
    Response<string[]> host_get_resident_vms(string session, string _host);

    [XmlRpcMethod("host.get_logging")]
    Response<object> host_get_logging(string session, string _host);

    [XmlRpcMethod("host.get_PIFs")]
    Response<string[]> host_get_pifs(string session, string _host);

    [XmlRpcMethod("host.get_suspend_image_sr")]
    Response<string> host_get_suspend_image_sr(string session, string _host);

    [XmlRpcMethod("host.get_crash_dump_sr")]
    Response<string> host_get_crash_dump_sr(string session, string _host);

    [XmlRpcMethod("host.get_crashdumps")]
    Response<string[]> host_get_crashdumps(string session, string _host);

    [XmlRpcMethod("host.get_patches")]
    Response<string[]> host_get_patches(string session, string _host);

    [XmlRpcMethod("host.get_PBDs")]
    Response<string[]> host_get_pbds(string session, string _host);

    [XmlRpcMethod("host.get_host_CPUs")]
    Response<string[]> host_get_host_cpus(string session, string _host);

    [XmlRpcMethod("host.get_cpu_info")]
    Response<object> host_get_cpu_info(string session, string _host);

    [XmlRpcMethod("host.get_hostname")]
    Response<string> host_get_hostname(string session, string _host);

    [XmlRpcMethod("host.get_address")]
    Response<string> host_get_address(string session, string _host);

    [XmlRpcMethod("host.get_metrics")]
    Response<string> host_get_metrics(string session, string _host);

    [XmlRpcMethod("host.get_license_params")]
    Response<object> host_get_license_params(string session, string _host);

    [XmlRpcMethod("host.get_ha_statefiles")]
    Response<string[]> host_get_ha_statefiles(string session, string _host);

    [XmlRpcMethod("host.get_ha_network_peers")]
    Response<string[]> host_get_ha_network_peers(string session, string _host);

    [XmlRpcMethod("host.get_blobs")]
    Response<object> host_get_blobs(string session, string _host);

    [XmlRpcMethod("host.get_tags")]
    Response<string[]> host_get_tags(string session, string _host);

    [XmlRpcMethod("host.get_external_auth_type")]
    Response<string> host_get_external_auth_type(string session, string _host);

    [XmlRpcMethod("host.get_external_auth_service_name")]
    Response<string> host_get_external_auth_service_name(string session, string _host);

    [XmlRpcMethod("host.get_external_auth_configuration")]
    Response<object> host_get_external_auth_configuration(string session, string _host);

    [XmlRpcMethod("host.get_edition")]
    Response<string> host_get_edition(string session, string _host);

    [XmlRpcMethod("host.get_license_server")]
    Response<object> host_get_license_server(string session, string _host);

    [XmlRpcMethod("host.get_bios_strings")]
    Response<object> host_get_bios_strings(string session, string _host);

    [XmlRpcMethod("host.get_power_on_mode")]
    Response<string> host_get_power_on_mode(string session, string _host);

    [XmlRpcMethod("host.get_power_on_config")]
    Response<object> host_get_power_on_config(string session, string _host);

    [XmlRpcMethod("host.get_local_cache_sr")]
    Response<string> host_get_local_cache_sr(string session, string _host);

    [XmlRpcMethod("host.get_chipset_info")]
    Response<object> host_get_chipset_info(string session, string _host);

    [XmlRpcMethod("host.get_PCIs")]
    Response<string[]> host_get_pcis(string session, string _host);

    [XmlRpcMethod("host.get_PGPUs")]
    Response<string[]> host_get_pgpus(string session, string _host);

    [XmlRpcMethod("host.set_name_label")]
    Response<string> host_set_name_label(string session, string _host, string _label);

    [XmlRpcMethod("host.set_name_description")]
    Response<string> host_set_name_description(string session, string _host, string _description);

    [XmlRpcMethod("host.set_other_config")]
    Response<string> host_set_other_config(string session, string _host, object _other_config);

    [XmlRpcMethod("host.add_to_other_config")]
    Response<string> host_add_to_other_config(string session, string _host, string _key, string _value);

    [XmlRpcMethod("host.remove_from_other_config")]
    Response<string> host_remove_from_other_config(string session, string _host, string _key);

    [XmlRpcMethod("host.set_logging")]
    Response<string> host_set_logging(string session, string _host, object _logging);

    [XmlRpcMethod("host.add_to_logging")]
    Response<string> host_add_to_logging(string session, string _host, string _key, string _value);

    [XmlRpcMethod("host.remove_from_logging")]
    Response<string> host_remove_from_logging(string session, string _host, string _key);

    [XmlRpcMethod("host.set_suspend_image_sr")]
    Response<string> host_set_suspend_image_sr(string session, string _host, string _suspend_image_sr);

    [XmlRpcMethod("host.set_crash_dump_sr")]
    Response<string> host_set_crash_dump_sr(string session, string _host, string _crash_dump_sr);

    [XmlRpcMethod("host.set_hostname")]
    Response<string> host_set_hostname(string session, string _host, string _hostname);

    [XmlRpcMethod("host.set_address")]
    Response<string> host_set_address(string session, string _host, string _address);

    [XmlRpcMethod("host.set_tags")]
    Response<string> host_set_tags(string session, string _host, string[] _tags);

    [XmlRpcMethod("host.add_tags")]
    Response<string> host_add_tags(string session, string _host, string _value);

    [XmlRpcMethod("host.remove_tags")]
    Response<string> host_remove_tags(string session, string _host, string _value);

    [XmlRpcMethod("host.set_license_server")]
    Response<string> host_set_license_server(string session, string _host, object _license_server);

    [XmlRpcMethod("host.add_to_license_server")]
    Response<string> host_add_to_license_server(string session, string _host, string _key, string _value);

    [XmlRpcMethod("host.remove_from_license_server")]
    Response<string> host_remove_from_license_server(string session, string _host, string _key);

    [XmlRpcMethod("host.disable")]
    Response<string> host_disable(string session, string _host);

    [XmlRpcMethod("Async.host.disable")]
    Response<string> async_host_disable(string session, string _host);

    [XmlRpcMethod("host.enable")]
    Response<string> host_enable(string session, string _host);

    [XmlRpcMethod("Async.host.enable")]
    Response<string> async_host_enable(string session, string _host);

    [XmlRpcMethod("host.shutdown")]
    Response<string> host_shutdown(string session, string _host);

    [XmlRpcMethod("Async.host.shutdown")]
    Response<string> async_host_shutdown(string session, string _host);

    [XmlRpcMethod("host.reboot")]
    Response<string> host_reboot(string session, string _host);

    [XmlRpcMethod("Async.host.reboot")]
    Response<string> async_host_reboot(string session, string _host);

    [XmlRpcMethod("host.dmesg")]
    Response<string> host_dmesg(string session, string _host);

    [XmlRpcMethod("Async.host.dmesg")]
    Response<string> async_host_dmesg(string session, string _host);

    [XmlRpcMethod("host.dmesg_clear")]
    Response<string> host_dmesg_clear(string session, string _host);

    [XmlRpcMethod("Async.host.dmesg_clear")]
    Response<string> async_host_dmesg_clear(string session, string _host);

    [XmlRpcMethod("host.get_log")]
    Response<string> host_get_log(string session, string _host);

    [XmlRpcMethod("Async.host.get_log")]
    Response<string> async_host_get_log(string session, string _host);

    [XmlRpcMethod("host.send_debug_keys")]
    Response<string> host_send_debug_keys(string session, string _host, string _keys);

    [XmlRpcMethod("Async.host.send_debug_keys")]
    Response<string> async_host_send_debug_keys(string session, string _host, string _keys);

    [XmlRpcMethod("host.bugreport_upload")]
    Response<string> host_bugreport_upload(string session, string _host, string _url, object _options);

    [XmlRpcMethod("Async.host.bugreport_upload")]
    Response<string> async_host_bugreport_upload(string session, string _host, string _url, object _options);

    [XmlRpcMethod("host.list_methods")]
    Response<string[]> host_list_methods(string session);

    [XmlRpcMethod("host.license_apply")]
    Response<string> host_license_apply(string session, string _host, string _contents);

    [XmlRpcMethod("Async.host.license_apply")]
    Response<string> async_host_license_apply(string session, string _host, string _contents);

    [XmlRpcMethod("host.destroy")]
    Response<string> host_destroy(string session, string _self);

    [XmlRpcMethod("Async.host.destroy")]
    Response<string> async_host_destroy(string session, string _self);

    [XmlRpcMethod("host.power_on")]
    Response<string> host_power_on(string session, string _host);

    [XmlRpcMethod("Async.host.power_on")]
    Response<string> async_host_power_on(string session, string _host);

    [XmlRpcMethod("host.emergency_ha_disable")]
    Response<string> host_emergency_ha_disable(string session);

    [XmlRpcMethod("host.get_data_sources")]
    Response<Proxy_Data_source[]> host_get_data_sources(string session, string _host);

    [XmlRpcMethod("host.record_data_source")]
    Response<string> host_record_data_source(string session, string _host, string _data_source);

    [XmlRpcMethod("host.query_data_source")]
    Response<double> host_query_data_source(string session, string _host, string _data_source);

    [XmlRpcMethod("host.forget_data_source_archives")]
    Response<string> host_forget_data_source_archives(string session, string _host, string _data_source);

    [XmlRpcMethod("host.assert_can_evacuate")]
    Response<string> host_assert_can_evacuate(string session, string _host);

    [XmlRpcMethod("Async.host.assert_can_evacuate")]
    Response<string> async_host_assert_can_evacuate(string session, string _host);

    [XmlRpcMethod("host.get_vms_which_prevent_evacuation")]
    Response<object> host_get_vms_which_prevent_evacuation(string session, string _self);

    [XmlRpcMethod("Async.host.get_vms_which_prevent_evacuation")]
    Response<string> async_host_get_vms_which_prevent_evacuation(string session, string _self);

    [XmlRpcMethod("host.evacuate")]
    Response<string> host_evacuate(string session, string _host);

    [XmlRpcMethod("Async.host.evacuate")]
    Response<string> async_host_evacuate(string session, string _host);

    [XmlRpcMethod("host.syslog_reconfigure")]
    Response<string> host_syslog_reconfigure(string session, string _host);

    [XmlRpcMethod("Async.host.syslog_reconfigure")]
    Response<string> async_host_syslog_reconfigure(string session, string _host);

    [XmlRpcMethod("host.management_reconfigure")]
    Response<string> host_management_reconfigure(string session, string _pif);

    [XmlRpcMethod("Async.host.management_reconfigure")]
    Response<string> async_host_management_reconfigure(string session, string _pif);

    [XmlRpcMethod("host.local_management_reconfigure")]
    Response<string> host_local_management_reconfigure(string session, string _interface);

    [XmlRpcMethod("host.management_disable")]
    Response<string> host_management_disable(string session);

    [XmlRpcMethod("host.get_management_interface")]
    Response<string> host_get_management_interface(string session, string _host);

    [XmlRpcMethod("Async.host.get_management_interface")]
    Response<string> async_host_get_management_interface(string session, string _host);

    [XmlRpcMethod("host.get_system_status_capabilities")]
    Response<string> host_get_system_status_capabilities(string session, string _host);

    [XmlRpcMethod("host.restart_agent")]
    Response<string> host_restart_agent(string session, string _host);

    [XmlRpcMethod("Async.host.restart_agent")]
    Response<string> async_host_restart_agent(string session, string _host);

    [XmlRpcMethod("host.shutdown_agent")]
    Response<string> host_shutdown_agent(string session);

    [XmlRpcMethod("host.set_hostname_live")]
    Response<string> host_set_hostname_live(string session, string _host, string _hostname);

    [XmlRpcMethod("host.compute_free_memory")]
    Response<string> host_compute_free_memory(string session, string _host);

    [XmlRpcMethod("Async.host.compute_free_memory")]
    Response<string> async_host_compute_free_memory(string session, string _host);

    [XmlRpcMethod("host.compute_memory_overhead")]
    Response<string> host_compute_memory_overhead(string session, string _host);

    [XmlRpcMethod("Async.host.compute_memory_overhead")]
    Response<string> async_host_compute_memory_overhead(string session, string _host);

    [XmlRpcMethod("host.sync_data")]
    Response<string> host_sync_data(string session, string _host);

    [XmlRpcMethod("host.backup_rrds")]
    Response<string> host_backup_rrds(string session, string _host, double _delay);

    [XmlRpcMethod("host.create_new_blob")]
    Response<string> host_create_new_blob(string session, string _host, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("Async.host.create_new_blob")]
    Response<string> async_host_create_new_blob(string session, string _host, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("host.call_plugin")]
    Response<string> host_call_plugin(string session, string _host, string _plugin, string _fn, object _args);

    [XmlRpcMethod("Async.host.call_plugin")]
    Response<string> async_host_call_plugin(string session, string _host, string _plugin, string _fn, object _args);

    [XmlRpcMethod("host.get_servertime")]
    Response<DateTime> host_get_servertime(string session, string _host);

    [XmlRpcMethod("host.get_server_localtime")]
    Response<DateTime> host_get_server_localtime(string session, string _host);

    [XmlRpcMethod("host.enable_external_auth")]
    Response<string> host_enable_external_auth(string session, string _host, object _config, string _service_name, string _auth_type);

    [XmlRpcMethod("host.disable_external_auth")]
    Response<string> host_disable_external_auth(string session, string _host, object _config);

    [XmlRpcMethod("host.retrieve_wlb_evacuate_recommendations")]
    Response<object> host_retrieve_wlb_evacuate_recommendations(string session, string _self);

    [XmlRpcMethod("Async.host.retrieve_wlb_evacuate_recommendations")]
    Response<string> async_host_retrieve_wlb_evacuate_recommendations(string session, string _self);

    [XmlRpcMethod("host.get_server_certificate")]
    Response<string> host_get_server_certificate(string session, string _host);

    [XmlRpcMethod("Async.host.get_server_certificate")]
    Response<string> async_host_get_server_certificate(string session, string _host);

    [XmlRpcMethod("host.apply_edition")]
    Response<string> host_apply_edition(string session, string _host, string _edition);

    [XmlRpcMethod("host.refresh_pack_info")]
    Response<string> host_refresh_pack_info(string session, string _host);

    [XmlRpcMethod("Async.host.refresh_pack_info")]
    Response<string> async_host_refresh_pack_info(string session, string _host);

    [XmlRpcMethod("host.set_power_on_mode")]
    Response<string> host_set_power_on_mode(string session, string _self, string _power_on_mode, object _power_on_config);

    [XmlRpcMethod("Async.host.set_power_on_mode")]
    Response<string> async_host_set_power_on_mode(string session, string _self, string _power_on_mode, object _power_on_config);

    [XmlRpcMethod("host.set_cpu_features")]
    Response<string> host_set_cpu_features(string session, string _host, string _features);

    [XmlRpcMethod("host.reset_cpu_features")]
    Response<string> host_reset_cpu_features(string session, string _host);

    [XmlRpcMethod("host.enable_local_storage_caching")]
    Response<string> host_enable_local_storage_caching(string session, string _host, string _sr);

    [XmlRpcMethod("host.disable_local_storage_caching")]
    Response<string> host_disable_local_storage_caching(string session, string _host);

    [XmlRpcMethod("host.migrate_receive")]
    Response<object> host_migrate_receive(string session, string _host, string _network, object _options);

    [XmlRpcMethod("Async.host.migrate_receive")]
    Response<string> async_host_migrate_receive(string session, string _host, string _network, object _options);

    [XmlRpcMethod("host.get_all")]
    Response<string[]> host_get_all(string session);

    [XmlRpcMethod("host.get_all_records")]
    Response<object> host_get_all_records(string session);

    [XmlRpcMethod("host_crashdump.get_record")]
    Response<Proxy_Host_crashdump> host_crashdump_get_record(string session, string _host_crashdump);

    [XmlRpcMethod("host_crashdump.get_by_uuid")]
    Response<string> host_crashdump_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("host_crashdump.get_uuid")]
    Response<string> host_crashdump_get_uuid(string session, string _host_crashdump);

    [XmlRpcMethod("host_crashdump.get_host")]
    Response<string> host_crashdump_get_host(string session, string _host_crashdump);

    [XmlRpcMethod("host_crashdump.get_timestamp")]
    Response<DateTime> host_crashdump_get_timestamp(string session, string _host_crashdump);

    [XmlRpcMethod("host_crashdump.get_size")]
    Response<string> host_crashdump_get_size(string session, string _host_crashdump);

    [XmlRpcMethod("host_crashdump.get_other_config")]
    Response<object> host_crashdump_get_other_config(string session, string _host_crashdump);

    [XmlRpcMethod("host_crashdump.set_other_config")]
    Response<string> host_crashdump_set_other_config(string session, string _host_crashdump, object _other_config);

    [XmlRpcMethod("host_crashdump.add_to_other_config")]
    Response<string> host_crashdump_add_to_other_config(string session, string _host_crashdump, string _key, string _value);

    [XmlRpcMethod("host_crashdump.remove_from_other_config")]
    Response<string> host_crashdump_remove_from_other_config(string session, string _host_crashdump, string _key);

    [XmlRpcMethod("host_crashdump.destroy")]
    Response<string> host_crashdump_destroy(string session, string _self);

    [XmlRpcMethod("Async.host_crashdump.destroy")]
    Response<string> async_host_crashdump_destroy(string session, string _self);

    [XmlRpcMethod("host_crashdump.upload")]
    Response<string> host_crashdump_upload(string session, string _self, string _url, object _options);

    [XmlRpcMethod("Async.host_crashdump.upload")]
    Response<string> async_host_crashdump_upload(string session, string _self, string _url, object _options);

    [XmlRpcMethod("host_crashdump.get_all")]
    Response<string[]> host_crashdump_get_all(string session);

    [XmlRpcMethod("host_crashdump.get_all_records")]
    Response<object> host_crashdump_get_all_records(string session);

    [XmlRpcMethod("host_patch.get_record")]
    Response<Proxy_Host_patch> host_patch_get_record(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_by_uuid")]
    Response<string> host_patch_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("host_patch.get_by_name_label")]
    Response<string[]> host_patch_get_by_name_label(string session, string _label);

    [XmlRpcMethod("host_patch.get_uuid")]
    Response<string> host_patch_get_uuid(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_name_label")]
    Response<string> host_patch_get_name_label(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_name_description")]
    Response<string> host_patch_get_name_description(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_version")]
    Response<string> host_patch_get_version(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_host")]
    Response<string> host_patch_get_host(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_applied")]
    Response<bool> host_patch_get_applied(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_timestamp_applied")]
    Response<DateTime> host_patch_get_timestamp_applied(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_size")]
    Response<string> host_patch_get_size(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_pool_patch")]
    Response<string> host_patch_get_pool_patch(string session, string _host_patch);

    [XmlRpcMethod("host_patch.get_other_config")]
    Response<object> host_patch_get_other_config(string session, string _host_patch);

    [XmlRpcMethod("host_patch.set_other_config")]
    Response<string> host_patch_set_other_config(string session, string _host_patch, object _other_config);

    [XmlRpcMethod("host_patch.add_to_other_config")]
    Response<string> host_patch_add_to_other_config(string session, string _host_patch, string _key, string _value);

    [XmlRpcMethod("host_patch.remove_from_other_config")]
    Response<string> host_patch_remove_from_other_config(string session, string _host_patch, string _key);

    [XmlRpcMethod("host_patch.destroy")]
    Response<string> host_patch_destroy(string session, string _self);

    [XmlRpcMethod("Async.host_patch.destroy")]
    Response<string> async_host_patch_destroy(string session, string _self);

    [XmlRpcMethod("host_patch.apply")]
    Response<string> host_patch_apply(string session, string _self);

    [XmlRpcMethod("Async.host_patch.apply")]
    Response<string> async_host_patch_apply(string session, string _self);

    [XmlRpcMethod("host_patch.get_all")]
    Response<string[]> host_patch_get_all(string session);

    [XmlRpcMethod("host_patch.get_all_records")]
    Response<object> host_patch_get_all_records(string session);

    [XmlRpcMethod("host_metrics.get_record")]
    Response<Proxy_Host_metrics> host_metrics_get_record(string session, string _host_metrics);

    [XmlRpcMethod("host_metrics.get_by_uuid")]
    Response<string> host_metrics_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("host_metrics.get_uuid")]
    Response<string> host_metrics_get_uuid(string session, string _host_metrics);

    [XmlRpcMethod("host_metrics.get_memory_total")]
    Response<string> host_metrics_get_memory_total(string session, string _host_metrics);

    [XmlRpcMethod("host_metrics.get_memory_free")]
    Response<string> host_metrics_get_memory_free(string session, string _host_metrics);

    [XmlRpcMethod("host_metrics.get_live")]
    Response<bool> host_metrics_get_live(string session, string _host_metrics);

    [XmlRpcMethod("host_metrics.get_last_updated")]
    Response<DateTime> host_metrics_get_last_updated(string session, string _host_metrics);

    [XmlRpcMethod("host_metrics.get_other_config")]
    Response<object> host_metrics_get_other_config(string session, string _host_metrics);

    [XmlRpcMethod("host_metrics.set_other_config")]
    Response<string> host_metrics_set_other_config(string session, string _host_metrics, object _other_config);

    [XmlRpcMethod("host_metrics.add_to_other_config")]
    Response<string> host_metrics_add_to_other_config(string session, string _host_metrics, string _key, string _value);

    [XmlRpcMethod("host_metrics.remove_from_other_config")]
    Response<string> host_metrics_remove_from_other_config(string session, string _host_metrics, string _key);

    [XmlRpcMethod("host_metrics.get_all")]
    Response<string[]> host_metrics_get_all(string session);

    [XmlRpcMethod("host_metrics.get_all_records")]
    Response<object> host_metrics_get_all_records(string session);

    [XmlRpcMethod("host_cpu.get_record")]
    Response<Proxy_Host_cpu> host_cpu_get_record(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_by_uuid")]
    Response<string> host_cpu_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("host_cpu.get_uuid")]
    Response<string> host_cpu_get_uuid(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_host")]
    Response<string> host_cpu_get_host(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_number")]
    Response<string> host_cpu_get_number(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_vendor")]
    Response<string> host_cpu_get_vendor(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_speed")]
    Response<string> host_cpu_get_speed(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_modelname")]
    Response<string> host_cpu_get_modelname(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_family")]
    Response<string> host_cpu_get_family(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_model")]
    Response<string> host_cpu_get_model(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_stepping")]
    Response<string> host_cpu_get_stepping(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_flags")]
    Response<string> host_cpu_get_flags(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_features")]
    Response<string> host_cpu_get_features(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_utilisation")]
    Response<double> host_cpu_get_utilisation(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.get_other_config")]
    Response<object> host_cpu_get_other_config(string session, string _host_cpu);

    [XmlRpcMethod("host_cpu.set_other_config")]
    Response<string> host_cpu_set_other_config(string session, string _host_cpu, object _other_config);

    [XmlRpcMethod("host_cpu.add_to_other_config")]
    Response<string> host_cpu_add_to_other_config(string session, string _host_cpu, string _key, string _value);

    [XmlRpcMethod("host_cpu.remove_from_other_config")]
    Response<string> host_cpu_remove_from_other_config(string session, string _host_cpu, string _key);

    [XmlRpcMethod("host_cpu.get_all")]
    Response<string[]> host_cpu_get_all(string session);

    [XmlRpcMethod("host_cpu.get_all_records")]
    Response<object> host_cpu_get_all_records(string session);

    [XmlRpcMethod("network.get_record")]
    Response<Proxy_Network> network_get_record(string session, string _network);

    [XmlRpcMethod("network.get_by_uuid")]
    Response<string> network_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("network.create")]
    Response<string> network_create(string session, Proxy_Network _record);

    [XmlRpcMethod("Async.network.create")]
    Response<string> async_network_create(string session, Proxy_Network _record);

    [XmlRpcMethod("network.destroy")]
    Response<string> network_destroy(string session, string _network);

    [XmlRpcMethod("Async.network.destroy")]
    Response<string> async_network_destroy(string session, string _network);

    [XmlRpcMethod("network.get_by_name_label")]
    Response<string[]> network_get_by_name_label(string session, string _label);

    [XmlRpcMethod("network.get_uuid")]
    Response<string> network_get_uuid(string session, string _network);

    [XmlRpcMethod("network.get_name_label")]
    Response<string> network_get_name_label(string session, string _network);

    [XmlRpcMethod("network.get_name_description")]
    Response<string> network_get_name_description(string session, string _network);

    [XmlRpcMethod("network.get_allowed_operations")]
    Response<string[]> network_get_allowed_operations(string session, string _network);

    [XmlRpcMethod("network.get_current_operations")]
    Response<object> network_get_current_operations(string session, string _network);

    [XmlRpcMethod("network.get_VIFs")]
    Response<string[]> network_get_vifs(string session, string _network);

    [XmlRpcMethod("network.get_PIFs")]
    Response<string[]> network_get_pifs(string session, string _network);

    [XmlRpcMethod("network.get_MTU")]
    Response<string> network_get_mtu(string session, string _network);

    [XmlRpcMethod("network.get_other_config")]
    Response<object> network_get_other_config(string session, string _network);

    [XmlRpcMethod("network.get_bridge")]
    Response<string> network_get_bridge(string session, string _network);

    [XmlRpcMethod("network.get_blobs")]
    Response<object> network_get_blobs(string session, string _network);

    [XmlRpcMethod("network.get_tags")]
    Response<string[]> network_get_tags(string session, string _network);

    [XmlRpcMethod("network.get_default_locking_mode")]
    Response<string> network_get_default_locking_mode(string session, string _network);

    [XmlRpcMethod("network.set_name_label")]
    Response<string> network_set_name_label(string session, string _network, string _label);

    [XmlRpcMethod("network.set_name_description")]
    Response<string> network_set_name_description(string session, string _network, string _description);

    [XmlRpcMethod("network.set_MTU")]
    Response<string> network_set_mtu(string session, string _network, string _mtu);

    [XmlRpcMethod("network.set_other_config")]
    Response<string> network_set_other_config(string session, string _network, object _other_config);

    [XmlRpcMethod("network.add_to_other_config")]
    Response<string> network_add_to_other_config(string session, string _network, string _key, string _value);

    [XmlRpcMethod("network.remove_from_other_config")]
    Response<string> network_remove_from_other_config(string session, string _network, string _key);

    [XmlRpcMethod("network.set_tags")]
    Response<string> network_set_tags(string session, string _network, string[] _tags);

    [XmlRpcMethod("network.add_tags")]
    Response<string> network_add_tags(string session, string _network, string _value);

    [XmlRpcMethod("network.remove_tags")]
    Response<string> network_remove_tags(string session, string _network, string _value);

    [XmlRpcMethod("network.create_new_blob")]
    Response<string> network_create_new_blob(string session, string _network, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("Async.network.create_new_blob")]
    Response<string> async_network_create_new_blob(string session, string _network, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("network.set_default_locking_mode")]
    Response<string> network_set_default_locking_mode(string session, string _network, string _value);

    [XmlRpcMethod("Async.network.set_default_locking_mode")]
    Response<string> async_network_set_default_locking_mode(string session, string _network, string _value);

    [XmlRpcMethod("network.get_all")]
    Response<string[]> network_get_all(string session);

    [XmlRpcMethod("network.get_all_records")]
    Response<object> network_get_all_records(string session);

    [XmlRpcMethod("VIF.get_record")]
    Response<Proxy_VIF> vif_get_record(string session, string _vif);

    [XmlRpcMethod("VIF.get_by_uuid")]
    Response<string> vif_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VIF.create")]
    Response<string> vif_create(string session, Proxy_VIF _record);

    [XmlRpcMethod("Async.VIF.create")]
    Response<string> async_vif_create(string session, Proxy_VIF _record);

    [XmlRpcMethod("VIF.destroy")]
    Response<string> vif_destroy(string session, string _vif);

    [XmlRpcMethod("Async.VIF.destroy")]
    Response<string> async_vif_destroy(string session, string _vif);

    [XmlRpcMethod("VIF.get_uuid")]
    Response<string> vif_get_uuid(string session, string _vif);

    [XmlRpcMethod("VIF.get_allowed_operations")]
    Response<string[]> vif_get_allowed_operations(string session, string _vif);

    [XmlRpcMethod("VIF.get_current_operations")]
    Response<object> vif_get_current_operations(string session, string _vif);

    [XmlRpcMethod("VIF.get_device")]
    Response<string> vif_get_device(string session, string _vif);

    [XmlRpcMethod("VIF.get_network")]
    Response<string> vif_get_network(string session, string _vif);

    [XmlRpcMethod("VIF.get_VM")]
    Response<string> vif_get_vm(string session, string _vif);

    [XmlRpcMethod("VIF.get_MAC")]
    Response<string> vif_get_mac(string session, string _vif);

    [XmlRpcMethod("VIF.get_MTU")]
    Response<string> vif_get_mtu(string session, string _vif);

    [XmlRpcMethod("VIF.get_other_config")]
    Response<object> vif_get_other_config(string session, string _vif);

    [XmlRpcMethod("VIF.get_currently_attached")]
    Response<bool> vif_get_currently_attached(string session, string _vif);

    [XmlRpcMethod("VIF.get_status_code")]
    Response<string> vif_get_status_code(string session, string _vif);

    [XmlRpcMethod("VIF.get_status_detail")]
    Response<string> vif_get_status_detail(string session, string _vif);

    [XmlRpcMethod("VIF.get_runtime_properties")]
    Response<object> vif_get_runtime_properties(string session, string _vif);

    [XmlRpcMethod("VIF.get_qos_algorithm_type")]
    Response<string> vif_get_qos_algorithm_type(string session, string _vif);

    [XmlRpcMethod("VIF.get_qos_algorithm_params")]
    Response<object> vif_get_qos_algorithm_params(string session, string _vif);

    [XmlRpcMethod("VIF.get_qos_supported_algorithms")]
    Response<string[]> vif_get_qos_supported_algorithms(string session, string _vif);

    [XmlRpcMethod("VIF.get_metrics")]
    Response<string> vif_get_metrics(string session, string _vif);

    [XmlRpcMethod("VIF.get_MAC_autogenerated")]
    Response<bool> vif_get_mac_autogenerated(string session, string _vif);

    [XmlRpcMethod("VIF.get_locking_mode")]
    Response<string> vif_get_locking_mode(string session, string _vif);

    [XmlRpcMethod("VIF.get_ipv4_allowed")]
    Response<string[]> vif_get_ipv4_allowed(string session, string _vif);

    [XmlRpcMethod("VIF.get_ipv6_allowed")]
    Response<string[]> vif_get_ipv6_allowed(string session, string _vif);

    [XmlRpcMethod("VIF.set_other_config")]
    Response<string> vif_set_other_config(string session, string _vif, object _other_config);

    [XmlRpcMethod("VIF.add_to_other_config")]
    Response<string> vif_add_to_other_config(string session, string _vif, string _key, string _value);

    [XmlRpcMethod("VIF.remove_from_other_config")]
    Response<string> vif_remove_from_other_config(string session, string _vif, string _key);

    [XmlRpcMethod("VIF.set_qos_algorithm_type")]
    Response<string> vif_set_qos_algorithm_type(string session, string _vif, string _algorithm_type);

    [XmlRpcMethod("VIF.set_qos_algorithm_params")]
    Response<string> vif_set_qos_algorithm_params(string session, string _vif, object _algorithm_params);

    [XmlRpcMethod("VIF.add_to_qos_algorithm_params")]
    Response<string> vif_add_to_qos_algorithm_params(string session, string _vif, string _key, string _value);

    [XmlRpcMethod("VIF.remove_from_qos_algorithm_params")]
    Response<string> vif_remove_from_qos_algorithm_params(string session, string _vif, string _key);

    [XmlRpcMethod("VIF.plug")]
    Response<string> vif_plug(string session, string _self);

    [XmlRpcMethod("Async.VIF.plug")]
    Response<string> async_vif_plug(string session, string _self);

    [XmlRpcMethod("VIF.unplug")]
    Response<string> vif_unplug(string session, string _self);

    [XmlRpcMethod("Async.VIF.unplug")]
    Response<string> async_vif_unplug(string session, string _self);

    [XmlRpcMethod("VIF.unplug_force")]
    Response<string> vif_unplug_force(string session, string _self);

    [XmlRpcMethod("Async.VIF.unplug_force")]
    Response<string> async_vif_unplug_force(string session, string _self);

    [XmlRpcMethod("VIF.set_locking_mode")]
    Response<string> vif_set_locking_mode(string session, string _self, string _value);

    [XmlRpcMethod("Async.VIF.set_locking_mode")]
    Response<string> async_vif_set_locking_mode(string session, string _self, string _value);

    [XmlRpcMethod("VIF.set_ipv4_allowed")]
    Response<string> vif_set_ipv4_allowed(string session, string _self, string[] _value);

    [XmlRpcMethod("Async.VIF.set_ipv4_allowed")]
    Response<string> async_vif_set_ipv4_allowed(string session, string _self, string[] _value);

    [XmlRpcMethod("VIF.add_ipv4_allowed")]
    Response<string> vif_add_ipv4_allowed(string session, string _self, string _value);

    [XmlRpcMethod("Async.VIF.add_ipv4_allowed")]
    Response<string> async_vif_add_ipv4_allowed(string session, string _self, string _value);

    [XmlRpcMethod("VIF.remove_ipv4_allowed")]
    Response<string> vif_remove_ipv4_allowed(string session, string _self, string _value);

    [XmlRpcMethod("Async.VIF.remove_ipv4_allowed")]
    Response<string> async_vif_remove_ipv4_allowed(string session, string _self, string _value);

    [XmlRpcMethod("VIF.set_ipv6_allowed")]
    Response<string> vif_set_ipv6_allowed(string session, string _self, string[] _value);

    [XmlRpcMethod("Async.VIF.set_ipv6_allowed")]
    Response<string> async_vif_set_ipv6_allowed(string session, string _self, string[] _value);

    [XmlRpcMethod("VIF.add_ipv6_allowed")]
    Response<string> vif_add_ipv6_allowed(string session, string _self, string _value);

    [XmlRpcMethod("Async.VIF.add_ipv6_allowed")]
    Response<string> async_vif_add_ipv6_allowed(string session, string _self, string _value);

    [XmlRpcMethod("VIF.remove_ipv6_allowed")]
    Response<string> vif_remove_ipv6_allowed(string session, string _self, string _value);

    [XmlRpcMethod("Async.VIF.remove_ipv6_allowed")]
    Response<string> async_vif_remove_ipv6_allowed(string session, string _self, string _value);

    [XmlRpcMethod("VIF.get_all")]
    Response<string[]> vif_get_all(string session);

    [XmlRpcMethod("VIF.get_all_records")]
    Response<object> vif_get_all_records(string session);

    [XmlRpcMethod("VIF_metrics.get_record")]
    Response<Proxy_VIF_metrics> vif_metrics_get_record(string session, string _vif_metrics);

    [XmlRpcMethod("VIF_metrics.get_by_uuid")]
    Response<string> vif_metrics_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VIF_metrics.get_uuid")]
    Response<string> vif_metrics_get_uuid(string session, string _vif_metrics);

    [XmlRpcMethod("VIF_metrics.get_io_read_kbs")]
    Response<double> vif_metrics_get_io_read_kbs(string session, string _vif_metrics);

    [XmlRpcMethod("VIF_metrics.get_io_write_kbs")]
    Response<double> vif_metrics_get_io_write_kbs(string session, string _vif_metrics);

    [XmlRpcMethod("VIF_metrics.get_last_updated")]
    Response<DateTime> vif_metrics_get_last_updated(string session, string _vif_metrics);

    [XmlRpcMethod("VIF_metrics.get_other_config")]
    Response<object> vif_metrics_get_other_config(string session, string _vif_metrics);

    [XmlRpcMethod("VIF_metrics.set_other_config")]
    Response<string> vif_metrics_set_other_config(string session, string _vif_metrics, object _other_config);

    [XmlRpcMethod("VIF_metrics.add_to_other_config")]
    Response<string> vif_metrics_add_to_other_config(string session, string _vif_metrics, string _key, string _value);

    [XmlRpcMethod("VIF_metrics.remove_from_other_config")]
    Response<string> vif_metrics_remove_from_other_config(string session, string _vif_metrics, string _key);

    [XmlRpcMethod("VIF_metrics.get_all")]
    Response<string[]> vif_metrics_get_all(string session);

    [XmlRpcMethod("VIF_metrics.get_all_records")]
    Response<object> vif_metrics_get_all_records(string session);

    [XmlRpcMethod("PIF.get_record")]
    Response<Proxy_PIF> pif_get_record(string session, string _pif);

    [XmlRpcMethod("PIF.get_by_uuid")]
    Response<string> pif_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("PIF.get_uuid")]
    Response<string> pif_get_uuid(string session, string _pif);

    [XmlRpcMethod("PIF.get_device")]
    Response<string> pif_get_device(string session, string _pif);

    [XmlRpcMethod("PIF.get_network")]
    Response<string> pif_get_network(string session, string _pif);

    [XmlRpcMethod("PIF.get_host")]
    Response<string> pif_get_host(string session, string _pif);

    [XmlRpcMethod("PIF.get_MAC")]
    Response<string> pif_get_mac(string session, string _pif);

    [XmlRpcMethod("PIF.get_MTU")]
    Response<string> pif_get_mtu(string session, string _pif);

    [XmlRpcMethod("PIF.get_VLAN")]
    Response<string> pif_get_vlan(string session, string _pif);

    [XmlRpcMethod("PIF.get_metrics")]
    Response<string> pif_get_metrics(string session, string _pif);

    [XmlRpcMethod("PIF.get_physical")]
    Response<bool> pif_get_physical(string session, string _pif);

    [XmlRpcMethod("PIF.get_currently_attached")]
    Response<bool> pif_get_currently_attached(string session, string _pif);

    [XmlRpcMethod("PIF.get_ip_configuration_mode")]
    Response<string> pif_get_ip_configuration_mode(string session, string _pif);

    [XmlRpcMethod("PIF.get_IP")]
    Response<string> pif_get_ip(string session, string _pif);

    [XmlRpcMethod("PIF.get_netmask")]
    Response<string> pif_get_netmask(string session, string _pif);

    [XmlRpcMethod("PIF.get_gateway")]
    Response<string> pif_get_gateway(string session, string _pif);

    [XmlRpcMethod("PIF.get_DNS")]
    Response<string> pif_get_dns(string session, string _pif);

    [XmlRpcMethod("PIF.get_bond_slave_of")]
    Response<string> pif_get_bond_slave_of(string session, string _pif);

    [XmlRpcMethod("PIF.get_bond_master_of")]
    Response<string[]> pif_get_bond_master_of(string session, string _pif);

    [XmlRpcMethod("PIF.get_VLAN_master_of")]
    Response<string> pif_get_vlan_master_of(string session, string _pif);

    [XmlRpcMethod("PIF.get_VLAN_slave_of")]
    Response<string[]> pif_get_vlan_slave_of(string session, string _pif);

    [XmlRpcMethod("PIF.get_management")]
    Response<bool> pif_get_management(string session, string _pif);

    [XmlRpcMethod("PIF.get_other_config")]
    Response<object> pif_get_other_config(string session, string _pif);

    [XmlRpcMethod("PIF.get_disallow_unplug")]
    Response<bool> pif_get_disallow_unplug(string session, string _pif);

    [XmlRpcMethod("PIF.get_tunnel_access_PIF_of")]
    Response<string[]> pif_get_tunnel_access_pif_of(string session, string _pif);

    [XmlRpcMethod("PIF.get_tunnel_transport_PIF_of")]
    Response<string[]> pif_get_tunnel_transport_pif_of(string session, string _pif);

    [XmlRpcMethod("PIF.get_ipv6_configuration_mode")]
    Response<string> pif_get_ipv6_configuration_mode(string session, string _pif);

    [XmlRpcMethod("PIF.get_IPv6")]
    Response<string[]> pif_get_ipv6(string session, string _pif);

    [XmlRpcMethod("PIF.get_ipv6_gateway")]
    Response<string> pif_get_ipv6_gateway(string session, string _pif);

    [XmlRpcMethod("PIF.get_primary_address_type")]
    Response<string> pif_get_primary_address_type(string session, string _pif);

    [XmlRpcMethod("PIF.set_other_config")]
    Response<string> pif_set_other_config(string session, string _pif, object _other_config);

    [XmlRpcMethod("PIF.add_to_other_config")]
    Response<string> pif_add_to_other_config(string session, string _pif, string _key, string _value);

    [XmlRpcMethod("PIF.remove_from_other_config")]
    Response<string> pif_remove_from_other_config(string session, string _pif, string _key);

    [XmlRpcMethod("PIF.set_disallow_unplug")]
    Response<string> pif_set_disallow_unplug(string session, string _pif, bool _disallow_unplug);

    [XmlRpcMethod("PIF.create_VLAN")]
    Response<string> pif_create_vlan(string session, string _device, string _network, string _host, string _vlan);

    [XmlRpcMethod("Async.PIF.create_VLAN")]
    Response<string> async_pif_create_vlan(string session, string _device, string _network, string _host, string _vlan);

    [XmlRpcMethod("PIF.destroy")]
    Response<string> pif_destroy(string session, string _self);

    [XmlRpcMethod("Async.PIF.destroy")]
    Response<string> async_pif_destroy(string session, string _self);

    [XmlRpcMethod("PIF.reconfigure_ip")]
    Response<string> pif_reconfigure_ip(string session, string _self, string _mode, string _ip, string _netmask, string _gateway, string _dns);

    [XmlRpcMethod("Async.PIF.reconfigure_ip")]
    Response<string> async_pif_reconfigure_ip(string session, string _self, string _mode, string _ip, string _netmask, string _gateway, string _dns);

    [XmlRpcMethod("PIF.reconfigure_ipv6")]
    Response<string> pif_reconfigure_ipv6(string session, string _self, string _mode, string _ipv6, string _gateway, string _dns);

    [XmlRpcMethod("Async.PIF.reconfigure_ipv6")]
    Response<string> async_pif_reconfigure_ipv6(string session, string _self, string _mode, string _ipv6, string _gateway, string _dns);

    [XmlRpcMethod("PIF.set_primary_address_type")]
    Response<string> pif_set_primary_address_type(string session, string _self, string _primary_address_type);

    [XmlRpcMethod("Async.PIF.set_primary_address_type")]
    Response<string> async_pif_set_primary_address_type(string session, string _self, string _primary_address_type);

    [XmlRpcMethod("PIF.scan")]
    Response<string> pif_scan(string session, string _host);

    [XmlRpcMethod("Async.PIF.scan")]
    Response<string> async_pif_scan(string session, string _host);

    [XmlRpcMethod("PIF.introduce")]
    Response<string> pif_introduce(string session, string _host, string _mac, string _device);

    [XmlRpcMethod("Async.PIF.introduce")]
    Response<string> async_pif_introduce(string session, string _host, string _mac, string _device);

    [XmlRpcMethod("PIF.forget")]
    Response<string> pif_forget(string session, string _self);

    [XmlRpcMethod("Async.PIF.forget")]
    Response<string> async_pif_forget(string session, string _self);

    [XmlRpcMethod("PIF.unplug")]
    Response<string> pif_unplug(string session, string _self);

    [XmlRpcMethod("Async.PIF.unplug")]
    Response<string> async_pif_unplug(string session, string _self);

    [XmlRpcMethod("PIF.plug")]
    Response<string> pif_plug(string session, string _self);

    [XmlRpcMethod("Async.PIF.plug")]
    Response<string> async_pif_plug(string session, string _self);

    [XmlRpcMethod("PIF.db_introduce")]
    Response<string> pif_db_introduce(string session, string _device, string _network, string _host, string _mac, string _mtu, string _vlan, bool _physical, string _ip_configuration_mode, string _ip, string _netmask, string _gateway, string _dns, string _bond_slave_of, string _vlan_master_of, bool _management, object _other_config, bool _disallow_unplug, string _ipv6_configuration_mode, string[] _ipv6, string _ipv6_gateway, string _primary_address_type);

    [XmlRpcMethod("Async.PIF.db_introduce")]
    Response<string> async_pif_db_introduce(string session, string _device, string _network, string _host, string _mac, string _mtu, string _vlan, bool _physical, string _ip_configuration_mode, string _ip, string _netmask, string _gateway, string _dns, string _bond_slave_of, string _vlan_master_of, bool _management, object _other_config, bool _disallow_unplug, string _ipv6_configuration_mode, string[] _ipv6, string _ipv6_gateway, string _primary_address_type);

    [XmlRpcMethod("PIF.db_forget")]
    Response<string> pif_db_forget(string session, string _self);

    [XmlRpcMethod("Async.PIF.db_forget")]
    Response<string> async_pif_db_forget(string session, string _self);

    [XmlRpcMethod("PIF.get_all")]
    Response<string[]> pif_get_all(string session);

    [XmlRpcMethod("PIF.get_all_records")]
    Response<object> pif_get_all_records(string session);

    [XmlRpcMethod("PIF_metrics.get_record")]
    Response<Proxy_PIF_metrics> pif_metrics_get_record(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_by_uuid")]
    Response<string> pif_metrics_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("PIF_metrics.get_uuid")]
    Response<string> pif_metrics_get_uuid(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_io_read_kbs")]
    Response<double> pif_metrics_get_io_read_kbs(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_io_write_kbs")]
    Response<double> pif_metrics_get_io_write_kbs(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_carrier")]
    Response<bool> pif_metrics_get_carrier(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_vendor_id")]
    Response<string> pif_metrics_get_vendor_id(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_vendor_name")]
    Response<string> pif_metrics_get_vendor_name(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_device_id")]
    Response<string> pif_metrics_get_device_id(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_device_name")]
    Response<string> pif_metrics_get_device_name(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_speed")]
    Response<string> pif_metrics_get_speed(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_duplex")]
    Response<bool> pif_metrics_get_duplex(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_pci_bus_path")]
    Response<string> pif_metrics_get_pci_bus_path(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_last_updated")]
    Response<DateTime> pif_metrics_get_last_updated(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.get_other_config")]
    Response<object> pif_metrics_get_other_config(string session, string _pif_metrics);

    [XmlRpcMethod("PIF_metrics.set_other_config")]
    Response<string> pif_metrics_set_other_config(string session, string _pif_metrics, object _other_config);

    [XmlRpcMethod("PIF_metrics.add_to_other_config")]
    Response<string> pif_metrics_add_to_other_config(string session, string _pif_metrics, string _key, string _value);

    [XmlRpcMethod("PIF_metrics.remove_from_other_config")]
    Response<string> pif_metrics_remove_from_other_config(string session, string _pif_metrics, string _key);

    [XmlRpcMethod("PIF_metrics.get_all")]
    Response<string[]> pif_metrics_get_all(string session);

    [XmlRpcMethod("PIF_metrics.get_all_records")]
    Response<object> pif_metrics_get_all_records(string session);

    [XmlRpcMethod("Bond.get_record")]
    Response<Proxy_Bond> bond_get_record(string session, string _bond);

    [XmlRpcMethod("Bond.get_by_uuid")]
    Response<string> bond_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("Bond.get_uuid")]
    Response<string> bond_get_uuid(string session, string _bond);

    [XmlRpcMethod("Bond.get_master")]
    Response<string> bond_get_master(string session, string _bond);

    [XmlRpcMethod("Bond.get_slaves")]
    Response<string[]> bond_get_slaves(string session, string _bond);

    [XmlRpcMethod("Bond.get_other_config")]
    Response<object> bond_get_other_config(string session, string _bond);

    [XmlRpcMethod("Bond.get_primary_slave")]
    Response<string> bond_get_primary_slave(string session, string _bond);

    [XmlRpcMethod("Bond.get_mode")]
    Response<string> bond_get_mode(string session, string _bond);

    [XmlRpcMethod("Bond.get_properties")]
    Response<object> bond_get_properties(string session, string _bond);

    [XmlRpcMethod("Bond.get_links_up")]
    Response<string> bond_get_links_up(string session, string _bond);

    [XmlRpcMethod("Bond.set_other_config")]
    Response<string> bond_set_other_config(string session, string _bond, object _other_config);

    [XmlRpcMethod("Bond.add_to_other_config")]
    Response<string> bond_add_to_other_config(string session, string _bond, string _key, string _value);

    [XmlRpcMethod("Bond.remove_from_other_config")]
    Response<string> bond_remove_from_other_config(string session, string _bond, string _key);

    [XmlRpcMethod("Bond.create")]
    Response<string> bond_create(string session, string _network, string[] _members, string _mac, string _mode, object _properties);

    [XmlRpcMethod("Async.Bond.create")]
    Response<string> async_bond_create(string session, string _network, string[] _members, string _mac, string _mode, object _properties);

    [XmlRpcMethod("Bond.destroy")]
    Response<string> bond_destroy(string session, string _self);

    [XmlRpcMethod("Async.Bond.destroy")]
    Response<string> async_bond_destroy(string session, string _self);

    [XmlRpcMethod("Bond.set_mode")]
    Response<string> bond_set_mode(string session, string _self, string _value);

    [XmlRpcMethod("Async.Bond.set_mode")]
    Response<string> async_bond_set_mode(string session, string _self, string _value);

    [XmlRpcMethod("Bond.set_property")]
    Response<string> bond_set_property(string session, string _self, string _name, string _value);

    [XmlRpcMethod("Async.Bond.set_property")]
    Response<string> async_bond_set_property(string session, string _self, string _name, string _value);

    [XmlRpcMethod("Bond.get_all")]
    Response<string[]> bond_get_all(string session);

    [XmlRpcMethod("Bond.get_all_records")]
    Response<object> bond_get_all_records(string session);

    [XmlRpcMethod("VLAN.get_record")]
    Response<Proxy_VLAN> vlan_get_record(string session, string _vlan);

    [XmlRpcMethod("VLAN.get_by_uuid")]
    Response<string> vlan_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VLAN.get_uuid")]
    Response<string> vlan_get_uuid(string session, string _vlan);

    [XmlRpcMethod("VLAN.get_tagged_PIF")]
    Response<string> vlan_get_tagged_pif(string session, string _vlan);

    [XmlRpcMethod("VLAN.get_untagged_PIF")]
    Response<string> vlan_get_untagged_pif(string session, string _vlan);

    [XmlRpcMethod("VLAN.get_tag")]
    Response<string> vlan_get_tag(string session, string _vlan);

    [XmlRpcMethod("VLAN.get_other_config")]
    Response<object> vlan_get_other_config(string session, string _vlan);

    [XmlRpcMethod("VLAN.set_other_config")]
    Response<string> vlan_set_other_config(string session, string _vlan, object _other_config);

    [XmlRpcMethod("VLAN.add_to_other_config")]
    Response<string> vlan_add_to_other_config(string session, string _vlan, string _key, string _value);

    [XmlRpcMethod("VLAN.remove_from_other_config")]
    Response<string> vlan_remove_from_other_config(string session, string _vlan, string _key);

    [XmlRpcMethod("VLAN.create")]
    Response<string> vlan_create(string session, string _tagged_pif, string _tag, string _network);

    [XmlRpcMethod("Async.VLAN.create")]
    Response<string> async_vlan_create(string session, string _tagged_pif, string _tag, string _network);

    [XmlRpcMethod("VLAN.destroy")]
    Response<string> vlan_destroy(string session, string _self);

    [XmlRpcMethod("Async.VLAN.destroy")]
    Response<string> async_vlan_destroy(string session, string _self);

    [XmlRpcMethod("VLAN.get_all")]
    Response<string[]> vlan_get_all(string session);

    [XmlRpcMethod("VLAN.get_all_records")]
    Response<object> vlan_get_all_records(string session);

    [XmlRpcMethod("SM.get_record")]
    Response<Proxy_SM> sm_get_record(string session, string _sm);

    [XmlRpcMethod("SM.get_by_uuid")]
    Response<string> sm_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("SM.get_by_name_label")]
    Response<string[]> sm_get_by_name_label(string session, string _label);

    [XmlRpcMethod("SM.get_uuid")]
    Response<string> sm_get_uuid(string session, string _sm);

    [XmlRpcMethod("SM.get_name_label")]
    Response<string> sm_get_name_label(string session, string _sm);

    [XmlRpcMethod("SM.get_name_description")]
    Response<string> sm_get_name_description(string session, string _sm);

    [XmlRpcMethod("SM.get_type")]
    Response<string> sm_get_type(string session, string _sm);

    [XmlRpcMethod("SM.get_vendor")]
    Response<string> sm_get_vendor(string session, string _sm);

    [XmlRpcMethod("SM.get_copyright")]
    Response<string> sm_get_copyright(string session, string _sm);

    [XmlRpcMethod("SM.get_version")]
    Response<string> sm_get_version(string session, string _sm);

    [XmlRpcMethod("SM.get_required_api_version")]
    Response<string> sm_get_required_api_version(string session, string _sm);

    [XmlRpcMethod("SM.get_configuration")]
    Response<object> sm_get_configuration(string session, string _sm);

    [XmlRpcMethod("SM.get_capabilities")]
    Response<string[]> sm_get_capabilities(string session, string _sm);

    [XmlRpcMethod("SM.get_other_config")]
    Response<object> sm_get_other_config(string session, string _sm);

    [XmlRpcMethod("SM.get_driver_filename")]
    Response<string> sm_get_driver_filename(string session, string _sm);

    [XmlRpcMethod("SM.set_other_config")]
    Response<string> sm_set_other_config(string session, string _sm, object _other_config);

    [XmlRpcMethod("SM.add_to_other_config")]
    Response<string> sm_add_to_other_config(string session, string _sm, string _key, string _value);

    [XmlRpcMethod("SM.remove_from_other_config")]
    Response<string> sm_remove_from_other_config(string session, string _sm, string _key);

    [XmlRpcMethod("SM.get_all")]
    Response<string[]> sm_get_all(string session);

    [XmlRpcMethod("SM.get_all_records")]
    Response<object> sm_get_all_records(string session);

    [XmlRpcMethod("SR.get_record")]
    Response<Proxy_SR> sr_get_record(string session, string _sr);

    [XmlRpcMethod("SR.get_by_uuid")]
    Response<string> sr_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("SR.get_by_name_label")]
    Response<string[]> sr_get_by_name_label(string session, string _label);

    [XmlRpcMethod("SR.get_uuid")]
    Response<string> sr_get_uuid(string session, string _sr);

    [XmlRpcMethod("SR.get_name_label")]
    Response<string> sr_get_name_label(string session, string _sr);

    [XmlRpcMethod("SR.get_name_description")]
    Response<string> sr_get_name_description(string session, string _sr);

    [XmlRpcMethod("SR.get_allowed_operations")]
    Response<string[]> sr_get_allowed_operations(string session, string _sr);

    [XmlRpcMethod("SR.get_current_operations")]
    Response<object> sr_get_current_operations(string session, string _sr);

    [XmlRpcMethod("SR.get_VDIs")]
    Response<string[]> sr_get_vdis(string session, string _sr);

    [XmlRpcMethod("SR.get_PBDs")]
    Response<string[]> sr_get_pbds(string session, string _sr);

    [XmlRpcMethod("SR.get_virtual_allocation")]
    Response<string> sr_get_virtual_allocation(string session, string _sr);

    [XmlRpcMethod("SR.get_physical_utilisation")]
    Response<string> sr_get_physical_utilisation(string session, string _sr);

    [XmlRpcMethod("SR.get_physical_size")]
    Response<string> sr_get_physical_size(string session, string _sr);

    [XmlRpcMethod("SR.get_type")]
    Response<string> sr_get_type(string session, string _sr);

    [XmlRpcMethod("SR.get_content_type")]
    Response<string> sr_get_content_type(string session, string _sr);

    [XmlRpcMethod("SR.get_shared")]
    Response<bool> sr_get_shared(string session, string _sr);

    [XmlRpcMethod("SR.get_other_config")]
    Response<object> sr_get_other_config(string session, string _sr);

    [XmlRpcMethod("SR.get_tags")]
    Response<string[]> sr_get_tags(string session, string _sr);

    [XmlRpcMethod("SR.get_sm_config")]
    Response<object> sr_get_sm_config(string session, string _sr);

    [XmlRpcMethod("SR.get_blobs")]
    Response<object> sr_get_blobs(string session, string _sr);

    [XmlRpcMethod("SR.get_local_cache_enabled")]
    Response<bool> sr_get_local_cache_enabled(string session, string _sr);

    [XmlRpcMethod("SR.get_introduced_by")]
    Response<string> sr_get_introduced_by(string session, string _sr);

    [XmlRpcMethod("SR.set_other_config")]
    Response<string> sr_set_other_config(string session, string _sr, object _other_config);

    [XmlRpcMethod("SR.add_to_other_config")]
    Response<string> sr_add_to_other_config(string session, string _sr, string _key, string _value);

    [XmlRpcMethod("SR.remove_from_other_config")]
    Response<string> sr_remove_from_other_config(string session, string _sr, string _key);

    [XmlRpcMethod("SR.set_tags")]
    Response<string> sr_set_tags(string session, string _sr, string[] _tags);

    [XmlRpcMethod("SR.add_tags")]
    Response<string> sr_add_tags(string session, string _sr, string _value);

    [XmlRpcMethod("SR.remove_tags")]
    Response<string> sr_remove_tags(string session, string _sr, string _value);

    [XmlRpcMethod("SR.set_sm_config")]
    Response<string> sr_set_sm_config(string session, string _sr, object _sm_config);

    [XmlRpcMethod("SR.add_to_sm_config")]
    Response<string> sr_add_to_sm_config(string session, string _sr, string _key, string _value);

    [XmlRpcMethod("SR.remove_from_sm_config")]
    Response<string> sr_remove_from_sm_config(string session, string _sr, string _key);

    [XmlRpcMethod("SR.create")]
    Response<string> sr_create(string session, string _host, object _device_config, string _physical_size, string _name_label, string _name_description, string _type, string _content_type, bool _shared, object _sm_config);

    [XmlRpcMethod("Async.SR.create")]
    Response<string> async_sr_create(string session, string _host, object _device_config, string _physical_size, string _name_label, string _name_description, string _type, string _content_type, bool _shared, object _sm_config);

    [XmlRpcMethod("SR.introduce")]
    Response<string> sr_introduce(string session, string _uuid, string _name_label, string _name_description, string _type, string _content_type, bool _shared, object _sm_config);

    [XmlRpcMethod("Async.SR.introduce")]
    Response<string> async_sr_introduce(string session, string _uuid, string _name_label, string _name_description, string _type, string _content_type, bool _shared, object _sm_config);

    [XmlRpcMethod("SR.make")]
    Response<string> sr_make(string session, string _host, object _device_config, string _physical_size, string _name_label, string _name_description, string _type, string _content_type, object _sm_config);

    [XmlRpcMethod("Async.SR.make")]
    Response<string> async_sr_make(string session, string _host, object _device_config, string _physical_size, string _name_label, string _name_description, string _type, string _content_type, object _sm_config);

    [XmlRpcMethod("SR.destroy")]
    Response<string> sr_destroy(string session, string _sr);

    [XmlRpcMethod("Async.SR.destroy")]
    Response<string> async_sr_destroy(string session, string _sr);

    [XmlRpcMethod("SR.forget")]
    Response<string> sr_forget(string session, string _sr);

    [XmlRpcMethod("Async.SR.forget")]
    Response<string> async_sr_forget(string session, string _sr);

    [XmlRpcMethod("SR.update")]
    Response<string> sr_update(string session, string _sr);

    [XmlRpcMethod("Async.SR.update")]
    Response<string> async_sr_update(string session, string _sr);

    [XmlRpcMethod("SR.get_supported_types")]
    Response<string[]> sr_get_supported_types(string session);

    [XmlRpcMethod("SR.scan")]
    Response<string> sr_scan(string session, string _sr);

    [XmlRpcMethod("Async.SR.scan")]
    Response<string> async_sr_scan(string session, string _sr);

    [XmlRpcMethod("SR.probe")]
    Response<string> sr_probe(string session, string _host, object _device_config, string _type, object _sm_config);

    [XmlRpcMethod("Async.SR.probe")]
    Response<string> async_sr_probe(string session, string _host, object _device_config, string _type, object _sm_config);

    [XmlRpcMethod("SR.set_shared")]
    Response<string> sr_set_shared(string session, string _sr, bool _value);

    [XmlRpcMethod("Async.SR.set_shared")]
    Response<string> async_sr_set_shared(string session, string _sr, bool _value);

    [XmlRpcMethod("SR.set_name_label")]
    Response<string> sr_set_name_label(string session, string _sr, string _value);

    [XmlRpcMethod("Async.SR.set_name_label")]
    Response<string> async_sr_set_name_label(string session, string _sr, string _value);

    [XmlRpcMethod("SR.set_name_description")]
    Response<string> sr_set_name_description(string session, string _sr, string _value);

    [XmlRpcMethod("Async.SR.set_name_description")]
    Response<string> async_sr_set_name_description(string session, string _sr, string _value);

    [XmlRpcMethod("SR.create_new_blob")]
    Response<string> sr_create_new_blob(string session, string _sr, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("Async.SR.create_new_blob")]
    Response<string> async_sr_create_new_blob(string session, string _sr, string _name, string _mime_type, bool _public);

    [XmlRpcMethod("SR.set_physical_size")]
    Response<string> sr_set_physical_size(string session, string _self, string _value);

    [XmlRpcMethod("SR.set_virtual_allocation")]
    Response<string> sr_set_virtual_allocation(string session, string _self, string _value);

    [XmlRpcMethod("SR.set_physical_utilisation")]
    Response<string> sr_set_physical_utilisation(string session, string _self, string _value);

    [XmlRpcMethod("SR.assert_can_host_ha_statefile")]
    Response<string> sr_assert_can_host_ha_statefile(string session, string _sr);

    [XmlRpcMethod("Async.SR.assert_can_host_ha_statefile")]
    Response<string> async_sr_assert_can_host_ha_statefile(string session, string _sr);

    [XmlRpcMethod("SR.assert_supports_database_replication")]
    Response<string> sr_assert_supports_database_replication(string session, string _sr);

    [XmlRpcMethod("Async.SR.assert_supports_database_replication")]
    Response<string> async_sr_assert_supports_database_replication(string session, string _sr);

    [XmlRpcMethod("SR.enable_database_replication")]
    Response<string> sr_enable_database_replication(string session, string _sr);

    [XmlRpcMethod("Async.SR.enable_database_replication")]
    Response<string> async_sr_enable_database_replication(string session, string _sr);

    [XmlRpcMethod("SR.disable_database_replication")]
    Response<string> sr_disable_database_replication(string session, string _sr);

    [XmlRpcMethod("Async.SR.disable_database_replication")]
    Response<string> async_sr_disable_database_replication(string session, string _sr);

    [XmlRpcMethod("SR.get_all")]
    Response<string[]> sr_get_all(string session);

    [XmlRpcMethod("SR.get_all_records")]
    Response<object> sr_get_all_records(string session);

    [XmlRpcMethod("VDI.get_record")]
    Response<Proxy_VDI> vdi_get_record(string session, string _vdi);

    [XmlRpcMethod("VDI.get_by_uuid")]
    Response<string> vdi_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VDI.create")]
    Response<string> vdi_create(string session, Proxy_VDI _record);

    [XmlRpcMethod("Async.VDI.create")]
    Response<string> async_vdi_create(string session, Proxy_VDI _record);

    [XmlRpcMethod("VDI.destroy")]
    Response<string> vdi_destroy(string session, string _vdi);

    [XmlRpcMethod("Async.VDI.destroy")]
    Response<string> async_vdi_destroy(string session, string _vdi);

    [XmlRpcMethod("VDI.get_by_name_label")]
    Response<string[]> vdi_get_by_name_label(string session, string _label);

    [XmlRpcMethod("VDI.get_uuid")]
    Response<string> vdi_get_uuid(string session, string _vdi);

    [XmlRpcMethod("VDI.get_name_label")]
    Response<string> vdi_get_name_label(string session, string _vdi);

    [XmlRpcMethod("VDI.get_name_description")]
    Response<string> vdi_get_name_description(string session, string _vdi);

    [XmlRpcMethod("VDI.get_allowed_operations")]
    Response<string[]> vdi_get_allowed_operations(string session, string _vdi);

    [XmlRpcMethod("VDI.get_current_operations")]
    Response<object> vdi_get_current_operations(string session, string _vdi);

    [XmlRpcMethod("VDI.get_SR")]
    Response<string> vdi_get_sr(string session, string _vdi);

    [XmlRpcMethod("VDI.get_VBDs")]
    Response<string[]> vdi_get_vbds(string session, string _vdi);

    [XmlRpcMethod("VDI.get_crash_dumps")]
    Response<string[]> vdi_get_crash_dumps(string session, string _vdi);

    [XmlRpcMethod("VDI.get_virtual_size")]
    Response<string> vdi_get_virtual_size(string session, string _vdi);

    [XmlRpcMethod("VDI.get_physical_utilisation")]
    Response<string> vdi_get_physical_utilisation(string session, string _vdi);

    [XmlRpcMethod("VDI.get_type")]
    Response<string> vdi_get_type(string session, string _vdi);

    [XmlRpcMethod("VDI.get_sharable")]
    Response<bool> vdi_get_sharable(string session, string _vdi);

    [XmlRpcMethod("VDI.get_read_only")]
    Response<bool> vdi_get_read_only(string session, string _vdi);

    [XmlRpcMethod("VDI.get_other_config")]
    Response<object> vdi_get_other_config(string session, string _vdi);

    [XmlRpcMethod("VDI.get_storage_lock")]
    Response<bool> vdi_get_storage_lock(string session, string _vdi);

    [XmlRpcMethod("VDI.get_location")]
    Response<string> vdi_get_location(string session, string _vdi);

    [XmlRpcMethod("VDI.get_managed")]
    Response<bool> vdi_get_managed(string session, string _vdi);

    [XmlRpcMethod("VDI.get_missing")]
    Response<bool> vdi_get_missing(string session, string _vdi);

    [XmlRpcMethod("VDI.get_parent")]
    Response<string> vdi_get_parent(string session, string _vdi);

    [XmlRpcMethod("VDI.get_xenstore_data")]
    Response<object> vdi_get_xenstore_data(string session, string _vdi);

    [XmlRpcMethod("VDI.get_sm_config")]
    Response<object> vdi_get_sm_config(string session, string _vdi);

    [XmlRpcMethod("VDI.get_is_a_snapshot")]
    Response<bool> vdi_get_is_a_snapshot(string session, string _vdi);

    [XmlRpcMethod("VDI.get_snapshot_of")]
    Response<string> vdi_get_snapshot_of(string session, string _vdi);

    [XmlRpcMethod("VDI.get_snapshots")]
    Response<string[]> vdi_get_snapshots(string session, string _vdi);

    [XmlRpcMethod("VDI.get_snapshot_time")]
    Response<DateTime> vdi_get_snapshot_time(string session, string _vdi);

    [XmlRpcMethod("VDI.get_tags")]
    Response<string[]> vdi_get_tags(string session, string _vdi);

    [XmlRpcMethod("VDI.get_allow_caching")]
    Response<bool> vdi_get_allow_caching(string session, string _vdi);

    [XmlRpcMethod("VDI.get_on_boot")]
    Response<string> vdi_get_on_boot(string session, string _vdi);

    [XmlRpcMethod("VDI.get_metadata_of_pool")]
    Response<string> vdi_get_metadata_of_pool(string session, string _vdi);

    [XmlRpcMethod("VDI.get_metadata_latest")]
    Response<bool> vdi_get_metadata_latest(string session, string _vdi);

    [XmlRpcMethod("VDI.set_other_config")]
    Response<string> vdi_set_other_config(string session, string _vdi, object _other_config);

    [XmlRpcMethod("VDI.add_to_other_config")]
    Response<string> vdi_add_to_other_config(string session, string _vdi, string _key, string _value);

    [XmlRpcMethod("VDI.remove_from_other_config")]
    Response<string> vdi_remove_from_other_config(string session, string _vdi, string _key);

    [XmlRpcMethod("VDI.set_xenstore_data")]
    Response<string> vdi_set_xenstore_data(string session, string _vdi, object _xenstore_data);

    [XmlRpcMethod("VDI.add_to_xenstore_data")]
    Response<string> vdi_add_to_xenstore_data(string session, string _vdi, string _key, string _value);

    [XmlRpcMethod("VDI.remove_from_xenstore_data")]
    Response<string> vdi_remove_from_xenstore_data(string session, string _vdi, string _key);

    [XmlRpcMethod("VDI.set_sm_config")]
    Response<string> vdi_set_sm_config(string session, string _vdi, object _sm_config);

    [XmlRpcMethod("VDI.add_to_sm_config")]
    Response<string> vdi_add_to_sm_config(string session, string _vdi, string _key, string _value);

    [XmlRpcMethod("VDI.remove_from_sm_config")]
    Response<string> vdi_remove_from_sm_config(string session, string _vdi, string _key);

    [XmlRpcMethod("VDI.set_tags")]
    Response<string> vdi_set_tags(string session, string _vdi, string[] _tags);

    [XmlRpcMethod("VDI.add_tags")]
    Response<string> vdi_add_tags(string session, string _vdi, string _value);

    [XmlRpcMethod("VDI.remove_tags")]
    Response<string> vdi_remove_tags(string session, string _vdi, string _value);

    [XmlRpcMethod("VDI.snapshot")]
    Response<string> vdi_snapshot(string session, string _vdi, object _driver_params);

    [XmlRpcMethod("Async.VDI.snapshot")]
    Response<string> async_vdi_snapshot(string session, string _vdi, object _driver_params);

    [XmlRpcMethod("VDI.clone")]
    Response<string> vdi_clone(string session, string _vdi, object _driver_params);

    [XmlRpcMethod("Async.VDI.clone")]
    Response<string> async_vdi_clone(string session, string _vdi, object _driver_params);

    [XmlRpcMethod("VDI.resize")]
    Response<string> vdi_resize(string session, string _vdi, string _size);

    [XmlRpcMethod("Async.VDI.resize")]
    Response<string> async_vdi_resize(string session, string _vdi, string _size);

    [XmlRpcMethod("VDI.resize_online")]
    Response<string> vdi_resize_online(string session, string _vdi, string _size);

    [XmlRpcMethod("Async.VDI.resize_online")]
    Response<string> async_vdi_resize_online(string session, string _vdi, string _size);

    [XmlRpcMethod("VDI.introduce")]
    Response<string> vdi_introduce(string session, string _uuid, string _name_label, string _name_description, string _sr, string _type, bool _sharable, bool _read_only, object _other_config, string _location, object _xenstore_data, object _sm_config, bool _managed, string _virtual_size, string _physical_utilisation, string _metadata_of_pool, bool _is_a_snapshot, DateTime _snapshot_time, string _snapshot_of);

    [XmlRpcMethod("Async.VDI.introduce")]
    Response<string> async_vdi_introduce(string session, string _uuid, string _name_label, string _name_description, string _sr, string _type, bool _sharable, bool _read_only, object _other_config, string _location, object _xenstore_data, object _sm_config, bool _managed, string _virtual_size, string _physical_utilisation, string _metadata_of_pool, bool _is_a_snapshot, DateTime _snapshot_time, string _snapshot_of);

    [XmlRpcMethod("VDI.db_introduce")]
    Response<string> vdi_db_introduce(string session, string _uuid, string _name_label, string _name_description, string _sr, string _type, bool _sharable, bool _read_only, object _other_config, string _location, object _xenstore_data, object _sm_config, bool _managed, string _virtual_size, string _physical_utilisation, string _metadata_of_pool, bool _is_a_snapshot, DateTime _snapshot_time, string _snapshot_of);

    [XmlRpcMethod("Async.VDI.db_introduce")]
    Response<string> async_vdi_db_introduce(string session, string _uuid, string _name_label, string _name_description, string _sr, string _type, bool _sharable, bool _read_only, object _other_config, string _location, object _xenstore_data, object _sm_config, bool _managed, string _virtual_size, string _physical_utilisation, string _metadata_of_pool, bool _is_a_snapshot, DateTime _snapshot_time, string _snapshot_of);

    [XmlRpcMethod("VDI.db_forget")]
    Response<string> vdi_db_forget(string session, string _vdi);

    [XmlRpcMethod("Async.VDI.db_forget")]
    Response<string> async_vdi_db_forget(string session, string _vdi);

    [XmlRpcMethod("VDI.update")]
    Response<string> vdi_update(string session, string _vdi);

    [XmlRpcMethod("Async.VDI.update")]
    Response<string> async_vdi_update(string session, string _vdi);

    [XmlRpcMethod("VDI.copy")]
    Response<string> vdi_copy(string session, string _vdi, string _sr);

    [XmlRpcMethod("Async.VDI.copy")]
    Response<string> async_vdi_copy(string session, string _vdi, string _sr);

    [XmlRpcMethod("VDI.set_managed")]
    Response<string> vdi_set_managed(string session, string _self, bool _value);

    [XmlRpcMethod("VDI.forget")]
    Response<string> vdi_forget(string session, string _vdi);

    [XmlRpcMethod("Async.VDI.forget")]
    Response<string> async_vdi_forget(string session, string _vdi);

    [XmlRpcMethod("VDI.set_sharable")]
    Response<string> vdi_set_sharable(string session, string _self, bool _value);

    [XmlRpcMethod("VDI.set_read_only")]
    Response<string> vdi_set_read_only(string session, string _self, bool _value);

    [XmlRpcMethod("VDI.set_missing")]
    Response<string> vdi_set_missing(string session, string _self, bool _value);

    [XmlRpcMethod("VDI.set_virtual_size")]
    Response<string> vdi_set_virtual_size(string session, string _self, string _value);

    [XmlRpcMethod("VDI.set_physical_utilisation")]
    Response<string> vdi_set_physical_utilisation(string session, string _self, string _value);

    [XmlRpcMethod("VDI.set_is_a_snapshot")]
    Response<string> vdi_set_is_a_snapshot(string session, string _self, bool _value);

    [XmlRpcMethod("VDI.set_snapshot_of")]
    Response<string> vdi_set_snapshot_of(string session, string _self, string _value);

    [XmlRpcMethod("VDI.set_snapshot_time")]
    Response<string> vdi_set_snapshot_time(string session, string _self, DateTime _value);

    [XmlRpcMethod("VDI.set_metadata_of_pool")]
    Response<string> vdi_set_metadata_of_pool(string session, string _self, string _value);

    [XmlRpcMethod("VDI.set_name_label")]
    Response<string> vdi_set_name_label(string session, string _self, string _value);

    [XmlRpcMethod("Async.VDI.set_name_label")]
    Response<string> async_vdi_set_name_label(string session, string _self, string _value);

    [XmlRpcMethod("VDI.set_name_description")]
    Response<string> vdi_set_name_description(string session, string _self, string _value);

    [XmlRpcMethod("Async.VDI.set_name_description")]
    Response<string> async_vdi_set_name_description(string session, string _self, string _value);

    [XmlRpcMethod("VDI.set_on_boot")]
    Response<string> vdi_set_on_boot(string session, string _self, string _value);

    [XmlRpcMethod("Async.VDI.set_on_boot")]
    Response<string> async_vdi_set_on_boot(string session, string _self, string _value);

    [XmlRpcMethod("VDI.set_allow_caching")]
    Response<string> vdi_set_allow_caching(string session, string _self, bool _value);

    [XmlRpcMethod("Async.VDI.set_allow_caching")]
    Response<string> async_vdi_set_allow_caching(string session, string _self, bool _value);

    [XmlRpcMethod("VDI.open_database")]
    Response<string> vdi_open_database(string session, string _self);

    [XmlRpcMethod("Async.VDI.open_database")]
    Response<string> async_vdi_open_database(string session, string _self);

    [XmlRpcMethod("VDI.read_database_pool_uuid")]
    Response<string> vdi_read_database_pool_uuid(string session, string _self);

    [XmlRpcMethod("Async.VDI.read_database_pool_uuid")]
    Response<string> async_vdi_read_database_pool_uuid(string session, string _self);

    [XmlRpcMethod("VDI.pool_migrate")]
    Response<string> vdi_pool_migrate(string session, string _vdi, string _sr, object _options);

    [XmlRpcMethod("Async.VDI.pool_migrate")]
    Response<string> async_vdi_pool_migrate(string session, string _vdi, string _sr, object _options);

    [XmlRpcMethod("VDI.get_all")]
    Response<string[]> vdi_get_all(string session);

    [XmlRpcMethod("VDI.get_all_records")]
    Response<object> vdi_get_all_records(string session);

    [XmlRpcMethod("VBD.get_record")]
    Response<Proxy_VBD> vbd_get_record(string session, string _vbd);

    [XmlRpcMethod("VBD.get_by_uuid")]
    Response<string> vbd_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VBD.create")]
    Response<string> vbd_create(string session, Proxy_VBD _record);

    [XmlRpcMethod("Async.VBD.create")]
    Response<string> async_vbd_create(string session, Proxy_VBD _record);

    [XmlRpcMethod("VBD.destroy")]
    Response<string> vbd_destroy(string session, string _vbd);

    [XmlRpcMethod("Async.VBD.destroy")]
    Response<string> async_vbd_destroy(string session, string _vbd);

    [XmlRpcMethod("VBD.get_uuid")]
    Response<string> vbd_get_uuid(string session, string _vbd);

    [XmlRpcMethod("VBD.get_allowed_operations")]
    Response<string[]> vbd_get_allowed_operations(string session, string _vbd);

    [XmlRpcMethod("VBD.get_current_operations")]
    Response<object> vbd_get_current_operations(string session, string _vbd);

    [XmlRpcMethod("VBD.get_VM")]
    Response<string> vbd_get_vm(string session, string _vbd);

    [XmlRpcMethod("VBD.get_VDI")]
    Response<string> vbd_get_vdi(string session, string _vbd);

    [XmlRpcMethod("VBD.get_device")]
    Response<string> vbd_get_device(string session, string _vbd);

    [XmlRpcMethod("VBD.get_userdevice")]
    Response<string> vbd_get_userdevice(string session, string _vbd);

    [XmlRpcMethod("VBD.get_bootable")]
    Response<bool> vbd_get_bootable(string session, string _vbd);

    [XmlRpcMethod("VBD.get_mode")]
    Response<string> vbd_get_mode(string session, string _vbd);

    [XmlRpcMethod("VBD.get_type")]
    Response<string> vbd_get_type(string session, string _vbd);

    [XmlRpcMethod("VBD.get_unpluggable")]
    Response<bool> vbd_get_unpluggable(string session, string _vbd);

    [XmlRpcMethod("VBD.get_storage_lock")]
    Response<bool> vbd_get_storage_lock(string session, string _vbd);

    [XmlRpcMethod("VBD.get_empty")]
    Response<bool> vbd_get_empty(string session, string _vbd);

    [XmlRpcMethod("VBD.get_other_config")]
    Response<object> vbd_get_other_config(string session, string _vbd);

    [XmlRpcMethod("VBD.get_currently_attached")]
    Response<bool> vbd_get_currently_attached(string session, string _vbd);

    [XmlRpcMethod("VBD.get_status_code")]
    Response<string> vbd_get_status_code(string session, string _vbd);

    [XmlRpcMethod("VBD.get_status_detail")]
    Response<string> vbd_get_status_detail(string session, string _vbd);

    [XmlRpcMethod("VBD.get_runtime_properties")]
    Response<object> vbd_get_runtime_properties(string session, string _vbd);

    [XmlRpcMethod("VBD.get_qos_algorithm_type")]
    Response<string> vbd_get_qos_algorithm_type(string session, string _vbd);

    [XmlRpcMethod("VBD.get_qos_algorithm_params")]
    Response<object> vbd_get_qos_algorithm_params(string session, string _vbd);

    [XmlRpcMethod("VBD.get_qos_supported_algorithms")]
    Response<string[]> vbd_get_qos_supported_algorithms(string session, string _vbd);

    [XmlRpcMethod("VBD.get_metrics")]
    Response<string> vbd_get_metrics(string session, string _vbd);

    [XmlRpcMethod("VBD.set_userdevice")]
    Response<string> vbd_set_userdevice(string session, string _vbd, string _userdevice);

    [XmlRpcMethod("VBD.set_bootable")]
    Response<string> vbd_set_bootable(string session, string _vbd, bool _bootable);

    [XmlRpcMethod("VBD.set_mode")]
    Response<string> vbd_set_mode(string session, string _vbd, string _mode);

    [XmlRpcMethod("VBD.set_type")]
    Response<string> vbd_set_type(string session, string _vbd, string _type);

    [XmlRpcMethod("VBD.set_unpluggable")]
    Response<string> vbd_set_unpluggable(string session, string _vbd, bool _unpluggable);

    [XmlRpcMethod("VBD.set_other_config")]
    Response<string> vbd_set_other_config(string session, string _vbd, object _other_config);

    [XmlRpcMethod("VBD.add_to_other_config")]
    Response<string> vbd_add_to_other_config(string session, string _vbd, string _key, string _value);

    [XmlRpcMethod("VBD.remove_from_other_config")]
    Response<string> vbd_remove_from_other_config(string session, string _vbd, string _key);

    [XmlRpcMethod("VBD.set_qos_algorithm_type")]
    Response<string> vbd_set_qos_algorithm_type(string session, string _vbd, string _algorithm_type);

    [XmlRpcMethod("VBD.set_qos_algorithm_params")]
    Response<string> vbd_set_qos_algorithm_params(string session, string _vbd, object _algorithm_params);

    [XmlRpcMethod("VBD.add_to_qos_algorithm_params")]
    Response<string> vbd_add_to_qos_algorithm_params(string session, string _vbd, string _key, string _value);

    [XmlRpcMethod("VBD.remove_from_qos_algorithm_params")]
    Response<string> vbd_remove_from_qos_algorithm_params(string session, string _vbd, string _key);

    [XmlRpcMethod("VBD.eject")]
    Response<string> vbd_eject(string session, string _vbd);

    [XmlRpcMethod("Async.VBD.eject")]
    Response<string> async_vbd_eject(string session, string _vbd);

    [XmlRpcMethod("VBD.insert")]
    Response<string> vbd_insert(string session, string _vbd, string _vdi);

    [XmlRpcMethod("Async.VBD.insert")]
    Response<string> async_vbd_insert(string session, string _vbd, string _vdi);

    [XmlRpcMethod("VBD.plug")]
    Response<string> vbd_plug(string session, string _self);

    [XmlRpcMethod("Async.VBD.plug")]
    Response<string> async_vbd_plug(string session, string _self);

    [XmlRpcMethod("VBD.unplug")]
    Response<string> vbd_unplug(string session, string _self);

    [XmlRpcMethod("Async.VBD.unplug")]
    Response<string> async_vbd_unplug(string session, string _self);

    [XmlRpcMethod("VBD.unplug_force")]
    Response<string> vbd_unplug_force(string session, string _self);

    [XmlRpcMethod("Async.VBD.unplug_force")]
    Response<string> async_vbd_unplug_force(string session, string _self);

    [XmlRpcMethod("VBD.assert_attachable")]
    Response<string> vbd_assert_attachable(string session, string _self);

    [XmlRpcMethod("Async.VBD.assert_attachable")]
    Response<string> async_vbd_assert_attachable(string session, string _self);

    [XmlRpcMethod("VBD.get_all")]
    Response<string[]> vbd_get_all(string session);

    [XmlRpcMethod("VBD.get_all_records")]
    Response<object> vbd_get_all_records(string session);

    [XmlRpcMethod("VBD_metrics.get_record")]
    Response<Proxy_VBD_metrics> vbd_metrics_get_record(string session, string _vbd_metrics);

    [XmlRpcMethod("VBD_metrics.get_by_uuid")]
    Response<string> vbd_metrics_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VBD_metrics.get_uuid")]
    Response<string> vbd_metrics_get_uuid(string session, string _vbd_metrics);

    [XmlRpcMethod("VBD_metrics.get_io_read_kbs")]
    Response<double> vbd_metrics_get_io_read_kbs(string session, string _vbd_metrics);

    [XmlRpcMethod("VBD_metrics.get_io_write_kbs")]
    Response<double> vbd_metrics_get_io_write_kbs(string session, string _vbd_metrics);

    [XmlRpcMethod("VBD_metrics.get_last_updated")]
    Response<DateTime> vbd_metrics_get_last_updated(string session, string _vbd_metrics);

    [XmlRpcMethod("VBD_metrics.get_other_config")]
    Response<object> vbd_metrics_get_other_config(string session, string _vbd_metrics);

    [XmlRpcMethod("VBD_metrics.set_other_config")]
    Response<string> vbd_metrics_set_other_config(string session, string _vbd_metrics, object _other_config);

    [XmlRpcMethod("VBD_metrics.add_to_other_config")]
    Response<string> vbd_metrics_add_to_other_config(string session, string _vbd_metrics, string _key, string _value);

    [XmlRpcMethod("VBD_metrics.remove_from_other_config")]
    Response<string> vbd_metrics_remove_from_other_config(string session, string _vbd_metrics, string _key);

    [XmlRpcMethod("VBD_metrics.get_all")]
    Response<string[]> vbd_metrics_get_all(string session);

    [XmlRpcMethod("VBD_metrics.get_all_records")]
    Response<object> vbd_metrics_get_all_records(string session);

    [XmlRpcMethod("PBD.get_record")]
    Response<Proxy_PBD> pbd_get_record(string session, string _pbd);

    [XmlRpcMethod("PBD.get_by_uuid")]
    Response<string> pbd_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("PBD.create")]
    Response<string> pbd_create(string session, Proxy_PBD _record);

    [XmlRpcMethod("Async.PBD.create")]
    Response<string> async_pbd_create(string session, Proxy_PBD _record);

    [XmlRpcMethod("PBD.destroy")]
    Response<string> pbd_destroy(string session, string _pbd);

    [XmlRpcMethod("Async.PBD.destroy")]
    Response<string> async_pbd_destroy(string session, string _pbd);

    [XmlRpcMethod("PBD.get_uuid")]
    Response<string> pbd_get_uuid(string session, string _pbd);

    [XmlRpcMethod("PBD.get_host")]
    Response<string> pbd_get_host(string session, string _pbd);

    [XmlRpcMethod("PBD.get_SR")]
    Response<string> pbd_get_sr(string session, string _pbd);

    [XmlRpcMethod("PBD.get_device_config")]
    Response<object> pbd_get_device_config(string session, string _pbd);

    [XmlRpcMethod("PBD.get_currently_attached")]
    Response<bool> pbd_get_currently_attached(string session, string _pbd);

    [XmlRpcMethod("PBD.get_other_config")]
    Response<object> pbd_get_other_config(string session, string _pbd);

    [XmlRpcMethod("PBD.set_other_config")]
    Response<string> pbd_set_other_config(string session, string _pbd, object _other_config);

    [XmlRpcMethod("PBD.add_to_other_config")]
    Response<string> pbd_add_to_other_config(string session, string _pbd, string _key, string _value);

    [XmlRpcMethod("PBD.remove_from_other_config")]
    Response<string> pbd_remove_from_other_config(string session, string _pbd, string _key);

    [XmlRpcMethod("PBD.plug")]
    Response<string> pbd_plug(string session, string _self);

    [XmlRpcMethod("Async.PBD.plug")]
    Response<string> async_pbd_plug(string session, string _self);

    [XmlRpcMethod("PBD.unplug")]
    Response<string> pbd_unplug(string session, string _self);

    [XmlRpcMethod("Async.PBD.unplug")]
    Response<string> async_pbd_unplug(string session, string _self);

    [XmlRpcMethod("PBD.set_device_config")]
    Response<string> pbd_set_device_config(string session, string _self, object _value);

    [XmlRpcMethod("Async.PBD.set_device_config")]
    Response<string> async_pbd_set_device_config(string session, string _self, object _value);

    [XmlRpcMethod("PBD.get_all")]
    Response<string[]> pbd_get_all(string session);

    [XmlRpcMethod("PBD.get_all_records")]
    Response<object> pbd_get_all_records(string session);

    [XmlRpcMethod("crashdump.get_record")]
    Response<Proxy_Crashdump> crashdump_get_record(string session, string _crashdump);

    [XmlRpcMethod("crashdump.get_by_uuid")]
    Response<string> crashdump_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("crashdump.get_uuid")]
    Response<string> crashdump_get_uuid(string session, string _crashdump);

    [XmlRpcMethod("crashdump.get_VM")]
    Response<string> crashdump_get_vm(string session, string _crashdump);

    [XmlRpcMethod("crashdump.get_VDI")]
    Response<string> crashdump_get_vdi(string session, string _crashdump);

    [XmlRpcMethod("crashdump.get_other_config")]
    Response<object> crashdump_get_other_config(string session, string _crashdump);

    [XmlRpcMethod("crashdump.set_other_config")]
    Response<string> crashdump_set_other_config(string session, string _crashdump, object _other_config);

    [XmlRpcMethod("crashdump.add_to_other_config")]
    Response<string> crashdump_add_to_other_config(string session, string _crashdump, string _key, string _value);

    [XmlRpcMethod("crashdump.remove_from_other_config")]
    Response<string> crashdump_remove_from_other_config(string session, string _crashdump, string _key);

    [XmlRpcMethod("crashdump.destroy")]
    Response<string> crashdump_destroy(string session, string _self);

    [XmlRpcMethod("Async.crashdump.destroy")]
    Response<string> async_crashdump_destroy(string session, string _self);

    [XmlRpcMethod("crashdump.get_all")]
    Response<string[]> crashdump_get_all(string session);

    [XmlRpcMethod("crashdump.get_all_records")]
    Response<object> crashdump_get_all_records(string session);

    [XmlRpcMethod("VTPM.get_record")]
    Response<Proxy_VTPM> vtpm_get_record(string session, string _vtpm);

    [XmlRpcMethod("VTPM.get_by_uuid")]
    Response<string> vtpm_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VTPM.create")]
    Response<string> vtpm_create(string session, Proxy_VTPM _record);

    [XmlRpcMethod("Async.VTPM.create")]
    Response<string> async_vtpm_create(string session, Proxy_VTPM _record);

    [XmlRpcMethod("VTPM.destroy")]
    Response<string> vtpm_destroy(string session, string _vtpm);

    [XmlRpcMethod("Async.VTPM.destroy")]
    Response<string> async_vtpm_destroy(string session, string _vtpm);

    [XmlRpcMethod("VTPM.get_uuid")]
    Response<string> vtpm_get_uuid(string session, string _vtpm);

    [XmlRpcMethod("VTPM.get_VM")]
    Response<string> vtpm_get_vm(string session, string _vtpm);

    [XmlRpcMethod("VTPM.get_backend")]
    Response<string> vtpm_get_backend(string session, string _vtpm);

    [XmlRpcMethod("VTPM.get_all_records")]
    Response<object> vtpm_get_all_records(string session);

    [XmlRpcMethod("console.get_record")]
    Response<Proxy_Console> console_get_record(string session, string _console);

    [XmlRpcMethod("console.get_by_uuid")]
    Response<string> console_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("console.create")]
    Response<string> console_create(string session, Proxy_Console _record);

    [XmlRpcMethod("Async.console.create")]
    Response<string> async_console_create(string session, Proxy_Console _record);

    [XmlRpcMethod("console.destroy")]
    Response<string> console_destroy(string session, string _console);

    [XmlRpcMethod("Async.console.destroy")]
    Response<string> async_console_destroy(string session, string _console);

    [XmlRpcMethod("console.get_uuid")]
    Response<string> console_get_uuid(string session, string _console);

    [XmlRpcMethod("console.get_protocol")]
    Response<string> console_get_protocol(string session, string _console);

    [XmlRpcMethod("console.get_location")]
    Response<string> console_get_location(string session, string _console);

    [XmlRpcMethod("console.get_VM")]
    Response<string> console_get_vm(string session, string _console);

    [XmlRpcMethod("console.get_other_config")]
    Response<object> console_get_other_config(string session, string _console);

    [XmlRpcMethod("console.set_other_config")]
    Response<string> console_set_other_config(string session, string _console, object _other_config);

    [XmlRpcMethod("console.add_to_other_config")]
    Response<string> console_add_to_other_config(string session, string _console, string _key, string _value);

    [XmlRpcMethod("console.remove_from_other_config")]
    Response<string> console_remove_from_other_config(string session, string _console, string _key);

    [XmlRpcMethod("console.get_all")]
    Response<string[]> console_get_all(string session);

    [XmlRpcMethod("console.get_all_records")]
    Response<object> console_get_all_records(string session);

    [XmlRpcMethod("user.get_record")]
    Response<Proxy_User> user_get_record(string session, string _user);

    [XmlRpcMethod("user.get_by_uuid")]
    Response<string> user_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("user.create")]
    Response<string> user_create(string session, Proxy_User _record);

    [XmlRpcMethod("Async.user.create")]
    Response<string> async_user_create(string session, Proxy_User _record);

    [XmlRpcMethod("user.destroy")]
    Response<string> user_destroy(string session, string _user);

    [XmlRpcMethod("Async.user.destroy")]
    Response<string> async_user_destroy(string session, string _user);

    [XmlRpcMethod("user.get_uuid")]
    Response<string> user_get_uuid(string session, string _user);

    [XmlRpcMethod("user.get_short_name")]
    Response<string> user_get_short_name(string session, string _user);

    [XmlRpcMethod("user.get_fullname")]
    Response<string> user_get_fullname(string session, string _user);

    [XmlRpcMethod("user.get_other_config")]
    Response<object> user_get_other_config(string session, string _user);

    [XmlRpcMethod("user.set_fullname")]
    Response<string> user_set_fullname(string session, string _user, string _fullname);

    [XmlRpcMethod("user.set_other_config")]
    Response<string> user_set_other_config(string session, string _user, object _other_config);

    [XmlRpcMethod("user.add_to_other_config")]
    Response<string> user_add_to_other_config(string session, string _user, string _key, string _value);

    [XmlRpcMethod("user.remove_from_other_config")]
    Response<string> user_remove_from_other_config(string session, string _user, string _key);

    [XmlRpcMethod("user.get_all_records")]
    Response<object> user_get_all_records(string session);

    [XmlRpcMethod("data_source.get_all_records")]
    Response<object> data_source_get_all_records(string session);

    [XmlRpcMethod("blob.get_record")]
    Response<Proxy_Blob> blob_get_record(string session, string _blob);

    [XmlRpcMethod("blob.get_by_uuid")]
    Response<string> blob_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("blob.get_by_name_label")]
    Response<string[]> blob_get_by_name_label(string session, string _label);

    [XmlRpcMethod("blob.get_uuid")]
    Response<string> blob_get_uuid(string session, string _blob);

    [XmlRpcMethod("blob.get_name_label")]
    Response<string> blob_get_name_label(string session, string _blob);

    [XmlRpcMethod("blob.get_name_description")]
    Response<string> blob_get_name_description(string session, string _blob);

    [XmlRpcMethod("blob.get_size")]
    Response<string> blob_get_size(string session, string _blob);

    [XmlRpcMethod("blob.get_public")]
    Response<bool> blob_get_public(string session, string _blob);

    [XmlRpcMethod("blob.get_last_updated")]
    Response<DateTime> blob_get_last_updated(string session, string _blob);

    [XmlRpcMethod("blob.get_mime_type")]
    Response<string> blob_get_mime_type(string session, string _blob);

    [XmlRpcMethod("blob.set_name_label")]
    Response<string> blob_set_name_label(string session, string _blob, string _label);

    [XmlRpcMethod("blob.set_name_description")]
    Response<string> blob_set_name_description(string session, string _blob, string _description);

    [XmlRpcMethod("blob.set_public")]
    Response<string> blob_set_public(string session, string _blob, bool _public);

    [XmlRpcMethod("blob.create")]
    Response<string> blob_create(string session, string _mime_type, bool _public);

    [XmlRpcMethod("blob.destroy")]
    Response<string> blob_destroy(string session, string _self);

    [XmlRpcMethod("blob.get_all")]
    Response<string[]> blob_get_all(string session);

    [XmlRpcMethod("blob.get_all_records")]
    Response<object> blob_get_all_records(string session);

    [XmlRpcMethod("message.create")]
    Response<string> message_create(string session, string _name, string _priority, string _cls, string _obj_uuid, string _body);

    [XmlRpcMethod("message.destroy")]
    Response<string> message_destroy(string session, string _self);

    [XmlRpcMethod("message.get")]
    Response<object> message_get(string session, string _cls, string _obj_uuid, DateTime _since);

    [XmlRpcMethod("message.get_all")]
    Response<string[]> message_get_all(string session);

    [XmlRpcMethod("message.get_since")]
    Response<object> message_get_since(string session, DateTime _since);

    [XmlRpcMethod("message.get_record")]
    Response<Proxy_Message> message_get_record(string session, string _self);

    [XmlRpcMethod("message.get_by_uuid")]
    Response<string> message_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("message.get_all_records")]
    Response<object> message_get_all_records(string session);

    [XmlRpcMethod("message.get_all_records_where")]
    Response<object> message_get_all_records_where(string session, string _expr);

    [XmlRpcMethod("secret.get_record")]
    Response<Proxy_Secret> secret_get_record(string session, string _secret);

    [XmlRpcMethod("secret.get_by_uuid")]
    Response<string> secret_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("secret.create")]
    Response<string> secret_create(string session, Proxy_Secret _record);

    [XmlRpcMethod("Async.secret.create")]
    Response<string> async_secret_create(string session, Proxy_Secret _record);

    [XmlRpcMethod("secret.destroy")]
    Response<string> secret_destroy(string session, string _secret);

    [XmlRpcMethod("Async.secret.destroy")]
    Response<string> async_secret_destroy(string session, string _secret);

    [XmlRpcMethod("secret.get_uuid")]
    Response<string> secret_get_uuid(string session, string _secret);

    [XmlRpcMethod("secret.get_value")]
    Response<string> secret_get_value(string session, string _secret);

    [XmlRpcMethod("secret.get_other_config")]
    Response<object> secret_get_other_config(string session, string _secret);

    [XmlRpcMethod("secret.set_value")]
    Response<string> secret_set_value(string session, string _secret, string _value);

    [XmlRpcMethod("secret.set_other_config")]
    Response<string> secret_set_other_config(string session, string _secret, object _other_config);

    [XmlRpcMethod("secret.add_to_other_config")]
    Response<string> secret_add_to_other_config(string session, string _secret, string _key, string _value);

    [XmlRpcMethod("secret.remove_from_other_config")]
    Response<string> secret_remove_from_other_config(string session, string _secret, string _key);

    [XmlRpcMethod("secret.get_all")]
    Response<string[]> secret_get_all(string session);

    [XmlRpcMethod("secret.get_all_records")]
    Response<object> secret_get_all_records(string session);

    [XmlRpcMethod("tunnel.get_record")]
    Response<Proxy_Tunnel> tunnel_get_record(string session, string _tunnel);

    [XmlRpcMethod("tunnel.get_by_uuid")]
    Response<string> tunnel_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("tunnel.get_uuid")]
    Response<string> tunnel_get_uuid(string session, string _tunnel);

    [XmlRpcMethod("tunnel.get_access_PIF")]
    Response<string> tunnel_get_access_pif(string session, string _tunnel);

    [XmlRpcMethod("tunnel.get_transport_PIF")]
    Response<string> tunnel_get_transport_pif(string session, string _tunnel);

    [XmlRpcMethod("tunnel.get_status")]
    Response<object> tunnel_get_status(string session, string _tunnel);

    [XmlRpcMethod("tunnel.get_other_config")]
    Response<object> tunnel_get_other_config(string session, string _tunnel);

    [XmlRpcMethod("tunnel.set_status")]
    Response<string> tunnel_set_status(string session, string _tunnel, object _status);

    [XmlRpcMethod("tunnel.add_to_status")]
    Response<string> tunnel_add_to_status(string session, string _tunnel, string _key, string _value);

    [XmlRpcMethod("tunnel.remove_from_status")]
    Response<string> tunnel_remove_from_status(string session, string _tunnel, string _key);

    [XmlRpcMethod("tunnel.set_other_config")]
    Response<string> tunnel_set_other_config(string session, string _tunnel, object _other_config);

    [XmlRpcMethod("tunnel.add_to_other_config")]
    Response<string> tunnel_add_to_other_config(string session, string _tunnel, string _key, string _value);

    [XmlRpcMethod("tunnel.remove_from_other_config")]
    Response<string> tunnel_remove_from_other_config(string session, string _tunnel, string _key);

    [XmlRpcMethod("tunnel.create")]
    Response<string> tunnel_create(string session, string _transport_pif, string _network);

    [XmlRpcMethod("Async.tunnel.create")]
    Response<string> async_tunnel_create(string session, string _transport_pif, string _network);

    [XmlRpcMethod("tunnel.destroy")]
    Response<string> tunnel_destroy(string session, string _self);

    [XmlRpcMethod("Async.tunnel.destroy")]
    Response<string> async_tunnel_destroy(string session, string _self);

    [XmlRpcMethod("tunnel.get_all")]
    Response<string[]> tunnel_get_all(string session);

    [XmlRpcMethod("tunnel.get_all_records")]
    Response<object> tunnel_get_all_records(string session);

    [XmlRpcMethod("PCI.get_record")]
    Response<Proxy_PCI> pci_get_record(string session, string _pci);

    [XmlRpcMethod("PCI.get_by_uuid")]
    Response<string> pci_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("PCI.get_uuid")]
    Response<string> pci_get_uuid(string session, string _pci);

    [XmlRpcMethod("PCI.get_class_name")]
    Response<string> pci_get_class_name(string session, string _pci);

    [XmlRpcMethod("PCI.get_vendor_name")]
    Response<string> pci_get_vendor_name(string session, string _pci);

    [XmlRpcMethod("PCI.get_device_name")]
    Response<string> pci_get_device_name(string session, string _pci);

    [XmlRpcMethod("PCI.get_host")]
    Response<string> pci_get_host(string session, string _pci);

    [XmlRpcMethod("PCI.get_pci_id")]
    Response<string> pci_get_pci_id(string session, string _pci);

    [XmlRpcMethod("PCI.get_dependencies")]
    Response<string[]> pci_get_dependencies(string session, string _pci);

    [XmlRpcMethod("PCI.get_other_config")]
    Response<object> pci_get_other_config(string session, string _pci);

    [XmlRpcMethod("PCI.set_other_config")]
    Response<string> pci_set_other_config(string session, string _pci, object _other_config);

    [XmlRpcMethod("PCI.add_to_other_config")]
    Response<string> pci_add_to_other_config(string session, string _pci, string _key, string _value);

    [XmlRpcMethod("PCI.remove_from_other_config")]
    Response<string> pci_remove_from_other_config(string session, string _pci, string _key);

    [XmlRpcMethod("PCI.get_all")]
    Response<string[]> pci_get_all(string session);

    [XmlRpcMethod("PCI.get_all_records")]
    Response<object> pci_get_all_records(string session);

    [XmlRpcMethod("PGPU.get_record")]
    Response<Proxy_PGPU> pgpu_get_record(string session, string _pgpu);

    [XmlRpcMethod("PGPU.get_by_uuid")]
    Response<string> pgpu_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("PGPU.get_uuid")]
    Response<string> pgpu_get_uuid(string session, string _pgpu);

    [XmlRpcMethod("PGPU.get_PCI")]
    Response<string> pgpu_get_pci(string session, string _pgpu);

    [XmlRpcMethod("PGPU.get_GPU_group")]
    Response<string> pgpu_get_gpu_group(string session, string _pgpu);

    [XmlRpcMethod("PGPU.get_host")]
    Response<string> pgpu_get_host(string session, string _pgpu);

    [XmlRpcMethod("PGPU.get_other_config")]
    Response<object> pgpu_get_other_config(string session, string _pgpu);

    [XmlRpcMethod("PGPU.set_other_config")]
    Response<string> pgpu_set_other_config(string session, string _pgpu, object _other_config);

    [XmlRpcMethod("PGPU.add_to_other_config")]
    Response<string> pgpu_add_to_other_config(string session, string _pgpu, string _key, string _value);

    [XmlRpcMethod("PGPU.remove_from_other_config")]
    Response<string> pgpu_remove_from_other_config(string session, string _pgpu, string _key);

    [XmlRpcMethod("PGPU.get_all")]
    Response<string[]> pgpu_get_all(string session);

    [XmlRpcMethod("PGPU.get_all_records")]
    Response<object> pgpu_get_all_records(string session);

    [XmlRpcMethod("GPU_group.get_record")]
    Response<Proxy_GPU_group> gpu_group_get_record(string session, string _gpu_group);

    [XmlRpcMethod("GPU_group.get_by_uuid")]
    Response<string> gpu_group_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("GPU_group.get_by_name_label")]
    Response<string[]> gpu_group_get_by_name_label(string session, string _label);

    [XmlRpcMethod("GPU_group.get_uuid")]
    Response<string> gpu_group_get_uuid(string session, string _gpu_group);

    [XmlRpcMethod("GPU_group.get_name_label")]
    Response<string> gpu_group_get_name_label(string session, string _gpu_group);

    [XmlRpcMethod("GPU_group.get_name_description")]
    Response<string> gpu_group_get_name_description(string session, string _gpu_group);

    [XmlRpcMethod("GPU_group.get_PGPUs")]
    Response<string[]> gpu_group_get_pgpus(string session, string _gpu_group);

    [XmlRpcMethod("GPU_group.get_VGPUs")]
    Response<string[]> gpu_group_get_vgpus(string session, string _gpu_group);

    [XmlRpcMethod("GPU_group.get_GPU_types")]
    Response<string[]> gpu_group_get_gpu_types(string session, string _gpu_group);

    [XmlRpcMethod("GPU_group.get_other_config")]
    Response<object> gpu_group_get_other_config(string session, string _gpu_group);

    [XmlRpcMethod("GPU_group.set_name_label")]
    Response<string> gpu_group_set_name_label(string session, string _gpu_group, string _label);

    [XmlRpcMethod("GPU_group.set_name_description")]
    Response<string> gpu_group_set_name_description(string session, string _gpu_group, string _description);

    [XmlRpcMethod("GPU_group.set_other_config")]
    Response<string> gpu_group_set_other_config(string session, string _gpu_group, object _other_config);

    [XmlRpcMethod("GPU_group.add_to_other_config")]
    Response<string> gpu_group_add_to_other_config(string session, string _gpu_group, string _key, string _value);

    [XmlRpcMethod("GPU_group.remove_from_other_config")]
    Response<string> gpu_group_remove_from_other_config(string session, string _gpu_group, string _key);

    [XmlRpcMethod("GPU_group.get_all")]
    Response<string[]> gpu_group_get_all(string session);

    [XmlRpcMethod("GPU_group.get_all_records")]
    Response<object> gpu_group_get_all_records(string session);

    [XmlRpcMethod("VGPU.get_record")]
    Response<Proxy_VGPU> vgpu_get_record(string session, string _vgpu);

    [XmlRpcMethod("VGPU.get_by_uuid")]
    Response<string> vgpu_get_by_uuid(string session, string _uuid);

    [XmlRpcMethod("VGPU.get_uuid")]
    Response<string> vgpu_get_uuid(string session, string _vgpu);

    [XmlRpcMethod("VGPU.get_VM")]
    Response<string> vgpu_get_vm(string session, string _vgpu);

    [XmlRpcMethod("VGPU.get_GPU_group")]
    Response<string> vgpu_get_gpu_group(string session, string _vgpu);

    [XmlRpcMethod("VGPU.get_device")]
    Response<string> vgpu_get_device(string session, string _vgpu);

    [XmlRpcMethod("VGPU.get_currently_attached")]
    Response<bool> vgpu_get_currently_attached(string session, string _vgpu);

    [XmlRpcMethod("VGPU.get_other_config")]
    Response<object> vgpu_get_other_config(string session, string _vgpu);

    [XmlRpcMethod("VGPU.set_other_config")]
    Response<string> vgpu_set_other_config(string session, string _vgpu, object _other_config);

    [XmlRpcMethod("VGPU.add_to_other_config")]
    Response<string> vgpu_add_to_other_config(string session, string _vgpu, string _key, string _value);

    [XmlRpcMethod("VGPU.remove_from_other_config")]
    Response<string> vgpu_remove_from_other_config(string session, string _vgpu, string _key);

    [XmlRpcMethod("VGPU.create")]
    Response<string> vgpu_create(string session, string _vm, string _gpu_group, string _device, object _other_config);

    [XmlRpcMethod("Async.VGPU.create")]
    Response<string> async_vgpu_create(string session, string _vm, string _gpu_group, string _device, object _other_config);

    [XmlRpcMethod("VGPU.destroy")]
    Response<string> vgpu_destroy(string session, string _self);

    [XmlRpcMethod("Async.VGPU.destroy")]
    Response<string> async_vgpu_destroy(string session, string _self);

    [XmlRpcMethod("VGPU.get_all")]
    Response<string[]> vgpu_get_all(string session);

    [XmlRpcMethod("VGPU.get_all_records")]
    Response<object> vgpu_get_all_records(string session);
  }
}
