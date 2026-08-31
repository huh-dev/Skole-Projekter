namespace Lesson06;

public class PrintKvittering : IKvitteringsAfsender
{
    public void SendKvittering(string tekst)
    {
        Console.WriteLine($"[Print] {tekst}");
    }
}
