namespace LINQ.Dag2;

public static class Group
{
    //GROUP PRODUCTS BY CATEGORY
    public static IEnumerable<IGrouping<string, Product>> GroupProductsByCategory(this List<Product> products)
    {
        return products.GroupBy(p => p.category);
    }

    //FIND THE COUNT OF PRODUCTS IN EACH CATEGORY
    public static IEnumerable<dynamic> FindTheCountOfProductsInEachCategory(this List<Product> products)
    {
        return products.GroupBy(p => p.category).Select(g => new { Category = g.Key, Count = g.Count() });
    }

    //FIND THE AVERAGE PRICE FOR PRODUCTS IN EACH CATEGORY
    public static IEnumerable<dynamic> FindTheAveragePriceOfProductsInEachCategory(this List<Product> products)
    {
        return products.GroupBy(p => p.category).Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.price) });
    }

    //FIND THE HIGHEST PRICE FOR PRODUCTS IN EACH CATEGORY
    public static IEnumerable<dynamic> FindTheHighestPriceForProductsInEachCategory(this List<Product> products)
    {
        return products.GroupBy(p => p.category).Select(g => new { Category = g.Key, HighestPrice = g.Max(p => p.price) });
    }
}