namespace StaticTraining
{
    internal class Skole
    {
        private static int antalSkoler {get; set;} = 0;
        private string navn {get; set;}

        public Skole(string navn)
        {
            this.navn = navn;
            antalSkoler++;
        }

        public static void PrintAntalSkoler()
        {
            Console.WriteLine($"Der er {antalSkoler} skoler i landet");
        }
    }
}
