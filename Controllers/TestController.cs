using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.DataStorage.DataHandling;
using PremiumTravelService.Api.Persistence;
using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.Controllers;

public class TestController : BaseApiController
{
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> TestJsonWrite([FromBody] Trip trip)
    {
        var jsonReaderWriter = new JsonReadWrite();
        
        await jsonReaderWriter.Write(trip);
        
        var readData = await jsonReaderWriter.Read();

        // var xmlReaderWriter = new XmlReadWrite();
        //
        // await xmlReaderWriter.Write(trip);
        //
        // var readData = await xmlReaderWriter.Read();
        //
        return new OkObjectResult(readData);
    }
}