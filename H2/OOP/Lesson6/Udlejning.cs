namespace Lesson06;

public class Udlejning
{

    public DateTime Startdato { get; set; }
    public DateTime Slutdato { get; set; }
    public Bil Bil { get; set; }
    public Kunde Kunde { get; set; }
    private readonly IKvitteringsAfsender _kvitteringsAfsender;

    public Udlejning(Kunde kunde, Bil bil, DateTime startdato, DateTime slutdato, IKvitteringsAfsender KvitteringsAfsender)
    {
        Kunde = kunde;
        Bil = bil;
        Startdato = startdato;
        Slutdato = slutdato;
        Bil.SætUdlejet(true);
        _kvitteringsAfsender = KvitteringsAfsender;
    }



    public double BeregnPris()
    {
        return Math.Round((Slutdato - Startdato).TotalDays * Bil.Dagspris, 2);
    }

    public void AfslutUdlejning()
    {
        Bil.SætUdlejet(false);
        Bil.OpdaterKilometerstand(Bil.Kilometerstand);
        GenerérKvittering();
    }


    public void GenerérKvittering()
    {
        string tekst = $"Kvittering sendt for udlejning {Startdato} - {Slutdato} til {Kunde.Navn}. Pris: {BeregnPris()}";
        _kvitteringsAfsender.SendKvittering(tekst);
    }
}
