using PremiumTravelService.Api.StateMachine;

namespace PremiumTravelService.Api.Persistence.Entities.StateMachine;

public class TripState
{
    public Guid TripId { get; set; }
    public StateType CurrentState { get; set; }
    public bool IsComplete { get; set; } = false;
    public bool IsStarted { get; set; } = false;
}