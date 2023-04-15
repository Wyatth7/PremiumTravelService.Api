using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Dto.Trips;
using PremiumTravelService.Api.Services.DataStorage;

namespace PremiumTravelService.Api.Controllers;

public class TripController : BaseApiController
{
    private readonly IDataStorageService _dataStorageService;
    
    public TripController(IDataStorageService dataStorageService)
    {
        _dataStorageService = dataStorageService;
    }

    [HttpGet]
    [Route("agent/{agentId:Guid:required}")]
    [Produces("Application/json")]
    public async Task<IActionResult> GetTripsForAgent([FromRoute] Guid agentId)
    {
        var storageData = await _dataStorageService.Read();

        var trips = storageData.Trips
            .Where(t => t.AssignedByPersonId == agentId)
            .OrderBy(t => t.TripId);

        var state = storageData.StateMachines
            .Where(sm => trips.Any(t => t.TripId == sm.TripId))
            .OrderBy(sm => sm.TripId);

        // var agentTripArray = new List<AgentTripDataDto>();
        //
        // foreach (var trip in trips)
        // {
        //     agentTripArray.Add(new AgentTripDataDto
        //     {
        //         Trip = trip,
        //         IsComplete = state.First(s => s.TripId == trip.TripId).IsComplete
        //     });
        // }

        var agentTripArray = trips.Select(trip => new AgentTripDataDto
        {
            Trip = trip,
            IsComplete = state.First(state => state.TripId == trip.TripId)
                .IsComplete
        });

        return new OkObjectResult(agentTripArray);
    }
}