global using VehicleInspection;

namespace VehicleInspection
{
    class Program
    {
        static void Main(string[] args)
        {
            string carBrand = "Toyota";
            string carModel = "Corolla";
            DateTime carProductionDate = new DateTime(2010, 1, 1);
            DateTime carInspectionDate = new DateTime(2020, 1, 1);

            Car car = new Car(carBrand, carModel, carProductionDate, carInspectionDate);

            string truckBrand = "Mercedes";
            string truckModel = "Actros";
            DateTime truckProductionDate = new DateTime(2015, 1, 1);
            DateTime truckInspectionDate = new DateTime(2025, 1, 1);

            Truck truck = new Truck(truckBrand, truckModel, truckProductionDate, truckInspectionDate);

            Console.WriteLine(car.InspectionStatus());
            Console.WriteLine(truck.InspectionStatus());

            Console.WriteLine(car.DisplayInfo());
            Console.WriteLine(truck.DisplayInfo());
        }

        private void test()
        {
            
        }

    }
}
