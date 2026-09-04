namespace Eventyrerlauget.Interfaces;

public interface IDamageable
{
    int MaxHp { get; }
    int CurrentHp { get; }


    void TakeDamage(int amount);
    void Heal(int amount);
}