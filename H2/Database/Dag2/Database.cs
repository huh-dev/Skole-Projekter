using System.Text.Json;
using System.Text.Json.Serialization;
using Dag2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace H2.Database.Dag2;

public static class Database
{
    public static AppDbContext Connect()
    {
        return new AppDbContext();
    }

    //MARK: AUTHORS
    public static async Task SeedData()
    {
        var database = Connect();

        //Options for JsonSerializer
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var authors = JsonSerializer.Deserialize<List<Author>>(File.ReadAllText("Data/authors.json"), options);
        var staff = JsonSerializer.Deserialize<List<Staff>>(File.ReadAllText("Data/staff.json"), options);
        var users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText("Data/users.json"), options);
        var books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText("Data/books.json"), options);

        database.Authors.AddRange(authors ?? []);
        database.Staff.AddRange(staff ?? []);
        database.Users.AddRange(users ?? []);
        database.Books.AddRange(books ?? []);

        await database.SaveChangesAsync();
    }
}