namespace Eventyrerlauget.Inventory;

public class Armor : Item
{
    public int armorClass { get; private set; }

    public Armor(int armorClass)
    {
        this.armorClass = armorClass;
    }
}