using Dag2.Services;
using H2.Database.Dag2.Triggers;

namespace H2.Database.Dag2;

class Program
{
    static async Task Main(string[] args)
    {
        var database = Database.Connect();
    
        //Seed data if db is empty
        if (!database.Authors.Any() && !database.Staff.Any() && !database.Users.Any() && !database.Books.Any())
        {
            await Database.SeedData();
        }

        //TEST AUTOMATIONS (create loaned book) This will trigger the loan_trigger.sql, and create a log in the database. But we will also save a log in the logs.txt file.
        // var loanedBook = await database.Create(2, 2, 1, DateTime.Now, DateTime.Now.AddDays(14));
        // Logs.Save(DateTime.Now, "Create", loanedBook.Id.ToString(), "loaned_books");

        //Create the triggers
        // database.CreateTriggers();

        
    }
}