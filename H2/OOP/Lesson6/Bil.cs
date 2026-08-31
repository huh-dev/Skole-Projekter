namespace Lesson06;

public class Bil
{
    public string Registreringsnummer { get; set; }
    public string Model { get; set; }
    public string Mærke { get; set; }
    public int Kilometerstand { get; set; }
    public double Dagspris { get; set; }
    public bool ErUdlejet { get; set; }

    public bool ErLedig()
    {
        return !ErUdlejet;
    }

    public void OpdaterKilometerstand(int kilometer)
    {
        Kilometerstand += kilometer;
    }

    public void SætUdlejet(bool erUdlejet)
    {
        ErUdlejet = erUdlejet;
    }
}