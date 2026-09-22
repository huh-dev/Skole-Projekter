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

        
    }
}