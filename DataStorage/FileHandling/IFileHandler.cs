using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.DataStorage.FileHandling;

/// <summary>
/// File handling interface.
/// Used for blue print of reading/writing to files.
/// </summary>
public interface IFileHandler
{
    /// <summary>
    /// Reads a file
    /// </summary>
    /// <param name="path">path of file</param>
    /// <returns>storage data or null</returns>
    Task<StorageData> Read(string path);
    /// <summary>
    /// Writes to a file
    /// </summary>
    /// <param name="storageData">data to write to file</param>
    /// <param name="path">path of file</param>
    /// <returns></returns>
    Task Write(StorageData storageData, string path);
}