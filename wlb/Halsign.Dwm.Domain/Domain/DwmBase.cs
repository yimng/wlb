using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Halsign.DWM.Domain
{
	public abstract class DwmBase
	{
		internal const int FLAGS_PLACED_VM = 1;
		internal const int FLAGS_SHOULD_POWER_ON = 2;
		internal const int FLAGS_SHOULD_POWER_OFF = 4;
		internal const int FLAGS_HOST_HAS_STATIC_MAX = 8;
		internal const int FLAGS_HOST_HAS_DYNAMIC_MAX = 16;
		internal const int FLAGS_HOST_HAS_POTENTIAL_MEM = 32;
		internal const int FLAGS_HOST_HAS_CPU_COUNT = 22;
		private int _id;
		private string _name = string.Empty;
		private string _uuid = string.Empty;
		private string _description = string.Empty;
		private int _poolId;
		private int _flags;
		private DwmStatus _status;
		private DwmStatus _lastResult;
		private DateTime _lastResultTime;
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public string Uuid
		{
			get
			{
				return this._uuid;
			}
			internal set
			{
				this._uuid = value;
			}
		}
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
		public int PoolId
		{
			get
			{
				return this._poolId;
			}
			set
			{
				if (value != 0)
				{
					this._poolId = value;
					return;
				}
				throw new DwmException("The ID of the pool to which the item belongs is invalid.", DwmErrorCode.InvalidParameter, null);
			}
		}
		public DwmStatus Status
		{
			get
			{
				return this._status;
			}
			internal set
			{
				this._status = value;
			}
		}
		public DwmStatus LastResult
		{
			get
			{
				return this._lastResult;
			}
			internal set
			{
				this._lastResult = value;
			}
		}
		public DateTime LastResultTime
		{
			get
			{
				return this._lastResultTime;
			}
			internal set
			{
				this._lastResultTime = value;
			}
		}
		internal int Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}
		protected DwmBase(string uuid, string name)
		{
			DwmBase.TimeBomb();
			this._uuid = uuid;
			this._name = name;
			this._status = DwmStatus.None;
			this._lastResult = DwmStatus.None;
		}
		protected DwmBase(int id)
		{
			DwmBase.TimeBomb();
			this._id = id;
		}
		internal static void TimeBomb()
		{
		}
		internal static int PoolUuidToId(string poolUuid)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(poolUuid))
			{
				num = DwmPoolBase.UuidToId(poolUuid);
			}
			if (num == 0)
			{
				throw new DwmException("The uuid of the pool to which the item belongs must be specified", DwmErrorCode.InvalidParameter, null);
			}
			return num;
		}
		protected void SetOtherConfig(string sql, string idParmName, string name, string value)
		{
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam(idParmName, this.Id));
			storedProcParamCollection.Add(new StoredProcParam("@name", name));
			storedProcParamCollection.Add(new StoredProcParam("@value", value));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sql, storedProcParamCollection);
			}
		}
		protected void SetOtherConfig(DBAccess dbAccess, string sql, string idParmName, string name, string value)
		{
			if (dbAccess != null)
			{
				dbAccess.ExecuteNonQuery(sql, new StoredProcParamCollection
				{
					new StoredProcParam(idParmName, this.Id),
					new StoredProcParam("@name", name),
					new StoredProcParam("@value", value)
				});
			}
		}
		protected void SetOtherConfig(string sql, string idParmName, Dictionary<string, string> config)
		{
			if (config != null && config.Count > 0)
			{
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				using (DBAccess dBAccess = new DBAccess())
				{
					foreach (KeyValuePair<string, string> current in config)
					{
						storedProcParamCollection.Clear();
						storedProcParamCollection.Add(new StoredProcParam(idParmName, this.Id));
						storedProcParamCollection.Add(new StoredProcParam("@name", current.Key));
						storedProcParamCollection.Add(new StoredProcParam("@value", current.Value));
						dBAccess.ExecuteNonQuery(sql, storedProcParamCollection);
					}
				}
			}
		}
		protected string GetOtherConfigItem(string sql, string idParmName, string itemName)
		{
			string result = string.Empty;
			try
			{
				if (!string.IsNullOrEmpty(itemName))
				{
					StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
					storedProcParamCollection.Add(new StoredProcParam(idParmName, this.Id));
					storedProcParamCollection.Add(new StoredProcParam("@item_name", itemName));
					using (DBAccess dBAccess = new DBAccess())
					{
						result = dBAccess.ExecuteScalarString(sql, storedProcParamCollection);
					}
				}
			}
			catch
			{
			}
			return result;
		}
		protected Dictionary<string, string> GetOtherConfig(string sql, string idParmName)
		{
			Dictionary<string, string> dictionary = null;
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam(idParmName, this.Id));
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sql, storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						dictionary = new Dictionary<string, string>();
						do
						{
							string @string = DBAccess.GetString(dataReader, "name");
							string string2 = DBAccess.GetString(dataReader, "value");
							try
							{
								dictionary.Add(@string, string2);
							}
							catch (Exception ex)
							{
								Logger.Trace("Exception adding '{0}' to other config from {1}", new object[]
								{
									@string,
									sql
								});
								Logger.LogException(ex);
							}
						}
						while (dataReader.Read());
					}
				}
			}
			return dictionary;
		}
		internal static T SafeGetItem<T>(ref T item) where T : new()
		{
			if (item == null)
			{
				item = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
			}
			return item;
		}
		internal static void WriteData(DBAccess db, StringBuilder sqlStatement)
		{
			if (sqlStatement != null && sqlStatement.Length > 0)
			{
				db.ExecuteSql(sqlStatement.ToString());
			}
		}
		internal static void WriteData(DBAccess db, string sqlStatement)
		{
			if (sqlStatement != null && sqlStatement.Length > 0)
			{
				db.ExecuteSql(sqlStatement);
			}
		}
		protected static void TraceCompaction(string fmt, params object[] args)
		{
			bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.DataCompactionTrace);
			if (valueAsBool)
			{
				Logger.Trace(fmt, args);
			}
		}
	}
}
