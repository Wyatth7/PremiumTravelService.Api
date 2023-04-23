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
    public async Task verifyTravellerSingletonContainsData()
    {
        TravellerSingleton ts = TravellerSingleton.GetInstance();

        var data = await ts.GetData();

        Assert.AreEqual(ts.ToString(), "PremiumTravelService.Api.Singleton.TravellerSingleton");
        Assert.IsTrue(data.Count() > 0);
        Assert.AreEqual(data.ElementAt(0).LastName, "Hardin");
        Assert.AreEqual(data.ElementAt(1).LastName, "ismon");
    }

    /// <summary>
    /// This test ensures that the package singleton contains valid data
    /// </summary>
    [Test]
    public async Task verifyPackageSingletonContainsData()
    {
        PackageSingleton ps = PackageSingleton.GetInstance();

        var data = await ps.GetData();

        Assert.AreEqual(ps.ToString(), "PremiumTravelService.Api.Singleton.PackageSingleton");
        Assert.IsTrue(data.Count() > 0);
        Assert.AreEqual(data.ElementAt(0).TripDetailId.ToString(), "8a3cb3be-e55f-4468-a48e-8940b10741df");
        Assert.AreEqual(data.ElementAt(1).TripDetailId.ToString(), "8a3cb3be-e55f-4468-a48e-8540b10741df");
    }


    /// <summary>
    /// This test ensures that the agent singleton contains valid data
    /// </summary>
    [Test]
    public async Task verifyAgentSingletonContainsData()
    {
        AgentSingleton ags = AgentSingleton.GetInstance();

        var data = await ags.GetData();

        Assert.AreEqual(ags.ToString(), "PremiumTravelService.Api.Singleton.AgentSingleton");
        Assert.IsTrue(data.Count() > 0);
        Assert.AreEqual(data.ElementAt(0).LastName, "Crowley");
    }
}