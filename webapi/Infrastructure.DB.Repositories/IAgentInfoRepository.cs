using CallCenterApi.Infrastructure.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CallCenterApi.Infrastructure.DB.Repositories;

public interface IAgentInfoRepository
{
    Task Create(AgentInfo agentInfo);

    Task<AgentInfo> Get(Guid agentId);

    void Update(AgentInfo agentInfo);
}