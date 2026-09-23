using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace H2.Database.Dag3;
using Input;
using Services.Authorization;
using H2.Database.Dag3.StoredProcedures;

public static class Program
{
    public static void Main(string[] args)
    {

        var context = Database.Connect();

        // ICurrentUserService currentUserService = new CurrentUserService();
        // currentUserService.SetUser("Alice");
        // IAuthorizationService authorizationService = new AuthorizationService(context, currentUserService);

        // Inserts.InsertAuthor(context, authorizationService);

        // context.CreateStoredProcedures();


        // try
        // {
        //     var author = Selects.GetSpecificAuthor(context, -1);


        //     if (author == null)
        //     {
        //         Console.WriteLine("Author not found");
        //         return;
        //     }

        //     Console.WriteLine($"Author: {author.Name}");

        // }
        // catch (MySqlException ex)
        // {
        //     Console.WriteLine(ex.Message);
        // }


    }
}