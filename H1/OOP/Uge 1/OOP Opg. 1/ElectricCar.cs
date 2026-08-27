namespace OOP_Opg_1
{
    internal sealed class ElectricCar : Car
    {
        private double battery {get; set;} = 100;

        public void InitElectricCar(string model, string brand, int year, double battery)
        {
            InitVehicle(model, brand, year, speed);
            this.battery = battery;
        }



        public override void Refuel(double procent)
        {
            this.battery = procent;
            Console.WriteLine($"Charging the vehicle - battery was {this.battery} kWh, and is now {procent} kWh");
        }

        public override void Drive(int speed)
        {
            if (battery > 0)
            {
                base.Drive(speed);
                battery -= 1;
                Console.WriteLine($"Driving at {speed} km/h, the battery is now {this.battery} kWh");
            }
            else
            {
                Console.WriteLine("Not enough battery");
            }
        }
        

    }
}
