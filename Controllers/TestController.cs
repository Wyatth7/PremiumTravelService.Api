using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Services.DataStorage;
using PremiumTravelService.Api.Services.Singleton;
using PremiumTravelService.Api.Services.StateMachine;

namespace PremiumTravelService.Api.Controllers;

public class TestController : BaseApiController
{

    private readonly IDataStorageService _dataStorageService;
    private readonly ISingletonService _singletonService;
    private readonly IStateMachineService _stateMachineService;
    
    public TestController(IDataStorageService dataStorageService,
        ISingletonService singletonService,
        IStateMachineService stateMachineService)
    {
        _dataStorageService = dataStorageService;
        _singletonService = singletonService;
        _stateMachineService = stateMachineService;
    }
    
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> TestJsonWrite([FromBody] StorageData storageData)
    {
        await _dataStorageService.Write(storageData);

        var readData = await _dataStorageService.Read();
        
        return new OkObjectResult(readData);
    }
    
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> TestSingletons()
    {
        var agentSingleton = _singletonService.GetAgentSingleton();

        var agents = await agentSingleton.GetData();

        var travellerSingleton = _singletonService.GetTravellerSingleton();

        var travellers = await travellerSingleton.GetData();

        var packageSingleton = _singletonService.GetPackageSingleton();

        var packages = await packageSingleton.GetData();
        
        return new OkObjectResult(new {Packages = packages, Travellers = travellers, Agents = agents});
    }

    [HttpGet]
    [Route("statemachine")]
    [Produces("application/json")]
    public async Task<IActionResult> TestStateMachine()
    {
        // await _stateMachineService.CreateState();

        await _stateMachineService.ResumeState(Guid.Parse("5a3cb3be-e55f-4468-a48e-8940b10741df"),
            new Person {PersonId = Guid.NewGuid(), FirstName = "wyatt", LastName = "Hardin"});
        
        var data = await _dataStorageService.Read();
        return new OkObjectResult(data);
    }
}