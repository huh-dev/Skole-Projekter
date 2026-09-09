namespace Heltevagten.Exceptions;

public class NoSuitableHeroFoundException : Exception
{
    public NoSuitableHeroFoundException(string message) : base(message)
    {
    }
}
