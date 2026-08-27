namespace GitOvelse;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Lav en lille hilsen");

        string input = Console.ReadLine() ?? string.Empty;
        Console.WriteLine($"Hej {input}");
    }
}
