using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.StateMachine.States;

public class NoteState : IState
{
    public Trip Process(Trip trip, object payload)
    {
        trip.ThankYouNote = (string) payload;
        return trip;
    }
}