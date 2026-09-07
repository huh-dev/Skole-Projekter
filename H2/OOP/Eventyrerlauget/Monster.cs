using Eventyrerlauget.Interfaces;

namespace Eventyrerlauget;

public class Monster : IDamageable
{

    public string Name { get; private set; }
    public int Damage { get; private set; }
    public int armorClass { get; private set; }
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }

    public Monster(string name, int damage, int _armorClass, int maxHp)
    {
        Name = name;
        Damage = damage;
        armorClass = _armorClass;
        MaxHp = maxHp;
        CurrentHp = maxHp;
    }
    
    public void Attack(IDamageable target, IDiceRoller diceRoller)
    {
        int attackRole = diceRoller.RollDice(20);

        if (attackRole >= target.ArmorClass())
        {
            target.TakeDamage(diceRoller.RollDice(Damage));
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHp -= amount;
        if (CurrentHp < 0)
        {
            CurrentHp = 0;
        }
    }

    public void Heal(int amount)
    {
        CurrentHp += amount;
        if (CurrentHp > MaxHp)
        {
            CurrentHp = MaxHp;
        }
    }

    public int ArmorClass()
    {
        return armorClass;
    }
}