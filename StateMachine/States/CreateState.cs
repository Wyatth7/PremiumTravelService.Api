using PremiumTravelService.Api.Models.State;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Services.StateMachine;

namespace PremiumTravelService.Api.StateMachine.States;

public class CreateState : IState
{
    public Trip Process(Trip? _, object payload)
    {
        var stateCreationModel = (StateCreationModel)payload;
        
        
        return new Trip
        {
            TripId = stateCreationModel.TripId,
            AssignedByPersonId = stateCreationModel.AssignedByPersonId
        };
    }
}