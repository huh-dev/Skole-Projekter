using dag4.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace H2.Database.Dag4;

public static class Database
{
    public static AppDbContext Connect()
    {
        return new AppDbContext();
    }
}