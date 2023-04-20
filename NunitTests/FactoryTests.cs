using NUnit.Framework;
using System;
using PremiumTravelService.Api.Factory;
using PremiumTravelService.Api.DataStorage.FileHandling;

namespace PremiumTravelService.Api.NunitTests;

[TestFixture]
public class FactoryTests
{
    [Test]
    public void StorageFactoryFileHandlerCreates_JSON()
    {
        StorageFactory storageFactory = new StorageFactory();
        var jsonFileHandler = storageFactory.CreateFileHandler("json");

        Assert.AreEqual(jsonFileHandler.GetType(), new JsonFileHandler().GetType());
    }

    [Test]
    public void StorageFactoryFileHandlerCreates_XML()
    {
        StorageFactory storageFactory = new StorageFactory();
        var xmlFileHandler = storageFactory.CreateFileHandler("xml");

        Assert.AreEqual(xmlFileHandler.GetType(), new XmlFileHandler().GetType());
    }

    [Test]
    public void StorageFactoryFileHandlerThrowsExceptionWhenInvalidArg()
    {
        StorageFactory storageFactory = new StorageFactory();

        Assert.Throws<InvalidOperationException>(() =>
        {
            storageFactory.CreateFileHandler("invalid");
        });
    }
}