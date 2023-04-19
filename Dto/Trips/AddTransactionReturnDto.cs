using PremiumTravelService.Api.Persistence.Entities.StateMachine;

namespace PremiumTravelService.Api.Dto.Trips;

public class AddTransactionReturnDto : TripState
{
    public decimal RemainingTripBalance { get; set; }
}