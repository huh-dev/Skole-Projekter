using Eventyrerlauget.Interfaces;

namespace Eventyrerlauget.CharacterTypes;

public class Wizard : Character, ISpellCaster
{
    public int maxMana { get; private set; }
    public int currentMana { get; private set; }

    public Wizard(string name, int level, int maxHp) : base(name, level, maxHp) {
        maxMana = 100;
        currentMana = maxMana;
    }

    public override void Attack(IDamageable target, IDiceRoller diceRoller)
    {
        target.TakeDamage(diceRoller.RollDice(10));
    }


    public void CastSpell(IDamageable target, IDiceRoller diceRoller)
    {
        target.TakeDamage(diceRoller.RollDice(10));
    }
}