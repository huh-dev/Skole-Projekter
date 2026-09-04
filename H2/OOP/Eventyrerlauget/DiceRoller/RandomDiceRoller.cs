using Eventyrerlauget.Interfaces;

namespace Eventyrerlauget.DiceRoller;

public class RandomDiceRoller : IDiceRoller
{
    public int RollDice(int sides)
    {
        return new Random().Next(1, sides + 1);
    }
}