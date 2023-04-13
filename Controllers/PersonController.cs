using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Singleton;

namespace PremiumTravelService.Api.Controllers;

public class PersonController : BaseApiController
{
    [HttpGet]
    [Route("agents")]
    [Produces("application/json")]
    public async Task<IActionResult> GetAgents()
    {
        var agents = await AgentSingleton.GetInstance().GetData();

        return new OkObjectResult(agents);
    }

    [HttpGet]
    [Route("travellers")]
    [Produces("application/json")]
    public async Task<IActionResult> GetTravellers()
    {
        var travellers = await TravellerSingleton.GetInstance().GetData();

        return new OkObjectResult(travellers);
    }
}