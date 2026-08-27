namespace StaticTraining
{
    internal class Elev
    {
        private static int antalElever {get; set;} = 0;
        private string navn {get; set;}

        public Elev(string navn)
        {
            this.navn = navn;
            antalElever++;
        }

        public static void PrintAntalElever() 
        {
            Console.WriteLine($"Der er {antalElever} elever i klassen");
        }
        
    }
}
