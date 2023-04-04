using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using PremiumTravelService.Api.DataStorage.FileHandling;
using PremiumTravelService.Api.Factory;
using PremiumTravelService.Api.Models.Options;
using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Services.DataStorage;

public class DataStorageService : IDataStorageService
{
    private readonly DataStorageOptions _dataStorageOptions;
    private readonly string _path;
    private readonly StorageFactory _storageFactory = new();
    private readonly FileHandler _fileHandler;

    public DataStorageService(IOptionsSnapshot<DataStorageOptions> options)
    {
        _dataStorageOptions = options.Value;
        _path = Path.GetFullPath(_dataStorageOptions.FilePath);
        _fileHandler = _storageFactory.CreateFileHandler(_dataStorageOptions.FileType);
    }

    public async Task Write(StorageData payload)
    {
        await _fileHandler.Write(payload, _path);
    }
    
    public async Task<StorageData> Read()
    {
        return await _fileHandler.Read(_path);
    }
}