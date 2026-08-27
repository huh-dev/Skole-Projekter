namespace Bank
{
    public sealed class AdminAccount : Users
    {
        public int id { get; set; }
        public AccountOwner accountOwner { get; set; }

        public AdminAccount(int id, string firstname, string lastname, AccountOwner accountOwner)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.accountOwner = accountOwner;
        }
        
    }
}
