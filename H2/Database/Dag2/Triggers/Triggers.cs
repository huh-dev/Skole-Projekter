using Dag2.Entities;
using Microsoft.EntityFrameworkCore;

namespace H2.Database.Dag2.Triggers;

public static class Triggers
{
    public static void CreateTriggers(this AppDbContext context)
    {
        var triggersFile = Path.Combine(Directory.GetCurrentDirectory(), "Triggers", "loan_trigger.sql");
        var triggers = File.ReadAllText(triggersFile);
        context.Database.ExecuteSqlRaw(triggers);
    }
}