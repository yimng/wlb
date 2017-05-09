using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmVif : DwmBase
	{
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static object _uuidCacheLock = new object();
		private int _networkId;
		private string _macAddress;
		private string _deviceNumber;
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
		public string DeviceNumber
		{
			get
			{
				return this._deviceNumber;
			}
			set
			{
				this._deviceNumber = value;
			}
		}
		public int NetworkId
		{
			get
			{
				return this._networkId;
			}
		}
		public DwmVif(string uuid, int networkId, int poolId) : base(uuid, null)
		{
			base.PoolId = poolId;
			this._networkId = networkId;
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmVif.UuidToId(uuid, base.PoolId);
				return;
			}
			throw new DwmException("The uuid of the VIF must be specified.", DwmErrorCode.InvalidParameter, null);
		}
		public DwmVif(string uuid, string networkUuid, string poolUuid) : base(uuid, null)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			this._networkId = DwmNetwork.UuidToId(networkUuid, base.PoolId);
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmVif.UuidToId(uuid, base.PoolId);
				if (base.Id == 0)
				{
					base.Id = -1;
				}
				return;
			}
			throw new DwmException("The uuid of the VIF must be specified.", DwmErrorCode.InvalidParameter, null);
		}
		internal DwmVif(int vifId) : base(vifId)
		{
		}
		internal static void RefreshCache()
		{
			object uuidCacheLock = DwmVif._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				DwmVif._uuidCache.Clear();
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
				if (!DwmVif._uuidCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from vif where uuid='{0}' and poolid={1}", uuid.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object uuidCacheLock = DwmVif._uuidCacheLock;
							Monitor.Enter(uuidCacheLock);
							try
							{
								if (!DwmVif._uuidCache.ContainsKey(key))
								{
									DwmVif._uuidCache.Add(key, num);
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
					string sqlStatement = "add_update_vif";
					StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
					storedProcParamCollection.Add(new StoredProcParam("@uuid", base.Uuid));
					storedProcParamCollection.Add(new StoredProcParam("@pool_id", base.PoolId));
					if (this._networkId > 0)
					{
						storedProcParamCollection.Add(new StoredProcParam("@network_id", this._networkId));
					}
					else
					{
						storedProcParamCollection.Add(new StoredProcParam("@network_id", null));
					}
					storedProcParamCollection.Add(new StoredProcParam("@mac_address", string.IsNullOrEmpty(this._macAddress) ? null : this._macAddress));
					storedProcParamCollection.Add(new StoredProcParam("@device_number", string.IsNullOrEmpty(this._deviceNumber) ? null : this._deviceNumber));
					storedProcParamCollection.Add(new StoredProcParam("@tstamp", DateTime.UtcNow));
					base.Id = db.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
				}
				catch (Exception ex)
				{
					Logger.Trace("Caught exception saving VIF uuid={0}", new object[]
					{
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
