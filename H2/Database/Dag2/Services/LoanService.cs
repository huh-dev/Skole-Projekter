using Dag2.Entities;

namespace Dag2.Services;

public static class LoanService
{


    public static async Task<LoanedBook> Create(this AppDbContext context, int userId, int bookId, int staffId, DateTime loanStart, DateTime loanEnd)
    {
        //We check if the loan period is valid.
        if (loanStart > loanEnd)
        {
            throw new ArgumentException("Låneperioden kan ikke være negativ.");
        }

        //We create a new loan object with the params given from the program, so we later can add the loan to the db.
        var loan = new LoanedBook
        {
            UserId = userId,
            BookId = bookId,
            StaffId = staffId,
            LoanStart = loanStart,
            LoanEnd = loanEnd
        };
        
        //We add the loan to the db and save the changes.
        context.LoanedBooks.Add(loan);
        await context.SaveChangesAsync();

        return loan;
    }

    public static async Task<LoanedBook> GetById(this AppDbContext context, int loanId, Staff? currentStaff)
    {
        //We check if the current staff is valid.
        if (currentStaff is null || string.IsNullOrWhiteSpace(currentStaff.Role))
        {
            throw new UnauthorizedAccessException("Kun staff kan se udlån.");
        }

        //We find the loan by id and return it.
        return await context.LoanedBooks.FindAsync(loanId);
    }

}