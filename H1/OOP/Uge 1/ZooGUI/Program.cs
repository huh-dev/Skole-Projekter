using Zoo;
using Types;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        LuiEdge.Car car = new LuiEdge.Car("BMW", 4);
        Console.WriteLine(car.name);
        Console.WriteLine(car.wheels);
        car.Drive();


        Types.Code code = new Types.Code("type");
        Console.WriteLine(code.count);
    }
}


internal class LuiEdge
{
    public abstract class Vehicles() 
    {
        public int wheels {get; set;}
        public string name {get; set;}

        public abstract void Drive();

    }

    internal interface ICar {
    }

    internal class Car : Vehicles, ICar {

        public Car(string name, int wheels)
        {
            this.name = name;
            this.wheels = wheels;
        }

        public override void Drive()
        {
            Console.WriteLine("Driving " + name + " with " + wheels + " wheels");
        }

    }
}
