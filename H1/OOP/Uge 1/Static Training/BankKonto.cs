namespace StaticTraining
{
    internal class BankKonto
    {
        private static decimal rente {get; set;} = 0.05m;
        private decimal saldo {get; set;}


        public static void UpdateRente(decimal nyRente)
        {
            rente = nyRente;
        }

        public void Indsaet(decimal belob)
        {
            //Check if belob is greater than 0
            if (belob > 0)
            {
                saldo += belob;
            }
            else
            {
                Console.WriteLine("Beløbet er negativt");
            }
        }

        public void Hæv(decimal belob)
        {
            //Check if belob is greater than saldo
            if (belob > saldo)
            {
                Console.WriteLine("Beløbet er større end saldoen");
            }
            else
            {
                saldo -= belob;
            }
        }
    }
}
