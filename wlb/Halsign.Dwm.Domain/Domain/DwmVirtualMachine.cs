using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
namespace Halsign.DWM.Domain
{
	public class DwmVirtualMachine : DwmBase
	{
		internal const long ONE_GIGA_BYTE = 1073741824L;
		private string _classification;
		private string _poolUuid;
		private string _poolName;
		private OptimizationMode _poolOptMode;
		private bool _isControlDomain;
		private bool _active;
		private bool _isAgile;
		private bool _driversUpToDate;
		private DwmHost _affinityHost;
		private string _affinityHostUuid;
		private int _affinityHostId;
		private int _runningOnHostId;
		private string _runningOnHostName;
		private string _runningOnHostUuid;
		private long _minimumStaticMemory;
		private long _maximumStaticMemory;
		private long _minimumDynamicMemory;
		private long _maximumDynamicMemory;
		private long _targetMemory;
		private long _memoryOverhead;
		private long _requiredMemory;
		private int _minimumCpus;
		private double _hvMemoryMultipler = 1.0;
		private DwmStorageRepositoryCollection _requiredStorage;
		private DwmVbdCollection _blockDevices;
		private DwmVifCollection _networkInterfaces;
		private DwmVmAverageMetric _metrics;
		private static Dictionary<string, int> _uuidCache = new Dictionary<string, int>();
		private static Dictionary<string, int> _nameCache = new Dictionary<string, int>();
		private static object _uuidCacheLock = new object();
		private static object _nameCacheLock = new object();
		public string Classification
		{
			get
			{
				return this._classification;
			}
			set
			{
				this._classification = value;
			}
		}
		public bool IsControlDomain
		{
			get
			{
				return this._isControlDomain;
			}
			set
			{
				this._isControlDomain = value;
			}
		}
		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}
		public DwmHost AffinityHost
		{
			get
			{
				return this._affinityHost;
			}
			set
			{
				this._affinityHost = value;
				if (value != null)
				{
					this._affinityHostId = this._affinityHost.Id;
					this._affinityHostUuid = this._affinityHost.Uuid;
				}
				else
				{
					this._affinityHostId = 0;
					this._affinityHostUuid = null;
				}
			}
		}
		public int AffinityHostId
		{
			get
			{
				return this._affinityHostId;
			}
			set
			{
				this._affinityHostId = value;
				if (this._affinityHost != null && this._affinityHost.Id != this._affinityHostId)
				{
					this._affinityHost = null;
				}
			}
		}
		public int RunningOnHostId
		{
			get
			{
				return this._runningOnHostId;
			}
			internal set
			{
				this._runningOnHostId = value;
			}
		}
		public string RunningOnHostName
		{
			get
			{
				return this._runningOnHostName;
			}
			internal set
			{
				this._runningOnHostName = value;
			}
		}
		public string RunningOnHostUuid
		{
			get
			{
				return this._runningOnHostUuid;
			}
			internal set
			{
				this._runningOnHostUuid = value;
			}
		}
		public long MinimumStaticMemory
		{
			get
			{
				return this._minimumStaticMemory;
			}
			set
			{
				this._minimumStaticMemory = value;
			}
		}
		public long MaximumStaticMemory
		{
			get
			{
				return this._maximumStaticMemory;
			}
			set
			{
				this._maximumStaticMemory = value;
			}
		}
		public long MinimumDynamicMemory
		{
			get
			{
				return this._minimumDynamicMemory;
			}
			set
			{
				this._minimumDynamicMemory = value;
			}
		}
		public long MaximumDynamicMemory
		{
			get
			{
				return this._maximumDynamicMemory;
			}
			set
			{
				this._maximumDynamicMemory = value;
			}
		}
		public long TargetMemory
		{
			get
			{
				return this._targetMemory;
			}
			set
			{
				this._targetMemory = value;
			}
		}
		public long MemoryOverhead
		{
			get
			{
				return this._memoryOverhead;
			}
			set
			{
				this._memoryOverhead = value;
			}
		}
		public long RequiredMemory
		{
			get
			{
				return this._requiredMemory;
			}
			internal set
			{
				this._requiredMemory = value;
			}
		}
		public int MinimumCpus
		{
			get
			{
				return this._minimumCpus;
			}
			set
			{
				this._minimumCpus = value;
			}
		}
		public double HvMemoryMultiplier
		{
			get
			{
				if (this._hvMemoryMultipler <= 0.0)
				{
				}
				return this._hvMemoryMultipler;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new DwmException("HvMemoryMultiplier cannot be less than or equal to 0", DwmErrorCode.InvalidParameter, null);
				}
				this._hvMemoryMultipler = value;
			}
		}
		public string PoolUuid
		{
			get
			{
				return this._poolUuid;
			}
			internal set
			{
				this._poolUuid = value;
			}
		}
		public string PoolName
		{
			get
			{
				return this._poolName;
			}
			internal set
			{
				this._poolName = value;
			}
		}
		public OptimizationMode PoolOptMode
		{
			get
			{
				return this._poolOptMode;
			}
			internal set
			{
				this._poolOptMode = value;
			}
		}
		public DwmHost OptimalHost
		{
			get
			{
				if (base.Id > 0)
				{
					return DwmAEVirtualMachine.GetOptimalHost(base.Id, 0);
				}
				throw new DwmException("The VM must be saved before the optimal host can be determined", DwmErrorCode.InvalidOperation, null);
			}
		}
		public bool IsAgile
		{
			get
			{
				return this._isAgile;
			}
			set
			{
				this._isAgile = value;
			}
		}
		public bool DriversUpToDate
		{
			get
			{
				return this._driversUpToDate;
			}
			set
			{
				this._driversUpToDate = value;
			}
		}
		public DwmStorageRepositoryCollection RequiredStorage
		{
			get
			{
				return DwmBase.SafeGetItem<DwmStorageRepositoryCollection>(ref this._requiredStorage);
			}
			internal set
			{
				this._requiredStorage = value;
			}
		}
		public DwmVbdCollection BlockDevices
		{
			get
			{
				return DwmBase.SafeGetItem<DwmVbdCollection>(ref this._blockDevices);
			}
		}
		public DwmVifCollection NetworkInterfaces
		{
			get
			{
				return DwmBase.SafeGetItem<DwmVifCollection>(ref this._networkInterfaces);
			}
		}
		public DwmVmAverageMetric Metrics
		{
			get
			{
				return DwmBase.SafeGetItem<DwmVmAverageMetric>(ref this._metrics);
			}
			internal set
			{
				this._metrics = value;
			}
		}
		public DwmVirtualMachine(string uuid, string name, int poolId) : base(uuid, name)
		{
			base.PoolId = poolId;
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmVirtualMachine.UuidToId(uuid, base.PoolId);
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new DwmException("The uuid or name of the Virtual Machine must be specified.", DwmErrorCode.InvalidParameter, null);
				}
				base.Id = DwmVirtualMachine.NameToId(name, base.PoolId);
			}
			this._active = true;
		}
		public DwmVirtualMachine(string uuid, string name, string poolUuid) : base(uuid, name)
		{
			base.PoolId = DwmBase.PoolUuidToId(poolUuid);
			if (!string.IsNullOrEmpty(uuid))
			{
				base.Id = DwmVirtualMachine.UuidToId(uuid, base.PoolId);
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new DwmException("The uuid or name of the Virtual Machine must be specified.", DwmErrorCode.InvalidParameter, null);
				}
				base.Id = DwmVirtualMachine.NameToId(name, base.PoolId);
			}
			this._active = true;
		}
		public DwmVirtualMachine(int vmId) : base(vmId)
		{
		}
		internal static void RefreshCache()
		{
			object uuidCacheLock = DwmVirtualMachine._uuidCacheLock;
			Monitor.Enter(uuidCacheLock);
			try
			{
				DwmVirtualMachine._uuidCache.Clear();
			}
			finally
			{
				Monitor.Exit(uuidCacheLock);
			}
			object nameCacheLock = DwmVirtualMachine._nameCacheLock;
			Monitor.Enter(nameCacheLock);
			try
			{
				DwmVirtualMachine._nameCache.Clear();
			}
			finally
			{
				Monitor.Exit(nameCacheLock);
			}
		}
		internal static int UuidToId(string uuid, int poolId)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(uuid))
			{
				string key = Localization.Format("{0}|{1}", uuid, poolId);
				if (!DwmVirtualMachine._uuidCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from virtual_machine where uuid='{0}' and poolid={1}", uuid.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object uuidCacheLock = DwmVirtualMachine._uuidCacheLock;
							Monitor.Enter(uuidCacheLock);
							try
							{
								if (!DwmVirtualMachine._uuidCache.ContainsKey(key))
								{
									DwmVirtualMachine._uuidCache.Add(key, num);
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
		internal static int NameToId(string name, int poolId)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(name))
			{
				string key = string.Format("{0}|{1}", name, poolId);
				if (!DwmVirtualMachine._nameCache.TryGetValue(key, out num))
				{
					using (DBAccess dBAccess = new DBAccess())
					{
						num = dBAccess.ExecuteScalarInt(Localization.Format("select id from virtual_machine where name='{0}' and poolid={1}", name.Replace("'", "''"), poolId));
						if (num != 0)
						{
							object nameCacheLock = DwmVirtualMachine._nameCacheLock;
							Monitor.Enter(nameCacheLock);
							try
							{
								if (!DwmVirtualMachine._nameCache.ContainsKey(key))
								{
									DwmVirtualMachine._nameCache.Add(key, num);
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
		public DwmVirtualMachine Copy()
		{
			return new DwmVirtualMachine(base.Uuid, base.Name, base.PoolId)
			{
				AffinityHostId = this.AffinityHostId,
				Classification = this.Classification,
				Description = base.Description,
				Id = base.Id,
				IsControlDomain = this.IsControlDomain,
				IsAgile = this.IsAgile,
				DriversUpToDate = this.DriversUpToDate,
				MinimumCpus = this.MinimumCpus,
				MinimumDynamicMemory = this.MinimumDynamicMemory,
				MaximumDynamicMemory = this.MaximumDynamicMemory,
				MinimumStaticMemory = this.MinimumStaticMemory,
				MaximumStaticMemory = this.MaximumStaticMemory,
				TargetMemory = this.TargetMemory,
				RequiredMemory = this.RequiredMemory,
				MemoryOverhead = this.MemoryOverhead,
				PoolId = base.PoolId,
				RequiredStorage = this.RequiredStorage.Copy(),
				Metrics = this.Metrics.Copy()
			};
		}
		public void Load()
		{
			string sqlStatement = "load_vm_by_id";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@vm_id", base.Id));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.UseTransaction = true;
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						int @int = DBAccess.GetInt(dataReader, "hostid");
						int int2 = DBAccess.GetInt(dataReader, "id");
						string @string = DBAccess.GetString(dataReader, "name");
						string string2 = DBAccess.GetString(dataReader, "uuid");
						if (string.IsNullOrEmpty(base.Uuid))
						{
							base.Uuid = string2;
						}
						if (string.IsNullOrEmpty(base.Name))
						{
							base.Name = @string;
						}
						this.RunningOnHostId = @int;
						base.Description = DBAccess.GetString(dataReader, "description");
						this.MinimumDynamicMemory = DBAccess.GetInt64(dataReader, "min_dynamic_memory");
						this.MaximumDynamicMemory = DBAccess.GetInt64(dataReader, "max_dynamic_memory");
						this.MinimumStaticMemory = DBAccess.GetInt64(dataReader, "min_static_memory");
						this.MaximumStaticMemory = DBAccess.GetInt64(dataReader, "max_static_memory");
						this.TargetMemory = DBAccess.GetInt64(dataReader, "target_memory");
						this.MemoryOverhead = DBAccess.GetInt64(dataReader, "memory_overhead");
						this.MinimumCpus = DBAccess.GetInt(dataReader, "min_cpus");
						this.HvMemoryMultiplier = DBAccess.GetDouble(dataReader, "hv_memory_multiplier");
						this.RequiredMemory = DBAccess.GetInt64(dataReader, "required_memory");
						this.IsControlDomain = DBAccess.GetBool(dataReader, "is_control_domain");
						this.IsAgile = DBAccess.GetBool(dataReader, "is_agile");
						this.DriversUpToDate = DBAccess.GetBool(dataReader, "drivers_up_to_date");
						this.AffinityHostId = DBAccess.GetInt(dataReader, "host_affinity");
						this.Active = DBAccess.GetBool(dataReader, "active");
						base.Status = (DwmStatus)DBAccess.GetInt(dataReader, "status");
						base.LastResult = (DwmStatus)DBAccess.GetInt(dataReader, "last_result");
						base.LastResultTime = DBAccess.GetDateTime(dataReader, "last_result_time");
						if (dataReader.NextResult() && dataReader.Read())
						{
							base.PoolId = DBAccess.GetInt(dataReader, "pool_id");
							this.PoolUuid = DBAccess.GetString(dataReader, "pool_uuid");
							this.PoolName = DBAccess.GetString(dataReader, "pool_name");
							this.PoolOptMode = (OptimizationMode)DBAccess.GetInt(dataReader, "pool_opt_mode");
						}
						if (dataReader.NextResult())
						{
							while (dataReader.Read())
							{
								DwmStorageRepository item = new DwmStorageRepository(DBAccess.GetString(dataReader, "uuid"), DBAccess.GetString(dataReader, "name"), DBAccess.GetInt(dataReader, "poolid"), DBAccess.GetInt64(dataReader, "size"), DBAccess.GetInt64(dataReader, "used"), DBAccess.GetBool(dataReader, "pool_default_sr"));
								this.RequiredStorage.Add(item);
							}
						}
					}
				}
			}
		}
		public static DwmVirtualMachine Load(string vmUuid, string poolUuid)
		{
			int poolId = DwmPoolBase.UuidToId(poolUuid);
			int num = DwmVirtualMachine.UuidToId(vmUuid, poolId);
			if (num <= 0)
			{
				throw new DwmException("Invalid VM uuid", DwmErrorCode.InvalidParameter, null);
			}
			return DwmVirtualMachine.Load(num);
		}
		public static DwmVirtualMachine Load(int vmId)
		{
			if (vmId > 0)
			{
				DwmVirtualMachine dwmVirtualMachine = new DwmVirtualMachine(vmId);
				dwmVirtualMachine.Load();
				return dwmVirtualMachine;
			}
			throw new DwmException("Invalid VM ID", DwmErrorCode.InvalidParameter, null);
		}
		internal static DwmVirtualMachine LoadWithMetrics(IDataReader reader)
		{
			string @string = DBAccess.GetString(reader, "uuid");
			string string2 = DBAccess.GetString(reader, "name");
			int @int = DBAccess.GetInt(reader, "poolid");
			return new DwmVirtualMachine(@string, string2, @int)
			{
				Id = DBAccess.GetInt(reader, "id"),
				MinimumCpus = DBAccess.GetInt(reader, "min_cpus"),
				MinimumDynamicMemory = DBAccess.GetInt64(reader, "min_dynamic_memory"),
				MaximumDynamicMemory = DBAccess.GetInt64(reader, "max_dynamic_memory"),
				MinimumStaticMemory = DBAccess.GetInt64(reader, "min_static_memory"),
				MaximumStaticMemory = DBAccess.GetInt64(reader, "max_static_memory"),
				TargetMemory = DBAccess.GetInt64(reader, "target_memory"),
				MemoryOverhead = DBAccess.GetInt64(reader, "memory_overhead"),
				HvMemoryMultiplier = DBAccess.GetDouble(reader, "hv_memory_multiplier"),
				RequiredMemory = DBAccess.GetInt64(reader, "required_memory"),
				IsAgile = DBAccess.GetBool(reader, "is_agile"),
				DriversUpToDate = DBAccess.GetBool(reader, "drivers_up_to_date"),
				Metrics = 
				{
					TotalMemory = DBAccess.GetInt64(reader, "total_memory"),
					HaveCurrentMetrics = DBAccess.GetInt(reader, "have_current_vm_metrics") > 0,
					HaveLast30Metrics = DBAccess.GetInt(reader, "have_last_30_vm_metrics") > 0,
					HaveYesterdayMetrics = DBAccess.GetInt(reader, "have_yesterday_vm_metrics") > 0,
					MetricsNow = 
					{
						AverageUsedMemory = DBAccess.GetInt64(reader, "avg_used_mem_now", 0L),
						AverageTotalMemory = DBAccess.GetInt64(reader, "avg_total_mem_now", 0L),
						AverageFreeMemory = DBAccess.GetInt64(reader, "avg_free_mem_now", 0L),
						AverageTargetMemory = DBAccess.GetInt64(reader, "avg_target_mem_now", 0L),
						AverageCpuUtilization = DBAccess.GetDouble(reader, "avg_cpu_now", 0.0),
						AveragePifReadsPerSecond = DBAccess.GetDouble(reader, "avg_net_read_now", 0.0),
						AveragePifWritesPerSecond = DBAccess.GetDouble(reader, "avg_net_write_now", 0.0),
						AveragePbdReadsPerSecond = DBAccess.GetDouble(reader, "avg_block_read_now", 0.0),
						AveragePbdWritesPerSecond = DBAccess.GetDouble(reader, "avg_block_write_now", 0.0),
						TotalVbdNetReadsPerSecond = DBAccess.GetDouble(reader, "total_vbd_net_read", 0.0),
						TotalVbdNetWritesPerSecond = DBAccess.GetDouble(reader, "total_vbd_net_write", 0.0),
						AverageRunstateFullContention = DBAccess.GetDouble(reader, "avg_runstate_full_contention_now", 0.0),
						AverageRunstateConcurrencyHazard = DBAccess.GetDouble(reader, "avg_runstate_concurrency_hazard_now", 0.0),
						AverageRunstatePartialContention = DBAccess.GetDouble(reader, "avg_runstate_partial_contention_now", 0.0),
						AverageRunstateFullRun = DBAccess.GetDouble(reader, "avg_runstate_fullrun_now", 0.0),
						AverageRunstatePartialRun = DBAccess.GetDouble(reader, "avg_runstate_partial_run_now", 0.0),
						AverageRunstateBlocked = DBAccess.GetDouble(reader, "avg_runstate_blocked_now", 0.0)
					},
					MetricsLast30Minutes = 
					{
						AverageUsedMemory = DBAccess.GetInt64(reader, "avg_used_mem_30", 0L),
						AverageTotalMemory = DBAccess.GetInt64(reader, "avg_total_mem_30", 0L),
						AverageFreeMemory = DBAccess.GetInt64(reader, "avg_free_mem_30", 0L),
						AverageTargetMemory = DBAccess.GetInt64(reader, "avg_target_mem_30", 0L),
						AverageCpuUtilization = DBAccess.GetDouble(reader, "avg_cpu_30", 0.0),
						AveragePifReadsPerSecond = DBAccess.GetDouble(reader, "avg_net_read_30", 0.0),
						AveragePifWritesPerSecond = DBAccess.GetDouble(reader, "avg_net_write_30", 0.0),
						AveragePbdReadsPerSecond = DBAccess.GetDouble(reader, "avg_block_read_30", 0.0),
						AveragePbdWritesPerSecond = DBAccess.GetDouble(reader, "avg_block_write_30", 0.0),
						TotalVbdNetReadsPerSecond = DBAccess.GetDouble(reader, "total_vbd_net_read_30", 0.0),
						TotalVbdNetWritesPerSecond = DBAccess.GetDouble(reader, "total_vbd_net_write_30", 0.0),
						AverageRunstateFullContention = DBAccess.GetDouble(reader, "avg_runstate_full_contention_30", 0.0),
						AverageRunstateConcurrencyHazard = DBAccess.GetDouble(reader, "avg_runstate_concurrency_hazard_30", 0.0),
						AverageRunstatePartialContention = DBAccess.GetDouble(reader, "avg_runstate_partial_contention_30", 0.0),
						AverageRunstateFullRun = DBAccess.GetDouble(reader, "avg_runstate_fullrun_30", 0.0),
						AverageRunstatePartialRun = DBAccess.GetDouble(reader, "avg_runstate_partial_run_30", 0.0),
						AverageRunstateBlocked = DBAccess.GetDouble(reader, "avg_runstate_blocked_30", 0.0)
					},
					MetricsYesterday = 
					{
						AverageUsedMemory = DBAccess.GetInt64(reader, "avg_used_mem_yesterday", 0L),
						AverageTotalMemory = DBAccess.GetInt64(reader, "avg_total_mem_yesterday", 0L),
						AverageFreeMemory = DBAccess.GetInt64(reader, "avg_free_mem_yesterday", 0L),
						AverageTargetMemory = DBAccess.GetInt64(reader, "avg_target_mem_yesterday", 0L),
						AverageCpuUtilization = DBAccess.GetDouble(reader, "avg_cpu_yesterday", 0.0),
						AveragePifReadsPerSecond = DBAccess.GetDouble(reader, "avg_net_read_yesterday", 0.0),
						AveragePifWritesPerSecond = DBAccess.GetDouble(reader, "avg_net_write_yesterday", 0.0),
						AveragePbdReadsPerSecond = DBAccess.GetDouble(reader, "avg_block_read_yesterday", 0.0),
						AveragePbdWritesPerSecond = DBAccess.GetDouble(reader, "avg_block_write_yesterday", 0.0),
						TotalVbdNetReadsPerSecond = DBAccess.GetDouble(reader, "total_vbd_net_read_yesterday", 0.0),
						TotalVbdNetWritesPerSecond = DBAccess.GetDouble(reader, "total_vbd_net_write_yesterday", 0.0),
						AverageRunstateFullContention = DBAccess.GetDouble(reader, "avg_runstate_full_contention_yesterday", 0.0),
						AverageRunstateConcurrencyHazard = DBAccess.GetDouble(reader, "avg_runstate_concurrency_hazard_yesterday", 0.0),
						AverageRunstatePartialContention = DBAccess.GetDouble(reader, "avg_runstate_partial_contention_yesterday", 0.0),
						AverageRunstateFullRun = DBAccess.GetDouble(reader, "avg_runstate_fullrun_yesterday", 0.0),
						AverageRunstatePartialRun = DBAccess.GetDouble(reader, "avg_runstate_partial_run_yesterday", 0.0),
						AverageRunstateBlocked = DBAccess.GetDouble(reader, "avg_runstate_blocked_yesterday", 0.0)
					}
				}
			};
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
			if (db == null)
			{
				throw new DwmException("Cannot pass null DBAccess instance to Save", DwmErrorCode.NullReference, null);
			}
			string sqlStatement = "add_update_virtual_machine";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@uuid", (base.Uuid == null) ? string.Empty : base.Uuid));
			storedProcParamCollection.Add(new StoredProcParam("@name", (base.Name == null) ? string.Empty : base.Name));
			storedProcParamCollection.Add(new StoredProcParam("@description", (base.Description == null) ? string.Empty : base.Description));
			storedProcParamCollection.Add(new StoredProcParam("@host_affinity", this._affinityHostId));
			storedProcParamCollection.Add(new StoredProcParam("@min_dynamic_memory", this._minimumDynamicMemory));
			storedProcParamCollection.Add(new StoredProcParam("@max_dynamic_memory", this._maximumDynamicMemory));
			storedProcParamCollection.Add(new StoredProcParam("@min_static_memory", this._minimumStaticMemory));
			storedProcParamCollection.Add(new StoredProcParam("@max_static_memory", this._maximumStaticMemory));
			storedProcParamCollection.Add(new StoredProcParam("@target_memory", this._targetMemory));
			storedProcParamCollection.Add(new StoredProcParam("@memory_overhead", this._memoryOverhead));
			storedProcParamCollection.Add(new StoredProcParam("@hv_mem_multiplier", this._hvMemoryMultipler));
			storedProcParamCollection.Add(new StoredProcParam("@min_cpus", this._minimumCpus));
			storedProcParamCollection.Add(new StoredProcParam("@poolid", base.PoolId));
			storedProcParamCollection.Add(new StoredProcParam("@is_control_domain", this._isControlDomain));
			storedProcParamCollection.Add(new StoredProcParam("@is_agile", this._isAgile));
			storedProcParamCollection.Add(new StoredProcParam("@drivers_up_to_date", this._driversUpToDate));
			storedProcParamCollection.Add(new StoredProcParam("@active", this._active));
			try
			{
				base.Id = db.ExecuteScalarInt(sqlStatement, storedProcParamCollection);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("BEGIN;\n");
				stringBuilder.Append(this.SaveVmStorageRelationships());
				stringBuilder.Append(this.SaveVmVbdRelationships());
				stringBuilder.Append(this.SaveVmVifRelationships());
				stringBuilder.Append("COMMIT;\n");
				DwmBase.WriteData(db, stringBuilder);
			}
			catch (Exception)
			{
			}
		}
		private StringBuilder SaveVmStorageRelationships()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._requiredStorage != null && this._requiredStorage.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < this._requiredStorage.Count; i++)
				{
					stringBuilder.AppendFormat("select * from add_update_vm_storage_repository ({0}, {1});\n", base.Id, this._requiredStorage[i].Id);
					stringBuilder2.AppendFormat("{0}{1}", (i == 0) ? string.Empty : ",", this._requiredStorage[i].Id);
				}
				stringBuilder.AppendFormat("delete from vm_storage_repository where vm_id={0} and sr_id not in ({1});\n", base.Id, stringBuilder2.ToString());
			}
			else
			{
				stringBuilder.AppendFormat("delete from vm_storage_repository where vm_id={0};\n", base.Id);
			}
			return stringBuilder;
		}
		private StringBuilder SaveVmVbdRelationships()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._blockDevices != null && this._blockDevices.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < this._blockDevices.Count; i++)
				{
					stringBuilder.AppendFormat("select * from add_update_vm_vbd ({0}, {1});\n", base.Id, this._blockDevices[i].Id);
					stringBuilder2.AppendFormat("{0}{1}", (i == 0) ? string.Empty : ",", this._blockDevices[i].Id);
				}
				stringBuilder.AppendFormat("delete from vm_vbd where vm_id={0} and vbd_id not in ({1});\n", base.Id, stringBuilder2.ToString());
			}
			else
			{
				stringBuilder.AppendFormat("delete from vm_vbd where vm_id={0};\n", base.Id);
			}
			return stringBuilder;
		}
		private StringBuilder SaveVmVifRelationships()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._networkInterfaces != null && this._networkInterfaces.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < this._networkInterfaces.Count; i++)
				{
					stringBuilder.AppendFormat("select * from add_update_vm_vif ({0}, {1});\n", base.Id, this._networkInterfaces[i].Id);
					stringBuilder2.AppendFormat("{0}{1}", (i == 0) ? string.Empty : ",", this._networkInterfaces[i].Id);
				}
				stringBuilder.AppendFormat("delete from vm_vif where vm_id={0} and vif_id not in ({1});\n", base.Id, stringBuilder2.ToString());
			}
			else
			{
				stringBuilder.AppendFormat("delete from vm_vif where vm_id={0};\n", base.Id);
			}
			return stringBuilder;
		}
		public static void DeleteVM(string vmUuid, string poolUuid)
		{
			if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(poolUuid))
			{
				Logger.Trace("Deleting VM {0} by setting active to false", new object[]
				{
					vmUuid
				});
				int poolId = DwmPoolBase.UuidToId(poolUuid);
				int num = DwmVirtualMachine.UuidToId(vmUuid, poolId);
				string sqlStatement = "delete_virtual_machine";
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("@vm_id", num));
				storedProcParamCollection.Add(new StoredProcParam("@tstamp", DateTime.UtcNow));
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
				}
			}
		}
		internal static void SetStatus(int vmId, DwmStatus status)
		{
			string sqlStatement = "set_vm_status";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@vm_id", vmId));
			storedProcParamCollection.Add(new StoredProcParam("@status", (int)status));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		internal static void SetLastResult(int vmId, DwmStatus result)
		{
			string sqlStatement = "set_vm_last_result";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@vm_id", vmId));
			storedProcParamCollection.Add(new StoredProcParam("@last_result", (int)result));
			storedProcParamCollection.Add(new StoredProcParam("@last_result_time", DateTime.UtcNow));
			using (DBAccess dBAccess = new DBAccess())
			{
				dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
			}
		}
		public static void SetRunningOnHost(string vmUuid, string hostUuid, string poolUuid, int recommendationId)
		{
			if (!string.IsNullOrEmpty(vmUuid) && !string.IsNullOrEmpty(poolUuid))
			{
				Logger.Trace("VM {0} now running on host {1}.  RecommendationId {2}", new object[]
				{
					vmUuid,
					(hostUuid == null) ? "null" : hostUuid,
					recommendationId
				});
				bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.IgnoreRecId);
				int poolId = DwmPoolBase.UuidToId(poolUuid);
				int num = DwmVirtualMachine.UuidToId(vmUuid, poolId);
				int num2 = string.IsNullOrEmpty(hostUuid) ? 0 : DwmHost.UuidToId(hostUuid, poolId);
				string sqlStatement = "add_update_host_vm";
				StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
				storedProcParamCollection.Add(new StoredProcParam("@hostid", num2));
				storedProcParamCollection.Add(new StoredProcParam("@vmid", num));
				storedProcParamCollection.Add(new StoredProcParam("@tstamp", DateTime.UtcNow));
				if (recommendationId > 0 && !valueAsBool)
				{
					storedProcParamCollection.Add(new StoredProcParam("@rec_id", recommendationId));
				}
				using (DBAccess dBAccess = new DBAccess())
				{
					dBAccess.ExecuteNonQuery(sqlStatement, storedProcParamCollection);
				}
				DwmPool.GenerateFillOrder(poolId);
			}
		}
	}
}
