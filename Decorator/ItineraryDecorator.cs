using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator;

public class ItineraryDecorator : IItinerary
{
    protected IItinerary Itinerary;

    ItineraryDecorator(IItinerary itinerary)
    {
        Itinerary = itinerary;
    }
    
    public Task ShowItinerary()
    {
        throw new NotImplementedException();
    }
}