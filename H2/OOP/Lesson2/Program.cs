using System;

namespace Lesson02.Library
{
    /// <summary>
    /// Demonstrationsprogram: viser hvordan Book- og Borrower-objekter
    /// erklæres og instantieres, hvordan overloadede konstruktører bruges,
    /// og hvordan indkapslingen i klasserne forhindrer ugyldig brug.
    /// Brug denne fil som inspiration til Lesson 2's opgave (opgave.md).
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("--- Instantiering af objekter ---");

            // Objekt 1: bruger hovedkonstruktøren (alle oplysninger kendt)
            Book book1 = new Book("1984", "George Orwell", "9788711539329", 1949);

            // Objekt 2: bruger overload uden udgivelsesår
            Book book2 = new Book("Fahrenheit 451", "Ray Bradbury", "9780345342966");

            // Objekt 3: bruger den korteste overload (kun titel og forfatter)
            Book book3 = new Book("Kladdehæfte om løs kobling", "Ukendt Forfatter");

            Console.WriteLine(book1);
            Console.WriteLine(book2);
            Console.WriteLine(book3);

            // To Borrower-objekter
            Borrower borrower1 = new Borrower("Amina Hansen", "L-1001");
            Borrower borrower2 = new Borrower("Mikkel Poulsen", "L-1002");

            Console.WriteLine();
            Console.WriteLine("--- Udlån ---");

            // Borrower1 låner book1
            book1.CheckOut();
            borrower1.BorrowBook();
            Console.WriteLine($"{borrower1.Name} har nu lånt: {book1}");

            // Forsøg på at udlåne samme bog igen - indkapslingen forhindrer
            // ugyldig tilstand ved at kaste en exception, som vi fanger her.
            try
            {
                book1.CheckOut();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Fejl fanget: {ex.Message}");
            }

            // Bemærk: følgende linjer ville IKKE kompilere, fordi IsOnLoan
            // og NumberOfBooksLoaned kun har "get" udefra - det er indkapsling
            // i praksis, håndhævet allerede af compileren:
            //
            // book1.IsOnLoan = false;
            // borrower1.NumberOfBooksLoaned = 100;

            Console.WriteLine();
            Console.WriteLine("--- Aflevering ---");

            book1.Return();
            borrower1.ReturnBook();
            Console.WriteLine($"{borrower1.Name} har nu lånt: {borrower1.NumberOfBooksLoaned} bog(er)");
            Console.WriteLine(book1);

            Console.WriteLine();
            Console.WriteLine("--- Forsøg på ugyldig oprettelse ---");
            try
            {
                Book invalidBook = new Book("", "En Forfatter");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fejl fanget: {ex.Message}");
            }
        }
    }
}
