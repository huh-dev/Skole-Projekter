namespace LINQ.Dag2;

public static class Conditions
{
    //DOES A PRODUCT EXSIST THAT COSTS MORE THAN 10000
    public static bool DoesAProductExistThatCostsMoreThan10000(this List<Product> products)
    {
        return products.Any(p => p.price > 10000m);
    }
    
    //DOES A PRODUCT EXSIST THAT HAVE THE CATEGORY "SKÆRM"
    public static bool DoesAProductExistThatHaveTheCategoryScreen(this List<Product> products)
    {
        return products.Any(p => p.category == "Skærm");
    }

    //DOES ALL PRODUCTS COST MORE THAN 500
    public static bool DoesAllProductsCostMoreThan500(this List<Product> products)
    {
        return products.All(p => p.price > 500);
    }

    //DOES ALL COMPUTERS COST MORE THAN 5000
    public static bool DoesAllComputersCostMoreThan5000(this List<Product> products)
    {
        return products.All(p => p.category == "Computer" && p.price > 5000m);
    }


}