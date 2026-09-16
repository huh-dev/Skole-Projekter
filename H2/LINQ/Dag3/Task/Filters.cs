using LINQ.Dag3.Entities;

namespace LINQ.Dag3.Task;

public static class Filters
{

    //GET A PRODUCT BY ITS CATEGORY (DYNAMIC)
    public static IQueryable<Products> GetProductByCategory(this IQueryable<Products> products, string category)
    {
        return products.Where(p => p.category == category);
    }

    //FIND ALL PRODUCTS THAT COSTS MORE THAN A CERTAIN PRICE
    public static IQueryable<Products> GetProductByPrice(this IQueryable<Products> products, decimal price)
    {
        return products.Where(p => p.price > price);
    }

    //FIND ALL PRODUCTS THAT HAVE A CERTAIN PRICE RANGE
    public static IQueryable<Products> GetProductByPriceRange(this IQueryable<Products> products, decimal minPrice, decimal maxPrice)
    {
        return products.Where(p => p.price >= minPrice && p.price <= maxPrice);
    }

    //FIND ALL PRODUCTS THAT HAVE A CERTAIN CATEGORY AND COSTS LESS THAN A CERTAIN PRICE
    public static IQueryable<Products> GetProductByCategoryAndPrice(this IQueryable<Products> products, string category, decimal price)
    {
        return products.Where(p => p.category == category && p.price < price);
    }

    //FIND ALL PRODUCTS THAT CONTAIN A CERTAIN STRING IN THEIR NAME
    public static IQueryable<Products> GetProductByString(this IQueryable<Products> products, string str)
    {
        return products.Where(p => p.name.Contains(str));
    }

    //GET AMOUNT OF UNITS SOLD FOR A CATEGORY
    public static Dictionary<string, int> GetAmountOfUnitsSoldForCategory(this IQueryable<Products> products)
    {
        return products.GroupBy(p => p.category).ToDictionary(g => g.Key, g => g.Sum(p => p.units_sold));
    }
}