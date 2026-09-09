using Heltevagten.Enums;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;
using Heltevagten.Exceptions;

namespace Heltevagten.Strategies;

public class ClosestHeroStrategy : IDispatchStrategy
{
    public void SelectHero(Incident incident, List<Hero> availableHeroes)
    {
        var closestHero = availableHeroes.OrderBy(h => h.CurrentLocation.CalculateDistance(incident.Location)).FirstOrDefault();
        if (closestHero != null)
        {
            closestHero.UpdateState(HeroState.Dispatched);
        }
        else
        {
            throw new NoSuitableHeroFoundException("No suitable hero found");
        }
    }
}