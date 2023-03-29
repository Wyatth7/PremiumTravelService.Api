using System.Text.Json;
using PremiumTravelService.Api.Persistence;

namespace PremiumTravelService.Api.DataStorage.DataHandling;

public class JsonReadWrite : IFileReadWrite<TestModel>
{
    private readonly string _path = System.IO.Path.GetFullPath(@"DataStorage/Storage/JsonStorage.json");
    
    public async Task Write(TestModel payload) {
        var options = new JsonSerializerOptions {WriteIndented = true};
        await using FileStream writeStream = File.OpenWrite(_path);
        await JsonSerializer.SerializeAsync(writeStream, payload, options).ConfigureAwait(false); ;
        await writeStream.DisposeAsync().ConfigureAwait(false);
    }

    public async Task<TestModel> Read() {
        await using FileStream readStream = File.OpenRead(_path);
        var data = await JsonSerializer.DeserializeAsync<TestModel>(readStream).ConfigureAwait(false);

        return data ?? new TestModel();
    }
}