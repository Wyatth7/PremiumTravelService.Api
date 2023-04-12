using PremiumTravelService.Api.Persistence.Entities.Itinerary;

namespace PremiumTravelService.Api.Decorator;

public interface IItinerary
{
    Task<Itinerary> PopulateItinerary();
}