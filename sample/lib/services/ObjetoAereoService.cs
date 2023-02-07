namespace lib;
public class ObjetoAereoService{
    // dado um id, retorna o quanto de risco este objeto corre
    //onde 
    //< 300m = 100 (max)
    // 301m>600m = 70
    // 601m>1600m = 20
    // > 1601m = 0 

    public static double GetRiscoByObjetoAereo(int id){
        var objetoCorrespondente  = ObjetoAereoRepository.Get(id);
        var todosOsOutros = 
        ObjetoAereoRepository.GetAll()
        .Where(e=>e.Id != id);
        double maxRisc = 0;
        foreach(var item in todosOsOutros){
            var distancia = distanciaEmMetros(objetoCorrespondente,item);
            var risc = GetRiscoByDistancia(distancia);
            if(risc > maxRisc){
                maxRisc = risc;
            }
        }
        return maxRisc;
    }

    public static double distanciaEmMetros(ObjetoAereo referencia, ObjetoAereo outro){
        double R = 6371e3; // radius of the earth in meters
        double latitude1 = referencia.Latitude * Math.PI / 180;
        double latitude2 = outro.Latitude * Math.PI / 180;
        double deltaLatitude = (outro.Latitude - referencia.Latitude) * Math.PI / 180;
        double deltaLongitude = (outro.Longitude - referencia.Longitude) * Math.PI / 180;
        double a = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
                    Math.Cos(latitude1) * Math.Cos(latitude2) *
                    Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distance = R * c;

        return distance;
    }
    private static double GetRiscoByDistancia(double distancia){
        if(distancia < 300){
            return 100;
        }
        if(distancia > 300 && distancia < 600){
            return 70;
        }
        if(distancia > 600 && distancia < 1600){
            return 20;
        }
        if(distancia > 1600){
            return 0;
        }
        throw new Exception("Distancia não mapeada");
    }
}