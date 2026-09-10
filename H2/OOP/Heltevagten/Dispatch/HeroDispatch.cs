using Heltevagten;
using Heltevagten.Enums;
using Heltevagten.Exceptions;
using Heltevagten.Incidents;
using Heltevagten.Interfaces;
using Heltevagten.Strategies;

namespace Heltevagten.Dispatch;

public class HeroDispatch
{
    private const int LowIncidentEnergyCost = 5;
    private const int MediumIncidentEnergyCost = 10;
    private const int HighIncidentEnergyCost = 15;

    private readonly DispatchCenter _dispatchCenter;
    private readonly Dictionary<Incident, Hero> _assignments;

    
    /*
     * MARK: CONSTRUCTOR
     * This will initialize the hero dispatch.
     * @param dispatchCenter - The dispatch center.
     * @param assignments - The assignments.
     */
    public HeroDispatch(DispatchCenter dispatchCenter, Dictionary<Incident, Hero> assignments)
    {
        _dispatchCenter = dispatchCenter;
        _assignments = assignments;
    }

    /*
     * MARK: OPEN
     * This will open the hero dispatch.
     */
    public void Open()
    {
        List<Incident> openIncidents = GetWaitingIncidents();

        if (openIncidents.Count == 0)
        {
            Console.WriteLine("No incidents waiting for a hero.");
            Pause();
            return;
        }

        List<Hero> availableHeroes = SearchEngine.FindAll(_dispatchCenter.Heroes, hero => hero.State == HeroState.Available).ToList();
        if (availableHeroes.Count == 0)
        {
            Console.WriteLine("No available heroes.");
            Pause();
            return;
        }

        for (int i = 0; i < openIncidents.Count; i++)
        {
            Incident incident = openIncidents[i];
            Console.WriteLine($"  {i + 1}) {incident.Description} (Lvl {incident.Level})");
        }

        Console.Write("Select incident (0 for all): ");
        if (!int.TryParse(Console.ReadLine(), out int selectedIncident) || selectedIncident < 0 || selectedIncident > openIncidents.Count)
        {
            Console.WriteLine($"Invalid input. Please enter a number between 0 and {openIncidents.Count}.");
            Pause();
            return;
        }

        List<Incident> selectedIncidents;
        if (selectedIncident == 0)
        {
            selectedIncidents = openIncidents;
        }
        else
        {
            selectedIncidents = new List<Incident> { openIncidents[selectedIncident - 1] };
        }

        Console.WriteLine();
        Console.WriteLine($"Selected {selectedIncidents.Count} incident(s).");
        Console.WriteLine("1) Closest hero");
        Console.WriteLine("2) First available hero");
        Console.WriteLine("3) Choose heroes manually");
        Console.WriteLine("0) Cancel");
        Console.Write("Strategy: ");

        if (!int.TryParse(Console.ReadLine(), out int strategyChoice) || strategyChoice < 0 || strategyChoice > 3)
        {
            Console.WriteLine("Invalid input. Please enter a number between 0 and 3.");
            Pause();
            return;
        }

        if (strategyChoice == 0)
        {
            return;
        }

        if (strategyChoice == 3)
        {
            DispatchManually(selectedIncidents);
            Pause();
            return;
        }

        IDispatchStrategy strategy = strategyChoice == 1
            ? new ClosestHeroStrategy()
            : new FirstAvailableStrategy();

        DispatchWithStrategy(selectedIncidents, strategy);
        Pause();
    }

    /*
     * MARK: GET INCIDENT ENERGY COST
     * This will get the energy cost of an incident.
     * @param level - The level of the incident.
     * @return The energy cost of the incident.
     */
    public static int GetIncidentEnergyCost(Severity level)
    {
        return level switch
        {
            Severity.Low => LowIncidentEnergyCost,
            Severity.Medium => MediumIncidentEnergyCost,
            Severity.High => HighIncidentEnergyCost,
            _ => LowIncidentEnergyCost
        };
    }

    
    /*
     * MARK: DISPATCH WITH STRATEGY
     * This will dispatch heroes with a given strategy.
     * @param incidents - The incidents to dispatch.
     * @param strategy - The strategy to use.
     */
    private void DispatchWithStrategy(List<Incident> incidents, IDispatchStrategy strategy)
    {
        foreach (Incident incident in incidents)
        {
            List<Hero> usableHeroes = GetUsableHeroes(incident);
            if (usableHeroes.Count == 0)
            {
                Console.WriteLine($"No available hero can take '{incident.Description}'.");
                continue;
            }

            try
            {
                strategy.SelectHero(incident, usableHeroes);
                Hero selectedHero = SearchEngine.FindFirst(usableHeroes, hero => hero.State == HeroState.Dispatched);
                AssignHero(incident, selectedHero);
            }
            catch (NoSuitableHeroFoundException)
            {
                Console.WriteLine($"No suitable hero found for '{incident.Description}'.");
            }
        }
    }

