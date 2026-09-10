using Heltevagten.Incidents;
using Heltevagten.Interfaces;

namespace Heltevagten.Heros;

public class StrongHero : Hero, ISuperStrong
{
    public StrongHero(string name, int energyLevel, int cost, Location currentLocation)
        : base(name, energyLevel, cost, currentLocation)
    {
    }

    public override string UseSignatureMove()
    {
        LiftHeavyObject();
        return $"{Name} uses super strength and clears the wreckage.";
    }

    public void LiftHeavyObject()
    {
    }
}
