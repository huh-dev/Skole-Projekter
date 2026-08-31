namespace Lesson06;

public class EmailKvittering : IKvitteringsAfsender
{
    public void SendKvittering(string tekst)
    {
        Console.WriteLine($"[Email] {tekst}");
    }
}
