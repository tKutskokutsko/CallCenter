using System.ComponentModel.DataAnnotations;

namespace CallCenterApi.Infrastructure.DB.Entities;

public class AgentInfo
{
    private AgentInfo(Guid agentId, string agentName, DateTime timestampUtc, string action, Guid[]? queueIds, string agentState)
        : this()
    {
        AgentId = agentId;
        AgentName = agentName;
        TimestampUtc = timestampUtc;
        Action = action;
        QueueIds = queueIds;
        AgentState = agentState;
    }

    private AgentInfo()
    {
    }

    [Key]
    public Guid AgentId { get; set; }
    public string AgentName { get; set; }
    public DateTime TimestampUtc { get; set; }
    public string Action { get; set; }
    public string AgentState { get; set; }
    public Guid[]? QueueIds { get; set; }

    public static AgentInfo Create(Guid agentId, string agentName, DateTime timestampUtc, string action, Guid[]? queueIds, string agentState)
    {
        return new AgentInfo(agentId, agentName, timestampUtc, action, queueIds, agentState);
    }

    public void SetQueueIds(Guid[]? queueIds)
    {
        QueueIds = queueIds;
    }

    public void SetAgentState(string agentState)
    {
        AgentState = agentState;
    }
}