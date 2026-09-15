namespace LINQ.Dag2;

//DTO RECORD
public record Product(string name, decimal price, string category);


class Program
{
    //DATA SOURCE
    private static readonly List<Product> Products = new()
    {
        new Product("Gaming Laptop", 12500m, "Computer"),
        new Product("Office Laptop", 7500m, "Computer"),
        new Product("Gaming Mus", 650m, "Tilbehør"),
        new Product("Keyboard", 1100m, "Tilbehør"),
        new Product("4K Skærm", 4500m, "Skærm"),
        new Product("Gaming Headset", 1500m, "Tilbehør"),
        new Product("27\" Gaming Skærm", 3500m, "Skærm"),
        new Product("USB-C Dock", 1800m, "Tilbehør"),
        new Product("MacBook Air", 9500m, "Computer"),
        new Product("Gaming PC", 15000m, "Computer"),
        new Product("Webkamera", 850m, "Tilbehør"),
        new Product("32\" 4K Skærm", 5500m, "Skærm"),
    };


    static void Main(string[] args)
    {

        var categories = Products.FindAllCategories();
        foreach (var category in categories)
        {
            Console.WriteLine(category);
        }
        Console.WriteLine("Kategorier: " + categories.Count());

        Console.WriteLine("--------------------------------");


    }
}