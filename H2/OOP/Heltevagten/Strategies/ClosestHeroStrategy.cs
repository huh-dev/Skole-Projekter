using Heltevagten.Enums;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;
using Heltevagten.Exceptions;

namespace Heltevagten.Strategies;

public class ClosestHeroStrategy : IDispatchStrategy
{
    public void SelectHero(Incident incident, List<Hero> availableHeroes)
    {
        Hero? closestHero = availableHeroes
            .OrderBy(hero => hero.CurrentLocation.CalculateDistance(incident.Location))
            .FirstOrDefault();

        if (closestHero is null)
        {
            throw new NoSuitableHeroFoundException("No suitable hero found");
        }

        closestHero.UpdateState(HeroState.Dispatched);
    }
}
