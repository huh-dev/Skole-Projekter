namespace Lesson06;

public interface IKvitteringsAfsender
{
    public string SendKvittering(Udlejning udlejning, Kunde kunde)
    {
        return $"Kvittering sendt for udlejning {udlejning.Startdato} - {udlejning.Slutdato} til {kunde.Navn}";
    }
}
