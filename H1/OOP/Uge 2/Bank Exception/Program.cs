using Bank;

AccountOwner accountOwner = new AccountOwner(123456, "John", "Doe");
AdminAccount adminAccount = new AdminAccount(1, "John", "Doe Admin", accountOwner);
Console.WriteLine($"Hej {adminAccount.fullname} Din konto er oprettet med id {adminAccount.id} og ejeren er linket med kunden {accountOwner.fullname}");
BankException bankException = new BankException();
// bankException.DepositDialog(accountOwner);
bankException.WithdrawDialog(accountOwner);

// Exception handling
