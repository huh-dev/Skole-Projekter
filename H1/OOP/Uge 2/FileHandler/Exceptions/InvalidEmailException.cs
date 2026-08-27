namespace FileHandler
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}