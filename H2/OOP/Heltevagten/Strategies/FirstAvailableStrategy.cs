using Heltevagten.Enums;
using Heltevagten.Exceptions;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;

namespace Heltevagten.Strategies;

public class FirstAvailableStrategy : IDispatchStrategy
{
    public Hero SelectHero(Incident incident, List<Hero> availableHeroes)
    {
        try
        {
            return SearchEngine.FindFirst(availableHeroes, hero => hero.State == HeroState.Available);
        }
        catch (InvalidOperationException)
        {
            throw new NoSuitableHeroFoundException($"No suitable hero found for '{incident.Description}'.");
        }
    }
}
