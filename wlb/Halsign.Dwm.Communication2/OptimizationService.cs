using System;
namespace Halsign.DWM.Communication2
{
	public class OptimizationService : WlbServiceBase<Optimization>
	{
		public override object OnGet(Optimization request)
		{
			return new OptimizationResponse
			{
				MoveVM = 
				{
					new MoveVM
					{
						FromHostUuid = "from",
						ToHostUuid = "to",
						Reason = "This is the reason"
					},
					new MoveVM
					{
						FromHostUuid = "from2",
						ToHostUuid = "to2",
						Reason = "This is the reason2"
					}
				}
			};
		}
	}
}
