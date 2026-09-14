using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.VisualBasic;

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

        //Lambda expression to get all products that costs more than 5000
        var productsMoreThan5000 = ProductsList.Where(p => isProductMoreThan5000(p.price));

        // foreach (var product in productsMoreThan5000)
        // {
        //     Console.WriteLine($"{product.name} costs {product.price}");
        // }

        //Lambda expression to get products with a price over 10000 or have the category "Skærm"
        var productsOver10000OrCategorySkærm = ProductsList.Where(p => p.price > 10000 || p.category == "Skærm");

        // foreach (var product in productsOver10000OrCategorySkærm)
        // {
        //     Console.WriteLine($"{product.name} costs {product.price} and is a {product.category}");
        // }

        // Lambda expression to get procuts that costs between 1000 and 10000 and does not have the category "Tilbehør"
        var ProductsBetween1000And10000NotTilbehør = ProductsList.Where(p => p.price > 1000 && p.price < 10000 && p.category != "Tilbehør");

        // foreach (var product in ProductsBetween1000And10000NotTilbehør)
        // {
        //     Console.WriteLine($"{product.name} costs {product.price} and is a {product.category}");
        // }

        // Lambda expression to get procuts that costs between 1000 and 10000 and does not have the category "Tilbehør" and sort by price
        var ProductsBetween1000And10000NotTilbehørSorted = ProductsList.Where(p =>
            p.price > 1000 && p.price < 10000 && p.category != "Tilbehør").OrderBy(p => p.price);
        
        // foreach (var product in ProductsBetween1000And10000NotTilbehørSorted)
        // {
        //     Console.WriteLine($"{product.name} costs {product.price} and is a {product.category}");
        // }
    }

    //MARK: Anonymous types
    private static void AnonymousTypes()
    {
        

        var products = new[]
        { 
            new { Name = "Gaming Laptop", Category = "Computer", Price = 12500 },
            new { Name = "Office Laptop", Category = "Computer", Price = 7500 },
        };

        var results = products.Select(p => new { p.Name, p.Category, p.Price });

        foreach (var result in results)
        {
            Console.WriteLine($"{result.Name} costs {result.Price} and is a {result.Category}");
        }

    }


    //MARK: QUERY OPERATORS
    private static void QueryOperators()
    {
        //Query syntax
        var expensiveProductsQuerySyntax = from product in ProductsList
                                where product.category == "Computer" && product.price > 1000
                                orderby product.price descending
                                select product;
        
        //Method syntax
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

        Expression<Func<Product, bool>> isProductMoreThan5000 = p => p.price > 5000;

    
        var compliedExpression = isProductMoreThan5000.Compile();

        Console.WriteLine(compliedExpression(new Product(name: "Gaming Laptop", category: "Computer", price: 12500)));
    }
}

public static class ExtensionMethods
{
    public static IEnumerable<Product> ExpensiveProducts(this List<Product> products)
    {
        return products.Where(p => p.price > 5000);
    }
}
