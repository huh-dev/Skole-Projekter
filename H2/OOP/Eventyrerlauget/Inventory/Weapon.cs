namespace Eventyrerlauget.Inventory;

public class Weapon : Item
{
    public int damage { get; private set; }

    public Weapon(int damage)
    {
        this.damage = damage;
    }
}