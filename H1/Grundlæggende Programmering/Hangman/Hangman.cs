namespace Hangman
{
    internal class Hangman
    {
        private string word = "";

        private string[,] players = new string[2,2];

        private int totalGuesses = 6;
        private int wrongGuesses = 0;

        private string revealedWord = "";


        //MARK: Setup
        public static void Setup()
        {
            Hangman game = new Hangman();
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("Gamemaster enter a word:");
            string? input = Console.ReadLine();
            game.word = input ?? "";
        
            Console.Clear();

            Console.WriteLine("Okay! the word is in. Please set the amount of players");
            int playersCount = Convert.ToInt32(Console.ReadLine());

            game.players = new string[playersCount, 2];

            for (int i = 0; i < playersCount; i++)
            {
                Console.WriteLine($"Player {i + 1} enter your name:");
                string? playerName = Console.ReadLine();
                game.players[i, 0] = playerName ?? ""; 
                game.players[i, 1] = "";  
            }

            Console.Clear();

            Console.WriteLine("Okay! Let's guess");
            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("This is how the current hangman looks like with 0 guesses");
            System.Threading.Thread.Sleep(2000);
            VisualPresentation(0);
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("The word is: " + game.word.Length + " letters long");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();

            game.revealedWord = new string('_', game.word.Length);

            int currentPlayer = 0;
            while (game.wrongGuesses < game.totalGuesses)  
            {
                game.Guess(currentPlayer);
                currentPlayer = (currentPlayer + 1) % playersCount;
            }


            Console.WriteLine("Game Over! The word was: " + game.word);
        }

        //MARK: VisualPresentation
        private static void VisualPresentation(int guesses)
        {
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine(guesses >= 1 ? "  O   |" : "      |");
                Console.WriteLine(guesses >= 4 ? " /|\\  |" : guesses >= 3 ? " /|   |" : guesses >= 2 ? "  |   |" : "      |");
                Console.WriteLine(guesses >= 6 ? " / \\  |" : guesses >= 5 ? " /    |" : "      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
        }

        //MARK: Guess
        private void Guess(int player)
        {
            string playerName = GetPlayerName(player);
            Console.WriteLine($"{playerName} enter a guess:");
            string? guess = Console.ReadLine();
            if (string.IsNullOrEmpty(guess)) return;

            //If the guess is a single letter, but is not in the word
            if (guess.Length == 1 && !CheckLetter(guess))
            {
                wrongGuesses++;
                Console.WriteLine($"{playerName} guessed {guess} - Incorrect guess - the hangman is now looking like this:");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                VisualPresentation(wrongGuesses);
                System.Threading.Thread.Sleep(2000);

                players[player, 1] += " " + guess;
                Console.Clear();
            }

            //If the guess is a single letter and is in the word
            else if (guess.Length == 1 && CheckLetter(guess))
            {
                Console.WriteLine("Correct guess!");
                Console.WriteLine("The word is now looking like this:");
                RevealWord(word, guess[0], player);
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
            }

            //If the guess is a word
            if (guess.Length > 1 && guess != word)
            {
                Console.WriteLine("Incorrect guess - the hangman is now looking like this:");
                wrongGuesses++;
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                VisualPresentation(wrongGuesses);
                System.Threading.Thread.Sleep(2000);
            } 

            //If the guess is a word and is correct
            else if (guess.Length > 1 && guess == word)
            {
                GameOver(player);
            }
        }


        //MARK: Get Player Name from Index
        private string GetPlayerName(int index)
        {
            return players[index, 0];
        }


        //MARK: Check if the letter is in the word
        private bool CheckLetter(string letter)
        {
            return word.Contains(letter);
        }

        //MARK: Reveal Word
        private void RevealWord(string word, char guess, int player)
        {
            string newRevealedWord = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == guess)
                {
                    newRevealedWord += guess;
                }
                else
                {
                    newRevealedWord += revealedWord[i]; 
                }
            }
            revealedWord = newRevealedWord;
            Console.WriteLine(revealedWord);

            //If the word is fully revealed, the game is over
            if (revealedWord == word)
            {
                GameOver(player);
            }
        }



        //MARK: Game Over
        private void GameOver(int player)
        {
            
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            string playerName = GetPlayerName(player);
            Console.WriteLine($"{playerName} wins!");  
            Console.WriteLine("Game Over! The word was: " + word);
            Console.WriteLine($"There were {wrongGuesses} wrong guesses");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("The hangman was looking like this:");
            VisualPresentation(wrongGuesses);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}

