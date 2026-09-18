
using System.Linq.Expressions;

namespace H2.LINQ.Dag1;

public record Product(string name, string category, int price);
public class Program
{

    //Dictionary list because of very fast lookup times for specfic keys.


    //Product name, Product category, Product price
    private static readonly List<Product> ProductsList = new()
    {
        new Product(name: "Gaming Laptop", category: "Computer", price: 12500),
        new Product(name: "Office Laptop", category: "Computer", price: 7500),
        new Product(name: "Keyboard", category: "Tilbehør", price: 650),
        new Product(name: "Office Mus", category: "Tilbehør", price: 1100),
        new Product(name: "4K Skærm", category: "Skærm", price: 4500),
    };

    public static void Main(string[] args)
    {

        // //Lambda Expressions
        // LambdaExpressions();

        // //Extension methods
        // var expensiveProducts = ProductsList.ExpensiveProducts();

        // foreach (var product in expensiveProducts)
        // {
        //     Console.WriteLine($"{product.name} costs {product.price} and is a {product.category}");
        // }

        // //Anonymous types
        // AnonymousTypes();

        // //Query operators
        // QueryOperators();

        // //Query expression
        // QueryExpression();

        //Expression tree
        ExpressionTree();

    }


    //MARK: Lambda expressions   
    private static void LambdaExpressions()
    {
        //Lambda expression if a product costs more than 5000 (BOOL)
        var isProductMoreThan5000 = (int price) => price > 5000;

        /*
        * Lambda expression to get all products that costs more than 5000
        * We use a where here to filter the products based on a previous lanmda expression. This is not normally how i would do it.
        * It would usually do the bool check inside the where clause itself.
        */
        var productsMoreThan5000 = ProductsList.Where(p => isProductMoreThan5000(p.price));

        /*
        * Lambda expression to get products with a price over 10000 or have the category "Skærm"
        * Here is how i would do it normally, but i would make an extension method for this, and take the category and price in as parameters, so it would be a more flexible, reuseable and dynamic solution / method.
        */
        var productsOver10000OrCategorySkærm = ProductsList.Where(p => p.price > 10000 || p.category == "Skærm");

        /*
        * Lambda expression to get procuts that costs between 1000 and 10000 and does not have the category "Tilbehør"
        * Again this would also be nicer as a dynamic solution / method
        */
        var ProductsBetween1000And10000NotTilbehør = ProductsList.Where(p => p.price > 1000 && p.price < 10000 && p.category != "Tilbehør");


        /*
        * Lambda expression to get procuts that costs between 1000 and 10000 and does not have the category "Tilbehør" and sort by price
        * Here we have an orderby clause at the end, to sort the products. The orderby expression, so the 
        */
        var ProductsBetween1000And10000NotTilbehørSorted = ProductsList.Where(p => p.price > 1000 && p.price < 10000 && p.category != "Tilbehør").OrderBy(p => p.price);
        
        // foreach (var product in ProductsBetween1000And10000NotTilbehørSorted)
        // {
        //     Console.WriteLine($"{product.name} costs {product.price} and is a {product.category}");
        // }
    }

    //MARK: Anonymous types
    private static void AnonymousTypes()
    {
        

        /**
        * These are anonymous types, the way that these work is that the list is a list of objects
        * They are used for the purpose og being a type that we dont define them as strings, ints etc. but that is something that the compilers do
        */
        var products = new[]
        { 
            new { Name = "Gaming Laptop", Category = "Computer", Price = 12500 },
            new { Name = "Office Laptop", Category = "Computer", Price = 7500 },
        };

        /**
        * We use a select here, with a new object / instance of an anonymous type. 
        * We do this to preview how it would work for a real use case where we dont need or dont want to know how the objects are structured.
        */
        var results = products.Select(p => new { p.Name, p.Category, p.Price });

        foreach (var result in results)
        {
            Console.WriteLine($"{result.Name} costs {result.Price} and is a {result.Category}");
        }

    }


    //MARK: QUERY OPERATORS
    private static void QueryOperators()
    {
        /**
        * This is the query syntax. We are using a from clause to iterate over the ProductsList, and a where clause to filter the products based on the category and price.
        * We are then using an orderby clause to sort the products by price in descending order.
        * We are then using a select clause to select the products.
        * This is made to show how in C# we can use a more declarative approach to querying data, instead of using a more imperative approach.
        */
        var expensiveProductsQuerySyntax = from product in ProductsList
                                where product.category == "Computer" && product.price > 1000
                                orderby product.price descending
                                select product;
        
        /**
        * This is the method syntax. We are using a where clause to filter the products based on the category and price.
        * We are then using an orderby clause to sort the products by price in descending order.
        * We are then using a select clause to select the products.
        * This is made to show how in C# we can use a more imperative approach to querying data, instead of using a more declarative approach.
        */
        var expensiveProductsMethodSyntax = ProductsList.Where(p => p.price > 1000 && p.category == "Computer").OrderBy(p => p.price);

        foreach (var product in expensiveProductsMethodSyntax)
        {
            Console.WriteLine($"{product.name} costs {product.price} and is a {product.category}");
        }

        //Count the number of products
        Console.WriteLine($"Number of products: {expensiveProductsMethodSyntax.Count()}");

        //The highest priced product
        Console.WriteLine($"Highest priced product: {expensiveProductsMethodSyntax.Max(p => p.price)}");


    }


    //MARK: Query expression
    private static void QueryExpression()
    {
        /**
        * This is a query expression. Again it shows the same declarative approach to querying data, as the query syntax.
        */
        IEnumerable<Product> expensiveProductsQueryExpression = from product in ProductsList
                                                                orderby product.price descending
                                                                select product;
        
        foreach (var product in expensiveProductsQueryExpression)
        {
            Console.WriteLine($"{product.name} costs {product.price} and is a {product.category}");
        }
    }

    //MARK: Expression Trees
    private static void ExpressionTree()
    {

        /**
        * This is a simple expression that would be used in a larger tree. The tree would be used to compile the expression into a delegate
        * That can be used in combination with other expressions to create a more complex tree with multiple conditions and parameters.
        * This is a good example of how LINQ can be used with more of its functions, because inside the expression we have a lambda expression.
        * Underneath we compile the expression, that then gives us a a IEnumberable<Product> that we can use to show the given result.
        */
        Expression<Func<Product, bool>> isProductMoreThan5000 = p => p.price > 5000 && p.category == "Computer";

        var compliedExpression = isProductMoreThan5000.Compile();

        Console.WriteLine(compliedExpression(ProductsList[0]));

    }

}

public static class ExtensionMethods
{
    /**
    * This is an extension method. It is a method that is added to a class, that is not part of the class itself.
    * It is a way to add functionality to a class, without having to modify the class itself.
    * It is a good example of how LINQ can be used with more of its functions, because inside the method we have a lambda expression.
    * Underneath we use the Where method to filter the products based on the price.
    */
    public static IEnumerable<Product> ExpensiveProducts(this List<Product> products)
    {
        return products.Where(p => p.price > 5000);
    }
}
