using NUnit.Framework;

namespace PremiumTravelService.Api.NunitTests;

[TestFixture]
public class StateTests
{
    [Test]
    public void SampleTest()
    {
        var val1 = 6;
        var val2 = 6;

        Assert.AreEqual(val1, val2);
    }
}