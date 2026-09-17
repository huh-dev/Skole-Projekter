namespace LINQ.Dag4.Tasks;

public static class Filter
{
    //FIND ALL PRODUCTS WITH A CATEGORY
    public static IEnumerable<Product> FindAllProductsWithCategory(this IEnumerable<Product> products, string category)
    {
        return products.Where(p => p.category == category);
    }

    //FIND ALL PRODUCTS THAT COSTS MORE THAN A CERTAIN PRICE
    public static IEnumerable<Product> FindAllProductsMoreThanPrice(this IEnumerable<Product> products, decimal price)
    {
        return products.Where(p => p.price > price);
    }

    //FIND ALL PRODUCTS WITH A CERTAIN PRICE RANGE
    public static IEnumerable<Product> FindAllProductsInPriceRange(this IEnumerable<Product> products, decimal minPrice, decimal maxPrice)
    {
        return products.Where(p => p.price >= minPrice && p.price <= maxPrice);
    }

    //FIND ALL PRODUCTS WITH A CERTAIN CATEGORY AND COSTS MORE THAN A CERTAIN PRICE
    public static IEnumerable<Product> FindAllProductsInCategoryAndMoreThanPrice(this IEnumerable<Product> products, string category, decimal price)
    {
        return products.Where(p => p.category == category && p.price > price);
    }

    //FIND ALL PRODUCTS THAT CONTAIN A CERTAIN STRING IN THEIR NAME
    public static IEnumerable<Product> FindAllProductsContainingStringInName(this IEnumerable<Product> products, string str)
    {
        return products.Where(p => p.name.Contains(str));
    }
}