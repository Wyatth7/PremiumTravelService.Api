using PremiumTravelService.Api.Persistence;
using PremiumTravelService.Api.Persistence.Entities;

namespace PremiumTravelService.Api.DataStorage.DataHandling;

public class XmlReadWrite : IFileReadWrite<Trip>
{
    private string _path = Path.GetFullPath(@"DataStorage/Storage/XmlStorage.xml");
    
    public async Task Write(Trip payload)
    {
        var xml = new System.Xml.Serialization.XmlSerializer(payload.GetType());
        await using FileStream writeStream = File.OpenWrite(_path);
        xml.Serialize(writeStream, payload);
        await writeStream.DisposeAsync();
    }
    
    public async Task<Trip> Read()
    {
        var xml = new System.Xml.Serialization.XmlSerializer(typeof(TestModel));
        await using FileStream readStream = File.OpenRead(_path);
        var xmlData = xml.Deserialize(readStream);
        
        if (xmlData is null) return new Trip();

        var testModelData = (Trip) xmlData;
        
        await readStream.DisposeAsync();
        
        return testModelData;
    }
}