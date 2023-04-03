using PremiumTravelService.Api.DataStorage.FileHandling;
using PremiumTravelService.Api.Models.Options;

namespace PremiumTravelService.Api.Factory;

public class StorageFactory
{
    public FileHandler CreateFileHandler(string classToCreate)
    {
        if (classToCreate.Equals(DataStorageType.Xml.ToString().ToLower()))
        {
            return new XmlFileHandler();
        }
        
        if (classToCreate.Equals(DataStorageType.Json.ToString().ToLower()))
        {
            return new JsonFileHandler();
        }

        throw new InvalidOperationException(classToCreate + " is not a valid member of StorageFactory.");
    }
}