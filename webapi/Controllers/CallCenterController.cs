using CallCenterApi.Models;
using CallCenterApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CallCenterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CallCenterController : ControllerBase
{
    private readonly IAgentInfoManagementService _agentInfoManagementService;

    public CallCenterController(IAgentInfoManagementService agentInfoManagementService)
    {
        _agentInfoManagementService = agentInfoManagementService;
    }

    /// <summary>
    /// Save agent information
    /// </summary>
    /// <param name="model">The <see cref="AgentInfoModel"></see> data</param>
    /// <response code="204">Bank account was updated</response>
    /// <response code="400">Bad request model data</response>
    /// <response code="404">Bank account was not found</response>
    /// <returns>No content</returns>
    [HttpPost("agentInfo")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] AgentInfoModel model)
    {
        try
        {
            var result = await _agentInfoManagementService.CheckAgentState(model);

            return Ok(result);
        }
        catch (LateEventException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}