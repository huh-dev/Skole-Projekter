using Heltevagten.Enums;
using Heltevagten.Exceptions;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;

namespace Heltevagten;

/// <summary>
/// Holds registered heroes and reported incidents. Hero selection is injected
/// through <see cref="IDispatchStrategy"/> so the center does not own that logic.
/// </summary>
public class DispatchCenter
{
    private readonly List<Hero> _heroes = new List<Hero>();
    private readonly List<Incident> _incidents = new List<Incident>();

    public IReadOnlyList<Hero> Heroes => _heroes;
    public IReadOnlyList<Incident> Incidents => _incidents;
    public IDispatchStrategy DispatchStrategy { get; private set; }

    public DispatchCenter(IDispatchStrategy dispatchStrategy)
    {
        DispatchStrategy = dispatchStrategy;
    }

    public void RegisterHero(Hero hero)
    {
        _heroes.Add(hero);
    }

    public void ReportIncident(Incident incident)
    {
        _incidents.Add(incident);
    }


    public void DispatchHeroToIncident(Incident incident, Hero? hero)
    {
        Hero selectedHero = hero ?? DispatchStrategy.SelectHero(
            incident,
            SearchEngine.FindAll(_heroes, h => h.State == HeroState.Available).ToList());

        if (selectedHero.State != HeroState.Available)
        {
            throw new HeroUnavailableException($"{selectedHero.Name} is {selectedHero.State.ToString().ToLower()} and cannot be dispatched.");
        }

        selectedHero.Dispatch(incident);
    }

    public void ReleaseHero(Hero hero)
    {
        hero.Release();
    }

    public void RechargeHero(Hero hero)
    {
        hero.Recharge();
    }

    public void ResolveIncident(Incident incident, Action<Incident> onResolved)
    {
        incident.Resolve();
        onResolved(incident);
    }
}
