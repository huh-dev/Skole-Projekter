namespace Bank
{
    public class BankException
    {
        public void DepositDialog(AccountOwner accountOwner)
        {
            bool isDialog = true;

            while (isDialog)
            {
                try
                {
                    Console.WriteLine("How much money do you want to deposit?");
                    string input = Console.ReadLine();
                    
                    int amount = int.Parse(input);
                    Console.WriteLine($"You have deposited {amount} dollar(s).");
                    accountOwner.bank.Deposit(amount);
                    Console.WriteLine(accountOwner.bank.GetBalance());
                    isDialog = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: You must enter a whole number. Try again.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again with a smaller amount.");
                } finally
                {
                    Console.WriteLine(accountOwner.bank.GetBalance());
                }
            }
        }

        public void WithdrawDialog(AccountOwner accountOwner)
        {
            bool isDialog = true;

            while (isDialog)
            {
                try
                {
                    Console.WriteLine("How much money do you want to withdraw?");
                    string input = Console.ReadLine();

                    int amount = int.Parse(input);
                    string result = accountOwner.bank.Withdraw(amount);
                    Console.WriteLine($"You have withdrawn {amount} dollar(s).");
                    isDialog = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: You must enter a whole number. Try again.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again with a smaller amount.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again with a smaller amount.");
                } finally
                {
                    Console.WriteLine(accountOwner.bank.GetBalance());
                }
            }
        }
    }
}
