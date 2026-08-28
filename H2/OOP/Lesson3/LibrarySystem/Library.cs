using LibrarySystem.Exceptions;

namespace LibrarySystem;

public class Library
{
    public List<Book> Books { get; set; }

    public void ShowAllBooks()
    {
        Console.Clear();
        Console.WriteLine("All Books in the Library");
        Console.WriteLine("--------------------------------");
        foreach (Book book in Books)
        {
            Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN}) - {(book.IsBorrowed ? "Borrowed" : "Available")}");
        }
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public void BorrowBook(Book book)
    {
        if (book.IsBorrowed)
        {
            throw new BookAlreadyBorrowedException();
        }

        book.IsBorrowed = true;
        Console.WriteLine($"Book {book.Title} borrowed successfully");
    }

    public void ReturnBook(Book book)
    {
        if (!book.IsBorrowed)
        {
            Console.WriteLine($"Book {book.Title} is not borrowed");
            return;
        }

        book.IsBorrowed = false;
        Console.WriteLine($"Book {book.Title} returned successfully");
    }

    public List<Book> SearchBook(string title)
    {
        return Books.Where(book => book.Title.ToLower().Contains(title.ToLower())).ToList();
    }
}
