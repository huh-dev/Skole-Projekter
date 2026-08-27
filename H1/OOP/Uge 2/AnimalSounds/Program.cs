global using AnimalSounds;

namespace AnimalSounds
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal dog = AnimalFactory.CreateAnimal(Animals.Dog, "Rex");
            // Animal cat = AnimalFactory.CreateAnimal(Animals.Cat, "Mia");
            // Animal sheep = AnimalFactory.CreateAnimal(Animals.Sheep, "Dolly");

            dog.MakeSound();
            // cat.MakeSound();
            // sheep.MakeSound();

            // Console.WriteLine("\nDyr med pigenavne:");
            // Animal dog2 = AnimalFactory.CreateAnimal(Animals.Dog);
            // Animal cat2 = AnimalFactory.CreateAnimal(Animals.Cat);
            // Animal sheep2 = AnimalFactory.CreateAnimal(Animals.Sheep);

            // Console.Write($"{dog2.name} siger: ");
            // dog2.MakeSound();
            // Console.Write($"{cat2.name} siger: ");
            // cat2.MakeSound();
            // Console.Write($"{sheep2.name} siger: ");
            // sheep2.MakeSound();

            Console.WriteLine("Press any key to stop the sound...");
            Console.ReadKey();
        }
    }
}
