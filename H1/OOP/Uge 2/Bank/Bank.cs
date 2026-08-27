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
                return "Amount must be greater than 0";
            }

            balance += amount;
            return "Deposit successful you now have " + balance + " dollars";
        }

        public string Withdraw(double amount)
        {
            if (amount <= 0)
            {
                return "Amount must be greater than 0";
            }

            if (amount > balance)
            {
                return "Insufficient balance";
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
