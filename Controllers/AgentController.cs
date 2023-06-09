﻿using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Services.DataStorage;
using PremiumTravelService.Api.Services.Singleton;

namespace PremiumTravelService.Api.Controllers;

public class AgentController : BaseApiController
{
    private readonly IDataStorageService _dataStorageService;
    private readonly ISingletonService _singletonService;
    
    public AgentController(IDataStorageService dataStorageService,
        ISingletonService singletonService)
    {
        _dataStorageService = dataStorageService;
        _singletonService = singletonService;
    }
    
    /// <summary>
    /// Gets a list of trips that belong to an agent
    /// </summary>
    /// <param name="agentId">ID of agent</param>
    /// <returns>list of trips</returns>
    [HttpGet]
    [Route("{agentId:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetAgentTrips([FromRoute] string agentId)
    {
        var storageData = await _dataStorageService.Read();

        var trips = storageData.Trips
            .Where(trip => trip.AssignedByPersonId == Guid.Parse(agentId));

        return new OkObjectResult(trips);
    }
    
    /// <summary>
    /// Gets an agent
    /// </summary>
    /// <param name="agentId">ID of an agent</param>
    /// <returns>Returns an agent</returns>
    [HttpGet]
    [Route("details/{agentId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetAgent([FromRoute] Guid agentId)
    {
        var agents = await _singletonService.GetAgentSingleton()
            .GetData();

        var agent = agents.FirstOrDefault(a => a.PersonId == agentId);

        return agent is not null ? new OkObjectResult(agent)
                : new BadRequestResult();
    }
}