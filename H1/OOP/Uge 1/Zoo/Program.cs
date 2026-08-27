namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Zoo
            Zoo zoo = new Zoo("Zoo");

            //Create Animals
            Lion lion = new Lion("Simba", "Lion", new DateTime(2010, 7, 3), "Roar");
            Elephant elephant = new Elephant("Dumbo", "Elephant", new DateTime(2005, 4, 15), "Trumpet");
            Giraffe giraffe = new Giraffe("Gigi", "Giraffe", new DateTime(2015, 2, 20), "Moo");
            Penguin penguin = new Penguin("Pingu", "Penguin", new DateTime(2018, 3, 10), "Quack");

            //Create Enclosures
            zoo.AddEnclosure(new Enclosure("Lion", 50, new List<Animal> { lion }));
            zoo.AddEnclosure(new Enclosure("Elephant", 100, new List<Animal> { elephant }));
            zoo.AddEnclosure(new Enclosure("Giraffe", 75, new List<Animal> { giraffe }));
            zoo.AddEnclosure(new Enclosure("Penguin", 25, new List<Animal> { penguin }));

            //Create ZooKeepers
            ZooKeeper zooKeeper = new ZooKeeper("John", 30);
            zooKeeper.AssignEnclosure(zoo.enclosures[0]);
            zooKeeper.AssignEnclosure(zoo.enclosures[3]);

            ZooKeeper zooKeeper2 = new ZooKeeper("Jane", 25);
            zooKeeper2.AssignEnclosure(zoo.enclosures[1]);
            zooKeeper2.AssignEnclosure(zoo.enclosures[2]);

            //Create Visitors
            Visitor visitor = new Visitor("John", 20);
            visitor.VisitEnclosure(zoo.enclosures[0]);
            visitor.VisitEnclosure(zoo.enclosures[3]);

            //Daily Routine
            zoo.DailyRoutine();

            //List All Animals
            zoo.ListAllAnimals();
            
        }
    }
}
