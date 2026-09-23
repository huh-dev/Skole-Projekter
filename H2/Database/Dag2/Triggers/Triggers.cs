using Dag2.Entities;
using Microsoft.EntityFrameworkCore;

namespace H2.Database.Dag2.Triggers;

public static class Triggers
{
    public static void CreateTriggers(this AppDbContext context)
    {
        /*
        We start by getting the trigger file path, and later read the entire file. After that we run an EF Core command called "ExecuteSqlRaw" to execute the trigger in its entirety.
        We can do it like this, because we have the entire sql of the trigger in the file. So by doing this, is the same as running an sql file in the database.
        */
        var triggersFile = Path.Combine(Directory.GetCurrentDirectory(), "Triggers", "loan_trigger.sql");
        var triggers = File.ReadAllText(triggersFile);
        context.Database.ExecuteSqlRaw(triggers);
    }
}