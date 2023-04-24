using NUnit.Framework;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Singleton;

namespace PremiumTravelService.Api.NunitTests;

[TestFixture]
public class SingletonTests
{
    /// <summary>
    /// This test ensures that the traveller singleton contains valid data
    /// </summary>
    [Test]
    public async Task VerifyTravellerSingletonContainsData()
    {
        var ts = TravellerSingleton.GetInstance();

        var data = await ts.GetData();

        Assert.AreEqual(ts.ToString(), "PremiumTravelService.Api.Singleton.TravellerSingleton");
        Assert.IsTrue(data.Any());
        Assert.AreEqual(data.ElementAt(0).LastName, "Hardin");
        Assert.AreEqual(data.ElementAt(1).LastName, "ismon");
    }

    /// <summary>
    /// This test ensures that the package singleton contains valid data
    /// </summary>
    [Test]
    public async Task VerifyPackageSingletonContainsData()
    {
        var ps = PackageSingleton.GetInstance();

        var data = await ps.GetData();

        Assert.AreEqual(ps.ToString(), "PremiumTravelService.Api.Singleton.PackageSingleton");
        Assert.IsTrue(data.Any());
        Assert.AreEqual(data.ElementAt(0).TripDetailId.ToString(), "d2b668df-6106-4cd1-94ca-dff0f2f2ee22");
        Assert.AreEqual(data.ElementAt(1).TripDetailId.ToString(), "e27fbaa6-8fa2-4f4d-9da7-307781bb7115");
    }


    /// <summary>
    /// This test ensures that the agent singleton contains valid data
    /// </summary>
    [Test]
    public async Task VerifyAgentSingletonContainsData()
    {
        var ags = AgentSingleton.GetInstance();

        var data = await ags.GetData();

        Assert.AreEqual(ags.ToString(), "PremiumTravelService.Api.Singleton.AgentSingleton");
        Assert.IsTrue(data.Any());
        Assert.AreEqual(data.ElementAt(0).LastName, "Crowley");
    }
}