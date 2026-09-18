namespace LINQ.Dag2;

public static class Filters
{

    //GET PRODUCTS BY CATEGORY OR/AND PRICE (DYNAMIC)
    public static IEnumerable<Product> GetProductsByCategoryOrPrice(this List<Product> products, string category, decimal price, bool isAnd = false)
    {
        return isAnd ? products.Where(p => p.category == category && p.price < price) : products.Where(p => p.category == category || p.price < price);
    }

    //GET PRODUCTS BY PRICE RANGE (DYNAMIC)
    public static IEnumerable<Product> GetProductsByPriceRange(this List<Product> products, decimal minPrice, decimal maxPrice)
    {
        return products.Where(p => p.price >= minPrice && p.price <= maxPrice);
    }

    //GET PRODUCTS BY NAME (DYNAMIC)
    public static IEnumerable<Product> GetProductsByName(this List<Product> products, string name)
    {
        return products.Where(p => p.name.Contains(name));
    }
}