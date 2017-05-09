using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
namespace Halsign.DWM.Framework
{
	public static class Configuration
	{
		private const string KEY_VAL_DELIM = "=";
		private static string _configurationFileName;
		private static FileSystemWatcher _fileWatcher;
		private static string _confFileDirectory;
		private static string _confFileFullName;
		private static Dictionary<ConfigItems, string> _configItemsDict;
		private static DateTime _lastLogTime;
		private static bool _reloading;
		private static object lockObject;
		public static string ConfigFileName
		{
			get
			{
				return Configuration._configurationFileName;
			}
			set
			{
				Configuration._configurationFileName = value;
				Configuration._confFileFullName = Path.Combine(Configuration._confFileDirectory, value);
				Configuration._fileWatcher.Filter = Configuration._configurationFileName;
				Configuration.ReloadConfiguration();
			}
		}
		static Configuration()
		{
			Configuration._configurationFileName = "wlb.conf";
			Configuration._lastLogTime = DateTime.MinValue;
			Configuration._reloading = false;
			Configuration.lockObject = new object();
			Configuration._confFileDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			Configuration._confFileFullName = Path.Combine(Configuration._confFileDirectory, Configuration._configurationFileName);
			Configuration._fileWatcher = new FileSystemWatcher();
			Configuration._fileWatcher.Path = Configuration._confFileDirectory;
			Configuration._fileWatcher.Filter = Configuration._configurationFileName;
			Configuration._fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			Configuration._fileWatcher.Changed += new FileSystemEventHandler(Configuration.ReloadConfiguration);
			Configuration._fileWatcher.EnableRaisingEvents = true;
			Configuration._configItemsDict = new Dictionary<ConfigItems, string>();
			Configuration.LoadConfiguration();
		}
		private static void LoadConfiguration()
		{
			Array values = Enum.GetValues(typeof(ConfigItems));
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!File.Exists(Configuration._confFileFullName))
			{
				throw new FileNotFoundException(string.Format("{0} not exists", Configuration._confFileFullName));
			}
			try
			{
				using (StreamReader streamReader = new StreamReader(Configuration._confFileFullName))
				{
					while (!streamReader.EndOfStream)
					{
						string text = streamReader.ReadLine().Trim();
						if (!string.IsNullOrEmpty(text) && !text.TrimStart(new char[0]).StartsWith("#"))
						{
							if (text.Contains("="))
							{
								string text2 = Configuration.ExtractKey(text);
								string value = Configuration.ExtractValue(text);
								if (dictionary.ContainsKey(text2.ToLower()))
								{
									throw new ArgumentException(text2 + " has duplicate entry in configuration file.");
								}
								dictionary.Add(text2.ToLower(), value);
							}
						}
					}
				}
				List<string> list = new List<string>();
				IEnumerator enumerator = values.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ConfigItems configItems = (ConfigItems)enumerator.Current;
						string text3;
						if (dictionary.TryGetValue(configItems.ToString("G").ToLower(), out text3))
						{
							object obj = Configuration.lockObject;
							Monitor.Enter(obj);
							try
							{
								if (!Configuration._configItemsDict.ContainsKey(configItems))
								{
									Configuration._configItemsDict.Add(configItems, text3);
								}
								else
								{
									if (Configuration._configItemsDict[configItems] != text3)
									{
										Configuration._configItemsDict[configItems] = text3;
										if (Configuration._reloading)
										{
											list.Add(configItems.ToString("G"));
										}
									}
								}
							}
							finally
							{
								Monitor.Exit(obj);
							}
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				if (Configuration._reloading && list.Count > 0)
				{
					Logger.LogInfo(string.Format("Following configurations were changed: {0}", string.Join(",", list.ToArray())), new object[0]);
					Configuration._reloading = false;
				}
			}
			catch (IOException)
			{
			}
			catch
			{
				throw;
			}
		}
		private static void ReloadConfiguration(object sender, FileSystemEventArgs e)
		{
			try
			{
				if (Configuration._lastLogTime == DateTime.MinValue || (DateTime.Now - Configuration._lastLogTime).TotalMilliseconds > 2000.0)
				{
					Configuration._lastLogTime = DateTime.Now;
					Logger.LogInfo("{0} changed, reloading configurations...", new object[]
					{
						Configuration.ConfigFileName
					});
					Configuration._reloading = true;
					Configuration.LoadConfiguration();
				}
			}
			catch
			{
			}
		}
		private static string ExtractKey(string s)
		{
			int length = s.IndexOf("=", 0);
			return s.Substring(0, length).Trim();
		}
		private static string ExtractValue(string s)
		{
			int num = s.IndexOf("=", 0);
			return s.Substring(num + 1, s.Length - num - 1).Trim();
		}
		public static void ReloadConfiguration()
		{
			object obj = Configuration.lockObject;
			Monitor.Enter(obj);
			try
			{
				Configuration._configItemsDict.Clear();
				Configuration.LoadConfiguration();
			}
			finally
			{
				Monitor.Exit(obj);
			}
		}
		public static int GetValueAsInt(ConfigItems configName)
		{
			return Configuration.InternalGetValueAsInt(configName, null);
		}
		public static int GetValueAsInt(ConfigItems configName, int defaultValue)
		{
			return Configuration.InternalGetValueAsInt(configName, defaultValue);
		}
		private static int InternalGetValueAsInt(ConfigItems configName, object defaultValue)
		{
			string value = Configuration.GetValue(configName, defaultValue);
			int result = 0;
			if (!int.TryParse(value, out result) && defaultValue != null)
			{
				result = (int)defaultValue;
			}
			return result;
		}
		public static bool GetValueAsBool(ConfigItems configName)
		{
			return Configuration.InternalGetValueAsBool(configName, null);
		}
		public static bool GetValueAsBool(ConfigItems configName, bool defaultValue)
		{
			return Configuration.InternalGetValueAsBool(configName, defaultValue);
		}
		private static bool InternalGetValueAsBool(ConfigItems configName, object defaultValue)
		{
			string a = Configuration.GetValue(configName, defaultValue).Trim().ToLower();
			bool result = false;
			if (a == "true" || a == "1")
			{
				result = true;
			}
			return result;
		}
		public static string GetValueAsString(ConfigItems configName)
		{
			return Configuration.InternalGetValueAsString(configName, null);
		}
		public static string GetValueAsString(ConfigItems configName, string defaultValue)
		{
			return Configuration.InternalGetValueAsString(configName, defaultValue);
		}
		private static string InternalGetValueAsString(ConfigItems configName, object defaultValue)
		{
			return Configuration.GetValue(configName, defaultValue);
		}
		public static long GetValueAsLong(ConfigItems configName)
		{
			return Configuration.InternalGetValueAsLong(configName, null);
		}
		public static long GetValueAsLong(ConfigItems configName, long defaultValue)
		{
			return Configuration.InternalGetValueAsLong(configName, defaultValue);
		}
		private static long InternalGetValueAsLong(ConfigItems configName, object defaultValue)
		{
			string value = Configuration.GetValue(configName, defaultValue);
			long result = 0L;
			if (!long.TryParse(value, out result) && defaultValue != null)
			{
				result = (long)defaultValue;
			}
			return result;
		}
		public static double GetValueAsDouble(ConfigItems configName)
		{
			return Configuration.InternalGetValueAsDouble(configName, null);
		}
		public static double GetValueAsDouble(ConfigItems configName, double defaultValue)
		{
			return Configuration.InternalGetValueAsDouble(configName, defaultValue);
		}
		private static double InternalGetValueAsDouble(ConfigItems configName, object defaultValue)
		{
			string value = Configuration.GetValue(configName, defaultValue);
			double result = 0.0;
			if (!double.TryParse(value, out result) && defaultValue != null)
			{
				result = (double)defaultValue;
			}
			return result;
		}
		private static string GetValue(ConfigItems configName, object defaultValue)
		{
			string result = string.Empty;
			object obj = Configuration.lockObject;
			Monitor.Enter(obj);
			try
			{
				if (!Configuration._configItemsDict.ContainsKey(configName) || string.IsNullOrEmpty(Configuration._configItemsDict[configName]))
				{
					if (defaultValue != null)
					{
						return defaultValue.ToString();
					}
					if (!Configuration._configItemsDict.ContainsKey(configName))
					{
						throw new KeyNotFoundException("'" + configName.ToString("G") + "' not found in " + Configuration._configurationFileName);
					}
					if (string.IsNullOrEmpty(Configuration._configItemsDict[configName]))
					{
						throw new KeyNotFoundException(string.Format("'{0}' has no value set and no default value was supplied.", configName.ToString("G")));
					}
				}
				result = Configuration._configItemsDict[configName];
			}
			finally
			{
				Monitor.Exit(obj);
			}
			return result;
		}
		public static void SetValue(ConfigItems confName, bool value)
		{
			Configuration.InternalSetValue(confName, value);
		}
		public static void SetValue(ConfigItems confName, int value)
		{
			Configuration.InternalSetValue(confName, value);
		}
		public static void SetValue(ConfigItems confName, double value)
		{
			Configuration.InternalSetValue(confName, value);
		}
		public static void SetValue(ConfigItems confName, long value)
		{
			Configuration.InternalSetValue(confName, value);
		}
		public static void SetValue(ConfigItems confName, string value)
		{
			Configuration.InternalSetValue(confName, value);
		}
		private static void InternalSetValue(ConfigItems confName, object value)
		{
			object obj = Configuration.lockObject;
			Monitor.Enter(obj);
			try
			{
				if (!Configuration._configItemsDict.ContainsKey(confName))
				{
					throw new KeyNotFoundException(confName.ToString("G") + " not found in " + Configuration._configurationFileName);
				}
				Configuration._configItemsDict[confName] = value.ToString();
			}
			finally
			{
				Monitor.Exit(obj);
			}
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (StreamReader streamReader = new StreamReader(Configuration._confFileFullName))
				{
					string text = string.Empty;
					while ((text = streamReader.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(text) && !text.TrimStart(new char[0]).StartsWith("#") && text.Contains("="))
						{
							string text2 = Configuration.ExtractKey(text);
							if (text2.ToLower() == confName.ToString("G").ToLower())
							{
								string value2 = string.Format("{0} = {1}", text2, value);
								stringBuilder.AppendLine(value2);
								continue;
							}
						}
						stringBuilder.AppendLine(text);
					}
				}
				try
				{
					Configuration._fileWatcher.EnableRaisingEvents = false;
					using (StreamWriter streamWriter = new StreamWriter(Configuration._confFileFullName, false))
					{
						streamWriter.Write(stringBuilder);
					}
				}
				finally
				{
					Configuration._fileWatcher.EnableRaisingEvents = true;
				}
			}
			catch (Exception ex)
			{
				try
				{
					Logger.LogException(ex);
				}
				catch
				{
				}
				throw;
			}
		}
	}
}
