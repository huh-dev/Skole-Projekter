namespace OOP_Opg_1
{
    internal class Car
    {

        public string model {get; set;}
        public string brand {get; set;}
        public int year {get; set;}
        public int speed {get; set;}
        public double fuel {get; set;} = 100;

        public void InitVehicle(string model, string brand, int year, int speed)
        {
            this.model = model;
            this.brand = brand;
            this.year = year;
            this.speed = speed;
        }
        public virtual void Drive(int speed)
        {

            if (fuel > 0)
            {
                this.speed = speed;
                fuel -= 0.5;
                Console.WriteLine($"Driving at {speed} km/h, the fuel is now {fuel} liters");
            }
            else
            {
                Console.WriteLine("Not enough fuel");
            }
        }

        public void Stop()
        {
            this.speed = 0;
            Console.WriteLine("Stopping the vehicle and setting speed to 0");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Model: {model}, Brand: {brand}, Year: {year}, Fuel: {fuel}");
        }

        public virtual void Refuel(double fuel)
        {
            Console.WriteLine($"The fuel was {this.fuel} liters, and is now {fuel} liters");
            this.fuel = fuel;   
        }

    }
}
