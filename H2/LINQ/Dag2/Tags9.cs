namespace LINQ.Dag2;

public static class Tags
{
    //FIND ALL TAGS FROM ALL PRODUCTS (OPTIONAL: REMOVE DUPLICATES)
    public static IEnumerable<string> FindAllTagsFromAllProductsWithTags(this List<Product> products, bool removeDuplicates = true)
    {
        return removeDuplicates ? products.SelectMany(p => p.tags).Distinct() : products.SelectMany(p => p.tags);
    }

    //FIND ALL PRODUCTS WITH A SPECIFIC TAG
    public static IEnumerable<Product> FindAllProductsWithATag(this List<Product> products, string tag)
    {
        return products.Where(p => p.tags.Contains(tag));
    }

    //COUNT ALL THE DIFFERENT TAGS IN MY PRODUCTS
    public static int CountAllTheDifferentTagsInMyProducts(this List<Product> products)
    {
        return products.SelectMany(p => p.tags).Distinct().Count();
    }

    //THE TAGS USED ON PRODUCTS WITH A HIGHER PRICE
    public static IEnumerable<string> TheTagsUsedOnProductsWithASpecificPrice(this List<Product> products, decimal price)
    {
        return products.Where(p => p.price > price).SelectMany(p => p.tags);
    }
}