using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
namespace Halsign.DWM.Framework
{
	public class DBAccess2 : IDisposable
	{
		private static string _connString;
		private NpgsqlConnection _connection;
		public int CommandTimeout
		{
			get;
			set;
		}
		public static string ConnectionString
		{
			get
			{
				if (DBAccess2._connString == null)
				{
					string valueAsString = Configuration.GetValueAsString(ConfigItems.DBServer);
					string valueAsString2 = Configuration.GetValueAsString(ConfigItems.DBName);
					int valueAsInt = Configuration.GetValueAsInt(ConfigItems.DBPort);
					string valueAsString3 = Configuration.GetValueAsString(ConfigItems.DBUsername);
					string text = Configuration.GetValueAsString(ConfigItems.DBPassword);
					bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.DBUseConnPooling);
					int valueAsInt2 = Configuration.GetValueAsInt(ConfigItems.DBMinPoolSize);
					int valueAsInt3 = Configuration.GetValueAsInt(ConfigItems.DBMaxPoolSize);
					try
					{
						text = Crypto.Decrypt(Configuration.GetValueAsString(ConfigItems.DBPassword));
					}
					catch
					{
						Configuration.SetValue(ConfigItems.DBPassword, Crypto.Encrypt(text));
					}
					Logger.Trace("Database:  host is {0}.  DB is {1}", new object[]
					{
						valueAsString,
						valueAsString2
					});
					DBAccess2._connString = new NpgsqlConnectionStringBuilder
					{
						Host = valueAsString,
						Database = valueAsString2,
						Port = valueAsInt,
						UserName = valueAsString3,
						Password = text,
						Pooling = valueAsBool,
						MinPoolSize = valueAsInt2,
						MaxPoolSize = valueAsInt3
					}.ToString();
				}
				return DBAccess2._connString;
			}
		}
		public DBAccess2()
		{
			this._connection = new NpgsqlConnection(DBAccess2.ConnectionString);
			this._connection.Open();
			this.CommandTimeout = Configuration.GetValueAsInt(ConfigItems.DBCmdTimeout, 20);
		}
		public NpgsqlCommand PrepareCommand(string sql)
		{
			return this.PrepareCommand(sql, null);
		}
		public NpgsqlCommand PrepareCommand(string sql, StoredProcParamCollection parms)
		{
			NpgsqlCommand command = new NpgsqlCommand(sql, this._connection);
			command.CommandTimeout = this.CommandTimeout;
			command.CommandType = ((!sql.Contains(" ")) ? CommandType.StoredProcedure : CommandType.Text);
			if (parms != null)
			{
				parms.ForEach(delegate(StoredProcParam p)
				{
					command.Parameters.Add(this.GetNpgsqlParameter(p));
				});
			}
			return command;
		}
		public int ExecuteSql(string sql)
		{
			NpgsqlCommand npgsqlCommand = this.PrepareCommand(sql);
			return npgsqlCommand.ExecuteNonQuery();
		}
		public T ExecuteScalar<T>(string sql) where T : new()
		{
			return this.ExecuteScalar<T>(sql, null);
		}
		public T ExecuteScalar<T>(string sql, StoredProcParamCollection parms) where T : new()
		{
			NpgsqlCommand npgsqlCommand = this.PrepareCommand(sql, parms);
			return (T)((object)npgsqlCommand.ExecuteScalar());
		}
		private NpgsqlParameter GetNpgsqlParameter(StoredProcParam parm)
		{
			NpgsqlParameter npgsqlParameter;
			if (parm.Value != null)
			{
				if (parm.Type != StoredProcParam.DataTypes.Unknown)
				{
					npgsqlParameter = new NpgsqlParameter(parm.Name, (NpgsqlDbType)parm.Type);
				}
				else
				{
					npgsqlParameter = new NpgsqlParameter(parm.Name, this.GetSqlType(parm.Value.GetType()));
				}
				npgsqlParameter.Value = parm.Value;
			}
			else
			{
				npgsqlParameter = new NpgsqlParameter(parm.Name, parm.Value);
			}
			return npgsqlParameter;
		}
		private NpgsqlDbType GetSqlType(Type type)
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
		public void Dispose()
		{
			try
			{
				if (this._connection.State != ConnectionState.Closed)
				{
					this._connection.Close();
				}
				this._connection.Dispose();
			}
			catch (Exception innerException)
			{
				throw new Exception("Error disposing DBAccess2 class.", innerException);
			}
		}
	}
}
