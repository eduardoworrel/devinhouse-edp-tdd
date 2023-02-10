using System;
namespace lib.lotofacil;
public class Sorteio
{
    private List<Jogo> jogos = new List<Jogo> { };
    private int _valorPremio;
    public List<Jogo> acertou6 = new List<Jogo> { };
    public List<Jogo> acertou5 = new List<Jogo> { };
    public List<Jogo> acertou4 = new List<Jogo> { };
    public Sorteio(int valorPremio)
    {
        _valorPremio = valorPremio;
    }

    public int GetValorPremio()
    {
        return this._valorPremio;
    }

    public void addJogo(Jogo jogo)
    {
        if (jogo.NumerosSelecionados.Count() != 6)
        {
            throw new Exception("Quantidade de numeros invalida");
        }

        if (jogo.NumerosSelecionados.Count() != jogo.NumerosSelecionados.Distinct().Count())
        {
            throw new Exception("Numeros não podem ser repetidos");
        }

        if (jogo.NumerosSelecionados.Any(n => n > 60 || n < 1))
        {
            throw new Exception("Número fora raio");
        }
        jogos.Add(jogo);
    }

    public int GetQuantidadeJogos()
    {
        return jogos.Count();
    }

    public int[] GeraNumerosAleatorios()
    {
        int[] numeros = new int[6];
        Random random = new Random();
        while (numeros.Distinct().Count() != 6)
        {
            numeros = new int[]
            {
                random.Next(1, 60),
                random.Next(1, 60),
                random.Next(1, 60),
                random.Next(1, 60),
                random.Next(1, 60),
                random.Next(1, 60),
            };

        }
        return numeros;

    }

    public void Sorteia(int[] numerosSorteados)
    {
        foreach (Jogo jogo in jogos)
        {
            var quantidadeAcertos = ComparaNumeros(jogo.NumerosSelecionados, numerosSorteados);
            switch (quantidadeAcertos)
            {
                case 6: acertou6.Add(jogo); break;
                case 5: acertou5.Add(jogo); break;
                case 4: acertou4.Add(jogo); break;
            }
        }


    }

    public double DividePorcentagemPremio(double valorPorcentagem, int qtVencedores, double premio)
    {
        if (qtVencedores <= 0)
            throw new Exception("Valores não podem ser 0");
        if (premio <= 0 || valorPorcentagem <= 0)
            return 0;
        return (premio * valorPorcentagem) / qtVencedores;
    }

    public void DistribuiPremiacao() 
    {
        double ganhou6 = 0;
        double ganhou5 = 0;
        double ganhou4 = 0;

        if (acertou6.Count > 0) 
        { 
            ganhou6 = DividePorcentagemPremio(0.8, acertou6.Count, GetValorPremio());
        }
        if (acertou5.Count > 0)
        {
            ganhou5 = DividePorcentagemPremio(0.15, acertou5.Count, GetValorPremio());
        }
        if (acertou4.Count > 0)
        {
            ganhou4 = DividePorcentagemPremio(0.05, acertou4.Count, GetValorPremio());
        }

        foreach (var item in acertou6)
        {
            item.Premiacao = ganhou6;
        }
        foreach (var item in acertou5)
        {
            item.Premiacao = ganhou5;
        }
        foreach (var item in acertou4)
        {
            item.Premiacao = ganhou4;
        }
    }

    private int ComparaNumeros(int[] jogoJogado, int[] numerosReferencia)
    {

        return jogoJogado.Intersect(numerosReferencia).Count();

    }



}
