namespace CallCenterApi.Models;

public class AgentInfoModel
{
    public Guid AgentId { get; set; }
    public string AgentName { get; set; }
    public DateTime TimestampUtc { get; set; }
    public string Action { get; set; }
    public Guid[]? QueueIds { get; set; }
}