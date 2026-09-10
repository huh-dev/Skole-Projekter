using Heltevagten.Incidents;

namespace Heltevagten.Heros;

public class HealingHero : Hero
{
    public HealingHero(string name, int energyLevel, int cost, Location currentLocation)
        : base(name, energyLevel, cost, currentLocation)
    {
    }

    public override string UseSignatureMove()
    {
        HealCivilians();
        return $"{Name} channels healing energy and treats the injured.";
    }

    public void HealCivilians()
    {
    }
}
