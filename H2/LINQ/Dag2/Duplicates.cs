namespace LINQ.Dag2;

public static class Duplicates
{
    //FIND ALL CATEGORIES
    public static IEnumerable<string> FindAllCategories(this List<Product> products)
    {
        return products.Select(p => p.category).Distinct();
    }
}