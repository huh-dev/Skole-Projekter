namespace Eventyrerlauget.Interfaces;

public interface ISpellCaster
{
    int maxMana { get; }
    int currentMana { get; }

    void CastSpell(IDamageable target);
}