using Eventyrerlauget.Interfaces;

namespace Eventyrerlauget;

public class Monster : IDamageable
{

    public string Name { get; private set; }
    public int Damage { get; private set; }
    public int ArmorClass { get; private set; }
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }

    public Monster(string name, int damage, int armorClass, int maxHp)
    {
        Name = name;
        Damage = damage;
        ArmorClass = armorClass;
        MaxHp = maxHp;
        CurrentHp = maxHp;
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
}