using Halsign.DWM.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Xml;
namespace ReportImport
{
	internal class Program
	{
		private static string _reportFolderPath;
		private static string _reportVersion;
		private static WlbReports _reports = new WlbReports();
		private static bool _verbose = false;
		private static void Main(string[] args)
		{
			bool flag = false;
			if (args.Length == 0)
			{
				flag = true;
			}
			else
			{
				int i = 0;
				while (i < args.Length)
				{
					string text = args[i].ToLower();
					if (text == null)
					{
						goto IL_D0;
					}
                    Dictionary<string, int> dic = null;
					if (dic == null)
					{
						dic = new Dictionary<string, int>(4)
						{

							{
								"-p",
								0
							},

							{
								"--version",
								1
							},

							{
								"-h",
								2
							},

							{
								"-v",
								3
							}
						};
					}
					int num;
					if (!dic.TryGetValue(text, out num))
					{
						goto IL_D0;
					}
					switch (num)
					{
					case 0:
						Program._reportFolderPath = args[++i];
						break;
					case 1:
						Program._reportVersion = args[++i];
						break;
					case 2:
						flag = true;
						break;
					case 3:
						Program._verbose = true;
						break;
					default:
						goto IL_D0;
					}
					IL_D7:
					i++;
					continue;
					IL_D0:
					flag = true;
					goto IL_D7;
				}
			}
			if (flag || string.IsNullOrEmpty(Program._reportFolderPath))
			{
				Program.ShowHelp();
			}
			else
			{
				Program.ImportReports();
			}
		}
		private static void ShowHelp()
		{
			Console.WriteLine("\nThis tool is used to import WLB reports from Reporting Services RDLC files\ninto the WLB database.\n\nUsage:\n\t-p [Report Folder Path]: Required. Specifies the physical path to the report RDLC files\n\t                         Path can be relative or absolute.\n\t-v: Verbose mode\n\t-h: Display this help\n\n");
		}
		private static void ImportReports()
		{
			Program.CleanReportFolderPath();
			if (Program._reportVersion == null || Program._reportVersion.Length == 0)
			{
				Console.WriteLine("Please give reportVersion(--version option)!");
				Environment.Exit(-1);
			}
			if (Program._verbose)
			{
				Console.WriteLine("Report path: {0}", Program._reportFolderPath);
			}
			string[] files = Directory.GetFiles(Program._reportFolderPath, "*.rdlc", SearchOption.TopDirectoryOnly);
			if (Program._verbose)
			{
				Console.WriteLine("Found {0} reports for Report Version {1}.", files.Length.ToString(), Program._reportVersion);
			}
			if (files.Length > 0)
			{
				string[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					FileInfo fileInfo = new FileInfo(text);
					string text2 = fileInfo.Name.Replace(fileInfo.Extension, string.Empty).Replace(" ", "_");
					if (Program._verbose)
					{
						Console.Write("Attempting to load report {0} from {1}...", text2, text);
					}
					XmlDocument xmlDocument = null;
					try
					{
						xmlDocument = new XmlDocument();
						xmlDocument.Load(text);
						if (Program._verbose)
						{
							Console.WriteLine("succeeded.");
						}
					}
					catch (Exception ex)
					{
						if (Program._verbose)
						{
							Console.WriteLine("failed!");
						}
						Console.WriteLine("  ***{0}", ex.Message);
					}
					if (Program._verbose)
					{
						Console.Write("Parsing report {0}...", text2);
					}
					try
					{
						XmlElement documentElement = xmlDocument.DocumentElement;
						string text3 = null;
						string text4 = null;
						XmlNodeList elementsByTagName = documentElement.GetElementsByTagName("ReportParameter");
						IEnumerator enumerator = elementsByTagName.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								XmlNode xmlNode = (XmlNode)enumerator.Current;
								string value = xmlNode.Attributes["Name"].Value;
								if (value != null)
								{
                                    Dictionary<string, int> dic = null;
									if (dic == null)
									{
										dic = new Dictionary<string, int>(2)
										{

											{
												"SPROC_NAME",
												0
											},

											{
												"REPORT_NAME_LABEL",
												1
											}
										};
									}
									int num;
									if (dic.TryGetValue(value, out num))
									{
										if (num != 0)
										{
											if (num == 1)
											{
												text4 = Program.GetReportNameLabel((XmlElement)xmlNode);
											}
										}
										else
										{
											text3 = Program.GetStoredProcedureName((XmlElement)xmlNode);
										}
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
						if (string.IsNullOrEmpty(text3))
						{
							throw new Exception("Report Stored Procedure was not specified");
						}
						if (text4 == null)
						{
							throw new Exception("Localized Report Name(s) was not specified");
						}
						WlbReport wlbReport = new WlbReport();
						wlbReport.ReportFile = text2;
						wlbReport.StoredProcedureName = text3;
						wlbReport.PhysicalPath = text;
						wlbReport.QueryParameters = Program.GetQueryParameters(documentElement, "QueryParameter");
						wlbReport.ReportParameters = Program.GetParameters(documentElement, "ReportParameter");
						wlbReport.ReportRdlc = xmlDocument.InnerXml;
						wlbReport.ReportNameLabel = text4;
						wlbReport.Labels = Program.GetReportLabels(documentElement);
						wlbReport.ReportVersion = Program._reportVersion;
						Program._reports.Add(text2, wlbReport);
						if (Program._verbose)
						{
							Console.WriteLine("suceeded.");
						}
					}
					catch (Exception ex2)
					{
						if (Program._verbose)
						{
							Console.WriteLine("failed!");
						}
						Console.WriteLine(" ***{0}", ex2.Message);
					}
				}
				try
				{
					if (Program._verbose)
					{
						Console.Write("Saving reports to database...");
					}
					Program._reports.Save();
					if (Program._verbose)
					{
						Console.WriteLine("succeeded.");
					}
				}
				catch (Exception ex3)
				{
					if (Program._verbose)
					{
						Console.WriteLine("failed!");
					}
					Console.WriteLine(" ***{0}", ex3.Message);
				}
			}
		}
		private static void CleanReportFolderPath()
		{
			Program._reportFolderPath = Program._reportFolderPath.TrimEnd(new char[]
			{
				'"'
			});
			if (Program._reportFolderPath.EndsWith("\\"))
			{
				Program._reportFolderPath = Program._reportFolderPath.TrimEnd(new char[]
				{
					'\\'
				});
			}
			try
			{
				Program._reportFolderPath = Path.GetFullPath(Program._reportFolderPath);
			}
			catch (Exception ex)
			{
				throw new Exception("Error resolving the report path: " + ex.Message);
			}
		}
		private static List<string> GetReportLabels(XmlElement root)
		{
			List<string> list = new List<string>();
			string innerXml = root.InnerXml;
			string text = "GetLabel(\"";
			string value = "\")";
			int i = 0;
			while (i > -1)
			{
				i = innerXml.IndexOf(text, i);
				if (i > -1)
				{
					int num = innerXml.IndexOf(value, i);
					string text2 = innerXml.Substring(i + text.Length, num - i - text.Length);
					if (!string.IsNullOrEmpty(text2) && !list.Contains(text2))
					{
						list.Add(text2);
					}
					i = num;
				}
			}
			return list;
		}
		private static XmlElement GetCustomXmlElement(XmlElement root)
		{
			XmlNodeList elementsByTagName = root.GetElementsByTagName("Custom");
			if (elementsByTagName != null && elementsByTagName.Count > 1)
			{
				string innerXml = elementsByTagName[0].InnerXml;
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml("<root>" + innerXml + "</root>");
				return xmlDocument.DocumentElement;
			}
			XmlDocument xmlDocument2 = new XmlDocument();
			string text = "Function Custom() As String";
			string value = "\nEnd Function";
			string innerXml2 = root.InnerXml;
			int num = innerXml2.IndexOf(text, 0);
			if (num > -1)
			{
				int num2 = innerXml2.IndexOf(value, num);
				string text2 = innerXml2.Substring(num + text.Length, num2 - num - text.Length);
				text2 = HttpUtility.HtmlDecode(text2.Trim().Substring("Return".Length).Trim().Trim("\"".ToCharArray()));
				xmlDocument2.LoadXml(text2);
				return xmlDocument2.DocumentElement;
			}
			return null;
		}
		private static XmlElement GetCustomXmlElement(XmlElement root, string version)
		{
			XmlElement customXmlElement = Program.GetCustomXmlElement(root);
			if (customXmlElement != null)
			{
				XmlNodeList elementsByTagName = customXmlElement.GetElementsByTagName("Version");
				IEnumerator enumerator = elementsByTagName.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						XmlNode xmlNode = (XmlNode)enumerator.Current;
						if (xmlNode.Attributes["value"].Value == version)
						{
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.LoadXml("<root>" + xmlNode.InnerXml + "</root>");
							return xmlDocument.DocumentElement;
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
			return null;
		}
		private static XmlElement GetCustomXmlElement(XmlElement root, string version, string tagName)
		{
			XmlElement customXmlElement = Program.GetCustomXmlElement(root, version);
			if (customXmlElement != null)
			{
				XmlNodeList elementsByTagName = customXmlElement.GetElementsByTagName(tagName);
				if (elementsByTagName != null && elementsByTagName.Count > 0)
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml("<root>" + elementsByTagName[0].InnerXml + "</root>");
					return xmlDocument.DocumentElement;
				}
			}
			return null;
		}
		private static OrderedDictionary GetQueryParameters(XmlElement root, string ParamNodeName)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			XmlNodeList elementsByTagName = root.GetElementsByTagName(ParamNodeName);
			string text = string.Empty;
			XmlElement customXmlElement = Program.GetCustomXmlElement(root, "Creedence", "QueryParameters");
			IEnumerator enumerator = elementsByTagName.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					XmlNode node = (XmlNode)enumerator.Current;
					text = Program.GetNodeAttribute(node, "Name");
					XmlNodeList xmlNodeList = null;
					if (customXmlElement != null)
					{
						xmlNodeList = customXmlElement.GetElementsByTagName(text);
					}
					if (xmlNodeList != null && xmlNodeList.Count > 0)
					{
						orderedDictionary.Add(text, xmlNodeList[0].InnerText);
					}
					else
					{
						orderedDictionary.Add(text, string.Empty);
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
			return orderedDictionary;
		}
		private static List<string> GetParameters(XmlElement root, string ParamNodeName)
		{
			List<string> list = new List<string>();
			XmlNodeList elementsByTagName = root.GetElementsByTagName(ParamNodeName);
			string item = string.Empty;
			IEnumerator enumerator = elementsByTagName.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					XmlNode node = (XmlNode)enumerator.Current;
					item = Program.GetNodeAttribute(node, "Name");
					list.Add(item);
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
			return list;
		}
		private static string GetNodeAttribute(XmlNode node, string attribute)
		{
			return node.Attributes[attribute].InnerText.TrimStart(new char[]
			{
				'@'
			});
		}
		private static string GetStoredProcedureName(XmlElement xmlElement)
		{
			XmlNodeList elementsByTagName = xmlElement.GetElementsByTagName("Value");
			if (elementsByTagName.Count > 0)
			{
				return elementsByTagName[0].InnerText;
			}
			return null;
		}
		private static string GetReportNameLabel(XmlElement xmlElement)
		{
			return xmlElement.GetElementsByTagName("Value")[0].InnerText;
		}
	}
}
