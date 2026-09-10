using Heltevagten.Interfaces;
using Heltevagten.Incidents;

namespace Heltevagten.Heros;

public class HealingHero : Hero
{
    public HealingHero(string name, int energyLevel, int cost, Location currentLocation) : base(name, energyLevel, cost, currentLocation)
    {
    }

    public void HealCivilians()
    {
        Console.WriteLine($"{Name} heals the civilians!");
    }
}