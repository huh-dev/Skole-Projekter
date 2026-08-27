namespace Nedarvning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dyr dyr = new Dyr();
            Hund hund = new Hund();
            Kat kat = new Kat();
            dyr.LavLyd();
            hund.LavLyd();
            kat.LavLyd();
        }
    }
}
