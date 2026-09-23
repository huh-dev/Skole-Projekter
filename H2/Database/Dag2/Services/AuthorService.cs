using Dag2.Entities;
using Microsoft.EntityFrameworkCore;
namespace Dag2.Services;

public class AuthorService
{
    private readonly AppDbContext _context;

    public AuthorService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Author> Delete(int authorId)
    {

        //Simple book count through LINQ
        var bookCount = await _context.Books.CountAsync(b => b.AuthorId == authorId);
        
        if (bookCount > 0)
        {
            throw new InvalidOperationException("Forfatter har bog(er) tilknyttet.");
        }

        //Here we find the author to delete, and if not found we throw a simple invalid exception.
        var author = await _context.Authors.FindAsync(authorId)
            ?? throw new InvalidOperationException("Forfatter ikke fundet.");
        
        //We remove the author from the db and save the changes.
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        
        return author;
    }


}