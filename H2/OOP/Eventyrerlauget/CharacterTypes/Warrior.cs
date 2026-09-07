using Eventyrerlauget.Interfaces;
using Eventyrerlauget.Inventory;
namespace Eventyrerlauget.CharacterTypes;

public class Warrior : Character, IDamageable
{

    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }


    public Warrior(string name, int level, int maxHp, Weapon? weapon = null, Armor? armor = null) 
        : base(name, level, maxHp, weapon, armor)
    {
        MaxHp = maxHp;
        CurrentHp = maxHp;
    }
}