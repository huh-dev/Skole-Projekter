namespace FileHandler
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException() 
            : base("Name cannot be empty or whitespace.")
        {
        }
    }
}