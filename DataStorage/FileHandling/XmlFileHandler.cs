using System.Xml.Serialization;
using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.DataStorage.FileHandling;

public class XmlFileHandler : FileHandler
{
    public override async Task Write(StorageData payload, string path)
    {
        var xml = new System.Xml.Serialization.XmlSerializer(payload.GetType());
        await using var writeStream = File.OpenWrite(path);
        xml.Serialize(writeStream, payload);
        await writeStream.DisposeAsync();
    }
    
    public override async Task<StorageData> Read(string path)
    {
        var xRoot = new XmlRootAttribute();
        xRoot.ElementName = "StorageData";
        xRoot.IsNullable = true;
        
        var xml = new System.Xml.Serialization.XmlSerializer(typeof(StorageData), xRoot);
        await using var readStream = File.OpenRead(path);
        var xmlData = xml.Deserialize(readStream);
        
        if (xmlData is null) return new StorageData();

        var tripData = (StorageData) xmlData;
        
        await readStream.DisposeAsync();
        
        return tripData;
    }
}