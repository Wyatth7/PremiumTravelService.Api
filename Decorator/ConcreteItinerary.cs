using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator;

public class ConcreteItinerary : ItineraryBase
{
    public override async Task<Itinerary> PopulateItinerary(Trip _, Itinerary itinerary)
    {
        return itinerary;
    }
}