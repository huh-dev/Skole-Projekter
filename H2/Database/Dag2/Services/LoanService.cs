using Dag2.Entities;

namespace Dag2.Services;

public static class LoanService
{

    public static async Task<LoanedBook> Create(this AppDbContext context, int userId, int bookId, int staffId, DateTime loanStart, DateTime loanEnd)
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
        
        context.LoanedBooks.Add(loan);
        await context.SaveChangesAsync();
        return loan;
    }

    public static async Task<LoanedBook> GetById(this AppDbContext context, int loanId, Staff? currentStaff)
    {
        if (currentStaff is null || string.IsNullOrWhiteSpace(currentStaff.Role))
        {
            throw new UnauthorizedAccessException("Kun staff kan se udlån.");
        }

        return await context.LoanedBooks.FindAsync(loanId);
    }

}