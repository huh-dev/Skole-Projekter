using LINQ.Dag4.Tasks;

namespace LINQ.Dag4;


public class Program
{
    public static void Main(string[] args)
    {
        // XmlCreation.CreateXml();
        var products = XmlCreation.ReadXml();

        // products = products.CreateProduct(new Product("Test", 100, "Test"));

        var _products = products.FindAllProductsWithCategory("Computer");
        foreach (var product in _products)
        {
            Console.WriteLine(product.name);
        }
        // var laptop = products.First(p => p.name == "Gaming Laptop");
        // products = products.DeleteProduct(laptop);
        XmlCreation.WriteXml(products);
    }
}