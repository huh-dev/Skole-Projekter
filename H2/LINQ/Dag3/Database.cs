using LINQ.Dag3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LINQ.Dag3;

public static class Database
{
    public static AppDbContext Connect()
    {
        var connectionString = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("Default");

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        var context = new AppDbContext(optionsBuilder.Options);


        context.Database.EnsureCreated();

        //INSERT PRODUCTS IN DB (ONLY RUN ONCE IF DB IS EMPTY)
        if (!context.Products.Any())
        {
            InsertProducts();

            context.SaveChanges();
        }


        return context;
    }


    //MARK: Insert Products
    public static void InsertProducts()
    {
        var context = Connect();

        context.Products.AddRange(
            new Products
            {
                name = "Gaming Laptop", category = "Computer", price = 12500m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "Office Laptop", category = "Computer", price = 7500m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "Gaming Mus", category = "Tilbehør", price = 650m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "Keyboard", category = "Tilbehør", price = 1100m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "4K Skærm", category = "Skærm", price = 4500m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "Gaming Headset", category = "Tilbehør", price = 1500m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "27\" Gaming Skærm", category = "Skærm", price = 3500m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "USB-C Dock", category = "Tilbehør", price = 1800m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "MacBook Air", category = "Computer", price = 9500m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "Gaming PC", category = "Computer", price = 15000m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "Webkamera", category = "Tilbehør", price = 850m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            },
            new Products
            {
                name = "32\" 4K Skærm", category = "Skærm", price = 5500m, created_at = DateTime.Now,
                updated_at = DateTime.Now
            }
        );

        Console.WriteLine("Products inserted successfully");
    }
}