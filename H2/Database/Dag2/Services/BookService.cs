namespace Dag2.Services;

public class BookService
{
    public float Price { get; private set; }

    public void SetPrice(float price)
    {

        //Simple price check through if statement.
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Prisen skal være større end 0.");
        }

        Price = price;
    }
}