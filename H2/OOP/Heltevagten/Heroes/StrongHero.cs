using Heltevagten.Interfaces;
using Heltevagten.Incidents;

namespace Heltevagten.Heros;

public class StrongHero : Hero, ISuperStrong
{
    public StrongHero(string name, int energyLevel, Location currentLocation) : base(name, energyLevel, currentLocation)
    {
    }


    public string UseSignatureMove()
    {
        return $"{Name} uses their signature move!";
    }

    public void LiftHeavyObject()
    {
        Console.WriteLine($"{Name} lifts a heavy object!");
    }


}