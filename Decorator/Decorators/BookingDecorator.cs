using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator.Decorators;

public class BookingDecorator : ItineraryDecorator
{
    public BookingDecorator(ItineraryBase itinerary) : base(itinerary)
    {
    }
    
    public override async Task<Itinerary> PopulateItinerary(Trip trip, Itinerary itinerary)
    {
        itinerary.ThankYouNote = trip.ThankYouNote;
        return await base.PopulateItinerary(trip, itinerary);
    }
}