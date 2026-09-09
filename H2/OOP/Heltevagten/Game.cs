using Heltevagten.Enums;
using Heltevagten.Heros;
using Heltevagten.Incidents;
using Heltevagten.Strategies;

namespace Heltevagten;

public class Game
{

    
    /**
     * MARK: PROPERTIES
     * These are the properties of the game that are used throughout the game.
     */
    private readonly DispatchCenter _dispatchCenter = new DispatchCenter(new FirstAvailableStrategy());
    private readonly Dictionary<Incident, Hero> _assignments = new Dictionary<Incident, Hero>();

    private const int LowIncidentEnergyCost = 5;
    private const int MediumIncidentEnergyCost = 10;
    private const int HighIncidentEnergyCost = 15;

    // Dictionary of named locations in the game. Each location has a descriptive name and coordinates.
    // The locations are used for calculating the energy required for heroes to travel.
    private readonly Dictionary<string, Location> _locations = new Dictionary<string, Location>
    {
        { "Central Plaza", new Location(0, 0) },
        { "North Park", new Location(2, 2) },
        { "East Bridge", new Location(4, 1) },
        { "South Docks", new Location(6, 0) },
        { "West Fields", new Location(8, 2) },
        { "City Hospital", new Location(10, 1) },
        { "Industrial Zone", new Location(12, 0) },
        { "Mountain Pass", new Location(14, 2) },
        { "River Crossing", new Location(16, 1) },
        { "Old Town", new Location(18, 0) },
        { "University", new Location(20, 2) }
    };

