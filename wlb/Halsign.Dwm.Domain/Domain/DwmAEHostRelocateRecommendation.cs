using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
namespace Halsign.DWM.Domain
{
	public class DwmAEHostRelocateRecommendation
	{
		private List<MoveRecommendation> _moveRecs;
		private bool _canPlaceAllVMs;
		private int _recommendationSetId;
		private DwmErrorCode _resultCode;
		public List<MoveRecommendation> MoveRecs
		{
			get
			{
				return this._moveRecs;
			}
			set
			{
				this._moveRecs = value;
			}
		}
		public bool CanPlaceAllVMs
		{
			get
			{
				return this._canPlaceAllVMs;
			}
			set
			{
				this._canPlaceAllVMs = value;
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
		public DwmErrorCode ResultCode
		{
			get
			{
				return this._resultCode;
			}
			set
			{
				this._resultCode = value;
			}
		}
	}
}
