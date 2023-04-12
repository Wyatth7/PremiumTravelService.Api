using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator.Decorators;

public class TravellerDecorator : ItineraryDecorator
{
    public TravellerDecorator(ItineraryBase itineraryBase) : base(itineraryBase)
    {
        
    }
    
    public override async Task<Itinerary> PopulateItinerary(Trip trip, Itinerary itinerary)
    {
        var travellers = trip.Travellers
            .Select(t => t.NameFull).ToArray();
        
        // base.ItineraryBase.Itinerary.Travellers = travellers;
        itinerary.Travellers = travellers;
        return await base.PopulateItinerary(trip, itinerary);
    }
}