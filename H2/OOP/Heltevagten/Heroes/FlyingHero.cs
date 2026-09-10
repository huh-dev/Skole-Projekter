using Heltevagten.Incidents;
using Heltevagten.Interfaces;

namespace Heltevagten.Heros;

public class FlyingHero : Hero, IFlyable
{
    public FlyingHero(string name, int energyLevel, int cost, Location currentLocation)
        : base(name, energyLevel, cost, currentLocation)
    {
    }

    public override string UseSignatureMove()
    {
        return $"{Name} dives from the sky and secures the scene from above.";
    }

    public override void Dispatch(Incident incident)
    {
        FlyTo(incident.Location);
        base.Dispatch(incident);
    }

    public void FlyTo(Location location)
    {
        CurrentLocation = location;
    }
}
