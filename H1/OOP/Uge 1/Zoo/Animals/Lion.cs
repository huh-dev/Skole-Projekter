namespace Zoo
{
    internal class Lion : Animal
    {

        public Lion(string name, string species, DateTime age, string sound) : base(name, species, age, sound)
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

        public void Hunt(string prey)
        {
            Console.WriteLine($"{name} is hunting {prey}");
        }
    }
}
