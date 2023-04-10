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
    
    public async Task<Itinerary> ResumeState(Guid tripId, object payload)
    {
        try
        {
            return await _tripStateMachine.ResumeState(tripId, payload);
        }
        catch (Exception e)
        {
            return new();
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