using Eventyrerlauget.Interfaces;
using Eventyrerlauget.Inventory;
namespace Eventyrerlauget.CharacterTypes;

public class Thief : Character, IDamageable
{
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }

    public Thief(string name, int level, int maxHp, Weapon? weapon = null, Armor? armor = null) 
        : base(name, level, maxHp, weapon, armor) {
        MaxHp = maxHp;
        CurrentHp = maxHp;
    }

    public override void Attack(IDamageable target, IDiceRoller diceRoller)
    {
        bool isSneakAttack = diceRoller.RollDice(20) > 15;
        
        int attackRole = diceRoller.RollDice(20);

        if (attackRole >= target.ArmorClass())
        {
            var damageDone = diceRoller.RollDice(WeaponDamage());

            if (isSneakAttack)
            {
                damageDone *= 2;
            }

            target.TakeDamage(damageDone);
        }
    }
}