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
                new Jogo
                {
                    Nome = "Renan",
                    CPF = "000000000",
                    NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 6 }
                }
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
                new Jogo
                {
                    Nome = "Andre",
                    CPF = "000000000",
                    NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 6 }
                }
            );
        });

    }


    [Test]

    public void TestaNumerosRepetidos()
    {
        var sorteio = new Sorteio(5);
        Assert.Catch(() =>
        {
            sorteio.addJogo(
                new Jogo
                {
                    Nome = "Andre",
                    CPF = "000000000",
                    NumerosSelecionados = new int[] { 1, 2, 3, 4, 4, 5 }
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
                new Jogo
                {
                    Nome = "Andre",
                    CPF = "000000000",
                    NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 6 }
                }
            );
        });
    }

    [Test]
    public void TestaHouveGanhadores()
    {
        Jogo jogo01 = new Jogo
        {
            Nome = "Eduardo",
            CPF = "111111111",
            NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 6 }
        };
        Jogo jogo02 = new Jogo
        {
            Nome = "Roni",
            CPF = "2222222",
            NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 7 }
        };
        Jogo jogo03 = new Jogo
        {
            Nome = "Nicolas",
            CPF = "0282828282",
            NumerosSelecionados = new int[] { 1, 2, 3, 4, 8, 9 }
        };
        Jogo jogo04 = new Jogo
        {
            Nome = "Renan",
            CPF = "0282828282",
            NumerosSelecionados = new int[] { 1, 2, 3, 4, 5, 9 }
        };
        Jogo jogo05 = new Jogo
        {
            Nome = "Nicolas",
            CPF = "0282828282",
            NumerosSelecionados = new int[] { 1, 2, 3, 4, 8, 9 }
        };
        Jogo jogo06 = new Jogo
        {
            Nome = "Nicolas",
            CPF = "0282828282",
            NumerosSelecionados = new int[] { 1, 2, 3, 4, 8, 9 }
        };

        var sorteio = new Sorteio(132);
        sorteio.addJogo(jogo01);
        sorteio.addJogo(jogo02);
        sorteio.addJogo(jogo03);
        sorteio.addJogo(jogo04);
        sorteio.addJogo(jogo05);
        sorteio.addJogo(jogo06);

        //var numerosSorteados = sorteio.GeraNumerosAleatorios();
        sorteio.Sorteia(new int[] { 1, 2, 3, 4, 5, 6 });


        Assert.AreEqual(sorteio.acertou6.Count(), 1);
        Assert.AreEqual(sorteio.acertou5.Count(), 2);
        Assert.AreEqual(sorteio.acertou4.Count(), 3);
    }



}