using Npgsql;
using System;
using System.Data.Common;
namespace Halsign.DWM.Framework
{
	public class DwmNpgsqlException : DbException
	{
		public enum PgSeverity
		{
			None,
			Debug,
			Info,
			Notice,
			Warning,
			Error,
			Log,
			Fatal,
			Panic
		}
		private DwmNpgsqlException.PgSeverity _severity;
		public DwmNpgsqlException.PgSeverity Severity
		{
			get
			{
				return this._severity;
			}
		}
		public DwmNpgsqlException()
		{
		}
		public DwmNpgsqlException(string message, Exception innerException) : base(message, innerException)
		{
			if (innerException is NpgsqlException)
			{
				this._severity = this.GetDbExceptionSeverity(innerException as NpgsqlException);
			}
		}
		private DwmNpgsqlException.PgSeverity GetDbExceptionSeverity(NpgsqlException ex)
		{
			DwmNpgsqlException.PgSeverity result = DwmNpgsqlException.PgSeverity.None;
			if (Localization.StringStartsWith(ex.Severity, "DEBUG", true))
			{
				result = DwmNpgsqlException.PgSeverity.Debug;
			}
			else
			{
				string text = ex.Severity.ToUpper();
				switch (text)
				{
				case "INFO":
					result = DwmNpgsqlException.PgSeverity.Info;
					break;
				case "NOTICE":
					result = DwmNpgsqlException.PgSeverity.Notice;
					break;
				case "WARNING":
					result = DwmNpgsqlException.PgSeverity.Warning;
					break;
				case "ERROR":
					result = DwmNpgsqlException.PgSeverity.Error;
					break;
				case "LOG":
					result = DwmNpgsqlException.PgSeverity.Log;
					break;
				case "FATAL":
					result = DwmNpgsqlException.PgSeverity.Fatal;
					break;
				case "PANIC":
					result = DwmNpgsqlException.PgSeverity.Panic;
					break;
				}
			}
			return result;
		}
	}
}
