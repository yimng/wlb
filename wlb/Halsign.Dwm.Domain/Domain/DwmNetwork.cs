using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmNetwork : DwmBase
	{
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static object _uuidCacheLock = new object();
		private string _bridge;
		public string Bridge
		{
			get
			{
				return this._bridge;
			}
			set
			{
				this._bridge = value;
			}
		}
		public DwmNetwork(string uuid, string name, string poolUuid) : base(uuid, name)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmNetwork.UuidToId(uuid, base.PoolId);
				return;
			}
			throw new DwmException("The uuid of the Network must be specified.", DwmErrorCode.InvalidParameter, null);
		}
		public DwmNetwork(string uuid, string name, int poolId) : base(uuid, name)
		{
			base.PoolId = poolId;
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmNetwork.UuidToId(uuid, base.PoolId);
				return;
			}
			throw new DwmException("The uuid or name of the Network must be specified.", DwmErrorCode.InvalidParameter, null);
		}
		internal static void RefreshCache()
		{
			object uuidCacheLock = DwmNetwork._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				DwmNetwork._uuidCache.Clear();
			}
			finally
			{
				Monitor.Exit(uuidCacheLock);
			}
		}
		internal static int UuidToId(string uuid, int poolId)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(uuid))
			{
				string key = Localization.Format("{0}|{1}", uuid, poolId);
				if (!DwmNetwork._uuidCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from hv_network where uuid='{0}' and poolid={1}", uuid.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object uuidCacheLock = DwmNetwork._uuidCacheLock;
							Monitor.Enter(uuidCacheLock);
							try
							{
								if (!DwmNetwork._uuidCache.ContainsKey(key))
								{
									DwmNetwork._uuidCache.Add(key, num);
								}
							}
							finally
							{
								Monitor.Exit(uuidCacheLock);
							}
						}
					}
				}
			}
			return num;
		}
		internal void Save(DBAccess db)
		{
			if (db != null)
			{
				try
				{
					string sqlStatement = "add_update_network";
					base.Id = db.ExecuteScalarInt(sqlStatement, new StoredProcParamCollection
					{
						new StoredProcParam("@uuid", base.Uuid),
						new StoredProcParam("@name", base.Name),
						new StoredProcParam("@pool_id", base.PoolId),
						new StoredProcParam("@description", base.Description),
						new StoredProcParam("@bridge", string.IsNullOrEmpty(this._bridge) ? null : this._bridge),
						new StoredProcParam("@tstamp", DateTime.UtcNow)
					});
				}
				catch (Exception ex)
				{
					Logger.Trace("Caught exception saving Network {0} uuid={1}", new object[]
					{
						base.Name,
						base.Uuid
					});
					Logger.LogException(ex);
				}
				return;
			}
			throw new DwmException("Cannot pass a null DBAccess reference to a Save method", DwmErrorCode.NullReference, null);
		}
	}
}
