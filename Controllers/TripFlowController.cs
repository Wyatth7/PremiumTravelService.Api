using Microsoft.AspNetCore.Mvc;
using PremiumTravelService.Api.Dto;
using PremiumTravelService.Api.Dto.Trips;
using PremiumTravelService.Api.Models.Payment;
using PremiumTravelService.Api.Models.State;
using PremiumTravelService.Api.Persistence.Entities.StateMachine;
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

    [HttpPost]
    [Route("resume/packages")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> ResumePackageTrip([FromBody] MultiSelectResumeModel multiSelectResumeModel)
    {
        var packageSingleton = await _singletonService.GetPackageSingleton().GetData();

        var packages = packageSingleton.Where(p =>
            multiSelectResumeModel.Payload.Any(detailId => detailId == p.TripDetailId))
            .OrderBy(p => p.TripStart)
            .ToArray();

        await _stateMachineService.ResumeState(multiSelectResumeModel.TripId, packages);
        
        var tripState = (await _dataStorageService.FetchTripState(multiSelectResumeModel.TripId));

        return new OkObjectResult(tripState);
    }

    [HttpPost]
    [Route("resume/payment/assignPerson")]
    [Produces("application/json")]
    public async Task<IActionResult> AssignPaymentPerson([FromBody] AddPaymentPersonDto addPaymentPersonDto)
    {
        var person = await _singletonService
            .GetTravellerSingleton()
            .GetData();

        var assignToPerson = person.FirstOrDefault(p => p.PersonId == addPaymentPersonDto.AssignToPersonId);

        await _stateMachineService.ResumeState(addPaymentPersonDto.TripId, assignToPerson);

        var tripState = await _stateMachineService.NextState(addPaymentPersonDto.TripId.ToString());

        return new OkObjectResult(tripState);
    }

    [HttpPost]
    [Route("resume/payment")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> AddTransaction([FromBody] TripPaymentTransactionDto tripPaymentTransactionDto)
    {
        var transactionModel = new TransactionModel
        {
            PersonId = tripPaymentTransactionDto.PersonId,
            PaymentType = tripPaymentTransactionDto.PaymentType,
            Total = tripPaymentTransactionDto.Amount,
            Card = tripPaymentTransactionDto.Card,
            Cash = tripPaymentTransactionDto.Cash,
            Check = tripPaymentTransactionDto.Check
        };

        await _stateMachineService.ResumeState(tripPaymentTransactionDto.TripId, transactionModel);

        var totalRemaining = await _dataStorageService.FetchRemainingTripBalance(tripPaymentTransactionDto.TripId);

        // if remaining balance is less than or equal
        // to 0, go to next state
        // else, get current state
        var state = totalRemaining <= 0
            ? await _stateMachineService.NextState(tripPaymentTransactionDto.TripId.ToString())
            : await _dataStorageService.FetchTripState(tripPaymentTransactionDto.TripId);
        
        return totalRemaining > 0 ? 
                new OkObjectResult(new AddTransactionReturnDto
                {
                    RemainingTripBalance = totalRemaining,
                    TripId = state.TripId,
                    IsComplete = state.IsComplete,
                    CurrentState = state.CurrentState
                })
            : new OkObjectResult(state);
    }

    [HttpPost]
    [Route("resume/thankYouNote")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> AddThankYouNote([FromBody] AddThankYouNoteDto addThankYouNoteDto)
    {
        await _stateMachineService.ResumeState(addThankYouNoteDto.TripId, addThankYouNoteDto.ThankYouNote);

        await _stateMachineService.NextState(addThankYouNoteDto.TripId.ToString());

        return new OkResult();
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