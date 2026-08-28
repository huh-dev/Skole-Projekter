using Lesson05.Interfaces;

namespace Lesson05;

public class Motorcykel : Vehicle, IUdlejelig
{
    public Motorcykel(string brand, string model, string topSpeedKmh) : base(brand, model, topSpeedKmh)
    {
    }

    public override decimal BeregnAfgift()
    {
        return 1000 * 0.5m;
    }

    public decimal BeregnLejepris(int antalDage)
    {
        return 1000 * antalDage;
    }
}