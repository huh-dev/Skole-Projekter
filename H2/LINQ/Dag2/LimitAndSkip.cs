namespace LINQ.Dag2;

public static class LimitAndSkip
{
    //FIND THE HIGHTEST 3 PRODUCTS
    public static IEnumerable<Product> FindTheHighest3Products(this List<Product> products)
    {
        return products.OrderByDescending(p => p.price).Take(3);
    }

    //FIND THE LOWEST 5 PRODUCTS
    public static IEnumerable<Product> FindTheLowest5Products(this List<Product> products, int amount)
    {
        return products.OrderBy(p => p.price).Take(amount);
    }

    //SORT THE PRODUCTS BY PRICE DESCENDING BUT KEEP ONLY THE 4 TO 6 PRODUCTS
    public static IEnumerable<Product> SortTheProductsByPriceDescendingButKeepOnlyThe4To6Products(this List<Product> products)
    {
        return products.OrderByDescending(p => p.price).Skip(3).Take(3);
    }

    //SORT PRODUCTS BY PAGES WHERE THERE ARE ONLY 3 PRODUCTS PER PAGE
    public static IEnumerable<Product> SortProductsByPagesWhereThereAreOnly3ProductsPerPage(this List<Product> products, int page, int productsPerPage)
    {
        return products.OrderByDescending(p => p.price).Skip((page - 1) * productsPerPage).Take(productsPerPage);
    }
}