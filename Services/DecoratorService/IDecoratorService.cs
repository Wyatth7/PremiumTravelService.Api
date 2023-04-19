using PremiumTravelService.Api.Persistence.Entities.Itinerary;

namespace PremiumTravelService.Api.Services.DecoratorService;

public interface IDecoratorService
{
    Task<Itinerary> CreateItinerary(Guid tripId);
}