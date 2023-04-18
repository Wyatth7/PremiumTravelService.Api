using NUnit.Framework;

namespace PremiumTravelService.Api.NunitTests;

[TestFixture]
public class TestingNunit
{
    [Test]
    public void SampleTest()
    {
        var val1 = 5;
        var val2 = 5;
        
        Assert.AreEqual(val1, val2);
    }
}