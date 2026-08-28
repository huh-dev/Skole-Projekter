namespace Lesson05;

public abstract class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string TopSpeedKmh { get; set; }

    public Vehicle(string brand, string model, string topSpeedKmh)
    {
        Brand = brand;
        Model = model;
        TopSpeedKmh = topSpeedKmh;
    }

    public virtual void VisGrundInfo()
    {
        Console.WriteLine($"Brand: {Brand}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Top Speed: {TopSpeedKmh} km/h");
    }

    public abstract decimal BeregnAfgift();

    
}