using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmPbd : DwmBase
	{
		private bool _isAttached;
		private int _storageId = -1;
		private string _storageUuid;
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static object _uuidCacheLock = new object();
		public bool CurrentlyAttached
		{
			get
			{
				return this._isAttached;
			}
			set
			{
				this._isAttached = value;
			}
		}
		public int StorageId
		{
			get
			{
				return this._storageId;
			}
			set
			{
				this._storageId = value;
			}
		}
		public DwmPbd(string uuid, string name, int storageId, int poolId) : base(uuid, name)
		{
			base.PoolId = poolId;
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmPbd.UuidToId(uuid, base.PoolId);
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new DwmException("The uuid or name of the PBD must be specified.", DwmErrorCode.InvalidParameter, null);
				}
				base.Id = DwmPbd.NameToId(name, base.PoolId);
			}
			this._storageId = storageId;
		}
		public DwmPbd(string uuid, string name, string storageUuid, string poolUuid) : base(uuid, name)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmPbd.UuidToId(uuid, base.PoolId);
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new DwmException("The uuid or name of the PBD must be specified.", DwmErrorCode.InvalidParameter, null);
				}
				base.Id = DwmPbd.NameToId(name, base.PoolId);
			}
			this._storageUuid = storageUuid;
		}
		internal static void RefreshCache()
		{
			object uuidCacheLock = DwmPbd._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				DwmPbd._uuidCache.Clear();
			}
			finally
			{
				Monitor.Exit(uuidCacheLock);
			}
		}
		protected static int UuidToId(string uuid, int poolId)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(uuid))
			{
				string key = Localization.Format("{0}|{1}", uuid, poolId);
				if (!DwmPbd._uuidCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from hv_pbd where uuid='{0}' and poolid={1}", uuid.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object uuidCacheLock = DwmPbd._uuidCacheLock;
							Monitor.Enter(uuidCacheLock);
							try
							{
								if (!DwmPbd._uuidCache.ContainsKey(key))
								{
									DwmPbd._uuidCache.Add(key, num);
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
		protected static int NameToId(string name, int poolId)
        {
            if (string.IsNullOrEmpty(name))
                return 0;
            using (DBAccess dbAccess = new DBAccess())
                return dbAccess.ExecuteScalarInt(Localization.Format("select id from hv_pbd where name='{0}' and poolid={1}", (object)name.Replace("'", "''"), (object)poolId));
        }
		public void Save(DBAccess db)
		{
			if (db != null)
			{
				try
				{
					string sqlStatement = "add_update_pbd";
					StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
					if (base.Id > 0)
					{
						storedProcParamCollection.Add(new StoredProcParam("@id", base.Id));
					}
					else
					{
						storedProcParamCollection.Add(new StoredProcParam("@id", null));
					}
					storedProcParamCollection.Add(new StoredProcParam("@uuid", base.Uuid));
					storedProcParamCollection.Add(new StoredProcParam("@name", base.Name));
					storedProcParamCollection.Add(new StoredProcParam("@pool_id", base.PoolId));
					storedProcParamCollection.Add(new StoredProcParam("@description", base.Description));
					if (this._storageId > 0)
					{
						storedProcParamCollection.Add(new StoredProcParam("@sr_id", this._storageId));
					}
					else
					{
						storedProcParamCollection.Add(new StoredProcParam("@sr_id", null));
					}
					storedProcParamCollection.Add(new StoredProcParam("@sr_uuid", string.IsNullOrEmpty(this._storageUuid) ? null : this._storageUuid));
					storedProcParamCollection.Add(new StoredProcParam("@tstamp", DateTime.UtcNow));
					base.Id = db.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
				}
				catch (Exception ex)
				{
					Logger.Trace("Caught exception saving PBD {0} uuid={1}", new object[]
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
