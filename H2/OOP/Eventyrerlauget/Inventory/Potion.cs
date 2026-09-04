using Eventyrerlauget.Interfaces;
namespace Eventyrerlauget.Inventory;

public class Potion : Item
{

    public void Drink(Character target, IDiceRoller diceRoller)
    {
        target.Heal(diceRoller.RollDice(10));
    }

}