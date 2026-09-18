namespace LINQ.Dag2;

public static class Sort
{
    //SORT PRODUCTS BY PRICE ASCENDING
    public static IEnumerable<Product> SortProductsByPriceAscending(this List<Product> products)
    {
        return products.OrderBy(p => p.price);
    }

    //SORT PRODUCTS BY PRICE DESCENDING
    public static IEnumerable<Product> SortProductsByPriceDescending(this List<Product> products)
    {
        return products.OrderByDescending(p => p.price);
    }

    //SORT PRODUCTS BY CATEGORY 
    public static IEnumerable<Product> SortProductsByCategory(this List<Product> products)
    {
        return products.OrderBy(p => p.category);
    }

    //SORT PRODUCTS BASED ON CATEGORY AND AFTER THAT PRICE OR NAME (DYNAMIC)
    public static IEnumerable<Product> SortProductsByCategoryAndPriceOrName(this List<Product> products, bool isName = false)
    {
        return products.OrderBy(p => p.category).ThenBy(p => isName ? p.name : p.price as IComparable);
    }
}