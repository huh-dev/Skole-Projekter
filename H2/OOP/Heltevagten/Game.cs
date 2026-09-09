using Heltevagten.Enums;
using Heltevagten.Exceptions;
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
                break;
            case 2:
                Dispatch();
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

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=======================================================================");
        Console.WriteLine("                         WELCOME TO HELTEVAGTEN                        ");
        Console.WriteLine("=======================================================================");
        Console.ResetColor();
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

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=======================================================================");
        Console.WriteLine("                              GOOD LUCK!                               ");
        Console.WriteLine("=======================================================================");
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        Console.Clear();
        _isRunning = true;
    }


    /**
     * MARK: PRINT STATUS
     * This will print the status of the game, including the heroes, incidents, and points.
     */
    private void PrintStatus()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=======================================================================");
        Console.WriteLine($"  HELTEVAGTEN DISPATCH CENTER                 Day {_currentDay} of {_totalDays} | Points: {_points}"); 
        Console.WriteLine("=======================================================================");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"--- HERO ROSTER ({_dispatchCenter.Heroes.Count}) ------------------------------------------");
        Console.ResetColor();

        foreach (Hero hero in _dispatchCenter.Heroes)
        {
            Console.ForegroundColor = hero.State.ToString() == "Available" ? ConsoleColor.Green : ConsoleColor.DarkGray;

            Console.WriteLine($"  - {hero.Name,-8} | Status: {hero.State,-12} | Energy: {hero.EnergyLevel}%");
        }

        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("--- ACTIVE INCIDENTS ------------------------------------------------");
        Console.ResetColor();

        List<Incident> openIncidents = GetOpenIncidents();

        if (openIncidents.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  [✓] All clear! No active incidents reported.");
            Console.ResetColor();
        }
        else
        {
            foreach (Incident incident in openIncidents)
            {
                bool isAssigned = _assignments.TryGetValue(incident, out Hero? hero);
                string heroName = isAssigned ? hero!.Name : "WAITING";

                // Highlight unassigned incidents in yellow to draw focus
                if (!isAssigned)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  [!] {incident.Description,-30} (Lvl {incident.Level}) -> [{heroName}]");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  [>] {incident.Description,-30} (Lvl {incident.Level}) -> [{heroName}]");
                }
            }
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=======================================================================");
        Console.ResetColor();
        Console.WriteLine();
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1) New incident");
        Console.WriteLine("2) Dispatch hero");
        Console.WriteLine("3) Resolve incident");
        Console.WriteLine("0) Quit");
    }

    


    /*
     * MARK: CREATE INCIDENT
     * This will create a new incident, the incident will be created based on the day, if it is day 5, the incident will be a a worse severity, be longer away etc.
     * Incidents can be resolved by any heroes, but there are specific incidents that will be resolved waster, or will bring the severity level down, based if the correct hero is dispatched.
     */
    private void CreateIncident()
    {

        // Get a proper location based on the day
        string locationName = _locations.Keys.ElementAt(_currentDay % _locations.Count);
        Location location = _locations[locationName];

        // Get a proper severity based on the day
        Severity severity = (Severity)(_currentDay % 3 + 1);

        // Create a new incident
        Incident incident = new Incident(
            "Incident at " + locationName,
            location,
            severity
        );




    }
        




    private void Dispatch()
    {
        List<Incident> waiting = GetOpenIncidents()
            .Where(incident => !_assignments.ContainsKey(incident))
            .ToList();

        if (waiting.Count == 0)
        {
            Console.WriteLine("No waiting incidents.");
            return;
        }

        Incident incident = waiting[0];
        List<Hero> availableBefore = _dispatchCenter.Heroes
            .Where(hero => hero.State == HeroState.Available)
            .ToList();

        try
        {
            _dispatchCenter.DispatchHeroToIncident(incident);
        }
        catch (NoSuitableHeroFoundException exception)
        {
            Console.WriteLine(exception.Message);
            return;
        }

        Hero? dispatched = _dispatchCenter.Heroes.FirstOrDefault(hero =>
            hero.State == HeroState.Dispatched && availableBefore.Contains(hero));

        if (dispatched is null)
        {
            Console.WriteLine("No hero was dispatched.");
            return;
        }

        _assignments[incident] = dispatched;
        Console.WriteLine($"{dispatched.Name} goes to {incident.Description}.");
    }

    private List<Incident> GetOpenIncidents()
    {
        return _dispatchCenter.Incidents.Where(incident => !incident.IsResolved).ToList();
    }

}
