using Heltevagten.Interfaces;
using Heltevagten.Incidents;

namespace Heltevagten.Heros;

public class StrongHero : Hero, ISuperStrong
{
    public StrongHero(string name, int energyLevel, int cost, Location currentLocation) : base(name, energyLevel, cost, currentLocation)
    {
    }

    public void LiftHeavyObject()
    {
        Console.WriteLine($"{Name} lifts a heavy object!");
    }


}