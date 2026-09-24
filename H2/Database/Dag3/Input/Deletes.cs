using Dag3.Entities;
using H2.Database.Dag3.Enums;
using H2.Database.Dag3.Services.Authorization;

namespace H2.Database.Dag3.Input;

public static class Deletes
{
    public static void DeleteAuthor(this AppDbContext db, IAuthorizationService authorizationService)
    {
        if (!authorizationService.IsAuthorizedAdmin(StaffRoles.Administrator))
        {
            Console.WriteLine("You are not authorized to delete an author");
            return;
        }

        Console.WriteLine("Write the id of the author you want to delete");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid id");
            return;
        }

        var author = db.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            Console.WriteLine("Author not found");
            return;
        }

        db.Authors.Remove(author);
        db.SaveChanges();

        Console.WriteLine("Author deleted successfully");
    }
}
