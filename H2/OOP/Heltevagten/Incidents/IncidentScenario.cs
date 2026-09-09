using Heltevagten.Enums;

namespace Heltevagten.Incidents;

public class IncidentScenario
{
    public string Description { get; private set; }
    public Severity Severity { get; private set; }
    public Risk Risk { get; private set; }
    public string LocationName { get; private set; }
    public int Day { get; private set; }
    public int Points { get; private set; }
    public int Duration { get; private set; }
    public IncidentScenario(string description, Severity severity, Risk risk, string locationName, int day, int points, int duration)
    {
        Description = description;
        Severity = severity;
        Risk = risk;
        LocationName = locationName;
        Day = day;
        Points = points;
        Duration = duration;
    }
}
