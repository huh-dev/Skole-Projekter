namespace Lesson06;

public abstract class Bil
{
    public string Registreringsnummer { get; set; }
    public string Model { get; set; }
    public string Mærke { get; set; }
    public int Kilometerstand { get; private set; }
    public double Dagspris { get; set; }
    public bool ErUdlejet { get; private set; }

    public Bil(string registreringsnummer, string mærke, string model, int kilometerstand, double dagspris)
    {
        Registreringsnummer = registreringsnummer;
        Mærke = mærke;
        Model = model;
        Kilometerstand = kilometerstand;
        Dagspris = dagspris;
    }

    public bool ErLedig()
    {
        return !ErUdlejet;
    }

    public void OpdaterKilometerstand(int kilometer)
    {
        Kilometerstand = kilometer;
    }

    public void SætUdlejet(bool erUdlejet)
    {
        ErUdlejet = erUdlejet;
    }
}