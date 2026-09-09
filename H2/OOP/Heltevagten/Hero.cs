using Heltevagten.Enums;
using Heltevagten.Incidents;

namespace Heltevagten;

public abstract class Hero
{
    public string Name { get; private set; }
    public int EnergyLevel { get; private set; }
    public int Cost { get; private set; }
    public HeroState State { get; private set; }
    public Location CurrentLocation { get; protected set; }

    public Hero(string name, int energyLevel, int cost, Location currentLocation)
    {
        Name = name;
        EnergyLevel = energyLevel;
        Cost = cost;
        CurrentLocation = currentLocation;
        State = HeroState.Available;
    }

    public string UseSignatureMove()
    {
        return $"{Name} uses their signature move!";
    }

    public void RechargeEnergyLevel(int amount)
    {
        EnergyLevel += amount;
    }

    public void UpdateState(HeroState newState)
    {
        State = newState;
    }
} 