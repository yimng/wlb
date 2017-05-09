using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmVbd : DwmBase
	{
		private int _storageId = -1;
		private string _storageUuid;
		private long _size;
		private long _used;
		private string _deviceName;
		private int _diskType;
		private int _diskNumber;
		private bool _isNetworkStorage;
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static object _uuidCacheLock = new object();
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
		public long Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}
		public long Used
		{
			get
			{
				return this._used;
			}
			set
			{
				this._used = value;
			}
		}
		public int DiskType
		{
			get
			{
				return this._diskType;
			}
			set
			{
				this._diskType = value;
			}
		}
		public string DeviceName
		{
			get
			{
				return this._deviceName;
			}
			set
			{
				this._deviceName = value;
			}
		}
		public int DiskNumber
		{
			get
			{
				return this._diskNumber;
			}
			set
			{
				this._diskNumber = value;
			}
		}
		public bool IsNetworkStorage
		{
			get
			{
				return this._isNetworkStorage;
			}
			set
			{
				this._isNetworkStorage = value;
			}
		}
		public DwmVbd(string uuid, string name, int storageId, int poolId) : base(uuid, name)
		{
			base.PoolId = poolId;
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmVbd.UuidToId(uuid, base.PoolId);
				this._storageId = storageId;
				return;
			}
			throw new DwmException("The uuid or name of the VBD must be specified.", DwmErrorCode.InvalidParameter, null);
		}
		public DwmVbd(string uuid, string name, string storageUuid, string poolUuid) : base(uuid, name)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmVbd.UuidToId(uuid, base.PoolId);
				if (base.Id == 0)
				{
					base.Id = -1;
				}
				this._storageUuid = storageUuid;
				return;
			}
			throw new DwmException("The uuid or name of the VBD must be specified.", DwmErrorCode.InvalidParameter, null);
		}
		internal static void RefreshCache()
		{
			object uuidCacheLock = DwmVbd._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				DwmVbd._uuidCache.Clear();
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
				if (!DwmVbd._uuidCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from vbd where uuid='{0}' and poolid={1}", uuid.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object uuidCacheLock = DwmVbd._uuidCacheLock;
							Monitor.Enter(uuidCacheLock);
							try
							{
								if (!DwmVbd._uuidCache.ContainsKey(key))
								{
									DwmVbd._uuidCache.Add(key, num);
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
					string sqlStatement = "add_update_vbd";
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
					if (this._storageId > 0)
					{
						storedProcParamCollection.Add(new StoredProcParam("@sr_id", this._storageId));
					}
					else
					{
						storedProcParamCollection.Add(new StoredProcParam("@sr_id", null));
					}
					storedProcParamCollection.Add(new StoredProcParam("@sr_uuid", string.IsNullOrEmpty(this._storageUuid) ? null : this._storageUuid));
					storedProcParamCollection.Add(new StoredProcParam("@size", this._size));
					storedProcParamCollection.Add(new StoredProcParam("@used", this._used));
					storedProcParamCollection.Add(new StoredProcParam("@device_name", string.IsNullOrEmpty(this._deviceName) ? null : this._deviceName));
					storedProcParamCollection.Add(new StoredProcParam("@disk_type", this._diskType));
					storedProcParamCollection.Add(new StoredProcParam("@disk_number", this._diskNumber));
					base.Id = db.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
				}
				catch (Exception ex)
				{
					Logger.Trace("Caught exception saving VBD {0} uuid={1}", new object[]
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