    private int _points;
    private int _currentDay = 1;
    private int _totalDays = 7;
    private bool _isRunning = false;

    
    /**
     * MARK: RUN
     * This is the main method that runs the game.
     * It will print the initial message, setup the game, and then run the game loop.
     */
    public void Run()
    {
        PrintInitMessage();
        RegisterFreeHeroes();

        // Start the incidents for the first day
        StartIncidents();

        while (_isRunning)
        {

            //The functions here are made like this, so this run method is not as long as it would be without them. 

            // Print the status of the game
            PrintStatus();

            // Print the menu of the game
            PrintMenu();

            Console.Write("Choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 3)
            {
                Console.WriteLine("Invalid input. Please enter a number between 0 and 3.");
                Pause();
                continue;
            }

            HandleChoice(choice);
        }
    }

    /**
     * MARK: HEROES DICTIONARY
     * This will be the dictionary of all heroes, keyed by hero name for fast lookup.
     * Many of these heroes will cost points to unlock, and a user will start with three at the start of the game.
     */
    private readonly Dictionary<string, Hero> _allHeroes = new Dictionary<string, Hero>()
    {
        { "Falcon", new FlyingHero("Falcon", 80, 0, new Location(0, 0)) },
        { "Atlas", new StrongHero("Atlas", 80, 0, new Location(3, 3)) },
        { "Pulse", new HealingHero("Pulse", 75, 0, new Location(4, 1)) },

        { "Tempest", new FlyingHero("Tempest", 100, 80, new Location(2, 8)) },
        { "Crusher", new StrongHero("Crusher", 110, 90, new Location(5, 5)) },
        { "Serenity", new HealingHero("Serenity", 95, 70, new Location(7, 1)) },
        { "Zephyr", new FlyingHero("Zephyr", 92, 65, new Location(10, 4)) },
        { "Mammoth", new StrongHero("Mammoth", 120, 110, new Location(6, 12)) },
        { "Beacon", new HealingHero("Beacon", 88, 55, new Location(12, 2)) },
        { "Titan", new StrongHero("Titan", 105, 85, new Location(15, 7)) }
    };


    /**
     * MARK: REGISTER FREE HEROES
     * This will register the free heroes to the dispatch center.
     */
    private void RegisterFreeHeroes()
    {
        _dispatchCenter.RegisterHero(_allHeroes["Falcon"]);
        _dispatchCenter.RegisterHero(_allHeroes["Atlas"]);
        _dispatchCenter.RegisterHero(_allHeroes["Pulse"]);
    }

    private void HandleChoice(int choice)
    {
        switch (choice)
        {
            case 0:
                _isRunning = false;
                Console.WriteLine("Dispatch center signing off. Stay safe out there.");
                break;
            case 1:
                DispatchHero();
                break;
            case 2:
                OpenHeroShop();
                break;
            case 3:
                CompleteResponses();
                break;
        }
    }

    /**
     * MARK: INIT MESSAGE
     * This will be the start print, explaining the game and the it's general concept.
     * This will only be printed once, at the start of the game.
     */
    private void PrintInitMessage()
    {
        Console.Clear();

        Console.WriteLine("=======================================================================");
        Console.WriteLine("                         WELCOME TO HELTEVAGTEN                        ");
        Console.WriteLine("=======================================================================");
        Console.WriteLine();

        Console.WriteLine("  You are a dispatcher working for Heltevagten, an organization that");
        Console.WriteLine("  controls the heroes found in the city.");
        Console.WriteLine();
        Console.WriteLine("  Your job is to dispatch heroes to incidents as they are reported.");
        Console.WriteLine("  You have 7 days to complete as many incidents as possible.");
        Console.WriteLine();
        Console.WriteLine("  Earn points based on the amount and severity of completed incidents.");
        Console.WriteLine("  Use your points to unlock new heroes and save more citizens!");
        Console.WriteLine();

        Console.WriteLine("=======================================================================");
        Console.WriteLine("                              GOOD LUCK!                               ");
        Console.WriteLine("=======================================================================");

        Console.WriteLine();
        Pause("Press any key to continue...");

        _isRunning = true;
    }


    /**
     * MARK: PRINT STATUS
     * This will print the status of the game, including the heroes, incidents, and points.
     */
    private void PrintStatus()
    {
        Console.Clear();

        Console.WriteLine("=======================================================================");
        Console.WriteLine($"  HELTEVAGTEN DISPATCH CENTER                 Day {_currentDay} of {_totalDays} | Points: {_points}");
        Console.WriteLine("=======================================================================");
        Console.WriteLine();

        Console.WriteLine($"--- HERO ROSTER ({_dispatchCenter.Heroes.Count}) ------------------------------------------");

        foreach (Hero hero in _dispatchCenter.Heroes)
        {
            Console.WriteLine($"  - {hero.Name,-8} | Status: {hero.State,-12} | Energy: {hero.EnergyLevel}%");
        }

        Console.WriteLine();
        Console.WriteLine("--- ACTIVE INCIDENTS ------------------------------------------------");

        List<Incident> openIncidents = GetOpenIncidents();

        if (openIncidents.Count == 0)
        {
            Console.WriteLine("  [OK] All clear! No active incidents reported.");
        }
        else
        {
            foreach (Incident incident in openIncidents)
            {
                bool isAssigned = _assignments.TryGetValue(incident, out Hero? hero);
                if (isAssigned)
                {
                    Console.WriteLine($"  [>] {incident.Description,-45} (Lvl {incident.Level}) -> {hero!.Name}");
                }
                else
                {
                    Console.WriteLine($"  [!] {incident.Description,-45} (Lvl {incident.Level}) -> WAITING");
                }
            }
        }

        Console.WriteLine("=======================================================================");
        Console.WriteLine();
    }

    private static void PrintMenu()
    {
        Console.WriteLine("1) Dispatch Hero");
        Console.WriteLine("2) Hero Shop");
        Console.WriteLine("3) Complete responses");
        Console.WriteLine("0) Quit");
    }

    


    /*
     * MARK: START INCIDENTS    
     * This will start the incidents for the day, the incidents will be created based on the day, if it is day 5, the incident will be a a worse severity, be longer away etc.
     * Incidents can be resolved by any heroes, but there are specific incidents that will be resolved waster, or will bring the severity level down, based if the correct hero is dispatched.
     */
    private void StartIncidents()
    {

        List<IncidentScenario> incidents = IncidentScenarios.ForDay(_currentDay);

        foreach (IncidentScenario scenario in incidents)
        {
            _dispatchCenter.ReportIncident(new Incident(scenario.Description, _locations[scenario.LocationName], scenario.Severity, scenario.Day, scenario.Points, scenario.Duration));
        }

    }
        




    /**
     * MARK: DISPATCH HERO
     * This will dispatch a hero to an incident.
     */
    private void DispatchHero()
    {
        List<Incident> openIncidents = GetWaitingIncidents();

        if (openIncidents.Count == 0)
        {
            Console.WriteLine("No incidents waiting for a hero.");
            Pause();
            return;
        }

        for (int i = 0; i < openIncidents.Count; i++)
        {
            Incident incident = openIncidents[i];
            Console.WriteLine($"  {i + 1}) {incident.Description} (Lvl {incident.Level})");
        }

        Console.Write("Select an incident: ");
        if (!int.TryParse(Console.ReadLine(), out int selectedIncident) || selectedIncident < 1 || selectedIncident > openIncidents.Count)
        {
            Console.WriteLine($"Invalid input. Please enter a number between 1 and {openIncidents.Count}.");
            Pause();
            return;
        }

        Incident selected = openIncidents[selectedIncident - 1];

        Console.Clear();
        Console.WriteLine($"You have now chosen the incident: {selected.Description}");
        Console.WriteLine("--------------------------------");

        List<Hero> availableHeroes = _dispatchCenter.Heroes.Where(hero => hero.State == HeroState.Available).ToList();
        if (availableHeroes.Count == 0)
        {
            Console.WriteLine("No available heroes.");
            Pause();
            return;
        }

        List<Hero> usableHeroes = availableHeroes
            .Where(hero => hero.HasEnoughEnergy(GetEnergyCost(hero, selected)))
            .ToList();
        if (usableHeroes.Count == 0)
        {
            Console.WriteLine("No available heroes have enough energy for this incident.");
            Pause();
            return;
        }

        Console.Write("Do you want to dispatch the closest hero? (y/n): ");
        string? closestHeroChoice = Console.ReadLine();
        if (closestHeroChoice == "y")
        {
            Hero closestHero = usableHeroes
                .OrderBy(hero => hero.CurrentLocation.CalculateDistance(selected.Location))
                .First();

            AssignHero(selected, closestHero);
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Please select a hero to dispatch to the incident.");

        for (int i = 0; i < usableHeroes.Count; i++)
        {
            Hero hero = usableHeroes[i];
            int energyCost = GetEnergyCost(hero, selected);
            Console.WriteLine($"  {i + 1}) {hero.Name,-8} | Energy: {hero.EnergyLevel}% | Needed: {energyCost}");
        }

        Console.Write("Select a hero: ");
        if (!int.TryParse(Console.ReadLine(), out int selectedHero) || selectedHero < 1 || selectedHero > usableHeroes.Count)
        {
            Console.WriteLine($"Invalid input. Please enter a number between 1 and {usableHeroes.Count}.");
            Pause();
            return;
        }

        Hero chosenHero = usableHeroes[selectedHero - 1];
        AssignHero(selected, chosenHero);
    }

    private void AssignHero(Incident incident, Hero hero)
    {
        int travelCost = GetTravelEnergyCost(hero, incident);
        int incidentCost = GetIncidentEnergyCost(incident.Level);
        int energyCost = travelCost + incidentCost;

        if (!hero.HasEnoughEnergy(energyCost))
        {
            Console.WriteLine($"{hero.Name} does not have enough energy. Needed: {energyCost}, current: {hero.EnergyLevel}.");
            Pause();
            return;
        }

        hero.UseEnergy(travelCost);
        _dispatchCenter.DispatchHeroToIncident(incident, hero);
        _assignments[incident] = hero;

        ShowLoading($"{hero.Name} is heading to the incident");
        Console.WriteLine($"{hero.Name} is responding. Travel energy used: {travelCost}.");
        Pause();
    }

    private void CompleteResponses()
    {
        if (_assignments.Count == 0)
        {
            Console.WriteLine("No heroes are currently responding to incidents.");
            Pause();
            return;
        }

        ShowLoading("Heroes are completing their incidents");

        foreach (KeyValuePair<Incident, Hero> assignment in _assignments.ToList())
        {
            Incident incident = assignment.Key;
            Hero hero = assignment.Value;
            int incidentCost = GetIncidentEnergyCost(incident.Level);

            hero.UseEnergy(incidentCost);
            incident.Resolve();
            hero.UpdateState(HeroState.Available);
            _points += incident.Points;
            _assignments.Remove(incident);

            Console.WriteLine($"{hero.Name} resolved '{incident.Description}'. Work energy used: {incidentCost}. +{incident.Points} points.");
        }

        Pause();
    }

    /**
     * MARK: HERO SHOP
     * Unlock new heroes with points earned from resolved incidents.
     */
    private void OpenHeroShop()
    {
        List<Hero> heroesForSale = _allHeroes.Values
            .Where(hero => !_dispatchCenter.Heroes.Contains(hero))
            .ToList();

        if (heroesForSale.Count == 0)
        {
            Console.WriteLine("The shop is empty. You already unlocked every hero.");
            Pause();
            return;
        }

        Console.WriteLine($"Hero Shop | Points: {_points}");
        Console.WriteLine("--------------------------------");

        for (int i = 0; i < heroesForSale.Count; i++)
        {
            Hero hero = heroesForSale[i];
            Console.WriteLine($"  {i + 1}) {hero.Name,-8} | {GetHeroRole(hero),-8} | Cost: {hero.Cost} points");
        }

        Console.WriteLine("  0) Back");
        Console.Write("Select a hero to unlock: ");

        if (!int.TryParse(Console.ReadLine(), out int selectedHero) || selectedHero < 0 || selectedHero > heroesForSale.Count)
        {
            Console.WriteLine($"Invalid input. Please enter a number between 0 and {heroesForSale.Count}.");
            Pause();
            return;
        }

        if (selectedHero == 0)
        {
            return;
        }

        Hero heroToUnlock = heroesForSale[selectedHero - 1];
        if (_points < heroToUnlock.Cost)
        {
            Console.WriteLine($"Not enough points. {heroToUnlock.Name} costs {heroToUnlock.Cost} points.");
            Pause();
            return;
        }

        _points -= heroToUnlock.Cost;
        _dispatchCenter.RegisterHero(heroToUnlock);
        ShowLoading($"Unlocking {heroToUnlock.Name}");
        Console.WriteLine($"{heroToUnlock.Name} has joined the roster. Points left: {_points}");
        Pause();
    }

    private List<Incident> GetOpenIncidents()
    {
        return _dispatchCenter.Incidents.Where(incident => !incident.IsResolved).ToList();
    }

    private List<Incident> GetWaitingIncidents()
    {
        return GetOpenIncidents().Where(incident => !_assignments.ContainsKey(incident)).ToList();
    }

    private int GetEnergyCost(Hero hero, Incident incident)
    {
        return GetTravelEnergyCost(hero, incident) + GetIncidentEnergyCost(incident.Level);
    }

    private static int GetTravelEnergyCost(Hero hero, Incident incident)
    {
        return (int)Math.Ceiling(hero.CurrentLocation.CalculateDistance(incident.Location));
    }

    private static int GetIncidentEnergyCost(Severity level)
    {
        return level switch
        {
            Severity.Low => LowIncidentEnergyCost,
            Severity.Medium => MediumIncidentEnergyCost,
            Severity.High => HighIncidentEnergyCost,
            _ => LowIncidentEnergyCost
        };
    }

    private static string GetHeroRole(Hero hero)
    {
        return hero switch
        {
            FlyingHero => "Flying",
            StrongHero => "Strong",
            HealingHero => "Healing",
            _ => "Hero"
        };
    }

    private static void ShowLoading(string message)
    {
        Console.Write(message);

        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(350);
            Console.Write(".");
        }

        Console.WriteLine(" done.");
    }

    private static void Pause(string message = "Press any key to continue...")
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Console.ReadKey(true);
    }

}
