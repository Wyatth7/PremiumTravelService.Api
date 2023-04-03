using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.DataStorage.FileHandling;

public interface IFileHandler
{
    Task<StorageData> Read(string path);
    Task Write(StorageData storageData, string path);
}