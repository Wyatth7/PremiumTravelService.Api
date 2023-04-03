using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Services.DataStorage;

namespace PremiumTravelService.Api.Controllers;

public class TestController : BaseApiController
{

    private readonly IDataStorageService _dataStorageService;

    public TestController(IDataStorageService dataStorageService)
    {
        _dataStorageService = dataStorageService;
    }
    
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> TestJsonWrite([FromBody] Trip trip)
    {
        await _dataStorageService.Write(trip);

        var readData = await _dataStorageService.Read();
        
        return new OkObjectResult(readData);
    }
}