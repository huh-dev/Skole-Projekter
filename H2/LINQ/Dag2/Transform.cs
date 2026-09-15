namespace LINQ.Dag2;

public static class Transform
{
    //GET PRODUCT NAMES
    public static IEnumerable<string> GetProductNames(this List<Product> products)
    {
        return products.Select(p => p.name);
    }

    //GET PRODUCTS NAME AND PRICE
    public static IEnumerable<(string name, decimal price)> GetProductNamesAndPrice(this List<Product> products)
    {
        return products.Select(p => (p.name, p.price));
    }

    //GET PRODUCTS NAME AND PRICE AND CATEGORY ANONYMOUS TYPE
    public static IEnumerable<dynamic> GetProductNamesAndPriceAndCategory(this List<Product> products)
    {
        return products.Select(p => new { p.name, p.price, p.category });
    }

    //GET PRODUCTS PRICE AND NAME IN STRING
    public static IEnumerable<string> GetProductPriceAndName(this List<Product> products)
    {
        return products.Select(p => $"{p.name} koster {p.price} kr.");
    }
}