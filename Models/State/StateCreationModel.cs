namespace PremiumTravelService.Api.Models.State;

public class StateCreationModel
{
    public Guid TripId { get; set; }
    public Guid AssignedByPersonId { get; set; }
}