using Heltevagten.Exceptions;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;

namespace Heltevagten.Heros;

public class FlyingHero : Hero, IFlyable
{
    public FlyingHero(string name, int energyLevel, int cost, Location currentLocation) : base(name, energyLevel, cost, currentLocation)
    {
    }

    public string UseSignatureMove()
    {
        return $"{Name} uses their signature move!";
    }

    public void FlyTo(Location location)
    {
        CurrentLocation = location;

        // Calculate the distance between the current location and the new location
        double distance = CurrentLocation.CalculateDistance(location);

        // Calculate the energy required to fly the distance
        int energyRequired = (int)distance;

        // Check if the hero has enough energy
        if (EnergyLevel < energyRequired)
        {
            throw new HeroUnavailableException($"{Name} does not have enough energy to fly to {location}");
        }
    }



}