using Eventyrerlauget.Interfaces;

namespace Eventyrerlauget.CharacterTypes;

public class Wizard : Character, IDamageable
{
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }

    public Wizard(string name, int level, int maxHp) : base(name, level, maxHp) {
        MaxHp = maxHp;
        CurrentHp = maxHp;
    }

    public override void Attack(IDamageable target)
    {
        target.TakeDamage(10);
    }
}