using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using PremiumTravelService.Api.Models.Options;
using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.Services.DataStorage;

public class DataStorageService : IDataStorageService
{
    private readonly DataStorageOptions _dataStorageOptions;
    private readonly string _path;

    public DataStorageService(IOptions<DataStorageOptions> options)
    {
        _dataStorageOptions = options.Value;
        _path = Path.GetFullPath(_dataStorageOptions.FilePath);
    }

    public async Task Write(Trip payload)
    {
        if (_dataStorageOptions.FileType == DataStorageType.Json.ToString())
        {
            await JsonWrite(payload);
        }
        else
        {
            await XmlWrite(payload);
        }
    }

    public async Task<Trip> Read()
    {
        if (_dataStorageOptions.FileType == DataStorageType.Json.ToString())
        {
            return await JsonRead();
        }
        else
        {
            return await XmlRead();
        }
    }
    
    private async Task JsonWrite(Trip payload) {
        var options = new JsonSerializerOptions {WriteIndented = true};
        await using FileStream writeStream = File.OpenWrite(_path);
        await JsonSerializer.SerializeAsync(writeStream, payload, options).ConfigureAwait(false); ;
        await writeStream.DisposeAsync().ConfigureAwait(false);
    }

    private async Task<Trip> JsonRead() {
        await using FileStream readStream = File.OpenRead(_path);
        var data = await JsonSerializer.DeserializeAsync<Trip>(readStream).ConfigureAwait(false);

        return data ?? new Trip();
    }
    
    private async Task XmlWrite(Trip payload)
    {
        var xml = new System.Xml.Serialization.XmlSerializer(payload.GetType());
        await using FileStream writeStream = File.OpenWrite(_path);
        xml.Serialize(writeStream, payload);
        await writeStream.DisposeAsync();
    }
    
    private async Task<Trip> XmlRead()
    {
        var xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Trip";
        xRoot.IsNullable = true;
        
        var xml = new System.Xml.Serialization.XmlSerializer(typeof(Trip), xRoot);
        await using FileStream readStream = File.OpenRead(_path);
        var xmlData = xml.Deserialize(readStream);
        
        if (xmlData is null) return new Trip();

        var testModelData = (Trip) xmlData;
        
        await readStream.DisposeAsync();
        
        return testModelData;
    }
}