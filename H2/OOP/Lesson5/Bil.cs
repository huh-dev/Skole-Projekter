using Lesson05.Interfaces;
namespace Lesson05;

public class Bil : Vehicle, IUdlejelig, IForsikringspligtig
{
    public Bil(string brand, string model, string topSpeedKmh) : base(brand, model, topSpeedKmh)
    {
    }

    public override decimal BeregnAfgift()
    {
        return 1000 * 1.5m;
    }

    public override void VisGrundInfo()
    {
        base.VisGrundInfo();
        Console.WriteLine($"Top Speed: {TopSpeedKmh} km/h");
        Console.WriteLine($"Afgift: {BeregnAfgift()} kr.");
    }

    public decimal BeregnLejepris(int antalDage)
    {
        return 1000 * antalDage;
    }

    public decimal BeregnForsikringspraemie(decimal pris)
    {
        return pris * 0.1m;
    }
}