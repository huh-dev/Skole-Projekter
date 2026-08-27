namespace Zoo
{
    internal class Penguin : Animal
    {

        public Penguin(string name, string species, DateTime age, string sound) : base(name, species, age, sound)
        {
            this.name = name;
            this.species = species;
            this.age = age;
            this.sound = sound;
        }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{name} is {sound}");
        }

        public override void Eat()
        {
            Console.WriteLine($"{name} is eating");
        }

        public override void Sleep()
        {
            Console.WriteLine($"{name} is sleeping");
        }
    }
}   
