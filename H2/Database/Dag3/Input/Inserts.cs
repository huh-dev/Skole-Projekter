using Dag3.Entities;
using H2.Database.Dag3.Enums;
using H2.Database.Dag3.Services.Authorization;
namespace H2.Database.Dag3.Input;
using Microsoft.EntityFrameworkCore;

public static class Inserts
{
    public static void InsertAuthor(this AppDbContext db, IAuthorizationService authorizationService)
    {

        Console.WriteLine("Write the name of the author");
        string name = Console.ReadLine();

        // Check that name is not empty
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Name is required");
            return;
        }

        // Check that the user is authorized to insert an author
        if (!authorizationService.IsAuthorizedStaff())
        {
            Console.WriteLine("You are not authorized to insert an author");
            return;
        }

        // LINQ for inserting the author which is already safe for sql injection
        db.Authors.Add(new Author() { Name = name });
        db.SaveChanges();

        Console.WriteLine("Author inserted successfully");
    }
}
