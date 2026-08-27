namespace OOP
{
    internal class Player : ActorBase
    {
        //Init player stats
        internal void InitPlayerStats(int health, int damage, int name)
        {
            //Set name
            Console.WriteLine("Hvad er dit navn nørd?");
            Name = Console.ReadLine();

            //Set race
            Console.WriteLine("Hvad er din race nørd?");
            Race = Console.ReadLine();

            Health = health;
            Damage = damage;
        }
    }
}