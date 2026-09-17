namespace LINQ.Dag4;

public class Product
{
    public string name { get; set; }
    public decimal price { get; set; }
    public string category { get; set; }

    public Product() { }

    public Product(string name, decimal price, string category)
    {
        this.name = name;
        this.price = price;
        this.category = category;
    }
}