using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
namespace Halsign.DWM.Framework
{
	public sealed class Localization
	{
		private Localization()
		{
		}
		public static int Compare(string s1, string s2, bool ignoreCase)
		{
			return string.Compare(s1, s2, ignoreCase, CultureInfo.InvariantCulture);
		}
		public static string Format(string fmt, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, fmt, args);
		}
		public static string Format(string fmt, object arg0)
		{
			return Localization.Format(fmt, new object[]
			{
				arg0
			});
		}
		public static string Format(string fmt, object arg0, object arg1)
		{
			return Localization.Format(fmt, new object[]
			{
				arg0,
				arg1
			});
		}
		public static int Parse(string s)
		{
			return int.Parse(s, CultureInfo.InvariantCulture);
		}
		public static int TryParse(string s)
		{
			int result = 0;
			int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
			return result;
		}
		public static DateTime ParseExactDateTime(string s, string format)
		{
			DateTime result;
			DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
			return result;
		}
		public static DateTime ParseDateTime(string s)
		{
			DateTime result;
			DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
			return result;
		}
		public static bool StringEndsWith(string s1, string s2, bool ignoreCase)
		{
			return s1.EndsWith(s2, ignoreCase, CultureInfo.InvariantCulture);
		}
		public static bool StringStartsWith(string s1, string s2, bool ignoreCase)
		{
			return s1.StartsWith(s2, ignoreCase, CultureInfo.InvariantCulture);
		}
		public static ulong ParseUInt64(string s)
		{
			return ulong.Parse(s, CultureInfo.InvariantCulture);
		}
		public static string DateTimeToSqlString(DateTime d)
		{
			return d.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture);
		}
		public static string DateTimeToSqlTimeString(DateTime d)
		{
			return d.ToString("HH:mm:ss:fff", CultureInfo.InvariantCulture);
		}
		public static bool AreListsEqual(List<int> list1, List<int> list2)
		{
			bool result = false;
			if (list1.Count == list2.Count)
			{
				for (int i = 0; i < list1.Count; i++)
				{
					if (list1[i] != list2[i])
					{
						break;
					}
					result = true;
				}
			}
			return result;
		}
		public static string ListToString(List<int> list)
		{
			if (list != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < list.Count; i++)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						(i != 0) ? "," : string.Empty,
						list[i]
					});
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}
		public static string ToUtf8(string unicodeString)
		{
			Encoding aSCII = Encoding.ASCII;
			Encoding uTF = Encoding.UTF8;
			byte[] bytes = Encoding.Unicode.GetBytes(unicodeString.ToCharArray());
			byte[] array = new byte[bytes.Length];
			int num = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				if (bytes[i] != 0)
				{
					array[num++] = bytes[i];
				}
			}
			byte[] array2 = new byte[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = array[j];
			}
			char[] chars = uTF.GetChars(array2);
			return new string(chars);
		}
		public static void GetIntervals(DateTime time, out int HourInterval, out int FiveMinuteInterval, out int DayOfYear, out int Year)
		{
			HourInterval = time.Hour;
			FiveMinuteInterval = (time.Hour * 60 + time.Minute) / 5;
			DayOfYear = time.DayOfYear;
			Year = time.Year;
		}
	}
}
