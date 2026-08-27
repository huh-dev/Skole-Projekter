namespace BattleshipGP
{
    internal class Game
    {
        enum Board { Ships, Hits }

        /*MARK: Updates

        - Changed the static ints to constants
        - Added a ShowBoard method to show the board better visually
        - Changed ShowShips and ShowHits to use the ShowBoard method
        - Added a Check to PlaceShip to check if the ship already exists on the coordinate
        - Added a Check for StartGame to check if the game is over
        - Added a Check GameOver Method to check if the game is over
        - Added a Check for Shoot to check if the shot hit a ship
        - Changed the Shoot method to use the new ShowBoard method - and make it dynamic with the new code
        */


        private const int xc = 4;
        private const int yc = 3; 
        private const int ships = 3;
        private const int players = 2;

        private static readonly int[,,,] gameArray = new int[players, 2, xc, yc];

        public static void SetupGame()
        {
            //Place ships for each player for each ship
            for (int i = 0; i < players; i++)
            {
                Console.WriteLine($"Player {i + 1}, place your ships.");
                for (int j = 0; j < ships; j++) { PlaceShip(i); }
            }

            StartGame();
        }

        private static void ShowBoard(int player, Board boardType)
        {

            //Header (Numbers)
            Console.Write("  ");
            for (int x = 0; x < xc; x++)
            {
                Console.Write(x + " ");
            }

            //Filler
            Console.WriteLine();

            //Body (Ships)
            for (int y = 0; y < yc; y++)
            {
                Console.Write(y + " ");
                for (int x = 0; x < xc; x++)
                {
                    char symbol = ' ';
                    if (gameArray[player, (int)boardType, x, y] == 1)
                    {
                        symbol = boardType == Board.Ships ? 'S' : 'X';
                    }
                    Console.Write(symbol + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void ShowShips(int player)
        {
            Console.WriteLine($"\nPlayer {player + 1}'s ships:");
            ShowBoard(player, Board.Ships);
        }

        private static void ShowHits(int player)
        {
            Console.WriteLine($"\nPlayer {player + 1}'s shots:");
            ShowBoard(player, Board.Hits);
        }

        private static void PlaceShip(int player)
        {
            Console.Clear();
            ShowShips(player);
            Console.WriteLine("Place a ship");
            int x, y;
            
            do { Console.WriteLine("Put X"); }
            while (!int.TryParse(Console.ReadLine(), out x) || x < 0 || x >= xc);
            do { Console.WriteLine("Put Y"); }
            while (!int.TryParse(Console.ReadLine(), out y) || y < 0 || y >= yc);
            
            if (gameArray[player, (int)Board.Ships, x, y] == 1)
            {
                Console.WriteLine("Ship already exists at this coordinate. Try again.");
                PlaceShip(player);
                return;
            }
            
            gameArray[player, (int)Board.Ships, x, y] = 1;
        }

        private static void StartGame()
        {
            bool gameOver = false;
            while (!gameOver)
            {
                for (int i = 0; i < players; i++)
                {
                    Shoot(i);
                    if (CheckGameOver())
                    {
                        gameOver = true;
                        break;
                    }
                }
            }
        }

        private static bool CheckGameOver()
        {
            for (int p = 0; p < players; p++)
            {
                bool hasShips = false;
                for (int x = 0; x < xc; x++)
                {
                    for (int y = 0; y < yc; y++)
                    {
                        if (gameArray[p, (int)Board.Ships, x, y] == 1)
                        {
                            hasShips = true;
                            break;
                        }
                    }
                    if (hasShips) break;
                }
                if (!hasShips)
                {
                    Console.WriteLine($"Game Over! Player {p + 1} has lost all ships!");
                    return true;
                }
            }
            return false;
        }

        private static void Shoot(int player)
        {
            Console.Clear();
            Console.WriteLine($"Player {player + 1}'s turn");
            ShowHits(player);
            int x, y;
            
            do { Console.WriteLine("Put X"); }
            while (!int.TryParse(Console.ReadLine(), out x) || x < 0 || x >= xc);
            do { Console.WriteLine("Put Y"); }
            while (!int.TryParse(Console.ReadLine(), out y) || y < 0 || y >= yc);

            bool hitAny = false;
            for (int i = 0; i < players; i++)
            {
                if (player != i)
                {
                    if (gameArray[i, (int)Board.Ships, x, y] == 1)
                    {
                        Console.WriteLine("Hit!");
                        gameArray[i, (int)Board.Ships, x, y] = 0;
                        hitAny = true;
                    }
                    gameArray[player, (int)Board.Hits, x, y] = 1;
                }
            }
            
            if (!hitAny)
            {
                Console.WriteLine("Miss!");
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}