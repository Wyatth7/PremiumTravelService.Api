using System.Text.Json;
using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.DataStorage.DataHandling;

public class JsonReadWrite : IFileReadWrite<Trip>
{
    private readonly string _path = System.IO.Path.GetFullPath(@"DataStorage/Storage/JsonStorage.json");
    
    public async Task Write(Trip payload) {
        var options = new JsonSerializerOptions {WriteIndented = true};
        await using FileStream writeStream = File.OpenWrite(_path);
        await JsonSerializer.SerializeAsync(writeStream, payload, options).ConfigureAwait(false); ;
        await writeStream.DisposeAsync().ConfigureAwait(false);
    }

    public async Task<Trip> Read() {
        await using FileStream readStream = File.OpenRead(_path);
        var data = await JsonSerializer.DeserializeAsync<Trip>(readStream).ConfigureAwait(false);

        return data ?? new Trip();
    }
}