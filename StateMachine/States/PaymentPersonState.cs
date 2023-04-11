using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills;

namespace PremiumTravelService.Api.StateMachine.States;

public class PaymentPersonState : IState
{
    public Trip Process(Trip trip, object payload)
    {
        trip.Payment.AssignedToPerson = (Person) payload;

        return trip;
    }
}