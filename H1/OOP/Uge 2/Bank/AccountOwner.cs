namespace Bank
{
    public sealed class AccountOwner : Users
    {
        public int customerId { get; set; }
        private Bank bank { get; set; }

        public AccountOwner(int customerId, string firstname, string lastname)
        {
            if (customerId.ToString().Length != 6)
            {
                throw new ArgumentException("Customer ID must be 6 digits");
            }

            this.customerId = customerId;
            this.firstname = firstname;
            this.lastname = lastname;

            Bank bank = new Bank(1000);
            Console.WriteLine(bank.balance);
            this.bank = bank;

        }
    }
}
