using LINQ.Dag3.Entities;
using LINQ.Dag3.Task;
using Spectre.Console;

namespace LINQ.Dag3;

public static class BarChart
{
    public static void CreateBarChart(this IQueryable<Products> products)
    {

        var amountOfUnitsSoldForCategory = Filters.GetAmountOfUnitsSoldForCategory(products);
        
        var chart = new Spectre.Console.BarChart()
            .Label("Products");

        foreach (var item in amountOfUnitsSoldForCategory)
        {
            chart.AddItem(item.Key, item.Value, Color.Green);
        }

        AnsiConsole.Write(chart);
    }
}