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

    public decimal DividePorcentagemPremio(int quantidadeAcertos)
    {


        if (quantidadeAcertos == 6)
        {
            if (acertou5.Count == 0 && acertou4.Count == 0)
            {

                return _valorPremio / acertou6.Count;
            }

            if (acertou5.Count != 0 && acertou4.Count == 0)
            {
                var novaPorcentagem = (100 * 80) / 95;

                return _valorPremio * novaPorcentagem / acertou6.Count;
            }

            if (acertou5.Count == 0 && acertou4.Count != 0)
            {
                var novaPorcentagem = (100 * 80) / 85;

                return _valorPremio * novaPorcentagem / acertou6.Count;
            }

            if (acertou5.Count != 0 && acertou4.Count != 0)
            {


                return _valorPremio * 0.8m / acertou6.Count;
            }


        }

        if (quantidadeAcertos == 5)
        {
            if (acertou6.Count == 0 && acertou4.Count == 0)
            {

                return _valorPremio / acertou5.Count;
            }

            if (acertou6.Count != 0 && acertou4.Count == 0)
            {
                var novaPorcentagem = (100 * 15) / 20;

                return _valorPremio * novaPorcentagem / acertou5.Count;
            }

            if (acertou6.Count == 0 && acertou4.Count != 0)
            {
                var novaPorcentagem = (100 * 15) / 95;

                return _valorPremio * novaPorcentagem / acertou5.Count;
            }

            if (acertou6.Count != 0 && acertou4.Count != 0)
            {


                return _valorPremio * 0.15m / acertou5.Count;
            }
        }

        if (quantidadeAcertos == 4)
        {

            if (acertou6.Count == 0 && acertou5.Count == 0)
            {

                return _valorPremio / acertou4.Count;
            }

            if (acertou6.Count != 0 && acertou5.Count == 0)
            {
                var novaPorcentagem = (100 * 5) / 20;

                return _valorPremio * novaPorcentagem / acertou4.Count;
            }

            if (acertou6.Count == 0 && acertou5.Count != 0)
            {
                var novaPorcentagem = (100 * 5) / 85;

                return _valorPremio * novaPorcentagem / acertou4.Count;
            }

            if (acertou6.Count != 0 && acertou5.Count != 0)
            {


                return _valorPremio * 0.05m / acertou4.Count;
            }



        }

        return 0;
    }

    public void DistribuiPremiacao()
    {
        decimal ganhou6 = 0;
        decimal ganhou5 = 0;
        decimal ganhou4 = 0;

        if (acertou6.Count > 0)
        {
            ganhou6 = DividePorcentagemPremio(6);
        }
        if (acertou5.Count > 0)
        {
            ganhou5 = DividePorcentagemPremio(5);
        }
        if (acertou4.Count > 0)
        {
            ganhou4 = DividePorcentagemPremio(4);
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
