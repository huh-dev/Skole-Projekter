namespace Lesson06;

public class Personbil : Bil
{
    public int AntalSæder { get; set; }
    
    public Personbil(string registreringsnummer, string mærke, string model, int kilometerstand, double dagspris, int antalSæder) : base(registreringsnummer, mærke, model, kilometerstand, dagspris)
    {
        AntalSæder = antalSæder;
    }
}