namespace DungeonEscape
{
    using System.Text.RegularExpressions;

    internal class DungeonEscapeGame
    {
        

        //Simple char multidimensional array (started with int, but wanted to use char to easier check for different objects instead of using integers)
        private static char[,] Map = new char[10,10];
        private static string[,] Game = new string[10,10];

        private static bool haveKey = false;

        private static int[,] positions = new int[2,2]; 

        //MARK: Debug Mode
        //Simple bool to enable to see traps, key and exit to easier test the game
        private static bool showDebug = false;

        //MARK: Setup Game
        public static void SetupGame()
        {
            Console.Clear();
            Console.WriteLine("Velkommen til Dungeon Escape!");
            
            // Reset all game state
            Map = new char[10,10];
            Game = new string[10,10];
            haveKey = false;
            positions = new int[2,2];
            
            GenerateMap();
            
            // Simple game loop
            while (true)
            {
                Console.Clear();
                DisplayMap();
                MovePlayer();
            }
        }

        //MARK: Generate Map
        private static void GenerateMap()
        {
            //Loading Screen for map generation
            Console.WriteLine("Generere map...");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();

            //Generation of the map frame - for the game (using # as boundries) - Also using a nested for loops to the 2d visualisation of the map
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    //Checking if the current position is on the edge of the map - if so, set the value to 'W' (which is the wall character)
                    if (i == 0 || i == Map.GetLength(0) - 1 || j == 0 || j == Map.GetLength(1) - 1)
                    {
                        Map[i,j] = 'W';
                    }
                }
            }

