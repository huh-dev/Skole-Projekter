namespace AnimalSounds
{
    public class AnimalFactory
    {
        public static Animal CreateAnimal(Animals animal, string name)
        {
            switch (animal)
            {
                case Animals.Dog:
                    return new Dog(name);
                case Animals.Cat:
                    return new Cat(name);
                case Animals.Sheep:
                    return new Sheep(name);
                default:
                    return null;
            }
        }

        public static Animal CreateAnimal(Animals animal)
        {
            switch (animal)
            {
                case Animals.Dog:
                    return new Dog("Luna");
                case Animals.Cat:
                    return new Cat("Bella");
                case Animals.Sheep:
                    return new Sheep("Sofia");
                default:
                    return null;
            }
        }
    }
}
