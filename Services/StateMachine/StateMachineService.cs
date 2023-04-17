using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.StateMachine;
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

    public async Task<TripState> NextState(string tripId)
    {
        return await _tripStateMachine.NextState(tripId);
    }

    public async Task<TripState> CreateState(Guid agentId)
    {
        // if there is a current state, null it and set a new one
        var state = await _tripStateMachine.CreateState(agentId);

        return state;
    }
}