            //Call the method GenerateMaze to generate the maze pattern
            GenerateMaze();
        }

        //MARK: Generate Maze
        private static void GenerateMaze()
        {
            //Generates the maze pattern (this is hardcoded to support the 10x10 map defined in the map and game array)
            char[,] maze = {
                {' ',' ',' ',' ','W','W',' ',' '},
                {'W',' ','W',' ',' ',' ',' ','W'},
                {' ',' ',' ',' ','W',' ',' ',' '},
                {' ','W',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','W','W',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' '},
                {'W','W',' ',' ',' ',' ',' ',' '},
                {'W','W',' ',' ',' ',' ',' ',' '}
            };

            //Nested for loops to iterate through the maze pattern and apply it to the map
            for (int row = 1; row < 9; row++)
            {
                for (int col = 1; col < 9; col++) 
                {
                    //Applying the maze pattern to the map
                    Map[row,col] = maze[row-1,col-1];
                }
            }

            //Apply the start position for the player
            Game[1,1] = "P";

            //Call the method PlaceStandardItems to place the items key and exit
            PlaceStandardItems();
        }

        //MARK: Place Standard Items
        private static void PlaceStandardItems()
        {

            // Place key at (3,5) or fallback to (4,4)
            positions[0,0] = Map[3,5] == ' ' ? 3 : 4;
            positions[0,1] = Map[3,5] == ' ' ? 5 : 4;

            // Place the key in the map
            Game[positions[0,0], positions[0,1]] = "K";

            // Place the exit at the bottom right of the map
            positions[1,0] = Map.GetLength(0) - 2;
            positions[1,1] = Map.GetLength(1) - 2;

            // Check if the exit is on a wall, if so, move it to the left (best solution, had a bit of trouble with this)
            while (Map[positions[1,0], positions[1,1]] != ' ' && positions[1,1] > 1)
            {
                positions[1,1]--;
            }

            // Place the exit in the map
            Game[positions[1,0], positions[1,1]] = "E";


            // Call the method PlaceTraps to place the traps that are in the map - these traps will make you lose the game if you fall into them
            PlaceTraps();
        }

        //MARK: Place Traps
        private static void PlaceTraps()
        {   
            // Predefined trap positions that ensure a path exists from start -> key -> exit
            // Traps are placed to create challenge while maintaining at least one valid path
            int[] trapRows = { 2, 4, 6, 7, 3, 5, 2, 6, 8, 4 };
            int[] trapCols = { 6, 2, 4, 6, 3, 6, 3, 7, 3, 7 };

            // In a for loop we iterate through the trapRows and trapCols arrays and place the traps
            for (int i = 0; i < trapRows.Length; i++)
            {
                int row = trapRows[i];
                int col = trapCols[i];

                Map[row, col] = 'T';  
            }
        }

        //MARK: Display Map
        private static void DisplayMap()
        {
            // We use the map in a nested for loop to display the map and its contents - so things like the player and where the player has been on the map, is something we want and do display
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    // If the game array contains the player character, we display it
                    if (Game[i,j] == "P")
                    {
                        Console.Write("P");
                    }

                    // If the game array contains the X character (these are the places the player has been)
                    else if (Game[i,j] == "X")
                    {
                        Console.Write("x");
                    }

                    // If the game array contains the discovered wall character
                    else if (Game[i,j] == "#")
                    {
                        Console.Write("#");
                    }

                    // We create the outer walls of the map
                    else if (Map[i,j] == 'W' && (i == 0 || i == Map.GetLength(0) - 1 || j == 0 || j == Map.GetLength(1) - 1))
                    {
                        Console.Write("#");
                    }

                    // Debug mode: show traps, keys, and exits
                    else if (showDebug)
                    {
                        if (Map[i,j] == 'T')
                        {
                            Console.Write("T");
                        }
                        else if (Game[i,j] == "K")
                        {
                            Console.Write("K");
                        }
                        else if (Game[i,j] == "E")
                        {
                            Console.Write("E");
                        }
                        else if (Map[i,j] == 'W')
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }

                    // If debug mode is off or no special character, display a space
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            
            // We use again a nested for loop to find the players current position in the game array - this is used to check for traps in the players range, the range here is 2 spaces in each direction
            int playerRow = 0, playerCol = 0;
            for (int i = 0; i < Game.GetLength(0); i++)
            {
                for (int j = 0; j < Game.GetLength(1); j++)
                {
                    // If the player is found in the game array, we set the playerRow and playerCol to the current position and break the loop
                    if (Game[i,j] == "P")
                    {
                        playerRow = i;
                        playerCol = j;
                        break;
                    }
                }
            }
            
            // We call a helper method to check if there are traps in the players range - this is used to display a message to the player if there are traps in the players range
            if (CheckNearbyTraps(playerRow, playerCol))
            {
                Console.WriteLine("\nPas på! Der er en fælde i nærheden!");
            }
            
            // We check if the player has found the key, and display that message
            if (haveKey)
            {
                Console.WriteLine("Du har fundet nøglen!");
            }
        }

        //MARK: Check Nearby Traps
        private static bool CheckNearbyTraps(int playerRow, int playerCol)
        {
            // We use a nested for loop to check if there are any traps in a range of 2 spaces in all directions
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    // We set the checkRow and checkCol to the current position plus the current iteration of the loop
                    int checkRow = playerRow + i;
                    int checkCol = playerCol + j;
                    
                    // We check if the checkRow and checkCol are within the bounds of the map and if the Map[checkRow, checkCol] is equal to 'T' (which is the trap character) - if so, we return true
                    if (checkRow >= 0 && checkRow < Map.GetLength(0) && checkCol >= 0 && checkCol < Map.GetLength(1) && Map[checkRow, checkCol] == 'T')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //MARK: Move Player
        private static void MovePlayer()
        {
            // We start by defining the playerRow and playerCol
            int playerRow = 0, playerCol = 0;

            // Now we use the game array in a nested for loop to get the correct position and redefine playerRow- and col to the acutal position 
            for (int i = 0; i < Game.GetLength(0); i++)
            {
                for (int j = 0; j < Game.GetLength(1); j++)
                {
                    // If the player is found in the game array, we set the variables and break the loop
                    if (Game[i,j] == "P")
                    {
                        playerRow = i;
                        playerCol = j;
                        break;
                    }
                }
            }
            
            Console.WriteLine("Hvor vil du bevæge dig hen? (W, A, S, D)");

            // Get users input and convert it to uppercase
            string input = Console.ReadLine().ToUpper();
            
            // Predefined variables to store the updated position
            int newRow = playerRow;
            int newCol = playerCol;
            
            // Switch statement to handle the users input - and for each case we either remove or add a row or coloum to the redefined variables above
            switch (input)
            {
                case "W":
                    newRow--;
                    break;
                case "A":
                    newCol--;
                    break;
                case "S":
                    newRow++;
                    break;
                case "D":
                    newCol++;
                    break;
                default:
                    //Validation
                    Console.WriteLine("Ugyldigt input");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    break;
            }
            
            // We call the method CheckPositions to check positions, if we've it anything or other things alike - also we send some args to the method - to ensure that we have all the actual information
            CheckPositions(newRow, newCol, playerRow, playerCol);

        }

        //MARK: Check Positions
        private static void CheckPositions(int newRow, int newCol, int playerRow, int playerCol)
        {
            // Npw we check if the newRow and newCol are within the bounds of the game array - if there are not, we dont update the player position
            if (newRow >= 0 && newRow < Game.GetLength(0) && newCol >= 0 && newCol < Game.GetLength(1))  
            {
                // When a wall is found, mark it in the Game array
                if (Map[newRow, newCol] == 'W')
                {
                    Game[newRow, newCol] = "#";
                    Console.WriteLine("Du har fundet en væg, du kan ikke gå den her vej!");
                    System.Threading.Thread.Sleep(1000);
                    return;
                }
                
                //Now we check if the position contains a trap
                if (Map[newRow, newCol] == 'T') 
                {
                    Console.Clear();
                    Console.WriteLine("Åh nej! Du faldt i en fælde!");
                    Console.WriteLine("Ønsker du at spille igen? (Y/N)");
                    string input = Console.ReadLine().ToUpper();
                    if (input == "Y")
                    {
                        SetupGame(); 
                    }
                    else
                    {
                        Console.WriteLine("Tak for spillet!");
                        System.Threading.Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                    return;
                }
                
                // Now we check if the newRow and newCol are equal to "K" - which is the key character
                if (Game[newRow, newCol] == "K")
                {
                    haveKey = true;
                    Console.WriteLine("Du har fundet nøglen!");
                    System.Threading.Thread.Sleep(1000);
                }
                else if (Game[newRow, newCol] == "E")
                {
                    if (haveKey)
                    {
                        Game[playerRow, playerCol] = "X";
                        Game[newRow, newCol] = "P";
                        GameOver();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Du har brug for en nøgle, før du kan komme ud!");
                        System.Threading.Thread.Sleep(1000);
                        return;
                    }
                }
                
                // Only mark the previous position with X if we're not attempting to enter the exit without a key
                if (!(Game[newRow, newCol] == "E" && !haveKey))
                {
                    Game[playerRow, playerCol] = "X";
                    Game[newRow, newCol] = "P";
                }
            }
        }


        //MARK: Game Over
        private static void GameOver()
        {
            // The user escaped the dungeon and we then ask if they want to play again
            Console.Clear();
            Console.WriteLine("Tillykke! Du har overlevet og er ny ude af dungeonen!");
            Console.WriteLine("Ønsker du at spille igen? (Y/N)");
            string input = Console.ReadLine().ToUpper();
            if (input == "Y")
            {
                SetupGame();
            }
            else
            {
                Console.WriteLine("Tak for spillet!");
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }
    }
}