using Lesson05.Interfaces;

namespace Lesson05;

class Program
{
    static void Main(string[] args)
    {
        // Vehicle (Koeretoej) kan ikke instantieres direkte, fordi klassen er abstract
        // Vehicle koeretoej = new Vehicle("Generisk", "Model", "100"); // denne linje ville give compilerfejl

        Bil bil = new Bil("Toyota", "Corolla", "200");
        Motorcykel motorcykel = new Motorcykel("Honda", "CBR", "200");

        List<Vehicle> vehicles = new List<Vehicle> { bil, motorcykel };

        Console.WriteLine("Polymorfi via basisklasse (Vehicle):");
        foreach (Vehicle vehicle in vehicles)
        {
            vehicle.VisGrundInfo();
            Console.WriteLine($"Afgift: {vehicle.BeregnAfgift()} kr.");
            Console.WriteLine();
        }

        List<IUdlejelig> udlejelige = new List<IUdlejelig> { bil, motorcykel };

        Console.WriteLine("Polymorfi via interface (IUdlejelig):");
        foreach (IUdlejelig udlejelig in udlejelige)
        {
            Console.WriteLine($"Lejepris for 10 dage: {udlejelig.BeregnLejepris(10)} kr.");
        }

        List<IForsikringspligtig> forsikringspligtige = new List<IForsikringspligtig> { bil };

        Console.WriteLine();
        Console.WriteLine("Polymorfi via interface (IForsikringspligtig):");
        foreach (IForsikringspligtig forsikringspligtig in forsikringspligtige)
        {
            Console.WriteLine($"Forsikringspraemie: {forsikringspligtig.BeregnForsikringspraemie(10000)} kr.");
        }
    }
}
