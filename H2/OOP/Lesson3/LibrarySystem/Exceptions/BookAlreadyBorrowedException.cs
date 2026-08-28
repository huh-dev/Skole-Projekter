namespace LibrarySystem.Exceptions;

public class BookAlreadyBorrowedException : Exception
{
    public BookAlreadyBorrowedException() : base("Book is already borrowed")
    {
    }
}