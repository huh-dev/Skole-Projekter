namespace Zoo
{
    internal abstract class Animal
    {
        public string name {get; set;}
        public string species {get; set;}
        public DateTime age {get; set;}  
        public string sound {get; set;}

        public Animal(string name, string species, DateTime age, string sound)
        {
            this.name = name;
            this.species = species;
            this.age = age;
            this.sound = sound;
        }

        public abstract void MakeSound();
        public abstract void Eat();
        public abstract void Sleep();


        
        
    }
}
