using CallCenterApi.Models;
using CallCenterApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CallCenterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IAgentInfoManagementService _agentInfoManagementService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IAgentInfoManagementService agentInfoManagementService)
    {
        _logger = logger;
        _agentInfoManagementService = agentInfoManagementService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<AgentInfoModel> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new AgentInfoModel()
        {
            TimestampUtc = DateTime.Now.AddDays(index),
            Action = Random.Shared.Next(-20, 55).ToString(),
            AgentId = Guid.NewGuid(),
            GueueIds = new[] { Guid.NewGuid() } ,
            AgentName = Random.Shared.Next(-20, 55).ToString()
        })
        .ToArray();
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