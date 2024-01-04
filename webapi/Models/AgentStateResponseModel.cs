namespace CallCenterApi.Models;

public class AgentStateResponseModel
{
    public AgentStateResponseModel(string agentState)
    {
        AgentState = agentState;
    }

    public string AgentState { get; set; }
}