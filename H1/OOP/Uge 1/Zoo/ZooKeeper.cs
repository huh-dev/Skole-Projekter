namespace Zoo
{
    internal class ZooKeeper
    {
        public string name { get; set; }
        public int age { get; set; }
        public List<Enclosure> assignedEnclosures { get; set; }

        public ZooKeeper(string name, int age)
        {
            this.name = name;
            this.age = age;
            this.assignedEnclosures = new List<Enclosure>();
        }

        public void AssignEnclosure(Enclosure enclosure)
        {
            assignedEnclosures.Add(enclosure);
            Console.WriteLine($"{name} has been assigned to {enclosure.name}");
        }
    
        public void FeedAnimals(Animal animal)
        {
            Console.WriteLine($"{name} is feeding {animal.name}");
        }

        public void CleanEnclosure(Enclosure enclosure)
        {
            Console.WriteLine($"{name} is cleaning {enclosure.name}");
        }                
    }
}
