using System;
namespace lib.lotofacil;
public class Sorteio
{
    private List<Jogo> jogos = new List<Jogo>{};
    private int _valorPremio;
    
    public Sorteio(int valorPremio){
       _valorPremio = valorPremio;
    }

    public int GetValorPremio(){
        return this._valorPremio;
    }

    public void addJogo(Jogo jogo){
        jogos.Add(jogo);
    }
    public int GetQuantidadeJogos(){
        return jogos.Count();
    }

}
