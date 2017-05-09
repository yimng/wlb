using Halsign.DWM.Framework;
using ServiceStack.Common.Web;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
namespace Halsign.DWM.Communication2
{
	public class DiagnosticsService : WlbServiceBase<Diagnostics>
	{
		public override object OnGet(Diagnostics request)
		{
			object result;
			try
			{
				result = new DiagnosticsResponse
				{
					WlbDiagnostics = this.InternalGetDiagnostics()
				};
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				throw new HttpError(HttpStatusCode.InternalServerError, base.GetExceptionErrorCode(ex).ToString(), ex.Message);
			}
			return result;
		}
		private string InternalGetDiagnostics()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("WLB diagnostic data:  {0} ({1} UTC)", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss"));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Database version: {0}", Configuration.GetValueAsInt(ConfigItems.DBSchemaVersion));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			stringBuilder.AppendFormat("Files in {0}", directoryInfo.FullName);
			stringBuilder.AppendLine();
			for (int i = 0; i < fileSystemInfos.Length; i++)
			{
				if ((fileSystemInfos[i].Attributes & FileAttributes.Directory) == (FileAttributes)0 && (Localization.Compare(fileSystemInfos[i].Extension, ".exe", true) == 0 || Localization.Compare(fileSystemInfos[i].Extension, ".dll", true) == 0))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fileSystemInfos[i].FullName);
					stringBuilder.AppendFormat("   {0}:  version={1}", fileSystemInfos[i].Name, versionInfo.FileVersion);
					stringBuilder.AppendLine();
				}
			}
			stringBuilder.AppendLine();
			string text = Path.Combine(Logger.GetLogDirectory(), Logger.LogFileName);
			stringBuilder.AppendFormat("Contents of log file '{0}':", text);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			try
			{
				FileInfo fileInfo = new FileInfo(text);
				int num = Configuration.GetValueAsInt(ConfigItems.DiagnosticsSizeKB) * 1000;
				if (fileInfo.Length < (long)num)
				{
					if (!string.IsNullOrEmpty(Logger.LastBackupLogFileName))
					{
						stringBuilder.AppendLine(this.GetLastBytesFromFile(Logger.LastBackupLogFileName, num - (int)fileInfo.Length));
					}
					stringBuilder.AppendLine(this.GetLastBytesFromFile(text, (int)fileInfo.Length));
				}
				else
				{
					stringBuilder.AppendLine(this.GetLastBytesFromFile(text, num));
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendFormat("Error reading log file: {0}", ex.Message);
				stringBuilder.AppendLine();
				Logger.LogException(ex);
			}
			return stringBuilder.ToString();
		}
		private string GetLastBytesFromFile(string filename, int bytes)
		{
			FileStream fileStream = null;
			string @string;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open);
				if (fileStream.Length < (long)bytes)
				{
					bytes = (int)fileStream.Length;
				}
				byte[] array = new byte[bytes];
				fileStream.Seek((long)(-(long)bytes), SeekOrigin.End);
				fileStream.Read(array, 0, array.Length);
				@string = Encoding.ASCII.GetString(array);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return @string;
		}
	}
}
