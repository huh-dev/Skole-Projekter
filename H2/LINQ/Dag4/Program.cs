using LINQ.Dag4.Tasks;

namespace LINQ.Dag4;


public class Program
{
    public static void Main(string[] args)
    {
        // XmlCreation.CreateXml();
        var products = XmlCreation.ReadXml();

        
        var filteredProducts = products.FindAllProductsWithCategory("Computer");
        foreach (var product in filteredProducts)
        {
            Console.WriteLine(product.name);
        }
    }
}