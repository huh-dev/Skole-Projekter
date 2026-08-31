namespace Lesson06;

class Program
{
    static void Main(string[] args)
    {
        Bil bil = new Bil
        {
            Registreringsnummer = "AB12345",
            Model = "Corolla",
            Mærke = "Toyota",
            Kilometerstand = 45000,
            Dagspris = 500,
            ErUdlejet = true
        };

        Kunde kunde = new Kunde
        {
            Navn = "Anders Andersen",
            Kørekortnummer = "1234567890",
            Telefonnummer = 1234567890
        };

        Udlejning udlejning = new Udlejning(new EmailKvittering())
        {
            Startdato = new DateTime(2026, 8, 25),
            Slutdato = new DateTime(2026, 8, 31),
            Bil = bil
        };

        Udlejning udlejningSms = new Udlejning(new SmsKvittering())
        {
            Startdato = new DateTime(2026, 8, 25),
            Slutdato = new DateTime(2026, 8, 31),
            Bil = bil
        };

        Udlejning udlejningPrint = new Udlejning(new PrintKvittering())
        {
            Startdato = new DateTime(2026, 8, 25),
            Slutdato = new DateTime(2026, 8, 31),
            Bil = bil
        };

        // Afslut udlejningen
        udlejning.Afslut(new DateTime(2026, 8, 31), 45000);
        udlejningSms.Afslut(new DateTime(2026, 8, 31), 45000);
        udlejningPrint.Afslut(new DateTime(2026, 8, 31), 45000);
        Console.WriteLine($"Udlejningen er afsluttet. Pris: {udlejning.BeregnPris()}");
        Console.WriteLine($"Udlejningen er afsluttet. Pris: {udlejningSms.BeregnPris()}");
        Console.WriteLine($"Udlejningen er afsluttet. Pris: {udlejningPrint.BeregnPris()}");

        // Generér kvittering Email, Sms eller Print
        string kvitteringEmail = udlejning.GenerérKvittering(kunde.Navn);
        Console.WriteLine(kvitteringEmail);

        string kvitteringSms = udlejningSms.GenerérKvittering(kunde.Navn);
        Console.WriteLine(kvitteringSms);

        string kvitteringPrint = udlejningPrint.GenerérKvittering(kunde.Navn);
        Console.WriteLine(kvitteringPrint);
    }
}
