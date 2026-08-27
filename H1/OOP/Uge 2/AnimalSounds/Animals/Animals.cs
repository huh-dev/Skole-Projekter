using NAudio.Wave;

namespace AnimalSounds
{
    public abstract class Animal 
    {
        public string name { get; set; }

        public abstract void MakeSound();
        public abstract void StopSound();

        public Animal(string name)
        {
            this.name = name;
        }
    }
}
