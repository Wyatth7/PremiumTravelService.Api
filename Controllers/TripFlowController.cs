using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Models.State;
using PremiumTravelService.Api.Services.DataStorage;
using PremiumTravelService.Api.Services.Singleton;
using PremiumTravelService.Api.Services.StateMachine;

namespace PremiumTravelService.Api.Controllers;

[Route("api/trip")]
public class TripFlowController : BaseApiController
{
    private readonly IStateMachineService _stateMachineService;
    private readonly IDataStorageService _dataStorageService;
    private readonly ISingletonService _singletonService;
    
    public TripFlowController(IStateMachineService stateMachineService, IDataStorageService dataStorageService,
        ISingletonService singletonService)
    {
        _stateMachineService = stateMachineService;
        _dataStorageService = dataStorageService;
        _singletonService = singletonService;
    }
    
    
    [HttpPost]
    [Route("create/{agentId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> CreateTrip([FromRoute] Guid agentId)
    {
        // pass in the agent id
        var state = await _stateMachineService.CreateState(agentId);

        return new OkObjectResult(state);
    }

    [HttpPost]
    [Route("resume/travellers")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> ResumeTravellersTrip([FromBody] MultiSelectResumeModel multiSelectResumeModel)
    {
        // remove all repeated ids

        var travellerSingleton = await _singletonService.GetTravellerSingleton().GetData();

        var travellers = travellerSingleton.Where(t =>
            multiSelectResumeModel.Payload.Any(tid => tid == t.PersonId));
        
        var itinerary = await _stateMachineService.ResumeState(multiSelectResumeModel.TripId, travellers);

        var tripState = (await _dataStorageService.FetchTripState(multiSelectResumeModel.TripId));

        if (itinerary is null) return new OkObjectResult(tripState);

        return new OkObjectResult(itinerary);
    }

    [HttpGet]
    [Route("state/{tripId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetCurrentTripState([FromRoute] Guid tripId)
    {
        var state = await _dataStorageService.FetchTripState(tripId);

        return new OkObjectResult(state);
    }

    [HttpPost]
    [Route("next/{tripId:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> MoveToNextState(string tripId)
    {
        var state = await _stateMachineService.NextState(tripId);

        return new OkObjectResult(state);
    }
}