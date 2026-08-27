namespace Zoo
{
    internal class Visitor
    {
        public string name {get; set;}
        public int age {get; set;}

        public Visitor(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public void VisitEnclosure(Enclosure enclosure)
        {
            Console.WriteLine($"{name} is visiting {enclosure.name}");
        }

        public void FeedAnimal(Animal animal)
        {
            Console.WriteLine($"{name} is feeding {animal.name}");
        }
        
    }
}
