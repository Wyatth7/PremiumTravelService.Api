using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Dto.Trips;

public class AgentTripDataDto
{
    public Guid TripId { get; set; }
    public bool IsComplete { get; set; }
}