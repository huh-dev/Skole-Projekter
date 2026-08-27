namespace CRUD
{
    internal class Create
    {
        public static void CreateVehicle()
        {
            string[] lines = Utils.GetDocumentContent();
            
            Console.WriteLine("Enter vehicle name: ");
            string vehicleName = Console.ReadLine();

            Console.WriteLine("Enter vehicle price: ");
            string vehiclePrice = Console.ReadLine();

            if (!Utils.IsIntValid(vehiclePrice))
            {
                Console.WriteLine("Invalid price input");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                CreateVehicle();
                return;
            }

            Console.WriteLine("Enter vehicle year: ");
            string vehicleYear = Console.ReadLine();

            if (!Utils.IsIntValid(vehicleYear))
            {
                Console.WriteLine("Invalid year input");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                CreateVehicle();
                return;
            }

            string newVehicle = $"{vehicleName},{vehiclePrice},{vehicleYear}";
            lines.Append(newVehicle);
            File.WriteAllLines("vehicles.txt", lines);  
        }
    }
}