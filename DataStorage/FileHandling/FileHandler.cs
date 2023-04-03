using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.DataStorage.FileHandling;

public abstract class FileHandler : IFileHandler
{
    public abstract Task<StorageData> Read(string path);
    public abstract Task Write(StorageData storageData, string path);
}