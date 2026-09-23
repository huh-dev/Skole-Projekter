using Dag3.Entities;
using Microsoft.EntityFrameworkCore;

namespace H2.Database.Dag3.Input;

public static class Selects
{
    public static Author GetSpecificAuthor(this AppDbContext db, int id)
    {
        return db.Authors
            .FromSqlRaw("CALL GetSpecificAuthor({0})", id)
            .AsEnumerable()
            .FirstOrDefault();
    }
}