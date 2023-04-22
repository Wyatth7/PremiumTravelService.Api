using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Singleton;

namespace PremiumTravelService.Api.Controllers;

[Route("api/singleton")]
public class SingletonController : BaseApiController
{
    
    /// <summary>
    /// Gets list of agents
    /// </summary>
    /// <returns>List of agents</returns>
    [HttpGet]
    [Route("agents")]
    [Produces("application/json")]
    public async Task<IActionResult> GetAgents()
    {
        var agents = await AgentSingleton.GetInstance().GetData();

        return new OkObjectResult(agents);
    }
    
    
    /// <summary>
    /// Gets list of travelers 
    /// </summary>
    /// <returns>List of travelers</returns>
    [HttpGet]
    [Route("travellers")]
    [Produces("application/json")]
    public async Task<IActionResult> GetTravellers()
    {
        var travellers = await TravellerSingleton.GetInstance().GetData();

        return new OkObjectResult(travellers);
    }

    /// <summary>
    /// Get a list of packages
    /// </summary>
    /// <returns>list of packages</returns>
    [HttpGet]
    [Route("packages")]
    [Produces("application/json")]
    public async Task<IActionResult> GetPackages()
    {
        var packages = await PackageSingleton.GetInstance().GetData();

        return new OkObjectResult(packages);
    }
}