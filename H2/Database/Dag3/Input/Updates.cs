using Dag3.Entities;
using H2.Database.Dag3.Enums;
using H2.Database.Dag3.Services.Authorization;
namespace H2.Database.Dag3.Input;

public static class Updates
{
    public static void UpdateAuthor(this AppDbContext db, IAuthorizationService authorizationService)
    {
        
        //Auth
        if (!authorizationService.IsAuthorizedStaff())
        {
            Console.WriteLine("You are not authorized to update an author");
            return;
        }

        //The id of the author to update
        Console.WriteLine("Write the id of the author you want to update");
        int id = int.Parse(Console.ReadLine());
        
        //Does the author exist?
        var author = db.Authors.FirstOrDefault(a => a.Id == id);

        if (author == null)
        {
            throw new Exception("Author not found");
        }

        //The new name of the author
        Console.WriteLine("Write the new name of the author");
        string name = Console.ReadLine();

        //Update the author
        author.Name = name;
        db.SaveChanges();

        Console.WriteLine("Author updated successfully");


        
    }
}