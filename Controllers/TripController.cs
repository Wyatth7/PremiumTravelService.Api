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

    /// <summary>
    /// Gets trips for an agent
    /// </summary>
    /// <param name="agentId">ID of agent</param>
    /// <returns>list of trips belonging to an agent</returns>
    [HttpGet]
    [Route("agent/{agentId:Guid:required}")]
    [Produces("Application/json")]
    public async Task<IActionResult> GetTripsForAgent([FromRoute] Guid agentId)
    {
        var storageData = await _dataStorageService.Read();

        var trips = storageData.Trips
            .Select(t => new { t.TripId, t.AssignedByPersonId })
            .Where(t => t.AssignedByPersonId == agentId);

        var state = storageData.StateMachines
            .Where(sm => trips.Any(t => t.TripId == sm.TripId));

        var agentTripArray = trips.Select(trip => new AgentTripDataDto
        {
            TripId = trip.TripId,
            IsComplete = state.First(stateItem => stateItem.TripId == trip.TripId)
                .IsComplete
        });

        return new OkObjectResult(agentTripArray);
    }

    /// <summary>
    /// Gets remaining balance of a trip
    /// </summary>
    /// <param name="tripId">ID of a trip</param>
    /// <returns>trip balance</returns>
    [HttpGet]
    [Route("payment/{tripId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetRemainingTripBalance([FromRoute] Guid tripId)
    {
        var remainingBalance = await _dataStorageService.GetRemainingTripBalance(tripId);

        return new OkObjectResult(remainingBalance);
    }

    /// <summary>
    /// Gets the state of a trip
    /// </summary>
    /// <param name="tripId">ID of a trip</param>
    /// <returns>state object for trip</returns>
    [HttpGet]
    [Route("currentState/{tripId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetTripState([FromRoute] Guid tripId)
    {
        var tripState = await _dataStorageService.GetTripState(tripId);

        return new OkObjectResult(tripState);
    }
}