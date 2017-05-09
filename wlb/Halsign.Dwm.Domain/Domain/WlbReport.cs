using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
namespace Halsign.DWM.Domain
{
	public class WlbReport
	{
		private int _reportId;
		private string _reportFile;
		private string _storedProcedureName;
		private string _physicalPath;
		private string _reportRdlc;
		private OrderedDictionary _queryParameters;
		private List<string> _reportParameters;
		private List<string> _labels;
		private Dictionary<string, string> _localizedNames;
		private string _reportNameLabel;
		private string _reportVersion;
		public int ReportId
		{
			get
			{
				return this._reportId;
			}
			internal set
			{
				this._reportId = value;
			}
		}
		public string ReportFile
		{
			get
			{
				return this._reportFile;
			}
			set
			{
				this._reportFile = value;
			}
		}
		public string StoredProcedureName
		{
			get
			{
				return this._storedProcedureName;
			}
			set
			{
				this._storedProcedureName = value;
			}
		}
		public string PhysicalPath
		{
			get
			{
				return this._physicalPath;
			}
			set
			{
				this._physicalPath = value;
			}
		}
		public string ReportRdlc
		{
			get
			{
				return this._reportRdlc;
			}
			set
			{
				this._reportRdlc = value;
			}
		}
		public OrderedDictionary QueryParameters
		{
			get
			{
				return this._queryParameters;
			}
			set
			{
				this._queryParameters = value;
			}
		}
		public List<string> ReportParameters
		{
			get
			{
				return this._reportParameters;
			}
			set
			{
				this._reportParameters = value;
			}
		}
		public List<string> Labels
		{
			get
			{
				return this._labels;
			}
			set
			{
				this._labels = value;
			}
		}
		public Dictionary<string, string> LocalizedNames
		{
			get
			{
				return this._localizedNames;
			}
			set
			{
				this._localizedNames = value;
			}
		}
		public string ReportNameLabel
		{
			get
			{
				return this._reportNameLabel;
			}
			set
			{
				this._reportNameLabel = value;
			}
		}
		public string ReportVersion
		{
			get
			{
				return this._reportVersion;
			}
			set
			{
				this._reportVersion = value;
			}
		}
		public WlbReport()
		{
			this._localizedNames = new Dictionary<string, string>();
			this._labels = new List<string>();
			this._queryParameters = new OrderedDictionary();
			this._reportParameters = new List<string>();
		}
		public static string DictionaryToString(OrderedDictionary dictionary)
		{
			List<string> list = new List<string>();
			IEnumerator enumerator = dictionary.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Current;
					list.Add(string.Format("{0}={1}", text, HttpUtility.UrlEncode((string)dictionary[text])));
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
			return string.Join("&", list.ToArray());
		}
		public static OrderedDictionary StringToDictionary(string queryString)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			string[] array = queryString.Split("&".ToCharArray());
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text.Contains("="))
				{
					string[] array3 = text.Split("=".ToCharArray(), 2);
					orderedDictionary.Add(array3[0], array3[1]);
				}
			}
			return orderedDictionary;
		}
		public static string ListToString(List<string> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string current in list)
			{
				if (!string.IsNullOrEmpty(current))
				{
					stringBuilder.Append(current + ",");
				}
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
		}
	}
}
