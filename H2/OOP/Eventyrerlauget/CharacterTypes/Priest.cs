using Eventyrerlauget.Interfaces;
using Eventyrerlauget.Inventory;
namespace Eventyrerlauget.CharacterTypes;

public class Priest : Character, ISpellCaster
{
    public int maxMana { get; private set; }
    public int currentMana { get; private set; }

    public Priest(string name, int level, int maxHp, Weapon? weapon = null, Armor? armor = null) 
        : base(name, level, maxHp, weapon, armor) {
        maxMana = 100;
        currentMana = maxMana;
    }

    public void CastSpell(IDamageable target, IDiceRoller diceRoller)
    {

        if (currentMana < 15)
        {
            Console.WriteLine("Not enough mana to cast spell");
            return;
        }

        currentMana -= 15;

        target.Heal(diceRoller.RollDice(10));
    }
}