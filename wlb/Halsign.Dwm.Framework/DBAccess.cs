using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Timers;
namespace Halsign.DWM.Framework
{
	public class DBAccess : IDisposable
	{
		public class NpgsqlErrors
		{
			public static string LOCK_NOT_AVAILABLE = "55P03";
			public static string DEADLOCK_DETECTED = "40P01";
			public static string TRANSACTION_ROLLBACK = "40000";
			public static string UNDEFINED_FUNCTION = "42883";
			public static string QUERY_CANCELED = "57014";
		}
		public static class Resources
		{
			public static long vm_metric_history = 1L;
			public static long vm_cpu_metric_history = 2L;
			public static long vm_vif_metric_history = 3L;
			public static long vm_vbd_metric_history = 4L;
			public static long host_metric_history = 5L;
			public static long host_cpu_metric_history = 6L;
			public static long host_pif_metric_history = 7L;
			public static long host_pbd_metric_history = 8L;
			public static long host_vm_history = 9L;
			public static long hv_pool_threshold_history = 10L;
			public static long consolidation_intervals = 11L;
			public static long hv_audit_log = 12L;
			public static long move_recommendation = 13L;
			public static long move_recommendation_detail = 14L;
			public static long generic = 15L;
			public static long GetResourceId(string resourceName)
			{
				string text = resourceName.ToLower();
				switch (text)
				{
				case "vm_metric_history":
					return DBAccess.Resources.vm_metric_history;
				case "vm_cpu_metric_history":
					return DBAccess.Resources.vm_cpu_metric_history;
				case "vm_vif_metric_history":
					return DBAccess.Resources.vm_vif_metric_history;
				case "vm_vbd_metric_history":
					return DBAccess.Resources.vm_vbd_metric_history;
				case "host_metric_history":
					return DBAccess.Resources.host_metric_history;
				case "host_cpu_metric_history":
					return DBAccess.Resources.host_cpu_metric_history;
				case "host_pif_metric_history":
					return DBAccess.Resources.host_pif_metric_history;
				case "host_pbd_metric_history":
					return DBAccess.Resources.host_pbd_metric_history;
				case "host_vm_history":
					return DBAccess.Resources.host_vm_history;
				case "hv_pool_threshold_history":
					return DBAccess.Resources.hv_pool_threshold_history;
				case "consolidation_intervals":
					return DBAccess.Resources.consolidation_intervals;
				case "hv_audit_log":
					return DBAccess.Resources.hv_audit_log;
				case "move_recommendation":
					return DBAccess.Resources.move_recommendation;
				case "move_recommendation_detail":
					return DBAccess.Resources.move_recommendation_detail;
				}
				return 0L;
			}
		}
		public enum LockMode
		{
			Exclusive,
			Shared
		}
		private enum ExecuteType
		{
			ExecuteScaler,
			ExecuteReader,
			ExecuteNonQuery
		}
		private static string _dbServer;
		private static string _dbPort;
		private static string _dbName;
		private static string _dbUsername;
		private static string _dbPassword;
		private static string _dbConnTimeout;
		private static string _dbUseConnPooling;
		private static string _dbMinPoolSize;
		private static string _dbMaxPoolSize;
		private static object _dbServerLock = new object();
		private string _szConnectString;
		private int _nTimeout;
		private NpgsqlConnection _conn;
		private NpgsqlCommand _command;
		private bool _useTxn;
		private NpgsqlTransaction _tran;
		private System.Timers.Timer _timeoutTimer;
		private volatile bool _commandFinished;
		private int _cmdTimeoutConfig;
		private List<long> _exclusiveAdvisoryLocks = new List<long>();
		private List<long> _sharedAdvisoryLocks = new List<long>();
		private int _traceEnabled;
		private static object _traceCountLock = new object();
		private static int _traceCount = 0;
		private bool _retryExecute = true;
		public string ConnectString
		{
			set
			{
				this._szConnectString = value;
			}
		}
		[DefaultValue(20)]
		public int Timeout
		{
			get
			{
				return this._nTimeout;
			}
			set
			{
				this._nTimeout = value;
				if (this._timeoutTimer != null && value > 0)
				{
					this._timeoutTimer.Stop();
					this._timeoutTimer.Interval = (double)(value * 1000);
				}
				else
				{
					if (this._timeoutTimer == null && value > 0)
					{
						this._timeoutTimer = new System.Timers.Timer();
						this._timeoutTimer.Interval = (double)(value * 1000);
						this.InitializeTimer();
					}
					else
					{
						if (value != 0)
						{
							throw new ArgumentOutOfRangeException("Timeout", "Timeout cannot be less than 0.");
						}
						this._timeoutTimer = null;
					}
				}
			}
		}
		public bool UseTransaction
		{
			get
			{
				return this._useTxn;
			}
			set
			{
				this._useTxn = value;
			}
		}
		public static int RetryMaxAttempts
		{
			get
			{
				int valueAsInt = Configuration.GetValueAsInt(ConfigItems.DBRetryAttempts);
				return (valueAsInt == 0) ? 3 : valueAsInt;
			}
		}
		public static int RetrySleepInterval
		{
			get
			{
				int valueAsInt = Configuration.GetValueAsInt(ConfigItems.DBRetryDelay);
				return (valueAsInt == 0) ? 15000 : (valueAsInt * 1000);
			}
		}
		public int AdvisoryLocksCount
		{
			get
			{
				return this._exclusiveAdvisoryLocks.Count + this._sharedAdvisoryLocks.Count;
			}
		}
		private bool CanConnectToDb
		{
			get
			{
				NpgsqlConnection npgsqlConnection = null;
				try
				{
					npgsqlConnection = new NpgsqlConnection(this.GetConnectString());
					npgsqlConnection.Open();
				}
				catch
				{
					return false;
				}
				finally
				{
					if (npgsqlConnection != null)
					{
						npgsqlConnection.Close();
					}
				}
				return true;
			}
		}
		private int NextTraceCount
		{
			get
			{
				this._traceEnabled = Configuration.GetValueAsInt(ConfigItems.DatabaseTrace);
				object traceCountLock = DBAccess._traceCountLock;
				Monitor.Enter(traceCountLock);
				int result;
				try
				{
					if (DBAccess._traceCount > 1000000)
					{
						DBAccess._traceCount = 0;
					}
					result = ++DBAccess._traceCount;
				}
				finally
				{
					Monitor.Exit(traceCountLock);
				}
				return result;
			}
		}
		public DBAccess()
		{
			this._cmdTimeoutConfig = Configuration.GetValueAsInt(ConfigItems.DBCmdTimeout);
			if (this._cmdTimeoutConfig > 0)
			{
				this._timeoutTimer = new System.Timers.Timer();
				this.Timeout = this._cmdTimeoutConfig;
			}
			this.InitializeTimer();
		}
		private void InitializeTimer()
		{
			if (this._timeoutTimer != null)
			{
				this._timeoutTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
				{
					if (this._command != null && !this._commandFinished)
					{
						this._timeoutTimer.Stop();
						this._command.Cancel();
					}
				};
			}
		}
		private void StartTimeoutTimer()
		{
			if (this._timeoutTimer != null)
			{
				this._timeoutTimer.Stop();
				this._timeoutTimer.Start();
				this._commandFinished = false;
			}
		}
		private void StopTimeoutTimer()
		{
			if (this._timeoutTimer != null)
			{
				this._timeoutTimer.Stop();
			}
		}
		private string GetConnectString()
		{
			if (this._szConnectString != null)
			{
				return this._szConnectString;
			}
			if (DBAccess._dbServer == null)
			{
				object dbServerLock = DBAccess._dbServerLock;
				Monitor.Enter(dbServerLock);
				try
				{
					DBAccess._dbServer = Configuration.GetValueAsString(ConfigItems.DBServer);
					DBAccess._dbName = Configuration.GetValueAsString(ConfigItems.DBName);
					DBAccess._dbPort = Configuration.GetValueAsString(ConfigItems.DBPort);
					DBAccess._dbUsername = Configuration.GetValueAsString(ConfigItems.DBUsername);
					DBAccess._dbPassword = Configuration.GetValueAsString(ConfigItems.DBPassword);
					DBAccess._dbConnTimeout = Configuration.GetValueAsString(ConfigItems.DBConnTimeout);
					DBAccess._dbUseConnPooling = ((!Configuration.GetValueAsBool(ConfigItems.DBUseConnPooling)) ? "False" : "True");
					DBAccess._dbMinPoolSize = Configuration.GetValueAsString(ConfigItems.DBMinPoolSize);
					DBAccess._dbMaxPoolSize = Configuration.GetValueAsString(ConfigItems.DBMaxPoolSize);
					try
					{
						DBAccess._dbPassword = Crypto.Decrypt(Configuration.GetValueAsString(ConfigItems.DBPassword));
					}
					catch
					{
						Configuration.SetValue(ConfigItems.DBPassword, Crypto.Encrypt(DBAccess._dbPassword));
					}
					Logger.Trace("Database:  host is {0}.  DB is {1}", new object[]
					{
						DBAccess._dbServer,
						DBAccess._dbName
					});
				}
				finally
				{
					Monitor.Exit(dbServerLock);
				}
			}
			return Localization.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};Timeout={5};Pooling={6};MinPoolSize={7};MaxPoolSize={8}", new object[]
			{
				DBAccess._dbServer,
				DBAccess._dbPort,
				DBAccess._dbUsername,
				DBAccess._dbPassword,
				DBAccess._dbName,
				DBAccess._dbConnTimeout,
				DBAccess._dbUseConnPooling,
				DBAccess._dbMinPoolSize,
				DBAccess._dbMaxPoolSize
			});
		}
		private NpgsqlConnection GetConnection()
		{
			if (this._conn == null)
			{
				this._conn = new NpgsqlConnection(this.GetConnectString());
				this._conn.Open();
				string environmentVariable = Environment.GetEnvironmentVariable("PGOPTIONS");
				if (environmentVariable != null && environmentVariable.StartsWith("-c "))
				{
					Logger.Trace("PGOPTIONS value: {0}", new object[]
					{
						environmentVariable
					});
					string text = "SET " + environmentVariable.Substring(3);
					Logger.Trace("PGOPTIONS sqlcmd: {0}", new object[]
					{
						text
					});
					NpgsqlCommand npgsqlCommand = new NpgsqlCommand(text, this._conn);
					npgsqlCommand.ExecuteNonQuery();
				}
			}
			if (this._useTxn && this._tran == null)
			{
				this._tran = this._conn.BeginTransaction();
			}
			return this._conn;
		}
		public static void PollUntilDbAcceptsConnection()
		{
			using (DBAccess dBAccess = new DBAccess())
			{
				while (!dBAccess.CanConnectToDb)
				{
					Thread.Sleep(1000);
				}
			}
		}
		private NpgsqlCommand PrepareAndReturnCommand(string sqlStatement, StoredProcParamCollection parms)
		{
			this._command = new NpgsqlCommand(sqlStatement, this.GetConnection());
			DBAccess.AddParameters(this._command, parms);
			if (!sqlStatement.Contains(" "))
			{
				this._command.CommandType = CommandType.StoredProcedure;
			}
			this._command.CommandTimeout = 0;
			return this._command;
		}
		public IDataReader ExecuteReader(string sqlStatement)
		{
			return this.ExecuteReader(sqlStatement, null);
		}
		public IDataReader ExecuteReader(string sqlStatement, StoredProcParamCollection parms)
		{
			NpgsqlDataReader result = null;
			int nextTraceCount = this.NextTraceCount;
			try
			{
				this.PrepareAndReturnCommand(sqlStatement, parms);
				this.Trace(this._command, (long)nextTraceCount);
				this.StartTimeoutTimer();
				result = this._command.ExecuteReader();
				this._commandFinished = true;
			}
			catch (NpgsqlException sqlEx)
			{
				this.StopTimeoutTimer();
				result = (NpgsqlDataReader)this.RetryExecute(this.PrepareAndReturnCommand(sqlStatement, parms), DBAccess.ExecuteType.ExecuteReader, sqlEx);
			}
			finally
			{
				this.TraceDone(this._command, (long)nextTraceCount);
				if (this._command != null)
				{
					this._command.Dispose();
					this._command = null;
				}
			}
			return result;
		}
		public DataSet ExecuteDataSet(string sqlStatement)
		{
			return this.ExecuteDataSet(sqlStatement, null);
		}
		public DataSet ExecuteDataSet(string sqlStatement, StoredProcParamCollection parms)
		{
			NpgsqlDataAdapter npgsqlDataAdapter = null;
			DataSet dataSet = null;
			try
			{
				this.PrepareAndReturnCommand(sqlStatement, parms);
				this.StartTimeoutTimer();
				npgsqlDataAdapter = new NpgsqlDataAdapter(this._command);
				dataSet = new DataSet();
				npgsqlDataAdapter.Fill(dataSet);
				this._commandFinished = true;
			}
			catch (NpgsqlException sqlEx)
			{
				this.StopTimeoutTimer();
				this._command = this.PrepareAndReturnCommand(sqlStatement, parms);
				this.RetryExecute(npgsqlDataAdapter, ref dataSet, sqlEx);
			}
			finally
			{
				if (this._command != null)
				{
					this._command.Dispose();
					this._command = null;
				}
				if (npgsqlDataAdapter != null)
				{
					npgsqlDataAdapter.Dispose();
				}
			}
			return dataSet;
		}
		private string GetDbExceptionMessage(NpgsqlException ex)
		{
			if (ex.Code == DBAccess.NpgsqlErrors.QUERY_CANCELED && !this._commandFinished)
			{
				return string.Format("Timeout expired.  The timeout period elapsed prior to completion of the operation. Current timeout: {0}.", this.Timeout);
			}
			return ex.Message;
		}
		public int ExecuteSql(string sqlStatement)
		{
			int result = 0;
			int nextTraceCount = this.NextTraceCount;
			try
			{
				this.PrepareAndReturnCommand(sqlStatement, null);
				this.Trace(this._command, (long)nextTraceCount);
				this.StartTimeoutTimer();
				result = this._command.ExecuteNonQuery();
				this._commandFinished = true;
			}
			catch (NpgsqlException sqlEx)
			{
				this.StopTimeoutTimer();
				this.RetryExecute(this.PrepareAndReturnCommand(sqlStatement, null), DBAccess.ExecuteType.ExecuteNonQuery, sqlEx);
			}
			finally
			{
				this.TraceDone(this._command, (long)nextTraceCount);
				if (this._command != null)
				{
					this._command.Dispose();
					this._command = null;
				}
			}
			return result;
		}
		public int ExecuteNonQuery(string sqlStatement)
		{
			return this.ExecuteNonQuery(sqlStatement, null);
		}
		public int ExecuteNonQuery(string sqlStatement, StoredProcParamCollection parms)
		{
			int result = 0;
			int nextTraceCount = this.NextTraceCount;
			try
			{
				this.PrepareAndReturnCommand(sqlStatement, parms);
				this.Trace(this._command, (long)nextTraceCount);
				this.StartTimeoutTimer();
				result = this._command.ExecuteNonQuery();
				this._commandFinished = true;
			}
			catch (NpgsqlException sqlEx)
			{
				this.StopTimeoutTimer();
				this.RetryExecute(this.PrepareAndReturnCommand(sqlStatement, parms), DBAccess.ExecuteType.ExecuteNonQuery, sqlEx);
			}
			finally
			{
				this.TraceDone(this._command, (long)nextTraceCount);
				if (this._command != null)
				{
					this._command.Dispose();
					this._command = null;
				}
			}
			return result;
		}
		public long ExecuteScalarLong(string sqlStatement)
		{
			return this.ExecuteScalarLong(sqlStatement, null);
		}
		public long ExecuteScalarLong(string sqlStatement, StoredProcParamCollection parms)
		{
			long result = 0L;
			object obj = this.ExecuteScalar(sqlStatement, parms);
			try
			{
				if (obj != null && !(obj is DBNull))
				{
					result = (long)obj;
				}
			}
			catch (NullReferenceException)
			{
				result = 0L;
			}
			catch (InvalidCastException ex)
			{
				if (!long.TryParse(obj.ToString(), out result))
				{
					Logger.LogException(ex);
					Logger.Trace("ExecuteScalarLong: sql={0}", new object[]
					{
						sqlStatement
					});
					if (parms != null)
					{
						for (int i = 0; i < parms.Count; i++)
						{
							Logger.Trace("ExecuteScalarInt:  parm[{0}] '{1}' '{2}'", new object[]
							{
								i,
								parms[i].Name,
								parms[i].Value
							});
						}
					}
				}
			}
			return result;
		}
		public int ExecuteScalarInt(string sqlStatement)
		{
			return this.ExecuteScalarInt(sqlStatement, null);
		}
		public int ExecuteScalarInt(string sqlStatement, StoredProcParamCollection parms)
		{
			int result = 0;
			object obj = this.ExecuteScalar(sqlStatement, parms);
			try
			{
				if (obj != null && !(obj is DBNull))
				{
					result = (int)obj;
				}
			}
			catch (NullReferenceException)
			{
				result = 0;
			}
			catch (InvalidCastException ex)
			{
				if (!int.TryParse(obj.ToString(), out result))
				{
					Logger.LogException(ex);
					Logger.Trace("ExecuteScalarInt: sql={0}", new object[]
					{
						sqlStatement
					});
					if (parms != null)
					{
						for (int i = 0; i < parms.Count; i++)
						{
							Logger.Trace("ExecuteScalarInt:  parm[{0}] '{1}' '{2}'", new object[]
							{
								i,
								parms[i].Name,
								parms[i].Value
							});
						}
					}
				}
			}
			return result;
		}
		public double ExecuteScalarDouble(string sqlStatement)
		{
			return this.ExecuteScalarDouble(sqlStatement, null);
		}
		public double ExecuteScalarDouble(string sqlStatement, StoredProcParamCollection parms)
		{
			double result = 0.0;
			object obj = this.ExecuteScalar(sqlStatement, parms);
			try
			{
				if (obj != null && !(obj is DBNull))
				{
					result = (double)obj;
				}
			}
			catch (NullReferenceException)
			{
				result = 0.0;
			}
			catch (InvalidCastException ex)
			{
				if (!double.TryParse(obj.ToString(), out result))
				{
					Logger.LogException(ex);
					Logger.Trace("ExecuteScalarDouble: sql={0}", new object[]
					{
						sqlStatement
					});
					if (parms != null)
					{
						for (int i = 0; i < parms.Count; i++)
						{
							Logger.Trace("ExecuteScalarDouble:  parm[{0}] '{1}' '{2}'", new object[]
							{
								i,
								parms[i].Name,
								parms[i].Value
							});
						}
					}
				}
			}
			return result;
		}
		public bool ExecuteScalarBool(string sqlStatement)
		{
			return this.ExecuteScalarBool(sqlStatement, null);
		}
		public bool ExecuteScalarBool(string sqlStatement, StoredProcParamCollection parms)
		{
			bool result = false;
			object obj = this.ExecuteScalar(sqlStatement, parms);
			try
			{
				if (obj is bool)
				{
					result = (bool)obj;
				}
				else
				{
					if (obj is int)
					{
						result = ((int)obj == 1);
					}
				}
			}
			catch (NullReferenceException)
			{
				result = false;
			}
			catch (InvalidCastException ex)
			{
				if (!bool.TryParse(obj.ToString(), out result))
				{
					Logger.LogException(ex);
					Logger.Trace("ExecuteScalarBool: sql={0}", new object[]
					{
						sqlStatement
					});
					if (parms != null)
					{
						for (int i = 0; i < parms.Count; i++)
						{
							Logger.Trace("ExecuteScalarBool:  parm[{0}] '{1}' '{2}'", new object[]
							{
								i,
								parms[i].Name,
								parms[i].Value
							});
						}
					}
				}
			}
			return result;
		}
		public T ExecuteScalar<T>(string sqlStatement) where T : new()
		{
			return this.ExecuteScalar<T>(sqlStatement, null);
		}
		public T ExecuteScalar<T>(string sqlStatement, StoredProcParamCollection parms) where T : new()
		{
			T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			try
			{
				result = (T)((object)this.ExecuteScalar(sqlStatement, parms));
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			return result;
		}
		public string ExecuteScalarString(string sqlStatement)
		{
			return this.ExecuteScalarString(sqlStatement, null);
		}
		public string ExecuteScalarString(string sqlStatement, StoredProcParamCollection parms)
		{
			string result;
			try
			{
				result = (string)this.ExecuteScalar(sqlStatement, parms);
			}
			catch (InvalidCastException)
			{
				result = string.Empty;
			}
			catch (NullReferenceException)
			{
				result = string.Empty;
			}
			return result;
		}
		public DateTime ExecuteScalarDateTime(string sqlStatement)
		{
			return this.ExecuteScalarDateTime(sqlStatement, null);
		}
		public DateTime ExecuteScalarDateTime(string sqlStatement, StoredProcParamCollection parms)
		{
			DateTime result;
			try
			{
				result = (DateTime)this.ExecuteScalar(sqlStatement, parms);
			}
			catch (InvalidCastException)
			{
				result = DateTime.MinValue;
			}
			catch (NullReferenceException)
			{
				result = DateTime.MinValue;
			}
			return result;
		}
		private object ExecuteScalar(string sqlStatement, StoredProcParamCollection parms)
		{
			object result = null;
			int nextTraceCount = this.NextTraceCount;
			try
			{
				this.PrepareAndReturnCommand(sqlStatement, parms);
				this.Trace(this._command, (long)nextTraceCount);
				this.StartTimeoutTimer();
				result = this._command.ExecuteScalar();
				this._commandFinished = true;
			}
			catch (NpgsqlException sqlEx)
			{
				this.StopTimeoutTimer();
				result = this.RetryExecute(this.PrepareAndReturnCommand(sqlStatement, parms), DBAccess.ExecuteType.ExecuteScaler, sqlEx);
			}
			finally
			{
				this.TraceDone(this._command, (long)nextTraceCount);
				if (this._command != null)
				{
					this._command.Dispose();
					this._command = null;
				}
			}
			return result;
		}
		private object RetryExecute(NpgsqlCommand cmd, DBAccess.ExecuteType executeType, NpgsqlException sqlEx)
		{
			if (!this._retryExecute)
			{
				throw sqlEx;
			}
			NpgsqlDataReader npgsqlDataReader = null;
			object obj = null;
			int num = 0;
			bool flag = false;
			if (sqlEx != null)
			{
				try
				{
					if (cmd != null)
					{
						if (this._tran != null)
						{
							this.RollbackTransaction();
						}
						if (sqlEx.Code == DBAccess.NpgsqlErrors.DEADLOCK_DETECTED.ToString() || sqlEx.Code == DBAccess.NpgsqlErrors.LOCK_NOT_AVAILABLE.ToString() || sqlEx.Code == DBAccess.NpgsqlErrors.TRANSACTION_ROLLBACK.ToString() || sqlEx.Code == DBAccess.NpgsqlErrors.QUERY_CANCELED.ToString())
						{
							string text = (cmd.CommandText.Length > 100) ? (cmd.CommandText.Substring(0, 100) + "...") : cmd.CommandText;
							Logger.Trace("Caught Npgsql Exception. Error Code: {0}, Message: {1}, Statement: '{2}'. Retrying...", new object[]
							{
								sqlEx.Code,
								this.GetDbExceptionMessage(sqlEx),
								text
							});
							this.Timeout *= 2;
							if (this._useTxn)
							{
								this._tran = this._conn.BeginTransaction();
							}
							this.StartTimeoutTimer();
							switch (executeType)
							{
							case DBAccess.ExecuteType.ExecuteScaler:
								obj = cmd.ExecuteScalar();
								break;
							case DBAccess.ExecuteType.ExecuteReader:
								npgsqlDataReader = cmd.ExecuteReader(CommandBehavior.Default);
								break;
							case DBAccess.ExecuteType.ExecuteNonQuery:
								num = cmd.ExecuteNonQuery();
								break;
							}
							this._commandFinished = true;
						}
						else
						{
							Logger.LogException(sqlEx, cmd);
							flag = true;
						}
					}
					else
					{
						Logger.LogException(sqlEx);
						flag = true;
					}
				}
				catch (Exception ex)
				{
					this.StopTimeoutTimer();
					if (cmd != null)
					{
						this.RollbackTransaction();
					}
					if (ex is NpgsqlException)
					{
						NpgsqlException ex2 = (NpgsqlException)ex;
						Logger.LogException(ex, cmd, this.GetDbExceptionMessage(ex2));
						throw new DwmNpgsqlException(this.GetDbExceptionMessage(ex2), ex2);
					}
					Logger.LogException(ex, cmd);
					throw;
				}
				finally
				{
					this.Timeout = this._cmdTimeoutConfig;
				}
			}
			if (flag)
			{
				throw new DwmNpgsqlException(this.GetDbExceptionMessage(sqlEx), sqlEx);
			}
            if (executeType == DBAccess.ExecuteType.ExecuteScaler)
                return obj;
            if (executeType == DBAccess.ExecuteType.ExecuteReader)
                return (object)npgsqlDataReader;
            return (object)num;
		}
		private void RetryExecute(NpgsqlDataAdapter adapter, ref DataSet ds, NpgsqlException sqlEx)
		{
			if (!this._retryExecute)
			{
				throw sqlEx;
			}
			bool flag = false;
			if (sqlEx != null)
			{
				try
				{
					if (adapter != null && adapter.SelectCommand != null)
					{
						if (this._useTxn)
						{
							this.RollbackTransaction();
						}
						if (sqlEx.Code == DBAccess.NpgsqlErrors.DEADLOCK_DETECTED.ToString() || sqlEx.Code == DBAccess.NpgsqlErrors.LOCK_NOT_AVAILABLE.ToString() || sqlEx.Code == DBAccess.NpgsqlErrors.TRANSACTION_ROLLBACK.ToString() || sqlEx.Code == DBAccess.NpgsqlErrors.QUERY_CANCELED.ToString())
						{
							string text = (adapter.SelectCommand.CommandText.Length > 100) ? (adapter.SelectCommand.CommandText.Substring(0, 100) + "...") : adapter.SelectCommand.CommandText;
							Logger.Trace("Caught Npgsql Exception. Error Code: {0}, Message: {1}, Statement: '{2}'. Retrying...", new object[]
							{
								sqlEx.Code,
								this.GetDbExceptionMessage(sqlEx),
								text
							});
							this.Timeout *= 2;
							if (this._useTxn)
							{
								this._tran = this._conn.BeginTransaction();
							}
							this.StartTimeoutTimer();
							adapter.Fill(ds);
						}
						else
						{
							Logger.LogException(sqlEx, adapter.SelectCommand);
							flag = true;
						}
					}
					else
					{
						Logger.LogException(sqlEx);
						flag = true;
					}
				}
				catch (Exception ex)
				{
					this.StopTimeoutTimer();
					this.RollbackTransaction();
					if (ex is NpgsqlException)
					{
						NpgsqlException ex2 = (NpgsqlException)ex;
						Logger.LogException(ex, adapter.SelectCommand, this.GetDbExceptionMessage(ex2));
						throw new DwmNpgsqlException(this.GetDbExceptionMessage(ex2), ex2);
					}
					Logger.LogException(ex, adapter.SelectCommand);
					throw;
				}
				finally
				{
					this.Timeout = this._cmdTimeoutConfig;
				}
				if (flag)
				{
					throw new DwmNpgsqlException(this.GetDbExceptionMessage(sqlEx), sqlEx);
				}
			}
		}
		private void RollbackTransaction()
		{
			try
			{
				if (this._useTxn && this._tran != null)
				{
					this._tran.Rollback();
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
		}
		~DBAccess()
		{
			this.Dispose(false);
		}
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					if (this._useTxn && this._tran != null)
					{
						this._tran.Commit();
					}
				}
				catch (Exception)
				{
				}
				try
				{
					this.RemoveAllAdvisoryLocks();
				}
				catch (Exception ex)
				{
					Logger.LogError("Exception trying to remove advisory lock(s)", new object[0]);
					Logger.LogException(ex);
				}
				if (this._conn != null)
				{
					this._conn.Close();
					this._conn.Dispose();
					this._conn = null;
				}
			}
		}
		private static void AddParameters(NpgsqlCommand cmd, StoredProcParamCollection parms)
		{
			if (parms != null)
			{
				for (int i = 0; i < parms.Count; i++)
				{
					NpgsqlParameter npgsqlParameter;
					if (parms[i].Value != null)
					{
						if (parms[i].Type != StoredProcParam.DataTypes.Unknown)
						{
							npgsqlParameter = new NpgsqlParameter(parms[i].Name, (NpgsqlDbType)parms[i].Type);
						}
						else
						{
							npgsqlParameter = new NpgsqlParameter(parms[i].Name, DBAccess.GetSqlType(parms[i].Value.GetType()));
						}
						npgsqlParameter.Value = parms[i].Value;
					}
					else
					{
						npgsqlParameter = new NpgsqlParameter(parms[i].Name, parms[i].Value);
					}
					cmd.Parameters.Add(npgsqlParameter);
				}
			}
		}
		private static NpgsqlDbType GetSqlType(Type type)
		{
			NpgsqlDbType result;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				result = NpgsqlDbType.Boolean;
				return result;
			case TypeCode.Char:
				result = NpgsqlDbType.Char;
				return result;
			case TypeCode.SByte:
			case TypeCode.Byte:
				result = NpgsqlDbType.Smallint;
				return result;
			case TypeCode.Int16:
				result = NpgsqlDbType.Smallint;
				return result;
			case TypeCode.Int32:
				result = NpgsqlDbType.Integer;
				return result;
			case TypeCode.Int64:
				result = NpgsqlDbType.Bigint;
				return result;
			case TypeCode.Single:
				result = NpgsqlDbType.Real;
				return result;
			case TypeCode.Double:
				result = NpgsqlDbType.Numeric;
				return result;
			case TypeCode.Decimal:
				result = NpgsqlDbType.Numeric;
				return result;
			case TypeCode.DateTime:
				result = NpgsqlDbType.Timestamp;
				return result;
			case TypeCode.String:
				result = NpgsqlDbType.Varchar;
				return result;
			}
			result = NpgsqlDbType.Varchar;
			return result;
		}
		public static string GetCurrentUserName()
		{
			string result = string.Empty;
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal != null)
			{
				IIdentity identity = currentPrincipal.Identity;
				if (identity != null)
				{
					result = identity.Name;
				}
			}
			return result;
		}
		public static string GetString(IDataRecord reader, int index)
		{
			return DBAccess.GetString(reader, index, string.Empty);
		}
		public static string GetString(IDataRecord reader, int index, string defaultVal)
		{
			string result = defaultVal;
			if (reader != null && index < reader.FieldCount && !reader.IsDBNull(index))
			{
				try
				{
					result = reader[index].ToString();
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static string GetString(IDataRecord reader, string col)
		{
			return DBAccess.GetString(reader, col, null);
		}
		public static string GetString(IDataRecord reader, string col, string defaultVal)
		{
			string result = defaultVal;
			if (reader != null)
			{
				try
				{
					result = reader[col].ToString();
				}
				catch (IndexOutOfRangeException)
				{
					result = defaultVal;
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static int GetInt(IDataRecord reader, int index)
		{
			int result = -1;
			if (reader != null && index < reader.FieldCount)
			{
				try
				{
					int.TryParse(reader[index].ToString(), out result);
				}
				catch (NullReferenceException)
				{
					result = -1;
				}
			}
			return result;
		}
		public static int GetInt(IDataRecord reader, int index, int defaultVal)
		{
			int result = defaultVal;
			if (reader != null && index < reader.FieldCount && !reader.IsDBNull(index))
			{
				try
				{
					int.TryParse(reader[index].ToString(), out result);
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static int GetInt(IDataRecord reader, string col)
		{
			return DBAccess.GetInt(reader, col, 0);
		}
		public static int GetInt(IDataRecord reader, string col, int defaultVal)
		{
			int result = defaultVal;
			if (reader != null)
			{
				try
				{
					int.TryParse(reader[col].ToString(), out result);
				}
				catch (IndexOutOfRangeException)
				{
					result = defaultVal;
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static long GetInt64(IDataRecord reader, int index)
		{
			return DBAccess.GetInt64(reader, index, -1L);
		}
		public static long GetInt64(IDataRecord reader, int index, long defaultVal)
		{
			long result = defaultVal;
			if (reader != null && index < reader.FieldCount && !reader.IsDBNull(index))
			{
				try
				{
					long.TryParse(reader[index].ToString(), out result);
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static long GetInt64(IDataRecord reader, string col)
		{
			return DBAccess.GetInt64(reader, col, 0L);
		}
		public static long GetInt64(IDataRecord reader, string col, long defaultVal)
		{
			long result = defaultVal;
			if (reader != null)
			{
				try
				{
					long.TryParse(reader[col].ToString(), out result);
				}
				catch (IndexOutOfRangeException)
				{
					result = defaultVal;
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static double GetDouble(IDataRecord reader, int index)
		{
			return DBAccess.GetDouble(reader, index, -1.0);
		}
		public static double GetDouble(IDataRecord reader, int index, double defaultVal)
		{
			double result = defaultVal;
			if (reader != null && index < reader.FieldCount && !reader.IsDBNull(index))
			{
				try
				{
					double.TryParse(reader[index].ToString(), out result);
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static double GetDouble(IDataRecord reader, string col)
		{
			return DBAccess.GetDouble(reader, col, 0.0);
		}
		public static double GetDouble(IDataRecord reader, string col, double defaultVal)
		{
			double result = defaultVal;
			try
			{
				double.TryParse(reader[col].ToString(), out result);
			}
			catch (IndexOutOfRangeException)
			{
				result = defaultVal;
			}
			catch (NullReferenceException)
			{
				result = defaultVal;
			}
			return result;
		}
		public static DateTime GetDateTime(IDataRecord reader, int index)
		{
			return DBAccess.GetDateTime(reader, index, DateTime.MinValue);
		}
		public static DateTime GetDateTime(IDataRecord reader, int index, DateTime defaultVal)
		{
			DateTime result = defaultVal;
			if (reader != null && index < reader.FieldCount && !reader.IsDBNull(index))
			{
				try
				{
					DateTime.TryParse(reader[index].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static DateTime GetDateTime(IDataRecord reader, string col)
		{
			return DBAccess.GetDateTime(reader, col, DateTime.MinValue);
		}
		public static DateTime GetDateTime(IDataRecord reader, string col, DateTime defaultVal)
		{
			DateTime result = defaultVal;
			try
			{
				DateTime.TryParse(reader[col].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
			}
			catch (IndexOutOfRangeException)
			{
				result = defaultVal;
			}
			catch (NullReferenceException)
			{
				result = defaultVal;
			}
			return result;
		}
		public static bool GetBool(IDataRecord reader, int index)
		{
			return DBAccess.GetBool(reader, index, false);
		}
		public static bool GetBool(IDataRecord reader, int index, bool defaultVal)
		{
			bool result = defaultVal;
			if (reader != null && index < reader.FieldCount && !reader.IsDBNull(index))
			{
				try
				{
					string text = reader[index].ToString();
					if (!bool.TryParse(text, out result))
					{
						int num;
						if (int.TryParse(text, out num))
						{
							result = (num == 1);
						}
						else
						{
							result = defaultVal;
						}
					}
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
				catch (FormatException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public static bool GetBool(IDataRecord reader, string col)
		{
			return DBAccess.GetBool(reader, col, false);
		}
		public static bool GetBool(IDataRecord reader, string col, bool defaultVal)
		{
			bool result = defaultVal;
			if (reader != null)
			{
				try
				{
					string text = reader[col].ToString();
					if (!bool.TryParse(text, out result))
					{
						int num;
						if (int.TryParse(text, out num))
						{
							result = (num == 1);
						}
						else
						{
							result = defaultVal;
						}
					}
				}
				catch (IndexOutOfRangeException)
				{
					result = defaultVal;
				}
				catch (NullReferenceException)
				{
					result = defaultVal;
				}
				catch (FormatException)
				{
					result = defaultVal;
				}
			}
			return result;
		}
		public void SetWorkMemoryMB(int workMem)
		{
			this.ExecuteSql(string.Format("set work_mem='{0}MB';", workMem));
		}
		public void ResetWorkMemory()
		{
			this.ExecuteSql("reset work_mem;");
		}
		public bool PlaceAdvisoryLock(long resourceId, DBAccess.LockMode lockMode)
		{
			string sqlStatement = "pg_try_advisory_lock";
			if (lockMode == DBAccess.LockMode.Shared)
			{
				sqlStatement = "pg_try_advisory_lock_shared";
			}
			if (this.ExecuteScalarBool(sqlStatement, new StoredProcParamCollection
			{
				new StoredProcParam("key", resourceId)
			}))
			{
				if (lockMode == DBAccess.LockMode.Exclusive)
				{
					this._exclusiveAdvisoryLocks.Add(resourceId);
				}
				else
				{
					if (lockMode == DBAccess.LockMode.Shared)
					{
						this._sharedAdvisoryLocks.Add(resourceId);
					}
				}
				return true;
			}
			return false;
		}
		public bool RemoveAdvisoryLock(long resourceId, DBAccess.LockMode lockMode)
		{
			string sqlStatement = "pg_advisory_unlock";
			if (lockMode == DBAccess.LockMode.Shared)
			{
				sqlStatement = "pg_advisory_unlock_shared";
			}
			if (this.ExecuteScalarBool(sqlStatement, new StoredProcParamCollection
			{
				new StoredProcParam("key", resourceId)
			}))
			{
				if (lockMode == DBAccess.LockMode.Exclusive && this._exclusiveAdvisoryLocks.Contains(resourceId))
				{
					this._exclusiveAdvisoryLocks.Remove(resourceId);
				}
				else
				{
					if (lockMode == DBAccess.LockMode.Shared && this._sharedAdvisoryLocks.Contains(resourceId))
					{
						this._sharedAdvisoryLocks.Remove(resourceId);
					}
				}
				return true;
			}
			return false;
		}
		public void RemoveAllAdvisoryLocks()
		{
			if (this.AdvisoryLocksCount > 0)
			{
				string sqlStatement = "pg_advisory_unlock_all";
				this.ExecuteScalar(sqlStatement, null);
				this._exclusiveAdvisoryLocks.Clear();
				this._sharedAdvisoryLocks.Clear();
			}
		}
		private void Trace(NpgsqlCommand cmd, long traceCount)
		{
			if (this._traceEnabled > 0)
			{
				if (this._traceEnabled == 1)
				{
					if (cmd != null && cmd.CommandType == CommandType.StoredProcedure && !cmd.CommandText.StartsWith("data_collection_") && !cmd.CommandText.StartsWith("get_host_last_metric_time"))
					{
						Logger.Trace("DBAccess: {0} ThreadId={1} {2} {3}", new object[]
						{
							traceCount,
							Thread.CurrentThread.ManagedThreadId,
							DateTime.Now.ToString("hh:mm:ss.ff"),
							cmd.CommandText
						});
					}
				}
				else
				{
					if (this._traceEnabled == 2)
					{
						if (cmd != null)
						{
							int num = cmd.CommandText.IndexOf("\n");
							Logger.Trace("DBAccess: {0} ThreadId={1} {2} {3}", new object[]
							{
								traceCount,
								Thread.CurrentThread.ManagedThreadId,
								DateTime.Now.ToString("hh:mm:ss.ff"),
								(num != -1) ? (cmd.CommandText.Substring(0, num) + "...") : cmd.CommandText
							});
						}
					}
					else
					{
						if (cmd != null)
						{
							Logger.Trace("DBAccess: {0} ThreadId={1} {2} {3}", new object[]
							{
								traceCount,
								Thread.CurrentThread.ManagedThreadId,
								DateTime.Now.ToString("hh:mm:ss.ff"),
								cmd.CommandText
							});
						}
					}
				}
			}
		}
		private void TraceDone(NpgsqlCommand cmd, long traceCount)
		{
			if (this._traceEnabled > 0)
			{
				if (this._traceEnabled == 1)
				{
					if (cmd != null && cmd.CommandType == CommandType.StoredProcedure && !cmd.CommandText.StartsWith("data_collection_") && !cmd.CommandText.StartsWith("get_host_last_metric_time"))
					{
						Logger.Trace("DBAccess: {0} ThreadId={1} {2} Done", new object[]
						{
							traceCount,
							Thread.CurrentThread.ManagedThreadId,
							DateTime.Now.ToString("hh:mm:ss.fff")
						});
					}
				}
				else
				{
					Logger.Trace("DBAccess: {0} ThreadId={1} {2} Done", new object[]
					{
						traceCount,
						Thread.CurrentThread.ManagedThreadId,
						DateTime.Now.ToString("hh:mm:ss.ff")
					});
				}
			}
		}
	}
}
