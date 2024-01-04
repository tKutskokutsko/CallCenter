using CallCenterApi.Infrastructure.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CallCenterApi.Infrastructure.DB.Repositories;

public class AgentInfoRepository : RepositoryBase, IAgentInfoRepository
{
    public AgentInfoRepository(DataContext dbContext)
        : base(dbContext)
    {
    }

    public Task Create(AgentInfo agentInfo)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(agentInfo));
        DbContext.AgentInfos.Add(agentInfo);

        return Task.CompletedTask;
    }

    public async Task<AgentInfo> Get(Guid agentId)
    {
        return await DbContext.AgentInfos.FirstOrDefaultAsync(x => x.AgentId == agentId);
    }

    public void Update(AgentInfo agentInfo)
    {
        DbContext.AgentInfos.Update(agentInfo);
    }
}