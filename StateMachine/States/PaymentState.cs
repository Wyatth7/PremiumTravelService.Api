using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.StateMachine.States;

public class PaymentState : IState
{
    public Trip Process(Trip trip, object payload)
    {
        return trip;
    }
}