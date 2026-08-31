using LibrarySystem.Exceptions;

namespace LibrarySystem;

class Program
{

    static Library library = new Library();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to the Library System");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Vis alle bøger");
            Console.WriteLine("2. Søg efter en bog");
            Console.WriteLine("3. Lån en bog");
            Console.WriteLine("4. Returner en bog");
            Console.WriteLine("5. Exit");

    
            
            int choice = 0;
            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice, please enter a number");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            switch (choice)
            {
                case 1:
                    library.ShowAllBooks();
                    break;
                case 2:
                    SearchBook();
                    break;
                case 3:
                    BorrowBook();
                    break;
                case 4:
                    ReturnBook();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

    }

    static void SearchBook()
    {
        Console.Clear();
        Console.WriteLine("Search for a book");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Enter the title of the book: ");

        string title = Console.ReadLine();
        List<Book> books = library.SearchBook(title);
        
       
        if (books.Count > 0)
        {
            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN}) - {(book.IsBorrowed ? "Borrowed" : "Available")}");
            }
        }
        else
        {
            Console.WriteLine("Book not found");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    static void BorrowBook()
    {
        Console.Clear();
        Console.WriteLine("Borrow a book");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Enter the title of the book: ");

        BookLogicComponent("borrow");

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
    

    static void ReturnBook()
    {
        Console.Clear();
        Console.WriteLine("Return a book");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Enter the title of the book: ");
        
        BookLogicComponent("return");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }


    static void BookLogicComponent(string type)
    {
        string? title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Invalid title, please enter a valid title");
            return;
        }

        List<Book> books = library.SearchBook(title.Trim());
        Book? book;

        if (books.Count == 1)
        {
            book = books[0];
        }
        else if (books.Count > 1)
        {
            Console.WriteLine("Multiple books found");
            Console.WriteLine("--------------------------------");
            foreach (Book b in books)
            {
                Console.WriteLine(
                    $"{b.Title} by {b.Author} (ISBN: {b.ISBN}) - {(b.IsBorrowed ? "Borrowed" : "Available")}");
            }

            Console.WriteLine("--------------------------------");
            string action = type == "borrow" ? "borrow" : "return";
            Console.WriteLine($"Enter the ISBN of the book you want to {action}: ");

            string? isbn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("Invalid ISBN, please enter a valid ISBN");
                return;
            }

            book = books.FirstOrDefault(b => b.ISBN == isbn.Trim());
            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }
        }
        else
        {
            Console.WriteLine("Book not found");
            return;
        }

        try
        {
            if (type == "return")
            {
                library.ReturnBook(book);
            }
            else if (type == "borrow")
            {
                library.BorrowBook(book);
            }
        }
        catch (BookAlreadyBorrowedException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}