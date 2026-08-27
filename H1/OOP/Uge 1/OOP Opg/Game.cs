namespace OOP
{

    internal class Game
    {


        public static void SetupGame()
        {
            //Random Player
            Random random = new Random();

            //Player
            Player hero = new Player();

            //Enemy
            Enemy enemy = new Enemy();

            //Init enemy
            enemy.InitEnemyStats(random.Next(100, 200), random.Next(10, 20), random.Next(0, 9), random.Next(0, 9));

            //Init player
            hero.InitPlayerStats(random.Next(100, 200), random.Next(10, 20), random.Next(0, 9));

            //Arena
            Arena(hero, enemy);

        }


        private static void Arena(Player hero, Enemy enemy)
        {
            //Arena
            Console.WriteLine("Arena");
            
            Console.WriteLine($"Hero: {hero.Name} with {hero.Health} health and {hero.Damage} damage and {hero.Race} race");
            Console.WriteLine($"Enemy: {enemy.Name} with {enemy.Health} health and {enemy.Damage} damage and {enemy.Race} race");
            
            
        }
    }
}