using NUnit.Framework;
using System;
using PremiumTravelService.Api.Factory;
using PremiumTravelService.Api.DataStorage.FileHandling;

namespace PremiumTravelService.Api.NunitTests;

[TestFixture]
public class FactoryTests
{
    /// <summary>
    /// This test ensures that the create file handler method appropriately
    /// creates a JSON handler
    /// </summary>
    [Test]
    public void VerifyStorageFactoryFileHandlerCreatesJson()
    {
        var storageFactory = new StorageFactory();
        var jsonFileHandler = storageFactory.CreateFileHandler("json");

        Assert.AreEqual(jsonFileHandler.GetType(), new JsonFileHandler().GetType());
    }

    /// <summary>
    /// This test ensures that the create file handler method appropriately
    /// creates a XML handler
    /// </summary>
    [Test]
    public void VerifyStorageFactoryFileHandlerCreatesXml()
    {
        var storageFactory = new StorageFactory();
        var xmlFileHandler = storageFactory.CreateFileHandler("xml");

        Assert.AreEqual(xmlFileHandler.GetType(), new XmlFileHandler().GetType());
    }

    /// <summary>
    /// This test ensures that the create file handler method appropriately
    /// throws an exception when invalid args are given
    /// </summary>
    [Test]
    public void VerifyStorageFactoryFileHandlerThrowsExceptionWhenInvalidArg()
    {
        var storageFactory = new StorageFactory();

        Assert.Throws<InvalidOperationException>(() =>
        {
            storageFactory.CreateFileHandler("invalid");
        });
    }
}