using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.DataStorage.FileHandling;

/// <summary>
/// Abstract file handling class.
/// Used for reading/writing to files.
/// </summary>
public abstract class FileHandler : IFileHandler
{
    public abstract Task<StorageData> Read(string path);
    public abstract Task Write(StorageData storageData, string path);
}