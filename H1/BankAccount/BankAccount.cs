namespace BankAccount
{
    public class BankAccount
    {
        public static decimal Balance = 0;
        private static bool canReadBalance = true;
        private static bool canWithdraw = true;
        private static bool canDeposit = true;


        public static void Setup()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Vælg hvilken handling du vil udføre:");
                Console.WriteLine("0) Exit\n" + (canDeposit ? "1) Deposit\n" : "") + (canWithdraw ? "2) Withdraw\n" : "") + (canReadBalance ? "3) Check Balance\n" : ""));
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        keepRunning = false;
                        break;
                    case "1":
                        Deposit();
                        break;
                    case "2":
                        Withdraw();
                        break;
                    case "3":
                        CheckBalance();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        break;
                }
            }
        }

        static void Deposit()
        {
            Console.WriteLine("Indtast beløb til indbetaling:");
            decimal amount = ParseDecimal(Console.ReadLine());
            Balance += amount;

            CheckDecimal(amount);

            Console.WriteLine($"Indbetalingen på {amount} er blevet tilføjet til kontoen. Nuværende saldo: {Balance}");
            Thread.Sleep(2000);
            Console.Clear();
        }

        static void Withdraw()
        {
            Console.WriteLine("Indtast beløb til udbetaling:");
            decimal amount = ParseDecimal(Console.ReadLine());

            CheckDecimal(amount);

            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Udbetalingen på {amount} er blevet tilføjet til kontoen. Nuværende saldo: {Balance}");
            }
            else
            {
                Console.WriteLine("Udbetalingen er ikke mulig. Kontoen har ikke tilstrækkeligt saldo.");
            }

            Thread.Sleep(2000);
            Console.Clear();
        }

        static void CheckBalance()
        {
            Console.WriteLine($"Nuværende saldo: {Balance}");
            Thread.Sleep(2000);
            Console.Clear();
        }

        //Helper Method
        static void CheckDecimal(decimal amount)
        {
            if (amount < 1)
            {
                Console.WriteLine("Indtast et beløb større end 0.");
                Thread.Sleep(2000);
                Console.Clear();
                return;
            }
        }

        static decimal ParseDecimal(string input)
        {
            decimal.TryParse(input, out decimal amount);
            return amount;
        }
    }
}