using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace UpdateUtils
{
	internal class ConfFile
	{
		private const string KEY_VAL_DELIM = "=";
		public string FileName
		{
			get;
			private set;
		}
		public ConfFormat Format
		{
			get;
			private set;
		}
		public string FileContent
		{
			get;
			private set;
		}
		public Dictionary<string, string> NameValuePairs
		{
			get;
			private set;
		}
		public ConfFile(string fileName)
		{
			this.FileName = fileName;
			using (StreamReader streamReader = new StreamReader(this.FileName))
			{
				this.FileContent = streamReader.ReadToEnd().Trim();
				if (this.FileContent.StartsWith("<"))
				{
					this.Format = ConfFormat.Xml;
				}
				else
				{
					this.Format = ConfFormat.Ini;
				}
			}
		}
		public void SetValue(string name, string value)
		{
			if (this.Format == ConfFormat.Xml)
			{
				throw new NotImplementedException("Setting value on Xml conf file is not implemented.");
			}
			if (this.Format == ConfFormat.Ini)
			{
				this.FileContent = Regex.Replace(this.FileContent, "^" + name + "\\s*=\\s*?.*", string.Format("{0} = {1}", name, value), RegexOptions.Multiline);
				this.NameValuePairs[name] = value;
			}
		}
		public void Save()
		{
			using (StreamWriter streamWriter = new StreamWriter(this.FileName))
			{
				streamWriter.WriteLine(this.FileContent);
			}
		}
		public Dictionary<string, string> ParseAndGetNameValuePairs()
		{
			this.NameValuePairs = new Dictionary<string, string>();
			using (StreamReader streamReader = new StreamReader(this.FileName))
			{
				if (this.Format == ConfFormat.Xml)
				{
					Regex regex = new Regex("<(?<tag>\\w*)>(?<text>.*)</\\k<tag>>");
					MatchCollection matchCollection = regex.Matches(this.FileContent);
					IEnumerator enumerator = matchCollection.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Match match = (Match)enumerator.Current;
							GroupCollection groups = match.Groups;
							string value = groups["tag"].Value;
							string value2 = groups["text"].Value;
							if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2) && !this.NameValuePairs.ContainsKey(value))
							{
								this.NameValuePairs.Add(value, value2);
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
				else
				{
					if (this.Format == ConfFormat.Ini)
					{
						while (!streamReader.EndOfStream)
						{
							string text = streamReader.ReadLine().Trim();
							if (!string.IsNullOrEmpty(text) && !text.TrimStart(new char[0]).StartsWith("#"))
							{
								if (text.Contains("="))
								{
									string key = ConfFile.ExtractKey(text);
									string value3 = ConfFile.ExtractValue(text);
									if (!this.NameValuePairs.ContainsKey(key))
									{
										this.NameValuePairs.Add(key, value3);
									}
								}
							}
						}
					}
				}
			}
			return this.NameValuePairs;
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
	}
}
