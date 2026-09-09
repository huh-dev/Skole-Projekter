using Heltevagten.Enums;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;
using Heltevagten.Exceptions;

namespace Heltevagten.Strategies;

public class FirstAvailableStrategy : IDispatchStrategy
{

    public void SelectHero(Incident incident, List<Hero> availableHeroes)
    {
        var firstAvailableHero = availableHeroes.FirstOrDefault(h => h.State == HeroState.Available);
        if (firstAvailableHero != null)
        {
            firstAvailableHero.UpdateState(HeroState.Dispatched);
        }
        else
        {
            throw new NoSuitableHeroFoundException("No suitable hero found");
        }
    }
}