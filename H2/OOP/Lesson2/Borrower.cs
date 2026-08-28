using System;

namespace Lesson02.Library
{
    /// <summary>
    /// Repræsenterer en låner (elev eller lærer) i skolebibliotekets system.
    /// Klassen demonstrerer auto-properties, brug af "this" til at adskille
    /// felt/parameter, samt indkapsling af en tæller (NumberOfBooksLoaned),
    /// der kun må ændres via klassens egne metoder.
    /// </summary>
    public class Borrower
    {
        // Konstant, der bruges internt til at håndhæve en forretningsregel
        // (se BorrowBook()). Private, fordi det er en implementeringsdetalje.
        private const int MaxNumberOfBooksLoaned = 5;

        // ----- Properties -----

        // Auto-property med både get og set: Name er noget, omverdenen
        // frit skal kunne læse OG ændre (fx hvis en elev skifter navn).
        // Vi validerer stadig i en lille hjælpe-property herunder ved at
        // gøre den til en fuld property i stedet for en ren auto-property.
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Navn må ikke være tomt.", nameof(value));
                }
                _name = value;
            }
        }

        // Auto-property med kun get: et lånernummer må ALDRIG ændres,
        // efter objektet er oprettet. Det sættes udelukkende i konstruktøren.
        public string BorrowerNumber { get; }

        // Auto-property med kun get udefra, men "private set" gør, at
        // klassen selv (i BorrowBook/ReturnBook) frit kan ændre værdien.
        // Det er samme mønster som IsOnLoan i Book.cs.
        public int NumberOfBooksLoaned { get; private set; }

        // ----- Konstruktør -----

        /// <summary>
        /// Opretter en ny låner. Bemærk brugen af "this." til at adskille
        /// parameteren "name" fra property'en "Name" (og tilsvarende for
        /// borrowerNumber), selvom de her faktisk hedder næsten det samme -
        /// det er netop den situation, hvor "this" gør koden entydig.
        /// </summary>
        public Borrower(string name, string borrowerNumber)
        {
            this.Name = name; // "this." er ikke strengt nødvendigt her, men gør
                               // det tydeligt, at vi sætter objektets egen property
            this.BorrowerNumber = borrowerNumber;
            this.NumberOfBooksLoaned = 0;
        }

        // ----- Metoder -----

        /// <summary>
        /// Registrerer, at låneren har lånt endnu en bog. Håndhæver
        /// forretningsreglen om maks. 5 lån ad gangen - dette er netop
        /// den slags kontrol af tilstand, indkapsling gør muligt: reglen
        /// kan kun håndhæves, fordi NumberOfBooksLoaned ikke kan ændres
        /// direkte udefra.
        /// </summary>
        public void BorrowBook()
        {
            if (NumberOfBooksLoaned >= MaxNumberOfBooksLoaned)
            {
                throw new InvalidOperationException(
                    $"{Name} har allerede lånt det maksimale antal bøger ({MaxNumberOfBooksLoaned}).");
            }

            NumberOfBooksLoaned++;
        }

        /// <summary>
        /// Registrerer, at låneren har afleveret en bog. Tælleren kan
        /// aldrig gå under 0.
        /// </summary>
        public void ReturnBook()
        {
            if (NumberOfBooksLoaned > 0)
            {
                NumberOfBooksLoaned--;
            }
        }

        public override string ToString()
        {
            return $"{Name} (lånernr. {BorrowerNumber}) - {NumberOfBooksLoaned} bog(er) lånt";
        }
    }
}
