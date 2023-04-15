using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Services.StateMachine;

namespace PremiumTravelService.Api.Controllers;

[Route("api/trip")]
public class TripFlowController : BaseApiController
{
    private readonly IStateMachineService _stateMachineService;

    public TripFlowController(IStateMachineService stateMachineService)
    {
        _stateMachineService = stateMachineService;
    }
    
    
    [HttpPost]
    [Route("create/{agentId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> CreateTrip([FromRoute] Guid agentId)
    {
        // pass in the agent id
        await _stateMachineService.CreateState(agentId);

        return Ok();
    }
}