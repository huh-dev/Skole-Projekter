using Eventyrerlauget.Inventory;

namespace Eventyrerlauget;

public abstract class Character
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }
    public Inventory.Inventory Inventory { get; private set; }
    public Dictionary<string, Item> Equipment { get; set; }

    public Character(string name, int level, int maxHp)
    {
        Name = name;
        Level = level;
        MaxHp = maxHp;
        CurrentHp = maxHp;
    }

    public abstract void Attack();

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
    
}