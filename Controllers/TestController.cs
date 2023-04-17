using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Decorator;
using PremiumTravelService.Api.Decorator.Decorators;
using PremiumTravelService.Api.Models.Payment;
using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CardInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.ChargeInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CheckInformation;
using PremiumTravelService.Api.Persistence.Enums;
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
    [Route("read")]
    [Produces("application/json")]
    public async Task<IActionResult> TestJsonRead()
    {
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
        //create
        var storageData = await _dataStorageService.Read();
        await _stateMachineService.CreateState(Guid.NewGuid());
        
        // _stateMachineService.NextState();

        // travellers addition
        //  var storageData = await _dataStorageService.Read();
        //  await _stateMachineService.ResumeState(
        //      storageData.Trips
        //          .Select(t => t.TripId)
        //          .First(t => t.Equals(Guid.Parse("b6d8cd41-41b7-4383-8546-850ee6fea21c"))),
        //      storageData.Travellers[0]);
        // _stateMachineService.NextState();

        //packages addition
        // var storageData = await _dataStorageService.Read();
        // var package = storageData.Packages
        //     .FirstOrDefault(p => 
        //         p.TripDetailId == Guid.Parse("8a3cb3be-e55f-4468-a48e-8940b10741df"));
        // await _stateMachineService.ResumeState(Guid.Parse("b6d8cd41-41b7-4383-8546-850ee6fea21c"),
        //     package);
        
        //payment person addition
        // var storageData = await _dataStorageService.Read();
        // await _stateMachineService.ResumeState(Guid.Parse("b6d8cd41-41b7-4383-8546-850ee6fea21c"),
        //     storageData.Travellers
        //         .First(t => t.PersonId.Equals(Guid.Parse("8a3cb3be-e55f-4468-a48e-8940b10741df"))));
        
        //payment addition
        // var storageData = await _dataStorageService.Read();
        // await _stateMachineService.ResumeState(Guid.Parse("b6d8cd41-41b7-4383-8546-850ee6fea21c"),
        //     new TransactionModel
        //     {
        //         PersonId = Guid.Parse("8a3cb3be-e55f-4468-a48e-8940b10741df"),
        //         PaymentType = PaymentType.Cash,
        //         Cash = new Cash(),
        //         Total = 600
        //     });
        
        // new TransactionModel
        // {
        //     PersonId = Guid.Parse("8a3cb3be-e55f-4468-a48e-8940b10741df"),
        //     PaymentType = PaymentType.Card,
        //     Card = new Card()
        //     {
        //         CardId = Guid.NewGuid(),
        //         CardNumber = "4444555566667777",
        //         ExpirationDate = DateTimeOffset.Now,
        //         NameOnCard = "wyatt",
        //         SecurityCode = "333",
        //         Address = new Address
        //         {
        //             AddressId = Guid.NewGuid(),
        //             City = "Chickamauga",
        //             State = "ga",
        //             Street = "test street 123",
        //             ZipCode = "30707"
        //         },
        //     },
        //     Total = 150
        // }
        
        // var storageData = await _dataStorageService.Read();
        // await _stateMachineService.ResumeState(Guid.Parse("b6d8cd41-41b7-4383-8546-850ee6fea21c"),
        //     "This is a thank you note");
        //
        var data = await _dataStorageService.Read();
        return new OkObjectResult(data);
    }

    [HttpGet]
    [Route("decorator")]
    [Produces("application/json")]
    public async Task<IActionResult> TestDecorator()
    {
        var simple = new ConcreteItinerary();

        var travellers = new TravellerDecorator(simple);
        var details = new TripDetailsDecorator(travellers);
        var billing = new BillingDecorator(details);
        var booking = new BookingDecorator(billing);

        var storageData = await _dataStorageService.Read();
        
        var itinerary = await booking
            .PopulateItinerary(storageData.Trips.First(),
                new Itinerary());
        
        return new OkObjectResult(itinerary);
    }
}