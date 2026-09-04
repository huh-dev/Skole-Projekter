namespace Eventyrerlauget.Inventory;



//NIKOLAI kig lige din bums <3 den skal ikke være idamageable tror vi under drink method
public class Potion : Item
{

    public void Drink(Character target)
    {
        target.Heal(10);
    }

}