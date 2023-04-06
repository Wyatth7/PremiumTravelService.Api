using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Services.StateMachine;

public interface IStateMachineService
{
    Task<(Itinerary, bool)> ResumeState(Guid tripId);
    
    void NextState();

    Task CreateState();
}