using Dag3.Entities;
using Microsoft.EntityFrameworkCore;
namespace H2.Database.Dag3.StoredProcedures;

public static class StoredProcedures
{
    public static void CreateStoredProcedures(this AppDbContext db)
    {
        var storedProceduresFile = Path.Combine(Directory.GetCurrentDirectory(), "StoredProcedures", "stored_procedures.sql");
        var storedProcedures = File.ReadAllText(storedProceduresFile);
        db.Database.ExecuteSqlRaw(storedProcedures);
    }
}