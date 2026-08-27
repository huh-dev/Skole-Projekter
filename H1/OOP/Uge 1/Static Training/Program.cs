namespace StaticTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            BankKonto.UpdateRente(0.05m);
            BankKonto konto1 = new BankKonto();
            konto1.Indsaet(1000);
            konto1.Hæv(500);
            Console.WriteLine($"Konto 1 har en saldo på: {konto1.Saldo}");
        }
    }
}
