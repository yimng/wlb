using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmStorageRepository : DwmBase
	{
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static object _uuidCacheLock = new object();
		private long _size;
		private long _used;
		private bool _pool_default_sr;
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
		public bool PoolDefaultSR
		{
			get
			{
				return this._pool_default_sr;
			}
			set
			{
				this._pool_default_sr = value;
			}
		}
		public DwmStorageRepository(int id) : base(id)
		{
			if (id == 0)
			{
				throw new DwmException("The ID of the storage repository must be specified", DwmErrorCode.InvalidParameter, null);
			}
		}
		public DwmStorageRepository(string uuid, string name, string poolUuid) : base(uuid, name)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			if (string.IsNullOrEmpty(uuid))
			{
				throw new DwmException("The uuid of the storage repository must be specified", DwmErrorCode.InvalidParameter, null);
			}
			base.Id = DwmStorageRepository.UuidToId(uuid, base.PoolId);
		}
		public DwmStorageRepository(string uuid, string name, int poolId) : base(uuid, name)
		{
			base.PoolId = poolId;
			if (string.IsNullOrEmpty(uuid))
			{
				throw new DwmException("The uuid of the storage repository must be specified", DwmErrorCode.InvalidParameter, null);
			}
			base.Id = DwmStorageRepository.UuidToId(uuid, base.PoolId);
		}
		public DwmStorageRepository(string uuid, string name, string poolUuid, long size, long used, bool poolDefaultSR) : base(uuid, name)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			if (string.IsNullOrEmpty(uuid))
			{
				throw new DwmException("The uuid of the storage repository must be specified", DwmErrorCode.InvalidParameter, null);
			}
			base.Id = DwmStorageRepository.UuidToId(uuid, base.PoolId);
			this.Size = size;
			this.Used = used;
			this.PoolDefaultSR = poolDefaultSR;
		}
		public DwmStorageRepository(string uuid, string name, int poolId, long size, long used, bool poolDefaultSR) : base(uuid, name)
		{
			base.PoolId = poolId;
			if (string.IsNullOrEmpty(uuid))
			{
				throw new DwmException("The uuid of the storage repository must be specified", DwmErrorCode.InvalidParameter, null);
			}
			base.Id = DwmStorageRepository.UuidToId(uuid, base.PoolId);
			this.Size = size;
			this.Used = used;
			this.PoolDefaultSR = poolDefaultSR;
		}
		protected static int UuidToId(string uuid, int poolId)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(uuid))
			{
				string key = string.Format("{0}|{1}", uuid, poolId);
				if (!DwmStorageRepository._uuidCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from storage_repository where uuid='{0}' and poolid={1}", uuid.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object uuidCacheLock = DwmStorageRepository._uuidCacheLock;
							Monitor.Enter(uuidCacheLock);
							try
							{
								if (!DwmStorageRepository._uuidCache.ContainsKey(key))
								{
									DwmStorageRepository._uuidCache.Add(key, num);
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
		public DwmStorageRepository Copy()
		{
			DwmStorageRepository dwmStorageRepository;
			if (!string.IsNullOrEmpty(base.Uuid))
			{
				dwmStorageRepository = new DwmStorageRepository(base.Uuid, base.Name, base.PoolId, this.Size, this.Used, this.PoolDefaultSR);
				dwmStorageRepository.Id = base.Id;
			}
			else
			{
				dwmStorageRepository = new DwmStorageRepository(base.Id);
				dwmStorageRepository.Name = base.Name;
				dwmStorageRepository.PoolId = base.PoolId;
				dwmStorageRepository.Size = this.Size;
				dwmStorageRepository.Used = this.Used;
				dwmStorageRepository.PoolDefaultSR = this.PoolDefaultSR;
			}
			dwmStorageRepository.Description = base.Description;
			return dwmStorageRepository;
		}
		internal void Save(DBAccess db)
		{
			if (db != null)
			{
				try
				{
					string sqlStatement = "add_update_storage_repository";
					base.Id = db.ExecuteScalarInt(sqlStatement, new StoredProcParamCollection
					{
						new StoredProcParam("@uuid", base.Uuid),
						new StoredProcParam("@name", base.Name),
						new StoredProcParam("@pool_id", base.PoolId),
						new StoredProcParam("@size", this.Size),
						new StoredProcParam("@used", this.Used),
						new StoredProcParam("@pool_default_sr", this.PoolDefaultSR)
					});
				}
				catch (Exception ex)
				{
					Logger.Trace("Caught exception saving SR {0} uuid={1}", new object[]
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
