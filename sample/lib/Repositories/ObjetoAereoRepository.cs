namespace lib;
public class ObjetoAereoRepository{
    private static readonly List<ObjetoAereo> banco = new(){
        new ObjetoAereo{
            Id=1,
            Nome="Avião",
            Latitude=48.862140,
            Longitude=2.289971,
            Altitude=300
        },  
        new ObjetoAereo{
            Id=2,
            Nome="Ovini",
            Latitude=48.862140,
            Longitude=2.289971,
            Altitude=2300
        },

    };
    public static ObjetoAereo Get(int id){
        return banco.Where(e=>e.Id == id).First();
    }
    public static List<ObjetoAereo> GetAll(){
        return banco.ToList();
    }
}