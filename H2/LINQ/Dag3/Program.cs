using System.ComponentModel.DataAnnotations;
using LINQ.Dag3.Task;
using LINQ.Dag3.Entities;
namespace LINQ.Dag3;

public class Program
{
    public static void Main(string[] args)
    {

        var database = Database.Connect();

        // database.Products.AddSpecificationsToProduct(2, new string[] { 
        //     "brand: Lenovo", 
        //     "processor: Intel Core i7",
        //     "ram: 16 GB",
        //     "storage: 1 TB SSD"
        // });

        // database.Products.AddSpecificationsToProduct(6, new string[] {
        //     "brand: Samsung",
        //     "resolution: 3840x2160",
        //     "size: 27\"",
        //     "refreshRate: 144 Hz"
        // });

        // database.Products.AddSpecificationsToProduct(12, new string[]
        // {
        //     "brand: Lenovo",
        //     "type: Webkamera",
        //     "resolution: 1080p",
        //     "frameRate: 60 fps"
        // });
   
        // var product = database.Products.FindProductBySpecification("Lenovo");

        // foreach (var p in product)
        // {
        //     Console.WriteLine("Product found: " + p.name);
        // }

        BarChart.CreateBarChart(database.Products);

        database.SaveChanges();


    }


}