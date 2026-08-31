namespace Lesson06;

public class SmsKvittering : IKvitteringsAfsender
{
    public void SendKvittering(string tekst)
    {
        Console.WriteLine($"[SMS] {tekst}");
    }
}
