using NUnit.Framework;

namespace PremiumTravelService.Api.NunitTests;

[TestFixture]
public class TestingNunit
{
    [Test]
    public void SampleTest()
    {
        var val1 = 6;
        var val2 = 6;
        
        Assert.AreEqual(val1, val2);
    }

    [Test]
    public void SampleTest2()
    {
        var val1 = 1;
        var val2 = 0;

        Assert.AreNotEqual(val1, val2);
    }
}