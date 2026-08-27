namespace CRUD
{
    class Program
    {
        static void Main(string[] args)
        {

            bool RunMainLoop = true;

            do
            {
                StartUpText();
                string userInput = Console.ReadLine();
                
                switch (userInput)
                {
                    case "0":
                        RunMainLoop = false;
                        break;
                    case "1":
                        Create.CreateVehicle();
                        break;
                    case "2":
                        // Read.ViewAllVehicles();
                        break;
                    case "3":
                        // Search.SearchVehicle();
                        break;
                    case "4":
                        // Update.UpdateData();
                        break;
                    case "5":
                        // Delete.DeleteVehicle();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (RunMainLoop);
        }


        private static void StartUpText()
        {
            Console.WriteLine("CRUD Application");
            Console.WriteLine("Welcome Admin! To our vehicle database!");
            Console.WriteLine("What would you like to do? \n\n0) Exit \n1) Create Vehicle \n2) View All Vehicles \n3) Search for a Vehicle \n4) Update a Vehicle \n5) Delete a Vehicle");
        }
    }
}


