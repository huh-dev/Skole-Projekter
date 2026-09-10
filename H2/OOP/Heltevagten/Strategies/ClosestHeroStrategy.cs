using Heltevagten.Exceptions;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;

namespace Heltevagten.Strategies;

public class ClosestHeroStrategy : IDispatchStrategy
{
    public Hero SelectHero(Incident incident, List<Hero> availableHeroes)
    {
        Hero? closestHero = availableHeroes
            .OrderBy(hero => hero.CurrentLocation.CalculateDistance(incident.Location))
            .FirstOrDefault();

        if (closestHero is null)
        {
            throw new NoSuitableHeroFoundException($"No suitable hero found for '{incident.Description}'.");
        }

        return closestHero;
    }
}
