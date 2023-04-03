namespace PremiumTravelService.Api.Persistence.Entities.StateMachine;

public class TripState
{
    public Guid TripId { get; set; }
    public string CurrentStateName { get; set; }
    public bool IsComplete { get; set; }
}