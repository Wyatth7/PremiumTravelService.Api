using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.Services.DataStorage;

public interface IDataStorageService
{
    Task Write(Trip payload);
    Task<Trip> Read();
}