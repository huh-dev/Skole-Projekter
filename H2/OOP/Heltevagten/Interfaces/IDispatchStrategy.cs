using Heltevagten.Incidents;

namespace Heltevagten.Interfaces;

public interface IDispatchStrategy
{
    Hero SelectHero(Incident incident, List<Hero> availableHeroes);
}
