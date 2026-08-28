namespace Lesson05;

class Program
{
    static void Main(string[] args)
    {

        List<Vehicle> vehicles = new List<Vehicle>();

        Bil bil = new Bil("Toyota", "Corolla", "200");
        Motorcykel motorcykel = new Motorcykel("Honda", "CBR", "200");
        vehicles.Add(bil);
        vehicles.Add(motorcykel);

        foreach (var vehicle in vehicles)
        {
            vehicle.VisGrundInfo();
            Console.WriteLine($"Afgift: {vehicle.BeregnAfgift()} kr.");
        }

        Console.WriteLine($"Forsikringspraemie: {bil.BeregnForsikringspraemie(10000)} kr.");
        Console.WriteLine($"Lejepris: {bil.BeregnLejepris(10)} kr.");
        Console.WriteLine($"Afgift: {bil.BeregnAfgift()} kr.");
    }
}