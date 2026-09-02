namespace Lesson06;

public class Varevogn : Bil
{
    public int LastevneKg { get; set; }
    
    public Varevogn(string registreringsnummer, string mærke, string model, int kilometerstand, double dagspris, int lastevneKg) : base(registreringsnummer, mærke, model, kilometerstand, dagspris)
    {
        LastevneKg = lastevneKg;
    }
}