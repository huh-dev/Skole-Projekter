using Heltevagten.Interfaces;
using Heltevagten.Incidents;

namespace Heltevagten.Heros;

public class HealingHero : Hero
{
    public HealingHero(string name, int energyLevel, Location currentLocation) : base(name, energyLevel, currentLocation)
    {
    }

    public string UseSignatureMove()
    {
        return $"{Name} uses their signature move!";
    }

    public void HealCivilians()
    {
        Console.WriteLine($"{Name} heals the civilians!");
    }
}