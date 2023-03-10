using lib.lotofacil;
using System.Runtime.Intrinsics.X86;

namespace test.lib.lotofacil;

public class Tests
{
    private int[] numerosSorteado = new int[] { 1, 2, 3, 4, 5, 6 };
    private Dictionary<int, Jogo> jogosGanhados = new();
    public Tests()
    {
        var jogo6 = new Jogo
        {
            Nome = "Renan",
            CPF = "000000000",
            NumerosSelecionados = numerosSorteado
        };
        var jogo5 = new Jogo
        {
            Nome = "Renan 8",
            CPF = "000000050",
            NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 8 }
        };
        var jogo4 = new Jogo
        {
            Nome = "Renan 2",
            CPF = "000000051",
            NumerosSelecionados = new int[] { 1, 2, 3, 4, 7, 8 }
        };

        jogosGanhados.Add(6, jogo6);
        jogosGanhados.Add(5, jogo5);
        jogosGanhados.Add(4, jogo4);
    }

    [Test]
    public void TestaSeSorteioIniciaComValor()
    {
        var sorteio = new Sorteio(1000);
        var valor = sorteio.GetValorPremio();
        Assert.AreEqual(valor, 1000);
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

        for (var i = 0; i < qtJogos; i++)
        {
            sorteio.addJogo(
                jogosGanhados[6]
            );
        }

        Assert.AreEqual(sorteio.GetQuantidadeJogos(), qtJogos);
    }

    [Test]

    public void TestaQuantidadeNumerosValida()
    {
        var sorteio = new Sorteio(5);
        Assert.Catch(() =>
        {
            sorteio.addJogo(
                new Jogo
                {
                    Nome = "Andre",
                    CPF = "000000000",
                    NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 6, 8 }
                }
            );
        });

        Assert.Catch(() =>
        {
            sorteio.addJogo(
                new Jogo
                {
                    Nome = "Andre",
                    CPF = "000000000",
                    NumerosSelecionados = new int[] { 1, 2, 3, 4 }
                }
            );
        });

        Assert.DoesNotThrow(() =>
        {
            sorteio.addJogo(
                jogosGanhados[6]
            );
        });

    }


    [Test]

    public void TestaNumerosRepetidos()
    {
        var sorteio = new Sorteio(5);
        Assert.Catch(() =>
        {
            sorteio.addJogo(new Jogo
            {
                Nome = "Andre",
                CPF = "000000000",
                NumerosSelecionados = new int[] { 1, 2, 3, 4, 4, 6 }
            }
            );
        });

    }

    //Outra funcionalidade será a capacidade de gerar 6 números aleatoriamente;

    [Test]
    public void TestaNumerosAleatorios()
    {
        var sorteio = new Sorteio(5);
        var numeros = sorteio.GeraNumerosAleatorios();

        Assert.AreEqual(numeros.Count(), 6);
        Assert.AreEqual(numeros.Distinct().Count(), 6);

        for (int i = 0; i < numeros.Count(); i++)
        {
            Assert.LessOrEqual(numeros[i], 60);
            Assert.GreaterOrEqual(numeros[i], 1);
        }
    }

    [Test]
    public void TestaBilheteNaoContemNumeroMaiorQue60eMenorQue1()
    {
        var sorteio = new Sorteio(5);
        Assert.Catch(() =>
        {
            sorteio.addJogo(
                new Jogo
                {
                    Nome = "Andre",
                    CPF = "000000000",
                    NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 61 }
                }
            );
        });

        Assert.Catch(() =>
        {
            sorteio.addJogo(
                new Jogo
                {
                    Nome = "Andre",
                    CPF = "000000000",
                    NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 0 }
                }
            );
        });
        Assert.DoesNotThrow(() =>
        {
            sorteio.addJogo(
                jogosGanhados[6]
            );
        });
    }

    [TestCase(2, 0, 1)]
    [TestCase(75, 31, 95421)]
    [TestCase(0, 0, 0)]
    public void TestaHouveGanhadores(int qt4, int qt5, int qt6)
    {
        var sorteio = new Sorteio(132);
        for (int i = 0; i < qt4; i++)
        {
            sorteio.addJogo(jogosGanhados[4]);
        }
        for (int i = 0; i < qt5; i++)
        {
            sorteio.addJogo(jogosGanhados[5]);
        }
        for (int i = 0; i < qt6; i++)
        {
            sorteio.addJogo(jogosGanhados[6]);
        }

        //var numerosSorteados = sorteio.GeraNumerosAleatorios();
        sorteio.Sorteia(numerosSorteado);

        Assert.AreEqual(sorteio.acertou6.Count(), qt6);
        Assert.AreEqual(sorteio.acertou5.Count(), qt5);
        Assert.AreEqual(sorteio.acertou4.Count(), qt4);
    }

    /*distribuir de forma equivalente, 
    80% para os que acertaram 6 números, 
    15% para os que acertaram 5 números 
    e 5% para os que acertaram 4 números;*/
    // [TestCase(1, 80, 2, 1000000)]
    // [TestCase(1, 1, 1, 1000001)]
    // [TestCase(3, 3, 3, 20)]
    // [TestCase(2, 2, 1, 100)]
    // [TestCase(3, 1, 3, 190)]
    // [TestCase(1, 4, 4, 0)]
    // public void ChecaCalculoPremiacao(int qt4, int qt5, int qt6, int premio)
    // {
    //     var sorteio = new Sorteio(premio);

    //     Assert.AreEqual(sorteio.DividePorcentagemPremio(0.8m, qt6, sorteio.GetValorPremio()), (sorteio.GetValorPremio() * 0.8m) / qt6);
    //     Assert.AreEqual(sorteio.DividePorcentagemPremio(0.15m, qt5, sorteio.GetValorPremio()), (sorteio.GetValorPremio() * 0.15m) / qt5);
    //     Assert.AreEqual(sorteio.DividePorcentagemPremio(0.05m, qt4, sorteio.GetValorPremio()), (sorteio.GetValorPremio() * 0.05m) / qt4);
    // }

    [TestCase(0, 80, 2, 1000000)]
    [TestCase(1, 0, 0, 1000001)]
    [TestCase(3, 3, 3, 20)]
    [TestCase(2, 2, 1, 100)]
    [TestCase(3, 1, 3, 190)]
    [TestCase(1, 4, 4, 0)]
    public void ChecaDistribuicaoPremios(int qt4, int qt5, int qt6, int premio)
    {
        var sorteio = new Sorteio(premio);

        for (int i = 0; i < qt4; i++)
        {
            sorteio.addJogo(jogosGanhados[4]);
        }
        for (int i = 0; i < qt5; i++)
        {
            sorteio.addJogo(jogosGanhados[5]);
        }
        for (int i = 0; i < qt6; i++)
        {
            sorteio.addJogo(jogosGanhados[6]);
        }

        sorteio.Sorteia(numerosSorteado);

        sorteio.DistribuiPremiacao();

        decimal percentualAcertou6;


        foreach (var i in sorteio.acertou6)
        {
            var valorCheck = sorteio.DividePorcentagemPremio(6);
            Assert.AreEqual(i.Premiacao, valorCheck);
        }
        foreach (var i in sorteio.acertou5)
        {
            var valorCheck = sorteio.DividePorcentagemPremio(5);
            Assert.AreEqual(i.Premiacao, valorCheck);
        }
        foreach (var i in sorteio.acertou4)
        {
            var valorCheck = sorteio.DividePorcentagemPremio(4);
            Assert.AreEqual(i.Premiacao, valorCheck);
        }
    }



}