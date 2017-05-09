using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
namespace Halsign.DWM.Communication
{
	public abstract class CommunicationBase
	{
		private const string LocalAdminSID = "S-1-5-32-544";
		private const string NetworkServiceSID = "S-1-5-20";
		private List<string> _dataCollectors;
		private static Dictionary<string, List<string>> _groupMembers = new Dictionary<string, List<string>>();
		private static object _groupMembersLock = new object();
		protected virtual bool AuthenticateCaller(string username, string password)
		{
			return true;
		}
		private List<string> GetDataCollectors()
		{
			if (this._dataCollectors == null)
			{
				this.ReloadDataCollectors();
			}
			return this._dataCollectors;
		}
		private void ReloadDataCollectors()
		{
			this._dataCollectors = new List<string>();
			string sqlStatement = "select domain_name, hostname from collection_host";
			char[] separator = new char[]
			{
				'.'
			};
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement))
				{
					while (dataReader.Read())
					{
						string @string = DBAccess.GetString(dataReader, 0);
						string string2 = DBAccess.GetString(dataReader, 1);
						string[] array = @string.Split(separator);
						if (array != null)
						{
							if (array.Length > 1)
							{
								this._dataCollectors.Add(string.Format("{0}\\{1}$", array[0], string2));
							}
							this._dataCollectors.Add(string.Format("{0}\\{1}$", @string, string2));
						}
						else
						{
							this._dataCollectors.Add(string.Format("{0}$", string2));
						}
					}
				}
			}
		}
		protected static void Trace(string fmt, params object[] args)
		{
			if (Configuration.GetValueAsBool(ConfigItems.WlbWebServiceTrace))
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
