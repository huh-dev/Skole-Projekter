using Heltevagten.Incidents;

namespace Heltevagten.Interfaces;

public interface IDispatchStrategy
{
    void SelectHero(Incident incident, List<Hero> availableHeroes);
}
