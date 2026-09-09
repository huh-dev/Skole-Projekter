namespace Heltevagten.Incidents;

public struct Location
{
    public double x { get; private set; }
    public double y { get; private set; }

    public double CalculateDistance(Location other)
    {
        return Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2));
    }
}