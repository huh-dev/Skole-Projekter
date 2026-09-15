namespace LINQ.Dag2;

//DTO RECORD
public record Product(string name, decimal price, string category, IReadOnlyList<string> tags);


class Program
{
    //DATA SOURCE
    private static readonly List<Product> Products = new()
    {
        new Product("Gaming Laptop", 12500m, "Computer", ["Gaming", "Laptop", "High Performance"]),
        new Product("Office Laptop", 7500m, "Computer", ["Office", "Laptop", "Portable"]),
        new Product("Gaming Mus", 650m, "Tilbehør", ["Gaming", "Mouse", "Accessory"]),
        new Product("Keyboard", 1100m, "Tilbehør", ["Keyboard", "Accessory", "Input"]),
        new Product("4K Skærm", 4500m, "Skærm", ["4K", "Monitor", "Display"]),
        new Product("Gaming Headset", 1500m, "Tilbehør", ["Gaming", "Headset", "Audio"]),
        new Product("27\" Gaming Skærm", 3500m, "Skærm", ["27 inch", "Gaming", "Monitor"]),
        new Product("USB-C Dock", 1800m, "Tilbehør", ["USB-C", "Dock", "Accessory"]),
        new Product("MacBook Air", 9500m, "Computer", ["MacBook", "Laptop", "Apple"]),
        new Product("Gaming PC", 15000m, "Computer", ["Gaming", "PC", "High Performance"]),
        new Product("Webkamera", 850m, "Tilbehør", ["Webcam", "Camera", "Accessory"]),
        new Product("32\" 4K Skærm", 5500m, "Skærm", ["32 inch", "4K", "Monitor"]),
    };


    static void Main(string[] args)
    {
        GenerateRapport();
    }

    
    //MARK: RAPPORT
    public static void GenerateRapport()
    {

        //PRODUCTS
        Console.WriteLine();
        Console.WriteLine("PRODUCTS");
        Console.WriteLine("--------------------------------");
        
        Console.WriteLine($"Total products: {Products.CountAllProducts()}");
        Console.WriteLine($"Cheapest product: {Products.Find(p => p.price == Products.GetLowestPriceOfAllProducts()).name} - {Products.GetLowestPriceOfAllProducts()}");
        Console.WriteLine($"Most expensive product: {Products.Find(p => p.price == Products.GetHighestPriceOfAllProducts()).name} - {Products.GetHighestPriceOfAllProducts()}");
        Console.WriteLine($"Average price: {Products.GetAveragePriceOfAllProducts()}");
        Console.WriteLine($"Total price: {Products.GetTotalPriceOfAllProducts()}");

        Console.WriteLine("--------------------------------");
        Console.WriteLine();

        //Categories

        Console.WriteLine("CATEGORIES");
        Console.WriteLine("--------------------------------");
     
        var categories = Products.GroupProductsByCategory();
        foreach (var category in categories)
        {
            Console.WriteLine($"Category: {category.Key}");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Count         : {category.Count()}");
            Console.WriteLine($"Average Price : {category.Average(p => p.price):N2} kr.");
            Console.WriteLine($"Highest Price : {category.Max(p => p.price):N2} kr.");
            Console.WriteLine($"Lowest Price  : {category.Min(p => p.price):N2} kr.");
            Console.WriteLine();
        }
   

        Console.WriteLine("--------------------------------");

        //Top Products
        Console.WriteLine("TOP PRODUCTS");
        Console.WriteLine("--------------------------------");

        var topProducts = Products.FindTheHighest3Products();
        Console.WriteLine("Top 3 highest priced products:");
        foreach (var product in topProducts)
        {
            Console.WriteLine($"  {product.name} - {product.price:N2} kr.");
        }

        Console.WriteLine();

        var lowestProducts = Products.FindTheLowest5Products(3);
        Console.WriteLine("Top 3 lowest priced products:");
        foreach (var product in lowestProducts)
        {
            Console.WriteLine($"  {product.name} - {product.price:N2} kr.");
        }


        Console.WriteLine("--------------------------------");

        //PRODUCT FILTERING
        Console.WriteLine("PRODUCT FILTERING");
        Console.WriteLine("--------------------------------");

        var filteredProducts = Products.GetProductsByCategoryOrPrice("Computer", 10000m, true);
        Console.WriteLine("Filtered products by category and price:");
        foreach (var product in filteredProducts)
        {
            Console.WriteLine($"  {product.name} - {product.price:N2} kr.");
        }

        Console.WriteLine();

        var filteredProductsByName = Products.GetProductsByName("Gaming");
        Console.WriteLine("Filtered products by name:");
        foreach (var product in filteredProductsByName)
        {
            Console.WriteLine($"  {product.name} - {product.price:N2} kr.");
        }


        Console.WriteLine("--------------------------------");

        //TAGS
        Console.WriteLine("TAGS");
        Console.WriteLine("--------------------------------");

        var tags = Products.FindAllTagsFromAllProductsWithTags();
        Console.WriteLine("All tags:");
        foreach (var tag in tags)
        {
            Console.WriteLine($"  {tag}");
        }

        Console.WriteLine();
        Console.WriteLine($"Count of all tags: {Products.CountAllTheDifferentTagsInMyProducts()}");

        Console.WriteLine();
        var tagsWithHigherPrice = Products.TheTagsUsedOnProductsWithASpecificPrice(5000m);
        Console.WriteLine("Tags used on products with a higher than 5000 kr price:");
        foreach (var tag in tagsWithHigherPrice)
        {
            Console.WriteLine($"  {tag}");
        }

        Console.WriteLine("--------------------------------");


    }
}