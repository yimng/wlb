// Decompiled with JetBrains decompiler
// Type: XenAPI.HTTP_actions
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Net;

namespace XenAPI
{
  public class HTTP_actions
  {
    private static void Get(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, string remotePath, IWebProxy proxy, string localPath, params object[] args)
    {
      HTTP.Get(dataCopiedDelegate, cancellingDelegate, HTTP.BuildUri(hostname, remotePath, args), proxy, localPath, timeout_ms);
    }

    private static void Put(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, string remotePath, IWebProxy proxy, string localPath, params object[] args)
    {
      HTTP.Put(progressDelegate, cancellingDelegate, HTTP.BuildUri(hostname, remotePath, args), proxy, localPath, timeout_ms);
    }

    public static void get_services(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/services", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }

    public static void put_import(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, bool restore, bool force, string sr_id)
    {
      HTTP_actions.Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/import", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "restore", (object) (bool) (restore ? 1 : 0), (object) "force", (object) (bool) (force ? 1 : 0), (object) "sr_id", (object) sr_id);
    }

    public static void put_import_metadata(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, bool restore, bool force)
    {
      HTTP_actions.Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/import_metadata", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "restore", (object) (bool) (restore ? 1 : 0), (object) "force", (object) (bool) (force ? 1 : 0));
    }

    public static void put_import_raw_vdi(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string vdi)
    {
      HTTP_actions.Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/import_raw_vdi", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "vdi", (object) vdi);
    }

    public static void get_export(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/export", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "uuid", (object) uuid);
    }

    public static void get_export_metadata(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/export_metadata", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "uuid", (object) uuid);
    }

    public static void get_host_backup(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/host_backup", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }

    public static void put_host_restore(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/host_restore", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }

    public static void get_host_logs_download(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/host_logs_download", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }

    public static void put_pool_patch_upload(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/pool_patch_upload", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }

    public static void get_pool_patch_download(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/pool_patch_download", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "uuid", (object) uuid);
    }

    public static void put_oem_patch_stream(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/oem_patch_stream", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }

    public static void get_vncsnapshot(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/vncsnapshot", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "uuid", (object) uuid);
    }

    public static void get_pool_xml_db_sync(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/pool/xmldbdump", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }

    public static void get_system_status(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string entries, string output)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/system-status", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "entries", (object) entries, (object) "output", (object) output);
    }

    public static void vm_rrd(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string uuid)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/vm_rrd", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "uuid", (object) uuid);
    }

    public static void host_rrd(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, bool json)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/host_rrd", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "json", (object) (bool) (json ? 1 : 0));
    }

    public static void rrd_updates(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, long start, string cf, long interval, bool host, string uuid, bool json)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/rrd_updates", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "start", (object) start, (object) "cf", (object) cf, (object) "interval", (object) interval, (object) "host", (object) (bool) (host ? 1 : 0), (object) "uuid", (object) uuid, (object) "json", (object) (bool) (json ? 1 : 0));
    }

    public static void put_blob(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string reff)
    {
      HTTP_actions.Put(progressDelegate, cancellingDelegate, timeout_ms, hostname, "/blob", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "ref", (object) reff);
    }

    public static void get_wlb_report(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id, string report, params string[] args)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/wlb_report", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id, (object) "report", (object) report, (object) args);
    }

    public static void get_wlb_diagnostics(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/wlb_diagnostics", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }

    public static void get_audit_log(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, int timeout_ms, string hostname, IWebProxy proxy, string path, string task_id, string session_id)
    {
      HTTP_actions.Get(dataCopiedDelegate, cancellingDelegate, timeout_ms, hostname, "/audit_log", proxy, path, (object) "task_id", (object) task_id, (object) "session_id", (object) session_id);
    }
  }
}
