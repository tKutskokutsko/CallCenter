using CallCenterApi.Infrastructure.DB.Entities;
using CallCenterApi.Infrastructure.DB.Repositories;
using CallCenterApi.Models;

namespace CallCenterApi.Services;

public class AgentInfoManagementService : IAgentInfoManagementService
{
    private static readonly TimeSpan LunchStartedTime = new(11, 0, 0);
    private static readonly TimeSpan LunchEndedTime = new(13, 0, 0);

    private readonly IAgentInfoRepository _agentInfoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AgentInfoManagementService(IAgentInfoRepository agentInfoRepository, IUnitOfWork unitOfWork)
    {
        _agentInfoRepository = agentInfoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AgentStateResponseModel?> CheckAgentState(AgentInfoModel model)
    {
        var agentInfo = await GetAgentInfo(model.AgentId);

        if (agentInfo is not null)
        {
            switch (model.Action)
            {
                case "START_DO_NOT_DISTURB" when IsLunchTime(model.TimestampUtc):
                    await _unitOfWork.Commit(() =>
                    {
                        agentInfo.SetQueueIds(model.QueueIds);
                        agentInfo.SetAgentState("ON_LUNCH");
                    });

                    return new AgentStateResponseModel("ON_LUNCH");

                case "CALL_STARTED":
                    await _unitOfWork.Commit(() =>
                    {
                        agentInfo.SetQueueIds(model.QueueIds);
                        agentInfo.SetAgentState("ON_CALL");
                    });

                    return new AgentStateResponseModel("ON_CALL");
            }

            if (IsLateEvent(model.TimestampUtc))
            {
                throw new LateEventException();
            }
        }
        else
        {
            await CreateAgentUser(model);
        }
        

        return null;
    }

    private async Task<AgentInfo?> GetAgentInfo(Guid agentId)
    {
        var agentInfo = await _agentInfoRepository.Get(agentId);

        return agentInfo;
    }

    private async Task CreateAgentUser(AgentInfoModel model)
    {
        var agent = AgentInfo.Create(model.AgentId, model.AgentName, model.TimestampUtc, model.Action, model.QueueIds, "new");

        await _unitOfWork.Commit(async () => { await _agentInfoRepository.Create(agent); });
    }

    private bool IsLunchTime(DateTime currentTime)
    {
        if (currentTime.TimeOfDay <= LunchEndedTime && currentTime.TimeOfDay >=LunchStartedTime)
        {
            return true;
        }
        
        return false;
    }

    private bool IsLateEvent(DateTime currentTime)
    {
        var result = (DateTime.UtcNow - currentTime).TotalHours;

        return result > 1;
    }
}