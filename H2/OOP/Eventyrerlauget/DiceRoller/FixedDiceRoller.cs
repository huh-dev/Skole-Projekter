using Eventyrerlauget.Interfaces;

namespace Eventyrerlauget.DiceRoller;

public class FixedDiceRoller : IDiceRoller
{
    public int RollDice(int sides)
    {
        return sides;
    }
}