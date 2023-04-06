using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Services.StateMachine;

namespace PremiumTravelService.Api.StateMachine.States;

public class CreateState : IState
{
    public Trip Process(Trip? _, object payload)
    {
        return new Trip {TripId = Guid.Parse((string)payload)};
    }
}