using System.Text.Json;
using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.DataStorage.FileHandling;

/// <summary>
/// Reads and writes data to json file.
/// Subclass of <see cref="FileHandler"/>.
/// </summary>
public class JsonFileHandler : FileHandler
{
    public override async Task Write(StorageData payload, string path) {
        await File.WriteAllTextAsync(path, "");
        var options = new JsonSerializerOptions {WriteIndented = true};
        await using var writeStream = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
        await JsonSerializer.SerializeAsync(writeStream, payload, options).ConfigureAwait(false); ;
        await writeStream.DisposeAsync();
    }

    public override async Task<StorageData> Read(string path) {
        await using var readStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var data = await JsonSerializer.DeserializeAsync<StorageData>(readStream).ConfigureAwait(false);

        await readStream.DisposeAsync();
        return data ?? new StorageData();
    }
}