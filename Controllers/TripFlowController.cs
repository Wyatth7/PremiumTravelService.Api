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
    
    /// <summary>
    /// Create a trip
    /// </summary>
    /// <param name="agentId">ID of agent</param>
    /// <returns>State of trip</returns>
    [HttpPost]
    [Route("create/{agentId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> CreateTrip([FromRoute] Guid agentId)
    {
        // pass in the agent id
        var state = await _stateMachineService.CreateState(agentId);

        return new OkObjectResult(state);
    }

    /// <summary>
    /// Resumes trip at traveler state
    /// </summary>
    /// <param name="multiSelectResumeModel">multi select dto data</param>
    /// <returns>trip state or itinerary</returns>
    [HttpPost]
    [Route("resume/travellers")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> ResumeTravellersTrip([FromBody] MultiSelectResumeModel multiSelectResumeModel)
    {
        // remove all repeated ids
        var currentTravellers = await _dataStorageService.GetTravellersForTrip(multiSelectResumeModel.TripId);

        if (currentTravellers is not null)
        {
            multiSelectResumeModel.Payload =
                multiSelectResumeModel.Payload
                    .Where(p =>
                        !currentTravellers
                            .Select(t => t.PersonId)
                            .Contains(p));
        }

        if (!multiSelectResumeModel.Payload.Any()) return new BadRequestResult();
        
        // assign new travellers
        var travellerSingleton = await _singletonService
            .GetTravellerSingleton()
            .GetData();

        var travellers = travellerSingleton.Where(t =>
            multiSelectResumeModel.Payload.Any(tid => tid == t.PersonId));
        
        var itinerary = await _stateMachineService.ResumeState(multiSelectResumeModel.TripId, travellers);

        var tripState = (await _dataStorageService.GetTripState(multiSelectResumeModel.TripId));

        if (itinerary is null) return new OkObjectResult(tripState);

        return new OkObjectResult(itinerary);
    }

    /// <summary>
    /// Resumes trip at package state
    /// </summary>
    /// <param name="multiSelectResumeModel">multi select dto data</param>
    /// <returns>trip state or itinerary</returns>
    [HttpPost]
    [Route("resume/packages")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> ResumePackageTrip([FromBody] MultiSelectResumeModel multiSelectResumeModel)
    {
        // remove all repeated packages
        var currentPackages = await _dataStorageService.GetPackagesForTrip(multiSelectResumeModel.TripId);

        if (currentPackages is not null)
        {
            multiSelectResumeModel.Payload =
                multiSelectResumeModel.Payload
                    .Where(p =>
                        !currentPackages
                            .Select(t => t.TripDetailId)
                            .Contains(p));
        }

        if (!multiSelectResumeModel.Payload.Any()) return new BadRequestResult();
        
        var packageSingleton = await _singletonService.GetPackageSingleton().GetData();

        var packages = packageSingleton.Where(p =>
            multiSelectResumeModel.Payload.Any(detailId => detailId == p.TripDetailId))
            .OrderBy(p => p.TripStart)
            .ToArray();

        await _stateMachineService.ResumeState(multiSelectResumeModel.TripId, packages);
        
        var tripState = (await _dataStorageService.GetTripState(multiSelectResumeModel.TripId));

        return new OkObjectResult(tripState);
    }

    /// <summary>
    /// Resumes trip at assign person state
    /// </summary>
    /// <param name="multiSelectResumeModel">multi select dto data</param>
    /// <returns>trip state or itinerary</returns>
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

    /// <summary>
    /// Resumes trip at person state
    /// </summary>
    /// <param name="tripPaymentTransactionDto">transaction DTO data</param>
    /// <returns>trip state or itinerary</returns>
    [HttpPost]
    [Route("resume/payment")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> AddTransaction([FromBody] TripPaymentTransactionDto tripPaymentTransactionDto)
    {
        var transactionModel = new TransactionModel
        {
            PaymentType = tripPaymentTransactionDto.PaymentType,
            Total = tripPaymentTransactionDto.Amount,
            Card = tripPaymentTransactionDto.Card,
            Cash = tripPaymentTransactionDto.Cash,
            Check = tripPaymentTransactionDto.Check,
            Loan = tripPaymentTransactionDto.Loan
        };

        await _stateMachineService.ResumeState(tripPaymentTransactionDto.TripId, transactionModel);

        var totalRemaining = await _dataStorageService.GetRemainingTripBalance(tripPaymentTransactionDto.TripId);

        // if remaining balance is less than or equal
        // to 0, go to next state
        // else, get current state
        var state = totalRemaining <= 0
            ? await _stateMachineService.NextState(tripPaymentTransactionDto.TripId.ToString())
            : await _dataStorageService.GetTripState(tripPaymentTransactionDto.TripId);
        
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

    /// <summary>
    /// Resumes trip at thank you note state
    /// </summary>
    /// <param name="multiSelectResumeModel">multi select dto data</param>
    /// <returns>trip state or itinerary</returns>
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

    /// <summary>
    /// Creates an itinerary for a trip
    /// </summary>
    /// <param name="tripId">ID of trip</param>
    /// <returns>An itinerary of a trip</returns>
    [HttpPost]
    [Route("resume/itinerary/{tripId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> CreateItinerary([FromRoute] Guid tripId)
    {
        var itinerary = await _stateMachineService.ResumeState(tripId, null);

        return new OkObjectResult(itinerary);
    }

    /// <summary>
    /// Gets current state of a trip
    /// </summary>
    /// <param name="tripId">ID of a trip</param>
    /// <returns>TripState object of specified trip</returns>
    [HttpGet]
    [Route("state/{tripId:Guid:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetCurrentTripState([FromRoute] Guid tripId)
    {
        var state = await _dataStorageService.GetTripState(tripId);

        return new OkObjectResult(state);
    }

    /// <summary>
    /// Moves trip to next state
    /// </summary>
    /// <param name="tripId">ID of a trip</param>
    /// <returns>state of a trip</returns>
    [HttpPost]
    [Route("next/{tripId:required}")]
    [Produces("application/json")]
    public async Task<IActionResult> MoveToNextState(string tripId)
    {
        var state = await _stateMachineService.NextState(tripId);

        return new OkObjectResult(state);
    }
}