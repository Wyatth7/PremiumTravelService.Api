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
    
    public async Task<(Itinerary, bool)> ResumeState(Guid tripId)
    {
        try
        {
            var isCompleted = await _tripStateMachine.ResumeState(tripId);
            
            // get completed state (not implemented yet)

            return (new(), true);
        }
        catch (Exception e)
        {
            return (new(), false);
        }
    }

    public void NextState()
    {
        _tripStateMachine.NextState();
    }

    public async Task CreateState()
    {
        // if there is a current state, null it and set a new one
        await _tripStateMachine.CreateState();
    }
}