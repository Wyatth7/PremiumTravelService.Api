using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Singleton;

namespace PremiumTravelService.Api.Controllers;

[Route("api/singleton")]
public class SingletonController : BaseApiController
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

    [HttpGet]
    [Route("packages")]
    [Produces("application/json")]
    public async Task<IActionResult> GetPackages()
    {
        var packages = await PackageSingleton.GetInstance().GetData();

        return new OkObjectResult(packages);
    }
}