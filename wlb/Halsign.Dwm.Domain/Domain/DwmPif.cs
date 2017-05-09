using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmPif : DwmBase
	{
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static object _uuidCacheLock = new object();
		private string _macAddress;
		private bool _isManagementInterface;
		private bool _isPhysical;
		private int _networkId;
		public string MacAddress
		{
			get
			{
				return this._macAddress;
			}
			set
			{
				this._macAddress = value;
			}
		}
		public bool IsManagementInterface
		{
			get
			{
				return this._isManagementInterface;
			}
			set
			{
				this._isManagementInterface = value;
			}
		}
		public bool IsPhysical
		{
			get
			{
				return this._isPhysical;
			}
			set
			{
				this._isPhysical = value;
			}
		}
		public int NetworkId
		{
			get
			{
				return this._networkId;
			}
		}
		public DwmPif(string uuid, string name, string networkUuid, string poolUuid) : base(uuid, name)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			this._networkId = DwmNetwork.UuidToId(networkUuid, base.PoolId);
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmPif.UuidToId(uuid, base.PoolId);
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new DwmException("The uuid or name of the PIF must be specified.", DwmErrorCode.InvalidParameter, null);
				}
				base.Id = DwmPif.NameToId(name, base.PoolId);
			}
		}
		public DwmPif(string uuid, string name, int networkId, int poolId) : base(uuid, name)
		{
			base.PoolId = poolId;
			this._networkId = networkId;
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmPif.UuidToId(uuid, base.PoolId);
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new DwmException("The uuid or name of the PIF must be specified.", DwmErrorCode.InvalidParameter, null);
				}
				base.Id = DwmPif.NameToId(name, base.PoolId);
			}
		}
		internal static void RefreshCache()
		{
			object uuidCacheLock = DwmPif._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				DwmPif._uuidCache.Clear();
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
				if (!DwmPif._uuidCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from hv_pif where uuid='{0}' and poolid={1}", uuid.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object uuidCacheLock = DwmPif._uuidCacheLock;
							Monitor.Enter(uuidCacheLock);
							try
							{
								if (!DwmPif._uuidCache.ContainsKey(key))
								{
									DwmPif._uuidCache.Add(key, num);
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
                return dbAccess.ExecuteScalarInt(Localization.Format("select id from hv_pif where name='{0}' and poolid={1}", (object)name.Replace("'", "''"), (object)poolId));
		}
		internal void Save(DBAccess db)
		{
			if (db != null)
			{
				try
				{
					string sqlStatement = "add_update_pif";
					StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
					storedProcParamCollection.Add(new StoredProcParam(":uuid", base.Uuid));
					storedProcParamCollection.Add(new StoredProcParam(":name", base.Name));
					storedProcParamCollection.Add(new StoredProcParam(":pool_id", base.PoolId));
					if (this._networkId > 0)
					{
						storedProcParamCollection.Add(new StoredProcParam(":network_id", this._networkId));
					}
					else
					{
						storedProcParamCollection.Add(new StoredProcParam(":network_id", null));
					}
					storedProcParamCollection.Add(new StoredProcParam(":description", base.Description));
					storedProcParamCollection.Add(new StoredProcParam(":mac_address", string.IsNullOrEmpty(this._macAddress) ? null : this._macAddress));
					storedProcParamCollection.Add(new StoredProcParam(":is_management_interface", this.IsManagementInterface));
					storedProcParamCollection.Add(new StoredProcParam(":is_physical", this.IsPhysical));
					storedProcParamCollection.Add(new StoredProcParam(":tstamp", DateTime.UtcNow));
					base.Id = db.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
				}
				catch (Exception ex)
				{
					Logger.Trace("Caught exception saving PIF {0} uuid={1}", new object[]
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
