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



    private int ComparaNumeros(int[] jogoJogado, int[] numerosReferencia)
    {

        return jogoJogado.Intersect(numerosReferencia).Count();

    }

}
