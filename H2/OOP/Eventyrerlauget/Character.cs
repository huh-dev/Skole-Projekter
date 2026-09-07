using Eventyrerlauget.Inventory;
using Eventyrerlauget.Interfaces;

namespace Eventyrerlauget;

public abstract class Character
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }
    public Inventory.Inventory Inventory { get; private set; }
    public Dictionary<string, Item> Equipment { get; set; }

    public Character(string name, int level, int maxHp, Weapon? weapon = null, Armor? armor = null)
    {
        Name = name;
        Level = level;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        Inventory = new Inventory.Inventory();
        Equipment = new Dictionary<string, Item>();
        Equipment.TryAdd("weapon", weapon);
        Equipment.TryAdd("armor", armor);
    }

    public abstract void Attack(IDamageable target, IDiceRoller diceRoller)
    {
        int attackRole = diceRoller.RollDice(20);

        if (attackRole >= target.ArmorClass())
        {
            target.TakeDamage(diceRoller.RollDice(WeaponDamage()))
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

    public void TakeDamage(int amount)
    {
        CurrentHp -= amount;

        if (CurrentHp < 0)
        {
            CurrentHp = 0;
        }
    }

    public int ArmorClass()
    {
        Equipment.TryGetValue("armor",  out Item armor);

        Armor castArmor = armor as Armor;
        
        return castArmor.armorClass ?? 10;
    }

    private int WeaponDamage()
    {
        Equipment.TryGetValue("weapon", out Item weapon);

        Weapon castWeapon = weapon as Weapon;
        
        return castWeapon.damage ?? 6;
    }
}