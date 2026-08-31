namespace Lesson06;

public class Udlejning
{
    public DateTime Startdato { get; set; }
    public DateTime Slutdato { get; set; }
    public Bil Bil { get; set; }

    public double BeregnPris()
    {
        return (Slutdato - Startdato).TotalDays * Bil.Dagspris;
    }

    public void Afslut(DateTime slutdato, int kilometerstand)
    {
        Slutdato = slutdato;
        Bil.OpdaterKilometerstand(kilometerstand);
        Bil.SætUdlejet(false);
    }
    
    
    
}