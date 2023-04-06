using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.StateMachine.States;

public class TravellerState : IState
{
    public Trip Process(Trip trip, object payload)
    {
        trip.Travellers ??= Array.Empty<Person>();
        
        var travellers = trip.Travellers.ToList();
        
        travellers.Add((Person)payload);

        trip.Travellers = travellers.ToArray();

        return trip;
    }
}