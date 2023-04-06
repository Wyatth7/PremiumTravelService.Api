using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.StateMachine.States;

public interface IState
{
    Trip Process(Trip trip, object payload);
    // Task<Trip> ProcessAsync(Trip trip, object payload);
}