using System;
using System.Net.Security;
using System.ServiceModel;
namespace Halsign.DWM.Communication
{
	[ServiceContract(Namespace = "http://schemas.halsign.com/ARDS", SessionMode = SessionMode.Allowed, ProtectionLevel = ProtectionLevel.None)]
	public interface IScheduledTask
	{
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		GetSchedTaskResponse GetScheduledTask(GetSchedTaskRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		SetSchedTaskResponse SetScheduledTask(SetSchedTaskRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		DeleteSchedTaskResponse DeleteScheduledTask(DeleteSchedTaskRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		GetSchedTaskActionTypeResponse GetScheduledTaskActionTypes(GetSchedTaskActionTypeRequest request);
	}
}
