namespace H2.Database.Dag3;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public static class Database
{
    public static AppDbContext Connect()
    {
        return new AppDbContext();
    }
}