using System.Text.RegularExpressions; 

class Program
{
    static void Main(string[] args)
    {
        //MARK: Start og application
        Console.WriteLine("Velkommen til telefonbogen");
        bool continueProgram = true;

        do 
        {
            Console.WriteLine("Vælg en handling: \n0) Exit \n1) Tilføj kontakt \n2) Søg efter kontakt \n3) Rediger kontakt \n4) Slet kontakt \n5) Sorter kontakter");
            string menuValue = Console.ReadLine() ?? string.Empty;
            
            //MARK: Menu switch
            switch (menuValue)
            {
                case "0":
                    continueProgram = false;
                    break;
                case "1":
                    AddContact();
                    break;
                case "2":
                    SearchContact();
                    break;
                case "3":
                    EditContact();
                    break;
                case "4":
                    DeleteContact();
                    break;
                case "5":
                    SortContacts();
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg");
                    break;
            }
        } while (continueProgram);

        Console.ReadKey();
    }

    //MARK: Add Contact Method
    static void AddContact()
    {
        bool addMore = true;
        while (addMore)
        {
            //Input fields from the user, to determind the contact information
            Console.WriteLine("Indtast navn: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Indtast efternavn: ");
            string lastName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Indtast telefonnummer: ");
            int phoneNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indtast email: ");
            string email = Console.ReadLine() ?? string.Empty;
            //Validate phonenumber
            if (phoneNumber.ToString().Length != 8)
            {
                Console.WriteLine("Telefonnummer skal være 8 cifre langt");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                return;
            }

            //Regex check to see if the email is valid
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                Console.WriteLine("Emailen er gyldig");
            }
            else
            {
                Console.WriteLine("Emailen er ikke gyldig");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                return;
            }

            //Validate if the user information already exists
            string[] lines = File.ReadAllLines("kontakter.txt");
            foreach (string line in lines)
            {
                if (line.Contains(name))
                {
                    Console.WriteLine("Kontakt allerede tilføjet");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    return;
                }
            }


            //Append the contact to the file
            string contact = $"{name};{phoneNumber};{email}\n";
            
            // Create the file with headers if it doesn't exist
            if (!File.Exists("kontakter.txt"))
            {
                string headers = "Navn;Telefon;Email\n";
                File.WriteAllText("kontakter.txt", headers);
            }
            
            File.AppendAllText("kontakter.txt", contact);
            Console.WriteLine("Kontakt tilføjet");

            //Display a message and wait for user input to go back to the menu
            Console.WriteLine("Ønsker du at tilføje en ny kontakt? (y/n)");
            string answer = Console.ReadLine() ?? string.Empty;
            if (answer.ToLower() != "y")
            {
                addMore = false;
            }
            Console.Clear();
        }
    }

    //MARK: Search Contact Method
    static void SearchContact()
    {
        bool searchMore = true;
        while (searchMore)
        {
            //Get the user input for the search
            Console.WriteLine("Indtast navn, efternavn, email eller telefonnummer: ");
            string search = Console.ReadLine() ?? string.Empty;
            bool found = false;

            //Read all lines from the file
            string[] lines = File.ReadAllLines("kontakter.txt");
            foreach (string line in lines)
            {
                if (line.Contains(search))
                {
                    Console.WriteLine(line);
                    found = true;
                }
            }

            //If no contact is found, display a message
            if (!found)
            {
                Console.WriteLine("Ingen kontakt fundet");
            }

            //Validation
            Console.WriteLine("Ønsker du at søge igen? (y/n)");
            string answer = Console.ReadLine() ?? string.Empty;
            if (answer.ToLower() != "y")
            {
                searchMore = false;
            }
            Console.Clear();
        }
    }

    //MARK: Edit Contact Method
    static void EditContact()
    {
        bool editMore = true;

        while (editMore)
        {
            //Get the user input for the name to edit
            Console.WriteLine("Find en bruger");
            string search = Console.ReadLine() ?? string.Empty;

            //Read all lines from the file
            string[] lines = File.ReadAllLines("kontakter.txt");
            foreach (string line in lines)
            {
                if (line.Contains(search))
                {
                    //Display the user to edit
                    Console.WriteLine(line);
                }
            }

            //Validation
            Console.WriteLine("Vil du redigere denne bruger? (y/n)");
            string answer = Console.ReadLine() ?? string.Empty;
            if (answer.ToLower() == "y")
            {
                //Start the edit mode
                bool editMode = true;

                while (editMode)
                {
                    Console.WriteLine("Hvilken information vil du redigere? \n1) Navn \n2) Telefonnummer \n3) Email");
                    string edit = Console.ReadLine() ?? string.Empty;

                    //Find the name in the file and replace it with the new name
                    string[] linesEdit = File.ReadAllLines("kontakter.txt");

                    switch (edit)
                    {
                        case "1":
                            //Get the new name from the user
                            Console.WriteLine("Indtast det nye navn: ");
                            string newName = Console.ReadLine() ?? string.Empty;
                            //Replace the old name with the new name
                            for (int i = 0; i < linesEdit.Length; i++)
                            {
                                if (linesEdit[i].Contains(search))
                                {
                                    string[] parts = linesEdit[i].Split(';');
                                    parts[0] = newName;
                                    linesEdit[i] = string.Join(";", parts);
                                }
                            }
                            File.WriteAllLines("kontakter.txt", linesEdit);
                            Console.WriteLine("Navn redigeret");
                            System.Threading.Thread.Sleep(3000);
                            Console.Clear();
                            break;
                        case "2":
                            //Get the new phone number from the user
                            Console.WriteLine("Indtast det nye telefonnummer: ");
                            string newPhoneNumber = Console.ReadLine() ?? string.Empty;
                            //Replace the old phone number with the new phone number
                            for (int i = 0; i < linesEdit.Length; i++)
                            {
                                if (linesEdit[i].Contains(search))
                                {
                                    string[] parts = linesEdit[i].Split(';');
                                    parts[1] = newPhoneNumber;
                                    linesEdit[i] = string.Join(";", parts);
                                }
                            }
                            File.WriteAllLines("kontakter.txt", linesEdit);
                            Console.WriteLine("Telefonnummer redigeret");
                            System.Threading.Thread.Sleep(3000);
                            Console.Clear();
                            break;
                        case "3":
                            //Get the new email from the user
                            Console.WriteLine("Indtast den nye email: ");
                            string newEmail = Console.ReadLine() ?? string.Empty;
                            //Replace the old email with the new email
                            for (int i = 0; i < linesEdit.Length; i++)
                            {
                                if (linesEdit[i].Contains(search))
                                {
                                    string[] parts = linesEdit[i].Split(';');
                                    parts[2] = newEmail;
                                    linesEdit[i] = string.Join(";", parts);
                                }
                            }
                            File.WriteAllLines("kontakter.txt", linesEdit);
                            Console.WriteLine("Email redigeret");
                            System.Threading.Thread.Sleep(3000);
                            Console.Clear();
                            break;
                    }
                }
            }
        }
    }

    //MARK: Delete Contact Method
    static void DeleteContact()
    {
        bool deleteMore = true;
        while (deleteMore)
        {
            //Get the user input for the name to delete
            Console.WriteLine("Find en bruger du ønsker at slette: ");
            string search = Console.ReadLine() ?? string.Empty;

            //Store the user to delete before the user has typed anything, so it can be used easier later 
            string userToDelete = string.Empty;

            //Read all lines from the file
            string[] lines = File.ReadAllLines("kontakter.txt");
            foreach (string line in lines)
            {
                if (line.Contains(search))
                {
                    //Store the user in the variable from earlier
                    userToDelete = line;
                    Console.WriteLine(userToDelete);
                    break;
                }
            }

            //Validation
            Console.WriteLine("Vil du slette denne bruger? (y/n)");

            //Get the user input for the answer
            string answer = Console.ReadLine() ?? string.Empty;
            if (answer.ToLower() == "y")
            {

                //Update the kontakter.txt file by removing the user to delete
                var updatedLines = lines.Where(line => line != userToDelete).ToArray();
                File.WriteAllLines("kontakter.txt", updatedLines);

                //Display a message and wait for user input to go back to the menu
                Console.WriteLine("Bruger slettet");
                Console.WriteLine("Ønsker du at slette en ny bruger? (y/n)");
                string answer2 = Console.ReadLine() ?? string.Empty;
                if (answer2.ToLower() != "y")
                {
                    deleteMore = false;
                }
                Console.Clear();
            }
        }
    }

    //MARK: Sort Contacts Method
    static void SortContacts()
    {
        // Start by getting all contacts from the txt file
        string[] lines = File.ReadAllLines("kontakter.txt");

        // Sort the contacts alphabetically by name
        var sortedLines = lines.OrderBy(line => line.Split(';')[0]).ToArray();
        
        //Display them
        foreach (string line in sortedLines)
        {
            Console.WriteLine(line);
        }

        //Display a message and wait for user input to go back to the menu
        Console.WriteLine("Kontakter sorteret");
        Console.WriteLine("Tryk på en tast for at gå tilbage til menuen");
        Console.ReadKey();
        Console.Clear();
        return;
    }


}
