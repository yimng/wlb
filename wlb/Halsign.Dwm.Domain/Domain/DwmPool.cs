using Halsign.DWM.Collectors;
using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmPool : DwmPoolBase
	{
		private string _userName;
		private string _password;
		private int _versionMajor = -1;
		private int _versionMinor = -1;
		private int _versionBuild = -1;
		private DwmHostCollection _listHosts;
		private DwmVmMovementCollection _recentVmMoves;
		private bool _isDeleted;
		private int _maxCpuRating = 1;
		private string _poolAuditLogGranularity;
		private static Dictionary<int, ICollectorActions> _collectorsCache;
		private static object _collectorsCacheLock = new object();
		private static object _poolSaveLock = new object();
		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this._userName = value;
			}
		}
		public string PoolAuditLogGranularity
		{
			get
			{
				return this._poolAuditLogGranularity;
			}
			set
			{
				this._poolAuditLogGranularity = value;
			}
		}
		public string Password
		{
			get
			{
				return this.DecryptPwd(this._password);
			}
			set
			{
				this._password = this.EncryptPwd(value);
			}
		}
		public int VersionMajor
		{
			get
			{
				return this._versionMajor;
			}
			set
			{
				this._versionMajor = value;
			}
		}
		public int VersionMinor
		{
			get
			{
				return this._versionMinor;
			}
			set
			{
				this._versionMinor = value;
			}
		}
		public int VersionBuild
		{
			get
			{
				return this._versionBuild;
			}
			set
			{
				this._versionBuild = value;
			}
		}
		internal string EncryptedPassword
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}
		public DwmHostCollection Hosts
		{
			get
			{
				return DwmBase.SafeGetItem<DwmHostCollection>(ref this._listHosts);
			}
			internal set
			{
				this._listHosts = value;
			}
		}
		public int MaxCpuRating
		{
			get
			{
				return this._maxCpuRating;
			}
			internal set
			{
				this._maxCpuRating = value;
			}
		}
		internal DwmVmMovementCollection RecentVmMoves
		{
			get
			{
				return DwmBase.SafeGetItem<DwmVmMovementCollection>(ref this._recentVmMoves);
			}
		}
		public bool AreCredentialsValid
		{
			get
			{
				bool result = true;
				Dictionary<string, string> otherConfig = this.GetOtherConfig();
				if (otherConfig != null)
				{
					string text = null;
					if (otherConfig.TryGetValue("CredentialsValid", out text) && !string.IsNullOrEmpty(text) && Localization.Compare(text, "false", true) == 0)
					{
						result = false;
					}
				}
				return result;
			}
			set
			{
				this.SetOtherConfig("CredentialsValid", (!value) ? "false" : "true");
			}
		}
		public ICollectorActions CollectorActions
		{
			get
			{
				ICollectorActions collectorActions = null;
				if (DwmPool._collectorsCache != null && !DwmPool._collectorsCache.TryGetValue(base.Id, out collectorActions))
				{
					collectorActions = null;
				}
				if (collectorActions == null)
				{
					collectorActions = DwmPool.CreateCollectorActions(base.HVType);
					if (collectorActions != null)
					{
						if (string.IsNullOrEmpty(base.PrimaryPoolMasterAddr) || string.IsNullOrEmpty(base.Protocol) || string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.EncryptedPassword) || base.PrimaryPoolMasterPort == 0)
						{
							DwmPool dwmPool = DwmPool.SimpleLoadByPoolId(base.Id);
							base.PrimaryPoolMasterAddr = dwmPool.PrimaryPoolMasterAddr;
							base.PrimaryPoolMasterPort = dwmPool.PrimaryPoolMasterPort;
							base.Protocol = dwmPool.Protocol;
							this.UserName = dwmPool.UserName;
							this.Password = dwmPool.Password;
						}
						try
						{
							collectorActions.Initialize(base.PrimaryPoolMasterAddr, base.PrimaryPoolMasterPort, base.Protocol, this.UserName, this.Password, base.Id);
						}
						catch (DwmException)
						{
							collectorActions = null;
						}
					}
				}
				return collectorActions;
			}
		}
		public DwmPool(string uuid, string name, DwmHypervisorType hypervisorType) : base(uuid, name, hypervisorType)
		{
		}
		public DwmPool(int poolId) : base(poolId)
		{
		}
		public void SetEnabled(bool enabled)
		{
			DwmPool.SetEnabled(base.Uuid, enabled);
			base.Enabled = enabled;
		}
		public static void SetEnabled(string poolUuid, bool enabled)
		{
			int num = DwmPoolBase.UuidToId(poolUuid);
			string sqlStatement = "hv_pool_set_enabled";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@pool_id", num));
			storedProcParamCollection.Add(new StoredProcParam("@enabled", enabled));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		public void UpdateDiscoveryStatus(DiscoveryStatus status)
		{
			if (base.Id > 0)
			{
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.ExecuteNonQuery("update_discovery_status", new StoredProcParamCollection
					{
						new StoredProcParam("_pool_id", base.Id, StoredProcParam.DataTypes.Integer),
						new StoredProcParam("_discovery_status", (int)status, StoredProcParam.DataTypes.Integer)
					});
				}
			}
		}
		public void GetDiscoveryStatus(out DiscoveryStatus status, out DateTime lastDiscoveryCompleted)
		{
			if (base.Id == 0)
			{
				throw new DwmException("Pool Id is 0. Pool not in the database.");
			}
			status = DiscoveryStatus.New;
			lastDiscoveryCompleted = DateTime.MinValue;
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("_pool_id", base.Id, StoredProcParam.DataTypes.Bigint));
				string sqlStatement = "get_discovery_status";
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						status = (DiscoveryStatus)DBAccess.GetInt(dataReader, "discovery_status");
						lastDiscoveryCompleted = DBAccess.GetDateTime(dataReader, "last_discovery_completed");
					}
				}
			}
		}
		internal static void SetLastResultByHostId(int hostId, DwmStatus result)
		{
			string sqlStatement = "set_pool_last_result_by_host_id";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@host_id", hostId));
			storedProcParamCollection.Add(new StoredProcParam("@last_result", (int)result));
			storedProcParamCollection.Add(new StoredProcParam("@last_result_time", DateTime.UtcNow));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		public static bool IsValidPool(string poolAddr, int poolPort, string userName, string password, DwmHypervisorType hvType)
		{
			bool result = false;
			try
			{
				ICollectorActions collectorActions = DwmPool.CreateCollectorActions(hvType);
				collectorActions.Initialize(poolAddr, poolPort, string.Empty, userName, password, 0);
				collectorActions.UnInitialize();
				result = true;
			}
			catch (Exception)
			{
			}
			return result;
		}
		public static bool IsValidPool(string poolAddr, int poolPort, string userName, string password, DwmHypervisorType hvType, out string poolName, out string poolUuid)
		{
			bool result = false;
			poolName = string.Empty;
			poolUuid = string.Empty;
			try
			{
				ICollectorActions collectorActions = DwmPool.CreateCollectorActions(hvType);
				collectorActions.Initialize(poolAddr, poolPort, string.Empty, userName, password, 0);
				poolName = collectorActions.GetPoolName();
				poolUuid = collectorActions.GetPoolUniqueIdentifier();
				collectorActions.UnInitialize();
				result = true;
			}
			catch (Exception)
			{
			}
			return result;
		}
		public static DwmPool Load(string poolUuid)
		{
			int num = DwmPoolBase.UuidToId(poolUuid);
			if (num <= 0)
			{
				throw new DwmException("Invalid Pool uuid", DwmErrorCode.InvalidParameter, null);
			}
			return DwmPool.Load(num);
		}
		public static DwmPool Load(int poolId)
		{
			DwmPool dwmPool = new DwmPool(poolId);
			dwmPool.Load();
			return dwmPool;
		}
		public void Load()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader("load_hv_pool", new StoredProcParamCollection
				{
					new StoredProcParam("@pool_id", base.Id)
				}))
				{
					if (dataReader.Read())
					{
						base.Id = DBAccess.GetInt(dataReader, "id");
						base.Name = DBAccess.GetString(dataReader, "name");
						if (string.IsNullOrEmpty(base.Uuid))
						{
							base.Uuid = DBAccess.GetString(dataReader, "uuid");
						}
						base.Description = DBAccess.GetString(dataReader, "description");
						if (base.HVType == DwmHypervisorType.None)
						{
							base.HVType = (DwmHypervisorType)DBAccess.GetInt(dataReader, "hv_type");
						}
						base.PrimaryPoolMasterAddr = DBAccess.GetString(dataReader, "pool_master_1_addr");
						base.SecondaryPoolMasterAddr = DBAccess.GetString(dataReader, "pool_master_2_addr");
						base.PrimaryPoolMasterPort = DBAccess.GetInt(dataReader, "pool_master_1_port");
						base.SecondaryPoolMasterPort = DBAccess.GetInt(dataReader, "pool_master_2_port");
						base.Protocol = DBAccess.GetString(dataReader, "protocol");
						base.Enabled = DBAccess.GetBool(dataReader, "enabled");
						base.IsLicensed = DBAccess.GetBool(dataReader, "is_licensed");
						this.UserName = DBAccess.GetString(dataReader, "username");
						this.EncryptedPassword = DBAccess.GetString(dataReader, "password");
						this.TouchedBy = DBAccess.GetString(dataReader, "touched_by");
						this.TimeStamp = DBAccess.GetDateTime(dataReader, "tstamp");
						base.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
						base.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
						base.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
						base.PoolDiscoveryStatus = (DiscoveryStatus)DBAccess.GetInt(dataReader, "discovery_status");
						base.LastDiscoveryCompleted = DBAccess.GetDateTime(dataReader, "last_discovery_completed");
					}
					if (dataReader.NextResult() && dataReader.Read())
					{
						base.LoadThresholdsAndWeights(dataReader);
					}
					if (dataReader.NextResult())
					{
						while (dataReader.Read())
						{
							DwmHost dwmHost = new DwmHost(DBAccess.GetString(dataReader, "uuid"), DBAccess.GetString(dataReader, "name"), DBAccess.GetInt(dataReader, "poolid"));
							dwmHost.Id = DBAccess.GetInt(dataReader, "hostid");
							dwmHost.Description = DBAccess.GetString(dataReader, "description");
							dwmHost.NumCpus = DBAccess.GetInt(dataReader, "num_cpus");
							dwmHost.CpuSpeed = DBAccess.GetInt(dataReader, "cpu_speed");
							dwmHost.NumNics = DBAccess.GetInt(dataReader, "num_pifs");
							dwmHost.IsPoolMaster = DBAccess.GetBool(dataReader, "is_pool_master");
							dwmHost.Enabled = DBAccess.GetBool(dataReader, "enabled");
							dwmHost.Metrics.FillOrder = DBAccess.GetInt(dataReader, "fill_order");
							dwmHost.IPAddress = DBAccess.GetString(dataReader, "ip_address");
							dwmHost.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
							dwmHost.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
							dwmHost.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
							dwmHost.Metrics.TotalMemory = DBAccess.GetInt64(dataReader, "total_mem");
							this.Hosts.Add(dwmHost);
						}
					}
					if (dataReader.NextResult())
					{
						this.Hosts.LoadVMs(dataReader);
					}
				}
			}
		}
		public string GetOtherConfigItem(string itemName)
		{
			return base.GetOtherConfigItem("hv_pool_config_get_item", "@pool_id", itemName);
		}
		public Dictionary<string, string> GetOtherConfig()
		{
			return base.GetOtherConfig("hv_pool_config_get", "@pool_id");
		}
		public void SetOtherConfig(string name, string value)
		{
			base.SetOtherConfig("hv_pool_config_update", "@pool_id", name, value);
		}
		public void SetOtherConfig(DBAccess dbAccess, string name, string value)
		{
			base.SetOtherConfig(dbAccess, "hv_pool_config_update", "@pool_id", name, value);
		}
		public void SetOtherConfig(Dictionary<string, string> config)
		{
			base.SetOtherConfig("hv_pool_config_update", "@pool_id", config);
		}
		public static DwmPool SimpleLoadByPoolId(int poolId)
		{
			string sql = "get_pool_by_pool_id";
			return DwmPool.SimpleLoad(sql, new StoredProcParamCollection
			{
				new StoredProcParam("@pool_id", poolId)
			});
		}
		internal static DwmPool SimpleLoadByPoolUuid(string poolUuid)
		{
			string sql = "get_pool_by_pool_uuid";
			return DwmPool.SimpleLoad(sql, new StoredProcParamCollection
			{
				new StoredProcParam("@pool_uuid", poolUuid)
			});
		}
		private static DwmPool SimpleLoad(string sql, StoredProcParamCollection parms)
		{
			DwmPool dwmPool = null;
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sql, parms))
				{
					while (dataReader.Read())
					{
						dwmPool = new DwmPool(DBAccess.GetInt(dataReader, "id"));
						dwmPool.HVType = (DwmHypervisorType)DBAccess.GetInt(dataReader, "hv_type");
						dwmPool.PrimaryPoolMasterAddr = DBAccess.GetString(dataReader, "pool_master_1_addr");
						dwmPool.PrimaryPoolMasterPort = DBAccess.GetInt(dataReader, "pool_master_1_port");
						dwmPool.Protocol = DBAccess.GetString(dataReader, "protocol");
						dwmPool.UserName = DBAccess.GetString(dataReader, "username");
						dwmPool.EncryptedPassword = DBAccess.GetString(dataReader, "password");
					}
				}
			}
			return dwmPool;
		}
		internal static ICollectorActions GetCollector(string poolUuid)
		{
			int poolId = DwmPoolBase.UuidToId(poolUuid);
			return DwmPool.GetCollector(poolId);
		}
		internal static ICollectorActions GetCollector(int poolId)
		{
			ICollectorActions result = null;
			DwmPool dwmPool = DwmPool.SimpleLoadByPoolId(poolId);
			if (dwmPool != null)
			{
				result = dwmPool.CollectorActions;
			}
			return result;
		}
		public void ReleaseCollectorActions(ICollectorActions collector)
		{
			if (collector != null)
			{
				collector.UnInitialize();
				if (DwmPool._collectorsCache != null && !DwmPool._collectorsCache.ContainsKey(base.Id))
				{
					DwmPool._collectorsCache.Remove(base.Id);
				}
			}
		}
		public static void LoadCache()
		{
			object collectorsCacheLock = DwmPool._collectorsCacheLock;
			Monitor.Enter(collectorsCacheLock);
			try
			{
				if (DwmPool._collectorsCache == null)
				{
					DwmPool._collectorsCache = new Dictionary<int, ICollectorActions>();
					DwmPoolCollection dwmPoolCollection = DwmPoolCollection.LoadPoolsForDataCollection();
					for (int i = 0; i < dwmPoolCollection.Count; i++)
					{
						DwmPool._collectorsCache.Add(dwmPoolCollection[i].Id, dwmPoolCollection[i].CollectorActions);
					}
					foreach (KeyValuePair<int, ICollectorActions> current in DwmPool._collectorsCache)
					{
						if (current.Value != null)
						{
							current.Value.Start();
						}
					}
				}
			}
			finally
			{
				Monitor.Exit(collectorsCacheLock);
			}
		}
		public static void UnloadCache()
		{
			object collectorsCacheLock = DwmPool._collectorsCacheLock;
			Monitor.Enter(collectorsCacheLock);
			try
			{
				if (DwmPool._collectorsCache != null)
				{
					foreach (KeyValuePair<int, ICollectorActions> current in DwmPool._collectorsCache)
					{
						if (current.Value != null)
						{
							current.Value.Stop();
						}
					}
					DwmPool._collectorsCache.Clear();
					DwmPool._collectorsCache = null;
				}
			}
			finally
			{
				Monitor.Exit(collectorsCacheLock);
			}
		}
		private static void AddPoolToCache(DwmPool pool)
		{
			object collectorsCacheLock = DwmPool._collectorsCacheLock;
			Monitor.Enter(collectorsCacheLock);
			try
			{
				if (DwmPool._collectorsCache != null && !DwmPool._collectorsCache.ContainsKey(pool.Id))
				{
					ICollectorActions collectorActions = pool.CollectorActions;
					collectorActions.Start();
					DwmPool._collectorsCache.Add(pool.Id, collectorActions);
				}
			}
			finally
			{
				Monitor.Exit(collectorsCacheLock);
			}
		}
		private static void DeletePoolFromCache(int poolId)
		{
			object collectorsCacheLock = DwmPool._collectorsCacheLock;
			Monitor.Enter(collectorsCacheLock);
			try
			{
				if (DwmPool._collectorsCache != null)
				{
					ICollectorActions collectorActions = null;
					if (DwmPool._collectorsCache.TryGetValue(poolId, out collectorActions))
					{
						collectorActions.Stop();
						DwmPool._collectorsCache.Remove(poolId);
					}
				}
			}
			finally
			{
				Monitor.Exit(collectorsCacheLock);
			}
		}
		public void Save()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				this.Save(dBAccess);
			}
		}
		public void Save(DBAccess db)
		{
			if (db != null && !this._isDeleted)
			{
				object poolSaveLock = DwmPool._poolSaveLock;
				Monitor.Enter(poolSaveLock);
				try
				{
					if (!string.IsNullOrEmpty(base.PrimaryPoolMasterAddr))
					{
						string sqlStatement = "add_update_hv_pool";
						StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
						storedProcParamCollection.Add(new StoredProcParam("@uuid", (base.Uuid == null) ? string.Empty : base.Uuid));
						storedProcParamCollection.Add(new StoredProcParam("@name", (base.Name == null) ? string.Empty : base.Name));
						storedProcParamCollection.Add(new StoredProcParam("@description", (base.Description == null) ? string.Empty : base.Description));
						storedProcParamCollection.Add(new StoredProcParam("@hv_type", (int)base.HVType));
						storedProcParamCollection.Add(new StoredProcParam("@enabled", base.Enabled));
						storedProcParamCollection.Add(new StoredProcParam("@protocol", string.IsNullOrEmpty(base.Protocol) ? "HTTP" : base.Protocol));
						storedProcParamCollection.Add(new StoredProcParam("@primary_pool_master_addr", (base.PrimaryPoolMasterAddr == null) ? string.Empty : base.PrimaryPoolMasterAddr));
						storedProcParamCollection.Add(new StoredProcParam("@primary_pool_master_port", base.PrimaryPoolMasterPort));
						storedProcParamCollection.Add(new StoredProcParam("@secondary_pool_master_addr", (base.SecondaryPoolMasterAddr == null) ? string.Empty : base.SecondaryPoolMasterAddr));
						storedProcParamCollection.Add(new StoredProcParam("@secondary_pool_master_port", base.SecondaryPoolMasterPort));
						storedProcParamCollection.Add(new StoredProcParam("@username", (this.UserName == null) ? string.Empty : this.UserName));
						storedProcParamCollection.Add(new StoredProcParam("@password", (this._password == null) ? string.Empty : this._password));
						storedProcParamCollection.Add(new StoredProcParam("@touched_by", (this.TouchedBy == null) ? DBAccess.GetCurrentUserName() : this.TouchedBy));
						storedProcParamCollection.Add(new StoredProcParam("@tstamp", DateTime.UtcNow));
						base.Id = db.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
						DwmPool.AddPoolToCache(this);
						storedProcParamCollection.Clear();
						storedProcParamCollection.Add(new StoredProcParam("@pool_id", base.Id));
						if (db.ExecuteScalarInt("get_pool_threshold_id_by_id", storedProcParamCollection) == 0)
						{
							this.SaveThresholds(db);
						}
					}
					else
					{
						if (base.Id <= 0)
						{
							throw new DwmException("The address of the pool must be specified before the pool can be saved.  The user name and password must be set before data can be collected from the pool", DwmErrorCode.InvalidOperation, null);
						}
					}
					if (this._optMode != OptimizationMode.None)
					{
						this.SetOtherConfig(db, "OptimizationMode", ((int)base.OptMode).ToString());
					}
					if (this._autoBalance != -1)
					{
						this.SetOtherConfig(db, "AutoBalanceEnabled", (!base.AutoBalance) ? "false" : "true");
					}
					if (this._managePower != -1)
					{
						this.SetOtherConfig(db, "PowerManagementEnabled", (!base.ManagePower) ? "false" : "true");
					}
					if (this._overCommitCpusInPerfMode != -1)
					{
						this.SetOtherConfig(db, "OverCommitCpuInPerfMode", (!base.OverCommitCpusInPerfMode) ? "false" : "true");
					}
					if (this._overCommitCpusInDensityMode != -1)
					{
						this.SetOtherConfig(db, "OverCommitCpuInDensityMode", (!base.OverCommitCpusInDensityMode) ? "false" : "true");
					}
					if (this._poolAuditLogGranularity != null)
					{
						this.SetOtherConfig(db, "PoolAuditLogGranularity", this.PoolAuditLogGranularity);
					}
					this.SetOtherConfig(db, "OverCommitCpuRatio", base.OverCommitCpuRatio.ToString());
					if (this._versionMajor != -1)
					{
						this.SetOtherConfig(db, "VersionMajor", this._versionMajor.ToString());
						this.SetOtherConfig(db, "VersionMinor", this._versionMinor.ToString());
						this.SetOtherConfig(db, "VersionBuild", this._versionBuild.ToString());
					}
				}
				finally
				{
					Monitor.Exit(poolSaveLock);
				}
				return;
			}
			throw new DwmException("Cannot pass null DBAccess instance to Save", DwmErrorCode.NullReference, null);
		}
		public void SavePoolChildren()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				this.SavePoolChildren(dBAccess);
			}
		}
		private void SavePoolChildren(DBAccess db)
		{
			if (this._listHosts != null && this._listHosts.Count > 0)
			{
				try
				{
					bool flag = true;
					for (int i = 0; i < this._listHosts.Count; i++)
					{
						flag &= this._listHosts[i].IsEnterpriseOrHigher;
					}
					this._isLicensed = flag;
					db.ExecuteNonQuery("hv_pool_set_licensed", new StoredProcParamCollection
					{
						new StoredProcParam("@pool_id", base.Id),
						new StoredProcParam("@is_licensed", flag)
					});
					this._listHosts.Save(db);
					StringBuilder stringBuilder = new StringBuilder();
					string empty = string.Empty;
					this._maxCpuRating = 1;
					stringBuilder.AppendFormat("select * from delete_hv_pool_host({0});\n", base.Id);
					for (int j = 0; j < this._listHosts.Count; j++)
					{
						stringBuilder.AppendFormat("select * from add_update_hv_pool_host({0}, {1});\n", base.Id, this._listHosts[j].Id);
						int num = this._listHosts[j].NumCpus * this._listHosts[j].CpuSpeed;
						if (num > this._maxCpuRating)
						{
							this._maxCpuRating = num;
						}
					}
					stringBuilder.AppendFormat("select * from generate_fill_order ({0});\n", base.Id);
					stringBuilder.AppendFormat("select * from set_pool_cpu_rating({0}, {1});\n", base.Id, this._maxCpuRating);
					DwmBase.WriteData(db, stringBuilder);
				}
				catch (Exception ex)
				{
					Logger.LogException(ex);
					throw ex;
				}
			}
		}
		public void SaveThresholds()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				this.SaveThresholds(dBAccess);
			}
		}
		public void SaveThresholds(DBAccess db)
		{
			if (db != null)
			{
				string sqlStatement = "add_update_hv_pool_threshold";
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("pool_id", base.Id));
				storedProcParamCollection.Add(new StoredProcParam("@host_cpu_threshold_critical", base.HostCpuThreshold.Critical));
				storedProcParamCollection.Add(new StoredProcParam("@host_cpu_threshold_high", base.HostCpuThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@host_cpu_threshold_medium", base.HostCpuThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@host_cpu_threshold_low", base.HostCpuThreshold.Low));
				long num = (long)base.HostMemoryThreshold.Critical;
				long num2 = (long)base.HostMemoryThreshold.High;
				long num3 = (long)base.HostMemoryThreshold.Medium;
				long num4 = (long)base.HostMemoryThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@host_memory_threshold_critical", num));
				storedProcParamCollection.Add(new StoredProcParam("@host_memory_threshold_high", num2));
				storedProcParamCollection.Add(new StoredProcParam("@host_memory_threshold_medium", num3));
				storedProcParamCollection.Add(new StoredProcParam("@host_memory_threshold_low", num4));
				long num5 = (long)base.HostPifReadThreshold.Critical;
				long num6 = (long)base.HostPifReadThreshold.High;
				long num7 = (long)base.HostPifReadThreshold.Medium;
				long num8 = (long)base.HostPifReadThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@host_net_read_threshold_critical", num5));
				storedProcParamCollection.Add(new StoredProcParam("@host_net_read_threshold_high", num6));
				storedProcParamCollection.Add(new StoredProcParam("@host_net_read_threshold_medium", num7));
				storedProcParamCollection.Add(new StoredProcParam("@host_net_read_threshold_low", num8));
				long num9 = (long)base.HostPifWriteThreshold.Critical;
				long num10 = (long)base.HostPifWriteThreshold.High;
				long num11 = (long)base.HostPifWriteThreshold.Medium;
				long num12 = (long)base.HostPifWriteThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@host_net_write_threshold_critical", num9));
				storedProcParamCollection.Add(new StoredProcParam("@host_net_write_threshold_high", num10));
				storedProcParamCollection.Add(new StoredProcParam("@host_net_write_threshold_medium", num11));
				storedProcParamCollection.Add(new StoredProcParam("@host_net_write_threshold_low", num12));
				long num13 = (long)base.HostPbdReadThreshold.Critical;
				long num14 = (long)base.HostPbdReadThreshold.High;
				long num15 = (long)base.HostPbdReadThreshold.Medium;
				long num16 = (long)base.HostPbdReadThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@host_disk_read_threshold_critical", num13));
				storedProcParamCollection.Add(new StoredProcParam("@host_disk_read_threshold_high", num14));
				storedProcParamCollection.Add(new StoredProcParam("@host_disk_read_threshold_medium", num15));
				storedProcParamCollection.Add(new StoredProcParam("@host_disk_read_threshold_low", num16));
				long num17 = (long)base.HostPbdWriteThreshold.Critical;
				long num18 = (long)base.HostPbdWriteThreshold.High;
				long num19 = (long)base.HostPbdWriteThreshold.Medium;
				long num20 = (long)base.HostPbdWriteThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@host_disk_write_threshold_critical", num17));
				storedProcParamCollection.Add(new StoredProcParam("@host_disk_write_threshold_high", num18));
				storedProcParamCollection.Add(new StoredProcParam("@host_disk_write_threshold_medium", num19));
				storedProcParamCollection.Add(new StoredProcParam("@host_disk_write_threshold_low", num20));
				storedProcParamCollection.Add(new StoredProcParam("@host_load_avg_threshold_critical", base.HostLoadAverageThreshold.Critical));
				storedProcParamCollection.Add(new StoredProcParam("@host_load_avg_threshold_high", base.HostLoadAverageThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@host_load_avg_threshold_medium", base.HostLoadAverageThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@host_load_avg_threshold_low", base.HostLoadAverageThreshold.Low));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_blocked_threshold_critical", base.HostRunstateBlockedThreshold.Critical));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_blocked_threshold_high", base.HostRunstateBlockedThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_blocked_threshold_medium", base.HostRunstateBlockedThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_blocked_threshold_low", base.HostRunstateBlockedThreshold.Low));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_partial_run_threshold_critical", base.HostRunstatePartialRunThreshold.Critical));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_partial_run_threshold_high", base.HostRunstatePartialRunThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_partial_run_threshold_medium", base.HostRunstatePartialRunThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_partial_run_threshold_low", base.HostRunstatePartialRunThreshold.Low));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_fullrun_threshold_critical", base.HostRunstateFullRunThreshold.Critical));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_fullrun_threshold_high", base.HostRunstateFullRunThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_fullrun_threshold_medium", base.HostRunstateFullRunThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_fullrun_threshold_low", base.HostRunstateFullRunThreshold.Low));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_partial_contention_threshold_critical", base.HostRunstatePartialContentionThreshold.Critical));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_partial_contention_threshold_high", base.HostRunstatePartialContentionThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_partial_contention_threshold_medium", base.HostRunstatePartialContentionThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_partial_contention_threshold_low", base.HostRunstatePartialContentionThreshold.Low));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_concurrency_hazard_threshold_critical", base.HostRunstateConcurrencyHazardThreshold.Critical));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_concurrency_hazard_threshold_high", base.HostRunstateConcurrencyHazardThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_concurrency_hazard_threshold_medium", base.HostRunstateConcurrencyHazardThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_concurrency_hazard_threshold_low", base.HostRunstateConcurrencyHazardThreshold.Low));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_full_contention_threshold_critical", base.HostRunstateFullContentionThreshold.Critical));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_full_contention_threshold_high", base.HostRunstateFullContentionThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_full_contention_threshold_medium", base.HostRunstateFullContentionThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@host_runstate_full_contention_threshold_low", base.HostRunstateFullContentionThreshold.Low));
				storedProcParamCollection.Add(new StoredProcParam("@weight_current_metrics", base.WeightCurrentMetrics));
				storedProcParamCollection.Add(new StoredProcParam("@weight_last_30_metrics", base.WeightRecentMetrics));
				storedProcParamCollection.Add(new StoredProcParam("@weight_yesterday_metrics", base.WeightHistoricMetrics));
				storedProcParamCollection.Add(new StoredProcParam("@vm_cpu_threshold_high", base.VmCpuUtilizationThreshold.High));
				storedProcParamCollection.Add(new StoredProcParam("@vm_cpu_threshold_medium", base.VmCpuUtilizationThreshold.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@vm_cpu_threshold_low", base.VmCpuUtilizationThreshold.Low));
				storedProcParamCollection.Add(new StoredProcParam("@vm_cpu_weight_high", base.VmCpuUtilizationWeight.High));
				storedProcParamCollection.Add(new StoredProcParam("@vm_cpu_weight_medium", base.VmCpuUtilizationWeight.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@vm_cpu_weight_low", base.VmCpuUtilizationWeight.Low));
				long num21 = (long)base.VmMemoryThreshold.High;
				long num22 = (long)base.VmMemoryThreshold.Medium;
				long num23 = (long)base.VmMemoryThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@vm_memory_threshold_high", num21));
				storedProcParamCollection.Add(new StoredProcParam("@vm_memory_threshold_medium", num22));
				storedProcParamCollection.Add(new StoredProcParam("@vm_memory_threshold_low", num23));
				storedProcParamCollection.Add(new StoredProcParam("@vm_memory_weight_high", base.VmMemoryWeight.High));
				storedProcParamCollection.Add(new StoredProcParam("@vm_memory_weight_medium", base.VmMemoryWeight.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@vm_memory_weight_low", base.VmMemoryWeight.Low));
				long num24 = (long)base.VmNetworkReadThreshold.High;
				long num25 = (long)base.VmNetworkReadThreshold.Medium;
				long num26 = (long)base.VmNetworkReadThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_read_threshold_high", num24));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_read_threshold_medium", num25));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_read_threshold_low", num26));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_read_weight_high", base.VmNetworkReadWeight.High));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_read_weight_medium", base.VmNetworkReadWeight.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_read_weight_low", base.VmNetworkReadWeight.Low));
				long num27 = (long)base.VmNetworkWriteThreshold.High;
				long num28 = (long)base.VmNetworkWriteThreshold.Medium;
				long num29 = (long)base.VmNetworkWriteThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_write_threshold_high", num27));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_write_threshold_medium", num28));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_write_threshold_low", num29));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_write_weight_high", base.VmNetworkWriteWeight.High));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_write_weight_medium", base.VmNetworkWriteWeight.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@vm_net_write_weight_low", base.VmNetworkWriteWeight.Low));
				long num30 = (long)base.VmDiskReadThreshold.High;
				long num31 = (long)base.VmDiskReadThreshold.Medium;
				long num32 = (long)base.VmDiskReadThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_read_threshold_high", num30));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_read_threshold_medium", num31));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_read_threshold_low", num32));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_read_weight_high", base.VmDiskReadWeight.High));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_read_weight_medium", base.VmDiskReadWeight.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_read_weight_low", base.VmDiskReadWeight.Low));
				long num33 = (long)base.VmDiskWriteThreshold.High;
				long num34 = (long)base.VmDiskWriteThreshold.Medium;
				long num35 = (long)base.VmDiskWriteThreshold.Low;
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_write_threshold_high", num33));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_write_threshold_medium", num34));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_write_threshold_low", num35));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_write_weight_high", base.VmDiskWriteWeight.High));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_write_weight_medium", base.VmDiskWriteWeight.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@vm_disk_write_weight_low", base.VmDiskWriteWeight.Low));
				storedProcParamCollection.Add(new StoredProcParam("@vm_runstate_weight_high", base.VmRunstateWeight.High));
				storedProcParamCollection.Add(new StoredProcParam("@vm_runstate_weight_medium", base.VmRunstateWeight.Medium));
				storedProcParamCollection.Add(new StoredProcParam("@vm_runstate_weight_low", base.VmRunstateWeight.Low));
				db.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
				return;
			}
			throw new DwmException("Cannot pass null DBAccess instance to SaveThresholds", DwmErrorCode.NullReference, null);
		}
		internal static void GenerateFillOrder(int poolId)
		{
			string sqlStatement = "generate_fill_order";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@pool_id", poolId));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		public void Delete()
		{
			if (base.Id > 0)
			{
				DwmPool.Delete(base.Id);
			}
			else
			{
				if (string.IsNullOrEmpty(base.Uuid))
				{
					throw new DwmException("The ID or UUID of the hypervisor pool to delete must be specified", DwmErrorCode.InvalidParameter, null);
				}
				DwmPool.Delete(base.Uuid);
			}
			this._isDeleted = true;
		}
		public static void Delete(int id)
		{
			object poolSaveLock = DwmPool._poolSaveLock;
			Monitor.Enter(poolSaveLock);
			try
			{
				DateTime now = DateTime.Now;
				string sqlStatement = "delete_pool_by_pool_id";
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("@pool_id", id));
				int num;
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.Timeout = 300;
					num = dBAccess.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
				}
				if (num > 0)
				{
					Logger.Trace("Deleted pool with id={0} in {1} seconds", new object[]
					{
						id,
						(DateTime.Now - now).Seconds
					});
					DwmPoolBase.RemovePoolFromCache(id);
					DwmPool.DeletePoolFromCache(id);
				}
			}
			finally
			{
				Monitor.Exit(poolSaveLock);
			}
		}
		public static void Delete(string uuid)
		{
			object poolSaveLock = DwmPool._poolSaveLock;
			Monitor.Enter(poolSaveLock);
			try
			{
				if (!string.IsNullOrEmpty(uuid))
				{
					DateTime now = DateTime.Now;
					string sqlStatement = "delete_pool_by_pool_uuid";
					StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
					storedProcParamCollection.Add(new StoredProcParam("@pool_uuid", uuid));
					int num;
					using (DBAccess dBAccess = new DBAccess())
					{
						dBAccess.Timeout = 300;
						num = dBAccess.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
					}
					if (num > 0)
					{
						Logger.Trace("Deleted pool with uuid={0} in {1} seconds", new object[]
						{
							uuid,
							(DateTime.Now - now).Seconds
						});
						DwmPoolBase.RemovePoolFromCache(uuid);
						DwmPool.DeletePoolFromCache(num);
					}
				}
			}
			finally
			{
				Monitor.Exit(poolSaveLock);
			}
		}
		internal static ICollector CreateCollector(DwmHypervisorType hvType)
		{
			AssemblyName assemblyRef = new AssemblyName("Halsign.Dwm.Collectors");
			Assembly assembly = Assembly.Load(assemblyRef);
			string typeName = null;
			switch (hvType)
			{
			case DwmHypervisorType.None:
				throw new DwmException("Cannot create data collector.  Hypervisor type is invalid", DwmErrorCode.InvalidParameter, null);
			case DwmHypervisorType.XenServer:
				typeName = "Halsign.DWM.Collectors.XenCollector";
				break;
			case DwmHypervisorType.VMWareEsx:
				typeName = "Halsign.DWM.Collectors.VmWareCollector";
				break;
			case DwmHypervisorType.HyperV:
				typeName = "Halsign.DWM.Collectors.HyperVCollector";
				break;
			}
			return assembly.CreateInstance(typeName) as ICollector;
		}
		internal static ICollectorActions CreateCollectorActions(DwmHypervisorType hvType)
		{
			AssemblyName assemblyRef = new AssemblyName("Halsign.Dwm.Collectors");
			Assembly assembly = Assembly.Load(assemblyRef);
			string typeName = null;
			switch (hvType)
			{
			case DwmHypervisorType.None:
				throw new DwmException("Cannot create data collector actions.  Hypervisor type is invalid", DwmErrorCode.InvalidParameter, null);
			case DwmHypervisorType.XenServer:
				typeName = "Halsign.DWM.Collectors.XenCollectorActions";
				break;
			case DwmHypervisorType.VMWareEsx:
				typeName = "Halsign.DWM.Collectors.VmWareCollectorActions";
				break;
			case DwmHypervisorType.HyperV:
				typeName = "Halsign.DWM.Collectors.HyperVCollectorActions";
				break;
			}
			return assembly.CreateInstance(typeName) as ICollectorActions;
		}
		public List<MoveRecommendation> GetOptimizations()
		{
			return DwmPool.GetOptimizations(base.Id);
		}
		public static List<MoveRecommendation> GetOptimizations(string poolUuid)
		{
			int num = DwmPoolBase.UuidToId(poolUuid);
			if (num > 0)
			{
				return DwmPool.InternalGetOptimization(num);
			}
			throw new DwmException("Invalid pool UUID", DwmErrorCode.InvalidParameter, null);
		}
		public static List<MoveRecommendation> GetOptimizations(int poolId)
		{
			return DwmPool.InternalGetOptimization(poolId);
		}
		private static List<MoveRecommendation> InternalGetOptimization(int poolId)
		{
			List<MoveRecommendation> list = null;
			string sqlStatement = "get_recommendations_for_pool_by_pool_id";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("_pool_id", poolId));
			storedProcParamCollection.Add(new StoredProcParam("_poll_interval_seconds", Configuration.GetValueAsInt(ConfigItems.AnalysisEnginePollInterval)));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						bool @bool = DBAccess.GetBool(dataReader, "is_licensed");
						if (!@bool)
						{
							throw new DwmException("The pool does not have paid for license", DwmErrorCode.NotLicensed, null);
						}
						if (dataReader.NextResult())
						{
							list = new List<MoveRecommendation>();
							while (dataReader.Read())
							{
								list.Add(new MoveRecommendation
								{
									RecommendationId = DBAccess.GetInt(dataReader, "recommendation_id"),
									RecommendationSetId = DBAccess.GetInt(dataReader, "event_id"),
									PoolId = DBAccess.GetInt(dataReader, "pool_id"),
									Severity = (OptimizationSeverity)DBAccess.GetInt(dataReader, "severity"),
									VmId = DBAccess.GetInt(dataReader, "vm_id"),
									VmName = DBAccess.GetString(dataReader, "vm_name"),
									VmUuid = DBAccess.GetString(dataReader, "vm_uuid"),
									MoveFromHostId = DBAccess.GetInt(dataReader, "move_from_host_id"),
									MoveFromHostUuid = DBAccess.GetString(dataReader, "move_from_host_uuid"),
									MoveFromHostName = DBAccess.GetString(dataReader, "move_from_host_name"),
									MoveToHostId = DBAccess.GetInt(dataReader, "move_to_host_id"),
									MoveToHostUuid = DBAccess.GetString(dataReader, "move_to_host_uuid"),
									MoveToHostName = DBAccess.GetString(dataReader, "move_to_host_name"),
									Reason = (ResourceToOptimize)DBAccess.GetInt(dataReader, "reason_id")
								});
							}
						}
					}
				}
			}
			return list;
		}
		public static List<MoveRecommendationStatus> GetOptimizationStatus(int optimizationId)
		{
			List<MoveRecommendationStatus> list = null;
			string sqlStatement = "get_recommendation_status";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@event_id", optimizationId));
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						list = new List<MoveRecommendationStatus>();
						do
						{
							MoveRecommendationStatus item = DwmPool.LoadMoveRecStatus(dataReader);
							list.Add(item);
						}
						while (dataReader.Read());
					}
				}
			}
			return list;
		}
		internal static MoveRecommendationStatus LoadMoveRecStatus(IDataReader reader)
		{
			return new MoveRecommendationStatus
			{
				RecommendationId = DBAccess.GetInt(reader, "recommendation_id"),
				RecommendationSetId = DBAccess.GetInt(reader, "event_id"),
				PoolId = DBAccess.GetInt(reader, "pool_id"),
				Severity = (OptimizationSeverity)DBAccess.GetInt(reader, "severity"),
				VmId = DBAccess.GetInt(reader, "vm_id"),
				VmName = DBAccess.GetString(reader, "vm_name"),
				VmUuid = DBAccess.GetString(reader, "vm_uuid"),
				MoveFromHostId = DBAccess.GetInt(reader, "move_from_host_id"),
				MoveFromHostUuid = DBAccess.GetString(reader, "move_from_host_uuid"),
				MoveFromHostName = DBAccess.GetString(reader, "move_from_host_name"),
				MoveToHostId = DBAccess.GetInt(reader, "move_to_host_id"),
				MoveToHostUuid = DBAccess.GetString(reader, "move_to_host_uuid"),
				MoveToHostName = DBAccess.GetString(reader, "move_to_host_name"),
				Reason = (ResourceToOptimize)DBAccess.GetInt(reader, "reason_id"),
				Status = (AuditEventStatus)DBAccess.GetInt(reader, "status"),
				StatusTimeStamp = DBAccess.GetDateTime(reader, "end_time")
			};
		}
		private string EncryptPwd(string password)
		{
			return Crypto.Encrypt(password);
		}
		private string DecryptPwd(string password)
		{
			if (string.IsNullOrEmpty(password))
			{
				return password;
			}
			return Crypto.Decrypt(password);
		}
	}
}
