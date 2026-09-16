using LINQ.Dag3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LINQ.Dag3.Task;

public static class Crud
{
    //ADD A NEW PRODUCT TO THE DATABASE
    public static EntityEntry AddProduct(this DbSet<Products> products, Products product)
    {
        return products.Add(product);
    }

    //FETCH ALL PRODUCTS FROM THE DATABASE
    public static IQueryable<Products> GetAllProducts(this DbSet<Products> products)
    {
        return products;
    }

    //CHANGE THE PRICE OF A PRODUCT
    public static Products UpdateProductPrice(this DbSet<Products> products, int id, decimal price)
    {
        var product = products.Find(id);

        if (product == null)
        {
            throw new Exception("Product not found");
        }
        
        product.price = price;
        products.Update(product);
        return product;
    }

    //DELETE A PRODUCT FROM THE DATABASE
    public static void DeleteProduct(this DbSet<Products> products, int id)
    {
        var product = products.Find(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        products.Remove(product);
    }

    //UPDATE PRODUCTS WITH A RANDOM NUMBER OF UNITS SOLD
    public static IQueryable<Products> UpdateProductUnitsSold(this DbSet<Products> products)
    {
        foreach (var product in products)
        {
            product.units_sold = new Random().Next(1, 100);
            products.Update(product);
        }
        
        return products;
    }
}