namespace Zoo; 

internal class Enclosure
{
    public string name { get; set; }
    public int size { get; set; }
    public List<Animal> animals { get; private set; } = new List<Animal>();

    public Enclosure(string name, int size, List<Animal> animals)
    {
        this.name = name;
        this.size = size;
        this.animals = animals;
    }

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
    }

    public void RemoveAnimal(Animal animal)
    {
        if (animals.Contains(animal))
        {
            animals.Remove(animal);
        }
        else
        {
            Console.WriteLine($"{animal.name} is not in {name} enclosure");
        }
    }

    public void ListAnimals()
    {
        foreach (Animal animal in animals)
        {
            Console.WriteLine(animal.name);
        }
    }

    public void Clean()
    {
        Console.WriteLine($"Cleaning {name} enclosure");
    }
}

