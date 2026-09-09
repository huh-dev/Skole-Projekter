using Heltevagten.Incidents;
using Heltevagten.Interfaces;

namespace Heltevagten;

public class DispatchCenter
{
    public List<Hero> Heroes { get; private set; }
    public List<Incident> Incidents { get; private set; }
    public IDispatchStrategy DispatchStrategy { get; private set; }

    public DispatchCenter(IDispatchStrategy dispatchStrategy)
    {
        Heroes = new List<Hero>();
        Incidents = new List<Incident>();
        DispatchStrategy = dispatchStrategy;
    }

    public void RegisterHero(Hero hero)
    {
        Heroes.Add(hero);
    }

    public void ReportIncident(Incident incident)
    {
        Incidents.Add(incident);
    }

    public void DispatchHeroToIncident(Incident incident)
    {
        // var hero = DispatchStrategy.SelectHero(Heroes, incident);
        // hero.DispatchToIncident(incident);
    }

}