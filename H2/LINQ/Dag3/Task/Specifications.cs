using System.Text.Json;
using LINQ.Dag3.Entities;
using Microsoft.EntityFrameworkCore;

namespace LINQ.Dag3.Task;

public static class Specifications
{
    //ADD SPECIFICATIONS TO A PRODUCT
    public static Products AddSpecificationsToProduct(this DbSet<Products> products, int productId, string[] specifications)
    {
        var _product = products.FirstOrDefault(p => p.id == productId);

        if (_product == null)
        {
            throw new Exception("Product not found");
        }

        _product.specifications = JsonSerializer.Serialize(specifications);

        return _product;
    }

    //FIND A PRODUCT BY SPECIFICATION
    public static IQueryable<Products> FindProductBySpecification(this IQueryable<Products> products, string specification)
    {
        return products.Where(p => p.specifications.Contains(specification));

    }
}