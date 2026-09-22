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
        var bookCount = await _context.Books.CountAsync(b => b.AuthorId == authorId);
        
        if (bookCount > 0)
        {
            throw new InvalidOperationException("Forfatter har bog(er) tilknyttet.");
        }

        var author = await _context.Authors.FindAsync(authorId)
            ?? throw new InvalidOperationException("Forfatter ikke fundet.");
        
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return author;
    }


}