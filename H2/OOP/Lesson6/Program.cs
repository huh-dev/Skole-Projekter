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

        List<IKvitteringsAfsender> afsendere =
        [
            new EmailKvittering(),
            new SmsKvittering(),
            new PrintKvittering()
        ];

        foreach (IKvitteringsAfsender afsender in afsendere)
        {
            Udlejning udlejning = new Udlejning(afsender)
            {
                Startdato = new DateTime(2026, 8, 25),
                Slutdato = new DateTime(2026, 8, 31),
                Bil = bil
            };

            udlejning.SendKvittering("Anders Andersen");
        }
    }
}
