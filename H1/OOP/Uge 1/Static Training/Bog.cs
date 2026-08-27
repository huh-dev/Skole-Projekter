namespace StaticTraining
{
    internal class Bog
    {
        private static int antalBøger {get; set;} = 0;
        private string titel {get; set;}

        public Bog(string titel)
        {
            this.titel = titel;
            antalBøger++;
        }

        public static void PrintAntalBøger()
        {
            Console.WriteLine($"Der er {antalBøger} bøger i biblioteket");
        }
    }
}
