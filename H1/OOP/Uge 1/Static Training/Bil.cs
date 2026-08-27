namespace StaticTraining
{
    internal class Bil
    {
        private static int antalBiler {get; set;} = 0;
        private string model {get; set;}
        private string mærke {get; set;}

        public Bil(string model, string mærke)
        {
            this.model = model;
            this.mærke = mærke;
            antalBiler++;
        }

        public static void PrintAntalBiler()
        {
            Console.WriteLine($"Der er {antalBiler} biler i bilparken");
        }
        
        
    }
}
