using Heltevagten.Enums;

namespace Heltevagten.Incidents;

public static class IncidentScenarios
{
    public static readonly IReadOnlyList<IncidentScenario> All = new List<IncidentScenario>
    {
        new IncidentScenario("Dumpster fire spreading toward the food stalls", Severity.Low, Risk.Fire, "Central Plaza", 1),
        new IncidentScenario("Crowd crush on the plaza steps", Severity.Low, Risk.Medical, "Central Plaza", 1),
        new IncidentScenario("Loose market stall collapses on a vendor", Severity.Low, Risk.Collapse, "Central Plaza", 1),
        new IncidentScenario("Pond overflow floods the park footpath", Severity.Low, Risk.Flood, "North Park", 1),
        new IncidentScenario("Child stuck in a tall oak after a kite snag", Severity.Low, Risk.Aerial, "North Park", 1),
        new IncidentScenario("Loose railing hanging over the walkway", Severity.Low, Risk.Collapse, "East Bridge", 1),

        new IncidentScenario("Smoke from a plaza cafe kitchen fire", Severity.Low, Risk.Fire, "Central Plaza", 2),
        new IncidentScenario("Fallen tree traps a cyclist on the path", Severity.Low, Risk.Collapse, "North Park", 2),
        new IncidentScenario("Picnic group overcome by heat exhaustion", Severity.Medium, Risk.Medical, "North Park", 2),
        new IncidentScenario("Cyclist pile-up in the bridge lane", Severity.Medium, Risk.Medical, "East Bridge", 2),
        new IncidentScenario("Engine fire on a moored fishing boat", Severity.Low, Risk.Fire, "South Docks", 2),
        new IncidentScenario("Crane cable snaps above the loading bay", Severity.Medium, Risk.Aerial, "South Docks", 2),

        new IncidentScenario("Rising water covers the lower bridge span", Severity.Medium, Risk.Flood, "East Bridge", 3),
        new IncidentScenario("Cargo crate stack topples on the pier", Severity.Medium, Risk.Collapse, "South Docks", 3),
        new IncidentScenario("Storm surge floods the loading bays", Severity.Medium, Risk.Flood, "South Docks", 3),
        new IncidentScenario("Dry grass fire spreading toward the barns", Severity.Medium, Risk.Fire, "West Fields", 3),
        new IncidentScenario("Tractor rollover pins a farm worker", Severity.Medium, Risk.Collapse, "West Fields", 3),
        new IncidentScenario("Ambulance queue blocking the ER doors", Severity.Medium, Risk.Medical, "City Hospital", 3),

        new IncidentScenario("Pesticide leak from a ruptured farm tank", Severity.Medium, Risk.Chemical, "West Fields", 4),
        new IncidentScenario("Worker stranded on a silo roof in high wind", Severity.Medium, Risk.Aerial, "West Fields", 4),
        new IncidentScenario("Power line down across the hospital courtyard", Severity.Medium, Risk.Aerial, "City Hospital", 4),
        new IncidentScenario("Chlorine leak in the hospital basement", Severity.Medium, Risk.Chemical, "City Hospital", 4),
        new IncidentScenario("Warehouse blaze near stacked chemical drums", Severity.Medium, Risk.Fire, "Industrial Zone", 4),
        new IncidentScenario("Forklift crush under a fallen steel beam", Severity.Medium, Risk.Collapse, "Industrial Zone", 4),

        new IncidentScenario("Mass casualty surge overflowing the ER", Severity.High, Risk.Medical, "City Hospital", 5),
        new IncidentScenario("Factory wall collapse traps night-shift workers", Severity.High, Risk.Collapse, "Industrial Zone", 5),
        new IncidentScenario("Toxic cloud from a ruptured plant pipe", Severity.High, Risk.Chemical, "Industrial Zone", 5),
        new IncidentScenario("Hikers stranded on a sheer cliff ledge", Severity.High, Risk.Aerial, "Mountain Pass", 5),
        new IncidentScenario("Brush fire closing the mountain road", Severity.High, Risk.Fire, "Mountain Pass", 5),
        new IncidentScenario("Bridge supports cracking in floodwater", Severity.High, Risk.Flood, "River Crossing", 5),

        new IncidentScenario("Rockslide buries the mountain road", Severity.High, Risk.Collapse, "Mountain Pass", 6),
        new IncidentScenario("Tanker spill contaminates the river", Severity.High, Risk.Chemical, "River Crossing", 6),
        new IncidentScenario("Ferry passengers trapped in rising water", Severity.High, Risk.Flood, "River Crossing", 6),
        new IncidentScenario("Timber house row burning through Old Town", Severity.High, Risk.Fire, "Old Town", 6),
        new IncidentScenario("Bomb scare emptying the market square", Severity.High, Risk.Collapse, "Old Town", 6),
        new IncidentScenario("Lab fire spreading through campus halls", Severity.High, Risk.Fire, "University", 6),

        new IncidentScenario("Historic tower leaning after a quake", Severity.High, Risk.Collapse, "Old Town", 7),
        new IncidentScenario("Collapsed cafe with civilians trapped", Severity.High, Risk.Medical, "Old Town", 7),
        new IncidentScenario("Lecture hall stampede with many injured", Severity.High, Risk.Medical, "University", 7),
        new IncidentScenario("Students trapped on a burning rooftop", Severity.High, Risk.Aerial, "University", 7),
        new IncidentScenario("Chemical hood explosion in the science wing", Severity.High, Risk.Chemical, "University", 7),
        new IncidentScenario("Cable car stalled above the river gorge", Severity.High, Risk.Aerial, "River Crossing", 7)
    };

    public static List<IncidentScenario> ForDay(int day)
    {
        return All.Where(scenario => scenario.Day == day).ToList();
    }

    public static List<IncidentScenario> ForSeverity(Severity severity)
    {
        return All.Where(scenario => scenario.Severity == severity).ToList();
    }

    public static List<IncidentScenario> ForRisk(Risk risk)
    {
        return All.Where(scenario => scenario.Risk == risk).ToList();
    }

    public static List<IncidentScenario> ForLocation(string locationName)
    {
        return All.Where(scenario => scenario.LocationName == locationName).ToList();
    }
}
