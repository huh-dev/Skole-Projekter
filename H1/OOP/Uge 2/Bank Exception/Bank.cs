namespace Bank
{
    public class Bank
    {
        private double balance;

        public Bank(double balance)
        {
            this.balance = balance;
        }

        public string Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount cant be negative");
            }


            balance += amount;
            return "Deposit successful you now have " + balance + " dollars";
        }

        public string Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount cant be negative");
            }

            if (amount > balance)
            {
                throw new InvalidOperationException($"Insufficient balance. You only have {balance} dollars.");
            }

            balance -= amount;
            return "Withdrawal successful you now have " + balance + " dollars";
        }

        public string GetBalance() 
        {
            return "You have " + balance + " dollars";
        }
    }
}
