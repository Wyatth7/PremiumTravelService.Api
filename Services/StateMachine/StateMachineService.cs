using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Services.DataStorage;
using PremiumTravelService.Api.StateMachine;

namespace PremiumTravelService.Api.Services.StateMachine;

public class StateMachineService : IStateMachineService
{
    private readonly TripStateMachine _tripStateMachine;

    public StateMachineService(IDataStorageService dataStorageService)
    {
        _tripStateMachine = new TripStateMachine(dataStorageService);
    }
    
    public async Task ResumeState(int tripId)
    {
        await _tripStateMachine.ResumeState(tripId);
    }

    public async Task PauseState()
    {
        await _tripStateMachine.PauseState();
    }

    public async Task NextState()
    {
        await _tripStateMachine.NextState();
    }

    public async Task CreateState()
    {
        // if there is a current state, null it and set a new one
        await _tripStateMachine.CreateState();
    }

    public Trip GetCurrentStateObject()
    {
        return _tripStateMachine.CurrentTrip;
    }
}