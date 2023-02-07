using lib;

namespace test.lib;

public class TestCalculadora
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void TestObjetosGrudados()
    {
        var risco = ObjetoAereoService.GetRiscoByObjetoAereo(1);
        Assert.GreaterOrEqual(risco,0);
        Assert.AreEqual(risco,100);

    }
}