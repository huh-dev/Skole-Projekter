using Heltevagten.Incidents;
using Heltevagten.Interfaces;
using Heltevagten.Enums;

namespace Heltevagten;

public class DispatchCenter
{
    public List<Hero> Heroes { get; private set; }
    public List<Incident> Incidents { get; private set; }
    public IDispatchStrategy DispatchStrategy { get; private set; }

    
    /*
     * MARK: CONSTRUCTOR
     * This will initialize the dispatch center.
     * @param dispatchStrategy - The dispatch strategy.
     */
    public DispatchCenter(IDispatchStrategy dispatchStrategy)
    {
        Heroes = new List<Hero>();
        Incidents = new List<Incident>();
        DispatchStrategy = dispatchStrategy;
    }

    /*
     * MARK: REGISTER HERO
     * This will register a hero to the dispatch center.
     * @param hero - The hero.
     */
    public void RegisterHero(Hero hero)
    {
        Heroes.Add(hero);
    }

    /*
     * MARK: REPORT INCIDENT
     * This will report an incident to the dispatch center.
     * @param incident - The incident.
     */
    public void ReportIncident(Incident incident)
    {
        Incidents.Add(incident);
    }

    /*
     * MARK: DISPATCH HERO TO INCIDENT
     * This will dispatch a hero to an incident.
     * @param incident - The incident.
     * @param hero - The hero.
     */
    public void DispatchHeroToIncident(Incident incident, Hero? hero)
    {
        if (hero is null)
        {
            DispatchStrategy.SelectHero(
                incident,
                SearchEngine.FindAll(Heroes, h => h.State == HeroState.Available).ToList());
            return;
        }

        hero.Dispatch(incident);
    }

}