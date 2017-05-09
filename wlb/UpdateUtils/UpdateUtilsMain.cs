using Halsign.DWM.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
namespace UpdateUtils
{
	internal class UpdateUtilsMain
	{
		[Flags]
		private enum Update
		{
			None = 0,
			PreDbUpdate = 1,
			SchemaUpdate = 2,
			FunctionUpdate = 4,
			PostDbUpdate = 8
		}
		private static int ERROR = 1;
		private static int SUCCESS = 0;
		private static string _flagFilesDir = "/tmp/wlb/update";
		private static int Main(string[] args)
		{
			Environment.SetEnvironmentVariable("ProcName", "UpdateUtils", EnvironmentVariableTarget.Process);
			Logger.ConsoleLogEnabled = false;
			if (!UpdateUtilsMain.PerformActions(args))
			{
				UpdateUtilsMain.ShowUsage();
				return UpdateUtilsMain.ERROR;
			}
			return UpdateUtilsMain.SUCCESS;
		}
		private static bool PerformActions(string[] cmdArgs)
		{
			if (cmdArgs.Length == 0)
			{
				return false;
			}
			string text = cmdArgs[0].Trim();
			switch (text)
			{
			case "-c":
			case "--migrate-config":
			{
				if (cmdArgs.Length != 3)
				{
					return false;
				}
				string text2 = null;
				if (!File.Exists(cmdArgs[1]))
				{
					text2 = cmdArgs[1];
				}
				else
				{
					if (!File.Exists(cmdArgs[2]))
					{
						text2 = cmdArgs[2];
					}
				}
				if (text2 != null)
				{
					Console.Error.WriteLine(string.Format("File '{0}' does not exist.", text2));
					return false;
				}
				UpdateUtilsMain.MigrateConfig(cmdArgs[1], cmdArgs[2]);
				return true;
			}
			case "-d":
			case "--migrate-db":
			{
				string sqlFilePath = null;
				if (cmdArgs.Length == 2)
				{
					if (!Directory.Exists(cmdArgs[1]))
					{
						Console.Error.WriteLine("'{0}' directory does not exist.", cmdArgs[1]);
						return false;
					}
					sqlFilePath = cmdArgs[1];
				}
				UpdateUtilsMain.UpdateDatabase(sqlFilePath);
				return true;
			}
			case "-t":
			case "--take-snapshot":
				throw new NotImplementedException();
			case "-r":
			case "--revert-snapshot":
				throw new NotImplementedException();
			}
			Console.Error.WriteLine(string.Format("Unrecognized command '{0}'", cmdArgs[0]));
			return true;
		}
		private static void ShowUsage()
		{
			Console.WriteLine("UpdateUtils performs different functions required to update Halsign Workload Balancing");
			Console.WriteLine("Usage:");
			Console.WriteLine("   UpdateUtils.exe <command> <options>");
			Console.WriteLine("Commands and options:");
			Console.WriteLine("   -c, --migrate-config <old-config-file-name> <new-config-file-name>");
			Console.WriteLine("       Migrate configurations from <old-config-file> to <new-config-file>.");
			Console.WriteLine("   -d, --migrate-db <sql-files-path>");
			Console.WriteLine("       Migrate database. If no file path is provided, current directory is used.");
			Console.WriteLine("   -t, --take-snapshot <snapshot-name>");
			Console.WriteLine("       Take a snapshot of WLB vpx.");
			Console.WriteLine("   -r, --revert-snapshot <snapshot-name>");
		}
		private static Dictionary<string, string> ParseAndGetNameValuePairsInDataBase()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			using (DBAccess dBAccess = new DBAccess())
			{
				DataSet dataSet = dBAccess.ExecuteDataSet("select category, item_name, value from core_config");
				IEnumerator enumerator = dataSet.Tables[0].Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataRow dataRow = (DataRow)enumerator.Current;
						string text = dataRow["category"].ToString();
						string text2 = dataRow["item_name"].ToString();
						string text3 = text;
						switch (text3)
						{
						case "General":
						{
							string text4 = text2;
							if (text4 != null)
							{
                                Dictionary<string, int> dic = null;
								if (dic == null)
								{
									dic = new Dictionary<string, int>(1)
									{

										{
											"DBSchemaVersion",
											0
										}
									};
								}
								int num2;
								if (dic.TryGetValue(text4, out num2))
								{
									if (num2 == 0)
									{
										dictionary["DBSchemaVersion"] = dataRow["value"].ToString();
									}
								}
							}
							break;
						}
						case "Collection":
						{
							string text4 = text2;
							if (text4 != null)
							{
                                Dictionary<string, int> dic = null;
								if (dic == null)
								{
									dic = new Dictionary<string, int>(2)
									{

										{
											"PollInterval",
											0
										},

										{
											"AuditLogRetrievalInterval",
											1
										}
									};
								}
								int num2;
								if (dic.TryGetValue(text4, out num2))
								{
									if (num2 != 0)
									{
										if (num2 == 1)
										{
											dictionary["AuditLogRetrievalInterval"] = (Convert.ToInt32(dataRow["value"]) * 60).ToString();
										}
									}
									else
									{
										dictionary["DataCollectionPollInterval"] = dataRow["value"].ToString();
									}
								}
							}
							break;
						}
						case "Intervals":
						{
							string text4 = text2;
							if (text4 != null)
							{
                                Dictionary<string, int> dic = null;
								if (dic == null)
								{
									dic = new Dictionary<string, int>(1)
									{

										{
											"PollInterval",
											0
										}
									};
								}
								int num2;
								if (dic.TryGetValue(text4, out num2))
								{
									if (num2 == 0)
									{
										dictionary["AnalysisEnginePollInterval"] = dataRow["value"].ToString();
									}
								}
							}
							break;
						}
						case "Security":
						{
							string text4 = text2;
							if (text4 != null)
							{
                                Dictionary<string, int> dic = null;
								if (dic == null)
								{
									dic = new Dictionary<string, int>(2)
									{

										{
											"WlbUsername",
											0
										},

										{
											"WlbPassword",
											1
										}
									};
								}
								int num2;
								if (dic.TryGetValue(text4, out num2))
								{
									if (num2 != 0)
									{
										if (num2 == 1)
										{
											dictionary["WlbPassword"] = dataRow["value"].ToString();
										}
									}
									else
									{
										dictionary["WlbUsername"] = dataRow["value"].ToString();
									}
								}
							}
							break;
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
			}
			return dictionary;
		}
		private static void MigrateConfig(string oldConfFile, string newConfFile)
		{
			string fileName = "migrateconfig";
			List<string> list = new List<string>();
			list.AddRange(new string[]
			{
				"WLBVersion"
			});
			try
			{
				ConfFile confFile = new ConfFile(oldConfFile);
				ConfFile confFile2 = new ConfFile(newConfFile);
				Dictionary<string, string> dictionary = confFile.ParseAndGetNameValuePairs();
				Dictionary<string, string> dictionary2 = confFile2.ParseAndGetNameValuePairs();
				Configuration.SetValue(ConfigItems.CryptoHash, dictionary["CryptoHash"]);
				Configuration.SetValue(ConfigItems.DBServer, dictionary["DBServer"]);
				Configuration.SetValue(ConfigItems.DBName, dictionary["DBName"]);
				Configuration.SetValue(ConfigItems.DBPort, dictionary["DBPort"]);
				Configuration.SetValue(ConfigItems.DBUsername, dictionary["DBUsername"]);
				Configuration.SetValue(ConfigItems.DBPassword, dictionary["DBPassword"]);
				Dictionary<string, string> dictionary3 = UpdateUtilsMain.ParseAndGetNameValuePairsInDataBase();
				foreach (string current in dictionary3.Keys)
				{
					if (dictionary2.ContainsKey(current) && !dictionary.ContainsKey(current))
					{
						Logger.LogInfo("MigrateConfig core_config to wlb.conf {0}={1}", new object[]
						{
							current,
							dictionary3[current]
						});
						dictionary[current] = dictionary3[current];
					}
				}
				foreach (KeyValuePair<string, string> current2 in dictionary)
				{
					if (dictionary2.ContainsKey(current2.Key) && dictionary2[current2.Key] != dictionary[current2.Key] && !list.Contains(current2.Key))
					{
						confFile2.SetValue(current2.Key, current2.Value);
					}
				}
				confFile2.Save();
				UpdateUtilsMain.TouchFlagFile(fileName, true);
			}
			catch
			{
				UpdateUtilsMain.TouchFlagFile(fileName, false);
			}
		}
		private static int GetCurrentSchemaVersion()
		{
			int result;
			try
			{
				result = Configuration.GetValueAsInt(ConfigItems.DBSchemaVersion);
			}
			catch (DwmException)
			{
				Logger.LogInfo("DBSchemaVersion config item not found in database. Returning 0.", new object[0]);
				result = 0;
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				result = 0;
			}
			return result;
		}
		private static void UpdateDatabase(string sqlFilePath)
		{
			string fileName = "dbupdate";
			string path = string.Empty;
			string path2 = string.Empty;
			string path3 = string.Empty;
			string path4 = string.Empty;
			string path5 = (sqlFilePath != null) ? sqlFilePath : Directory.GetCurrentDirectory();
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			int num = 0;
			UpdateUtilsMain.Update update = UpdateUtilsMain.Update.None;
			while (true)
			{
				int currentSchemaVersion = UpdateUtilsMain.GetCurrentSchemaVersion();
				num = currentSchemaVersion + 1;
				try
				{
					path = Path.Combine(path5, "preDbUpdate_" + num + ".sql");
					path2 = Path.Combine(path5, "schemaUpdate_" + num + ".sql");
					path3 = Path.Combine(path5, "functionUpdate_" + num + ".sql");
					path4 = Path.Combine(path5, "postDbUpdate_" + num + ".sql");
					if (!File.Exists(path))
					{
						break;
					}
					text = File.ReadAllText(path);
					text2 = File.ReadAllText(path2);
					text3 = File.ReadAllText(path3);
					text4 = File.ReadAllText(path4);
					update = UpdateUtilsMain.Update.None;
					using (DBAccess dBAccess = new DBAccess())
					{
						if (!string.IsNullOrEmpty(text))
						{
							dBAccess.ExecuteNonQuery(text);
							update = ((update == UpdateUtilsMain.Update.None) ? UpdateUtilsMain.Update.PreDbUpdate : (update | UpdateUtilsMain.Update.PreDbUpdate));
							Logger.LogInfo("Finished running database pre-update script.", new object[0]);
						}
						else
						{
							Logger.LogInfo("No preUpdateDbSqlScript found", new object[0]);
						}
						if (!string.IsNullOrEmpty(text2))
						{
							dBAccess.ExecuteNonQuery(text2);
							update = ((update == UpdateUtilsMain.Update.None) ? UpdateUtilsMain.Update.SchemaUpdate : (update | UpdateUtilsMain.Update.SchemaUpdate));
							Logger.LogInfo("Finished running database schema-update script.", new object[0]);
						}
						else
						{
							Logger.LogInfo("No updateDbSqlScript found", new object[0]);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							dBAccess.ExecuteNonQuery(text3);
							update = ((update == UpdateUtilsMain.Update.None) ? UpdateUtilsMain.Update.FunctionUpdate : (update | UpdateUtilsMain.Update.FunctionUpdate));
							Logger.LogInfo("Finished running database function-update script.", new object[0]);
						}
						else
						{
							Logger.LogInfo("No functionUpdateSqlScript found", new object[0]);
						}
						if (!string.IsNullOrEmpty(text4))
						{
							dBAccess.ExecuteNonQuery(text4);
							update = ((update == UpdateUtilsMain.Update.None) ? UpdateUtilsMain.Update.PostDbUpdate : (update | UpdateUtilsMain.Update.PostDbUpdate));
							Logger.LogInfo("Finished running database post-update script.", new object[0]);
						}
						else
						{
							Logger.LogInfo("No postDbSqlScript found", new object[0]);
						}
					}
					if (update != (UpdateUtilsMain.Update.PreDbUpdate | UpdateUtilsMain.Update.SchemaUpdate | UpdateUtilsMain.Update.FunctionUpdate | UpdateUtilsMain.Update.PostDbUpdate))
					{
						throw new DwmException("Some sql script files are missing, add missing files and try again.");
					}
					Configuration.SetValue(ConfigItems.DBSchemaVersion, num);
					UpdateUtilsMain.TouchFlagFile(fileName, true);
					Logger.LogInfo("Finished updating database schema version to {0}", new object[]
					{
						num.ToString()
					});
				}
				catch (Exception ex)
				{
					Logger.LogException(ex);
					UpdateUtilsMain.TouchFlagFile(fileName, false);
					break;
				}
			}
		}
		private static void TouchFlagFile(string fileName, bool succeeded, string content)
		{
			if (!Directory.Exists(UpdateUtilsMain._flagFilesDir))
			{
				Directory.CreateDirectory(UpdateUtilsMain._flagFilesDir);
			}
			fileName += ((!succeeded) ? ".failed" : ".passed");
			using (StreamWriter streamWriter = new StreamWriter(Path.Combine(UpdateUtilsMain._flagFilesDir, fileName)))
			{
				if (content != null)
				{
					streamWriter.WriteLine(content);
				}
			}
		}
		private static void TouchFlagFile(string fileName, bool succeeded)
		{
			UpdateUtilsMain.TouchFlagFile(fileName, succeeded, null);
		}
	}
}
