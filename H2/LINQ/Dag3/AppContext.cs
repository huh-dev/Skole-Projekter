using LINQ.Dag3.Entities;
using Microsoft.EntityFrameworkCore;
namespace LINQ.Dag3;

public class AppDbContext : DbContext
{
    public DbSet<Products> Products { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=linq_crud;User=root;Password=;Port=3306;",
                new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }
}