namespace Eventyrerlauget;

public class Party
{
    public List<Character> Members { get; private set; }

    public Party(List<Character> members)
    {
        Members = members;
    }
}