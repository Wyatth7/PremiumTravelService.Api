using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Dto.Trips;

public class AgentTripDataDto
{
    public Trip Trip { get; set; }
    public bool IsComplete { get; set; }
}