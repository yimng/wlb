using Halsign.DWM.Framework;
using System;
using System.Data;
namespace Halsign.DWM.Domain
{
	public class MoveRecommendation
	{
		private int _poolId;
		private string _poolUuid;
		private int _vmId;
		private string _vmUuid;
		private string _vmName;
		private int _moveToHostId;
		private string _moveToHostUuid;
		private string _moveToHostName;
		private int _moveFromHostId;
		private string _moveFromHostUuid;
		private string _moveFromHostName;
		private ResourceToOptimize _reason;
		private OptimizationSeverity _severity;
		private DateTime _timeStamp;
		private int _recommendationId;
		private int _recommendationSetId;
		private int _auditId;
		private RecommendationStatus _recommendationStatus;
		public int PoolId
		{
			get
			{
				return this._poolId;
			}
			set
			{
				this._poolId = value;
			}
		}
		public string PoolUuid
		{
			get
			{
				return this._poolUuid;
			}
			set
			{
				this._poolUuid = value;
			}
		}
		public int VmId
		{
			get
			{
				return this._vmId;
			}
			set
			{
				this._vmId = value;
			}
		}
		public string VmUuid
		{
			get
			{
				return this._vmUuid;
			}
			set
			{
				this._vmUuid = value;
			}
		}
		public string VmName
		{
			get
			{
				return this._vmName;
			}
			set
			{
				this._vmName = value;
			}
		}
		public int MoveToHostId
		{
			get
			{
				return this._moveToHostId;
			}
			set
			{
				this._moveToHostId = value;
			}
		}
		public string MoveToHostUuid
		{
			get
			{
				return this._moveToHostUuid;
			}
			set
			{
				this._moveToHostUuid = value;
			}
		}
		public string MoveToHostName
		{
			get
			{
				return this._moveToHostName;
			}
			set
			{
				this._moveToHostName = value;
			}
		}
		public int MoveFromHostId
		{
			get
			{
				return this._moveFromHostId;
			}
			set
			{
				this._moveFromHostId = value;
			}
		}
		public string MoveFromHostUuid
		{
			get
			{
				return this._moveFromHostUuid;
			}
			set
			{
				this._moveFromHostUuid = value;
			}
		}
		public string MoveFromHostName
		{
			get
			{
				return this._moveFromHostName;
			}
			set
			{
				this._moveFromHostName = value;
			}
		}
		public ResourceToOptimize Reason
		{
			get
			{
				return this._reason;
			}
			set
			{
				this._reason = value;
			}
		}
		public OptimizationSeverity Severity
		{
			get
			{
				return this._severity;
			}
			set
			{
				this._severity = value;
			}
		}
		public DateTime TimeStamp
		{
			get
			{
				return this._timeStamp;
			}
			set
			{
				this._timeStamp = value;
			}
		}
		public int RecommendationId
		{
			get
			{
				return this._recommendationId;
			}
			set
			{
				this._recommendationId = value;
			}
		}
		public int RecommendationSetId
		{
			get
			{
				return this._recommendationSetId;
			}
			set
			{
				this._recommendationSetId = value;
			}
		}
		public int AuditId
		{
			get
			{
				return this._auditId;
			}
			set
			{
				this._auditId = value;
			}
		}
		public RecommendationStatus RecommendationStatus
		{
			get
			{
				return this._recommendationStatus;
			}
			set
			{
				this._recommendationStatus = value;
			}
		}
		public MoveRecommendation()
		{
			this.Severity = OptimizationSeverity.None;
		}
		public MoveRecommendationStatus GetStatus()
		{
			return MoveRecommendation.GetStatus(this.RecommendationId);
		}
		public static MoveRecommendationStatus GetStatus(int recommendationId)
		{
			MoveRecommendationStatus result = null;
			string sqlStatement = "get_recommendation_detail_status";
			StoredProcParamCollection storedProcParamCollection = new StoredProcParamCollection();
			storedProcParamCollection.Add(new StoredProcParam("@recommendation_id", recommendationId));
			using (DBAccess dBAccess = new DBAccess())
			{
				using (IDataReader dataReader = dBAccess.ExecuteReader(sqlStatement, storedProcParamCollection))
				{
					if (dataReader.Read())
					{
						result = DwmPool.LoadMoveRecStatus(dataReader);
					}
				}
			}
			return result;
		}
	}
}
