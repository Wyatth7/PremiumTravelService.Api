using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Services.StateMachine;

public interface IStateMachineService
{
    Task<(Itinerary, bool)> ResumeState(Guid tripId, object payload);
    
    void NextState();

    Task CreateState();
}