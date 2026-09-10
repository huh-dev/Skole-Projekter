using Heltevagten.Enums;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;
using Heltevagten.Exceptions;

namespace Heltevagten.Strategies;

public class FirstAvailableStrategy : IDispatchStrategy
{
    public void SelectHero(Incident incident, List<Hero> availableHeroes)
    {
        try
        {
            Hero firstAvailableHero = SearchEngine.FindFirst(availableHeroes, hero => hero.State == HeroState.Available);
            firstAvailableHero.UpdateState(HeroState.Dispatched);
        }
        catch (InvalidOperationException)
        {
            throw new NoSuitableHeroFoundException("No suitable hero found");
        }
    }
}
