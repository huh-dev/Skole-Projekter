namespace FileHandler
{
    public class InvalidAgeException : Exception
    {
        public InvalidAgeException() : base("Age must be between 18 and 50")
        {
        }
    }
}
