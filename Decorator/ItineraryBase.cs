using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator;

public abstract class ItineraryBase : IItinerary
{

    // public Persistence.Entities.Itinerary.Itinerary Itinerary = new();
    public abstract Task<Itinerary> PopulateItinerary(Trip trip, Itinerary itinerary);
}