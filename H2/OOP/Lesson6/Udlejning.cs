namespace Lesson06;

public class Udlejning
{
    private readonly IKvitteringsAfsender _kvitteringsAfsender;

    public Udlejning(IKvitteringsAfsender kvitteringsAfsender)
    {
        _kvitteringsAfsender = kvitteringsAfsender;
    }

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



    public string GenerérKvittering(string kundeNavn)
    {
        string tekst = $"Kvittering sendt for udlejning {Startdato} - {Slutdato} til {kundeNavn}. Pris: {BeregnPris()}";
        _kvitteringsAfsender.SendKvittering(tekst);
        return tekst;
    }
}
