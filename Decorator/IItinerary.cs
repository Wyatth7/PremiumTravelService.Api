using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator;

public interface IItinerary
{
    Task<Itinerary> PopulateItinerary(Trip trip, Itinerary tripDetails);
}