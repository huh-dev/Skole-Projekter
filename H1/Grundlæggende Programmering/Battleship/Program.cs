namespace BattleshipGP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Removed "Hello, World!" and added the SetupGame method to not call the method directly but instead call it from the Game class
            Game.SetupGame();
        }
    }
}
