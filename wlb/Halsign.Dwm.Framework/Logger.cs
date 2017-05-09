using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
namespace Halsign.DWM.Framework
{
	public sealed class Logger
	{
		public const int EventIdCantWriteLogFile = 17001;
		public const int DefaultMaxFileSize = 10;
		public const int DefaultMaxLogFileCount = 10;
		internal const string kCompanyName = "Halsign";
		internal const string kProductName = "Workload Balancing";
		private const string kLogFileBase = "LogFile";
		private const string kBackFileBase = "LogFile_bck_";
		private const string kLogFileName = "LogFile.log";
		private static object logFileLock;
		private static string _logFile;
		private static string _logFileDirectory;
		private static bool _wroteLogFileGetFileExceptionEvent;
		private static bool _wroteLogFileIoExceptionEvent;
		private static bool _wroteLogFileAccessExceptionEvent;
		private static bool _wroteLogFileGeneralExceptionEvent;
		private static bool _logThreadDetails;
		private static bool _isConsoleLoggingSetExplicitly;
		private static bool _consoleLogEnabled;
		private static int _maxLogFileSizeMB;
		private static int _maxLogFileCount;
		private static bool _logInMillisecond;
		private static string _lastBackupLog;
		public static bool ConsoleLogEnabled
		{
			get
			{
				return Logger._consoleLogEnabled;
			}
			set
			{
				Logger._consoleLogEnabled = value;
				Logger._isConsoleLoggingSetExplicitly = true;
			}
		}
		public static string CompanyName
		{
			get
			{
				return "Halsign";
			}
		}
		public static string ProductName
		{
			get
			{
				return "Workload Balancing";
			}
		}
		public static string LogFileName
		{
			get
			{
				return "LogFile.log";
			}
		}
		public static string LogFileBackupBase
		{
			get
			{
				return "LogFile_bck_";
			}
		}
		public static string LastBackupLogFileName
		{
			get
			{
				if (string.IsNullOrEmpty(Logger._lastBackupLog))
				{
					string[] files = Directory.GetFiles(Logger._logFileDirectory, string.Format("{0}_bck_*", "LogFile"), SearchOption.TopDirectoryOnly);
					if (files.Length == 0)
					{
						return null;
					}
					Logger._lastBackupLog = files[0];
					DateTime creationTime = new FileInfo(Logger._lastBackupLog).CreationTime;
					for (int i = 1; i < files.Length; i++)
					{
						FileInfo fileInfo = new FileInfo(files[i]);
						if (fileInfo.CreationTime > creationTime)
						{
							creationTime = fileInfo.CreationTime;
							Logger._lastBackupLog = files[i];
						}
					}
				}
				return Logger._lastBackupLog;
			}
		}
		static Logger()
		{
			Logger.logFileLock = new object();
			Logger._isConsoleLoggingSetExplicitly = false;
			Logger._logFileDirectory = Configuration.GetValueAsString(ConfigItems.LogFileLocation);
		}
		public static void Trace(string msg)
		{
			Logger.Log(msg, new object[0]);
			System.Diagnostics.Trace.WriteLine(msg);
		}
		public static void Trace(string fmt, params object[] args)
		{
			Logger.Log(fmt, args);
		}
		public static void LogError(string fmt, params object[] args)
		{
			Logger.Log("ERROR: " + fmt, args);
		}
		public static void LogWarning(string fmt, params object[] args)
		{
			Logger.Log("WARNING: " + fmt, args);
		}
		public static void LogInfo(string fmt, params object[] args)
		{
			Logger.Log("INFO: " + fmt, args);
		}
		public static void LogException(Exception ex)
		{
			Logger.LogException(ex, null);
		}
		public static void LogException(Exception ex, IDbCommand cmd)
		{
			Logger.LogException(ex, cmd, null);
		}
		public static void LogException(Exception ex, IDbCommand cmd, string exceptionMessage)
		{
			if (ex != null)
			{
				Logger.Log("EXCEPTION: {0}", new object[]
				{
					(exceptionMessage != null) ? string.Format("{0} {1}", exceptionMessage, ex.ToString()) : ex.ToString()
				});
				if (cmd != null && !string.IsNullOrEmpty(cmd.CommandText))
				{
					if (Configuration.GetValueAsBool(ConfigItems.TraceDBCommandText))
					{
						Logger.Log("SQL Command Text for Exception: {0}", new object[]
						{
							cmd.CommandText
						});
					}
					else
					{
						int num = 200;
						string text = (cmd.CommandText.Length > num) ? (cmd.CommandText.Substring(0, num) + "...") : cmd.CommandText;
						Logger.Log("SQL Command Text for Exception: {0}", new object[]
						{
							text
						});
					}
				}
			}
		}
		private static void Log(string fmt, params object[] args)
		{
			string text = string.Empty;
			Logger._logThreadDetails = Configuration.GetValueAsBool(ConfigItems.LogThreadDetails);
			Logger._maxLogFileSizeMB = Configuration.GetValueAsInt(ConfigItems.MaxLogFileSizeInMB, 10);
			Logger._maxLogFileCount = Configuration.GetValueAsInt(ConfigItems.MaxLogFileCount, 10);
			if (Logger._logThreadDetails)
			{
				text = string.Format("({0}[{1}])", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId.ToString());
			}
			string text2;
			if (args != null && args.Length > 0)
			{
				text2 = string.Format(fmt, args);
			}
			else
			{
				text2 = fmt;
			}
			Logger._logInMillisecond = Configuration.GetValueAsBool(ConfigItems.LogInMillisecond);
			string format = (!Logger._logInMillisecond) ? "MM/dd/yyyy HH:mm:ss" : "MM/dd/yyyy HH:mm:ss.fff";
			text2 = string.Format("{0} - {1}{2}: {3} ", new object[]
			{
				DateTime.Now.ToString(format),
				Environment.GetEnvironmentVariable("ProcName", EnvironmentVariableTarget.Process),
				text,
				text2
			});
			try
			{
				if (Logger._isConsoleLoggingSetExplicitly)
				{
					if (Logger.ConsoleLogEnabled)
					{
						Console.WriteLine(text2);
					}
				}
				else
				{
					if (Configuration.GetValueAsBool(ConfigItems.ConsoleLogEnabled))
					{
						Console.WriteLine(text2);
					}
				}
			}
			catch (Exception)
			{
			}
			Debug.WriteLine(text2);
			object obj = Logger.logFileLock;
			Monitor.Enter(obj);
			try
			{
				string logFile = Logger.GetLogFile();
				if (!string.IsNullOrEmpty(logFile))
				{
					try
					{
						using (StreamWriter streamWriter = new StreamWriter(logFile, true))
						{
							streamWriter.WriteLine(text2);
						}
					}
					catch (IOException ex)
					{
						if (!Logger._wroteLogFileIoExceptionEvent)
						{
							string msg = string.Format("System.IO.IOException exception trying to write to the log file.\n{0}\n{1}", ex.Message, ex.StackTrace);
							Logger.WriteEventLog(msg);
							Logger._wroteLogFileIoExceptionEvent = true;
						}
					}
					catch (UnauthorizedAccessException ex2)
					{
						if (!Logger._wroteLogFileAccessExceptionEvent)
						{
							string msg2 = string.Format("System.UnauthorizedAccessException exception trying to write to the log file.  Check the permissions on the directory containing {0}.\n\n{1}\n{2}", logFile, ex2.Message, ex2.StackTrace);
							Logger.WriteEventLog(msg2);
							Logger._wroteLogFileAccessExceptionEvent = true;
						}
					}
					catch (Exception ex3)
					{
						if (!Logger._wroteLogFileGeneralExceptionEvent)
						{
							string msg3 = string.Format("{0} exception trying to write to the log file.\n{1}\n{2}", ex3.GetType().ToString(), ex3.Message, ex3.StackTrace);
							Logger.WriteEventLog(msg3);
							Logger._wroteLogFileGeneralExceptionEvent = true;
						}
					}
				}
			}
			finally
			{
				Monitor.Exit(obj);
			}
		}
		public static string GetLogDirectory()
		{
			if (!string.IsNullOrEmpty(Logger._logFileDirectory) && !Directory.Exists(Logger._logFileDirectory))
			{
				Directory.CreateDirectory(Logger._logFileDirectory);
			}
			return Logger._logFileDirectory;
		}
		private static string GetLogFile()
		{
			try
			{
				if (Logger._logFile == null)
				{
					Logger.GetLogDirectory();
					Logger._logFile = Logger.CombinePaths(new string[]
					{
						Logger._logFileDirectory,
						"LogFile.log"
					});
				}
				FileInfo fileInfo = new FileInfo(Logger._logFile);
				if (Logger._maxLogFileSizeMB < 1)
				{
					Logger._maxLogFileSizeMB = 10;
				}
				if (fileInfo.Exists && fileInfo.Length >= (long)(Logger._maxLogFileSizeMB * 1024 * 1024))
				{
					string[] files = Directory.GetFiles(Logger._logFileDirectory, string.Format("{0}_bck_*", "LogFile"), SearchOption.TopDirectoryOnly);
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					while (files != null && num3 < files.Length)
					{
						int num4 = files[num3].LastIndexOf('_') + 1;
						int num5 = files[num3].LastIndexOf(".log");
						string s = files[num3].Substring(num4, num5 - num4);
						int num6 = 0;
						if (int.TryParse(s, out num6))
						{
							if (num6 >= num)
							{
								num = num6 + 1;
							}
							if (num6 <= num2)
							{
								num2 = num6;
							}
						}
						num3++;
					}
					string text = Logger.CombinePaths(new string[]
					{
						Logger._logFileDirectory,
						string.Format("{0}{1}.log", "LogFile_bck_", num)
					});
					File.Move(Logger._logFile, text);
					Logger._lastBackupLog = text;
					if (Logger._maxLogFileCount < 1)
					{
						Logger._maxLogFileCount = 10;
					}
					if (files.Length > Logger._maxLogFileCount)
					{
						for (int i = num2; i <= num - Logger._maxLogFileCount; i++)
						{
							string path = Logger.CombinePaths(new string[]
							{
								Logger._logFileDirectory,
								string.Format("{0}{1}.log", "LogFile_bck_", i)
							});
							if (File.Exists(path))
							{
								try
								{
									File.Delete(path);
								}
								catch
								{
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (!Logger._wroteLogFileGetFileExceptionEvent)
				{
					string msg = string.Format("{0} exception trying to get the name of the log file.\n{1}\n{2}", ex.GetType().ToString(), ex.Message, ex.StackTrace);
					Logger.WriteEventLog(msg);
					Logger._wroteLogFileGetFileExceptionEvent = true;
				}
			}
			return Logger._logFile;
		}
		private static void WriteEventLog(string msg)
		{
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable("ProcName", EnvironmentVariableTarget.Process);
				string text = null;
				if (Localization.Compare(environmentVariable, "DwmDataColSvc.exe", true) == 0)
				{
					text = "DwmCollectionSvc";
				}
				else
				{
					if (Localization.Compare(environmentVariable, "DwmConfigSvc.exe", true) == 0)
					{
						text = "DwmConfigSvc";
					}
					else
					{
						if (Localization.Compare(environmentVariable, "DwmAnalEngSvc.exe", true) == 0)
						{
							text = "DwmAnalysisEngSvc";
						}
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					EventLog.WriteEntry(text, msg, EventLogEntryType.Information, 17001);
				}
			}
			catch (Exception)
			{
			}
		}
		private static string CombinePaths(params string[] paths)
		{
			string text = string.Empty;
			for (int i = 0; i < paths.Length; i++)
			{
				string path = paths[i];
				text = Path.Combine(text, path);
			}
			return text;
		}
	}
}
