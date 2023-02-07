using lib.lotofacil;

namespace test.lib.lotofacil;

public class Tests
{
    // [SetUp]
    // public void Setup()
    // {
    // }

    [Test]
    public void TestaSeSorteioIniciaComValor()
    {
        var sorteio = new Sorteio(1000);
        var valor = sorteio.GetValorPremio();
        Assert.AreEqual(valor,1000);
    }


    // Uma funcionalidade que seja 
    // capaz de receber os [números marcados pelo usuário] = jogo,
    //  validá-los e armazená-los;
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(1000)]
    [TestCase(10000)]
    public void TestaSeSorteioGuardaJogos(int qtJogos)
    {
        var sorteio = new Sorteio(1000);

        for(var i = 0; i < qtJogos;i++){
            sorteio.addJogo(
                new Jogo{
                    Nome="Renan",
                    CPF="000000000",
                    NumerosSelecionados=new int[]{1,2,3,4,5,6}
                }
            );
        }

        Assert.AreEqual(sorteio.GetQuantidadeJogos(),qtJogos);
    }
}