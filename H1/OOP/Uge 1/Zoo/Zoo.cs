namespace Zoo
{
    internal class Zoo
    {
        public string name {get; set;}
        public List<Enclosure> enclosures {get; set;}

        public Zoo(string name)
        {
            this.name = name;
            this.enclosures = new List<Enclosure>();
        }

        public void AddEnclosure(Enclosure enclosure)
        {
            enclosures.Add(enclosure);
        }

        public void ListAllAnimals()
        {
            foreach (Enclosure enclosure in enclosures)
            {
                foreach (Animal animal in enclosure.animals)
                {
                    Console.WriteLine($"{animal.name} - {animal.age.ToString("yyyy-MM-dd")} - {animal.species} ");
                }

            }
        }

        public void DailyRoutine()
        {
            Console.WriteLine("Animals are being fed");
            foreach (Enclosure enclosure in enclosures)
            {
                foreach (Animal animal in enclosure.animals)
                {
                    animal.Eat();
                    animal.Sleep();
                }
            }
            Console.WriteLine("Enclosures are being cleaned");
            foreach (Enclosure enclosure in enclosures)
            {
                enclosure.Clean();
            }
        }
        
    }
}
