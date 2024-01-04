using CallCenterApi.Models;

namespace CallCenterApi.Services;

public interface IAgentInfoManagementService
{
    Task<AgentStateResponseModel> CheckAgentState(AgentInfoModel model);
}