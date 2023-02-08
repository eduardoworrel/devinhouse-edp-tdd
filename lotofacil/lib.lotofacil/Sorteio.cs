using System;
namespace lib.lotofacil;
public class Sorteio
{
    private List<Jogo> jogos = new List<Jogo> { };
    private int _valorPremio;

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

}
