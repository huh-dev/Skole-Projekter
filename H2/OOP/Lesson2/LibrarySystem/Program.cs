using System.Text.Json;
using System.IO;

namespace LibrarySystem;

class Program
{

    static Library library = new Library();

    static List<Book> LoadBooks()
    {
        string json = File.ReadAllText("books.json");
        return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
    }

    static void Main(string[] args)
    {
        //Load books from JSON
        List<Book> books = LoadBooks();

        //Set books to library
        library.Books = books;

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

        string title;
        try
        {
            title = Console.ReadLine();
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid title, please enter a valid title");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return;
        }


        List<Book> books = library.SearchBook(title);

        if (books.Count == 1)
        {
            Book book = books.FirstOrDefault()!;
            library.BorrowBook(book);
        }
        else if (books.Count > 1)
        {
            Console.WriteLine("Multiple books found");
            Console.WriteLine("--------------------------------");
            foreach (Book b in books)
            {
                Console.WriteLine($"{b.Title} by {b.Author} (ISBN: {b.ISBN}) - {(b.IsBorrowed ? "Borrowed" : "Available")}");
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Enter the ISBN of the book you want to borrow: ");

            string isbn;

            try
            {
                isbn = Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid ISBN, please enter a valid ISBN");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Book book = books.FirstOrDefault(b => b.ISBN == isbn);

            if (book == null)
            {
                Console.WriteLine("Book not found");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            library.BorrowBook(book);
        }
        else
        {
            Console.WriteLine("Book not found");
        }
        
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
        
        string title;
        try
        {
            title = Console.ReadLine();
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid title, please enter a valid title");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        List<Book> books = library.SearchBook(title);
        
        if (books.Count == 1)
        {
            Book book = books.FirstOrDefault()!;
            library.ReturnBook(book);
        }
        else if (books.Count > 1)
        {
            Console.WriteLine("Multiple books found");
            Console.WriteLine("--------------------------------");
            foreach (Book b in books)
            {
                Console.WriteLine($"{b.Title} by {b.Author} (ISBN: {b.ISBN}) - {(b.IsBorrowed ? "Borrowed" : "Available")}");
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Enter the ISBN of the book you want to return: ");
            string isbn;
            try
            {
                isbn = Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid ISBN, please enter a valid ISBN");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Book book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null)
            {
                Console.WriteLine("Book not found");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            library.ReturnBook(book);
        }
        else
        {
            Console.WriteLine("Book not found");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
  
}