using Dag2.Entities;

namespace Dag2.Services;

public class LoanService
{

    private readonly AppDbContext _context;

    public LoanService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoanedBook> Create(int userId, int bookId, int staffId, DateTime loanStart, DateTime loanEnd)
    {
        if (loanStart > loanEnd)
        {
            throw new ArgumentException("Låneperioden kan ikke være negativ.");
        }

        var loan = new LoanedBook
        {
            UserId = userId,
            BookId = bookId,
            StaffId = staffId,
            LoanStart = loanStart,
            LoanEnd = loanEnd
        };
        
        _context.LoanedBooks.Add(loan);
        await _context.SaveChangesAsync();
        return loan;
    }

    public async Task<LoanedBook> GetById(int loanId, Staff? currentStaff)
    {
        if (currentStaff is null || string.IsNullOrWhiteSpace(currentStaff.Role))
        {
            throw new UnauthorizedAccessException("Kun staff kan se udlån.");
        }

        return await _context.LoanedBooks.FindAsync(loanId);
    }

}