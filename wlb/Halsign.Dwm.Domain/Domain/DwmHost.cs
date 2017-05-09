using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmHost : DwmBase
	{
		private int _numCpus = 1;
		private int _numVCpus;
		private int _cpuSpeed;
		private int _numNics = 1;
		private bool _isPoolMaster;
		private bool _enabled = true;
		private string _ipAddress;
		private bool _isEnterpriseOrHigher;
		private PowerStatus _powerState;
		private bool _participatesInPowerManagement;
		private bool _excludeFromPlacementRecommendations;
		private bool _excludeFromEvacuationRecommendations;
		private bool _excludeFromPoolOptimizationAcceptVMs;
		private long _memOverhead;
		private DateTime _metricsLastRetrieved = DateTime.MinValue;
		private DwmVirtualMachineCollection _listVMs;
		private DwmPifCollection _listPIFs;
		private DwmPbdCollection _listPBDs;
		private DwmStorageRepositoryCollection _availableStorage;
		private DwmHostAverageMetric _metrics;
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static Dictionary<string, int> _nameCache = new Dictionary<string, int>();
		private static Dictionary<string, string> _uuidNameCache = new Dictionary<string, string>();
		private static object _uuidCacheLock = new object();
		private static object _nameCacheLock = new object();
		private static object _uuidNameCacheLock = new object();
		private double _cpuScore;
		public int NumCpus
		{
			get
			{
				return this._numCpus;
			}
			set
			{
				this._numCpus = value;
			}
		}
		public int NumVCpus
		{
			get
			{
				return this._numVCpus;
			}
			set
			{
				this._numVCpus = value;
			}
		}
		public int CpuSpeed
		{
			get
			{
				return this._cpuSpeed;
			}
			set
			{
				this._cpuSpeed = value;
			}
		}
		public int NumNics
		{
			get
			{
				return this._numNics;
			}
			set
			{
				this._numNics = value;
			}
		}
		public string IPAddress
		{
			get
			{
				return this._ipAddress;
			}
			set
			{
				this._ipAddress = value;
			}
		}
		public bool IsPoolMaster
		{
			get
			{
				return this._isPoolMaster;
			}
			set
			{
				this._isPoolMaster = value;
			}
		}
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}
		public bool IsEnterpriseOrHigher
		{
			get
			{
				return this._isEnterpriseOrHigher;
			}
			set
			{
				this._isEnterpriseOrHigher = value;
			}
		}
		public PowerStatus PowerState
		{
			get
			{
				return this._powerState;
			}
			set
			{
				this._powerState = value;
			}
		}
		public long MemoryOverhead
		{
			get
			{
				return this._memOverhead;
			}
			set
			{
				this._memOverhead = value;
			}
		}
		internal bool ParticipatesInPowerManagement
		{
			get
			{
				return this._participatesInPowerManagement;
			}
			set
			{
				this._participatesInPowerManagement = value;
			}
		}
		internal bool ExcludeFromPlacementRecommendations
		{
			get
			{
				return this._excludeFromPlacementRecommendations;
			}
			set
			{
				this._excludeFromPlacementRecommendations = value;
			}
		}
		internal bool ExcludeFromEvacuationRecommendations
		{
			get
			{
				return this._excludeFromEvacuationRecommendations;
			}
			set
			{
				this._excludeFromEvacuationRecommendations = value;
			}
		}
		internal bool ExcludeFromPoolOptimizationAcceptVMs
		{
			get
			{
				return this._excludeFromPoolOptimizationAcceptVMs;
			}
			set
			{
				this._excludeFromPoolOptimizationAcceptVMs = value;
			}
		}
		public DwmVirtualMachineCollection VirtualMachines
		{
			get
			{
				return DwmBase.SafeGetItem<DwmVirtualMachineCollection>(ref this._listVMs);
			}
			internal set
			{
				this._listVMs = value;
			}
		}
		public DwmPifCollection PIFs
		{
			get
			{
				return DwmBase.SafeGetItem<DwmPifCollection>(ref this._listPIFs);
			}
		}
		public DwmPbdCollection PBDs
		{
			get
			{
				return DwmBase.SafeGetItem<DwmPbdCollection>(ref this._listPBDs);
			}
		}
		public DwmStorageRepositoryCollection AvailableStorage
		{
			get
			{
				return DwmBase.SafeGetItem<DwmStorageRepositoryCollection>(ref this._availableStorage);
			}
			internal set
			{
				this._availableStorage = value;
			}
		}
		public DwmHostAverageMetric Metrics
		{
			get
			{
				return DwmBase.SafeGetItem<DwmHostAverageMetric>(ref this._metrics);
			}
			internal set
			{
				this._metrics = value;
			}
		}
		internal double CpuScore
		{
			get
			{
				this._cpuScore = (double)(this.NumCpus * this.CpuSpeed);
				if (this.Metrics.MetricsNow != null)
				{
					this._cpuScore *= 1.0 - this.Metrics.MetricsNow.AverageCpuUtilization;
				}
				return this._cpuScore;
			}
		}
		public DateTime MetricsLastRetrieved
		{
			get
			{
				if (this._metricsLastRetrieved == DateTime.MinValue)
				{
					string sqlStatement = "get_host_last_metric_time";
					StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
					storedProcParamCollection.Add(new StoredProcParam("@host_id", base.Id));
					using (DBAccess dBAccess = new DBAccess())
					{
						this._metricsLastRetrieved = dBAccess.ExecuteScalarDateTime(sqlStatement, storedProcParamCollection);
					}
					if (this._metricsLastRetrieved == DateTime.MinValue)
					{
						this._metricsLastRetrieved = DateTime.UtcNow.AddMinutes(-1.0);
					}
				}
				return this._metricsLastRetrieved;
			}
			set
			{
				this._metricsLastRetrieved = value;
			}
		}
		public DwmHost(string uuid, string name, string poolUuid) : base(uuid, name)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmHost.UuidToId(uuid, base.PoolId);
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new DwmException("The uuid or name of the Physical Host must be specified.", DwmErrorCode.InvalidParameter, null);
				}
				base.Id = DwmHost.NameToId(name, base.PoolId);
			}
		}
		public DwmHost(string uuid, string name, int poolId) : base(uuid, name)
		{
			base.PoolId = poolId;
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmHost.UuidToId(uuid, base.PoolId);
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new DwmException("The uuid or name of the Physical Host must be specified.", DwmErrorCode.InvalidParameter, null);
				}
				base.Id = DwmHost.NameToId(name, base.PoolId);
			}
		}
		public DwmHost(int hostID) : base(hostID)
		{
		}
		internal static void RefreshCache()
		{
			object uuidCacheLock = DwmHost._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				DwmHost._uuidCache.Clear();
			}
			finally
			{
				Monitor.Exit(uuidCacheLock);
			}
			object nameCacheLock = DwmHost._nameCacheLock;
			Monitor.Enter(nameCacheLock);
			try
			{
				DwmHost._nameCache.Clear();
			}
			finally
			{
				Monitor.Exit(nameCacheLock);
			}
			object uuidNameCacheLock = DwmHost._uuidNameCacheLock;
			Monitor.Enter(uuidNameCacheLock);
			try
			{
				DwmHost._uuidNameCache.Clear();
			}
			finally
			{
				Monitor.Exit(uuidNameCacheLock);
			}
		}
		internal static int UuidToId(string uuid, int poolId)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(uuid))
			{
				string key = Localization.Format("{0}|{1}", uuid, poolId);
				if (!DwmHost._uuidCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from hv_host where uuid='{0}' and poolid={1}", uuid.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object uuidCacheLock = DwmHost._uuidCacheLock;
							Monitor.Enter(uuidCacheLock);
							try
							{
								if (!DwmHost._uuidCache.ContainsKey(key))
								{
									DwmHost._uuidCache.Add(key, num);
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
		public static int NameToId(string name, int poolId)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(name))
			{
				string key = Localization.Format("{0}|{1}", name, poolId);
				if (!DwmHost._nameCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from hv_host where name='{0}' and poolid={1}", name.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object nameCacheLock = DwmHost._nameCacheLock;
							Monitor.Enter(nameCacheLock);
							try
							{
								if (!DwmHost._nameCache.ContainsKey(key))
								{
									DwmHost._nameCache.Add(key, num);
								}
							}
							finally
							{
								Monitor.Exit(nameCacheLock);
							}
						}
					}
				}
			}
			return num;
		}
		public static string UuidToName(string uuid, int poolId)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(uuid))
			{
				string key = Localization.Format("{0}|{1}", uuid, poolId);
				if (!DwmHost._uuidNameCache.TryGetValue(key, out text))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						text = dBAccess.ExecuteScalarString(Localization.Format("select name from hv_host where uuid='{0}' and poolid={1}", uuid, poolId));
						if (string.IsNullOrEmpty(text))
						{
							object uuidNameCacheLock = DwmHost._uuidNameCacheLock;
							Monitor.Enter(uuidNameCacheLock);
							try
							{
								if (!DwmHost._uuidNameCache.ContainsKey(key))
								{
									DwmHost._uuidNameCache.Add(key, text);
								}
							}
							finally
							{
								Monitor.Exit(uuidNameCacheLock);
							}
						}
					}
				}
			}
			return text;
		}
		public DwmHost Copy()
		{
			return new DwmHost(base.Uuid, base.Name, base.PoolId)
			{
				Id = base.Id,
				CpuSpeed = this.CpuSpeed,
				Description = base.Description,
				IsPoolMaster = this.IsPoolMaster,
				Name = base.Name,
				NumCpus = this.NumCpus,
				NumVCpus = this.NumVCpus,
				NumNics = this.NumNics,
				ParticipatesInPowerManagement = this.ParticipatesInPowerManagement,
				ExcludeFromPlacementRecommendations = this.ExcludeFromPlacementRecommendations,
				ExcludeFromEvacuationRecommendations = this.ExcludeFromEvacuationRecommendations,
				ExcludeFromPoolOptimizationAcceptVMs = this.ExcludeFromPoolOptimizationAcceptVMs,
				PowerState = this.PowerState,
				MemoryOverhead = this.MemoryOverhead,
				Metrics = this.Metrics.Copy(),
				AvailableStorage = this.AvailableStorage.Copy(),
				VirtualMachines = this.VirtualMachines.Copy()
			};
		}
		internal static void SetOtherConfig(int hostId, string name, string value)
		{
			DwmHost dwmHost = new DwmHost(hostId);
			dwmHost.SetOtherConfig(name, value);
		}
		public void SetOtherConfig(string name, string value)
		{
			base.SetOtherConfig("hv_host_config_update", "@host_id", name, value);
			if (Localization.Compare(name, "ParticipatesInPowerManagement", true) == 0)
			{
				DwmPool.GenerateFillOrder(base.PoolId);
			}
		}
		public void SetOtherConfig(Dictionary<string, string> config)
		{
			base.SetOtherConfig("hv_host_config_update", "@host_id", config);
			if (config.ContainsKey("ParticipatesInPowerManagement"))
			{
				DwmPool.GenerateFillOrder(base.PoolId);
			}
		}
		public string GetOtherConfigItem(string itemName)
		{
			return base.GetOtherConfigItem("hv_host_config_get_item", "@host_id", itemName);
		}
		public Dictionary<string, string> GetOtherConfig()
		{
			return base.GetOtherConfig("hv_host_config_get", "@host_id");
		}
		public static void SetEnabled(string hostUuid, string poolUuid, bool enabled)
		{
			string sqlStatement = "hv_host_set_enabled";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@host_uuid", hostUuid));
			storedProcParamCollection.Add(new StoredProcParam("@pool_uuid", poolUuid));
			storedProcParamCollection.Add(new StoredProcParam("@enabled", enabled));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		internal static void SetStatus(int hostId, DwmStatus status)
		{
			string sqlStatement = "set_host_status";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@host_id", hostId));
			storedProcParamCollection.Add(new StoredProcParam("@status", (int)status));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		internal static void SetLastResult(int hostId, DwmStatus result)
		{
			string sqlStatement = "set_host_last_result";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@host_id", hostId));
			storedProcParamCollection.Add(new StoredProcParam("@last_result", (int)result));
			storedProcParamCollection.Add(new StoredProcParam("@last_result_time", DateTime.UtcNow));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		public static void SetPoweredOffByWlb(int hostId, bool poweredOffByWlb)
		{
			string sqlStatement = "set_host_powered_off_by_wlb";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@host_id", hostId));
			if (poweredOffByWlb)
			{
				storedProcParamCollection.Add(new StoredProcParam("@powered_off_by_wlb", 1));
			}
			else
			{
				storedProcParamCollection.Add(new StoredProcParam("@powered_off_by_wlb", 0));
			}
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		internal bool HasRequiredStorage(DwmStorageRepositoryCollection requiredSRs)
		{
			bool result = true;
			int num = 0;
			while (requiredSRs != null && num < requiredSRs.Count)
			{
				if (!this.AvailableStorage.ContainsKey(requiredSRs[num].Id))
				{
					result = false;
					break;
				}
				num++;
			}
			return result;
		}
		public static void DeleteHost(string hostUuid, string poolUuid)
		{
			if (!string.IsNullOrEmpty(hostUuid) && !string.IsNullOrEmpty(poolUuid))
			{
				Logger.Trace("Deleting host {0} by setting active to false", new object[]
				{
					hostUuid
				});
				int num = DwmPoolBase.UuidToId(poolUuid);
				int num2 = DwmHost.UuidToId(hostUuid, num);
				string sqlStatement = "delete_hv_host";
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("@pool_id", num));
				storedProcParamCollection.Add(new StoredProcParam("@host_id", num2));
				storedProcParamCollection.Add(new StoredProcParam("@tstamp", DateTime.UtcNow));
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
				}
			}
		}
		internal static DwmHost LoadWithMetrics(IDataReader reader)
		{
			string @string = DBAccess.GetString(reader, "uuid");
			string string2 = DBAccess.GetString(reader, "name");
			int @int = DBAccess.GetInt(reader, "poolid");
			return new DwmHost(@string, string2, @int)
			{
				Id = DBAccess.GetInt(reader, "id"),
				NumCpus = DBAccess.GetInt(reader, "num_cpus"),
				NumVCpus = DBAccess.GetInt(reader, "num_vcpus"),
				CpuSpeed = DBAccess.GetInt(reader, "cpu_speed"),
				NumNics = DBAccess.GetInt(reader, "num_pifs"),
				IsPoolMaster = DBAccess.GetBool(reader, "is_pool_master"),
				PowerState = (PowerStatus)DBAccess.GetInt(reader, "power_state"),
				ParticipatesInPowerManagement = DBAccess.GetBool(reader, "can_power"),
				ExcludeFromPlacementRecommendations = DBAccess.GetBool(reader, "exclude_placements"),
				ExcludeFromEvacuationRecommendations = DBAccess.GetBool(reader, "exclude_evacuations"),
				Metrics = 
				{
					FreeCPUs = DBAccess.GetInt(reader, "free_cpus"),
					PotentialFreeMemory = DBAccess.GetInt64(reader, "potential_free_memory"),
					FillOrder = DBAccess.GetInt(reader, "fill_order"),
					TotalMemory = DBAccess.GetInt64(reader, "total_mem"),
					FreeMemory = DBAccess.GetInt64(reader, "free_mem"),
					NumHighFullContentionVCpus = DBAccess.GetInt(reader, "full_contention_count"),
					NumHighConcurrencyHazardVCpus = DBAccess.GetInt(reader, "concurrency_hazard_count"),
					NumHighPartialContentionVCpus = DBAccess.GetInt(reader, "partial_contention_count"),
					NumHighFullrunVCpus = DBAccess.GetInt(reader, "fullrun_count"),
					NumHighPartialRunVCpus = DBAccess.GetInt(reader, "partial_run_count"),
					NumHighBlockedVCpus = DBAccess.GetInt(reader, "blocked_count"),
					MetricsNow = 
					{
						AverageFreeMemory = DBAccess.GetInt64(reader, "avg_free_mem_now", 0L),
						AverageCpuUtilization = DBAccess.GetDouble(reader, "avg_cpu_now", 0.0),
						AveragePifReadsPerSecond = DBAccess.GetDouble(reader, "avg_pif_read_now", 0.0),
						AveragePifWritesPerSecond = DBAccess.GetDouble(reader, "avg_pif_write_now", 0.0),
						AveragePbdReadsPerSecond = DBAccess.GetDouble(reader, "avg_pbd_read_now", 0.0),
						AveragePbdWritesPerSecond = DBAccess.GetDouble(reader, "avg_pbd_write_now", 0.0),
						AverageLoadAverage = DBAccess.GetDouble(reader, "avg_load_average_now", 0.0)
					},
					MetricsLast30Minutes = 
					{
						AverageFreeMemory = DBAccess.GetInt64(reader, "avg_free_mem_30", 0L),
						AverageCpuUtilization = DBAccess.GetDouble(reader, "avg_cpu_30", 0.0),
						AveragePifReadsPerSecond = DBAccess.GetDouble(reader, "avg_pif_read_30", 0.0),
						AveragePifWritesPerSecond = DBAccess.GetDouble(reader, "avg_pif_write_30", 0.0),
						AveragePbdReadsPerSecond = DBAccess.GetDouble(reader, "avg_pbd_read_30", 0.0),
						AveragePbdWritesPerSecond = DBAccess.GetDouble(reader, "avg_pbd_write_30", 0.0),
						AverageLoadAverage = DBAccess.GetDouble(reader, "avg_load_average_30", 0.0)
					},
					MetricsYesterday = 
					{
						AverageFreeMemory = DBAccess.GetInt64(reader, "avg_free_mem_yesterday", 0L),
						AverageCpuUtilization = DBAccess.GetDouble(reader, "avg_cpu_yesterday", 0.0),
						AveragePifReadsPerSecond = DBAccess.GetDouble(reader, "avg_pif_read_yesterday", 0.0),
						AveragePifWritesPerSecond = DBAccess.GetDouble(reader, "avg_pif_write_yesterday", 0.0),
						AveragePbdReadsPerSecond = DBAccess.GetDouble(reader, "avg_pbd_read_yesterday", 0.0),
						AveragePbdWritesPerSecond = DBAccess.GetDouble(reader, "avg_pbd_write_yesterday", 0.0),
						AverageLoadAverage = DBAccess.GetDouble(reader, "avg_load_average_yesterday", 0.0)
					}
				}
			};
		}
		internal void LoadPif(IDataReader reader)
		{
			int @int = DBAccess.GetInt(reader, "pif_id");
			string @string = DBAccess.GetString(reader, "pif_uuid");
			string string2 = DBAccess.GetString(reader, "pif_name");
			int int2 = DBAccess.GetInt(reader, "networkid");
			int int3 = DBAccess.GetInt(reader, "poolid");
			bool @bool = DBAccess.GetBool(reader, "is_management_interface");
			DwmPif dwmPif = new DwmPif(@string, string2, int2, int3);
			dwmPif.Id = @int;
			dwmPif.IsManagementInterface = @bool;
			this.PIFs.Add(dwmPif);
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
			if (db != null)
			{
				try
				{
					string sqlStatement = "add_update_hv_host";
					base.Id = db.ExecuteScalarInt(sqlStatement, new StoredProcParamCollection
					{
						new StoredProcParam("@uuid", base.Uuid),
						new StoredProcParam("@name", (base.Name == null) ? string.Empty : base.Name),
						new StoredProcParam("@pool_id", base.PoolId),
						new StoredProcParam("@description", (base.Description == null) ? string.Empty : base.Description),
						new StoredProcParam("@num_cpus", this._numCpus),
						new StoredProcParam("@cpu_speed", this._cpuSpeed),
						new StoredProcParam("@num_nics", this._numNics),
						new StoredProcParam("@is_pool_master", this._isPoolMaster),
						new StoredProcParam("@ip_address", this._ipAddress),
						new StoredProcParam("@memory_overhead", this._memOverhead),
						new StoredProcParam("@enabled", this._enabled),
						new StoredProcParam("@power_state", (int)this._powerState)
					});
					StringBuilder stringBuilder = new StringBuilder();
					string value = "BEGIN;\n";
					string value2 = "COMMIT;\n";
					stringBuilder.Append(value);
					stringBuilder.Append(this.SaveHostStorageRelationships());
					stringBuilder.Append(this.SaveHostVmRelationships());
					stringBuilder.Append(this.SaveHostPifRelationships());
					stringBuilder.Append(this.SaveHostPbdRelationships());
					stringBuilder.Append(value2);
					DwmBase.WriteData(db, stringBuilder);
				}
				catch (Exception ex)
				{
					Logger.Trace("Caught exception saving host {0} uuid={1}", new object[]
					{
						base.Name,
						base.Uuid
					});
					Logger.LogException(ex);
				}
				return;
			}
			throw new DwmException("Cannot pass null DBAccess instance to Save", DwmErrorCode.NullReference, null);
		}
		private StringBuilder SaveHostStorageRelationships()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._availableStorage != null && this._availableStorage.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < this._availableStorage.Count; i++)
				{
					stringBuilder.AppendFormat("insert into hv_host_storage_repository (host_id, sr_id) select {0}, {1}\nwhere not exists (select id from hv_host_storage_repository where host_id={0} and sr_id={1});\n", base.Id, this._availableStorage[i].Id);
					stringBuilder2.AppendFormat("{0}{1}", (i == 0) ? string.Empty : ",", this._availableStorage[i].Id);
				}
				stringBuilder.AppendFormat("delete from hv_host_storage_repository where host_id={0} and sr_id not in ({1});\n", base.Id, stringBuilder2.ToString());
			}
			else
			{
				stringBuilder.AppendFormat("delete from hv_host_storage_repository where host_id={0};\n", base.Id);
			}
			return stringBuilder;
		}
		private StringBuilder SaveHostVmRelationships()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._listVMs != null && this._listVMs.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < this._listVMs.Count; i++)
				{
					stringBuilder.AppendFormat("select * from add_update_host_vm( {0}, {1}, '{2}');\n", base.Id, this._listVMs[i].Id, Localization.DateTimeToSqlString(DateTime.UtcNow));
					stringBuilder2.AppendFormat("{0}{1}", (i == 0) ? string.Empty : ",", this._listVMs[i].Id);
				}
				stringBuilder.AppendFormat("delete from host_vm where hostid={0} and vmid not in ({1});\nupdate host_vm_history set end_time='{2}' \nwhere host_id={0}\n and end_time is null\n and vm_id not in ({1});\n", base.Id, stringBuilder2.ToString(), Localization.DateTimeToSqlString(DateTime.UtcNow));
			}
			else
			{
				stringBuilder.AppendFormat("delete from host_vm where hostid={0};\nupdate host_vm_history set end_time='{1}' \nwhere host_id={0} and end_time is null;\n", base.Id, Localization.DateTimeToSqlString(DateTime.UtcNow));
			}
			return stringBuilder;
		}
		private StringBuilder SaveHostPifRelationships()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._listPIFs != null && this._listPIFs.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < this._listPIFs.Count; i++)
				{
					stringBuilder.AppendFormat("insert into hv_host_pif (hostid, pif_id, tstamp) select {0}, {1},'{2}'\nwhere not exists (select id from hv_host_pif where hostid={0} and pif_id={1});\n", base.Id, this._listPIFs[i].Id, Localization.DateTimeToSqlString(DateTime.UtcNow));
					stringBuilder2.AppendFormat("{0}{1}", (i == 0) ? string.Empty : ",", this._listPIFs[i].Id);
				}
				stringBuilder.AppendFormat("delete from hv_host_pif where hostid={0} and pif_id not in ({1});\n", base.Id, stringBuilder2.ToString());
			}
			else
			{
				stringBuilder.AppendFormat("delete from hv_host_pif where hostid={0};\n", base.Id);
			}
			return stringBuilder;
		}
		private StringBuilder SaveHostPbdRelationships()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._listPBDs != null && this._listPBDs.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < this._listPBDs.Count; i++)
				{
					stringBuilder.AppendFormat("insert into hv_host_pbd (hostid, pbd_id, tstamp) select {0}, {1}, '{2}'\nwhere not exists (select id from hv_host_pbd where hostid={0} and pbd_id={1});\n", base.Id, this._listPBDs[i].Id, Localization.DateTimeToSqlString(DateTime.UtcNow));
					stringBuilder2.AppendFormat("{0}{1}", (i == 0) ? string.Empty : ",", this._listPBDs[i].Id);
				}
				stringBuilder.AppendFormat("delete from hv_host_pbd where hostid={0} and pbd_id not in ({1});\n", base.Id, stringBuilder2.ToString());
			}
			else
			{
				stringBuilder.AppendFormat("delete from hv_host_pbd where hostid={0};\n", base.Id);
			}
			return stringBuilder;
		}
		public void Load()
		{
			string sql = "load_host_by_id";
			this.InternalLoad(sql, new StoredProcParamCollection
			{
				new StoredProcParam("@host_id", base.Id)
			});
		}
		internal void SimpleLoad()
		{
			string sql = "load_host_simple_by_id";
			this.InternalLoad(sql, new StoredProcParamCollection
			{
				new StoredProcParam("@host_id", base.Id)
			});
		}
		private void InternalLoad(string sql, StoredProcParamCollection parms)
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader(sql, parms))
				{
					if (dataReader.Read())
					{
						if (string.IsNullOrEmpty(base.Uuid))
						{
							base.Uuid = DBAccess.GetString(dataReader, "uuid");
						}
						if (base.PoolId <= 0)
						{
							base.PoolId = DBAccess.GetInt(dataReader, "poolid");
						}
						base.Name = DBAccess.GetString(dataReader, "name");
						base.Description = DBAccess.GetString(dataReader, "description");
						this.NumCpus = DBAccess.GetInt(dataReader, "num_cpus");
						this.CpuSpeed = DBAccess.GetInt(dataReader, "cpu_speed");
						this.NumNics = DBAccess.GetInt(dataReader, "num_pifs");
						this.IsPoolMaster = DBAccess.GetBool(dataReader, "is_pool_master");
						this.Enabled = DBAccess.GetBool(dataReader, "enabled");
						this.Metrics.FillOrder = DBAccess.GetInt(dataReader, "fill_order");
						this.IPAddress = DBAccess.GetString(dataReader, "ip_address");
						this.MemoryOverhead = DBAccess.GetInt64(dataReader, "memory_overhead");
						base.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
						base.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
						base.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
						this.Metrics.TotalMemory = DBAccess.GetInt64(dataReader, "total_mem");
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								int @int = DBAccess.GetInt(dataReader, "hostid");
								int int2 = DBAccess.GetInt(dataReader, "vmid");
								string @string = DBAccess.GetString(dataReader, "name");
								string string2 = DBAccess.GetString(dataReader, "uuid");
								int int3 = DBAccess.GetInt(dataReader, "poolid");
								DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(string2, @string, int3);
								dwmVirtualMachine.Id = int2;
								dwmVirtualMachine.Description = DBAccess.GetString(dataReader, "description");
								dwmVirtualMachine.MinimumDynamicMemory = DBAccess.GetInt64(dataReader, "min_dynamic_memory");
								dwmVirtualMachine.MaximumDynamicMemory = DBAccess.GetInt64(dataReader, "max_dynamic_memory");
								dwmVirtualMachine.MinimumStaticMemory = DBAccess.GetInt64(dataReader, "min_static_memory");
								dwmVirtualMachine.MaximumStaticMemory = DBAccess.GetInt64(dataReader, "max_static_memory");
								dwmVirtualMachine.TargetMemory = DBAccess.GetInt64(dataReader, "target_memory");
								dwmVirtualMachine.MemoryOverhead = DBAccess.GetInt64(dataReader, "memory_overhead");
								dwmVirtualMachine.MinimumCpus = DBAccess.GetInt(dataReader, "min_cpus");
								dwmVirtualMachine.HvMemoryMultiplier = DBAccess.GetDouble(dataReader, "hv_memory_multiplier");
								dwmVirtualMachine.RequiredMemory = DBAccess.GetInt64(dataReader, "required_memory");
								dwmVirtualMachine.IsControlDomain = DBAccess.GetBool(dataReader, "is_control_domain");
								dwmVirtualMachine.IsAgile = DBAccess.GetBool(dataReader, "is_agile");
								dwmVirtualMachine.DriversUpToDate = DBAccess.GetBool(dataReader, "drivers_up_to_date");
								dwmVirtualMachine.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
								dwmVirtualMachine.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
								dwmVirtualMachine.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
								this.VirtualMachines.Add(dwmVirtualMachine);
							}
						}
					}
				}
			}
		}
		public static DwmHost Load(string hostUuid, string poolUuid)
		{
			int poolId = DwmPoolBase.UuidToId(poolUuid);
			int num = DwmHost.UuidToId(hostUuid, poolId);
			if (num <= 0)
			{
				throw new DwmException("Invalid Host uuid", DwmErrorCode.InvalidParameter, null);
			}
			return DwmHost.Load(num);
		}
		public static DwmHost Load(int hostId)
		{
			if (hostId > 0)
			{
				DwmHost dwmHost = new DwmHost(hostId);
				dwmHost.Load();
				return dwmHost;
			}
			throw new DwmException("Invalid host ID", DwmErrorCode.InvalidParameter, null);
		}
	}
}
