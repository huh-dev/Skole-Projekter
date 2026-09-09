using Heltevagten.Enums;

namespace Heltevagten.Incidents;

public class Incident
{
    public string Description { get; private set; }
    public Location Location { get; private set; }
    public Severity Level { get; private set; }
    public bool IsResolved { get; private set; }

    public event Action<Incident> IncidentResolved;

    public Incident(string description, Location location, Severity level)
    {
        Description = description;
        Location = location;
        Level = level;
        IsResolved = false;
    }

    public void Resolve()
    {
        IsResolved = true;
        IncidentResolved?.Invoke(this); // Notify the dispatch center that the incident has been resolved
    }
}   