using Dag3.Entities;
namespace H2.Database.Dag3.Input;
using Microsoft.EntityFrameworkCore;

public static class Inserts
{
    public static void InsertAuthor(this AppDbContext db)
    {

        Console.WriteLine("Write the name of the author");
        string name = Console.ReadLine();

        // Check that name is not empty
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Name is required");
            return;
        }

        // LINQ for inserting the author which is already safe for sql injection
        db.Authors.Add(new Author() { Name = name });
        db.SaveChanges();

        Console.WriteLine("Author inserted successfully");
    }
}