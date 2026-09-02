namespace Lesson06;

class Program
{
    static List<Bil> biler = new List<Bil>();
    static List<Udlejning> udlejninger = new List<Udlejning>();

    static void Main(string[] args)
    {
        biler.Add(new Varevogn("1234567890", "Ford", "Focus", 10000, 299, 1000));
        biler.Add(new Personbil("1234567891", "Toyota", "Corolla", 10000, 299, 2));
        Kunde kunde = new Kunde("John Doe", "1234567890", 1234567890);
        

        while (true)
        {
            Console.WriteLine("\nVælg en mulighed:");
            Console.WriteLine("1. Udlej bil");
            Console.WriteLine("2. Tjek Ledighed");
            Console.WriteLine("3. Afslut udlejning");

            Console.Write("\nDit valg: ");
            
            int valg = 0;
            try {
                valg = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input, please enter a valid input");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
        
            switch (valg)
            {
                case 1:
                    UdlejBil(biler, kunde);
                    break;
                case 2:
                    foreach (Bil bil in biler)
                    {
                        Console.WriteLine($"|==============================================|");
                        Console.WriteLine($"Registreringsnummer: {bil.Registreringsnummer}");
                        Console.WriteLine($"Mærke: {bil.Mærke}");
                        Console.WriteLine($"Model: {bil.Model}");
                        Console.WriteLine($"Kilometerstand: {bil.Kilometerstand}");
                        Console.WriteLine($"Dagspris: {bil.Dagspris} kr.");
                        Console.WriteLine(bil is Varevogn ? $"Lastevne: {((Varevogn)bil).LastevneKg} kg" : $"Antalsæder: {((Personbil)bil).AntalSæder}");
                
                        Console.WriteLine($"Status: {(bil.ErLedig() ? "Ledig" : "Udlejet")}");
                        Console.WriteLine();
                    }
                    break;
                case 3:
                    AfslutUdlejning();
                    break;
                default:
                    Console.WriteLine("Invalid input, please enter a valid input");
                    break;
            }
        }
    }

    private static void UdlejBil(List<Bil> biler, Kunde kunde)
    {
        Console.WriteLine("Indtast registreringsnummer: ");
        string registreringsnummer = Console.ReadLine();
        Bil bil = biler.FirstOrDefault(b => b.Registreringsnummer == registreringsnummer);
        if (bil == null)
        {
            Console.WriteLine("Bil ikke fundet");
            return;
        }
        if (!bil.ErLedig())
        {
            Console.WriteLine("Bil er allerede udlejet");
            return;
        }

        Udlejning Nyudlejning = new Udlejning(kunde, bil, DateTime.Now, DateTime.Now.AddDays(1), new EmailKvittering());
        udlejninger.Add(Nyudlejning);
        Console.WriteLine($"Bil {bil.Registreringsnummer} er nu udlejet til {kunde.Navn} fra {Nyudlejning.Startdato} til {Nyudlejning.Slutdato}");
        Console.WriteLine($"Pris: {Nyudlejning.BeregnPris()} kr.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        return;
    }

    private static void AfslutUdlejning()
    {
        Console.WriteLine("Indtast registreringsnummer: ");
        string registreringsnummer = Console.ReadLine();
        Udlejning Nyudlejning = udlejninger.FirstOrDefault(u => u.Bil.Registreringsnummer == registreringsnummer);
        if (Nyudlejning == null)
        {
            Console.WriteLine("Bil ikke fundet");
            return;
        }
        if (!Nyudlejning.Bil.ErLedig())
        {
            Console.WriteLine("Bil er ikke udlejet");
            return;
        }
        Nyudlejning.AfslutUdlejning();
        udlejninger.Remove(Nyudlejning);
        Console.WriteLine("Udlejning afsluttet");
        return;
    }
}
