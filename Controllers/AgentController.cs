using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Services.DataStorage;

namespace PremiumTravelService.Api.Controllers;

public class AgentController : BaseApiController
{
    private readonly IDataStorageService _dataStorageService;
    
    public AgentController(IDataStorageService dataStorageService)
    {
        _dataStorageService = dataStorageService;
    }
    
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
}