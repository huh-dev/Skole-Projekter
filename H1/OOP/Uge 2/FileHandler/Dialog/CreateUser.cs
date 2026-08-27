namespace FileHandler
{
    public class CreateUser
    {
        public void CreateUserDialog()
        {
            bool isDialog = true;

            while (isDialog)
            {
                try
                {
                    Console.WriteLine("Enter your firstname:");
                    string firstname = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(firstname))
                        throw new InvalidNameException();

                    Console.WriteLine("Enter your lastname:");
                    string lastname = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(lastname))
                        throw new InvalidNameException();
                    
                    Console.WriteLine("Enter your age:");
                    string age = Console.ReadLine();
                    int ageInt = int.Parse(age);
                    if ((ageInt < 18 || ageInt > 50) && $"{firstname} {lastname}" != "Niels Olesen")
                    {
                        throw new InvalidAgeException();
                    }

                    Console.WriteLine("Enter your email:");
                    string email = Console.ReadLine();

                    if (!email.Contains("@") || !email.Contains("."))
                    {
                        throw new InvalidEmailException(
                            "Invalid email format: Email must contain '@' and '.'",
                            new Exception("Email validation failed")
                        );
                    }

                    try 
                    {
                        string userData = $"{firstname},{lastname},{ageInt},{email}";
                        File.AppendAllText("Files/Users.txt", userData + "\n");
                        Console.WriteLine("User created successfully");

                        try
                        {
                            string[] users = File.ReadAllLines("Files/Users.txt");
                            Console.WriteLine("Users:");
                            foreach (string user in users)
                            {
                                Console.WriteLine(user);
                            }
                        }
                        catch (FileLoadException)
                        {
                            Console.WriteLine("Error: File not found. Please create a new file.");
                        }

                        isDialog = false;
                    }
                    catch (FileLoadException)
                    {
                        Console.WriteLine("Error: File not found. Please create a new file.");
                    }
                }
                catch (InvalidNameException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (InvalidAgeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (InvalidEmailException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Technical details: {ex.InnerException.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: You must enter a whole number for age. Try again.");
                }
            }
        }
    }
}
