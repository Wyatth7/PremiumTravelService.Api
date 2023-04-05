using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Services.StateMachine;

public interface IStateMachineService
{
    Task PauseState();
    
    Task ResumeState(int tripId);
    
    Task NextState();

    Task CreateState();

    Trip GetCurrentStateObject();
}