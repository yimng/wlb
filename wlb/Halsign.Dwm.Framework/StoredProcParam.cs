using System;
namespace Halsign.DWM.Framework
{
	public class StoredProcParam
	{
		public enum DataTypes
		{
			Unknown,
			Bigint,
			Boolean,
			Box,
			Bytea,
			Circle,
			Char,
			Date,
			Double,
			Integer,
			Line,
			LSeg,
			Money,
			Numeric,
			Path,
			Point,
			Polygon,
			Real,
			Smallint,
			Text,
			Time,
			Timestamp,
			Varchar,
			Refcursor,
			Inet,
			Bit,
			TimestampTZ,
			Uuid,
			Xml,
			Oidvector,
			Interval,
			TimeTZ,
			Name,
			Abstime,
			Array = -2147483648
		}
		private string _name;
		private object _value;
		private StoredProcParam.DataTypes _npgSqlType;
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}
		public StoredProcParam.DataTypes Type
		{
			get
			{
				return this._npgSqlType;
			}
			set
			{
				this._npgSqlType = value;
			}
		}
		public StoredProcParam(string name, object value) : this(name, value, StoredProcParam.DataTypes.Unknown)
		{
		}
		public StoredProcParam(string name, object value, StoredProcParam.DataTypes npgSqlType)
		{
			this._name = name;
			this._value = value;
			this._npgSqlType = npgSqlType;
		}
	}
}