    /*
     * MARK: DISPATCH MANUALLY
     * This will dispatch heroes manually.
     * @param incidents - The incidents to dispatch.
     */
    private void DispatchManually(List<Incident> incidents)
    {
        foreach (Incident incident in incidents)
        {
            List<Hero> usableHeroes = GetUsableHeroes(incident);
            if (usableHeroes.Count == 0)
            {
                Console.WriteLine($"No available hero can take '{incident.Description}'.");
                continue;
            }

            Console.WriteLine();
            Console.WriteLine($"{incident.Description} (Lvl {incident.Level})");

            for (int i = 0; i < usableHeroes.Count; i++)
            {
                Hero hero = usableHeroes[i];
                int energyCost = GetEnergyCost(hero, incident);
                Console.WriteLine($"  {i + 1}) {hero.Name,-8} | Energy: {hero.EnergyLevel}% | Needed: {energyCost}");
            }

            Console.Write("Select a hero (0 to skip): ");
            if (!int.TryParse(Console.ReadLine(), out int selectedHero) || selectedHero < 0 || selectedHero > usableHeroes.Count)
            {
                Console.WriteLine($"Invalid input. Skipping '{incident.Description}'.");
                continue;
            }

            if (selectedHero == 0)
            {
                continue;
            }

            AssignHero(incident, usableHeroes[selectedHero - 1]);
        }
    }

    
    /*
     * MARK: GET USABLE HEROES
     * This will get the usable heroes for an incident.
     * @param incident - The incident.
     * @return The usable heroes.
     */
    private List<Hero> GetUsableHeroes(Incident incident)
    {
        List<Hero> availableHeroes = SearchEngine.FindAll(_dispatchCenter.Heroes, hero => hero.State == HeroState.Available).ToList();
        return SearchEngine.FindAll(availableHeroes, hero => hero.HasEnoughEnergy(GetEnergyCost(hero, incident))).ToList();
    }

    private List<Incident> GetWaitingIncidents()
    {
        List<Incident> openIncidents = SearchEngine.FindAll(_dispatchCenter.Incidents, incident => !incident.IsResolved).ToList();
        return SearchEngine.FindAll(openIncidents, incident => !_assignments.ContainsKey(incident)).ToList();
    }

    /*
     * MARK: ASSIGN HERO
     * This will assign a hero to an incident.
     * @param incident - The incident.
     * @param hero - The hero.
     */
    private void AssignHero(Incident incident, Hero hero)
    {
        int travelCost = GetTravelEnergyCost(hero, incident);
        int incidentCost = GetIncidentEnergyCost(incident.Level);
        int energyCost = travelCost + incidentCost;

        if (!hero.HasEnoughEnergy(energyCost))
        {
            Console.WriteLine($"{hero.Name} does not have enough energy. Needed: {energyCost}, current: {hero.EnergyLevel}.");
            hero.UpdateState(HeroState.Available);
            return;
        }

        hero.UseEnergy(travelCost);
        _dispatchCenter.DispatchHeroToIncident(incident, hero);
        _assignments[incident] = hero;

        Console.WriteLine($"{hero.Name} is responding to '{incident.Description}'. Travel energy used: {travelCost}.");
    }

    

    /**
     * MARK: ENERGY COSTS
     */
    private static int GetEnergyCost(Hero hero, Incident incident)
    {
        return GetTravelEnergyCost(hero, incident) + GetIncidentEnergyCost(incident.Level);
    }

    private static int GetTravelEnergyCost(Hero hero, Incident incident)
    {
        return (int)Math.Ceiling(hero.CurrentLocation.CalculateDistance(incident.Location));
    }

    /*
     * MARK: PAUSE
     * This will pause the program.
     * @param message - The message to display.
     */

    private static void Pause(string message = "Press any key to continue...")
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Console.ReadKey(true);
    }
}
