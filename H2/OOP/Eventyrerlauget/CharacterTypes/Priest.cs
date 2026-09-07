using Eventyrerlauget.Interfaces;
namespace Eventyrerlauget.CharacterTypes;

public class Priest : Character, ISpellCaster
{
    public int maxMana { get; private set; }
    public int currentMana { get; private set; }

    public Priest(string name, int level, int maxHp) : base(name, level, maxHp) {
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