using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Services.DataStorage;

public interface IDataStorageService
{
    Task Write(StorageData payload);
    Task<StorageData> Read();
}