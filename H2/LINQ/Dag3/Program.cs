using LINQ.Dag3.Task;
using LINQ.Dag3.Entities;
namespace LINQ.Dag3;

public class Program
{
    public static void Main(string[] args)
    {

        var database = Database.Connect();

        var updatedProducts = database.Products.UpdateProductPrice(10, 125000m);

        database.Products.DeleteProduct(10);

        database.SaveChanges();


    }


}