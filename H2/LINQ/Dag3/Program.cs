using System.ComponentModel.DataAnnotations;
using LINQ.Dag3.Task;
using LINQ.Dag3.Entities;
namespace LINQ.Dag3;

public class Program
{
    public static void Main(string[] args)
    {

        var database = Database.Connect();

        database.SaveChanges();

        BarChart.CreateBarChart(database.Products);


    }


}