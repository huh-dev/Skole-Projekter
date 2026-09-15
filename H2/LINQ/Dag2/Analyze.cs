namespace LINQ.Dag2;

public static class Analyze
{
    //COUNT ALL PRODUCTS
    public static int CountAllProducts(this List<Product> products)
    {
        return products.Count();
    }

    //THE PRICE FOR ALL PRODUCTS
    public static decimal GetTotalPriceOfAllProducts(this List<Product> products)
    {
        return products.Sum(p => p.price);
    }

    //THE AVERAGE PRICE FOR ALL PRODUCTS
    public static decimal GetAveragePriceOfAllProducts(this List<Product> products)
    {
        return products.Average(p => p.price);
    }

    //THE LOWEST PRICE FOR ALL PRODUCTS
    public static decimal GetLowestPriceOfAllProducts(this List<Product> products)
    {
        return products.Min(p => p.price);
    }

    //THE HIGHEST PRICE FOR ALL PRODUCTS
    public static decimal GetHighestPriceOfAllProducts(this List<Product> products)
    {
        return products.Max(p => p.price);
    }

    //THE COUNT OF PRODUCTS IN THE CATEGORY "COMPUTER"
    public static int GetCountOfProductsInCategoryComputer(this List<Product> products)
    {
        return products.Count(p => p.category == "Computer");
    }

    //THE AVERGE PRICE FOR PRODUCTS IN THE CATEGORY TILBEHØR
    public static decimal GetAveragePriceOfProductsInCategoryTilbehor(this List<Product> products)
    {
        return products.Where(p => p.category == "Tilbehør").Average(p => p.price);
    }


}