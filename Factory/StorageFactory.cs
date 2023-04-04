using PremiumTravelService.Api.DataStorage.FileHandling;
using PremiumTravelService.Api.Models.Options;

namespace PremiumTravelService.Api.Factory;

/// <summary>
/// Storage factory for creating xml or json
/// file handlers.
/// </summary>
public class StorageFactory
{
    /// <summary>
    /// Creates an instance of subclass of FileHandler. 
    /// </summary>
    /// <param name="classToCreate">string name of class to create.</param>
    /// <returns>instance of json file handler or xml file handler.</returns>
    /// <exception cref="InvalidOperationException">throws if class to create is not xml or json.</exception>
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