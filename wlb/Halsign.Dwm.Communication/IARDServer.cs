using System;
using System.Net.Security;
using System.ServiceModel;
namespace Halsign.DWM.Communication
{
	[ServiceContract(Namespace = "http://schemas.halsign.com/ARDS", SessionMode = SessionMode.Allowed, ProtectionLevel = ProtectionLevel.None)]
	public interface IARDServer : IScheduledTask
	{
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		XenServerResponse AddvGateServer(XenServerRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		XenServerResponse RemovevGateServer(XenServerRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		SetConfigurationResponse SetXenPoolConfiguration(SetConfigurationRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		GetConfigurationResponse GetXenPoolConfiguration(GetConfigurationRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		VMRecommendationsResponse VMGetRecommendations(VMRecommendationsRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		HostRecommendationsResponse HostGetRecommendations(HostRecommendationsRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		PoolOptimizationResponse GetOptimizationRecommendations(PoolOptimizationRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		ReportResponse ExecuteReport(ReportRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		DiagnosticResponse GetDiagnostics(DiagnosticRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		GetHostConfigResponse GetHostConfiguration(GetHostConfigRequest request);
		[OperationContract(ProtectionLevel = ProtectionLevel.None)]
		SetHostConfigResponse SetHostConfiguration(SetHostConfigRequest request);
	}
}
