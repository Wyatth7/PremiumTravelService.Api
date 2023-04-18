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
        while (IsFileLocked(new FileInfo(path)))
        {
            Console.WriteLine("waiting");
        }
        
        try
        {
            await File.WriteAllTextAsync(path, "");
            var options = new JsonSerializerOptions {WriteIndented = true};
            await using var writeStream = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.Write);
            await JsonSerializer.SerializeAsync(writeStream, payload, options).ConfigureAwait(false); ;
            await writeStream.DisposeAsync();

        }
        catch (IOException e)
        {
            Console.WriteLine("catch");
            throw;
        }
    }

    public override async Task<StorageData> Read(string path) {
        while (IsFileLocked(new FileInfo(path)))
        {
            Console.WriteLine("waiting");
        }
        
        await using var readStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        var data = await JsonSerializer.DeserializeAsync<StorageData>(readStream).ConfigureAwait(false);

        await readStream.DisposeAsync();
        return data ?? new StorageData();
    }
    
   public static bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
        return false;
    }
}