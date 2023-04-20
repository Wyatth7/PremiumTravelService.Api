using NUnit.Framework;

namespace PremiumTravelService.Api.NunitTests;

[TestFixture]
public class FactoryTests
{
    [Test]
    public void StorageFactoryFileHandler_JSON()
    {
        StorageFactory sf = new StorageFactory();
        var fileHandler = sf.CreateFileHandler("json");

        Assert.Equal(fileHandler, new jsonFileHandler);
    }
}