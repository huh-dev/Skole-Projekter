using Eventyrerlauget.Interfaces;
using Eventyrerlauget.Inventory;
namespace Eventyrerlauget.CharacterTypes;

public class Wizard : Character, ISpellCaster
{
    public int maxMana { get; private set; }
    public int currentMana { get; private set; }

    public Wizard(string name, int level, int maxHp, Weapon? weapon = null, Armor? armor = null) 
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
        
        int attackRole = diceRoller.RollDice(20);

        if (attackRole >= target.ArmorClass())
        {
            target.TakeDamage(diceRoller.RollDice(10));
        }
    }
}