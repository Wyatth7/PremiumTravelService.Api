using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.StateMachine;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.StateMachine;

namespace PremiumTravelService.Api.Services.StateMachine;

public interface IStateMachineService
{
    /// <summary>
    /// Resumes a trip state
    /// </summary>
    /// <param name="tripId">Id of trip to resume</param>
    /// <param name="payload">Data to send to state</param>
    /// <returns>Itinerary if state is completed, true if complete</returns>
    Task<Itinerary> ResumeState(Guid tripId, object payload);
    
    /// <summary>
    /// Move to next state
    /// </summary>
    Task<TripState> NextState(string tripId);

    /// <summary>
    /// Creates a trip state
    /// </summary>
    /// <returns></returns>
    Task<StateType> CreateState(Guid agentId);
}