using PremiumTravelService.Api.Persistence.Entities.StateMachine;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Services.DataStorage;

namespace PremiumTravelService.Api.StateMachine;

public class TripStateMachine
{
    private readonly IDataStorageService _dataStorageService;
    
    private StateType CurrentState { get; set; }

    private TripState TripState { get; set; }

    public Trip CurrentTrip { get; private set; }

    public TripStateMachine(IDataStorageService dataStorageService)
    {
        _dataStorageService = dataStorageService;
    }
    
    public async Task CreateState()
    {
        CurrentTrip = new Trip {TripId = Guid.NewGuid()};
        CurrentState = StateType.Create;

        TripState = new()
        {
            TripId = CurrentTrip.TripId,
            CurrentState = CurrentState,
            IsComplete = false,
            IsStarted = true
        };

        await SaveTripState();
    }
    
    public async Task ResumeState(int tripId)
    {
        
    }

    public async Task PauseState()
    {
        await SaveTripState();
    }

    public async Task NextState()
    {
        // save current state and 
    }
    
    private async Task<Trip> LoadTripState(Guid tripId)
    {
        throw new NotImplementedException();
    }

    private async Task SaveTripState()
    {
        var storageData = await _dataStorageService.Read();

        if (storageData.Trips.Any(trip => trip.TripId == CurrentTrip.TripId)
            && storageData.StateMachines.Any(sm => sm.TripId == CurrentTrip.TripId))
        {
            storageData.Trips.Remove(storageData.Trips
                .Single(trip => trip.TripId == CurrentTrip.TripId));
            
            storageData.StateMachines.Remove(storageData.StateMachines.Single(sm => sm.TripId == CurrentTrip.TripId));
        }
        
        storageData.Trips.Add(CurrentTrip);

        storageData.StateMachines.Add(TripState);

        await _dataStorageService.Write(storageData);
    }
}