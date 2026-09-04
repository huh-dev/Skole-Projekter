namespace Eventyrerlauget.Interfaces;

public interface ISpellCaster : IDamageable
{
    int maxMana { get; }
    int currentMana { get; }

    void CastSpell(IDamageable target, IDiceRoller diceRoller);
}