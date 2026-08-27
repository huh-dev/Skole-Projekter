namespace OOP_Opg_1
{
    class Program
    {
        static void Main(string[] args)
        {   

            // List<Car> cars = new List<Car>();
            Car car = new Car();
            car.InitVehicle("Ford", "Mustang", 2024, 100);
            car.Drive(100);
            car.Refuel(100);
            car.Stop();
            // car.ShowInfo();

            ElectricCar electricCar = new ElectricCar();
            electricCar.InitElectricCar("Tesla", "Model S", 2024, 100);
            electricCar.Drive(100);
            electricCar.Refuel(100);
            electricCar.Stop();
            // electricCar.ShowInfo();

            Garage garage = new Garage();
            garage.AddCar(car);
            garage.AddCar(electricCar);
            garage.ShowCars();
        }
    }
}

